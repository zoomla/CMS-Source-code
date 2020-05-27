using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Plat.Group
{
    public partial class GroupDetail : System.Web.UI.Page
    {
        private B_Plat_Group gpBll = new B_Plat_Group();
        private B_User_Plat upBll = new B_User_Plat();
        public int Gid
        {
            get { return DataConvert.CLng(Request.QueryString["ID"]); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            MyBind();
        }
        public void MyBind()
        {
            M_Plat_Group gpMod = gpBll.SelReturnModel(Gid);
            M_User_Plat upMod = upBll.SelReturnModel(gpMod.CreateUser);
            GName_L.Text = gpMod.GroupName;
            DataTable dt = upBll.SelByGroup(upMod.CompID, Gid);
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            MyBind();
        }
    }
}