using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ZoomLa.SQLDAL;
using ZoomLa.Model.Plat;
using System.Data.SqlClient;
using ZoomLa.Model;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_App_AppTlp : ZL_Bll_InterFace<M_APP_APPTlp>
    {
        public string TbName, PK;
        public M_APP_APPTlp initMod = new M_APP_APPTlp();
        public DataTable dt = null;

        public B_App_AppTlp()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }

        public int Insert(M_APP_APPTlp model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

        public DataTable Sel()
        {
            return Sql.Sel(TbName, "", "CDate DESC");
        }
        public PageSetting SelPage(int cpage, int psize, int uid = 0)
        {
            string where = "1=1";
            if (uid > 0) { where += " AND UserID=" + uid; }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where, "CDate DESC");
            DBCenter.SelPage(setting);
            return setting;
        }
        public M_APP_APPTlp SelReturnModel(int ID)
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
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM " + TbName + " WHERE ID IN (" + ids + ")";
            return SqlHelper.ExecuteSql(sql);
        }
        public DataTable SelByUid(int uid)
        {
            string sql = "SELECT * FROM " + TbName + " WHERE UserID=" + uid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public bool UpdateByID(M_APP_APPTlp model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
    }
}
