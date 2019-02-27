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

public partial class manage_Plus_Author : System.Web.UI.Page
{
    private string m_FileExtArr;
    private string m_ShowPath;
    protected void Page_Load(object sender, EventArgs e)
    {    
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
        txtpic.Text=showphoto.Src = mauthor.AuthorPhoto;
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
        mauthor.AuthorPhoto = showphoto.Src.ToString();
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
        mauthor.AuthorPhoto = showphoto.Src.ToString();//this.txtpic.Text
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.m_FileExtArr = "jpg|gif|png|bmp|jpeg|swf|rar";
        string str3 = "";
        string foldername = "";
        string filename = "";
        string str2 = "";
        System.Web.UI.WebControls.FileUpload upload = (System.Web.UI.WebControls.FileUpload)this.FindControl("FileUpload1");
        StringBuilder builder2 = new StringBuilder();
        if (upload.HasFile)
        {
            str2 = Path.GetExtension(upload.FileName).ToLower();
            if (!this.CheckFilePostfix(str2.Replace(".", "")))
            {
                builder2.Append("照片" + upload.FileName + "不符合图片扩展名规则" + this.m_FileExtArr + @"\r\n");
            }
            else
            {
                if (((int)upload.FileContent.Length) > (20 * 0x400))
                {
                    builder2.Append("照片" + upload.FileName + "大小超过20" + @"KB\r\n");
                }
                else
                {
                    str3 = DataSecurity.MakeFileRndName();
                    foldername = base.Request.PhysicalApplicationPath + (VirtualPathUtility.AppendTrailingSlash("/Uploadfiles/Author")).Replace("/", "\\");// + this.FileSavePath()
                    filename = FileSystemObject.CreateFileFolder(foldername, HttpContext.Current) + str3 + str2;
                    upload.SaveAs(filename);
                    builder2.Append("照片" + upload.FileName + @"上传成功\r\n");
                    this.txtpic.Text = "UpLoadFiles/Author/" + str3 + str2;
                    showphoto.Src = "../../UpLoadFiles/Author/" + str3 + str2;
                }
            }
        }
        //if (builder2.Length > 0)
        //{
        //    Response.Write("<script language=\"javascript\" type=\"text/javascript\">alert('" + builder2.ToString() + "');/script>");
        //    Page.Response.Redirect("Advertisement.aspx");
        //    this.txtpic.Text = "UpLoadFiles/Plus/" + str3 + str2; 

        //}            
    }
    private bool CheckFilePostfix(string fileExtension)
    {
        return StringHelper.FoundCharInArr(this.m_FileExtArr.ToLower(), fileExtension.ToLower(), "|");
    } 
}
