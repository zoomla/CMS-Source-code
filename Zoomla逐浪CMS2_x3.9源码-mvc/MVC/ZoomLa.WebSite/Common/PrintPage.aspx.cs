using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.Common;
/*
 * 用于OA公文打印
 */ 
namespace ZoomLaCMS.Common
{
    public partial class PrintPage : System.Web.UI.Page
    {
        protected B_OA_Document oaBll = new B_OA_Document();
        protected M_OA_Document oaMod = new M_OA_Document();
        protected B_Mis_AppProg progBll = new B_Mis_AppProg();
        protected M_Mis_AppProg progMod = new M_Mis_AppProg();
        protected B_OA_Sign signBll = new B_OA_Sign();
        protected B_User userBll = new B_User();
        protected B_Admin adminBll = new B_Admin();
        protected OACommon oaCom = new OACommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!userBll.CheckLogin() && !adminBll.CheckLogin())
            {
                function.WriteErrMsg("无权限访问页面");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["appID"]))
            {
                oaMod = oaBll.SelReturnModel(Convert.ToInt32(Request.QueryString["appID"]));
                ModelHtml.Text = oaCom.ClearHolder(oaMod);
                SignImgBind();
            }
            else
            {
                if (Session["PrintCon"] != null)
                {
                    ModelHtml.Text = Session["PrintCon"].ToString();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "creatimg('" + Session["PrintImg"] + "');", true);
                }
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DisProg", "DisProg(\"" + oaCom.GetHolder(oaMod, 0) + "\");", true);
        }
        //签章
        private void SignImgBind()
        {
            DataTable imgDT = progBll.SelHasSign(Convert.ToInt32(Request.QueryString["appID"]));
            string result = "";
            foreach (DataRow dr in imgDT.Rows)
            {
                result += dr["Sign"].ToString() + ":" + dr["VPath"].ToString() + ",";
            }
            //如果有个人签章
            int signid = 0;
            if (!string.IsNullOrEmpty(oaMod.SignID) && oaMod.SignID.Split(':').Length > 0 && Int32.TryParse(oaMod.SignID.Split(':')[0], out signid))
            {
                M_OA_Sign signMod = signBll.SelReturnModel(signid);
                result += oaMod.SignID + ":" + signMod.VPath;
            }
            else
                result = result.TrimEnd(',');
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "InitPos('" + result + "');", true);
        }
    }
}