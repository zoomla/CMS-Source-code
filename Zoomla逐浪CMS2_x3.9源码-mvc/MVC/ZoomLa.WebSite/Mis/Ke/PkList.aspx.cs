using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Room;
using ZoomLa.BLL.Exam;
using ZoomLa.Model;
using ZoomLa.Model.Exam;
using ZoomLa.Common;

namespace ZoomLaCMS.MIS.Ke
{
    public partial class PkList : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_EDU_AutoPK PkBll = new B_EDU_AutoPK();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        void MyBind()
        {
            RPT.DataSource = PkBll.SelByUid(buser.GetLogin().UserID);
            RPT.DataBind();
        }
        public string GetUserName()
        {
            return buser.SelReturnModel(Convert.ToInt32(Eval("UserID"))).UserName;
        }
        protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}