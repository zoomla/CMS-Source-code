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
using ZoomLa.Common;

public partial class User_Project_ProjectRequire : System.Web.UI.Page
{
    private int m_uid = 1;//测试用
    private M_UserInfo muser = new M_UserInfo();
    private B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (HttpContext.Current.Request.Cookies["UserState"] != null)
            {
                this.m_uid = DataConverter.CLng(HttpContext.Current.Request.Cookies["UserState"]["UserID"]);
                TxtUserName.Text = buser.SeachByID(this.m_uid).UserName;
            }
            else
            {
                TxtUserName.Text = "过客";
                this.m_uid = 0;//允许过客发表需求
            }
            
        }
        

    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        string strusername = TxtUserName.Text.Trim();
        string strcontent = TxtRequireContent.Text.Trim();
        B_ClientRequire bclientrequire = new B_ClientRequire();
        M_ClientRequire mrequire=new M_ClientRequire();
        mrequire.Require=strcontent;
        mrequire.UserID=this.m_uid;
        mrequire.ReuqireDate=DateTime.Now;
        if (bclientrequire.Add(mrequire))
        {
            Response.Write("<script language=javascript> alert('添加成功！');window.document.location.href='ProjectList.aspx';</script>");
        }

    }
}
