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
using ZoomLa.Common;
using ZoomLa.Model;
using System.IO;
using System.Text;

public partial class Manage_I_Content_AddSource : CustomerPageAction
{
    private string m_FileExtArr;
    //private string m_ShowPath;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!string.IsNullOrEmpty(Request.QueryString["SID"]))
             Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='SourceManage.aspx'>来源管理</a></li><li class='active'>修改来源</li>");
        else
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='SourceManage.aspx'>来源管理</a></li><li class='active'>添加来源</li>");
        int SId = 0;
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["Action"] != null)
            {
                if (Request.QueryString["Action"] == "Modify")
                {
                    SId = Convert.ToInt32(Request.QueryString["SId"].Trim());
                    InItModify(SId);
                }
            }
        }
    }
    private void InItModify(int Sid)
    {
        B_Source bsource = new B_Source();      
        M_Source msource = bsource.GetSourceByid(Sid);
        TxtName.Text = msource.Name;
        TxtAddress.Text = msource.Address;
        TxtTel.Text = msource.Tel;
        TxtFax.Text = msource.Fax;
        TxtContacterName.Text = msource.Contacter;
        TxtHomePage.Text = msource.HomePage;
        SFileUp_File.FileUrl = msource.Photo;
        TxtZipCode.Text = msource.ZipCode.ToString();
        TxtEmail.Text = msource.Email;
        TxtMail.Text = msource.Mail;
        TxtIm.Text = msource.Im;
        TxtType.Text = "";//来源的分类是否为绑定?
        if (msource.IsElite)
            ChkElite.Checked = true;
        else
            ChkElite.Checked = false;
        if (msource.onTop)
        {
            ChkOnTop.Checked = true;
        }
        else
        {
            ChkOnTop.Checked = false;
        }
        ChkPass.Text = msource.Intro;
        if (msource.Passed)
        {
            ChkPass.Checked = true;
        }
        else
        {
            ChkPass.Checked = false;
        }
        EBtnModify.Visible = true;
        EBtnSubmit.Visible = false;
    }
   
    public string UpFiles()
    {
        this.m_FileExtArr = "jpg|gif|png|bmp|jpeg|swf|rar";
        string foldername = "";
        string filename = "";
        foldername = base.Request.PhysicalApplicationPath + (VirtualPathUtility.AppendTrailingSlash("/Uploadfiles/Source")).Replace("/", "\\");// + this.FileSavePath()
        filename = FileSystemObject.CreateFileFolder(foldername, HttpContext.Current);
        SFileUp_File.SaveUrl = function.PToV(filename);
        return SFileUp_File.SaveFile();
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        M_Source msource = new M_Source();
        B_Source bsource=new B_Source();
        msource.Name = TxtName.Text.ToString();
        msource.Address = TxtAddress.Text.ToString();
        msource.Tel = TxtTel.Text.ToString();
        msource.Fax = TxtFax.Text.ToString();
        msource.Contacter = TxtContacterName.Text.ToString();
        msource.HomePage=TxtHomePage.Text.ToString();
        msource.Photo = UpFiles(); //""
        msource.ZipCode = DataConverter.CLng(TxtZipCode.Text);
        msource.Email= TxtEmail.Text.ToString();
        msource.Mail = TxtMail.Text.ToString();
        msource.Im = TxtIm.Text.ToString();
        msource.LastUseTime = DateTime.Now;
        msource.Type = TxtType.Text.ToString();
        if (ChkElite.Checked)
        {
            msource.IsElite = true;
        }
        else
        {
             msource.IsElite=false;
        }

        if (ChkOnTop.Checked)
        {
            msource.onTop = true;
        }
        else
        {
            msource.onTop = false;
        }
       
        msource.Intro = ChkPass.Text.ToString();
        if (ChkPass.Checked)
        {
            msource.Passed = true;
        }
        else
        {
            msource.Passed = false;
        }
        if(bsource.Add(msource))
        {
            Response.Write("<script language=javascript> alert('添加成功！');window.document.location.href='SourceManage.aspx';</script>");
         }
    }
    private bool CheckFilePostfix(string fileExtension)
    {
        return StringHelper.FoundCharInArr(this.m_FileExtArr.ToLower(), fileExtension.ToLower(), "|");
    }
    //添加来源
    protected void EBtnModify_Click(object sender, EventArgs e)
    {
        M_Source msource = new M_Source();
        B_Source bsource = new B_Source();
        msource.SourceID = Convert.ToInt32(Request.QueryString["SId"].Trim());
        msource.Name = TxtName.Text.ToString();
        msource.Address = TxtAddress.Text.ToString();
        msource.Tel = TxtTel.Text.ToString();
        msource.Fax = TxtFax.Text.ToString();
        msource.Contacter = TxtContacterName.Text.ToString();
        msource.HomePage = TxtHomePage.Text.ToString();
        msource.Photo = UpFiles();///App_Themes/AdminDefaultTheme/images/default.gif
        msource.ZipCode = DataConverter.CLng(TxtZipCode.Text);
        msource.Email = TxtEmail.Text.ToString();
        msource.Mail = TxtMail.Text.ToString();
        msource.Im = TxtIm.Text.ToString();
        msource.LastUseTime = DateTime.Now;
        msource.Type = TxtType.Text.ToString();
        if (ChkElite.Checked)
        {
            msource.IsElite = true;
        }
        else
        {
            msource.IsElite = false;
        }

        if (ChkOnTop.Checked)
        {
            msource.onTop = true;
        }
        else
        {
            msource.onTop = false;
        }

        msource.Intro = ChkPass.Text.ToString();
        if (ChkPass.Checked)
        {
            msource.Passed = true;
        }
        else
        {
            msource.Passed = false;
        }
        if (bsource.Update(msource))
        {
            Response.Write("<script language=javascript> alert('修改成功！');window.document.location.href='SourceManage.aspx';</script>");
        }
    }
}
