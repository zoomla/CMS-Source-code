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
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Model;
using FreeHome.common;
using ZoomLa.Sns.BLL;

public partial class Search_Constellation : Page 
{
    B_User utbll = new B_User();
    UserTableBLL ut = new UserTableBLL();
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
        {
            M_UserInfo uinfo = utbll.GetUserByUserID(utbll.GetLogin().UserID);
            GetPage();
        }   
    }

    private void GetPage()
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

        //绑定星座
        cblConstellation.DataSource =UserRegConfig.GetInitUserReg(Xmlurl, "UserConstellation");
        cblConstellation.DataBind();
        
        //绑定血型
        cblBlood.DataSource= UserRegConfig.GetInitUserReg(Xmlurl, "UserBlood");
        cblBlood.DataBind();
    }

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
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string age1 = this.txtAge1.Text;
        string age2 = this.txtAge2.Text;
        string sextext = this.RadioButtonList1.Text;
        string pro = this.DropDownList3.Text;
        string citys = this.DropDownList4.Text;
        string blood = Getstr(cblBlood);
        string constellation = Getstr(cblConstellation);
        this.quickPanel.Visible = false;
        this.quickresultPanel.Visible = true;
        this.DataList1.DataSource = ut.GetConstellation(utbll.GetLogin().UserID, age1, age2, sextext, pro, citys, blood, constellation);
        this.DataList1.DataBind();
        if (DataList1.Items.Count < 1)
            this.Label1.Text = "暂时没有符合你要求的结果！";
    }

    protected string Getstr(CheckBoxList cb)
    {
        string str = "";
        for (int i = 0; i < cb.Items.Count; i++)
        {
            if (cb.Items[i].Selected)
                str += cb.Items[i].Text+",";
        }
        return str;
    }
}

