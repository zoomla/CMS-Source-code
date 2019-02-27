using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;

public partial class Manage_I_ASCX_SingelFileUp : System.Web.UI.UserControl
{
    public string FName { get { return Path.GetFileName(FileUrl); } }
    private string[] exname = ".doc,.docx,.xls,.xlsx".Split(',');
    public enum FileType
    {
        Img,Office,All
    }
    //保存后的路径
    [Browsable(true)]//FileUrl
    public string FileUrl 
    {
        set { ViewState["FileUP_FileUrl"] = value; }
        get { return ViewState["FileUP_FileUrl"] == null ? "" : ViewState["FileUP_FileUrl"].ToString(); }
    }
    //选择文件后的本地路径|上传后的虚拟路径
    [Browsable(false)]
    public string FVPath { get { return FVPath_T.Text; } set { FVPath_T.Text = value; } }
    //用户下的保存的路径(以/结尾),未指定则用默认路径
    [Browsable(false)]
    public string SaveUrl
    {
        get
        {
            if (ViewState["FileUP_SaveUrl"] == null)
            {
                ViewState["FileUP_SaveUrl"] = "/UploadFiles/Images/SingleF/";
            }
            return ViewState["FileUP_SaveUrl"].ToString();
        }
        set { ViewState["FileUP_SaveUrl"] = value; }
    }
    [Browsable(true)]   //img,office,all
    public FileType FType 
    {
        get { return ViewState["FileUP_FType"]==null?FileType.All:(FileType)Enum.Parse(typeof(FileType),ViewState["FileUP_FType"].ToString()); } 
        set { ViewState["FileUP_FType"] = value; } 
    }
    [Browsable(true)]
    public bool IsNull
    {
        get { return ViewState["FileUp_IsNull"] == null ? false : (bool)ViewState["FileUp_IsNull"]; }
        set { ViewState["FileUp_IsNull"] = value; }
    }
    //是否启用等比例压缩(只适用于图片)
    [Browsable(true)]
    public bool IsCompress
    {
        get
        {
            if (ViewState["FileUP_IsCompress"] == null)
            {
                ViewState["FileUP_IsCompress"] = false;
            }
            return Convert.ToBoolean(ViewState["FileUP_IsCompress"].ToString());
        }
        set { ViewState["FileUP_IsCompress"] = value; }
    }
    [Browsable(true)]
    public int MaxWidth
    {
        get
        {
            if (ViewState["FileUP_MaxWidth"] == null)
            {
                ViewState["FileUP_MaxWidth"] = 0;
            }
            return Convert.ToInt32(ViewState["FileUP_MaxWidth"]);
        }
        set { ViewState["FileUP_MaxWidth"] = value; }
    }
     [Browsable(true)]
    public int MaxHeight
    {
        get
        {
            if (ViewState["FileUP_MaxHeight"] == null)
            {
                ViewState["FileUP_MaxHeight"] = 0;
            }
            return Convert.ToInt32(ViewState["FileUP_MaxHeight"]);
        }
        set { ViewState["FileUP_MaxHeight"] = value; }
    }
    /// <summary>
    /// 是否以真实文件名存储(默认否)
    /// </summary>
    [Browsable(true)]
    public bool IsRelName
    {
        get
        {
            if (ViewState["FileUP_IsReName"] == null)
            {
                ViewState["FileUP_IsReName"] = false;
            }
            return Convert.ToBoolean(ViewState["FileUP_IsReName"].ToString());
        }
        set { ViewState["FileUP_IsReName"] = value; }
    }
    /// <summary>
    /// 是否加载JS等资源,如果一个页面有多个上传控件的话,除第一个其他为False
    /// </summary>
     [Browsable(true)]
    public bool LoadRes
    {
        get
        {
            if (ViewState["FileUP_LoadRes"] == null)
            {
                ViewState["FileUP_LoadRes"] = true;
            }
            return Convert.ToBoolean(ViewState["FileUP_LoadRes"].ToString());
        }
        set { ViewState["FileUP_LoadRes"] = value; }
    }
    public bool HasFile { get { return FileUp_File.HasFile; } }
    //----------------------------------------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            switch (FType)
            {
                case FileType.Img:
                    FileUp_File.Attributes.Add("accept", "image/x-png,image/gif,image/jpeg");
                    break;
                case FileType.Office:
                    FileUp_File.Attributes.Add("accept", "application/msexcel,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    break;
                case FileType.All:
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(FileUrl) && string.IsNullOrEmpty(FVPath)) { FVPath = FileUrl; }
        }
    }
    public string SaveFile()
    {
        //if (string.IsNullOrEmpty(FVPath_T.Text) && !IsNull) { function.WriteErrMsg("请选择上传文件!"); }
        //如未指定上传文件,则返回FVPath中的值,便于清空与拷贝地址进入
        if (!FileUp_File.HasFile) { return FVPath; }
        string filename = filename = FileUp_File.FileName;
        if (!IsRelName) { filename = DateTime.Now.ToString("yyyyMMddHHmmss") + function.GetRandomString(4) + Path.GetExtension(FileUp_File.FileName); }
        switch (FType)
        {
            case FileType.Img:
                {
                    if (!SafeSC.IsImage(FileUp_File.FileName)) { function.WriteErrMsg(Path.GetExtension(FileUp_File.FileName) + "不是有效的图片格式!"); }
                    ImgHelper imghelp = new ImgHelper();
                    //if (IsCompress)//压缩与最大比只能有一个生效
                    //{
                    //    imghelp.CompressImg(FileUp_File.PostedFile, 1000, vpath);
                    //}
                    bool hasSave = false;
                    if (MaxWidth > 0 || MaxHeight > 0)
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromStream(FileUp_File.PostedFile.InputStream);
                        img = imghelp.ZoomImg(img, MaxHeight, MaxWidth);
                        FileUrl = ImgHelper.SaveImage(SaveUrl + filename, img);
                        hasSave = true;
                    }
                    if (!hasSave) { FileUrl = SafeSC.SaveFile(SaveUrl, FileUp_File, filename); }
                }
                break;
            case FileType.Office:
                {
                    if (!exname.Contains(Path.GetExtension(FileUp_File.FileName))) { function.WriteErrMsg("必须上传doc|docx|xls|xlsx格式的文件!"); }
                    FileUrl = SafeSC.SaveFile(SaveUrl, FileUp_File, filename);
                }
                break;
            default:
                {
                    FileUrl = SafeSC.SaveFile(SaveUrl, FileUp_File, filename);
                }
                break;
        }
        //FileUrl = SaveUrl + filename;
        //if (!Directory.Exists(function.VToP(SaveUrl))) { SafeSC.CreateDir(SaveUrl); }
        //if (!FileUp_File.SaveAs(FileUrl)) { function.WriteErrMsg(FileUp_File.ErrorMsg); }
        return FileUrl;
    }
}