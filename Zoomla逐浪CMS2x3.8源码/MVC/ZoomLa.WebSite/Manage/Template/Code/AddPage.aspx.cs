using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Other;
using ZoomLa.Common;
using ZoomLa.Model.Other;
using ZoomLa.SQLDAL;
namespace ZoomLaCMS.Manage.Template.Code
{
    public partial class AddPage : CustomerPageAction
    {

        B_Code_Page pageBll = new B_Code_Page();
        HtmlHelper htmlBll = new HtmlHelper();
        M_Code_Page pageMod = null;
        private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        private string PageDir = "/Code/User/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "Config/SiteInfo.aspx'>系统设置</a></li><li><a href=\"PageList.aspx\">页面列表</a></li><li class=\"active\">页面管理</li>");
            }
        }
        private void MyBind()
        {
            if (Mid > 0)
            {
                pageMod = pageBll.SelReturnModel(Mid);
                Url_T.Text = pageMod.PageUrl;
                PageAlias_T.Text = pageMod.PageAlias;
                PageName_T.Text = pageMod.PageName;
                Remind_T.Text = pageMod.Remind;
                function.Script(this, "SetChkVal('models_chk','" + pageMod.Models + "');");
                function.Script(this, "SetRadVal('pagetype_rad','" + pageMod.PageType + "');");
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            if (Mid > 0) { pageMod = pageBll.SelReturnModel(Mid); }
            else { pageMod = new M_Code_Page(); }
            pageMod.PageAlias = PageAlias_T.Text.Trim();
            pageMod.PageName = PageName_T.Text.Trim();
            pageMod.PageType = Request.Form["pagetype_rad"];
            pageMod.Models = Request.Form["models_chk"];
            pageMod.Remind = Remind_T.Text;
            if (string.IsNullOrEmpty(Url_T.Text))
            {
                pageMod.PageUrl = PageDir + pageMod.PageName + ".aspx";
            }
            else { pageMod.PageUrl = Url_T.Text.Trim(); }
            if (pageMod.PageUrl.ToLower().Contains("/manage/")) { function.WriteErrMsg("命名不规范"); }
            //创建页面
            if (Mid > 0)
            {
                pageBll.UpdateByID(pageMod);
            }
            else
            {
                pageBll.Insert(pageMod);
            }
            function.WriteSuccessMsg("操作成功", "PageList.aspx");
        }
        protected void LoadPage_Btn_Click(object sender, EventArgs e)
        {
            string vpath = Url_T.Text.Trim();
            string ppath = Server.MapPath(vpath);
            if (string.IsNullOrEmpty(vpath)) { function.WriteErrMsg("加载路径不能为空"); }
            if (!File.Exists(ppath)) { function.WriteErrMsg(vpath + "文件不存在"); }
            string html = SafeSC.ReadFileStr(vpath);
            PageAlias_T.Text = htmlBll.GetPage(html).Title;
            PageName_T.Text = Path.GetFileNameWithoutExtension(ppath);
        }
    }
}