using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using ZoomLa.BLL.Helper;
using ZoomLa.Model;
using System.Data;
using ZoomLa.Components;

/*
 * 暂实现通过轮徇发送
 */ 
public partial class test_MsgsSend : System.Web.UI.Page
{
    B_Admin admin = new B_Admin();
    HtmlHelper htmlHelp = new HtmlHelper();
    B_WX_APPID appbll = new B_WX_APPID();
    B_Content_Video videoBll = new B_Content_Video();
    HttpHelper httpHelper = new HttpHelper();
    WxAPI api = null;
    public int AppID { get { return DataConverter.CLng(Request.QueryString["appid"]); } }
    public string MediaID { get { return Request.QueryString["media_id"]; } }//图文素材media_id
    public string Action { get { return Request.QueryString["action"]; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        api = WxAPI.Code_Get(AppID);
        if (!IsPostBack)
        {
            CheckSend();
            WxApp_RPT.DataSource = appbll.Sel();
            WxApp_RPT.DataBind();
            string alias = " [公众号:" + api.AppId.Alias + "]";
            if (!string.IsNullOrEmpty(Action) && Action.Equals("add"))
            {
                Save_Btn.Visible = false;
                SaveNew_B.Visible = true;
            }
            if (!string.IsNullOrEmpty(MediaID))
                MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "WeiXin/WxMaterial.aspx?type=news'>素材管理</a></li><li class='active'>图文群发" + api.AppId.Alias + "</li>");
        }
    }
    //检查发送状态
    public void CheckSend()
    {
        string value= api.GetWxConfig("sendall");
        if (!string.IsNullOrEmpty(value) && DateTime.Parse(value).Day == DateTime.Now.Day)
        {
            Message_Div.Visible = true;
        }
    }
    public void MyBind()
    {
        JArray newslist = JsonConvert.DeserializeObject<JArray>(api.GetWxConfig("newsmaterial"));
        JToken news= newslist.First(j => j["media_id"].ToString().Equals(MediaID));
        News_Hid.Value = JsonConvert.SerializeObject(news);
        if (string.IsNullOrEmpty(Action))
        {
            Update_Btn.Visible = true;
            SaveNew_B.Visible = true;
            SaveNew_B.Text = "添加为新素材";
            Save_Btn.Visible = false;
        }
        Return_Li.Visible = true;
        Return_Li.Text = "<a href='WxMaterial.aspx?type=news&appid=" + AppID + "' class='btn btn-primary'>返回列表</a>";
    }
    

    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        List<M_WXImgItem> imgList = JsonConvert.DeserializeObject<List<M_WXImgItem>>(Article_Hid.Value);
        //替换img路径
        foreach (var item in imgList)
            item.Description = htmlHelp.ConvertImgUrl(item.Description, "http://" + Request.Url.Authority);
        if (!string.IsNullOrEmpty(Request.Form["appids"]))
        {
            string[] appids = Request.Form["appids"].Split(',');
            foreach (var item in appids)
            {
                WxAPI m_api = WxAPI.Code_Get(Convert.ToInt32(item));
                string result = SendImgMsg(imgList);
                Status_Li.Text += "<dd>微信公众号:" + api.AppId.Alias + ":" + result + "</dd>";
            }
        }
        else
            function.WriteErrMsg("请选择微信公众号!");
        //List<M_WXNewsItem> itemList = new List<M_WXNewsItem>();
        //foreach (var item in imgList)
        //{
        //    上传文件
        //    FileInfo file = new FileInfo(function.VToP(item.PicUrl));
        //    FileStream fs = file.Open(FileMode.Open, FileAccess.Read);
        //    string media = JsonConvert.DeserializeObject<JObject>(wxBll.UploadImg(fs, Path.GetFileName(item.PicUrl)))["media_id"].ToString();//获取mediaID
        //    添加多图文信息
        //    itemList.Add(new M_WXNewsItem() { title = item.Title, digest = "描述", thumb_media_id = media, author = B_Admin.GetLogin().UserName, content = item.Description, content_source_url = "http://demo.z01.com" });
        //}
        //JObject jobj=JsonConvert.DeserializeObject<JObject>(wxBll.UploadMPNews(itemList));
        //M_WXAllMsg model = new M_WXAllMsg() { filter = new WXFiter() { group_id = "", is_to_all = true }, msgtype = "mpnews", mpnews = new M_WXMsgMedia() { media_id = jobj["media_id"].ToString() } };
        //string result = wxBll.SendAll(model);
        //jobj = JsonConvert.DeserializeObject<JObject>(result);
        //if (jobj["errcode"].ToString().Equals("0"))
        //{
        //    wxBll.GetWxConfig("sendall", DateTime.Now.ToString());
        //    function.WriteSuccessMsg("群发成功!");
        //}
        //else if (jobj["errcode"].ToString().Equals("45028"))
        //    function.WriteErrMsg("群发失败!您的微信群发次数超过上限");
        //else
        //    function.WriteErrMsg(result);
        //M_WxImgMsg msg = new M_WxImgMsg();
        //string json = Article_Hid.Value;
        //List<M_WXImgItem> itemList = JsonConvert.DeserializeObject<List<M_WXImgItem>>(Article_Hid.Value);
        //msg.Articles = itemList;
        //wxBll.SendAllBySingle(msg);
        //function.WriteSuccessMsg("图文消息发送完成");
    }
    public string SendImgMsg(List<M_WXImgItem> imgList)
    {
        JObject jobj = JsonConvert.DeserializeObject<JObject>(UpMpNews(imgList));
       // string news_id =string.IsNullOrEmpty(MediaID)?jobj["media_id"].ToString():MediaID;
        M_WXAllMsg model = new M_WXAllMsg() { filter = new M_WXFiter() { group_id = "", is_to_all = true }, msgtype = "mpnews", mpnews = new M_WXMsgMedia() { media_id = jobj["media_id"].ToString() } };
        string result = api.SendAll(model, false);
        jobj = JsonConvert.DeserializeObject<JObject>(result);
        if (jobj["errcode"].ToString().Equals("0"))
        {
            api.GetWxConfig("sendall", DateTime.Now.ToString());
            return "群发图文成功!";
        }
        else if (jobj["errcode"].ToString().Equals("45028"))
            return "群发失败!您的微信群发次数超过上限!";
        else
            return "群发失败!失败代码:"+result;
    }
    /// <summary>
    /// 微信上传图片素材
    /// </summary>
    /// <param name="vpath"></param>
    /// <returns></returns>
    public string UpWxImg(string vpath)
    {
        FileInfo file = new FileInfo(function.VToP(vpath));
        FileStream fs = file.Open(FileMode.Open, FileAccess.Read);
        return api.UploadImg(fs, Path.GetFileName(vpath));
    }
    /// <summary>
    /// 微信上传图文素材
    /// </summary>
    /// <param name="imgList"></param>
    /// <returns></returns>
    public string UpMpNews(List<M_WXImgItem> imgList)
    {
        List<M_WXNewsItem> itemList = new List<M_WXNewsItem>();
        foreach (var item in imgList)
        {
            //上传文件
            JObject fileobj = JsonConvert.DeserializeObject<JObject>(UpWxImg(item.PicUrl));
            string media = fileobj["media_id"].ToString();//获取mediaID
            //添加多图文信息
            itemList.Add(new M_WXNewsItem() { title = item.Title, digest = "描述", thumb_media_id = media, author = B_Admin.GetLogin().UserName, content = item.Description, content_source_url = "http://demo.z01.com" });
        }
        return api.UploadMPNews(itemList);
    }
    //多图文必须有图片ID,通过群发接口发送
    protected void TestMPNews_Btn_Click(object sender, EventArgs e)
    {
        //{"media_id":"CNksuQiytlLOh3-6H8Rg3lu8Nd4Gfolve-ViA3o73-M","url":"https:\/\/mmbiz.qlogo.cn\/mmbiz\/Naic0CNHuGhAmUNEYucicibQtWxYcKmU5hCh0dJhzFiawSSyNJ0WcicFQ3ARbSUjZgvFlOkNfKDAmCEK0IvBTmnUhag\/0?wx_fmt=jpeg"}
        //FileInfo file = new FileInfo(Server.MapPath("temp.jpg"));
        //FileStream fs = file.Open(FileMode.Open, FileAccess.Read);
        //string media=wxBll.UploadImg(fs, "temp.jpg");
        //function.WriteErrMsg(media);
        //-------------获取多图文
        //List<M_WXNewsItem> itemList = new List<M_WXNewsItem>();
        //itemList.Add(new M_WXNewsItem() { title = "标题1", digest = "描述", thumb_media_id = "CNksuQiytlLOh3-6H8Rg3lu8Nd4Gfolve-ViA3o73-M", author = "123", content = "jkljklj", content_source_url = "http://demo.z01.com" });
        //itemList.Add(new M_WXNewsItem() { title = "标题2", digest = "描述2", thumb_media_id = "qI6_Ze_6PtV7svjolgs-rN6stStuHIjs9_DidOHaj0Q-mwvBelOXCFZiq2OsIU-p",author="226", content = "jkjl", content_source_url = "http://demo.z01.com" });
        //string result = wxBll.UploadMPNews(itemList); function.WriteErrMsg(result);
        //{"media_id":"CNksuQiytlLOh3-6H8Rg3gOpUqAn8IzcsK4stBKjHJ4"}  多图文
        //----发送多图文
        M_WxImgMsg msgmod = new M_WxImgMsg();
        string json = Article_Hid.Value;
        List<M_WXImgItem> imgList = JsonConvert.DeserializeObject<List<M_WXImgItem>>(json);
        msgmod.Articles = imgList;
        api.SendAllBySingle(msgmod);
        function.WriteErrMsg("发送成功!");

    }
    protected void Update_Btn_Click(object sender, EventArgs e)
    {
        JObject jobj = JsonConvert.DeserializeObject<JObject>(News_Hid.Value);
        JArray contents = JsonConvert.DeserializeObject<JArray>(jobj["content"].ToString());
        int index=Convert.ToInt32(jobj["index"]);
        string imgurl = contents[index]["thumb_media_id"].ToString();
        if (imgurl.Contains("http://") || imgurl.Contains("https://"))//是否为网络路径
            imgurl = GetImgVpath(imgurl);
        JObject fileobj= JsonConvert.DeserializeObject<JObject>(UpWxImg(imgurl));
        contents[index]["thumb_media_id"] = fileobj["media_id"].ToString();
        api.UpdateWxMaterial(MediaID, index, JsonConvert.SerializeObject(contents[index]));
        function.WriteSuccessMsg("修改成功!","WxMaterial.aspx?type=news&appid="+AppID);
    }
    public string GetImgVpath(string imgurl)
    {
        string vdir = SiteConfig.SiteOption.UploadDir;
        string imgname =function.GetRandomString(8)+".jpg";
        string str = "/Image/{$Year}/{$Month}/";
        str = str.Replace("{$Year}", DateTime.Now.Year.ToString()).Replace("{$Month}", DateTime.Now.Month.ToString()).Replace("\\", "/");
        try { httpHelper.DownFile(imgurl, vdir + str + imgname); }
        catch (Exception ex) { function.WriteErrMsg("抓取图片失败,原因:" + ex.Message); }
        return vdir+str + imgname;
    }
    protected void SaveNew_B_Click(object sender, EventArgs e)
    {
        List<M_WXImgItem> imgList = JsonConvert.DeserializeObject<List<M_WXImgItem>>(Article_Hid.Value);
        //替换img路径
        foreach (var item in imgList)
        {
            item.Description = htmlHelp.ConvertImgUrl(item.Description, "http://" + Request.Url.Authority);
            if (!string.IsNullOrEmpty(MediaID) && (item.PicUrl.Contains("https://") || item.PicUrl.Contains("http://")))//当为修改图文时判断路径是否为网络格式
                item.PicUrl = GetImgVpath(item.PicUrl);
        }
        if (!string.IsNullOrEmpty(Request.Form["appids"]))
        {
            string[] appids = Request.Form["appids"].Split(',');
            foreach (var item in appids)
            {
                WxAPI m_api = WxAPI.Code_Get(Convert.ToInt32(item));
                string result = UpMpNews(imgList);
                if (!result.Contains("errcode"))
                    function.WriteSuccessMsg("操作成功!", "WxMaterial.aspx?type=news&appid=" + AppID);
                else
                    function.WriteSuccessMsg("操作失败!原因:" + result);
            }
        }
        else
            function.WriteErrMsg("请选择微信公众号!");
    }
}
//    参数	是否必须	说明(仅群发才支持)
//Articles	是	         图文消息，一个图文消息支持1到10条图文
//thumb_media_id	是	 图文消息缩略图的media_id，可以在基础支持-上传多媒体文件接口中获得
//author	否	         图文消息的作者
//title	是	             图文消息的标题
//content_source_url 否	 在图文消息页面点击“阅读原文”后的页面
//content	是	         图文消息页面的内容，支持HTML标签
//digest	否	         图文消息的描述
//show_cover_pic	否	 是否显示封面，1为显示，0为不显示

//    {
//   "articles": [
//         {
//                        "thumb_media_id":"qI6_Ze_6PtV7svjolgs-rN6stStuHIjs9_DidOHaj0Q-mwvBelOXCFZiq2OsIU-p",
//                        "author":"xxx",
//             "title":"Happy Day",
//             "content_source_url":"www.qq.com",
//             "content":"content",
//             "digest":"digest",
//                        "show_cover_pic":"1"
//         },
//         {
//                        "thumb_media_id":"qI6_Ze_6PtV7svjolgs-rN6stStuHIjs9_DidOHaj0Q-mwvBelOXCFZiq2OsIU-p",
//                        "author":"xxx",
//             "title":"Happy Day",
//             "content_source_url":"www.qq.com",
//             "content":"content",
//             "digest":"digest",
//                        "show_cover_pic":"0"
//         }
//   ]
//}