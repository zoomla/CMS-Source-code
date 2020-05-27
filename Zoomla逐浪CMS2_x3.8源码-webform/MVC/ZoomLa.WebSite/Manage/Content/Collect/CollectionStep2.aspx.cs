using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;
using ZoomLa.BLL.Helper;
using ZoomLa.Model.Collect;
using Newtonsoft.Json;

namespace ZoomLaCMS.Manage.Content.Collect
{
    public partial class CollectionStep2 : CustomerPageAction
    {
        B_CollectionItem bc = new B_CollectionItem();
        HtmlHelper htmlHelper = new HtmlHelper();
        M_Collect_ListFilter lfMod = new M_Collect_ListFilter(); 
        protected string title = "添加设置采集第二步";

        public int ItemID
        {
            get { return DataConverter.CLng(Request.QueryString["ItemID"]); }
        }
        private string Url
        {
            get { return ViewState["url"].ToString(); }
            set { ViewState["url"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_CollectionItem mc = bc.GetSelect(ItemID);
                Url = mc.CollUrl;
                lblLink.Text = "<a href='" + mc.CollUrl + "'  target=\"_blank\" class='btn btn-info' style='color:white;'>查看原页面</a>";
                SourceHtml_Hid.Value = htmlHelper.GetHtmlFromSite(Url);
                if (!string.IsNullOrEmpty(mc.ListSettings))
                {
                    lfMod = JsonConvert.DeserializeObject<M_Collect_ListFilter>(mc.ListSettings);
                    ListStart_T.Text = lfMod.ListStart;
                    ListEnd_T.Text = lfMod.ListEnd;
                    CharContain_T.Text = lfMod.CharContain;
                    CharRegex_T.Text = lfMod.CharRegex;
                    FillStart_T.Text = lfMod.FillStart;
                    FillEnd_T.Text = lfMod.FillEnd;
                }
                if (!string.IsNullOrEmpty(mc.LinkList)) { txtHtml.Text = mc.LinkList; }
                else { txtHtml.Text = SourceHtml_Hid.Value; }
                Call.SetBreadCrumb(Master, "<li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='CollectionManage.aspx'>信息采集</a></li><li class='active'>当前:" + mc.ItemName + "</li>");
            }
        }
        //筛选列表
        protected void FilterA_Btn_Click(object sender, EventArgs e)
        {
           lfMod=FillListFilter();
            txtHtml.Text = FilterList(SourceHtml_Hid.Value, lfMod);
        }
        private string FilterList(string html, M_Collect_ListFilter model)
        {
            StringBuilder sb = new StringBuilder();
            string listFilter = model.ListStart + "[\\s\\S]*?" + model.ListEnd;
            MatchCollection matches = new Regex(listFilter).Matches(html);
            Match TitleMatch = Regex.Match(html, listFilter, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            foreach (Match m in matches)
            {
                string url = (m.Value.Replace(model.ListStart, "").Replace(model.ListEnd, ""));
                if (url.ToLower().Contains("http") || url.ToLower().Contains("https")) { }
                else { url = model.FillStart + url + model.FillEnd; }//自动用根路径,或用户预设好的值填充前面
                sb.AppendLine(url);
            }
            if (string.IsNullOrEmpty(sb.ToString())) {return ""; }
            return htmlHelper.GetAllLink("<html><head></head><body>" + sb.ToString() + "</body></html>", model);
        }
        protected void Next_Btn_Click(object sender, EventArgs e)
        {
            M_CollectionItem mc = bc.GetSelect(ItemID);
            mc.ListSettings = JsonConvert.SerializeObject(FillListFilter());
            mc.LinkList = txtHtml.Text;
            bc.GetUpdate(mc);
            Response.Redirect("CollectionStep3.aspx?ItemId=" + ItemID);
        }
        private M_Collect_ListFilter FillListFilter()
        {
            lfMod.ListStart = ListStart_T.Text.Trim();
            lfMod.ListEnd = ListEnd_T.Text.Trim();
            //lfMod.CharStart = CharStart_T.Text.Trim();
            //lfMod.CharEnd = CharEnd_T.Text.Trim();
            lfMod.CharContain = CharContain_T.Text.Trim();
            lfMod.CharRegex = CharRegex_T.Text.Trim();
            lfMod.FillStart = FillStart_T.Text.Trim();
            lfMod.FillEnd = FillEnd_T.Text.Trim();
            return lfMod;
        }
    }
}