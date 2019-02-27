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
    using Newtonsoft.Json;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json.Linq;
    using System.Text;
    using ZoomLa.SQLDAL.SQL;

    public class B_Exam_Sys_Questions
    {
        private string strTableName, PK;
        private M_Exam_Sys_Questions initMod = new M_Exam_Sys_Questions();
        public B_Exam_Sys_Questions()
        {
            strTableName = initMod.TbName; PK = initMod.PK;
        }
        private DataTable SelByIDS(string ids, int pid)
        {
            ids = StrHelper.PureIDSForDB(ids);
            SafeSC.CheckIDSEx(ids);
            string sql = "SELECT *,IsRight=0,[Order]=0,Remark='',pid=" + pid + " FROM " + strTableName + " WHERE p_id IN(" + ids + ") ORDER BY p_type";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        /// <summary>
        /// 根据IDS获取试题,如果传入PaperID,则使用其中的临时值,对部分变量初始化
        /// </summary>
        /// <param name="ids">Qids</param>
        /// <param name="paperid">试卷ID</param>
        /// <returns>试题列表</returns>
        public DataTable SelByIDSForExam(string ids, int paperid = 0)
        {
            ids = StrHelper.PureIDSForDB(ids);
            if (string.IsNullOrEmpty(ids)) return null;
            SafeSC.CheckIDSEx(ids);
            string sql = "SELECT *,IsRight=0,Remark='',pid=0,[order]=0 FROM " + strTableName + " WHERE p_id IN(" + ids + ") ORDER BY p_type";
            DataTable questDT = SqlHelper.ExecuteTable(CommandType.Text, sql);
            //检测有无大题,有大题的话,将大题下的小题也抽出,并设置pid,
            DataRow[] bigdrs = questDT.Select("p_type=10");
            foreach (DataRow dr in bigdrs)
            {
                string qids = StrHelper.PureIDSForDB(dr["p_content"].ToString());
                int pid = DataConvert.CLng(dr["p_id"]);
                DataTable dt = SelByIDS(qids, pid);//大题不支持嵌套,即大题不能再选大题
                //为其加载在大题中配置好的排序
                DataTable childDT = JsonConvert.DeserializeObject<DataTable>(dr["QInfo"].ToString());
                foreach (DataRow child in childDT.Rows)
                {
                    DataRow[] childDR = dt.Select("p_id=" + child["p_id"]);
                    if (childDR.Length > 0)
                    {
                        childDR[0]["order"] = DataConvert.CLng(child["orderid"]);
                    }
                }
                questDT.Merge(dt);
            }
            //附加临时状态,用于自定义分数,试卷对题目的排序
            if (paperid > 0)
            {
                M_Exam_Sys_Papers paperMod = new B_Exam_Sys_Papers().SelReturnModel(paperid);
                if (paperMod != null && !string.IsNullOrEmpty(paperMod.QuestList))
                {
                    List<M_Exam_TempQuest> list = JsonConvert.DeserializeObject<List<M_Exam_TempQuest>>(paperMod.QuestList);
                    foreach (M_Exam_TempQuest model in list)
                    {
                        DataRow[] drs = questDT.Select("p_id=" + model.id);
                        if (drs.Length < 1) { continue; }
                        drs[0]["p_defaultscores"] = model.score;
                        drs[0]["order"] = model.order;
                    }
                }
            }//paper end;
            return questDT;
        }
        public DataTable SelByIDSForType(string ids, string type = "large")
        {
            string fields = "";
            switch (type)
            {
                case "large":
                    fields = "p_id,p_title,p_Content,p_type,orderid=0";
                    break;
                default:
                    fields = "*";
                    break;
            }
            return SelByIDS(1, int.MaxValue, ids, 99, fields).dt;
        }
        /// <summary>
        /// 用于管理试卷下题目
        /// </summary>
        public PageSetting SelByIDS(int cpage, int psize, string ids, int qtype = 99, string fields = "*")
        {
            ids = StrHelper.PureIDSForDB(ids);
            if (string.IsNullOrEmpty(ids)) return null;
            if (!SafeSC.CheckIDS(ids)) { return null; }
            fields += ",IsRight=0,Remark=''";
            //string sql = "SELECT " + fields + ",IsRight=0,Remark='' FROM " + strTableName + " WHERE p_id IN(" + ids + ") ";
            string where = " p_id IN(" + ids + ")";
            if (qtype != 99)//过滤大题
            {
                where += " AND p_Type=" + qtype;
            }
            else { where += " AND P_Type!=10"; }
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where, "p_type", null, fields);
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable SelByIDS(string ids, int qtype = 99, string fields = "*")
        {
            ids = StrHelper.PureIDSForDB(ids);
            if (string.IsNullOrEmpty(ids)) return null;
            if (!SafeSC.CheckIDS(ids)) { return null; }
            string sql = "SELECT " + fields + ",IsRight=0,Remark='' FROM " + strTableName + " WHERE p_id IN(" + ids + ") ";
            if (qtype != 99)//过滤大题
            {
                sql += " AND p_Type=" + qtype;
            }
            else { sql += " AND P_Type!=10"; }
            sql += " ORDER BY p_type";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            return U_SelByFilter(cpage, psize, 0, 99, "");
        }
        /// <summary>
        /// 用于大题筛选小题等场景,用于前台搜索筛选
        /// </summary>
        /// <qtype>按题型筛选,99为获取全部</qtype>
        public PageSetting U_SelByFilter(int cpage, int psize, int classid, int qtype, string title, int uid = 0, int issmall = -1)
        {
            string where = "1=1 ";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("title", "%" + title + "%") };
            if (classid > 0)
            {
                where += " AND p_class=" + classid;
            }
            if (!string.IsNullOrEmpty(title))
            {
                where += " AND p_title LIKE @title ";
            }
            if (qtype != 99)
            {
                where += " AND p_Type=" + qtype;
            }
            if (uid > 0)
            {
                where += " AND(UserID=" + uid + " OR IsShare=1)";
            }
            if (issmall > -1)
            {
                where += " AND IsSmall=" + issmall;
            }
            PageSetting setting = new PageSetting()
            {
                cpage = cpage,
                psize = psize,
                t1 = strTableName,
                pk = PK,
                t2 = "",
                where = where,
                order = "p_CreateTime DESC",
                sp = sp
            };
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 用于后台搜索筛选
        /// </summary>
        /// <returns></returns>
        public DataTable SelByFilter(int classid, int qtype, int grade, int diff, int version, string keyword, int issmall = -1)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE 1=1 ";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("keyword", "%" + keyword + "%") };
            if (classid > 0)
            {
                sql += " AND p_class=" + classid;
            }
            if (qtype != 99)
            {
                sql += " AND p_Type=" + qtype;
            }
            if (grade > 0)
            {
                sql += " AND p_Views=" + grade;
            }
            if (diff > 0)
            {
                sql += " AND p_Difficulty=" + diff;
            }
            if (version > 0)
            {
                sql += " AND Version=" + version;
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " AND p_title LIKE @keyword ";
            }
            if (issmall > -1)
            {
                sql += " AND IsSmall=" + issmall;
            }
            sql += " ORDER BY p_CreateTime DESC";
            return SqlHelper.ExecuteTable(sql, sp);
        }
        public string GetContent(int qid, int qtype, string content)
        {
            int id = qid;

            string tlp = "(<span class='answersp' contenteditable='true'></span>)";
            string tlp2 = "(<span class='answersp'>{0}</span>)";
            string blank = "（）";
            switch (qtype)
            {
                case 2:
                    content = content.Replace(blank, tlp);
                    return content;
                case 4:
                    {
                        string[] conArr = Regex.Split(content, Regex.Escape(blank));
                        content = "";
                        for (int i = 0; i < conArr.Length; i++)
                        {
                            if (i != (conArr.Length - 1))
                            { content += conArr[i] + string.Format(tlp2, (i + 1)); }
                        }
                        return content;
                    }
                default:
                    return content;
            }
        }
        public string GetSubmit(int qid, int qtype, ref string AngularJS)
        {
            string option = SafeSC.ReadFileStr(M_Exam_Sys_Questions.OptionDir + qid + ".opt");
            string emptyTlp = "<span style='color:red;'>未定义选项</span>";
            //if (string.IsNullOrEmpty(option)) return "";
            int id = qid;
            JArray arr = JsonConvert.DeserializeObject<JArray>(option);
            StringBuilder builder = new StringBuilder();
            switch (qtype)
            {
                case (int)M_Exam_Sys_Questions.QType.Radio:
                    {
                        if (arr == null || arr.Count < 1) { return emptyTlp; }
                        string name = "srad_" + id;
                        string tlp = "<li class='opitem'><label><input type='radio' name='{0}' value='{1}'>{1}. {2}</label></li>";
                        foreach (JObject obj in arr)
                        {
                            builder.Append(string.Format(tlp, name, obj["op"], obj["val"]));
                        }
                    }
                    break;
                case (int)M_Exam_Sys_Questions.QType.Multi:
                    {
                        if (arr == null || arr.Count < 1) { return emptyTlp; }
                        string name = "mchk_" + id;
                        string tlp = "<li class='opitem'><label><input class='opitem' type='checkbox' name='{0}' value='{1}'>{1}. {2}</label></li>";
                        foreach (JObject obj in arr)
                        {
                            builder.Append(string.Format(tlp, name, obj["op"], obj["val"]));
                        }
                    }
                    break;
                case (int)M_Exam_Sys_Questions.QType.FillBlank:
                    {
                        //string tlp = "<div contenteditable='true' class='answerdiv'>解：</div>";
                        //builder.Append(tlp);
                    }
                    break;
                case (int)M_Exam_Sys_Questions.QType.Answer://放置一个ueditor
                    {
                        string name = "answer_" + id;
                        string tlp = "<div id='" + name + "' contenteditable='true' class='answerdiv'>解：</div>";
                        builder.Append(tlp);
                    }
                    break;
                case (int)M_Exam_Sys_Questions.QType.FillTextBlank:
                    {
                        if (arr == null || arr.Count < 1) { return emptyTlp; }
                        string name = "filltextblank_" + id;
                        string tlp = "<li style='float:none;' ng-repeat='item in list." + name + "|orderBy:\"id\"'>"
                                     + "<div><div class='title'>{{item.id}},{{item.title}}</div>"
                                     + "<ul class='submitul'>"
                                     + "<li class='opitem' ng-repeat='opt in item.opts'><label><input type='radio' class='opitem' ng-value='opt.op' ng-model='item.answer'/>{{opt.op}}. <span ng-bind-html='opt.val | to_trusted'></span></label></li>"
                                     + "</ul></div><div style='clear:both;'></div></li>";
                        AngularJS += "$scope.list[\"" + name + "\"]=" + option + ";idsArr.push(" + id + ");";
                        builder.Append(tlp);
                    }
                    break;
            }
            return builder.ToString();
        }
        /*--------------------------------------------------------------------------*/
        public int GetInsert(M_Exam_Sys_Questions model)
        {
            B_KeyWord keybll = new B_KeyWord();
            keybll.AddKeyWord(model.Tagkey, 2);
            return DBCenter.Insert(model);
        }
        public bool GetUpdate(M_Exam_Sys_Questions model)
        {
            B_KeyWord keybll = new B_KeyWord();
            keybll.AddKeyWord(model.Tagkey, 2);
            return DBCenter.UpdateByID(model, model.p_id);
        }
        public bool DeleteByGroupID(int QuestionsID)
        {
            return Sql.Del(strTableName, PK + "=" + QuestionsID);
        }
        public void DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM " + strTableName + " WHERE " + PK + " IN(" + ids + ")";
            SqlHelper.ExecuteSql(sql);
        }
        public void U_DelByIDS(string ids, int uid)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM " + strTableName + " WHERE " + PK + " IN(" + ids + ")" + " AND UserID=" + uid;
            SqlHelper.ExecuteSql(sql);
        }
        public M_Exam_Sys_Questions GetSelect(int QuestionsID)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_Exam_Sys_Questions] WHERE [p_id] = @p_id";
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
                    return new M_Exam_Sys_Questions();
                }
            }
        }
        public DataTable Select_All()
        {
            return Sql.Sel(strTableName, "", "p_id desc");
        }
        public DataTable Select_All(int paperid, int userid = 0)
        {
            string wherestr = "";
            if (userid > 0)
            { wherestr = " AND UserID=" + userid; }
            return Sql.Sel(strTableName, "(Paper_Id=" + paperid + " OR Paper_ID=0) " + wherestr, "p_id desc");
        }
        /// <summary>
        /// 按类型管理试题
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="value">条件值</param>
        /// <returns></returns>
        public DataTable SelectByType(int type = 0, string value = "")
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@value", value) };
            string wherestr = "";
            switch (type)
            {
                case 1://按类别
                    wherestr += " AND p_Shipin=@value";
                    break;
                case 2://按题型
                    wherestr += " AND p_Type=@value";
                    break;
                case 3://按难度
                    wherestr += " AND p_Difficulty=@value";
                    break;
            }
            string sql = "SELECT * FROM " + strTableName + " WHERE 1=1 " + wherestr + " ORDER BY p_CreateTime DESC";
            return SqlHelper.ExecuteTable(sql, sp);
        }
        public string SelQuesTitleByIds(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "SELECT p_title FROM " + strTableName + " WHERE p_id IN (" + ids + ")";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql);
            string titles = "";
            foreach (DataRow item in dt.Rows)
            {
                titles += item["p_title"].ToString() + ",";
            }
            return titles.Trim(',');
        }
        /// <summary>
        /// 根据标题和内容查询试题记录
        /// </summary>
        public M_Exam_Sys_Questions GetSelectByCon(string title, string content)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_Exam_Sys_Questions] WHERE [p_title] like @p_title AND [p_Content] like @p_Content AND (parentId <=0 OR parentId is null) order by p_id desc";
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
                    return new M_Exam_Sys_Questions();
                }
            }
        }

        /// <summary>
        /// 根据标题和视频查询试题记录
        /// </summary>
        public M_Exam_Sys_Questions GetSelectByShipin(string title, string shipin)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_Exam_Sys_Questions] WHERE [p_title] like @p_title AND [p_Shipin] like @p_Shipin AND (parentId <=0 OR parentId is null) order by p_id desc";
            SqlParameter[] cmdParams = new SqlParameter[2];
            cmdParams[0] = new SqlParameter("@p_title", SqlDbType.NVarChar, 255);
            cmdParams[0].Value = title;
            cmdParams[1] = new SqlParameter("@p_Shipin", SqlDbType.NVarChar, 255);
            cmdParams[1].Value = shipin;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Exam_Sys_Questions();
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
        public DataTable GetSelectByCondition(int nd, int tx, int order, int c_ids, int k_ids)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_Exam_Sys_Questions] WHERE 1=1 AND (parentId <=0 OR parentId is null)";

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
        public DataTable GetSelectByC_ClassId(int C_ClassId)
        {
            string sqlStr = "SELECT [C_id],[C_ClassName],[C_Classid],[C_OrderBy]FROM [dbo].[ZL_Exam_Class] WHERE [C_Classid] =" + C_ClassId + " order by C_OrderBy";
            DataTable dt = new DataTable();
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
                string sqlStr = "SELECT * FROM [dbo].[ZL_Exam_Sys_Questions] where [p_id] in (" + sessionid + ") AND (parentId <=0 OR parentId is null)";
                return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据ID批量查询
        /// </summary>
        /// <param name="ids">id,多个以逗号隔开</param>
        /// <returns></returns>
        public DataTable Select_ids(string ids)
        {
            string sqlStr = "SELECT * FROM  [dbo].[ZL_Exam_Sys_Questions] WHERE p_id IN (" + ids + ") AND (parentId <=0 OR parentId is null)";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }

        /// <summary>
        /// 通过题型、分类查询
        /// </summary>
        /// <param name="type">题型ID</param>
        /// <param name="Classid">分类ID</param>
        /// <returns></returns>
        public DataTable Select_Type(int type, int num, int Classid)
        {
            string sqlStr = "SELECT Top " + num + " * FROM ZL_Exam_Sys_Questions WHERE p_Type=@p_Type AND p_Class=@p_Class  AND (parentId <=0 OR parentId is null) order by newid()";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@p_Type",type),
                new SqlParameter("@p_Class",Classid)
            };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, para);
        }


        /// <summary>
        /// 通过父题ID查询
        /// </summary>
        /// <param name="parentid">父题ID(组合题所属小题)</param>
        /// <returns></returns>
        public DataTable Select_ParentId(int parentid)
        {
            return Sql.Sel(strTableName, "parentId=" + parentid, null);
        }
        public bool UpdateByPaperID(string ids, int paperid)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "Update " + strTableName + " SET Paper_ID=0 WHERE Paper_Id=" + paperid + " Update " + strTableName + " SET Paper_Id=" + paperid + " WHERE p_id IN (" + ids + ")";
            return SqlHelper.ExecuteSql(sql);
        }
        //根据IDS计算出各类型题的数目,给Js展示
        public string GetCountByIDS(string ids)
        {
            ids = ids ?? "";
            ids = ids.Trim(',');
            string result = "";
            string sql = "SELECT p_Type as type,typestr='',COUNT(*) AS count FROM " + strTableName + " WHERE p_id IN(" + ids + ") GROUP BY p_Type";
            if (string.IsNullOrEmpty(ids)) return result;
            SafeSC.CheckIDSEx(ids);
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["typestr"] = M_Exam_Sys_Questions.GetTypeStr(DataConvert.CLng(dt.Rows[i]["type"]));
                }
                result = JsonConvert.SerializeObject(dt);
            }
            return result;
        }
        public void CountDiffcult()
        {
            DateTime time = DateTime.Now.AddDays(-7);
            string stime = "", etime = "";
            DateHelper.GetWeekSE(time, ref stime, ref etime);
            CountDiffcult(stime, etime);
        }

        /// <summary>
        /// 根据起始和结束时间,自动计算出难度系数
        /// 1,忽略教师未批阅的
        /// 2,如果本周未有学生考试则不更新
        /// 3,(计算公式是否应该改为难度偏移计算,以更准确)
        /// </summary>
        public void CountDiffcult(string stime, string etime)
        {
            string where = "WHERE IsRight>0 AND QID>0 ";
            if (!string.IsNullOrEmpty(stime)) { where += " AND CDate>=@stime"; }
            if (!string.IsNullOrEmpty(etime)) { where += " AND CDate<=@etime"; }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("stime", DataConvert.CDate(stime)), new SqlParameter("etime", DataConvert.CDate(etime)) };
            string sql = "SELECT Qid,"
                          + "(SELECT COUNT(*)AS Num FROM ZL_Exam_Answer WHERE QID=A.Qid AND IsRight=1)as rightNum,"
                          + "(SELECT COUNT(*)AS Num FROM ZL_Exam_Answer WHERE QID=A.Qid AND IsRight=2)as wrongNum "
                          + "FROM ZL_Exam_Answer A " + where + " GROUP BY QID";
            DataTable dt = SqlHelper.ExecuteTable(sql, sp);
            foreach (DataRow dr in dt.Rows)
            {
                double diff = DataConvert.CDouble(dr["rightNum"]) / (DataConvert.CDouble(dr["wrongNum"]) + DataConvert.CDouble(dr["rightNum"]));
                string upsql = "UPDATE " + strTableName + " SET p_Difficulty=" + diff + " WHERE p_id=" + dr["QID"];
                SqlHelper.ExecuteSql(upsql);
            }
        }
        public string GetDiffStr(double diff)
        {
            if (diff >= 0.91) { return "基础"; }
            if (diff >= 0.81) { return "容易"; }
            if (diff >= 0.51) { return "中等"; }
            if (diff >= 0.31) { return "偏难"; }
            return "极难";
        }
    }
}
