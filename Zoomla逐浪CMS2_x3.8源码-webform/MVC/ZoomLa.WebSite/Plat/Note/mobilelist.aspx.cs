using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.BLL.Project;
using System.Data;
using ZoomLa.Model.Project;

namespace ZoomLaCMS.Plat.Note
{
    public partial class mobilelist : System.Web.UI.Page
    {
        B_Pro_Step stepBll = new B_Pro_Step();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            M_UserInfo mu = buser.GetLogin();
            //DataTable dt= proBll.SelByUid(buser.GetLogin().UserID);
            DataTable dt = stepBll.SelByUid(mu.UserID);
            RPT.DataSource = dt;
            RPT.DataBind();
        }
        protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = DataConverter.CLng(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "del":
                    stepBll.Del(id);
                    MyBind();
                    break;
            }
        }
    }
}