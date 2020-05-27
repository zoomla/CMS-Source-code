namespace ZoomLaCMS.Design.h5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL.Design;
    using ZoomLa.Common;
    using ZoomLa.Model.Design;

    public partial class CheckPwd : System.Web.UI.Page
    {
        B_Design_Scence seBll = new B_Design_Scence();
        B_Design_PPT pptBll = new B_Design_PPT();
        private int Sid { get { return DataConverter.CLng(Request.QueryString["Sid"]); } }
        private string ZType { get { return DataConverter.CStr(Request.QueryString["ztype"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void SurePwd_Btn_Click(object sender, EventArgs e)
        {
            M_Design_Page seMod = null;
            switch (ZType)
            {
                case "ppt":
                    seMod = pptBll.SelReturnModel(Sid);
                    break;
                default:
                    seMod = seBll.SelReturnModel(Sid);
                    break;
            }
            remind_sp.InnerText = "";
            if (seMod.AccessPwd.Equals(AccessPwd_T.Text.Trim()))
            {
                B_Design_Helper.H5_AccessPwd += "," + seMod.ID + ",";
                switch (ZType)
                {
                    case "ppt":
                        Response.Redirect("/design/ppt/pcview.aspx?sid=" + seMod.ID);
                        break;
                    default:
                        Response.Redirect("/h5/" + seMod.ID);
                        break;
                }
            }
            else { remind_sp.InnerText = "访问密码不正确"; }
        }
    }
}