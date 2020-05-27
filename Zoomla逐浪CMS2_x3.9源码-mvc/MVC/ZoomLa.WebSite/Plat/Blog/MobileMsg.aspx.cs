using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model.Plat;
using ZoomLa.BLL.Plat;
using ZoomLa.Model;
using System.Text.RegularExpressions;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Drawing;
using System.IO;
using ZoomLa.BLL.Helper;

namespace ZoomLaCMS.Plat.Blog
{
    public partial class MobileMsg : System.Web.UI.Page
    {
        B_Blog_Msg msgBll = new B_Blog_Msg();
        B_User_Plat upBll = new B_User_Plat();
        RegexHelper regHelper = new RegexHelper();
        ImgHelper imghelper = new ImgHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                string imgstr = Request.Form["base64"];
                string result = "{\"msg\":\"上传失败\",\"imgurl\":\"\"}";
                if (!string.IsNullOrEmpty(imgstr) && imgstr.Contains("base64,"))
                {
                    string vpath = B_Plat_Common.GetDirPath(B_Plat_Common.SaveType.Blog) + function.GetRandomString(6) + ".jpg";
                    imgstr = Regex.Split(imgstr, Regex.Unescape("base64,"))[1];
                    imghelper.Base64ToImg(vpath, imgstr);
                    result = "{\"msg\":\"上传成功\",\"imgurl\":\"" + vpath + "\"}";
                }
                Response.Write(result);
                Response.Flush(); Response.End();
            }
        }
        public M_Blog_Msg FillMsg(string msg)
        {
            M_User_Plat upMod = B_User_Plat.GetLogin();
            M_Blog_Msg model = new M_Blog_Msg();
            model.MsgType = 1;
            model.Status = 1;
            model.CUser = upMod.UserID;
            model.CUName = upMod.TrueName;
            msg = Server.HtmlEncode(msg);
            msg = msg.Replace("\r\n", "<br/>");
            model.MsgContent = msg;
            model.pid = 0;
            model.ReplyID = 0;

            if (!string.IsNullOrEmpty(imgurl_hid.Value))
            {
                model.Attach = imgurl_hid.Value.Trim(',');
            }
            model.CDate = DateTime.Now;
            model.CompID = upMod.CompID;
            model.GroupIDS = "";
            return model;
        }
        protected void SaveContent_Click(object sender, EventArgs e)
        {
            M_Blog_Msg msgMod = FillMsg(Content_T.Text);
            msgBll.Insert(msgMod);
            Response.Redirect("/Plat/Blog/");
        }
    }
}