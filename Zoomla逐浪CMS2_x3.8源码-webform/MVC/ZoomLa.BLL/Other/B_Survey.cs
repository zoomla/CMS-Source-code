using System;
using System.Data;
using System.Configuration;
using System.Web;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Collections.Generic;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.SQLDAL.SQL;


namespace ZoomLa.BLL
{
    /*
     * 问卷调查与问卷问题
     */
    public class B_Survey
    {
        private string strTableName, PK;
        private M_Survey initMod = new M_Survey();
        public B_Survey()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_Survey SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
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
        private M_Survey SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
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
            return Sql.Sel(strTableName);
        }
        public bool UpdateByID(M_Survey model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.SurveyID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Survey model)
        {
            return Sql.insert(strTableName, initMod.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool AddSurvey(M_Survey m_Survey)
        {
            return Sql.insertID(strTableName, initMod.GetParameters(m_Survey), BLLCommon.GetParas(m_Survey), BLLCommon.GetFields(m_Survey)) > 0;
        }
        /// <summary>
        /// 更新
        /// </summary>
        public bool UpdateSurvey(M_Survey m_Survey)
        {
            return Sql.UpdateByIDs(strTableName, PK, m_Survey.SurveyID.ToString(), BLLCommon.GetFieldAndPara(m_Survey), initMod.GetParameters(m_Survey));
        }
        /// <summary>
        /// 根据ID获取问券调查实例信息
        /// </summary>
        public M_Survey GetSurveyBySid(int SurveyID)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int);
            cmdParams[0].Value = SurveyID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, "select * from ZL_Survey where SurveyID=@ID", cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                    return new M_Survey(true);
            }
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM " + strTableName + " WHERE SurveyID IN(" + ids + ")";
            SqlHelper.ExecuteSql(sql);
            return true;
        }
        /// <summary>
        /// 获取所有问券调查列表
        /// </summary>
        public static DataTable GetSurveyList()
        {
            string strSql = "select * from ZL_Survey order by SurveyID Desc,EndTime ASC";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        /// <summary>
        /// 是否已存在某名称的问卷
        /// </summary>
        public static bool IsExistSur(string SurveyName)
        {
            string strSql = "select count(SurveyID) from ZL_Survey where SurveyName=@SurveyName";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@SurveyName",SqlDbType.VarChar,50)
            };
            sp[0].Value = SurveyName;
            return SqlHelper.ExistsSql(strSql, sp);
        }
        /// <summary>
        /// 搜索相似名称的问卷列表
        /// </summary>
        public static DataTable SearchSur(string SurKey)
        {
            string strSql = "select * from ZL_Survey where SurveyName like '" + SurKey + "%' order by SurveyID Desc,EndTime ASC";


            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        /// <summary>
        /// 问卷调查是否有问题
        /// </summary>
        public static bool HasQuestion(int SurveyID)
        {
            string strSql = "select count(QID) from ZL_Question where SurveyID=@SurveyID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@SurveyID",SqlDbType.Int)
            };
            sp[0].Value = SurveyID;
            return SqlHelper.ExistsSql(strSql, sp);
        }
        /// <summary>
        /// 问题是否已存在问卷中
        /// </summary>
        public static bool ExistQue(int SID, string Title)
        {
            string strSql = "select count(QID) from ZL_Question where SurveyID=@SurveyID and QTitle=@QTitle";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@SurveyID",SqlDbType.Int),
                new SqlParameter("@QTitle",SqlDbType.VarChar)
            };
            sp[0].Value = SID;
            sp[1].Value = Title;
            return SqlHelper.ExistsSql(strSql, sp);
        }
        /// <summary>
        /// 问卷的最大问题排序号
        /// </summary>
        public static int GetMaxOrderID(int SurveyID)
        {
            string strSql = "select max(OrderID) from ZL_Question where SurveyID=@SurveyID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@SurveyID",SqlDbType.Int)
            };
            sp[0].Value = SurveyID;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, sp));
        }
        /// <summary>
        /// 当前问题排序的前一个问题的ID
        /// </summary>
        public static int PreQusID(int SurveyID, int CurOrderID)
        {
            string strSql = "select top 1 Qid from ZL_Question where SurveyID=@SurveyID and OrderID<@CurrentID order by OrderId Desc";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@SurveyID",SqlDbType.Int),
                new SqlParameter("@CurrentID",SqlDbType.Int)
            };
            cmdParam[0].Value = SurveyID;
            cmdParam[1].Value = CurOrderID;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParam));
        }
        /// <summary>
        /// 当前问题排序的后一个问题的ID
        /// </summary>
        public static int NexQusID(int SurveyID, int CurOrderID)
        {
            // string strSql = "select QID from (select QID from ZL_Question where SurveyID=@SurveyID and OrderId>@CurrentID order by OrderId Asc) where rownum=1";
            string strSql = "select Top 1 Qid from ZL_Question where SurveyID=@SurveyID and OrderId>@CurrentID order by OrderId Asc";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@SurveyID",SqlDbType.Int),
                new SqlParameter("@CurrentID",SqlDbType.Int)
            };
            cmdParam[0].Value = SurveyID;
            cmdParam[1].Value = CurOrderID;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParam));
        }
        /// <summary>
        /// 问卷的最小问题排序号
        /// </summary>
        public static int GetMinOrderID(int SurveyID)
        {
            string strSql = "select min(OrderID) from ZL_Question where SurveyID=@SurveyID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@SurveyID",SqlDbType.Int)
            };
            sp[0].Value = SurveyID;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, sp));
        }
        /// <summary>
        /// 提交答案
        /// </summary>
        public static bool AddAnswer(M_Answer info)
        {
            return Sql.insertID("ZL_Answer", info.GetParameters(), BLLCommon.GetParas(info), BLLCommon.GetFields(info)) > 0;
        }
        /// <summary>
        /// 问题的答案总数
        /// </summary>
        public static int GetAnsCount(int QID)
        {
            string strSql = "select count(AID) from ZL_Answer where QID=@QID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@QID",SqlDbType.Int)
            };
            sp[0].Value = QID;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, sp));
        }
        /// <summary>
        /// 问题某选项的提交总数
        /// </summary>
        public static int GetAnsOptionCount(int Qid, string Ovalue)
        {
            string strSql = "select count(AID) from ZL_Answer where QID=@QID and AnswerContent like @Answer";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@QID",SqlDbType.Int),
                new SqlParameter("@Answer",SqlDbType.NVarChar)
            };
            sp[0].Value = Qid;
            sp[1].Value = Ovalue;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, sp));
        }
        /// <summary>
        /// 问题某选项的提交总数
        /// </summary>
        /// <param name="sid">问卷ID</param>
        public int GetQuCount(int qid)
        {
            string str = "Select * from ZL_Answer Where QID=" + qid;
            return SqlHelper.ExecuteTable(CommandType.Text, str).Rows.Count;
        }
        public int GetCount(int sid)
        {
            string str = "Select * from ZL_Answer Where Surveyid=" + sid;
            return SqlHelper.ExecuteTable(CommandType.Text, str).Rows.Count;
        }
        /// <summary>
        /// 获取参与答卷的用户数目
        /// </summary>
        public static int GetUsersCnt(int sid)
        {
            //string strSql = "select Count(Distinct(Userid)) from  ZL_Answer_Recode where Sid = @sid";
            string strSql = "select Count(Userid) from  ZL_Answer_Recode where Sid = " + sid;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql));
        }
        /// <summary>
        /// 添加提交记录
        /// </summary>
        public static bool AddAnswerRecord(int SID, int UserID, string SubmitIP, DateTime SubmitDate, int Status)
        {
            string strSql = "insert into ZL_Answer_Recode (SID,UserID,SubmitIP,SubmitDate,Status)values(@SID,@UserID,@SubmitIP,@SubmitDate,@Status)";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@SID",SqlDbType.Int),
                new SqlParameter("@UserID",SqlDbType.Int),
                new SqlParameter("@SubmitIP",SqlDbType.NVarChar),
                new SqlParameter("@SubmitDate",SqlDbType.DateTime),
                new SqlParameter("@Status",SqlDbType.Int)
            };
            sp[0].Value = SID;
            sp[1].Value = UserID;
            sp[2].Value = SubmitIP;
            sp[3].Value = SubmitDate;
            sp[4].Value = Status;
            return SqlHelper.ExecuteSql(strSql, sp);
        }
        /// <summary>
        /// 用户是否已提交记录
        /// </summary>
        public static bool HasAnswerBySID(int SID, int UserID)
        {
            string strSql = "select count(RID) from ZL_Answer_Recode where SID=@SID and UserID=@UserID And Status<>-1";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@SID",SqlDbType.Int),
                new SqlParameter("@UserID",SqlDbType.Int)
            };
            sp[0].Value = SID;
            sp[1].Value = UserID;
            return SqlHelper.ExistsSql(strSql, sp);
        }
        /// <summary>
        /// 同一IP提交次数
        /// </summary>
        public static int HasAnswerCountIP(int SID, string SubmitIP)
        {
            string strSql = "select count(RID) from ZL_Answer_Recode where SID=@SID and SubmitIP=@SubmitIP And Status<>-1";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@SID",SqlDbType.Int),
                new SqlParameter("@SubmitIP",SqlDbType.NVarChar,50)
            };
            sp[0].Value = SID;
            sp[1].Value = SubmitIP;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, sp));
        }
        /// <summary>
        /// 获取问题总分和平均分
        /// </summary>
        /// <param name="qid">问题ID</param>
        /// <param name="qtypeid">问题类型ID</param>
        /// <returns></returns>
        public string GetScore(int qid, int qtypeid)
        {
            double score = 0;
            M_Question QMod = new M_Question();
            QMod = B_Survey.GetQuestion(Convert.ToInt32(qid));
            string[] qConte = QMod.QuestionContent.Split('|');
            string str = "select * from ZL_Answer where qid=" + qid;
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, str);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < qConte.Length; j++)
                {
                    if (!string.IsNullOrEmpty(qConte[j].Split(':').ToString()) && dt.Rows[i]["AnswerContent"].ToString() == qConte[j].Split(':')[0])
                    {
                        score += Convert.ToDouble(qConte[j].Split(':')[1]);
                    }
                }
            }
            return "总分：" + score.ToString() + " 平均分：" + score / dt.Rows.Count;
        }
        public string CreateIframe(int qid)
        {
            M_Question QMod = new M_Question();
            QMod = B_Survey.GetQuestion(Convert.ToInt32(qid));
            string[] qConte = QMod.QuestionContent.Split('|');
            string str = "select * from ZL_Answer where qid=" + qid;
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, str);
            string num = "";
            string text = "";
            for (int i = 0; i < qConte.Length; i++)
            {
                DataTable dt1;
                dt.DefaultView.RowFilter = "AnswerContent='" + qConte[i].Split(':')[0] + "'";
                dt1 = dt.DefaultView.ToTable();
                num += dt1.Rows.Count.ToString() + ",";
                text += qConte[i].Split(':')[0] + ",";
            }
            num = num.Trim(',');
            text = text.Trim(',');
            return num + "|" + text;
        }
        public bool CheckScore(string qcontent)
        {
            bool flag = true;
            string[] QCon = qcontent.Split('|');
            for (int i = 0; i < QCon.Length && true; i++)
            {
                if (!string.IsNullOrEmpty(QCon[i].Split(':')[1]) || QCon[i].Split(':')[1] == "0")
                    flag = false;
            }
            return flag;
        }
        public int SelectNum(int sid, int qid, string qcontent)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("qcontent", qcontent) };
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From ZL_Answer Where Surveyid=" + sid + " And Qid=" + qid + " And Answercontent=@qcontent", sp);
            return dt.Rows.Count;
        }
        /// <summary>
        /// 获取问题选项的分数
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public int GetScoreByContent(string qcontent, string content)
        {
            int score = 0;
            string[] qArr = qcontent.Split('|');
            for (int i = 0; i < qArr.Length; i++)
            {
                if (qArr[i].Split(':')[0] == content)
                    score = Convert.ToInt32(qArr[i].Split(':')[1]);
            }
            return score;
        }
        public DataTable GetCountScore(int sid)
        {
            //string str = "Select sum(AnswerScore)as AnswerScore,userid  From ZL_answer Where SurveyID=" + sid + " Group By UserID";
            return SqlHelper.JoinQuery("SUM(A.AnswerScore)AS AnswerScore,A.UserID,B.UserName", "ZL_answer", "ZL_User", "A.UserID=B.UserID", "SurveyID=" + sid + " GROUP BY A.UserID,B.UserName");
            //return SqlHelper.ExecuteTable(CommandType.Text, str);
        }
        public DataTable GetAnswerByUID(int uid, int sid)
        {
            return SqlHelper.JoinQuery("A.*,B.QTitle,B.TypeID,B.QOption", "ZL_Answer", "ZL_Question", "A.Qid=B.Qid", " A.SurveyID=" + sid + " AND A.UserID=" + uid);
            //string str = "Select * From ZL_Answer where SurveyID=" + sid + " And UserID=" + uid;
            //return SqlHelper.ExecuteTable(CommandType.Text, str);
        }
        public string GetScoreByUID(int sid, int uid)
        {
            int score = 0;
            DataTable dt = new DataTable();
            dt = GetAnswerByUID(uid, sid);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                score += Convert.ToInt32(dt.Rows[i]["AnswerScore"].ToString());
            }
            return score.ToString();
        }
        public DataTable GetAnswerN(int sid)
        {
            string str = "Select distinct Answercontent,AnswerScore from ZL_Answer where Surveyid=" + sid + " Order by AnswerScore Desc";
            return SqlHelper.ExecuteTable(CommandType.Text, str);
        }
        public int GetAnswerNum(int sid, string anscon)
        {
            string str = "Select * from ZL_Answer Where Answercontent=@Answercontent And Surveyid=" + sid;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Answercontent", anscon) };
            return SqlHelper.ExecuteTable(CommandType.Text, str, sp).Rows.Count;
        }
        /*-------------------------问答-------------------------------*/
        public static int AddQuestion(M_Question info)
        {
            return Sql.insertID(info.TbName, info.GetParameters(), BLLCommon.GetParas(info), BLLCommon.GetFields(info));
        }
        public static bool UpdateQuestion(M_Question info)
        {
            return Sql.UpdateByIDs(info.TbName, info.PK, info.QuestionID.ToString(), BLLCommon.GetFieldAndPara(info), info.GetParameters());
        }
        public static bool DelQuestion(string ids)
        {
            if (string.IsNullOrEmpty(ids)) {return false; }
            SafeSC.CheckIDSEx(ids);
            return DBCenter.DelByIDS("ZL_QUESTION","QID",ids);
        }
        public static M_Question GetQuestion(int QID)
        {
            string sql = "SELECT * FROM ZL_Question WHERE QID=" + QID;
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(CommandType.Text, sql))
            {
                if (rdr.Read())
                {
                    M_Question info = new M_Question();
                    info = info.GetQuestionFromReader(rdr);
                    return info;
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 获取问题列表
        /// </summary>
        public static DataTable GetQuestionList(int SurveyID)
        {
            string strSql = "select * from ZL_Question where SurveyID=@SurveyID order by OrderID Asc";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@SurveyID",SqlDbType.Int)
            };
            sp[0].Value = SurveyID;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        /// <summary>
        /// 获取问题实例列表
        /// </summary>
        public static IList<M_Question> GetQueList(int SurveyID)
        {
            string strSql = "select * from ZL_Question where SurveyID=" + SurveyID + " order by OrderID Asc";
            IList<M_Question> list = new List<M_Question>();
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(CommandType.Text, strSql))
            {
                while (rdr.Read())
                {
                    list.Add(new M_Question().GetQuestionFromReader(rdr));
                }
                rdr.Close();
                rdr.Dispose();
            }
            return list;
        }
        /*-------------------------New-------------------------------*/
        public PageSetting SelPage(int cpage, int psize, int uid, string skey)
        {
            string where = "1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            //if (uid > 0) { where += " AND UserID=" + uid; }
            if (!string.IsNullOrEmpty(skey)) { where += " AND SurveyName LIKE @skey"; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where, "", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}