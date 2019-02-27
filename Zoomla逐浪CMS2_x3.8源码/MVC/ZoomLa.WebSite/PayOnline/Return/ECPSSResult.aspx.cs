namespace ZoomLaCMS.PayOnline.Return
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Model;
    using System.Web.Security;
    using ZoomLa.Common;
    public partial class ECPSSResult : System.Web.UI.Page
    {
        B_PayPlat platBll = new B_PayPlat();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_PayPlat platmod = platBll.SelModelByClass(M_PayPlat.Plat.ECPSS);
                string md5key = platmod.MD5Key;
                BillNo_L.Text = Request["BillNo"];
                Amount_L.Text = Request["Amount"];
                Result_L.Text = Request["Result"];
                string Succeed = Request["Succeed"];
                string signinfo = Request["SignMD5info"];
                string md5src = BillNo_L.Text + "&" + Amount_L.Text + "&" + Succeed + "&" + md5key;
                if (!signinfo.Equals(FormsAuthentication.HashPasswordForStoringInConfigFile(md5src, "MD5")))
                {
                    function.WriteErrMsg("网页验证失败!");
                }
            }
        }
    }
}