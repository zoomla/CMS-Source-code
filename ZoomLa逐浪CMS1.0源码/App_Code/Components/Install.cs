namespace ZoomLa.Components
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;

    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.IO;
    using System.Reflection;
    using System.Diagnostics;
    using System.Collections;
    using System.Xml;
    using System.Text;
    using ZoomLa.BLL;
    using ZoomLa.Model;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using System.Net;
    /// <summary>
    /// Install 的摘要说明
    /// </summary>
    public class Install
    {
        private string server;
        private string uid;
        private string pwd;
        private string database;
        private string conn;  
        /// <summary>
        /// 在这里进行字符串的切割，获取服务器，登录名，密码，数据库
        /// </summary>      
       public Install()
        {
            //conn = System.Configuration.ConfigurationManager.AppSettings["Connection String"].ToString();
            //server = StringCut(conn,"server=",";");
            //uid = StringCut(conn,"uid=",";");
            //pwd = StringCut(conn,"pwd=",";");
            //database = StringCut(conn,"database=",";");           
        }
       public static void DbRestore2(string sqlname)//database=master;
        {
           //string str = "CREATE DATABASE " + sqlname;
            string str = sqlname;
            string constr = "Server=(local);Initial Catalog=ZoomLa;uid=ZoomLa;pwd=ZoomLa";//Initial Catalog=ZoomLa;|ZoomLa
            using (SqlConnection con = new SqlConnection(constr))
            {               
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();
               //con.ChangeDatabase();
                try
                {
                    cmd.ExecuteNonQuery();

                }
                catch(SqlException ee)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }         
      
        public static void WriteXml(string dataname,string xmlpath)
        {
            XmlDocument xmlDoc = new XmlDocument(); 
            xmlDoc.LoadXml(xmlpath);
            XmlNode xmldocSelect = xmlDoc.SelectSingleNode("configuration");
            XmlNodeList amend = xmldocSelect.SelectSingleNode("connectionStrings").ChildNodes;
            foreach (XmlNode xn in amend)
            {
                XmlElement xe = (XmlElement)xn;
                xe.SetAttribute("connectionString", "00");
            };
        }     
       

        /// <summary>
        ///  执行sql脚本写入数据库至新建的数据库中 (二)
        /// </summary>
        /// <param name="fileName">Server.MapPath("../../App_Data/test.sql")</param>
        /// <param name="connectString"></param>
        /// <returns></returns>
      public static bool CreateDataBase(string fileName, string connectString)
        {
            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();
            connection.Open();
            command.Connection = connection;
            using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
            {
                try
                {
                    while (!reader.EndOfStream)
                    {
                        StringBuilder builder = new StringBuilder();
                        while (!reader.EndOfStream)
                        {
                            string str = reader.ReadLine();
                            if (!string.IsNullOrEmpty(str) && str.ToUpper().Trim().Equals("GO"))
                            {
                                break;
                            }
                            builder.AppendLine(str);
                        }
                        command.CommandType = CommandType.Text;
                        command.CommandText = builder.ToString();
                        command.CommandTimeout = 300;
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException)
                {
                    return false;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                    connection.Dispose();
                }
            }
            return true;
        }
        /// <summary>
        /// 重写管理员信息
        /// </summary>
        /// <param name="ManagerName"></param>
        /// <param name="ManagerPwd"></param>
        /// <returns></returns>
        public static bool Add(string ManagerName, string ManagerPwd)
        {        
             string strHostIP = "";
            IPHostEntry oIPHost = Dns.GetHostEntry(Environment.MachineName);
            if (oIPHost.AddressList.Length > 0)
                strHostIP = oIPHost.AddressList[0].ToString();
            M_AdminInfo madmin = new M_AdminInfo();            
            madmin.AdminName = ManagerName;
            madmin.AdminPassword= StringHelper.MD5(ManagerPwd);
            madmin.RoleList = "0";
            madmin.UserName = ManagerName;
            madmin.LastLoginIP = strHostIP;
            madmin.Theme = "";
            madmin.EnableModifyPassword = true;
            madmin.EnableMultiLogin = true;
            madmin.RandNumber = String.Empty;            
            return (B_Admin.Add(madmin));
        }
        /// <summary>
        /// 连接sql
        /// </summary>
        /// <returns></returns>
        public static SqlConnection Connection(string sql)
        {

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = sql;
                con.Open();
                return con;
            }
            catch (SqlException ee)
            {

                return null;
                //throw ee;
            }
            finally
            {
                con.Close();
            }
        }
        public static bool Execute(string sqlPath, string constr)
        {           
            ArrayList strsql = ExecuteSql(sqlPath);
            return DbRestore3(strsql, constr);
        }
        public static bool DbRestore3(ArrayList alSql, string constr)
        {
            int len = alSql.Count;
            string str = "";
            //string constr = constr;// "Server=(local);uid=www;pwd=www";//Initial Catalog=ZoomLa;|ZoomLa
            if (Connection(constr) != null)
            {
                using (SqlConnection con = Connection(constr))// new SqlConnection(constr))
                {
                    con.Open();
                    SqlTransaction varTrans = con.BeginTransaction();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.Transaction = varTrans;
                    try
                    {
                        foreach (string varcommandText in alSql)
                        {
                            str = varcommandText;
                            cmd.CommandText = varcommandText;
                            cmd.ExecuteNonQuery();
                        }
                        varTrans.Commit();
                        return true;
                    }
                    catch (SqlException ee)
                    {
                        varTrans.Rollback();
                        return false;
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }

            }
            else
                return false;

        }
        public static ArrayList ExecuteSql(string sqlfile)//string
        {
            string commandText = "";
            string varLine = "";
            ArrayList alSql = new ArrayList();
            //alSql.Add(database);//脚本首句,指定使用的数据库
            string allsql = "";
            try
            {
                FileStream loading = new FileStream(sqlfile, FileMode.Open);
                StreamReader sr = new StreamReader(loading, System.Text.Encoding.Default);//strm);
                while (sr.Peek() >= 0)//-1
                {
                    varLine = sr.ReadLine();
                    allsql += varLine;
                    if (varLine == "")
                    {
                        continue;
                    }
                    if (varLine != "GO")
                    {
                        commandText += varLine;
                        commandText += "\r\n";
                        //allsql += commandText;
                    }
                    else
                    {
                        alSql.Add(commandText);//arraylist 分段保存sql完整语句
                        commandText = "";
                    }
                }
                sr.Close();
                loading.Close();
                return alSql;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
             
    }

}
