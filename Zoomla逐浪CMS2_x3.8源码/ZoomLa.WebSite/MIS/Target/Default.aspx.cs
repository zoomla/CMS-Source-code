using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class MIS_Target : System.Web.UI.Page
{
    B_Mis bll = new B_Mis();
    DataTable dt = new DataTable();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
       
     }

    protected void Bind1()
    {
      
    }


    protected void Button_Click(object sender, EventArgs e)
    {
        string drType = this.drType.SelectedValue;
        string TxtKey = this.TxtKey.Text;
        dt = bll.selbytitle(buser.GetLogin().UserName, drType, TxtKey);
        Repeater2.DataSource = dt;
        Repeater2.DataBind();
    }

    protected string GetLong(string id)
    {
        return "";
    }

    protected string GetPic(string pic)
    {
        string str = ""; 
        str = SiteConfig.SiteOption.UploadDir.Replace("/","");
        if (pic != "")
            str = "/" + str + "/" + pic;
        else str = "/" + str + "/nopic.gif";
        return str;
    }

}