<%@ WebHandler Language="C#" Class="cr" %>

using System;
using System.Web;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.BLL.Content;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Third;
using ZoomLa.Components;
using ZoomLa.SQLDAL;
using ZoomLa.Model;
using ZoomLa.Model.Content;
using ZoomLa.Model.Third;
using ZoomLa.PdoApi.CopyRight;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZoomLa.Common;
public class cr : API_Base, IHttpHandler
{
    B_Content conBll = new B_Content();
    B_Content_CR crBll = new B_Content_CR();
    C_CopyRight crAPI = null;
    public void ProcessRequest(HttpContext context)
    {

        M_AdminInfo adminMod = B_Admin.GetLogin();
        retMod.retcode = M_APIResult.Failed;
        //retMod.callback = CallBack;//暂不开放JsonP
        M_Third_PlatInfo infoMod = B_Third_PlatInfo.SelByFlag("版权印");
        if (infoMod == null || string.IsNullOrEmpty(infoMod.APPKey) || string.IsNullOrEmpty(infoMod.APPSecret)) { retMod.retmsg = "未配置版权印接口";RepToClient(retMod); }
        crAPI = new C_CopyRight();
        try
        {
            int gid = DataConvert.CLng(Req("gid"));
            switch (Action)
            {
                case "add":
                    {
                        M_CommonData conMod = conBll.SelReturnModel(gid);
                        if (conMod == null) { retMod.retmsg = "未指定内容!"; break; }
                        else
                        {
                            JObject obj = Add(conMod);
                            if (DataConvert.CLng(obj["value"]) == 1)
                            {
                                retMod.result = obj["data"].ToString();
                                retMod.retcode = M_APIResult.Success;
                            }
                            else { retMod.retmsg = obj["msg"].ToString(); }
                        }
                    }
                    break;
                case "del":
                    {
                        M_Content_CR crMod = crBll.SelByGid(gid);
                        if (crMod == null) { retMod.retmsg = "该内容未生成版权印"; break; }
                        string delResult = crAPI.Remove(crMod.WorksID);
                        JObject delObj = JsonConvert.DeserializeObject<JObject>(delResult);
                        if (DataConvert.CLng(delObj["value"]) != 1) { retMod.retmsg = "删除失败,返回" + delResult; }
                        else {
                            crBll.Del(crMod.ID);
                            retMod.retcode = M_APIResult.Success;
                        }
                    }
                    break;
                case "readd":
                    {
                        //删除
                        M_Content_CR crMod = crBll.SelByGid(gid);
                        if (crMod == null) { retMod.retmsg = "该内容未生成版权印"; break; }
                        string delResult = crAPI.Remove(crMod.WorksID);
                        JObject delObj = JsonConvert.DeserializeObject<JObject>(delResult);
                        if (DataConvert.CLng(delObj["value"]) != 1) { retMod.retmsg = "删除失败,返回" + delResult; break; }
                        else { crBll.Del(crMod.ID); }
                        //重新添加
                        M_CommonData conMod = conBll.SelReturnModel(gid);
                        JObject addObj = Add(conMod);
                        if (DataConvert.CLng(addObj["value"]) == 1)
                        {
                            retMod.result = addObj["data"].ToString();
                            retMod.retcode = M_APIResult.Success;
                        }
                        else { retMod.retmsg = addObj["msg"].ToString(); }
                    }
                    break;
                case "get":
                    {
                        M_Content_CR crMod = crBll.SelByGid(gid);
                        if (crMod != null) { retMod.retcode = M_APIResult.Success; retMod.result = JsonConvert.SerializeObject(crMod); }
                    }
                    break;
                default:
                    retMod.retmsg = "[" + Action + "]接口不存在";
                    break;
            }
        }
        catch (Exception ex) { retMod.retmsg = ex.Message; }
        RepToClient(retMod);
    }
    /// <summary>
    /// 添加版权
    /// </summary>
    /// <param name="conMod"></param>
    /// <returns></returns>
    public JObject Add(M_CommonData conMod)
    {
        CheckHasRule();
        double repprice = DataConvert.CDouble(Req("repprice"));
        double matprice = DataConvert.CDouble(Req("matprice"));
        string content = "";
        DataTable conDT = DBCenter.Sel(conMod.TableName, "ID=" + conMod.ItemID);
        if (conDT.Rows.Count > 0 && conDT.Columns.Contains("content")) { content = StringHelper.StripHtml(conDT.Rows[0]["content"].ToString()); }
        M_Content_CR crMod = crBll.CreateFromContent(conMod, content, repprice, matprice);
        string result = crAPI.Create(crMod);
        JObject obj = JsonConvert.DeserializeObject<JObject>(result);
        crMod.Status = DataConvert.CLng(obj["value"]);
        crMod.WorksID = obj["data"].ToString();
        crBll.Insert(crMod);
        return obj;
    }
    public bool IsReusable { get { return false; } }
    public void CheckHasRule()
    {
        string result = crAPI.GetRule(SiteConfig.SiteInfo.SiteName);
        try
        {
            JObject obj = JsonConvert.DeserializeObject<JObject>(result);
            if (obj["data"] == null || obj["data"]["rules"] == null || obj["data"]["rules"].ToString().Equals("[]"))
            {
                crAPI.SetDefPriceRule();
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message+"::"+ result); }
    }
}