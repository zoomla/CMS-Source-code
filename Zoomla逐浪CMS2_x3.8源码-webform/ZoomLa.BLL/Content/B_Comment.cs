namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    using System.Collections.Generic;
    public class B_Comment
    {
        private string TbName, PK;
        private M_Comment initMod = new M_Comment();
        public B_Comment()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public M_Comment SelReturnModel(int ID)
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
        public M_Comment SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, strWhere))
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
            return Sql.Sel(TbName);
        }
        public PageSetting SelPage(int cpage, int psize, int nid, int gid, int uid)
        {
            string pk = "A." + PK;
            string where = " 1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (nid > 0) { where += " AND B.NodeID=" + nid; }
            if (gid > 0) { where += " AND A.GeneralID=" + gid; }
            if (uid > 0) { where += " AND A.UserID=" + uid; }
            PageSetting setting = PageSetting.Double(cpage, psize, TbName, "ZL_CommonModel", pk, "A.GeneralID=B.GeneralID", where, pk + " DESC", sp, "A.*,B.Title AS GTitle");
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateByID(M_Comment model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.CommentID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public int insert(M_Comment model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        /// 发表(增加)评论
        /// </summary>
        /// <param name="commentInfo"></param>
        /// <returns></returns>
        public bool Add(M_Comment model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model)) > 0;
        }
        /// <summary>
        /// 审核指定ID的评论
        /// </summary>
        /// <param name="commentID"></param>
        /// <returns></returns>
        public bool Update_ByAudited_ID(int ID, bool Audited)
        {
            int aut = Audited ? 1 : 0;
            string sql = "UPDATE " + TbName + " SET Audited=" + aut + " where CommentID=" + ID;
            return SqlHelper.ExecuteSql(sql);
        }
        public bool UpdateByCIDs(string commentIds)
        {
            string[] commentIdsArr = commentIds.Split(',');
            string sql = "";
            SqlParameter[] sp = new SqlParameter[commentIdsArr.Length];

            for (int i = 0; i < commentIdsArr.Length; i++)
            {
                sp[i] = new SqlParameter("commentIds" + i, commentIdsArr[i]);
                sql += "@" + sp[i].ParameterName + ",";
            }
            sql = sql.TrimEnd(',');
            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, "update dbo.ZL_Comment set status=10 where commentID in (" + sql + ")", sp);
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public bool Delete(int ID)
        {
            return Sql.Del(TbName, PK + "=" + ID);
        }
        public DataTable SeachCommentByGid1(int generalID)
        {
            //string sql = "Select *,,"
            //            + " From " + TbName + " A Where GeneralID =" + generalID
            //            + " AND Audited=1 ORDER BY CommentTime DESC";
            //return SqlHelper.ExecuteTable(CommandType.Text, sql);

            string fields = "*,";
            fields += "(SELECT COUNT(status) FROM ZL_Comment_Count PKCount WHERE PKCount.status=1 AND PKCount.CommentID=A.CommentID) AS AgreeCount,";
            fields += "(SELECT COUNT(status) FROM ZL_Comment_Count PKCount WHERE PKCount.status=0 AND PKCount.CommentID=A.CommentID) AS DontCount,";
            fields += "ROW_NUMBER() OVER(ORDER BY CommentID) AS Layer ";
            string where = "GeneralID=" + generalID + " AND Audited=1 ";
            string order = PK + " DESC";
            return DBCenter.SelWithField(TbName, fields, where, order);
        }
        public DataTable SeachCommentAudit(bool Audited, int nodeID)
        {
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@Audit",SqlDbType.Bit)
            };
            sp[0].Value = Audited;
            return SqlHelper.JoinQuery("*", "ZL_Comment", "ZL_CommonModel", "A.GeneralID=B.GeneralID", "A.Audited=@Audit AND B.NodeID IN (SELECT NodeID FROM ZL_Node WHERE ParentID= " + nodeID.ToString() + " or NodeID= " + nodeID.ToString() + ")");
        }
        public DataTable SeachCommentAll(int nodeID)
        {
            return SqlHelper.JoinQuery("A.*,B.Title", "ZL_Comment", "ZL_CommonModel", "A.GeneralID=B.GeneralID", "ZL_CommonModel.NodeID = " + nodeID.ToString());
        }
        public DataTable SeachCommentsAll(int nodeID)
        {
            return SqlHelper.JoinQuery("A.*,B.Title", "ZL_Comment", "ZL_CommonModel", "A.GeneralID=B.GeneralID", "ZL_CommonModel.NodeID  in (select NodeID from ZL_Node where ParentID= " + nodeID.ToString() + " or NodeID= " + nodeID.ToString() + ")");
        }
        public DataTable SeachAllComment(string strwhere="")
        {
            return SqlHelper.JoinQuery("A.*,B.Title,B.HtmlLink,(SELECT COUNT(*) FROM ZL_Comment_Count Comm WHERE A.CommentID=Comm.CommentID AND Comm.status=1) AS Agree,"
                                        + "(SELECT COUNT(*) FROM ZL_Comment_Count Comm WHERE A.CommentID=Comm.CommentID AND Comm.status=-1) AS DisAgree", "ZL_Comment", "ZL_CommonModel", "A.GeneralID=B.GeneralID"
                                        , strwhere, "A.CommentTime DESC");
        }
        public DataTable SeachComment()
        {
            return SqlHelper.JoinQuery("A.*,B.Title", "ZL_Comment", "ZL_CommonModel", "A.GeneralID=B.GeneralID");
        }
        public DataTable SeachAllComment(bool Audited)
        {
            return SeachAllComment("A.Audited=" + (Audited ? 1 : 0));   
        }
        /// <summary>
        /// 查询某内容的评论
        /// </summary>
        /// <param name="generalID"></param>
        /// <returns></returns>
        public DataTable SeachComment_ByGeneralID(int generalID)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("generalID", generalID) };
            return SqlHelper.JoinQuery("A.*,B.Title", "ZL_Comment", "ZL_CommonModel", "A.GeneralID=B.GeneralID", "ZL_Comment.GeneralID = @generalID", "", sp);
        }
        public DataTable SeachCommentByID(int GeneralID, int status)
        {
            string sqlStr = "SELECT [CommentID],[GeneralID],[Title],[Contents] ,[Audited],(select userName from zl_user where zl_user.UserID=ZL_Comment.UserID) as UserID,[CommentTime],[Score] ,[PK] ,[files] ,[status] from ZL_Comment where GeneralID =@GeneralID and status=@status ";
            SqlParameter[] param = { new SqlParameter("@GeneralID", SqlDbType.Int, 4), new SqlParameter("@status", SqlDbType.Int, 4) };
            param[0].Value = GeneralID;
            param[1].Value = status;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, param);
        }
        /// <summary>
        /// 查询某内容指定标题的评论
        /// </summary>
        /// <param name="generalID"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        public DataTable SeachComment_ByIDTitle(int generalID, string Title)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Title", Title) };
            string sqlStr = "select * from ZL_Comment where GeneralID = " + generalID.ToString() + " and Title=@Title";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, sp);
        }
        public DataTable SeachComment_ByIDTitle(int generalID, string Title, string sql)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Title", Title) };
            string sqlStr = "select * from ZL_Comment where GeneralID = " + generalID.ToString() + " and Title=@Title";
            if (sql != "")
            {
                sqlStr = sqlStr + sql;
            }

            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, sp);
        }
        /// <summary>
        /// 返回某内容指定标题的评论
        /// </summary>
        public DataSet SeachCommentByGid(int generalID, string title)
        {
            DataSet ds = new DataSet();
            string sql = "Select * From " + TbName + " Where GeneralID =" + generalID + " and pid={0} and Audited=1 And Title=@title";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("title", title) };
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, string.Format(sql, 0), sp);
            dt.TableName = "0";
            ds.Tables.Add(CloneTable(dt, "0"));

            GetNodes(ref ds, dt, sql, sp);
            return ds;
        }
        public DataTable SeachCommentByGidtoTable(int generalID, string Title)
        {
            DataTable dtAll = new DataTable();
            DataSet ds = SeachCommentByGids(generalID, Title);
            if (ds.Tables.Count > 0)
            {
                foreach (DataColumn col in ds.Tables[0].Columns)
                {
                    dtAll.Columns.Add(new DataColumn(col.ColumnName, col.DataType));
                }
                GetNodes(ds, dtAll, ds.Tables[0]);//
            }
            return dtAll;
        }
        private DataSet SeachCommentByGids(int generalID, string title)
        {
            DataSet ds = new DataSet();
            string sql = "Select * From " + TbName + " Where GeneralID =" + generalID + " and pid={0} and Audited=1 And Title=@title";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("title", title) };
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
            dt.TableName = "0";
            ds.Tables.Add(CloneTable(dt, "0"));
            GetNodes(ref ds, dt, sql, sp);  //递归

            return ds;
        }
        void GetNodes(DataSet ds, DataTable dtAll, DataTable dt)
        {
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {

                    DataRow allrow = dtAll.NewRow();
                    foreach (DataColumn col in dtAll.Columns)
                    {
                        allrow[col.ColumnName] = row[col.ColumnName];
                    }
                    dtAll.Rows.Add(allrow);

                    string key = row["CommentID"].ToString();

                    DataTable node = ds.Tables[key];
                    GetNodes(ds, dtAll, node);  //递归

                }
            }

        }
        /// <summary>
        /// 获取某内容指定标题的评论的支持方或反对方总数
        /// </summary>
        /// <param name="generalID"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        public int GetComment_CountIDTitlePK(int generalID, bool PKS)
        {
            string sqlStr = "select count(*) from ZL_Comment where GeneralID = " + generalID + " AND pid=0";
            if (PKS)
            {
                sqlStr = sqlStr + " and PKS=1";//支持方
            }
            else
            {
                sqlStr = sqlStr + " and PKS=0";//反对方
            }
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr));
        }
        /// <summary>
        /// 查询某节点的是否审核通过的评论
        /// </summary>
        /// <param name="Audited"></param>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public DataTable SeachComment_ByAudited(bool Audited, int nodeID)
        {
            int aut = Audited ? 1 : 0;
            return SqlHelper.JoinQuery("A.*,B.Title", TbName, "ZL_CommonModel", "A.GeneralID=B.GeneralID", "where A.Audited =" + aut + " and B.NodeID=" + nodeID);
        }
        /// <summary>
        /// 查询用户发表的评论
        /// </summary>
        public DataTable SearchByUser(int UserID, int NodeID)
        {
            string sqlStr = "A.UserID = " + UserID.ToString();
            if (NodeID != 0)
            {
                sqlStr = sqlStr + " and B.NodeID=" + NodeID.ToString() + " order by A.GeneralID,A.CommentTime Desc";
            }
            return SqlHelper.JoinQuery("A.*,B.Title", TbName, "ZL_CommonModel", "A.GeneralID=B.GeneralID", sqlStr);
        }
        /// <summary>
        /// 根据用户ID、标题查询用户发布的评论
        /// </summary>
        public DataTable SearchByUser(int UserID, string searchTitle, int type, string status)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("searchTitle", "%" + searchTitle + "%"), new SqlParameter("status", status) };
            string sqlStr = "select * from ZL_Comment where UserID = " + UserID.ToString();
            if (!string.IsNullOrEmpty(searchTitle))
            {
                if (type == 0)
                    sqlStr = sqlStr + " and Title like @searchTitle";
                else
                    sqlStr = sqlStr + " and CommentID=" + DataConverter.CLng(searchTitle).ToString();
            }
            if (status != "")
            {
                sqlStr += "and status=@status";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, sp);
        }
        /// <summary>
        /// 查询文章评论
        /// </summary>
        /// <param name="generalID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable SeachComment_ByItemIDAudio(int generalID, int type)
        {
            string where = "";
            switch (type)
            {
                case 1:
                    where = "A.GeneralID =" + generalID + " and Audited=1";
                    break;
                case 2:
                    where = "A.GeneralID =" + generalID + " and Audited=0";
                    break;
                case 0:
                    where = "A.GeneralID =" + generalID;
                    break;
                default:
                    where = "A.GeneralID =" + generalID;
                    break;
            }
            return SqlHelper.JoinQuery("A.*,B.Title", "ZL_Comment", "ZL_CommonModel", "A.GeneralID=B.GeneralID", where, "CommentID desc");
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="status"></param>
        /// <param name="commentIds"></param>
        /// <returns></returns>
        public bool UpdateByCIDs(int status, string commentIds)
        {
            string[] commentIdsArr = commentIds.Split(',');
            string sql = "";
            SqlParameter[] sp = new SqlParameter[commentIdsArr.Length + 1];

            for (int i = 0; i < commentIdsArr.Length; i++)
            {
                sp[i] = new SqlParameter("commentIds" + i, commentIdsArr[i]);
                sql += "@" + sp[i].ParameterName + ",";
            }
            sql = sql.TrimEnd(',');
            sp[commentIdsArr.Length] = new SqlParameter("status", status);
            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, "update dbo.ZL_Comment set status=@status where commentID in (" + sql + ")", sp);
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public DataTable SeachCommentByGeneralID(int generalID, int status)
        {
            string sqlStr = "SELECT * from ZL_Comment where GeneralID =@GeneralID and status=@status ";
            SqlParameter[] param = { new SqlParameter("@GeneralID", SqlDbType.Int, 4), new SqlParameter("@status", SqlDbType.Int, 4) };
            param[0].Value = generalID;
            param[1].Value = status;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, param);
        }
        public DataTable SeachComment_ByGeneralID2(int generalID)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("generalID", generalID) };
            string sqlStr = "select * from ZL_Comment where GeneralID =@generalID";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, sp);
        }
        /// <summary>
        /// 复制表
        /// </summary>
        DataTable CloneTable(DataTable dt, string dtName)
        {
            DataTable clonedt = new DataTable();
            clonedt.TableName = dtName;
            if (dt != null && dt.Rows.Count > 0)
            {
                string[] arrs = new string[dt.Columns.Count];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    DataColumn col = dt.Columns[i];
                    arrs[i] = col.ColumnName;
                    clonedt.Columns.Add(new DataColumn(col.ColumnName, col.DataType));
                }
                foreach (DataRow row in dt.Rows)
                {
                    DataRow colrow = clonedt.NewRow();
                    foreach (string key in arrs)
                    {
                        colrow[key] = row[key];
                    }
                    clonedt.Rows.Add(colrow);
                }
                return clonedt;
            }
            else
            {
                return clonedt;
            }
        }
        void GetNodes(ref DataSet ds, DataTable dt, string sql, SqlParameter[] sp)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    int pid = Convert.ToInt32(row["CommentID"]);
                    DataTable dtnode = SqlHelper.ExecuteTable(CommandType.Text, string.Format(sql, pid), sp);
                    ds.Tables.Add(CloneTable(dtnode, row["CommentID"].ToString()));
                    GetNodes(ref ds, dtnode, sql, sp);
                }
            }
        }
        //举报操作
        public bool ReportComment(int cid, int uid)
        {
            string sql = "UPDATE " + TbName + " SET ReprotIDS+='," + uid + ",' WHERE CommentID=" + cid + " AND ReprotIDS NOT LIKE '," + uid + ",'";
            return SqlHelper.ExecuteSql(sql);
        }
        //支持操作
        public bool Support(int cid, int flag, string ip, int uid, string uname,int gid=0)
        {
            SqlParameter[] sp = new SqlParameter[5];
            sp[0] = new SqlParameter("@cid", cid);
            sp[1] = new SqlParameter("@flag", flag);
            sp[2] = new SqlParameter("@ip", ip);
            sp[3] = new SqlParameter("@uid", uid);
            sp[4] = new SqlParameter("@uname", uname);
            string strwhere = gid > 0 ? "GeneralID=" + gid : "CommentID=@cid";
            string sql = "SELECT COUNT(*) FROM ZL_Comment_Count WHERE " + strwhere + " AND IP=@ip";
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sql, sp));
            if (count == 0)
            {
                sql = "INSERT INTO ZL_Comment_Count(CommentID,status,IP,UserID,UserName,CDate,GeneralID) VALUES(@cid,@flag,@ip,@uid,@uname,'" + DateTime.Now.ToString() + "',"+gid+")";
                return SqlHelper.ExecuteSql(sql, sp);
            }
            return false;
        }
        public bool Support(int cid, int flg, string ip,int gid=0)
        {
            return Support(cid,flg,ip,0,"",gid);
        }
        public int GetUpCount(int gid,int flag)
        {
            string sql = "SELECT COUNT(*) FROM ZL_Comment_Count WHERE GeneralID=" + gid + " AND status="+flag;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sql));
        }
        //------------------------
        public void U_Del(int uid, string ids)
        {
            if (string.IsNullOrEmpty(ids)) {return; }
            SafeSC.CheckIDSEx(ids);
            DBCenter.DelByWhere(TbName, "CommentID IN (" + ids + ") AND UserID=" + uid);
        }
    }
}
