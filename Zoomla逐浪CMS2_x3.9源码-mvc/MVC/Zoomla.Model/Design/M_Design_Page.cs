using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Design
{
    public class M_Design_Page : M_Base
    {
        public enum PageEnum { All = -100, Page = 0, Tlp = 1, Global = 2 };
        public int ID { get; set; }
        public string guid { get; set; }
        private string _page = "";
        /// <summary>
        /// 页面数据
        /// </summary>
        public string page { get { if (string.IsNullOrEmpty(_page)) _page = "{}"; return _page; } set { _page = value; } }
        private string _comp = "";
        /// <summary>
        /// 压缩后的Json
        /// </summary>
        public string comp { get { if (string.IsNullOrEmpty(_comp)) _comp = "[]"; return _comp; } set { _comp = value; } }
        private string _scence = "";
        /// <summary>
        /// 场景,仅用于场景设计
        /// </summary>
        public string scence { get { if (string.IsNullOrEmpty(_scence)) _scence = "[]"; return _scence; } set { _scence = value; } }
        public DateTime CDate { get; set; }
        public DateTime UPDate { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remind { get; set; }
        public string labelArr { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// 引入的JS和CSS资源
        /// </summary>
        public string Resource { get; set; }
        public string Meta { get; set; }
        private string _path = "";
        /// <summary>
        /// 页面路径,给予路由定位,示例:/dir/index
        /// </summary>
        public string Path { get { return _path; } set { _path = ("/" + value.Replace(" ", "").Trim('/')); } }
        /// <summary>
        /// 0:普通页面,1:模板,2:全局组件
        /// </summary>
        public int ZType { get; set; }
        public int TlpID { get; set; }
        public int SiteID { get; set; }
        public int OrderID { get; set; }
        /// <summary>
        /// 预览图,用于预览与分享
        /// </summary>
        public string PreviewImg { get; set; }
        /// <summary>
        /// 缩图,用于场景封面
        /// </summary>
        public string ThumbImg { get; set; }
        /// <summary>
        /// 状态,参考内容
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 评分0-5.0
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// 用来标识场景的显示模式，如不显示广告，多个标识以','分隔
        /// </summary>
        public string Seflag { get; set; }
        //----------------------------临时
        /// <summary>
        /// 该页面是否为一个模板页
        /// </summary>
        public bool IsTemplate { get { return (SiteID == 0); } }
        /// <summary>
        /// 存储不需要保存的全局组件
        /// </summary>
        public List<M_Page_GlobalComp> comp_global = new List<M_Page_GlobalComp>();
        private string _tbname = "ZL_Design_Page";
        public override string TbName { get { return _tbname; } set { _tbname = value; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                {"ID","Int","4"},
                                {"Guid","VarChar","200"},
                                {"page","NText","400000"},
                                {"comp","NText","400000"},
                                {"CDate","DateTime","8"},
                                {"UPDate","DateTime","8"},
                                {"UserID","Int","4"},
                                {"UserName","NVarChar","100"},
                                {"Remind","NVarChar","100"},
                                {"labelArr","NText","400000"},
                                {"Title","NVarChar","50"},
                                {"Meta","NVarChar","4000"},
                                {"Resource","NText","10000"},
                                {"Path","NVarChar","500"},
                                {"ZType","Int","4"},
                                {"TlpID","Int","4"},
                                {"SiteID","Int","4"},
                                {"OrderID","Int","4"},
                                {"scence","NText","400000"},
                                {"PreviewImg","NVarChar","4000"},
                                {"ThumbImg","NVarChar","500"},
                                {"Status","Int","4"},
                                {"Score","Float","8"},
                                {"Seflag","NVarChar","200" }
         };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Design_Page model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            if (model.UPDate <= DateTime.MinValue) { model.UPDate = DateTime.Now; }
            if (string.IsNullOrEmpty(model.Title)) { model.Title = "页面_" + DateTime.Now.ToString("yyyyMMdd"); }
            if (string.IsNullOrEmpty(model.guid)) { model.guid = System.Guid.NewGuid().ToString(); }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.guid;
            sp[2].Value = model.page;
            sp[3].Value = model.comp;
            sp[4].Value = model.CDate;
            sp[5].Value = model.UPDate;
            sp[6].Value = model.UserID;
            sp[7].Value = model.UserName;
            sp[8].Value = model.Remind;
            sp[9].Value = model.labelArr;
            sp[10].Value = model.Title;
            sp[11].Value = model.Meta;
            sp[12].Value = model.Resource;
            sp[13].Value = model.Path;
            sp[14].Value = model.ZType;
            sp[15].Value = model.TlpID;
            sp[16].Value = model.SiteID;
            sp[17].Value = model.OrderID;
            sp[18].Value = model.scence;
            sp[19].Value = model.PreviewImg;
            sp[20].Value = model.ThumbImg;
            sp[21].Value = model.Status;
            sp[22].Value = model.Score;
            sp[23].Value = model.Seflag;
            return sp;
        }
        public M_Design_Page GetModelFromReader(DbDataReader rdr)
        {
            M_Design_Page model = new M_Design_Page();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.guid = ConverToStr(rdr["Guid"]);
            model.page = ConverToStr(rdr["page"]);
            model.comp = ConverToStr(rdr["comp"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.UPDate = ConvertToDate(rdr["UPDate"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.labelArr = ConverToStr(rdr["labelArr"]);
            model.Title = ConverToStr(rdr["Title"]);
            model.Meta = ConverToStr(rdr["Meta"]);
            model.Resource = ConverToStr(rdr["Resource"]);
            model.Path = ConverToStr(rdr["Path"]);
            model.ZType = ConvertToInt(rdr["ZType"]);
            model.TlpID = ConvertToInt(rdr["TlpID"]);
            model.SiteID = ConvertToInt(rdr["SiteID"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            //场景专用
            model.PreviewImg = ConverToStr(rdr["PreviewImg"]);
            model.ThumbImg = ConverToStr(rdr["ThumbImg"]);
            model.scence = ConverToStr(rdr["scence"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.Score = ConverToDouble(rdr["Score"]);
            model.Seflag = ConverToStr(rdr["Seflag"]);
            //站点专用
            rdr.Close();
            return model;
        }
        public M_Design_Page GetModelFromReader(DataRow rdr)
        {
            M_Design_Page model = new M_Design_Page();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.guid = ConverToStr(rdr["Guid"]);
            model.page = ConverToStr(rdr["page"]);
            model.comp = ConverToStr(rdr["comp"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.UPDate = ConvertToDate(rdr["UPDate"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.labelArr = ConverToStr(rdr["labelArr"]);
            model.Title = ConverToStr(rdr["Title"]);
            model.Meta = ConverToStr(rdr["Meta"]);
            model.Resource = ConverToStr(rdr["Resource"]);
            model.Path = ConverToStr(rdr["Path"]);
            model.ZType = ConvertToInt(rdr["ZType"]);
            model.TlpID = ConvertToInt(rdr["TlpID"]);
            model.SiteID = ConvertToInt(rdr["SiteID"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            //场景专用
            model.PreviewImg = ConverToStr(rdr["PreviewImg"]);
            model.ThumbImg = ConverToStr(rdr["ThumbImg"]);
            model.scence = ConverToStr(rdr["scence"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.Score = ConverToDouble(rdr["Score"]);
            model.Seflag = ConverToStr(rdr["Seflag"]);
            //站点专用
            return model;
        }
    }
    //因为服务端不好事件与逻辑处理,交由客户端处理,全局组件按来源划分
    public class M_Page_GlobalComp
    {
        public string path = "";//身份标识,不允许重复
        public string comp = "";
        public M_Page_GlobalComp(DataRow dr)
        {
            path = dr["path"].ToString();
            comp = dr["comp"].ToString();
        }
    }
}
