using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Design;
using ZoomLa.Common;


namespace ZoomLaCMS.Manage.I.ASCX
{
    //后端存的是直接是名称,所以名称不能重复(design)
    public partial class TlpDP : System.Web.UI.UserControl
    {
        B_SPage_Page pageBll = new B_SPage_Page();
        protected void Page_Load(object sender, EventArgs e)
        {
            //IndexTemplate
            DataTable dt = FileSystemObject.GetDTForTemplate();
            DataTable pageDT = pageBll.Sel();//加上design
            if (pageDT.Rows.Count > 0)
            {
                DataRow desdr = dt.NewRow();
                desdr["id"] = 10000;
                desdr["type"] = 5;
                desdr["rname"] = "可视设计";
                desdr["name"] = "可视设计";
                desdr["pid"] = 0;
                dt.Rows.InsertAt(desdr, 1);
                for (int i = 0; i < pageDT.Rows.Count; i++)
                {
                    DataRow child = dt.NewRow();
                    child["type"] = 6;
                    child["path"] = pageDT.Rows[i]["ID"];
                    child["rname"] = "可视设计_" + pageDT.Rows[i]["PageName"];
                    child["name"] = "可视设计_" + pageDT.Rows[i]["PageName"];
                    child["pid"] = 10000;
                    dt.Rows.InsertAt(child, 2);
                    
                }
            }
            TempList_RPT.DataSource = dt;
            TempList_RPT.DataBind();
        }
        public string GetFileInfo()
        {
            string html = "";
            string icon = "";
            int ftype = DataConverter.CLng(Eval("type"));
            //图标
            switch (ftype)
            {
                case 1:
                    icon = "<i class='fa fa-folder'></i>";
                    break;
                case 2:
                case 3:
                    icon = "<i class='fa fa-file'> </i>";
                    break;
                case 4:
                    icon = "<i class='fa fa-warning'> </i>";
                    break;
                case 5:
                    icon = "<i class='fa fa-laptop'> </i>";
                    break;
                case 6:
                    icon = "<i class='fa fa-file-powerpoint-o'> </i>";
                    break;
            }
            switch (ftype)
            {
                case 1://文件夹
                case 5://可视设计目录
                    html += "<a href='javascript:;' onclick='Tlp_toggleChild(this," + Eval("id") + ")' >" + icon + Eval("name") + "</a>";
                    break;
                case 2://子文件夹或文件
                case 6://可视设计文件
                    html += "<a href='javascript:;' style='display:none;' data-pid='" + Eval("pid") + "' onclick=\"Tlp_SetVal(this,'" + Eval("rname") + "')\"><img src='/Images/TreeLineImages/t.gif' /> " + icon + Eval("name") + "</a>";
                    break;
                case 3://根目录html文件
                case 4://未选中提示
                    html += "<a href='javascript:;'  onclick=\"Tlp_SetVal(this,'" + Eval("rname") + "')\">" + icon + Eval("name") + "</a>";
                    break;
            }
            return html;
        }
    }
}