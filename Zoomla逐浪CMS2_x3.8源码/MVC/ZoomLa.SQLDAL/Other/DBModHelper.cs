using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace ZoomLa.SQLDAL
{
    /*实例化后使用,配合DBHelper,用于对数据库的直接操作
     * 数据库相关业务逻辑操作
     * //CreateDatabaseUser("user01", "admin");//创建SQL登录名
     * //CreateDatabase("newDataBase");//创建指定数据库
     * //CreateUserMap("newDataBase", "user01");//设置新创建的SQL用户的映射
     */
    public class DBModHelper
    {
        public string dataSource, userName, passwd;//@".\SQL2008",sa,sa::数据库源,数据库管理员名称,密码 
        //管理员链接，使用其操作master数据库
        public string connectionString;
        private SqlConnection connection = new SqlConnection();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="u">数据库管理员用户名,必须有master的管理权限</param>
        /// <param name="p">数据库管理员密码</param>
        /// <param name="dataSourc">数据来源,默认为.</param>
        public DBModHelper(string u = "sa", string p = "sa", string dataSource = "(local)")
        {
            userName = u;
            passwd = p;
            connectionString = string.Format(@"Data Source={0};User ID={1};Password={2};Initial Catalog={3};Pooling=false", @dataSource, userName, passwd, "master");
        }
        /// <summary>
        /// 以连接字符串初始化
        /// @"Data Source={0};Initial Catalog=Master;Integrated Security=True;"
        /// </summary>
        /// <param name="dataSource"></param>
        public DBModHelper(string connstr)
        {
            connectionString = connstr;
        }
        //--------------------------Create
        /// <summary>
        /// 创建用户，无任何权限，需要添加数据库映射
        /// </summary>
        /// <param name="userName">需要添加的用户名</param>
        /// <param name="passwd">密码</param>
        public void CreateDatabaseUser(string userName, string passwd, string dbname = "master")
        {
            //用于连接master数据库的
            //执行语句，添加用户
            string commandText = @"EXEC master.dbo.sp_addlogin @loginame = N'" + userName + "', @passwd = N'" + passwd + "', @defdb = N'" + dbname + "'";
            DBHelper.ExecuteSQL(connectionString, commandText);//执行创建数据库的TSQL；
        }
        /// <summary>
        /// 添加数据库
        /// </summary>
        public void CreateDatabase(string dbName)
        {
            DBHelper.DB_Create(connectionString, dbName);
            ////创建数据库
            //string commandText = string.Format("CREATE DATABASE [{0}]", dbName);
            //SqlHelper.ExecuteNonQuery(connectionString, CommandType.Text, commandText, null);//执行创建数据库的TSQL；
        }
        /// <summary>
        /// 给指定数据库添加用户映射
        /// </summary>
        /// <param name="dbName">数据库名(必须存在)</param>
        /// <param name="userName">用户名(必须存在)</param>
        public void CreateUserMap(string dbName, string userName)
        {
            string commandText = string.Format("USE [{0}];EXEC dbo.sp_grantdbaccess @loginame = N'{1}', @name_in_db = N'{1}';EXEC sp_addrolemember 'db_datareader', '{1}';EXEC sp_addrolemember 'db_datawriter', '{1}';EXEC sp_addrolemember 'db_ddladmin', '{1}';EXEC sp_addrolemember 'db_owner', '{1}'", dbName, userName);
            try
            {
                DBHelper.ExecuteSQL(connectionString, commandText, null);//设置新创建的SQL用户的映射
            }
            catch
            {
                throw new Exception("创建数据库用户映射失败，请检查数据库管理员权限！或者该用户映射已经存在！");
            }
        }
        //-------------------------Retrieve
        /// <summary>
        /// 从连接字符串中获取信息:数据库名,用户名，密码，
        /// </summary>
        public string[] GetInfoFromConnStr(string connStr)
        {
            //<add name="Connection String" connectionString="Data Source=(local);Initial Catalog=Zoomla6x;User ID=Zoomla6x;Password=Zoomla6x" providerName="System.Data.SqlClient" />
            string[] dbInfo = new string[3];
            string[] connect1 = connStr.Split(new string[] { "Initial Catalog=" }, StringSplitOptions.RemoveEmptyEntries);
            string[] connect = connect1[1].Split(new string[] { ";User ID=" }, StringSplitOptions.RemoveEmptyEntries);//Zoomla6x;User ID=Zoomla6x;Password=Zoomla6x
            dbInfo[0] = connect[0];
            dbInfo[1] = connect[1].Split(';')[0];//sfsf;Password=Zoomla6x
            dbInfo[2] = connect[1].Split('=')[1];
            return dbInfo;
        }
        /// <summary>
        /// 数据库是否存在,大于0为存在,-1为连接不正常
        /// </summary>
        public int DBIsExist(string dbName)
        {
            int result = 0;
            try
            {
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("dbName", dbName) };
                string tempConnStr = string.Format(@"Data Source={0};User ID={1};Password={2};Initial Catalog={3};Pooling=false", dataSource, userName, passwd, dbName);
                string commandText = string.Format("SELECT DB_ID(@dbName)", dbName);
                SqlHelper.ExecuteReader(connection, CommandType.Text, commandText, sp);
            }
            catch
            {
                result = -1;
                return result;
            }
            return result;

        }
        //-------------------------Delete
        //EXEC sp_revokedbaccess N'Zdb_sfsssww_admin'   --移除用户对数据库的访问权限
        //EXEC sp_droplogin N'Zdb_sfsssww_admin'        --删除登录用户
        /// <summary>
        /// 删除指定数据库
        /// </summary>
        public bool DelDB(string dbName)
        {
            bool flag = false;
            string commandText = string.Format(@"USE [master]; Drop DataBase {0}", dbName);
            SqlHelper.ExecuteNonQuery(connectionString, CommandType.Text, commandText, null);
            flag = true;
            return flag;
        }
        /// <summary>
        /// 删除数据库用户
        /// </summary>
        public bool DelUserByName(string userName)
        {
            bool flag = false;
            //if (ZoomlaSecurityCenter.CheckData(userName)) { throw new Exception("包含不允许的字符"); }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("userName", userName) };
            string commandText = @"USE [master]; EXEC sp_droplogin @userName";
            SqlHelper.ExecuteNonQuery(connectionString,CommandType.Text, commandText, sp);
            flag = true;
            return flag;
        }
    }
}
