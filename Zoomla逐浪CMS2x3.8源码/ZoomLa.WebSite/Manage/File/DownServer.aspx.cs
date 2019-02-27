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
public partial class manage_Plus_DownServer : CustomerPageAction
{
    B_DownServer downll = new B_DownServer();
    protected void Page_Load(object sender, EventArgs e)
    {
        int DSId = 0;
        if (Encrypttype.SelectedValue != "0" && Encrypttype.SelectedValue != "1")
        {
            Encrypt.Visible = true;
        }
        else
        {
            Encrypt.Visible = false;
        }
        if (this.TimeEncrypt.Checked == true)
        {
            this.Tr2.Visible = true;
        }
        else
        {
            this.Tr2.Visible = false;
        }
        if (!Page.IsPostBack)
        {
            B_Group group = new B_Group();
            DataTable gtable = group.GetGroupList();
            ReadRoot.DataSource = gtable;
            ReadRoot.DataTextField = "GroupName";
            ReadRoot.DataValueField = "GroupID";
            ReadRoot.DataBind();
            if (Request.QueryString["Action"] != null)
            {
                if (Request.QueryString["Action"] == "Modify")
                {
                    LblTitle.Text = "修改下载服务器";
       
                    DSId = Convert.ToInt32(Request.QueryString["DSId"].Trim());
                    #region 显示默认数据
                    M_DownServer serverinfo = downll.GetDownServerByid(DSId);
                    this.Encrypttype.SelectedValue = serverinfo.UrlEncrypt.ToString();
                    this.EncryptKey.Text = serverinfo.EncryptKey.ToString();

                    if (serverinfo.UrlEncrypt < 2)
                    {
                        this.Encrypt.Visible = false;
                    }
                    else
                    {
                        this.Encrypt.Visible = true;
                    }

                    if (serverinfo.TimeEncrypt == 1)
                    {
                        this.TimeEncrypt.Checked = true;
                        this.Tr2.Visible = true;
                    }
                    else
                    {
                        this.TimeEncrypt.Checked = false;
                        this.Tr2.Visible = false;
                    }
                    this.UpTimeuti.Text = serverinfo.UpTimeuti.ToString();
                    this.UpTimeutiList.SelectedValue = serverinfo.UpTimeuti.ToString();
                    EncrypttypeShow();

                    string ReadRootstr = serverinfo.ReadRoot;
                    ReadRootstr = "," + ReadRootstr + ",";
                    for (int i = 0; i < this.ReadRoot.Items.Count; i++)
                    {
                        string itemsvalues = this.ReadRoot.Items[i].Value;
                        itemsvalues = "," + itemsvalues + ",";
                        if (ReadRootstr.IndexOf(itemsvalues) > -1)
                        {
                            this.ReadRoot.Items[i].Selected = true;
                        }
                    }

                    #endregion
                    InItModify(DSId);
                }
                else
                    LblTitle.Text = "添加下载服务器";
                 
            }
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "plus/ADManage.aspx'>扩展功能</a></li><li><a href='UploadFile.aspx'>文件管理</a></li><li><a href='DownServerManage.aspx'>下载服务器</a></li> <li><a href='DownServer.aspx'>" + LblTitle.Text + "</a></li>");
    }
    private void InItModify(int Dsid)
    {
        B_DownServer bdownerver = new B_DownServer();
        M_DownServer mdownserver = bdownerver.GetDownServerByid(Dsid);
        TxtServerName.Text = mdownserver.ServerName;
        TxtServerLogo.Text = mdownserver.ServerLogo;
        TxtServerUrl.Text = mdownserver.ServerUrl;
        DropShowType.SelectedValue = mdownserver.ShowType.ToString();
        EBtnModify.Visible = true;
        EBtnSubmit.Visible = false;
    }
    protected void EBtnModify_Click(object sender, EventArgs e)
    {
        B_DownServer bdownserver = new B_DownServer();
        int sid = Convert.ToInt32(Request.QueryString["DSId"].Trim());
        M_DownServer mdownserver = bdownserver.GetDownServerByid(sid); //new M_DownServer();
        mdownserver.ServerID = sid;//bdownserver.Max() + 1;
        mdownserver.ServerName = TxtServerName.Text.ToString();
        mdownserver.ServerLogo = TxtServerLogo.Text.ToString();
        mdownserver.ServerUrl = TxtServerUrl.Text.ToString();
        mdownserver.ShowType = Convert.ToInt32(DropShowType.SelectedValue);
        mdownserver.UrlEncrypt = Convert.ToInt32(this.Encrypttype.Text);
        mdownserver.EncryptKey = this.EncryptKey.Text;


        string readrootvaue = "";


        for (int c = 0; c < ReadRoot.Items.Count; c++)
        {
            if (ReadRoot.Items[c].Selected == true)
            {
                readrootvaue = readrootvaue + ReadRoot.Items[c].Value + ",";

            }
        }

        if (BaseClass.Right(readrootvaue, 1) == ",")
        {
            readrootvaue = BaseClass.Left(readrootvaue, readrootvaue.Length - 1);
        }
        mdownserver.ReadRoot = readrootvaue;


        if (mdownserver.Encryptime == null) { mdownserver.Encryptime = DateTime.Now; }
        if (mdownserver.Addtime == null) { mdownserver.Addtime = DateTime.Now; }
        if (this.TimeEncrypt.Checked)
        {
            mdownserver.TimeEncrypt = 1;
        }
        else
        {
            mdownserver.TimeEncrypt = 0;
        }
        mdownserver.UpTimeuti =Convert.ToInt32(this.UpTimeuti.Text);

        if (bdownserver.Update(mdownserver))
        {
            Response.Write("<script language=javascript> alert('修改成功！');window.document.location.href='DownServerManage.aspx';</script>");
        }
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        B_DownServer bdownserver = new B_DownServer();
        M_DownServer mdownserver = new M_DownServer();
        mdownserver.ServerID = bdownserver.Max() + 1;
        mdownserver.ServerName = TxtServerName.Text.ToString();
        mdownserver.ServerLogo = TxtServerLogo.Text.ToString();
        mdownserver.ServerUrl = TxtServerUrl.Text.ToString();
        mdownserver.ShowType = Convert.ToInt32(DropShowType.SelectedValue);

        mdownserver.UrlEncrypt = Convert.ToInt32(this.Encrypttype.Text);
        mdownserver.EncryptKey = this.EncryptKey.Text;
        mdownserver.Encryptime = DateTime.Now;
        mdownserver.Addtime = DateTime.Now;

        string readrootvaue = "";


        for (int c = 0; c < ReadRoot.Items.Count; c++)
        {
            if (ReadRoot.Items[c].Selected == true)
            {
                readrootvaue = readrootvaue + ReadRoot.Items[c].Value + ",";
               
            }
        }

        if (BaseClass.Right(readrootvaue, 1) == ",")
        {
            readrootvaue = BaseClass.Left(readrootvaue, readrootvaue.Length - 1);
        }

        

        mdownserver.ReadRoot = readrootvaue;

        if (this.TimeEncrypt.Checked)
        {
            mdownserver.TimeEncrypt = 1;
        }
        else
        {
            mdownserver.TimeEncrypt = 0;
        }
        mdownserver.UpTimeuti = Convert.ToInt32(this.UpTimeuti.Text);

        if (bdownserver.Add(mdownserver))
        {
            Response.Write("<script language=javascript> alert('添加成功！');window.document.location.href='DownServerManage.aspx';</script>");
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpTimeuti.Text = UpTimeutiList.SelectedValue;
    }

    protected void Encrypttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        EncrypttypeShow();
    }

    private void EncrypttypeShow()
    {
        switch (this.Encrypttype.SelectedValue)
        {
            case "1":
                this.Label2.Text = "URL常用加密方式，其地址通常是加密的迅雷专用下载地址";
                break;
            case "2":
                this.Label2.Text = "出自 IBM 的研究工作组；通常,自动取款机（Automated Teller Machine，ATM）都使用 DES";
                break;
            case "3":
                this.Label2.Text = "第一个既能用于数据加密也能用于数字签名的算法";
                break;
            case "0":
                this.Label2.Text = "";
                break;
        }
    }
}
