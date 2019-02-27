using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;


public partial class manage_User_UserConfirm : CustomerPageAction
{
    
    protected B_User buser = new B_User();
    int UserID;
    int IsConfirm;
    protected void Page_Load(object sender, EventArgs e)
    {
        ZoomLa.Common.function.AccessRulo();
        B_Admin ba = new B_Admin();
        ba.CheckIsLogin(); 
         UserID = Convert.ToInt32(Request.QueryString["uid"]);
          // int GroupID = Convert.ToInt32(Request.QueryString["Gid"]);    
        IsConfirm = Convert.ToInt32(Request.QueryString["IsConfirm"]);  
            if (!IsPostBack)
            {    
                this.Confirm.SelectedValue = IsConfirm.ToString();
            }
    }
    protected void submit_Click(object sender, EventArgs e)
    {
       // M_UserInfo muser = new M_UserInfo();      
      //  muser.UserID = UserID;     
        IsConfirm =Convert.ToInt32(this.Confirm.SelectedValue); 
        buser.UpdateUserConfirm(UserID,IsConfirm);       
        Response.Redirect("UserManage.aspx");
    }
}
