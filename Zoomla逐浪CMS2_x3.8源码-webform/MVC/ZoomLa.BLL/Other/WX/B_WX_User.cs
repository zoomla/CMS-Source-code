using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZoomLa.SQLDAL;
using ZoomLa.Model.Plat;
using System.Data.SqlClient;
using ZoomLa.Model;
using System.Data;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    /// <summary>
    /// 已关注我们微信平台的用户信息
    /// 信息通过:ZL_UserApp,wechat与用户绑定
    /// </summary>
    public class B_WX_User : ZL_Bll_InterFace<M_WX_User>
    {
        M_WX_User initMod = new M_WX_User();
        public string TbName, PK;
        public B_WX_User()
        {
            this.TbName = initMod.TbName;
            this.PK = initMod.GetPK();
        }
        public int Insert(M_WX_User model)
        {
            if (string.IsNullOrEmpty(model.OpenID)) { throw new Exception("微信用户的OpenID不能为空"); }
            if (DBCenter.IsExist(TbName, "OpenID=@openid", new List<SqlParameter>() { new SqlParameter("openid", model.OpenID) })) { throw new Exception("该OpenID已存在,不可重复加入"); }
            return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_WX_User model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public bool DelByOpenid(int appid, string openid)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@openid", openid) };
            string sql = "DELETE FROM " + TbName + " WHERE AppId=" + appid + " AND OpenID=@openid";
            return SqlHelper.ExecuteSql(sql, sp);
        }
        public M_WX_User SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public M_WX_User SelForOpenid(int appid, string openid)
        {
            string sql = "SELECT * FROM " + TbName + " WHERE AppId=" + appid + " AND OpenID=@openid";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@openid", openid) };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, sp))
            {
                if (reader.Read())
                    return initMod.GetModelFromReader(reader);
                else
                    return null;
            }
        }
        public DataTable Sel()
        {
            return Sql.Sel(TbName, "", "CDate Desc");
        }
        public DataTable SelByAppId(int appid)
        {
            string sql = "SELECT * FROM " + TbName + " WHERE AppId=" + appid + " ORDER BY CDate DESC"; ;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "", "CDate Desc");
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}
