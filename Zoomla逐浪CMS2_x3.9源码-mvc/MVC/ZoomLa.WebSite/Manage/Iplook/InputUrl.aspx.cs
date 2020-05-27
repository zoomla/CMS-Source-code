using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using ZoomLa.Model;
using ZoomLa.BLL;

namespace ZoomLaCMS.Manage.Iplook
{
    public partial class InputUrl : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li>扩展功能</li><li>IP管理</li><li>Url管理</li>");
        }
        protected void add_Click(object sender, EventArgs e)
        {


        }
    }
}