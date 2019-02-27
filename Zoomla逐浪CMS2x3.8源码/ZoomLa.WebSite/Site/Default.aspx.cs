using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

public partial class Plugins_Domain_InquiryDomName : System.Web.UI.Page
{
    protected B_User buser = new B_User();
    protected M_UserInfo userInfo = new M_UserInfo();
    protected M_Uinfo uinfo = new M_Uinfo();
    protected PagedDataSource pds = new PagedDataSource();
    protected string serverdomain = SiteConfig.SiteOption.ProjectServer;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataBind();
        }
    }
    private void DataBind(string key="")
    {
        //PageSize固定,只需要获取当前页即可
        pds.DataSource = TempDT;
        pds.PageSize = 30;
        pds.AllowPaging = true;
        //int cPage = GetPageIndex(pds, Request.QueryString["page"]);
        pds.CurrentPageIndex = 1;
        tempRepeater.DataSource = pds;
        tempRepeater.DataBind();
    }
    protected void checkBtn_Click(object sender,EventArgs e)
    {
        domNameL.Text = domNameT.Text.Trim().TrimEnd('.')+StationGroup.TDomName;


        if (buser.CheckLogin())
        {
            GetToNext();
        }
        else
        {
            regDiv.Visible = true;
        }
    }
    protected void userRegBtn_Click(object sender, EventArgs e)
    {
       
        userInfo.UserName = userNameT.Text.Replace(" ","");
        userInfo.UserPwd = StringHelper.MD5(userPwdT.Text.Trim());
        remindSpan.Visible = true;
        remindSpan.InnerText = "";
        if (string.IsNullOrEmpty(userNameT.Text.Trim())||string.IsNullOrEmpty(userPwdT.Text.Trim()))
        {
             remindSpan.InnerText = "用户名与密码不能为空!!";
        }
        else if (!buser.IsExistUName(userInfo.UserName))
        {
            buser.AddModel(userInfo);
            buser.SetLoginState(userInfo, "Day");
            GetToNext();
        }
        else
        {
            remindSpan.InnerText = "用户已存在!!";
        }
    }
    //已有用户登录
    protected void loginBtn_Click(object sender, EventArgs e)
    {
        string user = TextBox1.Text.Trim();
        string passwd = TextBox2.Text.Trim();
        remindSpan.Visible = true;
        if (!string.IsNullOrEmpty(user) || !string.IsNullOrEmpty(passwd))
        {
           userInfo = buser.AuthenticateUser(user, passwd);
           if (userInfo.IsNull)
               remindSpan.InnerText = "用户名或密码错误!!!";
           else
           {
               buser.SetLoginState(userInfo, "Day");
               GetToNext();
           }
        }
        else
        {
            remindSpan.InnerText = "用户名与密码不能为空!!!";
        }
    }
    private void GetToNext() 
    {
        Session["domNameL"] = domNameT.Text + StationGroup.TDomName;
        Response.Redirect("/Plugins/Domain/CreateSite.aspx?url=" + domNameL.Text);
    }
    public string GetTDomName()
    {
        return StationGroup.TDomName;
    }
    //------------------中间模板列表支持
    //模板表,模板数据较少更改，存入Cache缓存中
    public DataView TempDT
    {
        get
        {
            if (Cache["TempDT"] == null)
                Cache["TempDT"] = GetServerTemp();
            return (Cache["TempDT"] as DataTable).DefaultView;
        }
    }
    private DataTable GetServerTemp()
    {
        DataSet tableset = new DataSet();
        tableset.ReadXml(serverdomain + "/api/gettemplate.aspx?menu=getprojectinfo");
        return tableset.Tables[0];
    }
    public string GetThumbnail(string tempDirName,string project)
    {
        //Project
        string result = "<a class='thumbnail lightbox' title='" + project + "'  href='" + serverdomain + "/Template/" + tempDirName + "/view.jpg' target='_PreView'>\r\n<img alt='' style='width:120px;height:185px;' src='" + serverdomain + "/Template/" + Server.UrlEncode(tempDirName) + "/view.jpg'></a>";
        return result;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        domNameL.Text = TextBox3.Text;
        checkBtn_Click(null,null);
        dataField.Value = "1";
    }
}