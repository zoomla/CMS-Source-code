using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;

namespace ZoomLaCMS.Manage.Sentiment
{
    public partial class SenConfig : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Default.aspx'>企业办公</a></li><li><a href='Default.aspx'>舆情监测</a></li><li class='active'>监测维度</li>");
                Sign_T.Text = SiteConfig.SiteOption.SenSign;
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            SiteConfig.SiteOption.SenSign = Sign_T.Text;
            SiteConfig.Update();
            function.WriteSuccessMsg("保存成功");
        }
    }
}