namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using System.Collections.Generic;
    using SQLDAL.SQL;
    public class B_Card
    {
        private string TbName, PK;
        private M_Card initMod = new M_Card();
        public B_Card()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public bool delcards(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return SqlHelper.ExecuteSql("delete from ZL_Card where (Card_ID in (" + ids + "))");
        }
        public M_Card SelReturnModel(int ID)
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
        public DataTable Select_All()
        {
            return Sql.Sel(TbName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateByID(M_Card model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.Card_ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool GetDelete(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public int insert(M_Card model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool GetInsert(M_Card model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model)) > 0;
        }
        public DataTable SelCarByUserIDs(string ids, int type)
        {
            SafeSC.CheckIDSEx(ids);
            string strSql = "select  zl_orderinfo.userid,zl_orderinfo.id,zl_orderinfo.Paymentstatus,zl_card.cardnum,zl_orderinfo.OrderNo,zl_orderinfo.AddTime,zl_orderinfo.Ordersamount,zl_orderinfo.OrderStatus,zl_orderinfo.StateLogistics  from zl_orderinfo Inner join zl_card on ";
            strSql += " zl_orderinfo.userid=zl_card.associateuserid ";
            //   strSql+="  where zl_orderinfo.settle=1 and ";

            strSql += " where zl_orderinfo.userid in(select associateuserid from zl_card where putuserid in (" + ids + ") and associateuserid<>0 and zl_orderinfo.iscount=1 ) ";
            strSql += "order by zl_orderinfo.id desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        public bool GetUpdate(M_Card model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.Card_ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool GetUpdatePas(string Cardnum, string PAS, int userid)
        {
            string sqlStr = "update zl_card set AssociateUserID=@userid Where Cardnum=@Cardnum and CardPwd=@CardPwd";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@userid", SqlDbType.Int),
                new SqlParameter("@Cardnum", SqlDbType.NVarChar, 50),
                new SqlParameter("@CardPwd", SqlDbType.NVarChar, 50)
            };
            sp[0].Value = userid;
            sp[1].Value = Cardnum;
            sp[2].Value = PAS;
            return SqlHelper.ExecuteSql(sqlStr, sp);
        }
        /// <summary>
        /// 启用或停用N个卡
        /// </summary>
        public bool OpenOrStopCards(string Cardnums, int type)
        {
            string sqlStr = "";
            string[] CardnumsArr = Cardnums.Split(',');
            string sql = "";
            SqlParameter[] sp = new SqlParameter[CardnumsArr.Length];

            for (int i = 0; i < CardnumsArr.Length; i++)
            {
                sp[i] = new SqlParameter("Cardnums" + i, CardnumsArr[i]);
                sql += "@" + sp[i].ParameterName + ",";
            }
            sql = sql.TrimEnd(',');
            switch (type)
            {
                case 1:
                    sqlStr = "update zl_card set CardState=1";
                    break;
                case 2:
                    sqlStr = "update zl_card set CardState=2";
                    break;
                default:
                    sqlStr = "update zl_card set CardState=1";
                    break;
            }
            return SqlHelper.ExecuteSql(sqlStr + " Where Card_ID in(" + sql + ")", sp);
        }
        /// <summary>
        /// 启用或停用卡
        /// </summary>
        /// <param name="Cardnum"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool OpenOrStopCard(int Cardnum, int type)
        {
            string sqlStr = "";
            switch (type)
            {
                case 1:
                    sqlStr = "update zl_card set CardState=1 Where Card_ID =@Cardnum";
                    break;
                case 2:
                    sqlStr = "update zl_card set CardState=2 Where Card_ID =@Cardnum";
                    break;
                default:
                    sqlStr = "update zl_card set CardState=1 Where Card_ID =@Cardnum";
                    break;
            }
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@Cardnum", SqlDbType.Int, 4);
            cmdParams[0].Value = Cardnum;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        public bool InsertUpdate(M_Card model)
        {
            if (model.Card_ID > 0)
                GetUpdate(model);
            else
                GetInsert(model);
            return true;
        }
        public M_Card GetSelect(int ID)
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
        public DataTable SelByNum(int num)
        {
            string sql = "SELECT CardNum FROM " + TbName + " WHERE CardNum=" + num;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public M_Card SelectNum(string Cardnum)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@CardNum", SqlDbType.NVarChar, 50);
            cmdParams[0].Value = Cardnum;
            string strsql = "select * from zl_card where cardNum=@CardNum";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strsql, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Card();
                }
            }
        }
        public M_Card SelectUser(int userid)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@userid", SqlDbType.Int);
            cmdParams[0].Value = userid;
            string strsql = "select * from zl_card where AssociateUserID=@userid";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strsql, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Card();
                }
            }
        }
        public bool SelectPas(string Cardnum, string PAS)
        {
            string strSql = "select count(Card_ID) from zl_card where CardNum=@Cardnum and CardPwd=@PAS";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@Cardnum", SqlDbType.NVarChar,50),
                new SqlParameter("@PAS", SqlDbType.NVarChar,50)
            };
            sp[0].Value = Cardnum;
            sp[1].Value = PAS;
            return SqlHelper.ExistsSql(strSql, sp);
        }
        public DataTable SelByAuid(int uid)
        {
            string sql = "SELECT * FROM " + TbName + " WHERE AssociateUserID=" + uid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        /// <summary>
        ///查找代理商
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="Orderby"></param>
        /// <returns></returns>
        public DataTable SelCarByUserID(int id)
        {
            string strSql = "select * from ZL_Card where PutUserID=@ID order by(Card_ID) desc";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = id;

            return SqlHelper.ExecuteTable(CommandType.Text, strSql, cmdParams);
        }
        /// <summary>
        ///查找代理商下所有VIP用户定单
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="Orderby"></param>
        /// <returns></returns>
        public DataTable SelCarByUsers(int id, int type)
        {
            string strSql = "select zl_orderinfo.id,zl_orderinfo.Paymentstatus,zl_card.cardnum,zl_orderinfo.OrderNo,zl_orderinfo.AddTime,zl_orderinfo.Ordersamount,zl_orderinfo.OrderStatus,zl_orderinfo.StateLogistics  from zl_orderinfo Inner join zl_card on ";
            strSql += " zl_orderinfo.userid=zl_card.associateuserid ";
            //   strSql+="  where zl_orderinfo.settle=1 and ";

            strSql += " where zl_orderinfo.userid in(select associateuserid from zl_card where putuserid=@ID and associateuserid<>0 and zl_orderinfo.iscount=1 ) ";
            strSql += "order by zl_orderinfo.id desc";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = id;

            return SqlHelper.ExecuteTable(CommandType.Text, strSql, cmdParams);
        }

        /// <summary>
        /// 排序查询所有记录
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAll()
        {
            return Sql.Sel(TbName);
        }

        /// <summary>
        /// 双卡激活
        /// </summary>
        public bool DoubleActivation(string card1, string card2, int uid)
        {
            if (card1 == card2) { return false; }//不能传入同一个卡号
            M_Card cardMod1 = SelectNum(card1);
            M_Card cardMod2 = SelectNum(card2);
            if (cardMod1.CardNum == string.Empty || cardMod2.CardNum == string.Empty) { return false; }//卡号不存在
            if (cardMod1.ActivateState == 1 || cardMod2.ActivateState == 1) { return false; }//已被使用
            if (cardMod1.CircumscribeTime < DateTime.Now || cardMod2.CircumscribeTime < DateTime.Now) { return false; }//已过期

            string set = "StartTime = @time , ActivateState = 1 , ActivateUserID = " + uid;
            string where = "CardNum = @card1 OR CardNum = @card2";
            List<SqlParameter> sp = new List<SqlParameter>();
            sp.Add(new SqlParameter("time", DateTime.Now.ToString()));
            sp.Add(new SqlParameter("card1", card1));
            sp.Add(new SqlParameter("card2", card2));
            return DBCenter.UpdateSQL(TbName, set, where, sp);
        }
        public DataTable SelActivateRcords(int uid)
        {
            string where = "ActivateState = 1 AND ActivateUserID = " + uid;
            return DBCenter.Sel(TbName, where, "StartTime DESC");
        }
    }
}