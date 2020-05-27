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
using ZoomLa.Components;
using ZoomLa.Model;
using System.Xml;
using System.IO;
using System.Text;

public partial class User_UserZone_School_AddRessList : System.Web.UI.Page
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
            MyBind();
          
        }

    }

    //private string getnodetxt(string xmldocuments ,string nodename)
    //{
    //    xmldocuments = "<?xml version=\"1.0\" encoding=\"gb2312\"?><xml>" + xmldocuments+"</xml>";
    //    XmlDocument xmlread = new XmlDocument();
    //    xmlread.LoadXml(xmldocuments);
    //    XmlNode nodelist = xmlread.SelectSingleNode("//Userinfo/"+nodename+"");
    //    if (nodelist is object)
    //    {
    //        return nodelist.InnerText;
    //    }
    //    else { return ""; 
    //    }
    //}

    private DataSet xmltotable(string xmldocuments)
    {
        xmldocuments = "<?xml version=\"1.0\" encoding=\"gb2312\"?><xml>" + xmldocuments + "</xml>";
        Stream xmlreadStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(xmldocuments));
        DataSet setfs = new DataSet();
        setfs.ReadXml(xmlreadStream);
        return setfs;
    }


    private void MyBind()
    {
        M_UserInfo uinfo = ubll.GetLogin(); 
        //DataTable dt = bar.Select_Where(" UserID=" + uinfo.UserID, " stuff(AddRessContext,50,0,'<ID>'+CAST(ID AS NVARCHAR(10))+'</ID>') ", "UserID");
        DataTable dt = new DataTable();

        //Response.Write(getnodetxt(dt.Rows[0][0].ToString(), "UserName"));
        //Response.End();
        string xml = "";
        DataTable newtable = new DataTable();
        //Response.Write(dt.Rows.Count);
        //Response.End();
        if (dt.Rows.Count > 0)
        {
            int ii = 0;
            foreach (DataRow dr in dt.Rows)
            {
                xml += dt.Rows[ii][0].ToString();
                xml = xml.Replace("<?xml version=\"1.0\" encoding=\"gb2312\"?>", "");
                ii = ii + 1;
            }
        }

        DataSet ds = xmltotable(xml);
        if (ds.Tables.Count > 0)
            newtable = ds.Tables[0].DefaultView.Table;
        EGV.DataSource = newtable;
        EGV.DataBind();
    }
 
    protected string GetContext(string str)
    {
        if (str.Length > 30)
        {
            return str.Substring(0, 27) + "....";
        }
        else
        {
            return str;
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bar.GetDelete(int.Parse(EGV.DataKeys[e.RowIndex].Value.ToString()));
        Response.Write("<script>location.href='AddRessList.aspx'</script>");
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
}
