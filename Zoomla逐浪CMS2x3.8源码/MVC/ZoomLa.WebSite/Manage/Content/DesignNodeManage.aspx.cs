using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Content
{
    public partial class DesignNodeManage : CustomerPageAction
    {

        B_Node nodeBll = new B_Node();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind(string skey = "")
        {
            DataTable dt = nodeBll.Sel(0, skey);
            dt.DefaultView.RowFilter = "NodeBySite>0";
            RPT.DataSource = dt.DefaultView.ToTable();
            RPT.DataBind();
        }

        protected void RPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
        public string GetUser()
        {
            int uid = DataConverter.CLng(Eval("CUser", ""));
            var mu = buser.SelReturnModel(uid);
            if (mu.IsNull) { return ""; }
            else { return "<a href='javascript:;' title='查看用户' onclick=\"opendiag('查看会员','../User/Userinfo.aspx?id=" + mu.UserID + "');\">" + mu.UserName + "</a>"; }
        }

        protected void souchok_Click(object sender, EventArgs e)
        {
            string skey = souchkey.Text.Trim(' ');
            MyBind(skey);
            if (!string.IsNullOrEmpty(skey))
            {
                function.Script(this, "showsel();");
            }
        }
    }
}