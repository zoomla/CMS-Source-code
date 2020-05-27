using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Model.Site;
using ZoomLa.SQLDAL;


namespace ZoomLaCMS.Manage.Config
{
    public partial class AddRoute : System.Web.UI.Page
    {
        M_IDC_DomainList domMod = null;
        B_IDC_DomainList domBll = new B_IDC_DomainList();
        B_User buser = new B_User();
        private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Mid > 0) { MyBind(); }
                Call.SetBreadCrumb(Master, "<li>工作台</li><li>网站配置</li><li class='active'><a href='RouteConfig.aspx'>域名路由</a></li><li class='active'>路由管理</li>");
            }
        }
        private void MyBind()
        {
            domMod = domBll.SelReturnModel(Mid);
            SType_DP.SelectedValue = domMod.SType.ToString();
            domain_t.Text = domMod.DomName;
            Url_T.Text = domMod.Url;
            UserID_T.Text = buser.SelReturnModel(DataConvert.CLng(domMod.UserID)).UserName;
            Remind_T.Text = domMod.Remind;
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            string domain = domain_t.Text.Replace(" ", "");
            if (string.IsNullOrEmpty(domain_t.Text)) { function.WriteErrMsg("域名不能为空,示例格式:bbs.z01.com"); }
            if (Mid > 0) { domMod = domBll.SelReturnModel(Mid); }
            else
            {
                if (domBll.SelModelByDomain(domain) != null) { function.WriteErrMsg("该域名已存在,不能重复添加路由"); }
                domMod = new M_IDC_DomainList();
            }
            domMod.SType = DataConvert.CLng(SType_DP.SelectedValue);
            domMod.DomName = domain;
            domMod.Url = ConverUrl(Url_T.Text);
            domMod.UserID = DataConvert.CLng(UserID_Hid.Value).ToString();
            if (domMod.Url.StartsWith("~/"))
            {

            }
            else if (domMod.Url.StartsWith("/"))
            {
                domMod.Url = "~" + domMod.Url;
            }

            if (Mid > 0)
            {
                domBll.UpdateByID(domMod);
            }
            else { domBll.Insert(domMod); }
            RouteHelper.RouteDT = domBll.Sel();
            function.WriteSuccessMsg("操作成功", "RouteConfig.aspx");
        }
        public string GetDir(string path)
        {
            return path.Replace("~", "");
        }
        //将重写路径转换为虚拟路径
        public string ConverUrl(string url)
        {
            //不支持/Item/11.aspx路径
            url = url.Replace(" ", "");
            string realurl = url;
            url = url.ToLower();
            if (url.StartsWith("/class_"))
            {
                MatchCollection matchs = Regex.Matches(url, "(?<=(_))[.\\s\\S]*?(?=(/|.aspx))");
                int id = DataConverter.CLng(matchs[0].Value);
                int cpage = DataConverter.CLng(matchs.Count > 1 ? matchs[1].Value : "1");
                if (url.IndexOf("/nodepage") > 0)
                {
                    realurl = "~/NodePage.aspx?ID=" + id + "&CPage=" + cpage;
                }
                else if (url.IndexOf("default") > 0)
                {
                    realurl = "~/ColumnList.aspx?ID=" + id + "&CPage=" + cpage;
                }
                else if (url.IndexOf("nodenews") > 0)
                {
                    realurl = "~/NodeNews.aspx?ID=" + id + "&CPage=" + cpage;
                }
                else if (url.IndexOf("nodehot") > 0)
                {
                    realurl = "~/NodeHot.aspx?ID=" + id + "&CPage=" + cpage;
                }
                else if (url.IndexOf("nodeelite") > 0)
                {
                    realurl = "~/NodeElite.aspx?ID=" + id + "&CPage=" + cpage;
                }
            }
            else if (url.StartsWith("/item/"))
            {
                // /Item/10.aspx   /Item/10_2.aspx   
                url = Regex.Split(url, Regex.Escape("/item/"))[1].Split('.')[0];//10.aspx||10_2.aspx-->10||10_2
                if (url.Contains("_"))
                {
                    realurl = "~/Content.aspx?ID=" + url.Split('_')[0] + "&CPage=" + url.Split('_')[1];
                }
                else
                {
                    realurl = "~/Content.aspx?ID=" + url;
                }
            }
            return realurl;
        }
    }
}