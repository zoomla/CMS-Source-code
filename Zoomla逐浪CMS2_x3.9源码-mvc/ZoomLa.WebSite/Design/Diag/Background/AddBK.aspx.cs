using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Design;
using ZoomLa.Common;

namespace ZoomLaCMS.Design.Diag.Background
{
    public partial class AddBK : System.Web.UI.Page
    {
        B_Design_RES resBll = new B_Design_RES();
        public string Mode { get { return Request.QueryString["mode"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //RPTVideo.DataSource = null;
                //RPTVideo.DataBind();
                MyBind();
            }
        }
        public void MyBind()
        {
            RPTMinImg.DataSource = FileSystemObject.SearchImg(Server.MapPath("/Design/res/bkminimg/"));
            RPTMinImg.DataBind();
            if (Mode.Equals("scence"))
            {
                imageRad.Visible = false;
                imageTab.Visible = false;
                h5Rad.Visible = true;
                h5Tab.Visible = true;
                h5RPT.DataSource = resBll.SelByType("bk_h5", "img");
                //h5RPT.DataSource = FileSystemObject.SearchImg(Server.MapPath("/Design/res/bkimg/"));
                h5RPT.DataBind();
            }
            else
            {
                RPTImg.DataSource = resBll.SelByType("bk_pc", "img");
                //RPTImg.DataSource = FileSystemObject.SearchImg(Server.MapPath("/Design/res/bkimg/"));
                RPTImg.DataBind();
            }
        }
    }
}