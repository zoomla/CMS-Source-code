using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLa.Extend
{
    public class M_User_MoneyLog : M_Base
    {
        public int ExpHisID { get; set; }
        public int UserID { get; set; }
        public string detail { get; set; }
        //虚拟币类型
        public int ScoreType { get; set; }
        public double score { get; set; }
        //添加时间
        public DateTime HisTime { get; set; }
        //AdminID
        public int Operator { get; set; }
        public string OperatorIP { get; set; }
        public string Remark { get; set; }
        public int ComeType { get; set; }
        public M_User_MoneyLog()
        {
            this.HisTime = DateTime.Now;
        }
        public override string TbName { get { return "ZL_User_MoneyLog"; } }
        public override string PK { get { return "ExpHisID"; } }
        public override string[,] FieldList() { return M_User_MoneyLog.GetFieldList(); }
        public static string[,] GetFieldList()
        {
            string[,] Tablelist = {
                                  {"ExpHisID","Int","4"},
                                  {"UserID","Int","4"},
                                  {"Operator","Int","4"},
                                  {"detail","NVarChar","1000"},
                                  {"score","Money","16"},
                                  {"HisTime","DateTime","8"},
                                  {"OperatorIP","NVarChar","200"},
                                  {"ScoreType","Int","4"},
                                  {"Remark","NVarChar","200"}
                                 };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_User_MoneyLog model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ExpHisID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.Operator;
            sp[3].Value = model.detail;
            sp[4].Value = model.score;
            sp[5].Value = model.HisTime;
            sp[6].Value = model.OperatorIP;
            sp[7].Value = model.ScoreType;
            sp[8].Value = model.Remark;
            return sp;
        }
        public M_User_MoneyLog GetModelFromReader(SqlDataReader rdr)
        {
            M_User_MoneyLog model = new M_User_MoneyLog();
            model.ExpHisID = Convert.ToInt32(rdr["ExpHisID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.Operator =ConvertToInt(rdr["Operator"]);
            model.detail = ConverToStr(rdr["detail"]);
            model.score =  Convert.ToDouble(rdr["score"]);
            model.HisTime = ConvertToDate(rdr["HisTime"]);
            model.OperatorIP = ConverToStr(rdr["OperatorIP"]);
            model.ScoreType = Convert.ToInt32(rdr["ScoreType"]);
            model.Remark = ConverToStr(rdr["Remark"]);
            rdr.Close();
            return model;
        }
        public override string GetPK()
        {
            return PK;
        }
    }
    public class M_User_Money : M_Base
    {
        public int UserID { get; set; }
        public string Remark { get; set; }
        /// <summary>
        /// 复益账户,M_User_UMoneyLog
        /// </summary>
        public double UMoney { get; set; }
        //public string TbName = "ZL_User_Money";
        //public string PK = "ID";
        public override string TbName { get { return "ZL_User_Money"; } }
        public override string PK { get { return "ID"; } }
        public override string GetPK()
        {
            return "ID";
        }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"UserID","Int","4"},
        		        		{"Remark","NVarChar","500"},
        		        		{"UMoney","Money","16"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_User_Money model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.UserID;
            sp[1].Value = model.Remark;
            sp[2].Value = model.UMoney;
            return sp;
        }
        public M_User_Money GetModelFromReader(SqlDataReader rdr)
        {
            M_User_Money model = new M_User_Money();
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.UMoney = ConverToDouble(rdr["UMoney"]);
            model.Remark = ConverToStr(rdr["Remark"]);
            return model;
        }
    }
    public class B_VMoney
    {
        public enum ExSType { UMoney = 1 };
        private static string ExTbName = "ZL_User_Money";
        /// <summary>
        /// 处理扩展资金(ZL_User_Money)
        /// </summary>
        public static bool Ex_ChangeMoney(ExSType stype, int uid, double money, string detail)
        {
            if (uid < 1 || money == 0) { throw new Exception("用户ID或金额不正确"); }
            //是否存在记录,无则新建,更新为单项字段更新
            M_User_Money model = Ex_SelModel(uid);
            string sql = "UPDATE " + ExTbName + " SET {0}={1} WHERE UserID=" + uid;
            switch (stype)
            {
                case ExSType.UMoney:
                    model.UMoney += money;
                    sql = string.Format(sql, "UMoney", model.UMoney);
                    break;
                default:
                    throw new Exception("虚拟币[" + stype.ToString() + "]类型不存在");
            }
            SqlHelper.ExecuteSql(sql);
            M_User_MoneyLog hisMod = new M_User_MoneyLog();
            hisMod.UserID = uid;
            hisMod.score = money;
            hisMod.ScoreType = (int)stype;
            hisMod.detail = detail;
            Ex_AddLog(stype, hisMod);
            return true;
        }
        /// <summary>
        /// 获取指定用户的模型,如不存在,则插入一条记录后返回
        /// </summary>
        public static M_User_Money Ex_SelModel(int uid)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(ExTbName, " WHERE UserID=" + uid))
            {
                if (reader.Read())
                {
                    return new M_User_Money().GetModelFromReader(reader);
                }
                else
                {
                    M_User_Money model = new M_User_Money();
                    model.UserID = uid;
                    model.UMoney = 0;
                    model.Remark = "";
                    Sql.insert(ExTbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
                    return model;
                }
            }
        }
        public static void Ex_AddLog(ExSType stype, M_User_MoneyLog model)
        {
            model.HisTime = DateTime.Now;
            model.OperatorIP = "127.0.0.1";
            model.Remark = "自动计算";
            Sql.insertID("ZL_User_MoneyLog", model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
    }
}
