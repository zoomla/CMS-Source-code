namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Collections.Generic;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;

    /// <summary>
    /// B_Questions 的摘要说明
    /// </summary>
    public class B_Questions
    {
        public B_Questions()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="Questions"></param>
        /// <returns></returns>
        /// 
        public string strTableName = "";
        public string PK = "";
        public DataTable dt = null;
        private M_Questions initMod = new M_Questions();

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public M_Questions SelReturnModel(int ID)
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
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public DataTable Select_All()
        {
            return Sql.Sel(strTableName);
        }
        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool GetUpdate(M_Questions model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.p_id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }

        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Questions model)
        {
          return DBCenter.Insert(model);
        }
        public int GetInsert(M_Questions model)
        {
          return DBCenter.Insert(model);
        }




        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="Questions"></param>
        /// <returns></returns>
        public M_Questions GetSelect(int QuestionsID)
        {
            string sqlStr = "SELECT [p_id],[p_title],[p_Difficulty],[paper_Id],[p_Class],[p_Views],[p_Inputer],[p_Type],[p_Knowledge],[p_Answer],[p_Content],[p_CreateTime],[p_Order] FROM [dbo].[ZL_Questions] WHERE [p_id] = @p_id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@p_id", SqlDbType.Int, 4);
            cmdParams[0].Value = QuestionsID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Questions();
                }
            }
        }

        /// <summary>
        /// 根据试卷ID查询试题
        /// </summary>
        /// <param name="questionsPId"></param>
        /// <returns></returns>
        public List<M_Questions> GetSelectByPId(int questionsPId)
        {
            string sqlStr = "SELECT [p_id],[p_title],[p_Difficulty],[paper_Id],[p_Class],[p_Views],[p_Inputer],[p_Type],[p_Knowledge],[p_Answer],[p_Content],[p_CreateTime],[p_Order] FROM [dbo].[ZL_Questions] WHERE [paper_Id] = @paper_Id order by p_id desc";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@paper_Id", SqlDbType.Int, 4);
            cmdParams[0].Value = questionsPId;
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
            List<M_Questions> qus = GetSelByDataTable(dt);
            if (qus != null && qus.Count > 0)
            {
                return qus;
            }
            else
            {
                return new List<M_Questions>();
            }
        }


        private List<M_Questions> GetSelByDataTable(DataTable dt)
        {
            List<M_Questions> questions;
            if (dt != null && dt.Rows.Count > 0)
            {
                questions = new List<M_Questions>();
                foreach (DataRow dr in dt.Rows)
                {
                    M_Questions qu = new M_Questions();
                    qu.p_id = DataConverter.CLng(dr["p_id"]);
                    qu.p_title = dr["p_title"].ToString();
                    qu.p_CreateTime = DataConverter.CDate(dr["p_CreateTime"]);
                    qu.p_Content = dr["p_Content"].ToString();
                    qu.p_Answer = dr["p_Answer"].ToString();
                    qu.p_Class = DataConverter.CLng(dr["p_Class"]);
                    qu.p_Difficulty = DataConverter.CLng(dr["p_Difficulty"]);
                    qu.p_Inputer = dr["p_inputer"].ToString();
                    qu.p_Knowledge = DataConverter.CLng(dr["p_Knowledge"]);
                    qu.p_Type = DataConverter.CLng(dr["p_type"]);
                    qu.p_Views = DataConverter.CLng(dr["p_views"]);
                    qu.Paper_Id = DataConverter.CLng(dr["paper_Id"]);
                    qu.p_Order = DataConverter.CLng(dr["p_Order"]);
                    questions.Add(qu);
                }
                return questions;
            }
            else
            {
                return new List<M_Questions>();
            }
        }
        /// <summary>
        /// 查询所有的试题
        /// </summary>
        /// <returns>list集合</returns>
        public List<M_Questions> SelectQuestions()
        {
            DataTable dt = Select_All();
            List<M_Questions> questions = GetSelByDataTable(dt);
            if (questions != null && questions.Count > 0)
            {
                return questions;
            }
            else
            {
                return new List<M_Questions>();
            }
        }

        /// <summary>
        /// 根据标题和内容查询试题记录
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public M_Questions GetSelectByCon(string title, string content)
        {
            string sqlStr = "SELECT [p_id],[p_title],[p_Difficulty],[paper_Id],[p_Class],[p_Views],[p_Inputer],[p_Type],[p_Knowledge],[p_Answer],[p_Content],[p_CreateTime],[p_Order] FROM [dbo].[ZL_Questions] WHERE [p_title] like @p_title AND [p_Content] like @p_Content order by p_id desc";
            SqlParameter[] cmdParams = new SqlParameter[2];
            cmdParams[0] = new SqlParameter("@p_title", SqlDbType.NVarChar, 255);
            cmdParams[0].Value = title;
            cmdParams[1] = new SqlParameter("@p_Content", SqlDbType.NText, 16);
            cmdParams[1].Value = content;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Questions();
                }
            }
        }


        /// <summary>
        /// 根据条件查询试题
        /// </summary>
        /// <param name="nd">难度</param>
        /// <param name="tx">题型</param>
        /// <param name="order">排序</param>
        /// <param name="c_ids">分类</param>
        /// <param name="k_ids">知识点ID</param>
        /// <returns></returns>
        /// 
        /// <returns></returns>
        /// 

        /// <summary>
        /// 根据所属分类ID查询分类
        /// </summary>
        /// <param name="C_ClassId"></param>
        /// <returns></returns>
        public DataTable GetSelectByC_ClassId(int C_ClassId)
        {
            string sqlStr = "SELECT [C_id],[C_ClassName],[C_Classid],[C_OrderBy] FROM [dbo].[ZL_Questions_Class] WHERE [C_Classid] =" + C_ClassId + " order by C_OrderBy";
            DataTable dt = new DataTable();
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }

        public string GetClassIDstr(int Cid)
        {
            string listst = Cid.ToString();
            DataTable classlist = GetSelectByC_ClassId(Cid);
            for (int i = 0; i < classlist.Rows.Count; i++)
            {
                listst = listst + "," + classlist.Rows[i]["C_id"].ToString();

                string linkstr = GetClassIDstr(DataConverter.CLng(classlist.Rows[i]["C_id"].ToString()));
                if (linkstr != "")
                {
                    listst = listst + "," + linkstr;
                }
            }
            return listst;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="diff">难度</param>
        /// <param name="type">题型</param>
        /// <param name="cid">分类ID</param>
        /// <param name="kid">知识点ID</param>
        /// <param name="order">排序，默认ID DESC,1按使用次数倒序,2按添加时间倒序</param>
        /// <returns></returns>
        public PageSetting SelPage(int cpage, int psize, int diff = -100, int type = -100, int cid = -100, int kid = -100, int order = -100)
        {
            string where = " 1=1";
            string orderStr = "";
            if (diff != -100) { where += " AND p_Difficulty=" + diff; }
            if (type != -100) { where += " AND p_Type=" + type; }
            if (cid != -100) { where += " AND p_Class in (" + GetClassIDstr(cid) + ")"; }
            if (kid != -100) { where += " AND p_Knowledge=" + kid; }
            switch (order)
            {
                case 1:
                    orderStr = "p_Views DESC";
                    break;
                case 2:
                    orderStr = "p_CreateTime DESC";
                    break;
            }
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where, orderStr);
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable GetSelectByCondition(int nd, int tx, int order, int c_ids, int k_ids)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_Questions] WHERE 1=1";

            if (nd > 0)//难度
            {
                sqlStr = sqlStr + " and p_Difficulty=" + nd.ToString() + "";
            }
            if (tx > 0)//题型
            {
                sqlStr = sqlStr + " and p_Type=" + tx.ToString() + "";
            }
            if (c_ids > 0)//分类ID，支持多级
            {
                sqlStr = sqlStr + " and p_Class in (" + GetClassIDstr(c_ids) + ")";
            }
            if (k_ids > 0)//知识点ID
            {
                sqlStr = sqlStr + " and p_Knowledge=" + k_ids.ToString() + "";
            }

            if (order > 0)//排序
            {
                if (order == 1)//按使用次数倒序排列
                {
                    sqlStr = sqlStr + " order by p_Views desc";
                }
                else if (order == 2)//按添加时间倒序排列
                {
                    sqlStr = sqlStr + " order by p_CreateTime desc";
                }
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }

        public DataTable SelectSession(string sessionid)
        {
            if (sessionid != "")
            {
                if (BaseClass.Right(sessionid, 1) == ",")
                {
                    sessionid = BaseClass.Left(sessionid, sessionid.Length - 1);
                }
            }

            if (sessionid != null && sessionid != "")
            {
                string sqlStr = "SELECT [p_id],[p_title],[p_Difficulty],[paper_Id],[p_Class],[p_Views],[p_Inputer],[p_Type],[p_Knowledge],[p_Answer],[p_Content],[p_CreateTime],[p_Order] FROM [dbo].[ZL_Questions] where [p_id] in (" + sessionid + ")";
                return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
            }
            else
            {
                return null;
            }
        }
    }
}
