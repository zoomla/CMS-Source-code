using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.SQLDAL;
using ZoomLa.Model.Exam;
using System.Data.SqlClient;
using ZoomLa.Model;
using System.Data;
using ZoomLa.Common;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Exam
{
    public class B_Exam_Answer : ZL_Bll_InterFace<M_Exam_Answer>
    {
        private string TbName, PK;
        private B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
        private M_Exam_Answer initMod = new M_Exam_Answer();
        public B_Exam_Answer()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Exam_Answer model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_Exam_Answer model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public M_Exam_Answer SelReturnModel(int ID)
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
            return Sql.Sel(TbName, "", "CDate Desc");
        }
        public PageSetting SelPage(int cpage, int psize, int uid = -100)
        {
            string where = "1=1";
            if (uid != -100) { where += " AND UserID=" + uid + " AND Remind=1"; }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where);
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable SelAllAnswer()
        {
            string sql = "SELECT * FROM " + TbName + " WHERE Remind=1";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public DataTable SelByFlow(string flow)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("flow", flow) };
            string fields = "A.*,A.QType as p_type,B.p_id,B.p_title,B.p_Content,B.p_OptionInfo,B.IsToShare";
            return SqlHelper.JoinQuery(fields, TbName, "ZL_Exam_Sys_Questions", "A.Qid=B.p_id", "A.FlowID=@flow", "A.QType", sp);
        }
        public M_Exam_Answer SelMainModel(string flow)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("flow", flow) };
            string sql = " WHERE FlowID=@flow AND Remind=1";
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, sql, sp))
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
        //-----------------------------------User使用
        //查看我的考试成绩
        public DataTable SelAllMyAnswer(int uid)
        {
            //string sql = "SELECT * FROM " + TbName + " WHERE ID IN (SELECT Min(ID) FROM " + TbName + " Group BY FlowID) AND UserID=" + uid;
            string sql = "SELECT * FROM " + TbName + " WHERE UserID=" + uid + " AND Remind=1";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        //根据FlowID,试卷ID与用户ID获取答案
        public DataTable SelByPid(int uid, int pid, string flow)
        {
            //ID,QID,QType,QTitle,Answer,IsRight,Remark
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("flow", flow) };
            string fields = "A.*,A.QType as p_type,B.p_id,B.p_title,B.p_Content,B.p_OptionInfo,B.Jiexi,B.p_shuming,B.IsToShare ";
            //已经取出所有的题目,需要按原大题给其设定Pid?
            return SqlHelper.JoinQuery(fields, TbName, "ZL_Exam_Sys_Questions", "A.Qid=B.p_id", "A.FlowID=@flow AND A.UserID=" + uid + " AND PaperID=" + pid, "A.QType", sp);
        }
        //--------通用
        /// <summary>
        /// 返回题目类型表,给RPT绑定后,再依此筛选试卷的问题DataTable
        /// </summary>
        public DataTable GetTypeDT(DataTable questDT)
        {
            if (questDT == null || questDT.Rows.Count < 1) { return null; }
            DataTable typedt = new DataTable();
            typedt.Columns.Add(new DataColumn("QType", typeof(int)));
            typedt.Columns.Add(new DataColumn("QName", typeof(string)));
            typedt.Columns.Add(new DataColumn("QNum", typeof(int)));//题目数
            typedt.Columns.Add(new DataColumn("TotalScore", typeof(int)));//总计分数
            typedt.Columns.Add(new DataColumn("IsBig", typeof(int)));//0:否,1:是
            typedt.Columns.Add(new DataColumn("LargeContent", typeof(string)));
            string[] qtype = "单选题,多选题,填空题,解答题,完形填空".Split(',');
            for (int i = 0; i < qtype.Length; i++)
            {
                DataRow dr = typedt.NewRow();
                questDT.DefaultView.RowFilter = "p_type=" + i + " AND pid=0";
                DataTable tempdt = questDT.DefaultView.ToTable();
                if (tempdt.Rows.Count < 1) continue;
                dr["QType"] = i;
                dr["QName"] = qtype[i];
                dr["QNum"] = tempdt.Rows.Count;
                dr["TotalScore"] = tempdt.Compute("SUM(p_defaultscores)", "");
                dr["IsBig"] = 0;
                typedt.Rows.Add(dr);
            }
            //增加大题类型
            DataRow[] bigdrs = questDT.Select("p_type=10");
            if (bigdrs.Length > 0)
            {
                foreach (DataRow bigdr in bigdrs)
                {
                    DataRow dr = typedt.NewRow();
                    questDT.DefaultView.RowFilter = "pid=" + bigdr["p_id"];
                    DataTable tempdt = questDT.DefaultView.ToTable();
                    if (tempdt.Rows.Count < 1) continue;
                    dr["QType"] = bigdr["p_id"];
                    dr["QName"] = bigdr["p_title"];//可换成类型
                    dr["QNum"] = tempdt.Rows.Count;
                    dr["TotalScore"] = bigdr["p_defaultscores"];
                    dr["IsBig"] = 1;
                    dr["LargeContent"] = bigdr["LargeContent"];
                    typedt.Rows.Add(dr);
                }
            }
            return typedt;
        }
        public void SumScore(string flow)
        {
            string sql = "UPDATE ZL_Exam_Answer SET TotalScore=(SELECT SUM(Score) From ZL_Exam_Answer WHERE FlowID=@flow AND IsRight=1) "
                       + "WHERE FlowID=@flow AND Remind=1";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("flow", flow) };
            SqlHelper.ExecuteSql(sql, sp);
        }
    }
}
