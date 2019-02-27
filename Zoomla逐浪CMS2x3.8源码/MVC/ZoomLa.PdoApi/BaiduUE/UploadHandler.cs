using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Model;
using ZoomLa.BLL.Design;

public class UploadHandler : Handler
{
    ImgHelper imghelper = new ImgHelper();
    B_GuestBookCate cateBll = new B_GuestBookCate();
    public UploadConfig UploadConfig { get; private set; }
    public UploadResult Result { get; private set; }

    public UploadHandler(HttpContext context, UploadConfig config): base(context)
    {
        this.UploadConfig = config;
        this.Result = new UploadResult() { State = UploadState.Unknown };
    }

    public override void Process()
    {
        byte[] uploadFileBytes = null;
        string uploadFileName = null;
        if (UploadConfig.Base64)
        {
            uploadFileName = UploadConfig.Base64Filename;
            uploadFileBytes = Convert.FromBase64String(Request[UploadConfig.UploadFieldName]);
        }
        else
        {
            var file = Request.Files[UploadConfig.UploadFieldName];
            uploadFileName = file.FileName;

            if (!CheckFileType(uploadFileName))
            {
                Result.State = UploadState.TypeNotAllow;
                WriteResult();
                return;
            }
            if (!CheckFileSize(file.ContentLength))
            {
                Result.State = UploadState.SizeLimitExceed;
                WriteResult();
                return;
            }

            uploadFileBytes = new byte[file.ContentLength];
            try
            {
                file.InputStream.Read(uploadFileBytes, 0, file.ContentLength);
            }
            catch (Exception)
            {
                Result.State = UploadState.NetworkError;
                WriteResult();
            }
        }

        Result.OriginFileName = uploadFileName;
      
        var savePath = PathFormatter.Format(uploadFileName, UploadConfig.PathFormat);
        var localPath = Server.MapPath(savePath);
        string vdir = function.PToV(Path.GetDirectoryName(localPath)) + "/";
        string fname = Path.GetFileName(localPath);
        if (!Directory.Exists(Path.GetDirectoryName(localPath))) { Directory.CreateDirectory(Path.GetDirectoryName(localPath)); }
        try
        {
            if (UploadConfig.IsBar)//压缩图片,水印,如访问量大,应将其生成临时表
            {
                var file=Request.Files[UploadConfig.UploadFieldName];
                M_GuestBookCate model = GetBarModel(UploadConfig);
                if (model == null) { throw new Exception("贴吧图片上传出错,model的值为空"); }
                if (model.ZipImgSize > 0 && file.ContentLength > (model.ZipImgSize * 1024))
                {
                    imghelper.CompressImg(file, model.ZipImgSize, savePath);//需改为读取版面配置
                }
                else
                {
                     ZLLog.L("here2");
                    //string fname = Path.GetFileName(localPath);
                    savePath = SafeSC.SaveFile(vdir, fname, uploadFileBytes);
                    //File.WriteAllBytes(localPath, uploadFileBytes);
                }
                savePath =ImgHelper.AddWater(savePath);
            }
            else
            {
                savePath = SafeSC.SaveFile(vdir, fname, uploadFileBytes);
                savePath = ImgHelper.AddWater(savePath);
            }
            Result.Url = savePath;
            Result.State = UploadState.Success;
        }
        catch (Exception e)
        {
            Result.State = UploadState.FileAccessError;
            Result.ErrorMessage = e.Message;
        }
        finally
        {
            WriteResult();
        }
    }
    //public string UrlRef
    //{
    //    get
    //    {
    //        string _urlRef = "";
    //        if (!string.IsNullOrEmpty(Request.QueryString["urlref"]))//多图上传
    //        {
    //            _urlRef = Request.QueryString["urlref"].ToLower();
    //        }
    //        else
    //        {
    //            _urlRef = Request.UrlReferrer == null ? "" : Request.UrlReferrer.ToString().ToLower();
    //        }
    //        return _urlRef;
    //    }
    //}
    //private bool IsBar()
    //{
    //    bool flag = false;
    //    if (!string.IsNullOrEmpty(UrlRef))
    //    {
    //        flag = (UrlRef.Contains("/pclass?") || UrlRef.Contains("/pitem?") || UrlRef.Contains("/editcontent?"));
    //    }
    //    return flag;
    //}
    private M_GuestBookCate GetBarModel(UploadConfig config)
    {
        M_GuestBookCate model = null;
        B_Guest_Bar barBll = new B_Guest_Bar();
        if (config.SourceUrl.Contains("/pclass?"))
        {
            int cateid = DataConverter.CLng(StrHelper.GetValFromUrl(config.SourceUrl, "id"));
            model = cateBll.SelReturnModel(cateid);
        }
        else
        {
            int cateid = 0;
            int postid = DataConverter.CLng(StrHelper.GetValFromUrl(config.SourceUrl, "cateid"));
            if (postid > 0)
            {
                cateid = barBll.SelCateIDByPost(postid);
            }
            else { cateid = DataConverter.CLng(StrHelper.GetValFromUrl(config.SourceUrl, "cateid")); }
            model = cateBll.SelReturnModel(cateid);
        }
        return model;
    }
    private void WriteResult()
    {
        this.WriteJson(new
        {
            state = GetStateMessage(Result.State),
            url = Result.Url,
            title = Result.OriginFileName,
            original = Result.OriginFileName,
            error = Result.ErrorMessage
        });
    }

    private string GetStateMessage(UploadState state)
    {
        switch (state)
        {
            case UploadState.Success:
                return "SUCCESS";
            case UploadState.FileAccessError:
                return "文件访问出错，请检查写入权限";
            case UploadState.SizeLimitExceed:
                return "文件大小超出服务器限制";
            case UploadState.TypeNotAllow:
                return "不允许的文件格式";
            case UploadState.NetworkError:
                return "网络错误"; 
        }
        return "未知错误";
    }

    private bool CheckFileType(string filename)
    {
        var fileExtension = Path.GetExtension(filename).ToLower();
        return UploadConfig.AllowExtensions.Select(x => x.ToLower()).Contains(fileExtension);
    }

    private bool CheckFileSize(int size)
    {
        return size < UploadConfig.SizeLimit;
    }
}
public class UploadConfig
{
    /// <summary>
    /// 文件命名规则
    /// </summary>
    public string PathFormat { get; set; }

    /// <summary>
    /// 上传表单域名称
    /// </summary>
    public string UploadFieldName { get; set; }

    /// <summary>
    /// 上传大小限制
    /// </summary>
    public int SizeLimit { get; set; }

    /// <summary>
    /// 上传允许的文件格式
    /// </summary>
    public string[] AllowExtensions { get; set; }

    /// <summary>
    /// 文件是否以 Base64 的形式上传
    /// </summary>
    public bool Base64 { get; set; }

    /// <summary>
    /// Base64 字符串所表示的文件名
    /// </summary>
    public string Base64Filename { get; set; }
    //----------------
    public bool IsBar { get; set; }
    /// <summary>
    /// design,用于区分来源,以进行不同的存储操作
    /// </summary>
    public string Plat { get; set; }
    public string SourceUrl { get; set; }
}
public class UploadResult
{
    public UploadState State { get; set; }
    public string Url { get; set; }
    public string OriginFileName { get; set; }

    public string ErrorMessage { get; set; }
}
public enum UploadState
{
    Success = 0,
    SizeLimitExceed = -1,
    TypeNotAllow = -2,
    FileAccessError = -3,
    NetworkError = -4,
    Unknown = 1,
}

