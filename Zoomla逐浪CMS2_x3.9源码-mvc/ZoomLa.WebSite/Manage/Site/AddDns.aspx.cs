using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Site;

namespace ZoomLaCMS.Manage.Site
{
    public partial class AddDns : System.Web.UI.Page
    {    //IP与域名可自动识别，加上正则判断吧
        protected M_IDC_DNSTable dnsModel = new M_IDC_DNSTable();
        protected B_IDC_DNSTable dnsBll = new B_IDC_DNSTable();
        protected B_IDC_DNSSubDom subDomBll = new B_IDC_DNSSubDom();
        protected B_User buser = new B_User();
        protected B_Admin badmin = new B_Admin();
        protected string domID;

        protected void Page_Load(object sender, EventArgs e)
        {
            badmin.CheckIsLogin();
            domID = Request.QueryString["id"];
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>站群管理</a></li><li class='active'>DNS管理</li>");
                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    DataBindUser();
                }
                else //修改
                {
                    string id = Request.QueryString["id"];
                    dnsModel = dnsBll.SelByID(id);
                    domNameT.Text = dnsModel.Domain;
                    dom_ipT.Text = dnsModel.IP;
                    userNameT.Text = dnsModel.User_ID.ToString();
                    userID.Value = dnsModel.User_ID.ToString();
                    sub_dom_numT.Text = dnsModel.Max_sub_domain.ToString();
                    url_forwardT.Text = dnsModel.Max_url_forward.ToString();
                    saveBtn.Text = "修改";
                    SubDataBind();
                    tab3.Visible = false;
                    addDiv.Visible = true;
                    subDiv.Visible = true;
                }
            }
        }
        private void DataBindUser(string key = "")
        {
            DataTable dt = new DataTable();
            dt = buser.Sel();

            EGV.DataSource = dt;
            EGV.DataBind();
        }
        private void SubDataBind()
        {
            DataTable dt = new DataTable();
            dt = subDomBll.SelByMainID(domID);
            subDomEGV.DataSource = dt;
            subDomEGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBindUser();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Select":
                    M_UserInfo muser = new M_UserInfo();
                    muser = buser.GetUserByUserID(DataConverter.CLng(e.CommandArgument.ToString()));
                    userID.Value = muser.UserID.ToString();
                    userNameT.Text = muser.UserName;
                    tab3.Visible = false;
                    addDiv.Visible = true;
                    break;
                default:
                    break;
            }
        }
        protected void subDomEGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del2":
                    subDomBll.DelByID(e.CommandArgument.ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('操作成功!!!');", true);
                    SubDataBind();
                    break;
                default:
                    break;
            }
        }
        public string GetGroupName(string GroupID)
        {
            B_Group bgp = new B_Group();
            return bgp.GetByID(DataConverter.CLng(GroupID)).GroupName;
        }
        protected void searchBtn_Click(object sender, EventArgs e)
        {
            DataBindUser(searchText.Text.Trim());
        }
        protected void saveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["id"]))//添加
            {
                TextToModel(dnsModel);
                int id = dnsBll.Insert(dnsModel);//默认插入三条子域名,Ftp与Mail,PoP等子域名
                subDomBll.InitInsert(id, dnsModel.IP, badmin.GetAdminLogin().AdminId);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功!!');", true);
                Response.Redirect("DnsManage.aspx");
            }
            else//修改
            {
                dnsModel = dnsBll.SelByID(domID);
                TextToModel(dnsModel);
                dnsBll.UpdateModel(dnsModel);
                userNameT.Text = "admin";
                userID.Value = dnsModel.User_ID.ToString();
                remindSpan.Visible = true;
                remindSpan.InnerText = "操作DNS成功!!!";
            }
        }
        private void TextToModel(M_IDC_DNSTable model)
        {
            dnsModel.User_ID = DataConverter.CLng(userID.Value);
            dnsModel.Domain = B_IDC_DomainPrice.RemoveW(domNameT.Text);
            dnsModel.IP = dom_ipT.Text.Trim();
            //dnsModel.MX = dom_mailT.Text.Trim();
            dnsModel.Max_sub_domain = DataConverter.CLng(sub_dom_numT.Text.Trim());
            dnsModel.Max_url_forward = DataConverter.CLng(url_forwardT.Text.Trim());
            dnsModel.State = dom_status.Checked ? 1 : 0;
        }
        //保存子域名修改
        protected void subSaveBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < subDomEGV.Rows.Count; i++)
            {
                GridViewRow gr = subDomEGV.Rows[i];
                string id = (gr.Cells[0].FindControl("subDomID") as HiddenField).Value;
                string name = (gr.Cells[0].FindControl("subDomainEdit") as TextBox).Text;
                string data = (gr.Cells[0].FindControl("subDomDataEdit") as TextBox).Text;
                subDomBll.UpdateByID(id, name, data);
            }
            SubDataBind();
        }
        protected void addSubBtn_Click(object sender, EventArgs e)
        {
            subDomBll.Insert(Convert.ToInt32(domID), subDomNameT.Text.Trim(), subDomDataT.Text.Trim(), badmin.GetAdminLogin().AdminId);
            SubDataBind();
        }

        protected void subDomEGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            subDomEGV.PageIndex = e.NewPageIndex;
            SubDataBind();
        }
        protected void subDelBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.Form["subChk"])) Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请先选中需要删除的数据!!');", true);
            else
            {
                subDomBll.BatDelByID(Request.Form["subChk"]);
                SubDataBind();
            }
        }
    }
}