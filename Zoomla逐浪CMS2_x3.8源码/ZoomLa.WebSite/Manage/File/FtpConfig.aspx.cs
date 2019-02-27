using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.BLL.FTP;
using ZoomLa.Model.FTP;
using System.Data;

public partial class manage_File_FtpConfig : CustomerPageAction
{
    private B_FTP b_FTP = new B_FTP();
    private M_FtpConfig m_FtpConfig = new M_FtpConfig();
    private DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin badmin = new B_Admin();
        if (!IsPostBack)
        {
            this.FTPID.Value = Request["DSId"] == null ? "0" : Request["DSId"];
            if (Request.QueryString["DSId"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["DSID"]);
                m_FtpConfig = b_FTP.SeleteIDByAll(id);
                this.Alias.Value = m_FtpConfig.Alias;
                this.txt_fs.Text = m_FtpConfig.FtpServer;
                this.txt_pt.Text = m_FtpConfig.FtpPort;
                this.txt_user.Text = m_FtpConfig.FtpUsername;
                this.txt_pass.Text = m_FtpConfig.FtpPassword;
                this.txt_url.Text = m_FtpConfig.Url;
                this.txt_file.Text = m_FtpConfig.SavePath;
                this.Label1.Text = "云端存储修改";
                this.Save_Btn.Text = "修改配置";
            }
            else
            {
                this.Label1.Text = "云端存储配置";
                this.Save_Btn.Text = "添加配置";
                this.Alias.Value = DateTime.Now.ToString("ffff");
            }
            if (Request["action"] == "CheckName")
            {
                CheckName();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "plus/ADManage.aspx'>扩展功能</a></li><li><a href='UploadFile.aspx'>文件管理</a></li><li><a href='FtpAll.aspx'>云端存储</a></li><li><a href='FtpConfig.aspx'>添加云端服务器</a></li>");
        }
    }

    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request.QueryString["DSID"]);

        m_FtpConfig.FtpServer = txt_fs.Text.Trim();
        m_FtpConfig.FtpPort = txt_pt.Text.Trim();
        m_FtpConfig.Alias = Alias.Value.Trim();
        m_FtpConfig.FtpUsername = txt_user.Text.Trim();
        m_FtpConfig.FtpPassword = txt_pass.Text.Trim();
        m_FtpConfig.Url = txt_url.Text.Trim();
        m_FtpConfig.OutTime = "25";
        m_FtpConfig.SavePath = txt_file.Text.Trim();

        if (Request.QueryString["DSID"] != null)
        {
            b_FTP.UpdateFtpFile(id, m_FtpConfig);
            function.WriteSuccessMsg("修改成功!", "FtpAll.aspx");
        }
        else
        {
            b_FTP.AddFTPFile(m_FtpConfig);
            function.WriteSuccessMsg("添加成功!", "FtpAll.aspx");
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Upload(this.txt_file.Text.Trim());
    }

    /// <summary>
    /// 查询远程FTP用户名，密码及服务器
    /// </summary>
    private void Upload(string filename)
    {
        string ftpUserID = this.txt_user.Text.Trim();
        string ftpPassword = this.txt_pass.Text.Trim();
        filename = this.txt_file.Text;
        string ftpServerIP = this.txt_fs.Text;
        FileInfo fileInf = new FileInfo(filename);
        string Uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;
        //throw new Exception(Uri);
        try
        {
            FtpWebRequest reqFTP;

            // 根据uri创建FtpWebRequest对象 
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileInf.Name));
            //reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/"));

            //throw new Exception(Uri);

            // ftp用户名和密码 
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

            if (reqFTP.UsePassive == true)
            {
                reqFTP.UsePassive = false;
            }
            else
            {
                reqFTP.UsePassive = true;
            }

            // 默认为true，连接不会被关闭 
            // 在一个命令之后被执行 
            reqFTP.KeepAlive = false;

            // 指定执行什么命令 
            //reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            // 指定数据传输类型 
            reqFTP.UseBinary = true;
            FtpWebResponse fwr = (FtpWebResponse)reqFTP.GetResponse();
            fwr.Close();
            function.WriteSuccessMsg("测试成功!");
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("530"))
            {
                function.WriteErrMsg("用户名或密码错误!");
            }
            else if (ex.Message.Contains("550"))
            {
                function.WriteErrMsg("没有对应的服务器!");
            }
            else
            {
                function.WriteErrMsg("没有对应的服务器!");
            }
        }
    }

    private void CheckName()
    {
        string strAlias = Request["Alias"];
        dt = b_FTP.SelByAlias(strAlias);
        if (Request["ID"] == "0")
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                Response.Write("1");
            }
            else
            {
                Response.Write("0");
            }
        }
        else
        {
            m_FtpConfig = b_FTP.SeleteIDByAll(Convert.ToInt32(Request["ID"]));
            if (!(dt != null && dt.Rows.Count > 0) || (strAlias == m_FtpConfig.Alias))
            {
                Response.Write("0");
            }
            else
            {
                Response.Write("1");
            }
        }
        Response.Flush();
        Response.Close();
    }
}