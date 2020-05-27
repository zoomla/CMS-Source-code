using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Components;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;
using System.Text;
using ZoomLa.Common;

public partial class manage_QrCode : CustomerPageAction
{
    B_QrCode Bcode = new B_QrCode();
    M_QrCode Mcode = new M_QrCode();
    B_User buser = new B_User();
    DataTable dt = new DataTable();
    B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            BtnSave.Visible = false;
            if(!string.IsNullOrEmpty(Request["ID"]))
            {
                int ID = DataConverter.CLng(Request["ID"]);
                Mcode = Bcode.SelReturnModel(ID);
                this.TxtContent.Text = Mcode.CodeContents;
                this.Level.SelectedValue = Mcode.CodeLevel;
                this.Version.SelectedValue = Mcode.CodeVersion.ToString();
                BtnSave.Visible = true;
                ShowImgs.Visible = true;
                ImgCode.ImageUrl =SiteConfig.SiteOption.UploadDir+ "/" + Mcode.ImageUrl;
                string tit = Mcode.CodeContents;
               
                if (Request["Type"] == "1")
                {
                    string[] str = tit.Split(new Char[] { ';' });
                    {
                        if (str.Length > 0)
                        {
                            //for (int i = 0; i > str.Length; i++)
                            //{

                            FN.Value = str[0].Split(new Char[] { ':' })[1];
                            TEL.Value = str[1].Split(new Char[] { ':' })[1];
                            EMAIL.Value = str[2].Split(new Char[] { ':' })[1];
                            MSN.Value = str[3].Split(new Char[] { ':' })[1];
                            QQ.Value = str[4].Split(new Char[] { ':' })[1];
                            URL.Value = str[5].Split(new Char[] { ':' })[1];
                           // }
                        }
                    }
                    tit = tit.Split(new Char[] { ';' })[0].Split(new Char[] { ':' })[2] + "的名片";
                }
                TxtZoneCode.Text = "<img src='" + SiteConfig.SiteOption.UploadDir + "/" + Mcode.ImageUrl + "' alt='" + tit + "' />";
            }
        }
		
    Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "WeiXin/Default.aspx'>微信管理</a></li><li>二维码管理<a href='QrCode.aspx'>[生成二维码]</a></li>");
     }

    protected void BtnCreate_Click(object sender, EventArgs e)
    { 
        
        if (TxtContent.Text.Trim() == String.Empty)
        {
            function.WriteErrMsg("数据不能为空");
            return;
        }
        if (TxtContent.Text.Length > 150)
        {
            function.WriteErrMsg("长度不能超过150个字符");
            return;
        }
        ImgCode.ImageUrl = null;
        this.ShowImgs.Visible = true;
        QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
        //qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.Blue;
        //qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.YellowGreen;
        qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
        try
        {
            int scale = Convert.ToInt16(TxtSize.Text);
            qrCodeEncoder.QRCodeScale = scale;
        }
        catch 
        {
            function.WriteErrMsg("无效大小");
            return;
        }
        try
        {
            int version = Convert.ToInt16(this.Version.SelectedValue);
            qrCodeEncoder.QRCodeVersion = version;
        }
        catch
        {
            function.WriteErrMsg("无效版本");
        }

        string errorCorrect = this.Level.SelectedValue;
        if (errorCorrect == "L")
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
        else if (errorCorrect == "M")
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
        else if (errorCorrect == "Q")
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
        else if (errorCorrect == "H")
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
        String data = "";
        if (this.TxtContent.Text.IndexOf("http://") > 0)
        {
            data = this.TxtContent.Text;
        }
        data = this.TxtContent.Text;
        System.Drawing.Image image = qrCodeEncoder.Encode(data, System.Text.Encoding.Default);
        try
        {
            Mcode.CodeContents = this.TxtContent.Text;
            Mcode.CodeType = "文字";
            Mcode.CodeLevel = this.Level.SelectedValue;
            Mcode.CodeVersion = DataConverter.CLng(this.Version.SelectedValue);
            Mcode.UserName = badmin.GetAdminLogin().AdminName;
            Mcode.Type = 0;
            image = SaveCode(qrCodeEncoder, Server.HtmlEncode(data), image, 0);
        }
        catch (Exception e1)
        {
            Response.Write(e1);
            return;
        }
        finally
        {
            if (image != null)
            {
                image.Dispose();
                image = null;
            }
        }
        BtnSave.Visible = true;
    }

    private System.Drawing.Image SaveCode(QRCodeEncoder qrCodeEncoder, String data, System.Drawing.Image image,int Type)
    {
        image = qrCodeEncoder.Encode(data, System.Text.Encoding.Default);
        string upclass, newtimename, filenames, filesname, indexname, tempfilename;
        upclass = Server.HtmlEncode(Request.QueryString["menu"]);
        using (Bitmap bp = new Bitmap(image))
        {
            using (MemoryStream stream = new MemoryStream())
            {
                //bp.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                filenames = "QrCode.jpg";
                if (filenames.Length > 0)
                {
                    if (filenames.IndexOf(".") > 0)
                    {
                        filesname = filenames.Substring(filenames.LastIndexOf(".")).ToLower();
                        Random indexcc = new Random();
                        indexname = Convert.ToString(indexcc.Next(9999));
                            
                        if (!Directory.Exists(Server.MapPath(SiteConfig.SiteOption.UploadDir + "/QrCode" )))
                        {
                            Directory.CreateDirectory(Server.MapPath(SiteConfig.SiteOption.UploadDir + "/QrCode"));
                        }
                        newtimename = Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Millisecond);
                        tempfilename = SiteConfig.SiteOption.UploadDir + "/QrCode/" + newtimename + indexname + filesname;
                        try
                        {
                            image.Save(Server.MapPath(tempfilename));
                        }
                        catch (Exception err)
                        {
                            Response.Write(err);
                        }
                        ImgCode.ImageUrl = tempfilename;
                        string tit = TxtContent.Text;
                        if (Type == 1)
                        {
                            tit = data;
                        }
                        TxtZoneCode.Text = "<img src='" + tempfilename + "' alt='" + tit + "' />";
                        Mcode.ImageUrl = "QrCode/" + newtimename + indexname + filesname;

                        Mcode.CreateTime = DateTime.Now;
                        if (!string.IsNullOrEmpty(Request["ID"]))
                        {
                            Mcode.ID = DataConverter.CLng(Request["ID"]);
                            if (Bcode.UpdateByID(Mcode))
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改成功');</script>");
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改失败');</script>");
                            }
                        }
                        else
                        {
                            if (Bcode.Insert(Mcode) > 0)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('创建成功');</script>");
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('创建失败');</script>");
                            }
                        }
                    }
                }
            }
        }
        return image;
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        FileDownload(Server.MapPath(this.ImgCode.ImageUrl.ToString()));
    }

    private void FileDownload(string FullFileName)
    {
        SafeSC.DownFile(function.PToV(FullFileName));
    }

    //protected void BtnOpen_Click(object sender, EventArgs e)
    //{
    //    if (string.IsNullOrEmpty(this.FileOpen.FileName))
    //    {
    //        Response.Write("<script>alert('请选择路径');</script>");
    //    }
    //    else
    //    {
    //        string fileNames = this.FileOpen.PostedFile.FileName;
    //        string pat = HttpContext.Current.Request.MapPath("~/manage/Image/");
    //        this.FileOpen.SaveAs(pat + fileNames);
    //        this.ImgCode.ImageUrl = "~/manage/Image/" + fileNames;
    //    }
    //}

    protected void BtnDecode_Click(object sender, EventArgs e)
     {
    //    QRCodeDecoder decoder = new QRCodeDecoder();
    //    System.Drawing.Image img = System.Drawing.Image.FromFile(ImgCode.ImageUrl);
    //    String decodedString = decoder.decode(new QRCodeBitmapImage(new Bitmap(img)));
    //    txtEncodeData.Text = decodedString; 
    }

    protected void BtnCreates_Click(object sender, EventArgs e)
    {
        ImgCode.ImageUrl = null;
        this.ShowImgs.Visible = true;
        QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
        //二维码颜色
        //qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.WhiteSmoke;
        //qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.YellowGreen;
        qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
        try
        {
            int scale = Convert.ToInt16(TxtSize.Text);
            qrCodeEncoder.QRCodeScale = scale;
        }
        catch 
        {
            function.WriteErrMsg("无效大小");
            return;
        }
        try
        {
            int version = Convert.ToInt16(this.Version.SelectedValue);
            qrCodeEncoder.QRCodeVersion = version;
        }
        catch 
        {
            function.WriteErrMsg("无效版本");
        }

        string errorCorrect = this.Level.SelectedValue;
        if (errorCorrect == "L")
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
        else if (errorCorrect == "M")
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
        else if (errorCorrect == "Q")
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
        else if (errorCorrect == "H")
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
        String data = "MECARD:";

            data = data + " N:" + this.FN.Value+";";


            data = data + " TEL:" + this.TEL.Value+";";


            data = data + " EMAIL:" + this.EMAIL.Value+";";
 
      
            data = data + " X-MSN:" + this.MSN.Value+";";
        
       
            data = data + " X-QQ:" + this.QQ.Value+";";

            if (this.URL.Value.IndexOf("http://") > 0)
            {
                data = data + " URL:" + this.URL.Value + ";";
            }
            else
            {
                data = data + " URL:http://" + this.URL.Value + ";";
            }
            if (data.Length > 154)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('总长度不能超过154个字符');</script>");
            }
        System.Drawing.Image image = qrCodeEncoder.Encode(Server.HtmlEncode(data), System.Text.Encoding.UTF8);
        try
        { 
            Mcode.CodeType = "名片";
            Mcode.CodeLevel = this.Level.SelectedValue;
            Mcode.CodeVersion = DataConverter.CLng(this.Version.SelectedValue);
            Mcode.UserName = badmin.GetAdminLogin().AdminName;
            Mcode.CodeContents = data; 
            Mcode.Type = 1;
            image = SaveCode(qrCodeEncoder,Server.HtmlEncode(data), image,1);
        }
        catch (Exception e1)
        {
            Response.Write(e1);
            return;
        }
        finally
        {
            if (image != null)
            {
                image.Dispose();
                image = null;
            }
        }
        BtnSave.Visible = true;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        FileDownload(Server.MapPath(this.ImgCode.ImageUrl.ToString()));
    }

    protected void BtnSavess_Click(object sender, EventArgs e)
    {
        FileDownload(Server.MapPath(this.ImgCode.ImageUrl.ToString()));
    }
    protected void Del_Click(object sender, EventArgs e)
    { } 
}