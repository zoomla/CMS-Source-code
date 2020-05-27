using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.SQLDAL;
using ZoomLa.Common;
using ZoomLa.Model.Exam;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.BLL.Exam
{
    public class B_Exam_PaperNode : ZL_Bll_InterFace<M_Exam_PaperNode>
    {
        string TbName = "", PK = "";
        public M_Exam_PaperNode initMod = new M_Exam_PaperNode();
        public B_Exam_PaperNode()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }

        public int Insert(M_Exam_PaperNode model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }

        public M_Exam_PaperNode SelReturnModel(int ID)
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

        public bool UpdateByID(M_Exam_PaperNode model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public DataTable SelByPid(int pid)
        {
            string sql = "SELECT A.*,B.TypeName AS ParentName,(SELECT COUNT(*) FROM "+TbName+" C WHERE C.Pid=A.ID) AS ChildCount FROM " + TbName+" A LEFT JOIN "+TbName+" B ON A.Pid=B.ID WHERE A.Pid="+pid;
            return SqlHelper.ExecuteTable(sql);
        }
    }
}
