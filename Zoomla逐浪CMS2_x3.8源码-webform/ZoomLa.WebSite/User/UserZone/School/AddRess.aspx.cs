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
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Xml;
using ZoomLa.Common;
public partial class User_UserZone_School_AddRess : System.Web.UI.Page
{
    #region 业务对象
    B_User ubll = new B_User();
    B_AddRessList bar = new B_AddRessList();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        ubll.CheckIsLogin();
        if (!IsPostBack)
        {
            M_UserInfo uinfo = ubll.GetLogin();

            if (Request.QueryString["AID"] != null)
            {
                M_AddRessList mar = bar.GetSelect(int.Parse(Request.QueryString["AID"]));
                txtName.Text = getnodetxt(mar.AddRessContext, "UserName");
                txtPhone.Text = getnodetxt(mar.AddRessContext, "UserPhone");
                txtQQ.Text = getnodetxt(mar.AddRessContext, "UserQQ");
                txtMSN.Text = getnodetxt(mar.AddRessContext, "UserMSN");
                txtAdd.Text = getnodetxt(mar.AddRessContext, "UserAdd");
                txtMail.Text = getnodetxt(mar.AddRessContext, "UserMail");
                txtContext.Text = getnodetxt(mar.AddRessContext, "UserContext");
            }
        }
    }

    private string getnodetxt(string xmldocuments, string nodename)
    {
        XmlDocument xmlread = new XmlDocument();
        xmlread.LoadXml(xmldocuments);

        XmlNode nodelist = xmlread.SelectSingleNode("//Userinfo/" + nodename + "");
        if (nodelist is object)
        {
            return nodelist.InnerText;
        }
        else
        {
            return "";
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string strXML = "<?xml version=\"1.0\" encoding=\"gb2312\"?><Userinfo>";

        strXML += "<UserName>" +BaseClass.CheckInjection(txtName.Text) + "</UserName>";
        strXML += "<UserPhone>" + BaseClass.CheckInjection(txtPhone.Text) + "</UserPhone>";
        strXML += "<UserQQ>" + BaseClass.CheckInjection(txtQQ.Text) + "</UserQQ>";
        strXML += "<UserMSN>" + BaseClass.CheckInjection(txtMSN.Text) + "</UserMSN>";
        strXML += "<UserMail>" + BaseClass.CheckInjection(txtMail.Text) + "</UserMail>";
        strXML += "<UserAdd>" + BaseClass.CheckInjection(txtAdd.Text) + "</UserAdd>";
        strXML += "<UserContext>" + BaseClass.CheckInjection(txtContext.Text) + "</UserContext>";
        strXML += "</Userinfo>";


        M_AddRessList mar = new M_AddRessList();
        mar.UserID = ubll.GetLogin().UserID;
        mar.AddRessContext = strXML;
        if (Request.QueryString["AID"] != null)
        {
            mar.ID = int.Parse(Request.QueryString["AID"]);
            bar.GetUpdate(mar);
        }
        else
        {
            bar.GetInsert(mar);
        }
        Response.Write("<script>location.href='AddRessList.aspx'</script>");
    }
}
