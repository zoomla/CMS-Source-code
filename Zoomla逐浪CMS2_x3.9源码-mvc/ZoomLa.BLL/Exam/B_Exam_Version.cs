using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Exam;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Exam
{
    public class B_Exam_Version : ZL_Bll_InterFace<M_Exam_Version>
    {
        private string TbName = "", PK = "";
        private M_Exam_Version initMod = new M_Exam_Version();
        public B_Exam_Version()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Exam_Version model)
        {
            model.OrderID = DataConvert.CLng(DBCenter.ExecuteScala(TbName, "MAX(OrderID)", "")) + 1;
            return Sql.insertID(model.TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

        public bool UpdateByID(M_Exam_Version model)
        {
            return Sql.UpdateByID(model.TbName, PK, model.ID, BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }

        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }

        public M_Exam_Version SelReturnModel(int ID)
        {
            using (SqlDataReader sdr = Sql.SelReturnReader(TbName, PK, ID))
            {
                if (sdr.Read())
                {
                    return initMod.GetModelFromReader(sdr);
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
        public DataTable SelAll(int uid = 0)
        {
            string fields = "A.*,B.GradeName,(SELECT C_ClassName FROM ZL_Exam_Class WHERE A.NodeID=C_id) AS NodeName ";
            string where = "";
            if (uid > 0) { where = "A.UserID=" + uid; }
            return SqlHelper.JoinQuery(fields, TbName, "ZL_Grade", "A.Grade=B.GradeID", where, "A.CDATE DESC");
        }
        public DataTable GetChildVersion(int pid, int uid = 0)
        {
            string fields = "A.*,B.GradeName,(SELECT COUNT(*) FROM " + TbName + " WHERE Pid=A.ID) AS Child,(SELECT C_ClassName FROM ZL_Exam_Class WHERE A.NodeID=C_id) AS NodeName ";
            string where = "A.Pid = " + pid;
            if (uid > 0) { where = "A.UserID=" + uid; }
            return SqlHelper.JoinQuery(fields, TbName, "ZL_Grade", "A.Grade=B.GradeID", where, "A.OrderID ASC");
        }
    }
}
