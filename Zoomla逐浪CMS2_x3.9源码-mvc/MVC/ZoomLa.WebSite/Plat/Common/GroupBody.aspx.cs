using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.BLL.Plat;
using ZoomLa.Model.Plat;

namespace ZoomLaCMS.Plat.Common
{
    public partial class GroupBody : System.Web.UI.Page
    {
        B_Plat_Group gpBll = new B_Plat_Group();
        B_Structure struBll = new B_Structure();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            DataTable dt = gpBll.SelByCompID(B_User_Plat.GetLogin().CompID);
            string attlp = "<label style='margin-bottom:0px;'>@groupname<input type='checkbox' name='plat_group_chk' data-gid='@groupid' data-gname='@groupname' style='margin-left:2px;'></label>";
            string childs_tlp = "<div class='userlist_item group_item' data-gid='@groupid'><div class='pull-left'><i class='fa fa-users' style='color:#03a9f4;margin-right:5px;'></i></div><div class='pull-left item_name'>" + attlp + "</div><div class='pull-right item_add'><a href='javascript:;' title='查看用户'><i class='fa fa-user' style='font-size:16px;'></i> 查看用户</a></div><div style='clear:both;'></div></div>";
            //Group_Lit.Text = GetAllDT(dt, 0);
            foreach (DataRow dr in dt.Rows)
            {
                Group_Lit.Text += TlpReplace(childs_tlp, dr);
            }
        }
        private string TlpReplace(string tlp, DataRow dr)
        {
            return tlp.Replace("@groupid", dr["ID"].ToString()).Replace("@groupname", dr["GroupName"].ToString()).Replace("@pid", dr["Pid"].ToString());
        }
    }
}