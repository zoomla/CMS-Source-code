using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Shop;

public partial class User_Shop312_AddDeliverType : System.Web.UI.Page
{
    B_Shop_FareTlp fareBll = new B_Shop_FareTlp();
    B_User buser = new B_User();
    public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        if (Mid > 0)
        {
            M_Shop_FareTlp fareMod = fareBll.SelReturnModel(Mid);
            M_UserInfo mu = buser.GetLogin();
            if (mu.UserID != fareMod.UserID) { function.WriteErrMsg("你无权限修改该模板!"); }
            TlpName_T.Text = fareMod.TlpName;
            Fare_Hid.Value = fareMod.Express;
            function.Script(this, "ShowPrice();");
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        M_Shop_FareTlp fareMod = new M_Shop_FareTlp();
        if (Mid > 0) { fareMod = fareBll.SelReturnModel(Mid); }
        fareMod.TlpName = TlpName_T.Text;
        fareMod.PriceMode = Convert.ToInt32(Request.Form["pricemod_rad"]);
        fareMod.Express = Fare_Hid.Value;//运费Json,后期扩展支持地区,重量
        fareMod.UserID = mu.UserID;//312用于存用户ID,后台ID为DBNull
        //fareMod.Mail = "";
        fareMod.Remind = Remind_T.Text;
        fareMod.Remind2 = Remind2_T.Text;
        if (Mid > 0)
        { fareBll.UpdateByID(fareMod); }
        else { fareBll.Insert(fareMod); }
        Response.Redirect("DeliverType.aspx");
    }
}