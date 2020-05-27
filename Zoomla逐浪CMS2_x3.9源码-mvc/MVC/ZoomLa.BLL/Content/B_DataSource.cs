using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZoomLa.Model;
using ZoomLa.Model.Content;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Content
{
    public class B_DataSource
    {
        public B_DataSource() 
        {
            PK = initmod.PK;
            strTableName = initmod.TbName;
        }
        public string PK, strTableName;
        public DataTable dt = null;
        private M_DataSource initmod = new M_DataSource();

        //------------------Retrieve
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public PageSetting SelPage(int cpage, int psize, int id = 0)
        {
            string where = " 1=1";
            if (id > 0) { where += " AND " + PK + "=" + id; }
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where);
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public M_DataSource SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        //select * from zl_user as a left join zl_DataSource  as b on a.userid = b.userid and b.status =2 and OwnGroupID != 28 查询非本组成员
        /// <summary>
        /// 用于分配权限,查出非本组成员
        /// </summary>
        /// <returns></returns>
        public DataTable SelUser(string GroupID)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("GroupID", GroupID) };
            string strSql = "ZL_User as a left join ZL_DataSource  as b on a.userid = b.userid and b.status =2 and OwnGroupID != @GroupID";
            string where = "a.UserID not in(select UserID from ZL_DataSource  where [status]=2 and OwnGroupID = @GroupID";
            return Sql.Sel(strSql, where, "",sp);
        }
        /// <summary>
        /// 获取该权限组下用户
        /// </summary>
        public DataTable SelUsersByGroupID(string GroupID)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("GroupID", GroupID) };
            string strSql = "ZL_User as a left join ZL_DataSource  as b on a.userid = b.userid and b.status =2";
            string where = "OwnGroupID=@GroupID";
            if (GroupID.Equals("1")) { where += " OR OwnGroupID is null"; }
            return Sql.Sel(strSql, where, "",sp);
        }
        //-------------------Create
        /// <summary>
        /// 依据模型，插入一条数据,Parameter形式,返回ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(M_DataSource model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        //-------------------Update
        /// <summary>
        /// 更改用户所在组，如无信息，则插入，有则更新
        /// </summary>
        public void MoveUsersGroup(string userID, string GroupID)
        {
            string strSql = "if exists(select * from ZL_DataSource where UserID=@UserID)Update ZL_DataSource set OwnGroupID=@GroupID where UserID=@UserID if not exists(select * from ZL_DataSource where UserID=@UserID)  Insert into ZL_DataSource (UserID,[status],OwnGroupID) values(@UserID,'2',@GroupID) ";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@UserID",userID),
                 new SqlParameter("@GroupID",GroupID)
            };
            Sql.insert(strSql, sp);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        public bool UpdateModel(M_DataSource model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        //-------------------Delete
        public bool DeleteByID(int ID)
        {
            return Sql.Del(strTableName, PK+"="+ID);
        }
        public bool Del(string strWhere)
        {
            return Sql.Del(strTableName, strWhere);
        }
        /// <summary>
        /// 用于标签
        /// </summary>
        /// <param name="type">数据源类型或数据源ID</param>
        public static SqlBase GetDSByType(string type)
        {
            B_DataSource dsBll = new B_DataSource();
            switch (type)
            {
                case "main":
                case "second":
                case "new":
                    return DBCenter.DB;
                default:
                    M_DataSource dsMod = dsBll.SelReturnModel(Convert.ToInt32(type));
                    SqlBase db = SqlBase.CreateHelper(dsMod.Type);
                    db.ConnectionString = dsMod.ConnectionString;
                    return db;
            }
        }
    }
}
