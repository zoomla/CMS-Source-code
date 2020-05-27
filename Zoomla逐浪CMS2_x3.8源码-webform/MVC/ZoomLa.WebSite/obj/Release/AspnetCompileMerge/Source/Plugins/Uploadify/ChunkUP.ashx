<%@ WebHandler Language="C#" Class="ChunkUP" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Linq;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.FTP;
using ZoomLa.SQLDAL;
/*
 * 分片上传,用于需要较大文件上传的功能
 * var myFromData = { upmode: "0", guid: "<%=Guid.NewGuid().ToString()%>" };//传递给服务端的数据
 *  threads: 3,
 *  chunked: true,
 *  chunkSize: 20 * 1024 * 1024,//iis默认为30M
 */

public class ChunkUP : IHttpHandler,IRequiresSessionState
{
    B_User buser = new B_User();
    B_Admin badmin = new B_Admin();
    ChunkHelper chkHelp = null;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Request.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
        context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
        string guid = context.Request["guid"];
        if (string.IsNullOrEmpty(guid)) { throw new Exception("未上传Guid"); }
        if (!buser.CheckLogin() && !badmin.CheckLogin()) { throw new Exception("未登录"); }
        HttpPostedFile file = context.Request.Files["Filedata"];
        if (file == null)
            file = context.Request.Files["file"];//接受Uploadify或WebUploader传参,优先Uploadify
        if (file.ContentLength < 1) { return; }
        if (SafeSC.FileNameCheck(file.FileName))
        {
            throw new Exception("不允许上传该后缀名的文件");
        }
        string action = context.Request.Params["action"];
        string uploadPath = "/UploadFiles/ChunkFile/";

