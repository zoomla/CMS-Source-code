using System;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_CommonData:M_Base
    {
        #region 属性定义
        public bool IsNull {get {return GeneralID < 1; } }
        /// <summary>
        /// 内容全局ID
        /// </summary>
        public int GeneralID
        {
            get;
            set;
        }
        /// <summary>
        /// 所属节点ID
        /// </summary>
        public int NodeID
        {
            get;
            set;
        }
        /// <summary>
        /// 所属内容模型ID
        /// </summary>
        public int ModelID
        {
            get;
            set;
        }
        /// <summary>
        /// 相应表的记录ID
        /// </summary>
        public int ItemID
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string TableName
        {
            get;
            set;
        }
        /// <summary>
        /// 内容标题
        /// </summary>
        public string Title
        {
            get;
            set;
        }
        /// <summary>
        /// 录入者
        /// </summary>
        public string Inputer
        {
            get;
            set;
        }
        /// <summary>
        /// 点击数
        /// </summary>
        public int Hits
        {
            get;
            set;
        }
        public string Rtype
        {
            get;
            set;
        }
        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 信息状态 -2为删除，-1为退稿，0为待审核，99为终审通过，其它为自定义
        /// </summary>
        public int Status
        {
            get;
            set;
        }
        /// <summary>
        /// 推荐
        /// </summary>
        public int EliteLevel
        {
            get;
            set;
        }
        /// <summary>
        /// 所属其他节点数组
        /// </summary>
        public string InfoID
        {
            get;
            set;
        }
        /// <summary>
        /// 所属专题ID数组
        /// </summary>
        public string SpecialID
        {
            get;
            set;
        }
        /// <summary>
        /// 是否已生成0=未生成 1=已生成
        /// </summary>
        public int IsCreate
        {
            get;
            set;
        }
        /// <summary>
        /// 生成的静态页面地址(/html/news/26.shtml)
        /// </summary>
        public string HtmlLink
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Template
        {
            get;
            set;
        }
        /// <summary>
        /// 关键字
        /// </summary>
        public string TagKey
        {
            get;
            set;
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpDateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 修改时间类型
        /// </summary>
        public int UpDateType
        {
            get;
            set;
        }
        /// <summary>
        /// 标题颜色
        /// </summary>
        public string Titlecolor
        {
            get;
            set;
        }
        /// <summary>
        /// PDF地址
        /// </summary>
        public string PdfLink
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Cast_File
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Because_Back
        {
            get;
            set;
        }

        /// <summary>
        ///  发文用户ID(原为中标用户ID)
        /// </summary>
        public int SuccessfulUserID
        {
            get;
            set;
        }
        /// <summary>
        /// 改为存IP
        /// </summary>
        private string ComplianceUserList
        {
            get;
            set;
        }
        /// <summary>
        /// 副标题
        /// </summary>
        public string Subtitle
        {
            get;
            set;
        }
        /// <summary>
        /// 拼音标题缩写
        /// </summary>
        public string PYtitle
        {
            get;
            set;
        }
        /// <summary>
        /// 方案数量
        /// </summary>
        public int Pronum
        {
            get;
            set;
        }
        /// <summary>
        /// 项目完成周期
        /// </summary>
        public int ProWeek
        {
            get;
            set;
        }
        /// <summary>
        /// 悬赏方式
        /// </summary>
        public int BidType
        {
            get;
            set;
        }
        /// <summary>
        /// 悬赏价格
        /// </summary>
        public double BidMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 竞标状态(-1-不达标 0-正常 1-竞标 2-请求结算 3-已结算)
        /// </summary>
        public int IsBid
        {
            get;
            set;
        }
        /// <summary>
        /// 黄页默认风格(Disuse)
        /// </summary>
        public int DefaultSkins
        {
            get;
            set;
        }
        /// <summary>
        /// 内容类别
        /// </summary>
        public int OrederClass
        {
            get;
            set;
        }
        /// <summary>
        /// 排序ID
        /// </summary>
        public int OrderID
        {
            get;
            set;
        }
        /// <summary>
        /// 第一级节点ID
        /// </summary>
        public int FirstNodeID
        {
            get;
            set;
        }

        /// <summary>
        /// 标题字体
        /// </summary>
        public string TitleStyle
        {
            get;
            set;
        }

        /// <summary>
        /// 父级树
        /// </summary>
        public string ParentTree
        {
            get;
            set;
        }

        /// <summary>
        /// 首页图片
        /// </summary>
        public string TopImg
        {
            get;
            set;
        }
        /// <summary>
        /// 关联内容IDS
        /// </summary>
        public string RelatedIDS { get; set; }
        /// <summary>
        /// 是否为店铺
        /// </summary>
        public bool IsStore
        {
            get { return TableName.ToLower().StartsWith("zl_store_"); }
        }
        /// <summary>
        /// 跨站踩集标识
        /// </summary>
        public int IsComm { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get { return ComplianceUserList; } set { ComplianceUserList = value; } }
        /// <summary>
        /// 审核时间
        /// </summary>
        public string AuditTime { get; set; }

        ///// <summary>
        ///// JSON
        ///// </summary>
        //public string Addition { get; set; }
        #endregion
        public override string PK { get { return "GeneralID"; } }
        public override string TbName { get { return "ZL_CommonModel"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"GeneralID","Int","4"},
                                  {"OrderID","Int","4"},
                                  {"NodeID","Int","4"},
                                  {"ModelID","Int","4"}, 
                                  {"ItemID","Int","4"},
                                  {"TableName","NVarChar","50"},
                                  {"Title","NVarChar","255"},
                                  {"Inputer","NVarChar","255"}, 
                                  {"Hits","Int","4000"}, 
                                  {"CreateTime","DateTime","8"}, 
                                  {"Status","Int","4"}, 
                                  {"EliteLevel","Int","4"}, 
                                  {"InfoID","NVarChar","255"}, 
                                  {"SpecialID","NVarChar","255"}, 
                                  {"IsCreate","Int","4"}, 
                                  {"HtmlLink","NVarChar","500"}, 
                                  {"Titlecolor","NVarChar","255"}, 
                                  {"Template","NVarChar","255"}, 
                                  {"Tagkey","NVarChar","1000"}, 
                                  {"UpDateTime","DateTime","8"}, 
                                  {"UpDateType","Int","4"}, 
                                  {"cast_file","Int","4"}, 
                                  {"because_back","NVarChar","200"}, 
                                  {"PdfLink","NVarChar","500"}, 
                                  {"Rtype","NVarChar","255"}, 
                                  {"SuccessfulUserID","Int","4"}, 
                                  {"ComplianceUserList","NVarChar","2000"}, 
                                  {"Subtitle","NVarChar","2000"}, 
                                  {"PYtitle","NVarChar","50"}, 
                                  {"BidMoney","Money","8"}, 
                                  {"IsBid","Int","4"}, 
                                  {"Pronum","Int","4"}, 
                                  {"ProWeek","Int","4"}, 
                                  {"BidType","Int","4"}, 
                                  {"DefaultSkins","Int","4"}, 
                                  {"OrederClass","Int","4"}, 
                                  {"FirstNodeID","Int","4"}, 
                                  {"TitleStyle","NVarChar","255"}, 
                                  {"ParentTree","NVarChar","255"}, 
                                  {"TopImg","NVarChar","100"},
                                  {"RelatedIDS","VarChar","3000"},
                                  {"IsComm","Int","4"},
                                  {"AuditTime","NVarChar","100" }
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_CommonData model = this;
            if (model.CreateTime.Year < 1901) model.CreateTime = DateTime.Now;
            if (model.UpDateTime.Year < 1901) model.UpDateTime = DateTime.Now;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.GeneralID;
            sp[1].Value = model.OrderID;
            sp[2].Value = model.NodeID;
            sp[3].Value = model.ModelID;
            sp[4].Value = model.ItemID;
            sp[5].Value = model.TableName;
            sp[6].Value = model.Title;
            sp[7].Value = model.Inputer;
            sp[8].Value = model.Hits;
            sp[9].Value = model.CreateTime;
            sp[10].Value = model.Status;
            sp[11].Value = model.EliteLevel;
            sp[12].Value = model.InfoID;
            sp[13].Value = model.SpecialID;
            sp[14].Value = model.IsCreate;
            sp[15].Value = model.HtmlLink;
            sp[16].Value = model.Titlecolor;
            sp[17].Value = model.Template;
            sp[18].Value = model.TagKey;
            sp[19].Value = model.UpDateTime;
            sp[20].Value = model.UpDateType;
            sp[21].Value = model.Cast_File;
            sp[22].Value = model.Because_Back;
            sp[23].Value = model.PdfLink;
            sp[24].Value = model.Rtype;
            sp[25].Value = model.SuccessfulUserID;
            sp[26].Value = model.ComplianceUserList;
            sp[27].Value = model.Subtitle;
            sp[28].Value = model.PYtitle;
            sp[29].Value = model.BidMoney;
            sp[30].Value = model.IsBid;
            sp[31].Value = model.Pronum;
            sp[32].Value = model.ProWeek;
            sp[33].Value = model.BidType;
            sp[34].Value = model.DefaultSkins;
            sp[35].Value = model.OrederClass;
            sp[36].Value = model.FirstNodeID;
            sp[37].Value = model.TitleStyle;
            sp[38].Value = model.ParentTree;
            sp[39].Value = model.TopImg;
            sp[40].Value = model.RelatedIDS;
            sp[41].Value = model.IsComm;
            sp[42].Value = model.AuditTime;
            return sp;
        }
        public M_CommonData GetModelFromReader(DbDataReader rdr)
        {
            M_CommonData model = new M_CommonData();
            model.GeneralID = Convert.ToInt32(rdr["GeneralID"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            model.NodeID = ConvertToInt(rdr["NodeID"]);
            model.ModelID = ConvertToInt(rdr["ModelID"]);
            model.ItemID = ConvertToInt(rdr["ItemID"]);
            model.TableName = ConverToStr(rdr["TableName"]);
            model.Title = ConverToStr(rdr["Title"]);
            model.Inputer = ConverToStr(rdr["Inputer"]);
            model.Hits = ConvertToInt(rdr["Hits"]);
            model.CreateTime = ConvertToDate(rdr["CreateTime"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.EliteLevel = ConvertToInt(rdr["EliteLevel"]);
            model.InfoID = ConverToStr(rdr["InfoID"]);
            model.SpecialID = ConverToStr(rdr["SpecialID"]);
            model.IsCreate = ConvertToInt(rdr["IsCreate"]);
            model.HtmlLink = ConverToStr(rdr["HtmlLink"]);
            model.Titlecolor = ConverToStr(rdr["Titlecolor"]);
            model.Template = ConverToStr(rdr["Template"]);
            model.TagKey = ConverToStr(rdr["Tagkey"]);
            model.UpDateTime = ConvertToDate(rdr["UpDateTime"]);
            model.UpDateType = ConvertToInt(rdr["UpDateType"]);
            model.Cast_File = ConvertToInt(rdr["cast_file"]);
            model.Because_Back = ConverToStr(rdr["because_back"]);
            model.PdfLink = ConverToStr(rdr["PdfLink"]);
            model.Rtype = ConverToStr(rdr["Rtype"]);
            model.SuccessfulUserID = ConvertToInt(rdr["SuccessfulUserID"]);
            model.ComplianceUserList = ConverToStr(rdr["ComplianceUserList"]);
            model.Subtitle = ConverToStr(rdr["Subtitle"]);
            model.PYtitle = ConverToStr(rdr["PYtitle"]);
            model.BidMoney = ConverToDouble(rdr["BidMoney"]);
            model.IsBid = ConvertToInt(rdr["IsBid"]);
            model.Pronum = ConvertToInt(rdr["Pronum"]);
            model.ProWeek = ConvertToInt(rdr["ProWeek"]);
            model.BidType = ConvertToInt(rdr["BidType"]);
            model.DefaultSkins = ConvertToInt(rdr["DefaultSkins"]);
            model.OrederClass = ConvertToInt(rdr["OrederClass"]);
            model.FirstNodeID = ConvertToInt(rdr["FirstNodeID"]);
            model.TitleStyle = ConverToStr(rdr["TitleStyle"]);
            model.ParentTree = ConverToStr(rdr["ParentTree"]);
            model.TopImg = ConverToStr(rdr["TopImg"]);
            model.RelatedIDS = ConverToStr(rdr["RelatedIDS"]);
            model.IsComm = ConvertToInt(rdr["IsComm"]);
            model.AuditTime = ConverToStr(rdr["AuditTime"]);
            rdr.Close();
            return model;
        }
        public M_CommonData GetModelFromReader(DataRow rdr)
        {
            M_CommonData model = new M_CommonData();
            model.GeneralID = Convert.ToInt32(rdr["GeneralID"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            model.NodeID = ConvertToInt(rdr["NodeID"]);
            model.ModelID = ConvertToInt(rdr["ModelID"]);
            model.ItemID = ConvertToInt(rdr["ItemID"]);
            model.TableName = ConverToStr(rdr["TableName"]);
            model.Title = ConverToStr(rdr["Title"]);
            model.Inputer = ConverToStr(rdr["Inputer"]);
            model.Hits = ConvertToInt(rdr["Hits"]);
            model.CreateTime = ConvertToDate(rdr["CreateTime"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.EliteLevel = ConvertToInt(rdr["EliteLevel"]);
            model.InfoID = ConverToStr(rdr["InfoID"]);
            model.SpecialID = ConverToStr(rdr["SpecialID"]);
            model.IsCreate = ConvertToInt(rdr["IsCreate"]);
            model.HtmlLink = ConverToStr(rdr["HtmlLink"]);
            model.Titlecolor = ConverToStr(rdr["Titlecolor"]);
            model.Template = ConverToStr(rdr["Template"]);
            model.TagKey = ConverToStr(rdr["Tagkey"]);
            model.UpDateTime = ConvertToDate(rdr["UpDateTime"]);
            model.UpDateType = ConvertToInt(rdr["UpDateType"]);
            model.Cast_File = ConvertToInt(rdr["cast_file"]);
            model.Because_Back = ConverToStr(rdr["because_back"]);
            model.PdfLink = ConverToStr(rdr["PdfLink"]);
            model.Rtype = ConverToStr(rdr["Rtype"]);
            model.SuccessfulUserID = ConvertToInt(rdr["SuccessfulUserID"]);
            model.ComplianceUserList = ConverToStr(rdr["ComplianceUserList"]);
            model.Subtitle = ConverToStr(rdr["Subtitle"]);
            model.PYtitle = ConverToStr(rdr["PYtitle"]);
            model.BidMoney = ConverToDouble(rdr["BidMoney"]);
            model.IsBid = ConvertToInt(rdr["IsBid"]);
            model.Pronum = ConvertToInt(rdr["Pronum"]);
            model.ProWeek = ConvertToInt(rdr["ProWeek"]);
            model.BidType = ConvertToInt(rdr["BidType"]);
            model.DefaultSkins = ConvertToInt(rdr["DefaultSkins"]);
            model.OrederClass = ConvertToInt(rdr["OrederClass"]);
            model.FirstNodeID = ConvertToInt(rdr["FirstNodeID"]);
            model.TitleStyle = ConverToStr(rdr["TitleStyle"]);
            model.ParentTree = ConverToStr(rdr["ParentTree"]);
            model.TopImg = ConverToStr(rdr["TopImg"]);
            model.RelatedIDS = ConverToStr(rdr["RelatedIDS"]);
            model.IsComm = ConvertToInt(rdr["IsComm"]);
            model.AuditTime = ConverToStr(rdr["AuditTime"]);
            return model;
        }
    }
}