<%@ WebHandler Language="C#" Class="bar" %>

using System;
using System.Web;
using ZoomLa.Model;
using ZoomLa.BLL;
//仅用于贴吧
public class bar : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        Handler action = null;
        switch (context.Request["action"])
        {
            case "config":
                action = new ConfigHandler(context);
                break;
            case "uploadimage":
                action = new UploadHandler(context, new UploadConfig()
                {
                    IsBar = true,
                    SourceUrl = context.Request["SourceUrl"],
                    AllowExtensions = Config.GetStringList("imageAllowFiles"),
                    PathFormat = Config.GetString("imagePathFormat"),
                    SizeLimit = Config.GetInt("imageMaxSize"),
                    UploadFieldName = Config.GetString("imageFieldName")
                });
                break;
            default:
                break;
        }
        action.Process();
    }
    public bool IsReusable { get { return false; } }
}