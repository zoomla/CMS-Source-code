using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;
using ZoomLa.Components;
using ZoomLa.BLL.Helper;
using Winista.Text.HtmlParser.Util;
using Winista.Text.HtmlParser.Filters;
using Newtonsoft.Json;
using System.Xml;

namespace ZoomLaCMS.Manage.Content.Collect
{
    public partial class CollectionInfoPage : System.Web.UI.Page
    {
        B_CollectionItem bc = new B_CollectionItem();
        RegexHelper regHelper = new RegexHelper();
        HtmlHelper htmlHelper = new HtmlHelper();
        private string Url
        {
            get { return ViewState["url"] as string; }
            set { ViewState["url"] = value; }
        }
        private int ItemID
        {
            get { return DataConverter.CLng(Request.QueryString["ItemID"]); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.HideBread(Master);
            }
        }
        public void MyBind()
        {
            M_CollectionItem mc = bc.GetSelect(ItemID);
            Link_DP.DataSource = mc.LinkList.Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Link_DP.DataBind();
            Url = StrHelper.UrlDeal(Link_DP.Items[0].Value);
            txtHtml.Text = htmlHelper.GetHtmlFromSite(Url);
            CurLink_L.Text = "获取成功：" + Url;
            //----------------
            try
            {
                if (!string.IsNullOrWhiteSpace(mc.InfoPageSettings))
                {
                    FilterModel model = JsonConvert.DeserializeObject<FilterModel>(mc.InfoPageSettings);
                    EType_T.Text = model.EType;
                    ID_T.Text = model.ID;
                    Class_T.Text = model.CSS;
                    Script_Chk.Checked = model.AllowScript;
                }
            }
            catch { }
        }
        protected void Link_DP_SelectedIndexChanged(object sender, EventArgs e)
        {
            Url =StrHelper.UrlDeal(Link_DP.SelectedValue);
            txtHtml.Text = htmlHelper.GetHtmlFromSite(Url);
            CurLink_L.Text = "获取成功：" + Url;
        }
        protected void Test_Btn_Click(object sender, EventArgs e)
        {
            string html = htmlHelper.GetHtmlFromSite(Url);
            FilterModel model = GetFModel();
            txtHtml.Text = htmlHelper.GetByFilter(html, model);
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            FilterModel model = GetFModel();
            Json_Hid.Value = JsonConvert.SerializeObject(model);
            function.Script(this, "SaveConfig();");
        }
        public FilterModel GetFModel()
        {
            FilterModel model = new FilterModel()
            {
                EType = EType_T.Text,
                ID = ID_T.Text,
                CSS = Class_T.Text,
                Start = Server.HtmlEncode(Start_T.Text),
                End = Server.HtmlEncode(End_T.Text),
                AllowScript = Script_Chk.Checked
            };
            return model;
        }
    }
}