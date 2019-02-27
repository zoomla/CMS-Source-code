using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.User.Addon
{
    public class B_UserAPP
    {
        private string PK, TbName;
        private Appinfo initMod = new Appinfo();
        public B_UserAPP() 
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(Appinfo model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(Appinfo model)
        {
            if (string.IsNullOrEmpty(model.SourcePlat)) { throw new Exception("未指定平台"); }
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public Appinfo SelReturnModel(int ID)
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
        public Appinfo SelModelByUid(int uid, string source, string appid = "")
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("source", source),new SqlParameter("appid",appid) };
            string where = " WHERE UserID=" + uid + " AND SourcePlat=@source ";
            if (!string.IsNullOrEmpty(appid)) {where+=" AND AppID=@appid"; }
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, where, sp))
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
        public Appinfo SelModelByOpenID(string openid, string source)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("source", source), new SqlParameter("openid", openid) };
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, " WHERE OpenID=@openid AND SourcePlat=@source", sp))
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
        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize,TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}
