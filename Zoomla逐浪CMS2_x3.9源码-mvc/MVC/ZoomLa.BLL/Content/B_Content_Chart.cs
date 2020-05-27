using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.Model;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Content
{
    public class B_Content_Chart:ZL_Bll_InterFace<M_Content_Chart>
    {
        public M_Content_Chart initMod = new M_Content_Chart();
        public string TbName = "";
        public string PK;
        public B_Content_Chart()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_Content_Chart model)
        {
            return Sql.insert(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

        public bool UpdateByID(M_Content_Chart model)
        {

            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public bool Del(int ID)
        {
            return Sql.Del(TbName,ID);
        }
        public bool DelByIds(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                SafeSC.CheckIDSEx(ids);
                string sql = "DELETE FROM "+TbName+" WHERE ID IN ("+ids+")";
                return SqlHelper.ExecuteSql(sql);
            }
            return false;
        }

        public M_Content_Chart SelReturnModel(int ID)
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

        public DataTable Sel()
        {
            return Sql.Sel(TbName, "", "CDate DESC");
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "CDate DESC");
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable SelByUid(int uid)
        {
            string sql = "SELECT * FROM "+TbName+" WHERE UserID="+uid+" ORDER BY CDate DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
    }
}
