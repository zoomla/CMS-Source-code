using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Xml;
using ZoomLa.Components;
using System.IO;

namespace ZoomLaCMS.Manage.Template
{
    public partial class LabelExport : CustomerPageAction
    {
        B_Label bll = new B_Label();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZoomLa.Model.ZLEnum.Auth.label, "LabelExport"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "Config/SiteOption.aspx'>系统设置</a></li><li class='active'><a href='LabelManage.aspx'>标签管理</a></li><li class='active'>标签升级</li>" + Call.GetHelp(21));
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Remind_L.Text = "你没有需要升级的标签";
        }
        protected void GlobalUP_Btn_Click(object sender, EventArgs e)
        {
            string pdir = function.VToP(SiteConfig.SiteOption.TemplateDir + "/配置库/标签/");
            if (!Directory.Exists(pdir))
            {
                Remind_L.Text = "你没有需要升级的标签";
            }
            else
            {
                //Directory.Move(bll.dir, bll.dir + function.GetRandomString(2));
                Directory.Delete(bll.dir, true);
                Directory.Move(pdir, bll.dir);
                Remind_L.Text = "国际版本标签升级完成";
            }
        }
    }
}