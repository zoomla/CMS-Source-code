using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class User_UserFunc_Watermark_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Title_T.Text = Call.SiteName+"证书生成";
        B_User buser = new B_User();
        M_UserInfo mu = buser.GetLogin();
        Name.Text = mu.UserName;
        VName.Text = "queezn:197016";
        CardName.Text = "奥华康,加纳";
        GiveMan.Text = Call.SiteName;
        StartDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
        EndDate.Text = DateTime.Now.AddYears(1).ToString("yyyy/MM/dd");
        Auth_Code_T.Text = "1401459842002690";
        Auth_Name_T.Text = Call.SiteName;
    }
}