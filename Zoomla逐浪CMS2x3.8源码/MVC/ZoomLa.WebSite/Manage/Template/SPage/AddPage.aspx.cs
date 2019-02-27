using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Template.SPage
{
    public partial class AddPage :CustomerPageAction
    {
        B_User buser = new B_User();
        B_SPage_Page pageBll = new B_SPage_Page();
        public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>可视设计</a></li><li><a href='Default.aspx'>页面列表</a></li><li class='active'><a href='"+Request.RawUrl+"'>页面管理</a></li>");
            }
        }
        private void MyBind()
        {
            if (Mid > 0)
            {
                M_SPage_Page pageMod = pageBll.SelReturnModel(Mid);
                PageName_T.Text = pageMod.PageName;
                PageDesc_T.Text = pageMod.PageDesc;
                PageDSLabel_T.Text = pageMod.PageDSLabel;
                PageRes_T.Text = pageMod.PageRes;
                PageBottom_T.Text = pageMod.PageBottom;
                ViewUrl_T.Text = pageMod.ViewUrl;
            }
            else { PageRes_T.Text = "<meta charset=\"utf-8\">\r\n<title>页面标题</title>"; }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            M_SPage_Page pageMod = new M_SPage_Page();
            if (Mid > 0)
            {
                pageMod = pageBll.SelReturnModel(Mid);
            }
            pageMod.PageName = PageName_T.Text;
            pageMod.PageDesc = PageDesc_T.Text;
            pageMod.PageDSLabel = PageDSLabel_T.Text;
            pageMod.PageRes = PageRes_T.Text;
            pageMod.PageBottom = PageBottom_T.Text;
            pageMod.ViewUrl=ViewUrl_T.Text.Trim();
            if (Mid > 0)
            {
                pageBll.UpdateByID(pageMod);
            }
            else
            {
                pageMod.UserID = mu.UserID;
                pageMod.ID = pageBll.Insert(pageMod);
            }
            function.WriteSuccessMsg("添加成功","Default.aspx");
        }
    }
}