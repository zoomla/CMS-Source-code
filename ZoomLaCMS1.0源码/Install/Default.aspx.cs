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
using System.Data.SqlClient;
using ZoomLa.Components;
using System.Xml;
using System.Text;
using System.IO;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Net;
using System.Web.Configuration;
public partial class Install_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IsInStall();
        InitInstall();
        
    }
    /// <summary>
    /// 检查一下是否安装过
    /// </summary>
    public void IsInStall() 
    { 

        string str = WebConfigurationManager.AppSettings["Installed"].ToLower();
        if (Convert.ToBoolean(str))
                Page.Response.Redirect("../Default.aspx");       
            
    }
    public void InitInstall()
    {
        string fileName = Server.MapPath("../Config/ZL_License.txt"); 
        StringBuilder builder = new StringBuilder();
        using (StreamReader reader = new StreamReader(fileName, System.Text.Encoding.GetEncoding("UTF-8")))// Encoding.GetEncoding("GB2312")
        {
            try
            {
                while (!reader.EndOfStream)
                {
                    string str = reader.ReadLine();
                    if (!string.IsNullOrEmpty(str))
                    {
                        builder.AppendLine(str);  
                    }                                     
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
        TxtLicense.Value = builder.ToString();  
    }
    protected void ChlkAgreeLicense_CheckedChanged(object sender, EventArgs e)
    {
        if (ChlkAgreeLicense.Checked)
            StartNextButton.Enabled = true;  
    }
    protected void PreviousButtonStep3_Click(object sender, EventArgs e)
    {

    }
    protected void NextButtonStep4_Click(object sender, EventArgs e)
    {
        TxtSiteUrl.Text ="http://"+ Request.ServerVariables["HTTP_HOST"].ToString();
    }   
    protected void NextButtonStep3_Click(object sender, EventArgs e)
    {
        TxtPwd.Value = TxtPassword.Text.ToString();
        HDSql.Value = DropSqlVersion.SelectedValue.ToString();
    }
    protected void NextButtonStep5_Click(object sender, EventArgs e)
    {
        B_User buser = new B_User();
        string message=string.Empty;
        string strHostIP = "";
        IPHostEntry oIPHost = Dns.GetHostEntry(Environment.MachineName);
        if (oIPHost.AddressList.Length > 0)
            strHostIP = oIPHost.AddressList[0].ToString();

        string adminname = TxtAdminName.Text.ToString();
        string strtitle =TxtSiteTitle.Text.ToString();
      
        string Code = TxtSiteManageCode.Text;
        string pwd = TxtAdminPassword.Text.ToString();//管理员密码
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("../Config/Site.config"));
        XmlElement xmldocSelect = (XmlElement)xmlDoc.DocumentElement.SelectSingleNode("SiteInfo");       
        XmlElement xe2 = (XmlElement)xmldocSelect.SelectSingleNode("SiteTitle");
        xe2.InnerText=strtitle; 
        XmlNode xe3 = (XmlElement)xmldocSelect.SelectSingleNode("SiteUrl");//.ChildNodes
        xe3.InnerText = TxtSiteUrl.Text.ToString();     
        XmlElement xmldocSelect2 = (XmlElement)xmlDoc.DocumentElement.SelectSingleNode("SiteOption");
        XmlElement xe5 = (XmlElement)xmldocSelect2.SelectSingleNode("SiteManageCode");
        xe5.InnerText = Code;
        xmlDoc.Save(Server.MapPath("../Config/Site.config"));

        //WebConfigurationManager.AppSettings["Installed"]="true";

        XmlDocument xmlDoc2 = new XmlDocument();
        xmlDoc2.Load(Server.MapPath("../Config/AppSettings.config"));
        XmlNodeList amde = xmlDoc2.SelectSingleNode("appSettings").ChildNodes;
        foreach (XmlNode xn in amde)
        {
            XmlElement xe = (XmlElement)xn;
            if (xe.GetAttribute("key").ToString() == "Installed")
                xe.SetAttribute("value", "true");
        }
        xmlDoc2.Save(Server.MapPath("../Config/AppSettings.config"));

        if (Install.Add("admin", pwd))
        {
            M_UserInfo muser = new M_UserInfo();
            muser.UserName = "admin";
            muser.UserPwd = StringHelper.MD5(pwd);
            muser.RegTime = DateTime.Now;
            muser.LastLockTime = DateTime.MaxValue;
            muser.LastLoginTimes = DateTime.Now;
            muser.LastPwdChangeTime = DateTime.MaxValue;
            muser.Email = TxtEmail.Text;
            muser.Question ="admin";
            muser.Answer = StringHelper.MD5(pwd);
            muser.GroupID = 0;
            muser.UserFace = "";
            muser.Sign = "";
            muser.LastLoginIP = strHostIP;
            muser.CheckNum = new Random().ToString();
            buser.Add(muser);
            message="<script language=javascript> alert('安装完成！');</script>";
        }
        else
        {
            message="<script language=javascript> alert('安装配置失败！请检查后重新操作!');</script>";
        }
        if (!this.IsStartupScriptRegistered("message"))
        {
            Page.RegisterStartupScript("message", message);
        }           
    
    }
    /// <summary>
    /// 创建数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnCreateDateBase_Click(object sender, EventArgs e)
    {        
        string sqlversion=DropSqlVersion.SelectedValue;
        string datasource=TxtDataSource.Text.ToString().Trim();
        string dataname =TxtDataBase.Text.ToString();
        string username = TxtUserID.Text.ToString();
        string userpwd = TxtPwd.Value.ToString();    
        string connstr = "Data Source=" + datasource + ";Initial Catalog=" + dataname + ";User ID=" + username + ";Password=" + userpwd;
        string sqlpath =string.Empty;
        if (HDSql.Value.ToString().Trim() == "2000")
            sqlpath = "../App_Data/ZoomLaCMS2000.sql";
        else
            sqlpath = "../App_Data/ZoomLaCMS2005.sql";
        string filename = Server.MapPath(sqlpath);  
        string strmessage ="alert('数据库配置完成!')";// string.Empty;
        bool flag = false;
        if (Install.Connection(connstr) == null)
        {
            strmessage ="alert('数据库连接失败,可能如下原因: \\n1.数据库不存在!\\n2.用户或密码错误\\n3.用户不具有访问该数据库权限')";            
        }
        else
        {
            if (!writexml(connstr)) 
            {
                strmessage="alert('配置数据库失败!')";
            }
            else
            {
                if (!Install.CreateDataBase(filename, connstr))
                {
                    strmessage="alert('用户无写入权限或数据库已经存在,执行sql脚本失败!')";
                }
                else
                {
                    flag = true;
                }

            }
        }
        if (!flag)
        {
            strmessage = "<script>" + strmessage + "; </script>";
            NextButtonStep4.Enabled = false;
        }
        else
        {
            strmessage = "<script>" + strmessage + "; </script>";//window.document.location.href=window.document.location.href
          
            NextButtonStep4.Enabled = true;
            BtnCreateDateBase.Enabled = false;
        }

        if (!this.IsStartupScriptRegistered("check"))
        {
            Page.RegisterStartupScript("check", strmessage);
        }
    }
    public bool writexml(string connstr)
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("../Config/ConnectionStrings.config"));
            XmlElement xmldocSelect = (XmlElement)xmlDoc.DocumentElement.SelectSingleNode("add");            
            xmldocSelect.SetAttribute("connectionString", connstr);
            xmlDoc.Save(Server.MapPath("../Config/ConnectionStrings.config"));//ConnectionStrings.config
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public void writexml()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("user.xml"));
        XmlNode xmldocSelect = xmlDoc.SelectSingleNode("ServerConfig");
        //XmlElement el = xmlDoc.CreateElement("person"); //添加person节点
        //XmlElement element1 = xmlDoc.CreateElement("id");
        //element1.InnerText = "sdf";//向创建的节点中添加文本
        //el.AppendChild(element1);
        //el.SetAttribute("name", "风云"); //添加person节点的属性"name"
        //el.SetAttribute("sex", "女"); //添加person节点的属性 "sex"
        //el.SetAttribute("age", "25"); //添加person节点的属性 "age"
        //xmldocSelect.AppendChild(el);
        XmlNodeList amend =xmldocSelect.SelectSingleNode("person").ChildNodes;
        foreach (XmlNode xn in amend)
        {
            XmlElement xe = (XmlElement)xn;
            xe.SetAttribute("id", "11");
        }
        //amend.SetAttribute("id","sgsg");
        //amend.SetAttribute("connectionString","server=(local);database=123");
        //xmldocSelect.AppendChild(amend);
        xmlDoc.Save(Server.MapPath("user.xml"));
    }    
    protected void ChlkIsCreateDataBase_CheckedChanged(object sender, EventArgs e)
    {
        if (ChlkIsCreateDataBase.Checked)
            NextButtonStep4.Enabled = true;
        else
            NextButtonStep4.Enabled = false;
    }
    protected void WzdInstall_NextButtonClick(object sender, EventArgs e)
    {
    }
    protected void WzdInstall_FinishButtonClick(object sender, EventArgs e)
    {
    }
}
