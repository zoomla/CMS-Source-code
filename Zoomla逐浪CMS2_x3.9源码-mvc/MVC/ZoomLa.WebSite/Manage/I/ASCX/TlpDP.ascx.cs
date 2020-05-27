using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;


namespace ZoomLaCMS.Manage.I.ASCX
{
    public partial class TlpDP : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = FileSystemObject.GetDTForTemplate();
            TempList_RPT.DataSource = dt;
            TempList_RPT.DataBind();
        }
        public string GetFileInfo()
        {
            if (Eval("type").Equals("1"))
                return "<a href='javascript:;' onclick='Tlp_toggleChild(this," + Eval("id") + ")' ><span class='fa fa-folder'></span> " + Eval("name") + "</a>";
            else if (Eval("type").Equals("2"))//子文件
            {
                return "<a href='javascript:;' style='display:none;' data-pid='" + Eval("pid") + "' onclick=\"Tlp_SetVal(this,'" + Eval("rname") + "')\"><img src='/Images/TreeLineImages/t.gif' /> <span class='fa fa-file'></span> " + Eval("name") + "</a>";
            }
            else
            {
                string icon = Eval("type").Equals("4") ? "fa fa-warning" : "fa fa-file";//跟文件图标
                return "<a href='javascript:;'  onclick=\"Tlp_SetVal(this,'" + Eval("rname") + "')\"> <span class='" + icon + "'></span> " + Eval("name") + "</a>";

            }
        }
    }
}