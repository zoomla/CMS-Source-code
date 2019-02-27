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
using ZoomLa.Sns.BLL;

public partial class Zone_ZoneFriendSearch : Page 
{

    private string Xmlurl
    {
        get
        {
            return Server.MapPath(@"~/User/Command/UserRegXml.xml");
        }
        set
        {
            Xmlurl = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            Bind();
    }

    private void Bind()
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

    //绑定城市
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        string pro = DropDownList3.SelectedValue;
        if (pro != "")
        {
            this.DropDownList4.Visible = true;
            //List<Pcity> listc = ct.ReadCity(Server.MapPath(@"~/User/Command/SystemData.xml"), pro);
            //DropDownList4.DataSource = listc;
            //DropDownList4.DataTextField = "name";
            //DropDownList4.DataValueField = "code";
            //DropDownList4.DataBind();
        }
        else
        {
            this.DropDownList4.Items.Clear();
            this.DropDownList4.Visible = false;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string age1 = this.TextBox1.Text;
        string age2 = this.TextBox2.Text;
        string sextext = this.RadioButtonList1.Text;
        string pro="",citys="";
        if (DropDownList3.SelectedValue != "")
        {
            pro = this.DropDownList3.Text;
            citys = this.DropDownList4.Text;
        }
        //string marry = this.marryDropDownList.Text;
        Response.Redirect("GetSearch.aspx?age1=" + age1 + "&age2=" + age2 + "&sextext=" + sextext + "&pro=" + pro + "&citys=" + citys);
        
    }
}
