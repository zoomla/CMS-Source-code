using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;


public partial class MIS_MisInfoView : System.Web.UI.Page
{

    B_MisInfo bll = new B_MisInfo();
    M_MisInfo model = new M_MisInfo();
    DataTable dt = new DataTable();
    B_User buser = new B_User();
    new int ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin("/MIS/MisInfo.aspx");
        M_UserInfo info = buser.GetLogin();
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {
                 ID = DataConverter.CLng(Request.QueryString["ID"]);
                 model= bll.SelReturnModel(ID);
                 lblName.Text = model.Inputer;
                 lblCreateTime.Text = model.CreateTime.ToString();
                 lblContent.Text = model.Content;
                 lblTitle.Text = model.Title;
                
            }
        } 
    }
}