        /*-------------------------------------------------------------------------------*/
        try
        {
            if (context.Session[guid] != null)
            {
                chkHelp = (ChunkHelper)context.Session[guid];
                chkHelp.context = HttpContext.Current;
                string result=chkHelp.ChunkUploader(file);

                //如果完成的话,清除
                if (chkHelp.IsComplete)
                {
                    if (action.Equals("UPVideo"))//后台视频上传
                    {
                        UpVideo(function.PToV(result),file);
                    }
                    ZLLog.L(ZLEnum.Log.fileup, new M_Log()
                    {
                        UName = "",
                        Source = context.Request.RawUrl,
                        Message = "UPVideo_分片上传成功|文件名:" + file.FileName + "|" + "保存路径:" + result
                    });
                    context.Session[guid] = null;

                }
                return;
            }
            else
            {
                switch (action)
                {
                    case "UPVideo"://上传视频(管理员)
                        if (!badmin.CheckLogin()) { throw new Exception("UPVideo,管理员未登录"); }
                        uploadPath = SiteConfig.SiteOption.UploadDir + "/Video/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                        break;
                }
                chkHelp = new ChunkHelper(HttpContext.Current);
                chkHelp.SetPath(uploadPath, Path.GetFileName(file.FileName));
                context.Session[guid] = chkHelp;
                string result= chkHelp.ChunkUploader(file);
                if (string.IsNullOrEmpty(context.Request.Form["chunks"]))
                {
                    UpVideo(result, file);
                }
            }
        }
        catch (Exception ex)
        {
            ZLLog.L(ZLEnum.Log.fileup, new M_Log()
            {
                UName = "",
                Source = context.Request.RawUrl,
                Message = "分片上传失败|文件名:" + file.FileName + "|" + "原因:" + ex.Message
            });
        }
    }
    //视频上传
    public void UpVideo(string result,HttpPostedFile file)
    {

        B_Content_Video videoBll = new B_Content_Video();
        ZoomLa.Model.FTP.M_FtpConfig ftpMod = new ZoomLa.BLL.FTP.B_FTP().SelFirstModel();
        string uploadPath = SiteConfig.SiteOption.UploadDir + "/Video/" + DateTime.Now.ToString("yyyyMMdd") + "/";
        if (SiteConfig.SiteOption.OpenFTP.Equals("2") && ftpMod.ID > 0)
        {
            AsynchronousFtpUpLoader ftphelp = new AsynchronousFtpUpLoader();
            string ftpresult = "http://" + ftpMod.FtpServer + "/" + Path.GetFileName(file.FileName);
            M_Content_Video videoMod = new M_Content_Video()
            {
                VName = Path.GetFileName(result),
                VTime = "",
                VPath = ftpresult,
                Thumbnail = "",
                CDate = DateTime.Now
            };
            ftphelp.UpLoad(ftpMod.FtpServer + ":" + ftpMod.FtpPort, ftpMod.FtpUsername, ftpMod.FtpPassword, function.VToP(result));
            videoBll.Insert(videoMod);
        }
        else
        {
            VideoFile model = null;
            string imgvpath = uploadPath + Path.GetFileNameWithoutExtension(result) + ".jpg";//预览图
            try
            {
                VideoHelper conver = new VideoHelper(function.VToP("/Tools/ffmpeg.exe"), "/Temp/Video/");
                model = conver.GetVideoInfo(function.VToP(result));
                conver.CutImgFromVideo(result, imgvpath);
            }
            catch { imgvpath = ""; }
            M_Content_Video videoMod = new M_Content_Video()
            {
                VName = Path.GetFileName(result),
                VTime = (model == null ? "" : model.Duration.ToString()),
                VPath = result,
                Thumbnail = imgvpath,
                CDate = DateTime.Now
            };
            videoBll.Insert(videoMod);
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}
public class ChunkHelper
{
    private HttpResponse Response { get { return context.Response; } }
    private HttpRequest Request { get { return context.Request; } }
    public HttpContext context { get; set; }
    public bool IsComplete = false;
    private string guid { get; set; }
    private string vdir { get; set; }
    private string fname { get; set; }
    /// <summary>
    /// 构造,传入文件所在的上下文与路径
    /// </summary>
    /// <param name="vpath">虚拟路径+文件名</param>
    public ChunkHelper(HttpContext context)
    {
        this.context = context;
    }
    public void SetPath(string vpath)
    {
        this.vdir = Path.GetDirectoryName(vpath).Replace("\\", "/") + "/";
        this.fname = Path.GetFileName(vpath);
    }
    public void SetPath(string vdir, string fname)
    {
        this.vdir = vdir;
        this.fname = fname;
    }
    /// <summary>
    ///处理分片上传,如未分片,则直接保存(后期置入内存)
    /// </summary>
    /// <returns>返回文件保存的虚拟路径,为空则保存失败</returns>
    public string ChunkUploader(HttpPostedFile file)
    {
        string result = "";
        if (Request.Form.AllKeys.Any(m => m == "chunk"))
        {
            this.guid = Request["guid"];
            nowCount++;
            int curChunk = Convert.ToInt32(Request.Form["chunk"]);//这次请求的碎片index
            int chunks = Convert.ToInt32(Request.Form["chunks"]);//共有多少chunks
            {
                string guidName = guid + "_" + curChunk;
                string vpath = SafeSC.SaveFile(vdir, file, guidName);
            }
            //如果是最后一个分片，则整合
            if (nowCount == chunks)
            {
                result = UnitChunk(this.guid, chunks);
            }
        }
        else//没有分片直接保存
        {
            result = SafeSC.SaveFile(vdir, file, fname);
        }
        return result;
    }
    //整合分片文件(后期根据需要分片整合,或内存整合)
    private string UnitChunk(string guid, int chunk)
    {
        string vpath = function.VToP(vdir + fname);
        FileStream addFile = new FileStream(vpath, FileMode.Append, FileAccess.Write);
        BinaryWriter AddWriter = new BinaryWriter(addFile);
        try
        {
            for (int i = 0; i <= chunk; i++)
            {
                string guidpath = function.VToP(vdir + guid + "_" + i);
                Stream stream = new FileStream(guidpath, FileMode.Open);
                BinaryReader TempReader = new BinaryReader(stream);
                AddWriter.Write(TempReader.ReadBytes((int)stream.Length));
                stream.Dispose();
                TempReader.Dispose();
                File.Delete(guidpath);
            }
            return vpath;
        }
        catch { return vpath; }
        finally
        {
            AddWriter.Dispose();
            addFile.Dispose();
            IsComplete = true;
        }
    }
    //-----------
    //总计已处理多少分片
    private int nowCount = 0;
}