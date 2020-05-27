namespace ZoomLa.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

    public class M_Node : M_Base
    {
        private bool _isnull = false;
        public M_Node()
        {
            this.Description = "";
            this.Meta_Keywords = "";
            this.Meta_Description = "";
            this.ItemOpenType = false;
            this.PurviewType = true;
            this.CommentType = "1";
            this.HitsOfHot = 0;
            this.ListTemplateFile = "";
            this.IndexTemplate = "";
            this.ContentModel = "";
            this.ListPageHtmlEx = 0;
            this.ContentFileEx = 0;
            this.ContentPageHtmlRule = 0;
            this.LastinfoTemplate = "";
            this.HotinfoTemplate = "";
            this.IndexTemplate = "";
            this.ProposeTemplate = "";
            this.ConsumePoint = 0;
            this.ConsumeType = 0;
            this.ConsumeTime = 0;
            this.ConsumeCount = 0;
            this.Shares = 0;
            this.OpenTypeTrue = "";
            this.ItemOpenTypeTrue = "";
            this.Custom = "";
            this.NodeListUrl = "";
        }
        public M_Node(bool flag) : this() { _isnull = flag; }
        public int NodeID { get; set; }
        public string NodeName { get; set; }
        /// <summary>
        /// 节点类型,0为容器栏目,1为专题栏目,2为单页面,3:外部链接,10:design节点(非正常节点,不抽出)
        /// </summary>
        public int NodeType { get; set; }
        /// <summary>
        /// 模块类型。默认为0-站群(disuse)，1为内容，2为商城商品，3为内容，5-店铺商城,6为互动，7为公开互动
        /// </summary>
        public int NodeListType { get; set; }
        /// <summary>
        /// 节点会员组浏览篇数
        /// </summary>
        public string Viewinglimit { get; set; }
        /// <summary>
        /// 节点提示，不支持HTML
        /// </summary>
        public string Tips { get; set; }
        /// <summary>
        /// 节点目录，只能是英文字母和数字，并且要以字母开头
        /// </summary>
        public string NodeDir { get; set; }
        /// <summary>
        /// 外部链接
        /// </summary>
        public string NodeUrl { get; set; }
        /// <summary>
        /// 栏目列表页
        /// </summary>
        public string NodeListUrl { get; set; }
        /// <summary>
        /// 栏目图片地址
        /// </summary>
        public string NodePic { get; set; }
        /// <summary>
        /// 栏目说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 栏目META关键词
        /// </summary>
        public string Meta_Keywords { get; set; }
        /// <summary>
        /// 针对搜索引擎的说明
        /// </summary>
        public string Meta_Description { get; set; }
        /// <summary>
        /// 打开方式0原窗口
        /// </summary>
        public bool OpenNew { get; set; }
        /// <summary>
        /// 栏目权限。0--开放栏目  1--认证栏目
        /// </summary>
        public bool PurviewType { get; set; }
        /// <summary>
        /// 评论权限0不允许，1:游客,2:登录用户
        /// </summary>
        public string CommentType
        {
            get;
            set;
        }
        /// <summary>
        /// 热点的点击数最小值
        /// </summary>
        public int HitsOfHot { get; set; }
        /// <summary>
        /// 列表页模板
        /// </summary>
        public string ListTemplateFile { get; set; }
        /// <summary>
        /// 栏目首页模板
        /// </summary>
        public string IndexTemplate { get; set; }
        /// <summary>
        /// 最新信息模板
        /// </summary>
        public string LastinfoTemplate { get; set; }
        /// <summary>
        /// 热门信息模板
        /// </summary>
        public string HotinfoTemplate { get; set; }
        /// <summary>
        /// 推荐信息模板
        /// </summary>
        public string ProposeTemplate { get; set; }
        /// <summary>
        /// 选择的内容模型数组
        /// </summary>
        public string ContentModel { get; set; }
        /// <summary>
        /// 节点下项目的打开方式
        /// </summary>
        public bool ItemOpenType { get; set; }
        /// <summary>
        /// 内容页的文件名规则
        /// </summary>
        public int ContentPageHtmlRule { get; set; }
        /// <summary>
        /// 列表首页的文件扩展名
        /// </summary>
        public int ListPageHtmlEx { get; set; }
        /// <summary>
        /// 栏目列表页面扩展名
        /// </summary>
        public int ListPageEx
        {
            get; set;
        }
        /// <summary>
        /// 最新信息页扩展名
        /// </summary>
        public int LastinfoPageEx { get; set; }
        /// <summary>
        /// 热门信息页扩展名
        /// </summary>
        public int HotinfoPageEx { get; set; }
        /// <summary>
        /// 推荐信息扩展名
        /// </summary>
        public int ProposePageEx { get; set; }
        /// <summary>
        /// 内容页文件扩展名
        /// </summary>
        public int ContentFileEx { get; set; }
        /// <summary>
        /// 节点目录生成静态位置0-根目录 1-继承父目录结构
        /// </summary>
        public int HtmlPosition { get; set; }
        /// <summary>
        /// 父节点ID
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 排序序号
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 节点深度
        /// </summary>
        public int Depth { get; set; }
        /// <summary>
        /// 查看内容消费点券数
        /// </summary>
        public int ConsumePoint { get; set; }
        /// <summary>
        /// 重复收费方式
        /// 0-不重复收费
        /// 1-距离上次收费时间多少小时后重新收费
        /// 2-重复阅读内容多少次重新收费
        /// 3-上述两者都满足时重新收费
        /// 4-上述两者任一个满足时就重新收费
        /// 5-每阅读一次就重复收费一次（建议不要使用）
        /// </summary>
        public int ConsumeType { get; set; }
        /// <summary>
        /// 距离上次收费时间多少小时后重新收费
        /// </summary>
        public int ConsumeTime { get; set; }
        /// <summary>
        /// 重复阅读内容多少次重新收费
        /// </summary>
        public int ConsumeCount { get; set; }
        /// <summary>
        /// 会员添加的内容分成比例
        /// </summary>
        public float Shares { get; set; }
        /// <summary>
        /// 自设内容 用{SplitCustom}分隔
        /// </summary>
        public string Custom { get; set; }
        /// <summary>
        /// 是否空节点
        /// </summary>
        public bool IsNull { get { return _isnull; } }
        /// <summary>
        /// 安全保护 1:开启
        /// </summary>
        public int SafeGuard
        {
            get;
            set;
        }
        /// <summary>
        /// 替换的打开方式
        /// </summary>
        public string OpenTypeTrue { get; set; }
        /// <summary>
        /// URL格式 en,cn
        /// </summary>
        public string ItemOpenTypeTrue { get; set; }
        /// <summary>
        /// 是否为design节点
        /// </summary>
        public int NodeBySite { get; set; }
        /// <summary>
        /// 是否简洁模式 1:是
        /// </summary>
        public int Contribute { get; set; }
        /// <summary>
        /// 是否需要审核,存储内容起始状态
        /// </summary>
        /// 
        public int SiteContentAudit { get; set; }
        /// <summary>
        /// 发布内容需要点卡
        /// </summary>
        public int AddPoint { get; set; }
        /// <summary>
        /// 发布内容需要金钱
        /// </summary>
        public double AddMoney { get; set; }
        /// <summary>
        /// 点击时间有效范围
        /// </summary>
        public int ClickTimeout { get; set; }
        /// <summary>
        /// 阅读权限
        /// </summary>
        public string Purview { get; set; }
        /// <summary>
        /// 发布内容添加积分数
        /// </summary>
        public int AddUserExp { get; set; }
        /// <summary>
        /// 下载内容扣除积分数
        /// </summary>
        public int DeducUserExp { get; set; }
        public DateTime CDate { get; set; }
        public DateTime EditDate { get; set; }
        public int CUser { get; set; }
        public string CUName { get; set; }

        //-----------------------
        public int Child { get; set; }
        public string AuditNodeList { get; set; }
        public string SiteConfige { get; set; }
        string _pk = "NodeID";
        public override string PK { get { return _pk; } set { _pk = value; } }
        public override string TbName { get { return "ZL_Node"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                {"NodeID","Int","4"},
                                {"NodeName","NVarChar","50"},
                                {"NodeType","Int","4"},
                                {"NodeDir","NVarChar","50"},
                                {"NodeUrl","NVarChar","255"},
                                {"NodeListUrl","NVarChar","255"},
                                {"Tips","NVarChar","4000"},
                                {"ParentID","Int","4"},
                                {"OrderID","Int","4"},
                                {"Child","Int","4"},
                                {"Depth","Int","4"},
                                {"NodePicUrl","NVarChar","255"},
                                {"Description","NVarChar","4000"},
                                {"Meta_Keywords","NVarChar","4000"},
                                {"Meta_Description","NVarChar","4000"},
                                {"OpenType","Bit","1"},
                                {"PurviewType","Bit","1"},
                                {"CommentType","NVarChar","5"},
                                {"HitsOfHot","Int","4"},
                                {"IndexTemplate","NVarChar","255"},
                                {"ListTemplateFile","NVarChar","255"},
                                {"LastinfoTemplate","NVarChar","255"},
                                {"HotinfoTemplate","NVarChar","255"},
                                {"ProposeTemplate","NVarChar","255"},
                                {"ContentModel","NVarChar","255"},
                                {"ItemOpenType","Bit","1"},
                                {"ContentHtmlRule","Int","4"},
                                {"ListPageHtmlEx","Int","4"},
                                {"ContentFileEx","Int","4"},
                                {"HtmlPosition","Int","4"},
                                {"NodeListType","Int","4"},
                                {"ConsumePoint","Int","4"},
                                {"ConsumeType","Int","4"},
                                {"ConsumeTime","Int","4"},
                                {"ConsumeCount","Int","4"},
                                {"Shares","Float","8"},
                                {"Custom","NText","5000"},
                                {"SafeGuard","Int","4"},
                                {"ItemOpenTypeTrue","NVarChar","50"},
                                {"OpenTypeTrue","NVarChar","50"},
                                {"NodeBySite","Int","4"},
                                {"SiteConfige","NText","16"},
                                {"Contribute","Int","4"},
                                {"SiteContentAudit","Int","4"},
                                {"AuditNodeList","NVarChar","1000"},
                                {"AddPoint","Int","4"},
                                {"AddMoney","Money","8"},
                                {"ClickTimeout","Int","4"},
                                {"Purview","NVarChar","1000"},
                                {"AddUserExp","Int","4"},
                                {"DeducUserExp","Int","4"},
                                {"ListPageEx","Int","4"},
                                {"LastinfoPageEx","Int","4"},
                                {"HotinfoPageEx","Int","4"},
                                {"ProposePageEx","Int","4"},
                                {"Viewinglimit","NVarChar","100"},
                                {"CDate","DateTime","8"},
                                {"EditDate","DateTime","8" },
                                {"CUser","Int","4"},
                                {"CUName","NVarChar","500"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Node model = this;
            SqlParameter[] sp = GetSP();
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            model.EditDate = DateTime.Now;
            sp[0].Value = model.NodeID;
            sp[1].Value = model.NodeName;
            sp[2].Value = model.NodeType;
            sp[3].Value = model.NodeDir;
            sp[4].Value = model.NodeUrl;
            sp[5].Value = model.NodeListUrl;
            sp[6].Value = model.Tips;
            sp[7].Value = model.ParentID;
            sp[8].Value = model.OrderID;
            sp[9].Value = model.Child;
            sp[10].Value = model.Depth;
            sp[11].Value = model.NodePic;
            sp[12].Value = model.Description;
            sp[13].Value = model.Meta_Keywords;
            sp[14].Value = model.Meta_Description;
            sp[15].Value = model.OpenNew;
            sp[16].Value = model.PurviewType;
            sp[17].Value = model.CommentType;
            sp[18].Value = model.HitsOfHot;
            sp[19].Value = model.IndexTemplate;
            sp[20].Value = model.ListTemplateFile;
            sp[21].Value = model.LastinfoTemplate;
            sp[22].Value = model.HotinfoTemplate;
            sp[23].Value = model.ProposeTemplate;
            sp[24].Value = model.ContentModel;
            sp[25].Value = model.ItemOpenType;
            sp[26].Value = model.ContentPageHtmlRule;
            sp[27].Value = model.ListPageHtmlEx;
            sp[28].Value = model.ContentFileEx;
            sp[29].Value = model.HtmlPosition;
            sp[30].Value = model.NodeListType;
            sp[31].Value = model.ConsumePoint;
            sp[32].Value = model.ConsumeType;
            sp[33].Value = model.ConsumeTime;
            sp[34].Value = model.ConsumeCount;
            sp[35].Value = model.Shares;
            sp[36].Value = model.Custom;
            sp[37].Value = model.SafeGuard;
            sp[38].Value = model.ItemOpenTypeTrue;
            sp[39].Value = model.OpenTypeTrue;
            sp[40].Value = model.NodeBySite;
            sp[41].Value = model.SiteConfige;
            sp[42].Value = model.Contribute;
            sp[43].Value = model.SiteContentAudit;
            sp[44].Value = model.AuditNodeList;
            sp[45].Value = model.AddPoint;
            sp[46].Value = model.AddMoney;
            sp[47].Value = model.ClickTimeout;
            sp[48].Value = model.Purview;
            sp[49].Value = model.AddUserExp;
            sp[50].Value = model.DeducUserExp;
            sp[51].Value = model.ListPageEx;
            sp[52].Value = model.LastinfoPageEx;
            sp[53].Value = model.HotinfoPageEx;
            sp[54].Value = model.ProposePageEx;
            sp[55].Value = model.Viewinglimit;
            sp[56].Value = model.CDate;
            sp[57].Value = model.EditDate;
            sp[58].Value = model.CUser;
            sp[59].Value = model.CUName;
            return sp;
        }
        public M_Node GetModelFromReader(DbDataReader rdr)
        {
            M_Node model = new M_Node();
            try
            {
                model.NodeID = Convert.ToInt32(rdr["NodeID"]);
                model.NodeName = ConverToStr(rdr["NodeName"]);
                model.NodeType = ConvertToInt(rdr["NodeType"]);
                model.Tips = ConverToStr(rdr["Tips"]);
                model.NodeDir = ConverToStr(rdr["NodeDir"]);
                model.NodePic = ConverToStr(rdr["NodePicUrl"]);
                model.NodeUrl = ConverToStr(rdr["NodeUrl"]);
                model.NodeListUrl = ConverToStr(rdr["NodeListUrl"]);
                model.Description = ConverToStr(rdr["Description"]);
                model.Meta_Keywords = ConverToStr(rdr["Meta_Keywords"]);
                model.Meta_Description = ConverToStr(rdr["Meta_Description"]);
                model.OpenNew = ConverToBool(rdr["OpenType"]);
                model.PurviewType = ConverToBool(rdr["PurviewType"]);
                model.CommentType = ConverToStr(rdr["CommentType"]);
                model.HitsOfHot = ConvertToInt(rdr["HitsOfHot"]);
                model.ListTemplateFile = ConverToStr(rdr["ListTemplateFile"]);
                model.IndexTemplate = ConverToStr(rdr["IndexTemplate"]);
                model.LastinfoTemplate = ConverToStr(rdr["LastinfoTemplate"]);
                model.HotinfoTemplate = ConverToStr(rdr["HotinfoTemplate"]);
                model.ProposeTemplate = ConverToStr(rdr["ProposeTemplate"]);
                model.ContentModel = ConverToStr(rdr["ContentModel"]);
                model.ItemOpenType = ConverToBool(rdr["ItemOpenType"]);
                model.ContentPageHtmlRule = ConvertToInt(rdr["ContentHtmlRule"]);
                model.ListPageHtmlEx = ConvertToInt(rdr["ListPageHtmlEx"]);
                model.ListPageEx = ConvertToInt(rdr["ListPageEx"]);
                model.LastinfoPageEx = ConvertToInt(rdr["LastinfoPageEx"]);
                model.HotinfoPageEx = ConvertToInt(rdr["HotinfoPageEx"]);
                model.ProposePageEx = ConvertToInt(rdr["ProposePageEx"]);
                model.ContentFileEx = ConvertToInt(rdr["ContentFileEx"]);
                model.HtmlPosition = ConvertToInt(rdr["HtmlPosition"]);
                model.ParentID = ConvertToInt(rdr["ParentID"]);
                model.NodeListType = ConvertToInt(rdr["NodeListType"]);
                model.OrderID = ConvertToInt(rdr["OrderID"]);
                model.Child = ConvertToInt(rdr["Child"]);
                model.Depth = ConvertToInt(rdr["Depth"]);
                model.ConsumePoint = ConvertToInt(rdr["ConsumePoint"]);
                model.ConsumeType = ConvertToInt(rdr["ConsumeType"]);
                model.ConsumeTime = ConvertToInt(rdr["ConsumeTime"]);
                model.ConsumeCount = ConvertToInt(rdr["ConsumeCount"]);
                model.Shares = (float)ConverToDouble(rdr["Shares"]);
                model.Custom = ConverToStr(rdr["Custom"]);
                model.SafeGuard = ConvertToInt(rdr["SafeGuard"]);
                model.OpenTypeTrue = ConverToStr(rdr["OpenTypeTrue"]);
                model.ItemOpenTypeTrue = ConverToStr(rdr["ItemOpenTypeTrue"]);
                model.NodeBySite = ConvertToInt(rdr["NodeBySite"]);
                model.SiteConfige = ConverToStr(rdr["SiteConfige"]);
                model.Contribute = ConvertToInt(rdr["Contribute"]);
                model.SiteContentAudit = ConvertToInt(rdr["SiteContentAudit"]);
                model.AuditNodeList = ConverToStr(rdr["AuditNodeList"]);
                model.AddMoney = ConverToDouble(rdr["AddMoney"]);
                model.AddPoint = ConvertToInt(rdr["AddPoint"]);
                model.ClickTimeout = ConvertToInt(rdr["ClickTimeout"]);
                model.Purview = ConverToStr(rdr["Purview"]);
                model.AddUserExp = ConvertToInt(rdr["AddUserExp"]);
                model.DeducUserExp = ConvertToInt(rdr["DeducUserExp"]);
                model.Viewinglimit = ConverToStr(rdr["Viewinglimit"]);
                model.CDate = ConvertToDate(rdr["CDate"]);
                model.EditDate = ConvertToDate(rdr["EditDate"]);
                model.CUser = ConvertToInt(rdr["CUser"]);
                model.CUName = ConverToStr(rdr["CUName"]);
            }
            catch (Exception ex) { throw new Exception("来源:M_Node,原因:" + ex.Message); }
            rdr.Close();
            return model;
        }
        public M_Node GetModelFromReader(DataRow rdr)
        {
            M_Node model = new M_Node();
            try
            {
                model.NodeID = Convert.ToInt32(rdr["NodeID"]);
                model.NodeName = ConverToStr(rdr["NodeName"]);
                model.NodeType = ConvertToInt(rdr["NodeType"]);
                model.Tips = ConverToStr(rdr["Tips"]);
                model.NodeDir = ConverToStr(rdr["NodeDir"]);
                model.NodePic = ConverToStr(rdr["NodePicUrl"]);
                model.NodeUrl = ConverToStr(rdr["NodeUrl"]);
                model.NodeListUrl = ConverToStr(rdr["NodeListUrl"]);
                model.Description = ConverToStr(rdr["Description"]);
                model.Meta_Keywords = ConverToStr(rdr["Meta_Keywords"]);
                model.Meta_Description = ConverToStr(rdr["Meta_Description"]);
                model.OpenNew = ConverToBool(rdr["OpenType"]);
                model.PurviewType = ConverToBool(rdr["PurviewType"]);
                model.CommentType = ConverToStr(rdr["CommentType"]);
                model.HitsOfHot = ConvertToInt(rdr["HitsOfHot"]);
                model.ListTemplateFile = ConverToStr(rdr["ListTemplateFile"]);
                model.IndexTemplate = ConverToStr(rdr["IndexTemplate"]);
                model.LastinfoTemplate = ConverToStr(rdr["LastinfoTemplate"]);
                model.HotinfoTemplate = ConverToStr(rdr["HotinfoTemplate"]);
                model.ProposeTemplate = ConverToStr(rdr["ProposeTemplate"]);
                model.ContentModel = ConverToStr(rdr["ContentModel"]);
                model.ItemOpenType = ConverToBool(rdr["ItemOpenType"]);
                model.ContentPageHtmlRule = ConvertToInt(rdr["ContentHtmlRule"]);
                model.ListPageHtmlEx = ConvertToInt(rdr["ListPageHtmlEx"]);
                model.ListPageEx = ConvertToInt(rdr["ListPageEx"]);
                model.LastinfoPageEx = ConvertToInt(rdr["LastinfoPageEx"]);
                model.HotinfoPageEx = ConvertToInt(rdr["HotinfoPageEx"]);
                model.ProposePageEx = ConvertToInt(rdr["ProposePageEx"]);
                model.ContentFileEx = ConvertToInt(rdr["ContentFileEx"]);
                model.HtmlPosition = ConvertToInt(rdr["HtmlPosition"]);
                model.ParentID = ConvertToInt(rdr["ParentID"]);
                model.NodeListType = ConvertToInt(rdr["NodeListType"]);
                model.OrderID = ConvertToInt(rdr["OrderID"]);
                model.Child = ConvertToInt(rdr["Child"]);
                model.Depth = ConvertToInt(rdr["Depth"]);
                model.ConsumePoint = ConvertToInt(rdr["ConsumePoint"]);
                model.ConsumeType = ConvertToInt(rdr["ConsumeType"]);
                model.ConsumeTime = ConvertToInt(rdr["ConsumeTime"]);
                model.ConsumeCount = ConvertToInt(rdr["ConsumeCount"]);
                model.Shares = (float)ConverToDouble(rdr["Shares"]);
                model.Custom = ConverToStr(rdr["Custom"]);
                model.SafeGuard = ConvertToInt(rdr["SafeGuard"]);
                model.OpenTypeTrue = ConverToStr(rdr["OpenTypeTrue"]);
                model.ItemOpenTypeTrue = ConverToStr(rdr["ItemOpenTypeTrue"]);
                model.NodeBySite = ConvertToInt(rdr["NodeBySite"]);
                model.SiteConfige = ConverToStr(rdr["SiteConfige"]);
                model.Contribute = ConvertToInt(rdr["Contribute"]);
                model.SiteContentAudit = ConvertToInt(rdr["SiteContentAudit"]);
                model.AuditNodeList = ConverToStr(rdr["AuditNodeList"]);
                model.AddMoney = ConverToDouble(rdr["AddMoney"]);
                model.AddPoint = ConvertToInt(rdr["AddPoint"]);
                model.ClickTimeout = ConvertToInt(rdr["ClickTimeout"]);
                model.Purview = ConverToStr(rdr["Purview"]);
                model.AddUserExp = ConvertToInt(rdr["AddUserExp"]);
                model.DeducUserExp = ConvertToInt(rdr["DeducUserExp"]);
                model.Viewinglimit = ConverToStr(rdr["Viewinglimit"]);
                model.CDate = ConvertToDate(rdr["CDate"]);
                model.EditDate = ConvertToDate(rdr["EditDate"]);
                model.CUser = ConvertToInt(rdr["CUser"]);
                model.CUName = ConverToStr(rdr["CUName"]);
            }
            catch (Exception ex) { throw new Exception("来源:M_Node,原因:" + ex.Message); }
            return model;
        }
    }
}