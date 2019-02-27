using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.BLL.Plat
{
    public class B_User_Token:ZL_Bll_InterFace<M_User_Token>
    {
        public string TbName, PK;
        M_User_Token iniModel = new M_User_Token();
        public B_User_Token()
        {
            this.TbName = iniModel.TbName;
            this.PK = iniModel.PK;
        }
        public int Insert(M_User_Token model)
        {
           return DBCenter.Insert(model);
        }

        public bool UpdateByID(M_User_Token model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public void InsertORUpdate(M_User_Token model) 
        {
            if (model.id > 0)
                UpdateByID(model);
            else
                Insert(model);
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public DataTable SelByUid(int uid)
        {
            string sql = "Select * from " + TbName + " Where uid=" + uid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        public M_User_Token SelReturnModel(int ID)
        {
            using (SqlDataReader dr = Sql.SelReturnReader(TbName, PK, ID))
            {
                if (dr.Read())
                {
                    return iniModel.GetModelFromReader(dr);
                }
                else
                {
                    return null;
                }
            }
        }
        public M_User_Token SelModelByUid(int uid)
        {
            using (SqlDataReader dr = Sql.SelReturnReader(TbName, " Where Uid=" + uid))
            {
                if (dr.Read())
                {
                    return iniModel.GetModelFromReader(dr);
                }
                else
                {
                    return null;
                }
            }
        }
        //-------------------------
        /// <summary>
        /// 依据OPENID,获取用户信息
        /// </summary>
        /// <param name="openID">OpenID</param>
        /// <param name="account">QQ|WX</param>
        /// <returns></returns>
        public M_User_Token SelByOpenID(string openID, string account = "QQ")
        {
            switch (account.ToUpper())
            {
                case "QQ":
                case "WX":
                    account = account + "OpenID";
                    break;
                default:
                    throw new Exception("帐户类型错误");
            }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("openid", openID) };
            using (SqlDataReader dr = Sql.SelReturnReader(TbName, " WHERE [" + account + "]=@openid", sp))
            {
                if (dr.Read())
                {
                    return iniModel.GetModelFromReader(dr);
                }
                else
                {
                    return null;
                }
            }
        }
        //-------------------------
        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }
        public string[] SelTokenByIDS(string ids)
        {
            if (string.IsNullOrEmpty(ids)) return null;
            SafeSC.CheckIDSEx(ids);
            string sql = "Select * From " + TbName + " Where Uid in(" + ids + ")";
            string token = "";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql);
            foreach (DataRow dr in dt.Rows)
            {
                token += dr["token"];
            }
            return token.TrimEnd(',').Split(',');
        }
    }
}
