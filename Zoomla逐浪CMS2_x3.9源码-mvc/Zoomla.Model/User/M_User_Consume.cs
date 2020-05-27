using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.User
{
    /// <summary>
    /// 消费分成模型
    /// </summary>
    [Serializable]
    public class M_User_UnitWeek : M_Base
    {
        public int ID { get; set; }
        //用户自身的消费金额,不包含下级消费金额
        public double AMount { get; set; }
        //下级消费金额汇总
        public double ChildAMount { get; set; }

        /// <summary>
        /// 已被提成掉的值汇总,Unit0为下级提成掉0%的
        /// </summary>
        public double Unit0 { get; set; }
        public double Unit10 { get; set; }
        public double Unit20 { get; set; }
        public double Unit30 { get; set; }
        /// <summary>
        /// 下级提成后的业绩汇总,上层计长完提成后,将其归入Unit,
        /// 如ChildUnit0被上一父级提成完后,根据被提成的额度划入父级的Unit10--Unit30
        /// </summary>

        public double ChildUnit0 { get; set; } //下级未被提成的值
        public double ChildUnit10 { get; set; }//下级已提成10%的值
        public double ChildUnit20 { get; set; }//下级已提成20%的值
        public double ChildUnit30 { get; set; }//下级已提成30%的值
        /// <summary>
        /// 我实际能提成到的值,0为无提成
        /// </summary>
        public double RealUnit0 { get; set; }//无提成的值
        public double RealUnit10 { get; set; }//实际可以提成10%的值(根据用户等级,来自Unit0--Unit20)
        public double RealUnit20 { get; set; }//实际可以提成20%的值
        public double RealUnit30 { get; set; }//实际可以提成30%的值
        //额外提成,给予的实际奖金,不需要另作计算
        public double Bonus { get; set; }
        //(disuse,仅用于古文化,每个需要有不同的计算公式,需要开放出来,或外部实现)
        public double RealUnit { get { return RealUnit10 * 0.01 + RealUnit20 * 0.02 + RealUnit30 * 0.03; } }
        //-----End;
        public int UserID { get; set; }
        public int PUserID { get; set; }
        /// <summary>
        /// 自身的GroupID
        /// </summary>
        public int GroupID { get; set; }
        /// <summary>
        /// 下线所有的会员信息统计,如果要全部的会员,则调用最开始进入树的model即可
        /// </summary>
        public List<M_ConsumeUser> childUser = new List<M_ConsumeUser>();
        public List<M_User_UnitWeek> child = new List<M_User_UnitWeek>();
        public string Remind { get; set; }
        //直属下一级的会员ID(似乎无过多用处)
        public string ChildIDS { get; set; }
        //记录创建日期
        public DateTime CDate { get; set; }
        /// <summary>
        /// 0:未分成,1:已分成
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 当前所处的列表层级,是否应该加入层级树功能,生成用户的层级树
        /// </summary>
        public int Level { get; set; }
        public string UserName { get; set; }
        //----不存入数据库
        /// <summary>
        /// 父级点,用于二叉树回朔
        /// </summary>
        public M_User_UnitWeek PModel = null;
        ////全额与百分之十的提成的下线,只需要抽出下一线,通过下一级再见到下一级
        //public List<M_User_Consume> child0 = new List<M_User_Consume>();
        //public List<M_User_Consume> child10 = new List<M_User_Consume>();
        public string UnitWeekTbName = "ZL_User_UnitWeek";
        public override string TbName { get { return "ZL_User_Consume"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"ChildAMount","Money","16"},
        		        		{"RealUnit","Money","16"},
        		        		{"RealUnit0","Money","16"},
        		        		{"RealUnit10","Money","16"},
        		        		{"RealUnit20","Money","16"},
        		        		{"RealUnit30","Money","16"},
        		        		{"UserID","Int","4"},
                                {"PUserID","Int","4"},
                                {"CDate","DateTime","8"},
                                {"Remind","NVarChar","200"},
                                {"ChildIDS","VarChar","8000"},
                                {"State","Int","4"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_User_UnitWeek model = this;
            if (CDate <= DateTime.MinValue) CDate = DateTime.Now;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.ChildAMount;
            sp[2].Value = model.RealUnit;
            sp[3].Value = model.RealUnit0;
            sp[4].Value = model.RealUnit10;
            sp[5].Value = model.RealUnit20;
            sp[6].Value = model.RealUnit30;
            sp[7].Value = model.UserID;
            sp[8].Value = model.PUserID;
            sp[9].Value = model.CDate;
            sp[10].Value = model.Remind;
            sp[11].Value = model.ChildIDS;
            sp[12].Value = model.State;
            return sp;
        }
        public M_User_UnitWeek GetModelFromReader(SqlDataReader rdr)
        {
            M_User_UnitWeek model = new M_User_UnitWeek();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.ChildAMount = ConvertToInt(rdr["ChildAMount"]);
            model.RealUnit0 = ConverToDouble(rdr["RealUnit0"]);
            model.RealUnit10 = ConverToDouble(rdr["RealUnit10"]);
            model.RealUnit20 = ConverToDouble(rdr["RealUnit20"]);
            model.RealUnit30 = ConverToDouble(rdr["RealUnit30"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.PUserID = ConvertToInt(rdr["PUserID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.ChildIDS = ConverToStr(rdr["ChildIDS"]);
            model.State = ConvertToInt(rdr["State"]);
            rdr.Close();
            return model;
        }
    }
   /// <summary>
    /// 消费记录模型
   /// </summary>
    public class M_User_Consume : M_Base
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        //产生分成记录时的推荐人
        public int PUserID { get; set; }
        /// <summary>
        /// 消费类型,1:组升级,2:购物加PV
        /// </summary>
        public int MType { get; set; }
        //可计分成的PV
        public double AMount { get; set; }
        public DateTime CDate { get; set; }
        //计算后的分成比率
        public string UnitPercent { get; set; }
        //计算后的应得分成
        public double UnitAmount { get; set; }
        //分成备注
        public string Remind { get; set; }
        public int State { get; set; }
        //详情,如用于记录 升组后的组
        public string Detail { get; set; }
        public int BindID { get; set; }
        public override string TbName { get { return "ZL_User_Consume"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"UserID","Int","4"},
        		        		{"PUserID","Int","4"},
        		        		{"MType","Int","4"},
        		        		{"AMount","Money","16"},
        		        		{"CDate","DateTime","8"},
        		        		{"UnitPercent","NVarChar","50"},
        		        		{"UnitAmount","Money","16"},
                                {"State","Int","4"},
                                {"Remind","NVarChar","200"},
                                {"Detail","NVarChar","200"},
                                {"BindID","Int","4"}
        };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_User_Consume model)
        {
            if (CDate <= DateTime.MinValue) CDate = DateTime.Now;
            if (UserID == PUserID) PUserID = 0;//不允许让自己成为推荐人
            if (UserID == 0) { throw new Exception("异常,未记录到用户信息"); }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.PUserID;
            sp[3].Value = model.MType;
            sp[4].Value = model.AMount;
            sp[5].Value = model.CDate;
            sp[6].Value = model.UnitPercent;
            sp[7].Value = model.UnitAmount;
            sp[8].Value = model.State;
            sp[9].Value = model.Remind;
            sp[10].Value = model.Detail;
            sp[11].Value = model.BindID;
            return sp;
        }
        public M_User_Consume GetModelFromReader(SqlDataReader rdr)
        {
            M_User_Consume model = new M_User_Consume();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.PUserID = Convert.ToInt32(rdr["PUserID"]);
            model.MType = ConvertToInt(rdr["MType"]);
            model.AMount = Convert.ToDouble(rdr["AMount"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.UnitPercent = ConverToStr(rdr["UnitPercent"]);
            model.UnitAmount = ConverToDouble(rdr["UnitAmount"]);
            model.State = ConvertToInt(rdr["State"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.Detail = ConverToStr(rdr["Detail"]);
            model.BindID = Convert.ToInt32(rdr["BindID"]);
            rdr.Close();
            return model;
        }
    }
    /// <summary>
    /// 仅用于消费二叉树的用户模型
    /// </summary>
    public class M_ConsumeUser
    {
        public int UserID=0;
        public string UserName="";
        public int GroupID=0;
        public M_ConsumeUser() { }
        public M_ConsumeUser(DataRow dr) { LoadFromDR(dr); }
        public void LoadFromDR(DataRow dr) 
        {
            UserID = Convert.ToInt32(dr["UserID"]);
            UserName = dr["UserName"].ToString();
            GroupID = Convert.ToInt32(dr["GroupID"]);
        }
    }
}
