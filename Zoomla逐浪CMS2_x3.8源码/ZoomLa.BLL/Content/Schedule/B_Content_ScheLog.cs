using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Content;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Content
{
    public class B_Content_ScheLog : ZL_Bll_InterFace<M_Content_ScheLog>
    {
        private M_Content_ScheLog initMod = new M_Content_ScheLog();
        private string PK, TbName;
        public B_Content_ScheLog()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_Content_ScheLog model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Content_ScheLog model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public M_Content_ScheLog SelReturnModel(int ID)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, PK, ID))
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
        public DataTable Sel(int top, int taskID = 0)
        {
            string where = " B.ExecuteType IS NOT NULL";
            if (taskID > 0) { where += " AND A.TaskID=" + taskID; }
            PageSetting setting = new PageSetting()
            {
                pk = "A.ID",
                psize = top,
                fields = "A.*,B.ExecuteType",
                t1 = TbName,
                t2 = "ZL_Content_ScheTask",
                on = "A.TaskID=B.ID",
                where = where,
                order = "A.ID DESC"
            };
            return DBCenter.SelPage(setting);
        }
        public PageSetting SelPage(int cpage, int psize, int taskID = 0)
        {
            string where = "1=1 ";
            if (taskID > 0) { where += " AND A.TaskID = " + taskID; }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, "A.ID", where);
            setting.fields = "A.*,B.ExecuteType";
            setting.t2 = "ZL_Content_ScheTask";
            setting.on = "A.TaskID=B.ID";
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName, "", PK + " DESC");
        }
    }
}
