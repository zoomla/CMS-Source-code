using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;

namespace ZoomLaCMS.Plat.Note.Diag
{
    public partial class cutpic : System.Web.UI.Page
    {
        ImgHelper imghelp = new ImgHelper();
        private string savePath = "/UploadFiles/User/cutpic/";
        public string ImgPath { get { return GetImgPath(IPath); } }
        public string IPath { get { return Request.QueryString["ipath"] ?? ""; } }
        public double ImgPercent { get { return Convert.ToDouble(ViewState["ImgPercent"]); } set { ViewState["ImgPercent"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                string result = "";
                try
                {
                    string action = Request["action"];
                    string vpath = ImgPath;
                    string warning = "";
                    double percent = Convert.ToDouble(Request["percent"]);
                    if (action.Equals("crop"))
                    {
                        double x1 = double.Parse(Request.Form["x1"]);
                        double y1 = double.Parse(Request.Form["y1"]);
                        double width = double.Parse(Request.Form["width"]);
                        double height = double.Parse(Request.Form["height"]);
                        x1 = x1 / percent; y1 = y1 / percent;
                        width = width / percent; height = height / percent;
                        savePath += (function.GetRandomString(4) + Path.GetFileName(vpath));
                        result = ImageDealLib.imgcrop(vpath, savePath, (int)x1, (int)y1, (int)width, (int)height, ImageDealLib.FileCache.Save, out warning);
                    }
                }
                catch (Exception ex) { result = ex.Message; }
                Response.Clear(); Response.Write(result); Response.Flush(); Response.End();
            }
            if (!IsPostBack)
            {
                if (!Directory.Exists(Server.MapPath(savePath))) { Directory.CreateDirectory(Server.MapPath(savePath)); }
                if (string.IsNullOrEmpty(IPath)) { function.WriteErrMsg("未指定图片路径"); }
                using (FileStream fs = new FileStream(Server.MapPath(ImgPath), FileMode.Open, FileAccess.Read))
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(fs);
                    //1,首先将图片强制压缩为760的宽
                    //2,将比率交与前端,前端等比缩小裁剪框(1920*650)
                    if (img.Width < 1920 || img.Height < 650) { function.Script(this, "minImgAlert();"); }
                    ImgPercent = (double)760 / (double)img.Width;
                    int width = (int)(img.Width * ImgPercent);
                    int height = (int)(img.Height * ImgPercent);
                    img.Dispose();
                    Bitmap bmp = imghelp.ZoomImg(ImgPath, height, width);
                    string vpath = savePath + function.GetRandomString(6) + ".jpg";
                    imghelp.SaveImg(vpath, bmp);
                    photo_img.Src = vpath;
                }
            }
        }
        //只允许处理/uploadfiles/user/目录下的图片
        private string GetImgPath(string url)
        {
            return SafeSC.PathDeal("/UploadFiles/User/" + IPath.ToLower().Replace("/uploadfiles/user/", ""));
        }
    }
}