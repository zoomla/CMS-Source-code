using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Content
{
    public class B_DocPermission
    {
        public B_DocPermission()
        {
            PK = initmod.PK;
            strTableName = initmod.TbName;
        }
        public string PK, strTableName;
        public DataTable dt = null;
        private M_DocPermission initmod = new M_DocPermission();

        //------------------Retrieve
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }

        /// <summary>
        /// 返回定义的用户组或用户信息1为用户组,2为用户资源的关联
        /// </summary>
        /// <returns></returns>
        public DataTable SelGroup(string status)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("status", status) };
            return Sql.Sel(strTableName, "[Status] = @status", "", sp);
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public PageSetting SelPage(int cpage, int psize, string status = "")
        {
            string where = " 1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(status)) { where += " AND [Status]=@status"; sp.Add(new SqlParameter("status", status)); }
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where, "", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 根据UserID，获取其组权限模型
        /// </summary>
        /// <returns></returns>
        public M_DocPermission SelByUserID(string userID)
        {
            //select * from ZL_DocPermission where id=(select OwnGroupID from ZL_DocPermission where UserID = '1' )
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("userID", userID) };
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, " Where id = (select OwnGroupID from ZL_DocPermission Where UserID = @userID)", sp))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    //如无记录，则返回默认组权限
                    return defaultGroup();
                }
            }
        }

        /// <summary>
        /// 返回默认组,现在先认为默认组ID为1
        /// </summary>
        /// <returns></returns>
        public M_DocPermission defaultGroup()
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, " Where id = 1"))
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

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public M_DocPermission SelReturnModel(int ID)
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

        /// <summary>
        /// 根据条件查询一条记录
        /// </summary>
        public M_DocPermission SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
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

        //select * from zl_user as a left join zl_DocPermission  as b on a.userid = b.userid and b.status =2 and OwnGroupID != 28 查询非本组成员
        /// <summary>
        /// 用于分配权限,查出非本组成员
        /// </summary>
        /// <returns></returns>
        public DataTable SelUser(string GroupID)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("GroupID", GroupID) };
            string strSql = "ZL_User as a left join ZL_DocPermission  as b on a.userid = b.userid and b.status =2 and OwnGroupID !=@GroupID";
            string where = "a.UserID not in(select UserID from ZL_DocPermission  where [status]=2 and OwnGroupID = @GroupID";
            return Sql.Sel(strSql, where, "", sp);
        }

        /// <summary>
        /// 获取该权限组下用户
        /// </summary>
        public DataTable SelUsersByGroupID(string GroupID)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("GroupID", GroupID) };
            string strSql = "ZL_User as a left join ZL_DocPermission  as b on a.userid = b.userid and b.status =2";
            string where = "OwnGroupID =@GroupID";
            if (GroupID.Equals("1")) { where += " OR OwnGroupID is null"; }
            return Sql.Sel(strSql, where, "", sp);
        }

        //-------------------Create
        /// <summary>
        /// 依据模型，插入一条数据,Parameter形式,返回ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(M_DocPermission model)
        {
            return Sql.insert(strTableName, model.GetParameters(), model.GetParas(), model.GetFields());
        }

        //-------------------Update
        /// <summary>
        /// 更改用户所在组，如无信息，则插入，有则更新
        /// </summary>
        public void MoveUsersGroup(string userID, string GroupID)
        {
            string strSql = "if exists(select * from ZL_DocPermission where UserID=@UserID)Update ZL_DocPermission set OwnGroupID=@GroupID where UserID=@UserID if not exists(select * from ZL_DocPermission where UserID=@UserID)  Insert into ZL_DocPermission (UserID,[status],OwnGroupID) values(@UserID,'2',@GroupID) ";
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
        public bool UpdateModel(M_DocPermission model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), model.GetFieldAndPara(), model.GetParameters());
        }

        //-------------------Delete

        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }

        public bool Del(string strWhere)
        {
            return Sql.Del(strTableName, strWhere);
        }
    }
}
