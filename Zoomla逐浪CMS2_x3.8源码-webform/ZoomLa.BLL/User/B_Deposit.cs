using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ZoomLa.SQLDAL;
using ZoomLa.Model.Plat;
using System.Data.SqlClient;
using ZoomLa.Model;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    //用于用户资金记录,二元期权
    public class B_Deposit : ZL_Bll_InterFace<M_Deposit>
    {
        //入账不成功的,不计分红次数
        public string TbName, PK;
        public M_Deposit initMod = new M_Deposit();
        public DataTable dt = null;
        private int Valid = (int)M_Deposit.SType.Valid;
        private int Balance = 1, Present = 2;//高频账户,赠金
        public B_Deposit()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Deposit model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Deposit model)
        {
          return DBCenter.UpdateByID(model,model.ID);
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public M_Deposit SelReturnModel(int ID)
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
        public M_Deposit U_SelModel(int id, int uid)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, " WHERE ID=" + id + " AND UserID=" + uid))
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
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 获取帐户余额
        /// </summary>
        public DataTable U_SelBalance(int uid)
        {
            return SelByMyType(uid,Balance);
        }
        /// <summary>
        /// 获取赠金
        /// </summary>
        public DataTable U_SelPresent(int uid)
        {
            return SelByMyType(uid,Present);
        }
        public DataTable SelBalance()
        {
            string sql = "SELECT * FROM " + TbName + " WHERE MyType=" + Balance + " AND Money>0 AND MyState=" + Valid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        /// <summary>
        /// 带分红次数
        /// </summary>
        private DataTable SelByMyType(int uid, int type)
        {
            //MyStatus=1已入账手工账户的
            string field = "";
            switch (type)
            {
                case 1:
                    field = "(SELECT COUNT(ID) FROM ZL_C_fhmx WHERE BindID=A.ID) AS BonusCount";
                    break;
                case 2:
                    field = "(SELECT COUNT(ID) FROM ZL_C_fhmx WHERE BindID=A.BindID) AS BonusCount";
                    break;
                default:
                    throw new Exception("金额类型错误");
            }
            string sql = "SELECT *," + field + " FROM " + TbName + " A WHERE MyType=" + type + " AND MyState=" + Valid + " AND Money>0 AND UserID=" + uid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        /// <summary>
        /// 按用户聚合信息,只取本金,用于推广佣金处,计算用户的投资额
        /// </summary>
        public DataTable SelSumByUid()
        {
            string sql = "SELECT UserID,MyType,SUM([Money]) AS AMount FROM " + TbName + " WHERE MyType=" + Balance + " AND MyState=" + Valid + " GROUP BY UserID,MyType";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public int SelBonusCount(int id)
        {
            string sql = "SELECT COUNT(ID) AS Count FROM ZL_C_Fhmx WHERE BindID=" + id;
            DataTable dt = SqlHelper.ExecuteTable(sql);
            return Convert.ToInt32(dt.Rows[0]["Count"]);
        }
    }
}
