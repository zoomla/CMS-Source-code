using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class MIS_Memo_New : System.Web.UI.Page
{
    B_MisInfo bll = new B_MisInfo();
    M_MisInfo model = new M_MisInfo();
    DataTable dt = new DataTable();
    public B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin("/MIS/AddMemo.aspx");
        M_UserInfo info = buser.GetLogin();
        if (!IsPostBack)
        {
            txtWarn.Attributes.Add("onfocus","PopupDiv('div_warn', 'txtWarn', 'warn')");
            txtShare.Attributes.Add("onfocus","PopupDiv('div_share', 'txtShare', 'share')");
            if (Request.QueryString["ID"] != null)
            {
                int ID = DataConverter.CLng(Request.QueryString["ID"]);
                model = bll.SelReturnModel(ID);
                txtMTitle.Text = model.Title;
                txtContent.Text = model.Content;
                txtShare.Text = model.IsShare;
                txtWarn.Text = model.IsWarn;
                ltlTitle.Text = "修改提醒";
            }
            else
            {
                ltlTitle.Text = "新建提醒";
            }
            if (ViewState["PageIndex"] == null)
                ViewState["PageIndex"] = 0;
            //获取用户数据源
            //ViewState["UserSource"] = buser.GetNamesList();
            UserDataBind();
        } 
    }

    protected void UserDataBind()
    {
        int index = Convert.ToInt32(ViewState["PageIndex"]);
        PagedDataSource pdsUsers = new PagedDataSource();
        pdsUsers.DataSource = (ViewState["UserSource"] as List<string>);
        pdsUsers.AllowPaging = true;
        pdsUsers.PageSize = 5;
        pdsUsers.CurrentPageIndex = index;
        rptUserLists.DataSource = pdsUsers;
        rptUserLists.DataBind();
        if (pdsUsers.IsLastPage)
        btnNext.Enabled = false;
    }
    protected void Button_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            int ID = DataConverter.CLng(Request.QueryString["ID"]);
            model = bll.SelReturnModel(ID);
        }
        model.Title = txtMTitle.Text;
        model.IsWarn = txtWarn.Text;
        model.IsShare = txtShare.Text;
        model.Content = txtContent.Text;
        if (Request.QueryString["ID"] != null)
        {
            bll.UpdateByID(model);
            Response.Write("<script>alert('修改成功');location.href='Default.aspx'</script>");
        }
        else
        {
            model.ProID = DataConverter.CLng(Request.QueryString["ProID"]);
            model.MID = DataConverter.CLng(Request.QueryString["MID"]);
            model.Inputer = buser.GetLogin().UserName;
            model.Type = 4;
            model.CreateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(model.Content.ToString()))
            {
                bll.insert(model);
                Response.Write("<script>alert('添加成功'); location.href='Default.aspx'</script>");
            }
            else
            {
                errMsgContent.Style["display"] = "block";
            }
        }
    }
    protected void txtContent_TextChanged(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(txtContent.Text))
            errMsgContent.Style["display"] = "none";            
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = Convert.ToInt32(ViewState["PageIndex"]) + 1;
        UserDataBind();
    }
}