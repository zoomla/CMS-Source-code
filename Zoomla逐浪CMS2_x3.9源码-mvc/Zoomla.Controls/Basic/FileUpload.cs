using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using ZoomLa.Safe;
using System.IO;

namespace Controls
{
    public class FileUpload : System.Web.UI.WebControls.FileUpload
    {
        [DefaultValue(false), Localizable(true), Bindable(true), Category(""), Description("是否只允许上传图片")]
        public bool OnlyImg
        {
            get
            {
                if(ViewState["OnlyImg"]==null)
                {
                    ViewState["OnlyImg"]=false;
                }
                return (bool)ViewState["OnlyImg"];
            }
            set { ViewState["OnlyImg"] = value; }
        }
        //[DefaultValue(false), Localizable(true), Bindable(true), Category(""), Description("是否只允许上传Word与Excel等格式")]
        //public bool OnlyOffice
        //{
        //    get
        //    {
        //        return (bool)ViewState[this.UniqueID + "OnlyOffice"];
        //    }
        //    set { ViewState[this.UniqueID + "OnlyOffice"] = value; }
        //}
        [DefaultValue(""), Localizable(true), Bindable(true), Category(""), Description("允许文件后缀名")]
        public string AllowExt
        {
            get
            {
                if (ViewState["AllowExt"] == null) { ViewState["AllowExt"] = ""; }
                return ViewState["AllowExt"].ToString();
            }
            set { ViewState["AllowExt"] = value; }
        }
        public string ErrorMsg = "";
        public new bool SaveAs(string fileName)
        {
            fileName = (fileName ?? "").Replace(" ", "");
            if (!HasFile) { ErrorMsg = "未指定需要上传的文件"; return false; }
            if (FileContent.Length < 100) { ErrorMsg = "请勿上传空文件"; return false; }
            if (string.IsNullOrEmpty(fileName)) { ErrorMsg = "保存路径不能为空"; return false; }
            if (string.IsNullOrEmpty(Path.GetExtension(fileName))) { ErrorMsg = "文件后缀名不能为空"; return false; }
            if (fileName.Contains(":")) { ErrorMsg = "保存路径不正确,请使用虚拟路径"; return false; }
            if (OnlyImg)
            {
                if (!SafeC.IsImage(fileName)) { ErrorMsg = "仅允许png,jpg,gif等Web图片格式"; return false; }
            }
            else if (!string.IsNullOrEmpty(AllowExt))//后缀名检测
            {
                string ext = Path.GetExtension(fileName).ToLower().Replace(".", "");
                if (!AllowExt.ToLower().Split(',').Contains(ext)) { ErrorMsg = "仅允许" + AllowExt + "后缀名的文件"; return false; }
            }
            SafeC.SaveFile(fileName, this.PostedFile);
            return true;
        }
    }
}
