using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class manage_WeiXin_QrCodeDecode : CustomerPageAction
{
    //private string m_FileExtArr;//扩展
    private string m_ShowPath;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "WeiXin/Default.aspx'>微信管理</a></li><li>生成二维码<a href='QrCodeManage.aspx'>[二维码管理]</a></li>");
        }
    }
    protected void CodeDecoder(string path)
    {
        QRCodeDecoder decoder = new QRCodeDecoder();
        System.IO.FileStream fs = new System.IO.FileStream(Server.MapPath(path),
        System.IO.FileMode.Open, System.IO.FileAccess.Read);//Server.MapPath(ImgCode.ImageUrl) 
        try
        {
            String decodedString = decoder.decode(new QRCodeBitmapImage(new Bitmap(System.Drawing.Image.FromStream(fs))), System.Text.Encoding.Default);
            fs.Close();
            txtEncodeData.Text = decodedString.Replace("MECARD:", "名片信息").Replace(" N:", "<br>姓名：").Replace("TEL:", "<br>电话：").Replace("EMAIL:", "<br>邮箱：").Replace("X-MSN:", "<br>MSN：").Replace("X-QQ:", "<br>QQ：").Replace(";", "").Replace("URL:", "<br>地址：");
        }
        catch (Exception)
        {
            fs.Close();
            function.WriteErrMsg("解析出错，请确认上传的图片是否为二维码!");
        }

    }
    protected string fileName = "";
    protected void BtnUpload_Click(object sender, EventArgs e)
    {
      
    }

    private bool CheckFilePostfix(string fileExtension)
    {
        //throw new Exception(this.m_FileExtArr.ToLower()+"$"+ fileExtension.ToLower());
        //return StringHelper.FoundCharInArr(this.m_FileExtArr.ToLower(), fileExtension.ToLower(), "|");
        return true;
    }

    private string FileSavePath()
    {
        string str = "";
        if (SiteConfig.SiteOption.EnableUploadFiles)
        {

            str = "/QrDecode/{$FileType}/{$Year}/{$Month}/";
            str = str.Replace("{$FileType}", Path.GetExtension(this.FupFile.FileName).ToLower().Replace(".", "")).Replace("{$Year}", DateTime.Now.Year.ToString()).Replace("{$Month}", DateTime.Now.Month.ToString()).Replace("\\", "/");
            //  this.m_ShowPath = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir) + str;
            this.m_ShowPath = str;
        }
        return str;
    }



}