using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Design
{
    public partial class Config :CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>动力模块</a></li><li class='active'>动力配置</li>");
                Domain_T.Text = SiteConfig.SiteOption.DesignDomain;
                MBSiteCount_T.Text = SiteConfig.SiteOption.DN_MBSiteCount.ToString();
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            if (!Domain_T.Text.Equals(SiteConfig.SiteOption.DesignDomain))
            {
                B_IDC_DomainList domBll = new B_IDC_DomainList();
                SiteConfig.SiteOption.DesignDomain = Domain_T.Text;
                domBll.ChangeDomain(SiteConfig.SiteOption.DesignDomain);
                RouteHelper.RouteDT = domBll.Sel();
            }
            SiteConfig.SiteOption.DN_MBSiteCount = DataConvert.CLng(MBSiteCount_T.Text);
            SiteConfig.Update();
            function.WriteSuccessMsg("更新成功");
        }
    }
}