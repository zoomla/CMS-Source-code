using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

/*
 * 功能:用于处理批量上传的Word文件,/Edit/Edit.aspx发送
 */

namespace ZoomLaCMS.Edit
{
    public partial class batUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(Request.QueryString["case"]))//是需要批量上传.我们先判断有无该目录，如无，则建，然后再保存
            {
                string myCase = Server.UrlDecode(Request.QueryString["case"]);
                SafeSC.CreateDir("~/uploadFiles/DocTemp/", myCase);
                string path = Server.MapPath("~/uploadFiles/DocTemp/" + myCase + "/");
                //目录建好了，然后我们开始存文档
                Response.Clear();
                //ID为文档的主键，如果ID不为空，则更新数据，否则新建一条记录
                string ID = Request.Params["ID"];
                string DocTitle, content;
                DocTitle = "test";
                if (!string.IsNullOrEmpty(ID))
                {
                    DocTitle = Server.UrlDecode(Request.Params["DocTitle"]);
                }
                DocTitle = Server.UrlDecode(Request.Params["DocTitle"]);
                content = Server.UrlDecode(Request.Params["content"]);
                if (Request.Files.Count > 0)
                {
                    HttpPostedFile upPhoto = Request.Files[0];
                    int upPhotoLength = upPhoto.ContentLength;
                    byte[] PhotoArray = new Byte[upPhotoLength];
                    Stream PhotoStream = upPhoto.InputStream;
                    PhotoStream.Read(PhotoArray, 0, upPhotoLength); //这些编码是把文件转换成二进制的文件
                    if (DocTitle.ToLower().Contains(".aspx") || DocTitle.ToLower().Contains(".exe")) { return; }
                    SafeSC.SaveFile(path, DocTitle, PhotoArray);
                }
                Response.ContentType = "text/plain";
                Response.Write("Complete"); Response.Flush(); Response.End();
            }
        }
    }
}