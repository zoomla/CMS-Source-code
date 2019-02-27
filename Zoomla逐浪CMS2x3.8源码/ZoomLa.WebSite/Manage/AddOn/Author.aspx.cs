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
using System.IO;
using System.Text;
using ZoomLa.Components;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;

public partial class Manage_I_Content_Author : CustomerPageAction
{
    private string m_FileExtArr="";
    //private string m_ShowPath;
    protected B_Author authorBll = new B_Author();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (function.isAjax(Request))
        {
            string flag = "1";
            string AuthorName = Request.Form["value"].Trim();
            if (string.IsNullOrEmpty(AuthorName))
            {
                Response.Write(-1);
                Response.End();
            }
            else
            {

                if (authorBll.CheckAuthorName(AuthorName, Convert.ToInt32(Request.QueryString["AUID"])))
                    flag = "1";
                else
                    flag = "0";
                Response.Write(flag);
                Response.End();
            }
        }
        if(!string.IsNullOrEmpty(Request.QueryString["AUId"]))
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='AuthorManage.aspx'>作者管理</a></li><li class='active'>修改作者</li>");
        else
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='AuthorManage.aspx'>作者管理</a></li><li class='active'>添加作者</li>");
        int DSId = 0;
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["Action"] != null)
            {
                if (Request.QueryString["Action"] == "Modify")
                {
                    DSId = Convert.ToInt32(Request.QueryString["AUId"].Trim());
                    InItModify(DSId);
                }
            }
        }
       

    }
    private void InItModify(int Auid)
    {
        B_Author bauthor = new B_Author();
        M_Author mauthor = bauthor.GetAuthorByid(Auid);
        SFile_UP.FileUrl = mauthor.AuthorPhoto;
        TxtName.Text = mauthor.AuthorName;
        RadlSex.SelectedValue = mauthor.AuthorSex.ToString();
        TxtAddress.Text = mauthor.AuthorAddress;
        TxtTel.Text = mauthor.AuthorTel;
        TxtFax.Text = mauthor.AuthorFax;
        TxtCompany.Text = mauthor.AuthorCompany;
        TxtDepartment.Text = mauthor.AuthorDepartment;
        TxtHomePage.Text = mauthor.AuthorHomePage;
        TxtZipCode.Text = mauthor.AuthorZipCode.ToString();
        TxtEmail.Text = mauthor.AuthorEmail;
        TxtIm.Text = mauthor.AuthorIm;
        SetAuthorType(mauthor.AuthorType);//设置作者类别        
        tbUBday.Value = mauthor.AuthorBirthDay.ToShortDateString();
        if (mauthor.AuthorIsElite)
            ChkElite.Checked = true;
        else
            ChkElite.Checked = false;
        if (mauthor.AuthoronTop)
            ChkOnTop.Checked = true;
        else
            ChkOnTop.Checked = false;
        TxtIntro.Text = mauthor.AuthorIntro;
        if (mauthor.AuthorPassed)
            ChkPass.Checked = true;
        EBtnModify.Visible = true;
        EBtnSubmit.Visible = false;  
    }
    private void SetAuthorType(string authortype)
    {
        switch(authortype)
        {
            case "大陆作者":
                RadlAuthorType.SelectedIndex =0;
                break;
            case "港台作者":
                RadlAuthorType.SelectedIndex = 1;
                break;
            case "海外作者":
                RadlAuthorType.SelectedIndex = 2;
                break;
            case "本站特约":
                RadlAuthorType.SelectedIndex = 3;
                break;
            case "其他作者":
                RadlAuthorType.SelectedIndex = 4;
                break;
            default:
                RadlAuthorType.SelectedIndex = 5;
                break;
        }        
    }
    private string GetAuthorType(int authortype)
    {
        string AT = "";
        switch (authortype)
        {
            case 0:
                AT = "大陆作者";
                break;
            case 1:
                AT = "港台作者";
                break;
            case 2:
                AT = "海外作者";
                break;
            case 3:
               AT="本站特约";
                break;
            case 4:
                AT="其他作者";
                break;
            default:
                AT = "大陆作者";
                break;
        }
        return AT;
    }
    protected void EBtnModify_Click(object sender, EventArgs e)
    {
        M_Author mauthor = new M_Author();
        B_Author bauthor = new B_Author();
        mauthor.ID= Convert.ToInt32(Request.QueryString["AUId"].Trim());
        mauthor.AuthorID =0;
        mauthor.AuthorName = TxtName.Text.ToString();
        mauthor.AuthorPhoto = UpPhontoFile();
        //mauthor. = TxtUserName.Text.ToString(); 会员名没存
        mauthor.AuthorSex = Convert.ToInt32(RadlSex.SelectedValue);
        mauthor.AuthorLastUseTime = DateTime.Now;
        mauthor.AuthorAddress = TxtAddress.Text.ToString();
        mauthor.AuthorTel = TxtTel.Text.ToString();
        mauthor.AuthorFax = TxtFax.Text.ToString();
        mauthor.AuthorCompany = TxtCompany.Text.ToString();
        mauthor.AuthorDepartment = TxtDepartment.Text.ToString();
        mauthor.AuthorHomePage = TxtHomePage.Text.ToString();
        mauthor.AuthorZipCode = DataConverter.CLng(TxtZipCode.Text.ToString());
        mauthor.AuthorEmail = TxtEmail.Text.ToString();
        mauthor.AuthorMail = TxtMail.Text.ToString();
        mauthor.AuthorIm = TxtIm.Text.ToString();
        mauthor.AuthorType = GetAuthorType(RadlAuthorType.SelectedIndex);
        mauthor.AuthorBirthDay = DataConverter.CDate(tbUBday.Value);
        if (ChkElite.Checked)
        {
            mauthor.AuthorIsElite = true;
        }
        if (ChkOnTop.Checked)
        {
            mauthor.AuthoronTop = true;
        }
        mauthor.AuthorIntro = TxtIntro.Text.ToString();
        if (ChkPass.Checked)
        {
            mauthor.AuthorPassed = true;
        }
        if (bauthor.Update_Author_ByID(mauthor))
            Response.Write("<script language=javascript> alert('修改成功！');window.document.location.href='AuthorManage.aspx';</script>");
          
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        M_Author mauthor = new M_Author();
        B_Author bauthor=new B_Author();
        mauthor.AuthorID = 0;
        mauthor.AuthorName= TxtName.Text.ToString();
        mauthor.AuthorPhoto = UpPhontoFile();
        //mauthor. = TxtUserName.Text.ToString(); 会员名没存
        mauthor.AuthorSex = Convert.ToInt32(RadlSex.SelectedValue); 
        mauthor.AuthorLastUseTime = DateTime.Now;
        mauthor.AuthorAddress = TxtAddress.Text.ToString();
        mauthor.AuthorTel = TxtTel.Text.ToString();
        mauthor.AuthorFax = TxtFax.Text.ToString();
        mauthor.AuthorCompany = TxtCompany.Text.ToString();
        mauthor.AuthorDepartment = TxtDepartment.Text.ToString();
        mauthor.AuthorHomePage = TxtHomePage.Text.ToString();
        mauthor.AuthorZipCode = DataConverter.CLng(TxtZipCode.Text.ToString());
        mauthor.AuthorEmail = TxtEmail.Text.ToString();
        mauthor.AuthorMail = TxtMail.Text.ToString();
        mauthor.AuthorIm = TxtIm.Text.ToString();
        mauthor.AuthorType = GetAuthorType(RadlAuthorType.SelectedIndex);
        mauthor.AuthorBirthDay = DataConverter.CDate(tbUBday.Value);
        if (ChkElite.Checked)
        {
            mauthor.AuthorIsElite = true;// 1;
        }
        if (ChkOnTop.Checked)
        {
            mauthor.AuthoronTop = true;//1;
        }
        mauthor.AuthorIntro = TxtIntro.Text.ToString();
        if (ChkPass.Checked)
        {
            mauthor.AuthorPassed = true;//1;
        }
        if (bauthor.Add(mauthor))
            Response.Write("<script language=javascript> alert('添加成功！');window.document.location.href='AuthorManage.aspx';</script>");
            
    }
    public string UpPhontoFile()
    {
        string foldername = "";
        string filename = "";
        foldername = base.Request.PhysicalApplicationPath + (VirtualPathUtility.AppendTrailingSlash("/" + SiteConfig.SiteOption.UploadDir + "/Author")).Replace("/", "\\");// + this.FileSavePath()
        filename = FileSystemObject.CreateFileFolder(foldername, HttpContext.Current);
        SFile_UP.SaveUrl = function.PToV(filename);
        return SFile_UP.SaveFile();
    }
    private bool CheckFilePostfix(string fileExtension)
    {
        return StringHelper.FoundCharInArr(this.m_FileExtArr.ToLower(), fileExtension.ToLower(), "|");
    } 
}
