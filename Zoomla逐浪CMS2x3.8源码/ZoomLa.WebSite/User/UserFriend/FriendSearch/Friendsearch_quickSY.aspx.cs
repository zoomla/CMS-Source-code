using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
public partial class Friendsearch_quickSY : Page
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            GetPage();
        }
    }
    private string Xmlurl
    {
        get{ return Server.MapPath(@"~/User/Command/UserRegXml.xml");}
        set{ Xmlurl = value;}
    }
    private void GetPage()
    {
        if(!this.Page.IsPostBack)
        {
        //绑定省
        //List<province> list2 = ct.readProvince(Server.MapPath(@"~/User/Command/SystemData.xml"));
        //DropDownList3.DataSource = list2;
        //DropDownList3.DataTextField = "name";
        //DropDownList3.DataValueField = "code";
        //DropDownList3.DataBind();
        ListItem li3 = new ListItem();
        li3.Value = "";
        li3.Text = "不限";
        li3.Selected = true;
        DropDownList3.Items.Add(li3);
        } 
    }
    //绑定城市
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        string pro = DropDownList3.SelectedValue;
        if (pro != "")
        {
            //this.DropDownList4.Visible = true;
            //List<Pcity> listc = ct.ReadCity(Server.MapPath(@"~/User/Command/SystemData.xml"), pro);
            //DropDownList4.DataSource = listc;
            //DropDownList4.DataTextField = "name";
            //DropDownList4.DataValueField = "code";
            //DropDownList4.DataBind();
        }
        else
        {
            DropDownList4.Visible = false;
            DropDownList4.Items.Clear();
        }
    }  
}