using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Site;
using ZoomLa.Components;

namespace ZoomLaCMS.Manage.Site
{
    public partial class DomManage :CustomerPageAction
    {
        protected IISHelper iisHelper = new IISHelper();
        protected B_Admin badmin = new B_Admin();
        protected B_User buser = new B_User();
        protected B_IDC_DomainList domListBll = new B_IDC_DomainList();

        protected string clientID, apiPasswd;

        protected void Page_Load(object sender, EventArgs e)
        {
            IdentityAnalogue ia = new IdentityAnalogue();
            ia.CheckEnableSA();
            clientID = StationGroup.newNetClientID;
            apiPasswd = StationGroup.newNetApiPasswd;
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>站群管理</a></li><li class='active'>域名管理</li>");
                DataBind();
            }
        }
        //----EGV
        private void DataBind(string key = "")
        {
            DataTable dt = domListBll.SelWithUser();
            if (!string.IsNullOrEmpty(key))
            {
                dt.DefaultView.RowFilter = "DomName like '%" + key + "%'";
                dt = dt.DefaultView.ToTable();
            }
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void searchBtn_Click(object sender, EventArgs e)
        {
            DataBind(keyWord.Text.Trim());
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBind();
        }
        protected void EGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            EGV.EditIndex = -1;
            EGV.Columns[4].Visible = false;
            DataBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit2":
                    EGV.EditIndex = Convert.ToInt32(e.CommandArgument as string);
                    EGV.Columns[4].Visible = true;
                    break;
                case "Details":
                    Response.Redirect("AddDomain.aspx?Edit=1&ID=" + e.CommandArgument);
                    break;
                case "Renewals"://确定续费
                    EGV.EditIndex = -1;
                    Update(Convert.ToInt32(e.CommandArgument as string));
                    EGV.Columns[4].Visible = false;
                    break;
                default: break;
            }
            DataBind();
        }
        private void Update(int rowNum)
        {
            GridViewRow gr = EGV.Rows[rowNum];
            string url = ((Label)gr.FindControl("lDomain")).Text.Trim();
            string period = Request.Form["periodDP"];
            Renewals(url, period);
        }
        //查到期(Disuse)
        //protected void GetEndDate_Click(object sender, EventArgs e)
        //{
        //    //到期日等查询服务
        //    if (string.IsNullOrEmpty("")) return;
        //    remind.Text = "";
        //    string[] suffix = Request.Form["domNameChk"].Split(',');
        //    remind.Text = "";
        //    foreach (string s in suffix)
        //    {
        //        string url = "" + s;
        //        remind.Text += url;
        //        remind.Text += DomNameHelper.GetEndDate(url) + "<br />";
        //    }
        //}
        //续费业务方法
        private string Renewals(string url, string period)
        {
            // begindate    续费起始日期 yyyy-mm-dd,即当前到期时间
            string begindate = DomNameHelper.GetEndDate(url, "yyyy-MM-dd");
            string checksum = DomNameHelper.MD5("DomainRenew" + clientID + apiPasswd + url + "E" + begindate, 32);//以32位
            List<QueryParam> param = new List<QueryParam>();
            param.Add(new QueryParam("checksum", checksum));
            param.Add(new QueryParam("dn", url));//域名
            param.Add(new QueryParam("enc", "E"));
            param.Add(new QueryParam("client", clientID));
            param.Add(new QueryParam("begindate", begindate));
            param.Add(new QueryParam("period", period));//续费多久，以年为单位
            DomNameHelper _XinNet = new DomNameHelper(ApiType.domain, param);
            return _XinNet.Result;
        }
    }
}