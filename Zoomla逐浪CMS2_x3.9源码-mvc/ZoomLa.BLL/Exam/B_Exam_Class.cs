namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Collections.Generic;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using ZoomLa.BLL.Helper;
    public class B_Exam_Class
    {
        public B_Exam_Class()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        private string PK, TbName;
        private M_Exam_Class initMod = new M_Exam_Class();
        public int GetInsert(M_Exam_Class model)
        {
            return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool GetUpdate(M_Exam_Class model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.C_id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool DeleteByGroupID(int Questions_ClassID)
        {
            return Sql.Del(TbName, "C_id=" + Questions_ClassID);
        }
        public M_Exam_Class GetSelect(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Exam_Class();
                }
            }
        }
        public M_Exam_Class GetSelectByCName(string C_ClassName)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("C_ClassName", C_ClassName) };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(TbName, "[C_ClassName] = @C_ClassName", sp))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Exam_Class();
                }
            }
        }
        public DataTable GetSelectByC_ClassId(int C_ClassId)
        {
            string sqlStr = "SELECT A.*,(SELECT COUNT(*) FROM [dbo].[ZL_Exam_Class] WHERE C_Classid=A.C_id) AS ChildCount,B.C_ClassName AS ParentName FROM [dbo].[ZL_Exam_Class] A"
                             + " LEFT JOIN ZL_Exam_Class B ON A.C_Classid=B.C_id WHERE A.[C_Classid] =" + C_ClassId + " order by A.C_OrderBy";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        public DataTable Select_All()
        {
            return Sql.Sel(TbName, null, " C_OrderBy");
        }
        public int SelFirstNodeID(int nodeid)
        {
            if (nodeid < 1) { return 0; }
            int parentID = nodeid;
            do
            {
                DataTable dt = SqlHelper.ExecuteTable("SELECT TOP 1 C_id AS NodeID,C_Classid AS ParentID FROM " + TbName + " Where C_id=" + parentID);
                if (dt == null || dt.Rows.Count < 1 || DataConvert.CLng(dt.Rows[0]["ParentID"]) < 1) { return parentID; }
                parentID = DataConvert.CLng(dt.Rows[0]["ParentID"]);
            } while (parentID > 0);
            return nodeid;
        }
        public DataTable SelByIDS(string ids)
        {
            ids = StrHelper.PureIDSForDB(ids);
            if (string.IsNullOrEmpty(ids)) return null;
            SafeSC.CheckDataEx(ids);
            return SqlHelper.ExecuteTable("SELECT * FROM " + TbName + " WHERE c_id IN(" + ids + ")");
        }
        public List<M_Exam_Class> SelectQuesClasses()
        {
            DataTable dt = new DataTable();
            dt = Select_All();
            return GetSelectByDt(dt);
        }
        private List<M_Exam_Class> GetSelectByDt(DataTable dt)
        {
            List<M_Exam_Class> mqc;
            if (dt != null && dt.Rows.Count > 0)
            {
                mqc = new List<M_Exam_Class>();
                foreach (DataRow dr in dt.Rows)
                {
                    M_Exam_Class mq = new M_Exam_Class();
                    mq.C_Classid = DataConverter.CLng(dr["C_ClassId"]);
                    mq.C_id = DataConverter.CLng(dr["C_id"]);
                    mq.C_OrderBy = DataConverter.CLng(dr["C_OrderBy"].ToString());
                    mq.C_ClassName = dr["c_ClassName"].ToString();
                    mqc.Add(mq);
                }
                return mqc;
            }
            else
            {
                return new List<M_Exam_Class>();
            }
        }
    }
}
