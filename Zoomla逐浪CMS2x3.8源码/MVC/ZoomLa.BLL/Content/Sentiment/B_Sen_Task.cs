using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Sentiment;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Sentiment
{
    public class B_Sen_Task : ZL_Bll_InterFace<M_Sen_Task>
    {
        public string TbName, PK;
        public M_Sen_Task initMod = new M_Sen_Task();
        public B_Sen_Task()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel()
        {
            return Sql.Sel(TbName, "", " ID ASC");
        }
        public DataTable SelTop(int top = 10)
        {
            string sql = "SELECT TOP " + 10 + " * FROM " + TbName;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public DataTable SelById(int ID)
        {
            string sql = "SELECT * FROM " + TbName + " WHERE ID=" + ID + " ORDER BY CDate DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public int Insert(M_Sen_Task model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_Sen_Task model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public void DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM " + TbName + " WHERE ID IN(" + ids + ")";
            SqlHelper.ExecuteSql(sql);
        }
        public M_Sen_Task SelReturnModel(int ID)
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
        public M_Sen_Task SelLastModel() 
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName,"WHERE 1=1"))
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
        public PageSetting SelPage(int cpage, int psize)
        {
            string where = "1=1 ";
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where);
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}
