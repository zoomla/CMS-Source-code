<%@ WebHandler Language="C#" Class="Common" %>

using System;
using System.Web;
using System.IO;
using ThoughtWorks.QRCode.Codec;
using System.Drawing;
using System.Drawing.Imaging;
public class Common : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        if (!string.IsNullOrEmpty(context.Request["url"]))//生成url二维码
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;//大小
            qrCodeEncoder.QRCodeVersion = 0;//版本
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;//纠错级别
            string data = context.Request["url"];
            System.Drawing.Image image = qrCodeEncoder.Encode(data, System.Text.Encoding.UTF8);
            MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/Gif";
            HttpContext.Current.Response.BinaryWrite(stream.ToArray());
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}