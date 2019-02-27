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
using ZoomLa.BLL;//using ZLCollecte.Common;
using ZoomLa.Common;
using ZoomLa.Model;
using System.IO;
using System.Text;

public partial class manage_Plus_AddSource : System.Web.UI.Page
{
    private string m_FileExtArr;
    private string m_ShowPath;
    protected void Page_Load(object sender, EventArgs e)
    {
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
        txtpic.Text = msource.Photo;
        showphoto.Src ="../../"+msource.Photo;
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
                    foldername = base.Request.PhysicalApplicationPath + (VirtualPathUtility.AppendTrailingSlash("/Uploadfiles/Source")).Replace("/", "\\");// + this.FileSavePath()
                    filename = FileSystemObject.CreateFileFolder(foldername, HttpContext.Current) + str3 + str2;
                    upload.SaveAs(filename);
                    builder2.Append("照片" + upload.FileName + @"上传成功\r\n");
                    this.txtpic.Text = "UpLoadFiles/Source/" + str3 + str2;
                    showphoto.Src = "../../UpLoadFiles/Source/" + str3 + str2;
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
        msource.Photo = txtpic.Text.ToString();//../../manage/Images/default.gif
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
        msource.Photo = txtpic.Text.ToString();//../../manage/Images/default.gif
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
