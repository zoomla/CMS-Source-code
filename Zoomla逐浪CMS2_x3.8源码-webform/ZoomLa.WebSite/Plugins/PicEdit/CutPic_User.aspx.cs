using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Safe;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.BLL.Helper;
using Newtonsoft.Json.Linq;

public partial class Plugins_PicEdit_CutPic_User : System.Web.UI.Page
{
    //临时路径,关闭或保存时,将该目录移除
    private string savePath = "/UploadFiles/Pic/Cutpic/";
    private ImgHelper imgHelper = new ImgHelper();
    /// <summary>
    /// 初始显示截图窗与宽高
    /// </summary>
    public string CutWL { get { return Request.QueryString["cutwl"] ?? ""; } }
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin();
        if (function.isAjax())
        {
            string action = Request.Form["action"], vpath = Request.Form["vpath"], result = "", warning = "";//动作,需要加水印的图片路径
            if (vpath.Contains("?"))
            {
                vpath = vpath.Substring(0, vpath.IndexOf("?"));
            }
            switch (action)
            {
                case "crop"://根据提交的图片路径与宽度等参数,完成剪切
                    {
                        int x1 = int.Parse(Request["x1"]);
                        int y1 = int.Parse(Request["y1"]);
                        int width = int.Parse(Request["width"]);
                        int height = int.Parse(Request["height"]);
                        savePath += (function.GetRandomString(4) + Path.GetFileName(vpath));
                        result = ImageDealLib.imgcrop(vpath, savePath, x1, y1, width, height, ImageDealLib.FileCache.Save, out warning);
                    }
                    break;
                case "rotate":
                    {
                        int angle = DataConverter.CLng(Request.Form["angle"]);
                        Bitmap bmp = imgHelper.Rotate(vpath, angle);
                        savePath += function.GetRandomString(6) + Path.GetExtension(vpath);
                        result = imgHelper.SaveImg(savePath, bmp);
                        bmp.Dispose();
                    }
                    break;
                case "zoom"://缩放
                    {
                        int width = int.Parse(Request.Form["width"]);
                        int height = int.Parse(Request.Form["height"]);
                        savePath += (function.GetRandomString(4) + Path.GetFileName(vpath));
                        Bitmap bmp = imgHelper.ZoomImg(vpath, height, width);
                        result = imgHelper.SaveImg(savePath, bmp);
                    }
                    break;
                case "fontwater"://文字水印
                    {
                        System.Drawing.Image fontimg = AddFontWater(savePath + function.GetRandomString(6) + ".jpg");
                        savePath += "fontw_" + Path.GetFileName(vpath);
                        result = ImageDealLib.makewatermark(vpath, fontimg, GetWaterType(Request.Form["pos"]), savePath, ImageDealLib.ImageType.JPEG, ImageDealLib.FileCache.Save, out warning);
                        result += "?" + function.GetRandomString(6);
                    }
                    break;
                case "imgwater"://水印图片路径
                    {
                        string watervpath = Request.Form["watervpath"];
                        int trans = DataConverter.CLng(Request.Form["trans"]);
                        savePath += "imgw_" + Path.GetFileName(vpath);
                        result = ImageDealLib.makewatermark(vpath, watervpath, GetWaterType(Request.Form["pos"]), savePath, ImageDealLib.ImageType.JPEG, ImageDealLib.FileCache.Save, out warning, trans);
                        result += "?" + function.GetRandomString(6);
                    }
                    break;
            }
            Response.Write(result); Response.Flush(); Response.End();
        }
        if (!IsPostBack)
        {
            M_UserInfo mu=buser.GetLogin();
            string ipath = Request.QueryString["ipath"].ToLower().Replace("/uploadfiles/user/","");
            string UserPath = "/UploadFiles/User/" + mu.UserName + mu.UserID + "/" + ipath.TrimStart('/') ;//不做过多的限制,如需要文件安全,则使用单独的文件服务器
            if (string.IsNullOrEmpty(UserPath) || !SafeC.IsImage(UserPath)) { function.WriteErrMsg("只允许编辑图片文件!"); }
            if (!Directory.Exists(Server.MapPath(savePath))) Directory.CreateDirectory(Server.MapPath(savePath));
            if (!File.Exists(Server.MapPath(UserPath))) { function.WriteErrMsg("需要修改的图片" + UserPath + "不存在"); }
            NowImg_Hid.Value = SourceImg_Hid.Value = UserPath;
            System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath(UserPath));
            ImgWidth_Hid.Value = img.Width.ToString();
            ImgHeight_Hid.Value = img.Height.ToString();
            RPT.DataSource = GetWaterImgDT();
            RPT.DataBind();
            img.Dispose();
        }
    }
    public System.Drawing.Image AddFontWater(string vpath)
    {
        //string json = "{text:\"水印文字\",family: \"Arial\", size: \"18pt\", weight: \"bold\", style: \"normal\", decoration: \"line-through\",color:\"rgb(255, 51, 153)\", background: \"rgb(204,153,102)\" }";
        string json = Request.Form["fontmodel"];
        FontModel model = JsonConvert.DeserializeObject<FontModel>(json);
        return ImageDealLib.ConverFontToImg(vpath, model);
    }
    public ImageDealLib.WaterType GetWaterType(string pos)
    {
        return (ImageDealLib.WaterType)Enum.ToObject(typeof(ImageDealLib.WaterType), DataConverter.CLng(pos));
    }
    //暂为手动生成,后期改为数据库获取
    public DataTable GetWaterImgDT()
    {
        //function.WriteErrMsg(WaterModuleConfig.WaterConfig.WaterImgs);
        List<JObject> list = JsonConvert.DeserializeObject<List<JObject>>(WaterModuleConfig.WaterConfig.WaterImgs);
        //string[] imgs = "/Images/admin_logo.jpg|/Images/logo.png|/Images/baidulogo.gif".Split('|');
        DataTable dt = FileSystemObject.GetDTFormat2();
        for (int i = 0; i < list.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["Name"] = Path.GetFileName(list[i]["url"].ToString());
            dr["Path"] = list[i]["url"].ToString();
            dt.Rows.Add(dr);
        }
        return dt;
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        //原图资源有时未释放,需要查看
        if (NowImg_Hid.Value.Equals(SourceImg_Hid.Value)) { function.WriteErrMsg("请修改后再保存"); }
        byte[] img = SafeC.ReadFileByte(NowImg_Hid.Value);
        SafeC.SaveFile(SourceImg_Hid.Value, Path.GetFileName(SourceImg_Hid.Value), img);
        function.Script(this, "AfterSave();");
    }
}
