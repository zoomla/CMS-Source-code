using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Other;
using ZoomLa.Common;
using ZoomLa.Model.Other;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Template.Code
{
    public partial class PageCode : CustomerPageAction
    {
        M_Code_Page pageMod = null;
        B_Code_Page pageBll = new B_Code_Page();
        private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Mid < 1) { function.WriteErrMsg("未传入有效ID"); }
                MyBind();
            }
        }
        private void MyBind()
        {
            pageMod = pageBll.SelReturnModel(Mid);
            if (!File.Exists(Server.MapPath(pageMod.PageUrl))) { function.WriteErrMsg(pageMod.PageUrl + "文件不存在"); }
            PageStr_T.Text = SafeSC.ReadFileStr(pageMod.PageUrl);
            CodeStr_T.Text = SafeSC.ReadFileStr(pageMod.PageUrl + ".cs");
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "Config/SiteInfo.aspx'>系统设置</a></li><li><a href=\"PageList.aspx\">页面列表</a></li><li class='active'><a href='" + Request.RawUrl + "'>页面源码</a>页面:" + pageMod.PageAlias + "[" + pageMod.PageUrl + "]</li>");
        }
        protected void SaveCode_Btn_Click(object sender, EventArgs e)
        {
            //更新创建aspx
            pageMod = pageBll.SelReturnModel(Mid);
            string ppath = Server.MapPath(pageMod.PageUrl);
            string pageStr = PageStr_T.Text;
            string codeStr = CodeStr_T.Text;
            File.WriteAllText(ppath, pageStr);
            File.WriteAllText(ppath + ".cs", codeStr);
            function.WriteSuccessMsg("源码更新完成", "PageList.aspx");
        }
    }
}