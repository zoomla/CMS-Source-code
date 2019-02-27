namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using System.Net.Mail;
    using System.Collections.Generic;
    using ZoomLa.Components;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_IServer
    {
        public B_IServer()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public string PK, strTableName;
        public M_IServer initMod = new M_IServer();
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
        public M_IServer SeachById(int ID)
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
        /// 根据条件查询一条记录
        /// </summary>
        public M_IServer SelReturnModel(string strWhere)
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

        /// <summary>
        /// 查询所有记录
        /// </summary>
        public DataTable SeachIServerAll()
        {
            return Sql.Sel(strTableName);
        }

        /// <summary>
        /// 排序
        /// </summary>
        public DataTable Sel(string strWhere, string strOrderby)
        {
            return Sql.Sel(strTableName, strWhere, strOrderby);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public DataTable SelPage(string strWhere, string strOrderby, int pageNum, int pageSize)
        {
            return Sql.SelPage(strTableName, PK, strWhere, strOrderby, pageNum, pageSize);
        }

        public bool UpdateByID(M_IServer model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.QuestionId.ToString(), initMod.GetFieldAndPara(), initMod.GetParameters());
        }

        public bool DeleteById(int ID)
        {
            return Sql.Del(strTableName, "QuestionId=" + ID.ToString());
        }

        public bool Del(string strWhere)
        {
            return Sql.Del(strTableName, strWhere);
        }


        public int AddQuestion(M_IServer model)
        {
            return Sql.insertID(model.TbName, initMod.GetParameters(), initMod.GetParas(), initMod.GetFields());
        }

        //添加问题
        public bool Add(M_IServer model)
        {
            return AddQuestion(model) > 0;
        }

        // 更新问题信息
        public bool Update(M_IServer model)
        {
            UpdateByID(model);
            return true;
        }


        //根据用户名查询
        public static DataTable SeachByUserId(int UserId)
        {
            string sqlStr = "SELECT * FROM ZL_IServer where UserId=@UserId Order by SubTime desc";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@UserId", SqlDbType.Int),
            };
            cmdParams[0].Value = UserId;

            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }

        /// <summary>
        /// 根据用户名查询问题分类（分组查询）
        /// 当userid小于0时，则查询所有
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>return DateTable</returns>
        public DataTable GetSeachUserIdType(int userid)
        {
            string sql = "SELECT Type FROM ZL_IServer where 1=1 ";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@UserId", SqlDbType.VarChar);
            if (userid > 0)
            {
                sql += " and UserId=@UserId";
                cmdParams[0].Value = userid;
            }
            sql += " group by Type";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, cmdParams);
        }

        //根据部分条件查询
        public static DataTable SeachSendIServer(int QuestionId, int UserId, string Title, string State, string Type, string Root, string Priority, DateTime BetweenSubTime, DateTime ToSubTime)
        {
            string sqlStr = "SELECT * FROM ZL_IServer WHERE 1=1";
            SqlParameter[] parameter = new SqlParameter[9];
            parameter[0] = new SqlParameter("@QuestionId", SqlDbType.Int);
            parameter[1] = new SqlParameter("@UserId", SqlDbType.Int);
            parameter[2] = new SqlParameter("@Title", SqlDbType.NVarChar, 100);
            parameter[3] = new SqlParameter("@State", SqlDbType.VarChar);
            parameter[4] = new SqlParameter("@Type", SqlDbType.VarChar);
            parameter[5] = new SqlParameter("@Root", SqlDbType.VarChar);
            parameter[6] = new SqlParameter("@Priority", SqlDbType.VarChar);
            parameter[7] = new SqlParameter("@BetweenSubTime", SqlDbType.DateTime);
            parameter[8] = new SqlParameter("@ToSubTime", SqlDbType.DateTime);
            if (QuestionId > 0)
            {
                sqlStr = sqlStr + " AND QuestionId=@QuestionId ";
                parameter[0].Value = QuestionId;
            }
            if (UserId > 0)
            {
                sqlStr = sqlStr + "AND UserId = @UserId";
                parameter[1].Value = UserId;
            }
            if (Title.Length > 0)
            {
                sqlStr = sqlStr + "AND Title = @Title";
                parameter[2].Value = Title;
            }
            if (State.Length > 0)
            {
                sqlStr = sqlStr + "AND State = @State";
                parameter[3].Value = State;
            }
            if (Type.Length > 0)
            {
                sqlStr = sqlStr + "AND Type = @Type";
                parameter[4].Value = Type;
            }
            if (Root.Length > 0)
            {
                sqlStr = sqlStr + "AND Root = @Root";
                parameter[5].Value = Root;
            }
            if (Priority.Length > 0)
            {
                sqlStr = sqlStr + "AND Priority = @Priority";
                parameter[6].Value = Priority;
            }
            if (BetweenSubTime != null && ToSubTime != null)
            {
                sqlStr = sqlStr + "AND SubTime between @BetweenSubTime and @ToSubTime";
                parameter[7].Value = BetweenSubTime;
                parameter[8].Value = ToSubTime;
            }

            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, parameter);
        }
        public PageSetting SelPage(int cpage, int psize, int qid = -100, int uid = -100, string title = "", string state = "", string type = "", string root = "", string priority = "", DateTime? BetweenSubTime = null, DateTime? ToSubTime = null)
        {
            string where = " 1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (qid != -100) { where += " AND  QuestionId=" + qid; }
            if (uid != -100) { where += " AND UserId=" + uid; }
            if (!string.IsNullOrEmpty(title)) { where += " AND Title = @Title"; sp.Add(new SqlParameter("Title", title)); }
            if (!string.IsNullOrEmpty(state)) { where += " AND State = @State"; sp.Add(new SqlParameter("State", state)); }
            if (!string.IsNullOrEmpty(type)) { where += " AND Type = @Type"; sp.Add(new SqlParameter("Type", type)); }
            if (!string.IsNullOrEmpty(root)) { where += " AND Root = @Root"; sp.Add(new SqlParameter("Root", root)); }
            if (!string.IsNullOrEmpty(priority)) { where += " AND Priority=@Priority"; sp.Add(new SqlParameter("Priority", priority)); }
            if (BetweenSubTime != null && ToSubTime != null)
            {
                where += " AND SubTime between @BetweenSubTime and @ToSubTime";
                sp.Add(new SqlParameter("BetweenSubTime", BetweenSubTime));
                sp.Add(new SqlParameter("ToSubTime", ToSubTime));
            }
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where, "", sp);
            DBCenter.SelPage(setting);
            return setting;
        }

        public static int getiServerNum(string state, int UserId, int OrderID, string type = "")
        {
            string sqlStr = "SELECT COUNT(*) FROM ZL_IServer WHERE 1=1 ";
            SqlParameter[] cmdParams = new SqlParameter[3];
            cmdParams[0] = new SqlParameter("@State", SqlDbType.VarChar);
            cmdParams[1] = new SqlParameter("@UserId", SqlDbType.Int);
            cmdParams[2] = new SqlParameter("@OrderID", SqlDbType.Int);
            cmdParams[3] = new SqlParameter("@type", SqlDbType.VarChar);
            if (state.Length > 0)
            {
                sqlStr = sqlStr + " AND State=@State ";
                cmdParams[0].Value = state;
            }
            if (UserId > 0)
            {
                sqlStr = sqlStr + " AND UserId=@UserId ";
                cmdParams[1].Value = UserId;
            }

            if (OrderID > 0)
            {
                sqlStr = sqlStr + " AND OrderType=@OrderID";
                cmdParams[2].Value = OrderID;
            }
            if (type != "")
            {
                sqlStr = sqlStr + " AND Type=@type ";
                cmdParams[3].Value = type;
            }
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
            return count;
        }
        public static int getiServerNum(string state, int UserId, string type = "")
        {
            string sqlStr = "SELECT COUNT(*) FROM ZL_IServer WHERE 1=1 ";
            SqlParameter[] cmdParams = new SqlParameter[3];
            cmdParams[0] = new SqlParameter("@State", SqlDbType.VarChar);
            cmdParams[1] = new SqlParameter("@UserId", SqlDbType.Int);
            cmdParams[2] = new SqlParameter("@type", SqlDbType.VarChar);
            if (state.Length > 0)
            {
                sqlStr = sqlStr + " AND State=@State ";
                cmdParams[0].Value = state;
            }
            if (UserId > 0)
            {
                sqlStr = sqlStr + " AND UserId=@UserId ";
                cmdParams[1].Value = UserId;
            }
            if (type != "")
            {
                sqlStr = sqlStr + " AND Type=@type ";
                cmdParams[2].Value = type;
            }
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
            return count;
        }
        public static int getiServerNum(string state, int UserId, string orderall, string type, bool isrun)
        {
            string sqlStr = "SELECT COUNT(*) FROM ZL_IServer WHERE 1=1 ";
            SqlParameter[] cmdParams = new SqlParameter[3];
            cmdParams[0] = new SqlParameter("@State", SqlDbType.VarChar);
            cmdParams[1] = new SqlParameter("@UserId", SqlDbType.Int);
            cmdParams[2] = new SqlParameter("@type", SqlDbType.VarChar);
            if (state.Length > 0)
            {
                sqlStr = sqlStr + " AND State=@State ";
                cmdParams[0].Value = state;
            }
            if (UserId > 0)
            {
                sqlStr = sqlStr + " AND UserId=@UserId ";
                cmdParams[1].Value = UserId;
            }
            if (type != "")
            {
                sqlStr += " and Type=@type";
                cmdParams[2].Value = type;
            }
            if (orderall == "OrderType")
            {
                sqlStr = sqlStr + " AND OrderType>0";
            }
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
            return count;
        }
        /// <summary>
        /// 根据问题类型查询相应的总数
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="UserId">用户id</param>
        /// <param name="orderall">排序字段</param>
        /// <param name="type">问题的类型</param>
        /// <returns></returns>
        public int getiServerNum(string state, int UserId, string orderall, string type, int orderid)
        {
            string sqlStr = "SELECT COUNT(*) FROM ZL_IServer WHERE 1=1 ";
            SqlParameter[] cmdParams = new SqlParameter[4];
            cmdParams[0] = new SqlParameter("@State", SqlDbType.VarChar);
            cmdParams[1] = new SqlParameter("@UserId", SqlDbType.Int);
            cmdParams[2] = new SqlParameter("@type", SqlDbType.VarChar);
            cmdParams[3] = new SqlParameter("@OrderID", SqlDbType.Int);
            if (state.Length > 0)
            {
                sqlStr = sqlStr + " AND State=@State ";
                cmdParams[0].Value = state;
            }
            if (UserId > 0)
            {
                sqlStr = sqlStr + " AND UserId=@UserId ";
                cmdParams[1].Value = UserId;
            }
            if (type != "")
            {
                sqlStr = sqlStr + " AND Type=@type ";
                cmdParams[2].Value = type;
            }
            if (orderid > 0)
            {
                sqlStr = sqlStr + " AND OrderType=@OrderID";
                cmdParams[3].Value = orderid;
            }
            if (orderall == "OrderType")
            {
                sqlStr = sqlStr + " AND OrderType>0";
            }
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
            return count;
        }
        public static DataTable SeachTop(string State, int UserId, string type = "")
        {
            string sqlStr = "SELECT TOP 5 * FROM ZL_IServer WHERE 1 = 1 ";
            SqlParameter[] cmdParams = new SqlParameter[3];
            cmdParams[0] = new SqlParameter("@State", SqlDbType.VarChar);
            cmdParams[1] = new SqlParameter("@UserId", SqlDbType.Int);
            cmdParams[2] = new SqlParameter("@type", SqlDbType.VarChar);
            if (State.Length > 0)
            {
                sqlStr = sqlStr + " AND State = @State";
                cmdParams[0].Value = State;
            }
            if (UserId > 0)
            {
                sqlStr = sqlStr + " AND UserId = @UserId";
                cmdParams[1].Value = UserId;
            }
            if (type != "")
            {
                sqlStr = sqlStr + " AND Type=@type ";
                cmdParams[2].Value = type;
            }
            sqlStr = sqlStr + " ORDER BY SubTime DESC";

            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }
        public static DataTable SeachLikeTitle(string Title)
        {
            string sqlStr = "SELECT * FROM ZL_IServer WHERE 1=1 AND Title LIKE '%'+@Title+'%'";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@Title", SqlDbType.VarChar);
            cmdParams[0].Value = Title;
            sqlStr = sqlStr + " ORDER BY SubTime DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }
        public static DataTable SeachTop(string State, int UserId, string orderall, bool isrun)
        {
            string sqlStr = "SELECT TOP 5 * FROM ZL_IServer WHERE 1 = 1 ";
            SqlParameter[] cmdParams = new SqlParameter[2];
            cmdParams[0] = new SqlParameter("@State", SqlDbType.VarChar);
            cmdParams[1] = new SqlParameter("@UserId", SqlDbType.Int);
            if (State.Length > 0)
            {
                sqlStr = sqlStr + " AND State = @State";
                cmdParams[0].Value = State;
            }
            if (UserId > 0)
            {
                sqlStr = sqlStr + " AND UserId = @UserId";
                cmdParams[1].Value = UserId;
            }

            if (orderall == "OrderType")
            {
                sqlStr = sqlStr + " AND UserId = @UserId and OrderType>0";
            }
            sqlStr = sqlStr + " ORDER BY SubTime DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }

        /// <summary>
        /// 根据问题类型查询相应的前5条数据
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="UserId">用户id</param>
        /// <param name="orderall">排序字段</param>
        /// <param name="type">问题的类型</param>
        /// <returns></returns>
        public DataTable SeachTop(string State, int UserId, string orderall, string type, int orderid)
        {
            string sqlStr = "SELECT TOP 5 * FROM ZL_IServer WHERE 1 = 1 ";
            SqlParameter[] cmdParams = new SqlParameter[4];
            cmdParams[0] = new SqlParameter("@State", SqlDbType.VarChar);
            cmdParams[1] = new SqlParameter("@UserId", SqlDbType.Int);
            cmdParams[2] = new SqlParameter("@type", SqlDbType.VarChar);
            cmdParams[3] = new SqlParameter("@OrderID", SqlDbType.Int);
            if (State.Length > 0)
            {
                sqlStr = sqlStr + " AND State = @State";
                cmdParams[0].Value = State;
            }
            if (UserId > 0)
            {
                sqlStr = sqlStr + " AND UserId = @UserId";
                cmdParams[1].Value = UserId;
            }
            if (type != "")
            {
                sqlStr = sqlStr + " AND Type=@type ";
                cmdParams[2].Value = type;
            }
            if (orderid > 0)
            {
                sqlStr = sqlStr + " AND OrderType=@OrderID";
                cmdParams[3].Value = orderid;
            }
            if (orderall == "OrderType")
            {
                sqlStr = sqlStr + " AND UserId = @UserId and OrderType>0";
            }

            sqlStr = sqlStr + " ORDER BY SubTime DESC";

            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }

        public static DataTable SeachTop(string State, int UserId, int OrderType)
        {
            string sqlStr = "SELECT TOP 5 * FROM ZL_IServer WHERE 1 = 1 ";
            SqlParameter[] cmdParams = new SqlParameter[3];
            cmdParams[0] = new SqlParameter("@State", SqlDbType.VarChar);
            cmdParams[1] = new SqlParameter("@UserId", SqlDbType.Int);
            cmdParams[2] = new SqlParameter("@OrderType", SqlDbType.Int);

            if (State.Length > 0)
            {
                sqlStr = sqlStr + " AND State = @State";
                cmdParams[0].Value = State;
            }
            if (UserId > 0)
            {
                sqlStr = sqlStr + " AND UserId = @UserId";
                cmdParams[1].Value = UserId;
            }
            if (OrderType > 0)
            {
                sqlStr = sqlStr + " AND OrderType = @OrderType";
                cmdParams[2].Value = OrderType;
            }
            sqlStr = sqlStr + " ORDER BY SubTime DESC";

            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }
        public static DataTable SeachLikeTitle(string Title, string State, int UserId)
        {
            string sqlStr = "SELECT * FROM ZL_IServer WHERE 1=1 AND Title LIKE '%'+@Title+'%'";
            SqlParameter[] cmdParams = new SqlParameter[3];
            cmdParams[0] = new SqlParameter("@Title", SqlDbType.VarChar);
            cmdParams[1] = new SqlParameter("@State", SqlDbType.VarChar);
            cmdParams[2] = new SqlParameter("@UserId", SqlDbType.Int);
            cmdParams[0].Value = Title;
            if (State.Length > 0)
            {
                sqlStr = sqlStr + " AND State = @State";
                cmdParams[1].Value = State;
            }
            if (UserId > 0)
            {
                sqlStr = sqlStr + " AND UserId = @UserId";
                cmdParams[2].Value = UserId;
            }
            sqlStr = sqlStr + " ORDER BY SubTime DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }
        public static DataTable SeachLikeTitle(string Title, string State, int UserId, string orderall, bool isrun)
        {
            string sqlStr = "SELECT * FROM ZL_IServer WHERE 1=1 AND Title LIKE @Title";
            SqlParameter[] cmdParams = new SqlParameter[3];
            cmdParams[0] = new SqlParameter("@Title", SqlDbType.VarChar);
            cmdParams[1] = new SqlParameter("@State", SqlDbType.VarChar);
            cmdParams[2] = new SqlParameter("@UserId", SqlDbType.Int);
            cmdParams[0].Value = "%" + Title + "%";
            if (State.Length > 0)
            {
                sqlStr = sqlStr + " AND State = @State";
                cmdParams[1].Value = State;
            }
            if (UserId > 0)
            {
                sqlStr = sqlStr + " AND UserId = @UserId";
                cmdParams[2].Value = UserId;
            }

            if (orderall == "OrderType")
            {
                sqlStr = sqlStr + " AND  OrderType>0";
            }
            sqlStr = sqlStr + " ORDER BY SubTime DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }
        public DataTable SeachLikeTitle(string Title, string State, int UserId, string orderall, string type, int orderid)
        {
            string sqlStr = "SELECT * FROM ZL_IServer WHERE 1=1 AND Title LIKE @Title";
            SqlParameter[] cmdParams = new SqlParameter[5];
            cmdParams[0] = new SqlParameter("@Title", SqlDbType.VarChar);
            cmdParams[1] = new SqlParameter("@State", SqlDbType.VarChar);
            cmdParams[2] = new SqlParameter("@UserId", SqlDbType.Int);
            cmdParams[3] = new SqlParameter("@type", SqlDbType.VarChar);
            cmdParams[4] = new SqlParameter("@OrderID", SqlDbType.Int);
            cmdParams[0].Value = "%" + Title + "%";
            if (State.Length > 0)
            {
                sqlStr = sqlStr + " AND State = @State";
                cmdParams[1].Value = State;
            }
            if (UserId > 0)
            {
                sqlStr = sqlStr + " AND UserId = @UserId";
                cmdParams[2].Value = UserId;
            }
            if (type != "")
            {
                sqlStr = sqlStr + " AND Type = @type";
                cmdParams[3].Value = type;
            }
            if (orderid > 0)
            {
                sqlStr = sqlStr + " AND OrderType=@OrderID";
                cmdParams[4].Value = orderid;
            }
            if (orderall == "OrderType")
            {
                sqlStr = sqlStr + " AND  OrderType>0";
            }
            sqlStr = sqlStr + " ORDER BY SubTime DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }

        public static DataTable SeachLikeTitle(string Title, string State, int UserId, int OrderType)
        {
            string sqlStr = "SELECT * FROM ZL_IServer WHERE 1=1 AND Title LIKE '%'+@Title+'%'";
            SqlParameter[] cmdParams = new SqlParameter[4];
            cmdParams[0] = new SqlParameter("@Title", SqlDbType.VarChar);
            cmdParams[1] = new SqlParameter("@State", SqlDbType.VarChar);
            cmdParams[2] = new SqlParameter("@UserId", SqlDbType.Int);
            cmdParams[3] = new SqlParameter("@OrderType", SqlDbType.Int);
            cmdParams[0].Value = Title;
            if (State.Length > 0)
            {
                sqlStr = sqlStr + " AND State = @State";
                cmdParams[1].Value = State;
            }
            if (UserId > 0)
            {
                sqlStr = sqlStr + " AND UserId = @UserId";
                cmdParams[2].Value = UserId;
            }

            if (OrderType > 0)
            {
                sqlStr = sqlStr + " AND OrderType = @OrderType";
                cmdParams[3].Value = OrderType;
            }

            sqlStr = sqlStr + " ORDER BY SubTime DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }
        /// <summary>
        /// 按订单类型查询反馈信息
        /// </summary>
        /// <param name="OrderType">订单类型</param>
        /// <param name="ordername">排序字段</param>
        /// <returns></returns>
        public static DataTable SeachIServerByOrder(int OrderType, string ordername)
        {
            string sqlStr = "SELECT * FROM ZL_IServer WHERE OrderType=" + OrderType + " order by SubTime desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
    }
}
