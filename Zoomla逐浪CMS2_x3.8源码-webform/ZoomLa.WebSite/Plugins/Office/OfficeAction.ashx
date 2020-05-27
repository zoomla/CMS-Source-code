<%@ WebHandler Language="C#" Class="OfficeAction" %>

using System;
using System.Web;
using System.Web.SessionState;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
public class OfficeAction : IHttpHandler, IRequiresSessionState
{
    OACommon oacom = new OACommon();
    B_OA_Document oaBll = new B_OA_Document();
    B_User buser = new B_User();
    private int AppID { get { return DataConverter.CLng(HttpContext.Current.Request.QueryString["AppID"]); } }//AppID
    private string FName { get { return HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["fname"] ?? ""); } }//仅传文件名
    public void ProcessRequest(HttpContext context)
    {
        HttpRequest Request = context.Request;
        HttpResponse Response = context.Response;
        string action = Request["action"];
        switch (action)
        {
            case "getword"://返回Word二进制文件
                {
                    Response.Clear();
                    Response.ContentType = "Application/msword";
                    string fname = HttpUtility.UrlDecode(Request["fname"]);
                    int appid = DataConverter.CLng(Request["appid"]);
                    string vpath = "";
                    if (appid > 0)
                    {
                        M_OA_Document oaMod = oaBll.SelReturnModel(appid);
                        M_UserInfo mu = buser.SelReturnModel(oaMod.UserID);
                        vpath = oacom.GetMyDir(mu) +  HttpUtility.UrlDecode(oaMod.PrivateAttach);
                    }
                    else
                    {
                        M_UserInfo mu = buser.GetLogin();
                        vpath = oacom.GetMyDir(mu) + fname;
                    }
                    Response.BinaryWrite(SafeSC.ReadFileByte(vpath));   //读取二进制的文件
                    Response.Flush(); Response.End();
                }
                break;
            case "saveword":
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFile file = Request.Files[0];
                        if (AppID > 0)
                        {
                            M_OA_Document oaMod = oaBll.SelReturnModel(AppID);
                            M_UserInfo mu = buser.SelReturnModel(oaMod.UserID);
                            SafeSC.SaveFile(oacom.GetMyDir(mu), file, FName);
                        }
                        else//第一次创建
                        {
                            HttpContext curReq = HttpContext.Current;
                            if (curReq.Request.Cookies["UserState"] != null)
                            {

                                string loginName = curReq.Request.Cookies["UserState"]["LoginName"], password = curReq.Request.Cookies["UserState"]["Password"];
                                ZLLog.L("SaveWord:"+loginName + ":" + password);
                            }
                            else { ZLLog.L("SaveWord:empty"); }
                            M_UserInfo mu = buser.GetLogin();
                            SafeSC.SaveFile(oacom.GetMyDir(mu), file, FName);
                        }
                        Response.Write("true");
                    }
                    else
                    {
                        Response.Write("No File Upload!");
                    } 
                }
                break;
        }
    }

    public bool IsReusable { get { return false; } }
}