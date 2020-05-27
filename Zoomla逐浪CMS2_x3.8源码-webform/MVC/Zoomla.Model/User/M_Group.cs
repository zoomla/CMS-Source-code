namespace ZoomLa.Model
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    public class M_Group : M_Base
    {
        private bool m_IsNull;
        public M_Group() { }
        public M_Group(bool flag)
        {
            this.m_IsNull = flag;
        }
        //余额,银币,积分升级价格
        public double UpGradeMoney { get; set; }
        public double UpSIcon { get; set; }
        public double UpPoint { get; set; }

        /// <summary>
        /// 信誉值
        /// </summary>
        public int Credit { get; set; }

        /// <summary>
        /// 返利比率
        /// </summary>
        public float RebateRate { get; set; }

        /// <summary>
        /// 会员组ID
        /// </summary>
        public int GroupID { get; set; }
        /// <summary>
        /// 会员组名
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string OtherName { get; set; }
        /// <summary>
        /// 会员组说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 注册时是否可选
        /// </summary>
        public bool RegSelect { get; set; }
        /// <summary>
        /// 是否默认会员组
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// 每天可发布内容数量
        /// </summary>
        public int CCountPerDay { get; set; }
        /// <summary>
        /// 收藏夹设置
        /// </summary>
        public int FavCount { get; set; }
        /// <summary>
        /// 计费方式
        /// </summary>
        public int ConsumeType { get; set; }
        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsNull
        {
            get { return this.m_IsNull; }
        }
        /// <summary>
        /// 是否为企业用组：0-否 1-是
        /// </summary>
        public int CompanyGroup { get; set; }
        /// <summary>
        ///默认VIP 积分
        /// </summary>
        public int VIPNum { get; set; }
        /// <summary>
        /// 是否为VIP用组：0-否 1-是
        /// </summary>
        public int VIPGroup { get; set; }
        /// <summary>
        /// 招生人员
        /// </summary>
        public string Enroll { get; set; }
        /// <summary>
        /// 父会员组ID
        /// </summary>
        public int ParentGroupID { get; set; }
        /// <summary>
        /// 用于OA，部门签章信息
        /// </summary>
        public string SignImg { get; set; }
        public int OrderID { get; set; }
        public override string PK { get { return "GroupID"; } }
        public override string TbName { get { return "ZL_Group"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"GroupID","Int","4"},
                                  {"GroupName","NVarChar","50"},
                                  {"OtherName","NVarChar","255"},
                                  {"Description","NVarChar","255"},
                                  {"RegSelect","Bit","5"},
                                  {"IsDefault","Bit","5"},
                                  {"CCountPerDay","Int","4"},
                                  {"FavCount","Int","4"},
                                  {"ConsumeType","Int","4"}, 
                                  {"UpGradeMoney","Money","8"},
                                  {"UpSIcon","Money","8"},
                                  {"UpPoint","Money","8"},
                                  {"CompanyGroup","Int","4"},
                                  {"VIPNum","Int","4"},
                                  {"VIPGroup","Int","4"},
                                  {"RebateRate","Float","8"},
                                  {"Credit","Int","4"},
                                  {"Enroll","VarChar","500"},
                                  {"ParentGroupID","Int","4"},
                                  {"SignImg","NVarChar","200"},
                                  {"OrderID","Int","4"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Group model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.GroupID;
            sp[1].Value = model.GroupName;
            sp[2].Value = model.OtherName;
            sp[3].Value = model.Description;
            sp[4].Value = model.RegSelect;
            sp[5].Value = model.IsDefault;
            sp[6].Value = model.CCountPerDay;
            sp[7].Value = model.FavCount;
            sp[8].Value = model.ConsumeType;
            sp[9].Value = model.UpGradeMoney;
            sp[10].Value = model.UpSIcon;
            sp[11].Value = model.UpPoint;
            sp[12].Value = model.CompanyGroup;
            sp[13].Value = model.VIPNum;
            sp[14].Value = model.VIPGroup;
            sp[15].Value = model.RebateRate;
            sp[16].Value = model.Credit;
            sp[17].Value = model.Enroll;
            sp[18].Value = model.ParentGroupID;
            sp[19].Value = model.SignImg;
            sp[20].Value = model.OrderID;
            return sp;
        }
        public M_Group GetModelFromReader(DbDataReader rdr)
        {
            M_Group model = new M_Group();
            model.GroupID = Convert.ToInt32(rdr["GroupID"]);
            model.GroupName = ConverToStr(rdr["GroupName"]);
            model.OtherName = ConverToStr(rdr["OtherName"]);
            model.Description = ConverToStr(rdr["Description"]);
            model.RegSelect = ConverToBool(rdr["RegSelect"]);
            model.IsDefault = ConverToBool(rdr["IsDefault"]);
            model.CCountPerDay = ConvertToInt(rdr["CCountPerDay"]);
            model.FavCount = ConvertToInt(rdr["FavCount"]);
            model.ConsumeType = ConvertToInt(rdr["ConsumeType"]);
            model.UpGradeMoney = ConverToDouble(rdr["UpGradeMoney"]);
            model.UpSIcon = ConverToDouble(rdr["UpSIcon"]);
            model.UpPoint = ConverToDouble(rdr["UpPoint"]);
            model.CompanyGroup = ConvertToInt(rdr["CompanyGroup"]);
            model.VIPNum = ConvertToInt(rdr["VIPNum"]);
            model.VIPGroup = ConvertToInt(rdr["VIPGroup"]);
            model.RebateRate = ConvertToInt(rdr["RebateRate"]);
            //model.Credit = ConvertToInt(rdr["Credit"]);
            model.Enroll = ConverToStr(rdr["Enroll"]);
            model.ParentGroupID = ConvertToInt(rdr["ParentGroupID"]);
            model.SignImg = ConverToStr(rdr["SignImg"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            rdr.Close();
            return model;
        }
    }
}