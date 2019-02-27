using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.IO;
using System.Net;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZoomLa.Components;

public partial class Manage_WeiXin_WxMaterial : CustomerPageAction
{
    WxAPI api = null;
    B_WX_APPID appBll = new B_WX_APPID();
    public int AppId 
    {
        get { return ViewState["AppID"]==null?DataConverter.CLng(Request.QueryString["appid"]):DataConverter.CLng(ViewState["AppID"]); }
        set { ViewState["AppID"] = value; }
    }
    public string Type { get { return Request.QueryString["type"]; } }
    public string singletlp = "<div class='info'>{0}<br />{1}<img src='{2}' /><span class='gray_9'>{3}</span></div>";//单图文标题模板";
    public string newsTitletlp = "<div class='info'>{0}<img src='{1}' /><div class='margin_t5 title'>{2}</div></div>";//多图文标题模板
    public string newsItemTlp = "<div class='container-fluid sub_info'><div>{0}</div><div><img src='{1}' /></div></div>";//多图文副图文模板
    public string NodeName//微信缓存
    {
        get { return Type.Equals("news") ? "newsmaterial" : "othermaterial"; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string result = "", action = Request.Form["action"];
            int appid = DataConverter.CLng(Request.Form["appid"]);
            api = WxAPI.Code_Get(appid);
            switch (action)
            {
                case "del":
                    result = api.DelWxMaterial(Request.Form["media_id"], Request.Form["type"]);
                    break;
                default:
                    break;
            }
            Response.Write(result); Response.Flush(); Response.End();
        }
        if (AppId > 0) { api = WxAPI.Code_Get(AppId); }
        if (!IsPostBack)
        {
            DataTable dt = appBll.Sel();
            WxApp_RPT.DataSource = dt;
            WxApp_RPT.DataBind();
            Datas_Hid.Value = GetConfigData(dt);
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "WeiXin/WxAppManage.aspx'>公众号管理</a></li><li class='active'>素材管理 [<a href='MsgsSend.aspx?action=add&appid=" + AppId + "'>添加素材</a>]</li>");
        }
    }
    //获取图片与图文数据
    public string GetConfigData(DataTable dt)
    {
        if (AppId > 0)
        {
            return GetMeterialData();
        }
        foreach (DataRow item in dt.Rows)
        {
            api = WxAPI.Code_Get(Convert.ToInt32(item["ID"]));
            string result = GetMeterialData();
            if (result.Contains("errcode"))
                continue;
            AppId = api.AppId.ID;
            return result;
        }
        function.WriteErrMsg("请添加有效的微信公众号!");
        return "";
    }
    public string GetPreUrl()
    {
        string encodeurl = Server.UrlEncode("http://"+Request.Url.Authority+"/"+ZoomLa.Components.SiteConfig.SiteOption.ManageDir+"/WeiXin/PreWxMsg.aspx?appid="+AppId +"&media_id="+Eval("media_id"));
        return "http://app.z01.com/Class_2/NodePage.aspx?url=" + encodeurl + "&t=m";
    }
    public string GetMeterialData()
    {
        string result = api.GetWxConfig(NodeName);
        if (string.IsNullOrEmpty(result))
            result = api.GetWxMaterial(Type, AppId > 0);
        return result;
    }
    public void MyBind()
    {
        Items_value.Value = SaveImgUrl(Items_value.Value);
        api.GetWxConfig(NodeName, Items_value.Value);
        DataTable dt = JsonHelper.JsonToDT(Items_value.Value);
        if (Type.Equals("news"))
        {
            NewsRPT.DataSource = dt;
            dt.DefaultView.Sort = "update_time DESC";
            NewsRPT.DataBind();
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["isnew"]))
        {
            Response.Redirect("WxMaterial.aspx?type=news&appid=" + AppId);
        }
        else
        {
            OtherRPT.DataSource = dt;
            OtherRPT.DataBind();
        }
    }
    //同步多图文图片url
    public string SaveImgUrl(string json)
    {
        if (Type.Equals("news"))
        {
            JArray images = JsonConvert.DeserializeObject<JArray>(api.GetWxConfig("othermaterial"));//获取所有图片素材
            if (images == null || images.Count <= 0)
                Response.Redirect("WxMaterial.aspx?type=image&appid="+AppId+"&isnew=1");
            JArray news = JsonConvert.DeserializeObject<JArray>(json);
            for (int i = 0; i < news.Count; i++)
            {
                if (news[i]["saveimg"].ToString().Equals("0"))//没有同步图片
                {
                    JArray sub_news = JsonConvert.DeserializeObject<JArray>(news[i]["content"].ToString());//得到多图文数组
                    for (int j = 0; j < sub_news.Count; j++)
                    {
                        string media_id = sub_news[j]["thumb_media_id"].ToString();
                        if (images.Count(v => v["media_id"].ToString().Equals(media_id)) > 0)//匹配图片素材
                        {
                            JToken image = images.First(v => v["media_id"].ToString().Equals(media_id));
                            sub_news[j]["thumb_media_id"] = image["url"].ToString();
                        }
                        else
                        {
                            sub_news[j]["thumb_media_id"] = "/Images/nopic.gif";
                        }
                    }
                    news[i]["content"] = JsonConvert.SerializeObject(sub_news);
                    news[i]["saveimg"] = "1";
                }
            }
            return JsonConvert.SerializeObject(news);
        }
       
        return json;
    }
    public string GetNewsInfo()
    {
        return "";
    }
    protected void ImgFile_B_Click(object sender, EventArgs e)
    { 
        string result = api.UploadImg(ImgFile_Up.PostedFile.InputStream,ImgFile_Up.FileName);
        if (!result.Contains("\"errcode\""))
        {
            function.WriteSuccessMsg("上传图片成功!",Request.RawUrl);
        }
        function.WriteErrMsg("上传失败!错误吗:" + result);
    }
    
    protected void BindData_B_Click(object sender, EventArgs e)
    {
        MyBind();
    }
    protected void NewsRPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView drv = e.Item.DataItem as DataRowView;
        if (drv != null)
        {
            Literal lit = e.Item.FindControl("Infos_Li") as Literal;
            DataTable dt = JsonHelper.JsonToDT(drv["content"].ToString());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)//首个使用多图文标题模板
                {
                    if (dt.Rows.Count > 1)//有多个图文
                        lit.Text += string.Format(newsTitletlp, GetDateTime(drv["update_time"].ToString()), dt.Rows[i]["thumb_media_id"], dt.Rows[i]["title"]);
                    else
                        lit.Text += string.Format(singletlp, dt.Rows[i]["title"], GetDateTime(drv["update_time"].ToString()), dt.Rows[i]["thumb_media_id"], dt.Rows[i]["digest"]);
                }
                else
                {
                    lit.Text += string.Format(newsItemTlp, dt.Rows[i]["title"], dt.Rows[i]["thumb_media_id"]);
                }
            }
        }
    }
    //根据时间戳获取时间格式
    public string GetDateTime(string unixtime)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(unixtime + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        DateTime now= dtStart.Add(toNow);
        return now.ToString();
    }
}