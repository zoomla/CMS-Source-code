using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System;
using ZoomLa.BLL.Helper;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using System.Data;
using ZoomLa.BLL.Other;

namespace ZoomLa.BLL
{
    public class WxAPI
    {
        //支付等使用随机生成的nonce
        public const string nonce = "5K8264ILTKCH16CQ2502SI8ZNMTM67VS";
        private HttpContext context = HttpContext.Current;
        private readonly string apiurl = "https://api.weixin.qq.com/cgi-bin/";
        public M_WX_APPID AppId { get; private set; }
        /// <summary>
        /// 当前有效的Token如过期则会自动刷新,如需强制刷新,执行GetToken()
        /// (Token仅有一个有效,勿在其他页面重新申请Token)
        /// </summary>
        public string AccessToken
        {
            get
            {
                if (string.IsNullOrEmpty(AppId.Token))
                {
                    GetToken();
                }
                DateTime tokendate = AppId.TokenDate;
                if ((DateTime.Now - tokendate).TotalMinutes > 110)
                {
                    GetToken();
                }
                return AppId.Token;
            }
        }
        /// <summary>
        /// 用于JSAPI授权,如选择图片等功能
        /// </summary>
        public string JSAPITicket
        {
            get
            {
                if (string.IsNullOrEmpty(AppId.JSAPITicket))
                {
                    AppId.JSAPITicket = JSAPI_GetTicket(AccessToken);
                    AppId.JSAPIDate = DateTime.Now;
                }
                DateTime tokendate = AppId.JSAPIDate;
                if ((DateTime.Now - tokendate).TotalSeconds > 7100)//7200
                {
                    AppId.JSAPITicket = JSAPI_GetTicket(AccessToken);
                    AppId.JSAPIDate = DateTime.Now;
                }
                return AppId.JSAPITicket;
            }
        }
        private WxAPI(M_WX_APPID appMod) { AppId = appMod; }
        //---------------------------------------------------多公众号缓存处理
        public static List<WxAPI> WXCodeList = new List<WxAPI>();
        private static void Code_Add(WxAPI apiMod)
        {
            if (apiMod.AppId == null || string.IsNullOrEmpty(apiMod.AppId.APPID) || string.IsNullOrEmpty(apiMod.AppId.Secret)) { return; }
            //如果不存在则加入,否则更新
            WxAPI m_api = WXCodeList.FirstOrDefault(p => p.AppId.APPID.Equals(apiMod.AppId.APPID));
            if (m_api == null) { WXCodeList.Add(apiMod); }
            else
            {
                WXCodeList.Remove(m_api);
                WXCodeList.Add(apiMod);
            }
        }
        /// <summary>
        /// 如果只有一个公众号的情况下,调用此获取,仅用于具体开发时,前端页面调用
        /// </summary>
        public static WxAPI Code_Get()
        {
            M_WX_APPID appMod = new B_WX_APPID().GetDefault();
            if (appMod == null) { throw new Exception("你尚未定义公众号"); }
            return Code_Get(appMod);
        }
        public static WxAPI Code_Get(int id)
        {
            M_WX_APPID appMod = new B_WX_APPID().SelReturnModel(id);
            if (appMod == null) { throw new Exception("参数错误AppID[" + id + "]不存在"); }
            return Code_Get(appMod);
        }
        /// <summary>
        /// [main]获取目标微信的缓存,如果不存在则插入缓存
        /// 根据需要对其中的数据进行局部更新: WxAPI.Code_Get(appmod).AppId.WelStr = appmod.WelStr;
        /// </summary>
        public static WxAPI Code_Get(M_WX_APPID appMod)
        {
            if (appMod == null) { return null; }
            if (string.IsNullOrEmpty(appMod.APPID) || string.IsNullOrEmpty(appMod.Secret)) { return null; }
            WxAPI m_api = WXCodeList.FirstOrDefault(p => p.AppId.APPID.Equals(appMod.APPID) && p.AppId.Secret.Equals(appMod.Secret));
            if (m_api == null)
            {
                m_api = new WxAPI(appMod);
                string token = m_api.AccessToken;
                Code_Add(m_api);
            }
            return m_api;
        }
        //---------------------------------------------------Tools
        public static string GetMsgTypeStr(int type)
        {
            switch (type)
            {
                case 0:
                    return "文本";
                case 1:
                    return "图文";
                case 2:
                    return "视频";
                case 3:
                    return "音乐";
                default:
                    return "未知类型";
            }
        }
        public void ErroMsg(string result)
        {
            JObject obj = JsonConvert.DeserializeObject<JObject>(result);
            string errcode = obj["errcode"].ToString();
            switch (errcode)
            {
                case "40001":
                    throw new Exception("您的微信公众号[" + AppId.WxNo + "]是无效的!操作失败");
                case "48001":
                    throw new Exception("您的微信公众号[" + AppId.WxNo + "]未经过认证!操作失败");
                case "45028":
                    throw new Exception("群发失败!您的微信群发次数超过上限!");
                case "45009":
                    throw new Exception("您的微信公众号[" + AppId.WxNo + "]在执行该操作时超过使用次数上限!");
                default:
                    throw new Exception("操作失败!错误代码:" + result);
            }
        }
        //---------------------------------------------------API方法
        /// <summary>
        /// 获取最新的Token,执行该方法也会刷新当前AccessToken
        /// </summary>
        public string GetToken()
        {
            string result = APIHelper.GetWebResult(apiurl + "token?grant_type=client_credential&appid=" + AppId.APPID + "&secret=" + AppId.Secret);
            if (result.Contains("errcode")) { ErroMsg(result); }
            //{"access_token":"7EHneznPapbfKYIQISQGVw4comvbkxIWe5e7JmTkp2Y5P93aIO5FjjEeyvk65L4lcPeL6VuMOMZ7CKel95L_ljZnjZrdi-MGPK9mZZOuSN8","expires_in":7200}
            JObject obj = JsonConvert.DeserializeObject<JObject>(result);
            AppId.Token = obj["access_token"].ToString();
            AppId.TokenDate = DateTime.Now;
            return AppId.Token;
        }
        #region 消息发送
        //向用户发送信息(用于主动推送)
        public string SendMsg(string uid, string content)
        {
            string data = "{\"touser\":\"" + uid + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + content + "\"}}";
            return APIHelper.GetWebResult(apiurl + "message/custom/send?access_token=" + AccessToken, "POST", data);
        }
        /// <summary>
        /// 发送图文消息(用于主动推送)
        /// </summary>
        public string SendImgMsg(M_WxImgMsg model)
        {
            return APIHelper.GetWebResult(apiurl + "message/custom/send?access_token=" + AccessToken, "POST", model.ToJson());
        }
        /// <summary>
        /// 发送图文,
        /// </summary>
        /// <param name="mediaid">图片的素材ID</param>
        /// <returns></returns>
        public string SendImg(string uid, string mediaid)
        {
            string msg = "{\"touser\":\"" + uid + "\",\"msgtype\":\"image\",\"image\":{\"media_id\":\"" + mediaid + "\"}}";
            return APIHelper.GetWebResult(apiurl + "message/custom/send?access_token=" + AccessToken, "POST", msg);
        }
        //群发文本
        public string SendAll(string content, string gid = "")
        {
            M_WXAllMsg model = new M_WXAllMsg() { filter = new M_WXFiter() { group_id = gid, is_to_all = string.IsNullOrEmpty(gid) }, text = new WXMsgText() { content = content }, msgtype = "text" };
            //string data = JsonConvert.SerializeObject(model);
            //return GetWebResult(apiurl + "message/mass/sendall?access_token=" + AccessToken, "POST", data);
            return SendAll(model);
        }
        //图片 WXAllMsg model = new WXAllMsg() { filter = new WXFiter() { group_id = gid, is_to_all = true }, msgtype = "image", image = new WXMsgMedia() { media_id = obj["media_id"].ToString() } };
        //图文 WXAllMsg model = new WXAllMsg() { filter = new WXFiter() { group_id = gid, is_to_all = true }, msgtype = "mpnews", mpnews = new WXMsgMedia() { media_id = "" } };
        public string SendAll(M_WXAllMsg model, bool flag = true)
        {
            string result = APIHelper.GetWebResult(apiurl + "message/mass/sendall?access_token=" + AccessToken, "POST", model.ToJson());
            if (result.Contains("errcode") && flag)
                ErroMsg(result);
            return result;
        }
        //单个发送,以避开群发,人数多应该单开线程
        public string SendAllBySingle(string content)
        {
            string result = "";
            List<string> ulist = GetUserList();
            M_WxImgMsg imgMsg = new M_WxImgMsg();

            for (int i = 0; i < ulist.Count; i++)
            {
                imgMsg.ToUserName = ulist[i];
                result += SendMsg(ulist[i], content) + ",";
                System.Threading.Thread.Sleep(200);
            }
            return result;
        }
        public string SendAllBySingle(M_WxImgMsg imgMsg)
        {
            string result = "";
            List<string> ulist = GetUserList();
            for (int i = 0; i < ulist.Count; i++)
            {
                imgMsg.ToUserName = ulist[i];
                result += SendImgMsg(imgMsg) + ",";
                System.Threading.Thread.Sleep(200);
            }
            return result;
        }
        /// <summary>
        /// 上传多图文素材,订阅与服务号必须认证后才能用
        /// </summary>
        /// <returns>媒体素材ID,用于多图文上传</returns>
        public string UploadMPNews(List<M_WXNewsItem> list)
        {
            string url = apiurl + "material/add_news?access_token=" + AccessToken;
            string json = "{\"articles\": " + JsonConvert.SerializeObject(list) + "}";
            string result = APIHelper.GetWebResult(url, "POST", json);
            if (!result.Contains("\"errcode\""))
            {
                JObject obj = JsonConvert.DeserializeObject<JObject>(result);
                AddMpNewsForLocal(obj["media_id"].ToString(), JsonConvert.SerializeObject(list));
            }
            return result;
        }
        /// <summary>
        /// 在本地添加图文素材
        /// </summary>
        /// <param name="media_id"></param>
        /// <param name="subnews"></param>
        private void AddMpNewsForLocal(string media_id, string subnews)
        {
            string newsmaterial = GetWxConfig("newsmaterial");
            JArray newslist = null;
            if (string.IsNullOrEmpty(newsmaterial))
                newslist = new JArray();
            else
                newslist = JsonConvert.DeserializeObject<JArray>(newsmaterial);
            JObject newsobj = new JObject();
            newsobj.Add(new JProperty("media_id", media_id));
            newsobj.Add(new JProperty("content", subnews));
            newsobj.Add(new JProperty("saveimg", "0"));
            newsobj.Add(new JProperty("update_time", function.DateToUnix(DateTime.Now)));
            newslist.Add(newsobj);
            GetWxConfig("newsmaterial", JsonConvert.SerializeObject(newslist));
        }
        #endregion
        #region 素材管理
        /// <summary>
        /// 获取或修改微信配置项目
        /// </summary>
        public string GetWxConfig(string nodename, string value = "")
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(function.VToP("/Config/ServerInfo.config"));
            if (doc.SelectSingleNode("/Nodes/wxlog/" + AppId.APPID) == null)
            {
                CreateMaterialXml(doc);
            }
            XmlNode xmlsend = doc.SelectSingleNode("/Nodes/wxlog/" + AppId.APPID + "/" + nodename);
            if (!string.IsNullOrEmpty(value))
            {
                xmlsend.InnerText = value;
                doc.Save(function.VToP("/Config/ServerInfo.config"));
            }
            return xmlsend.InnerText;
        }
        /// <summary>
        /// 初始化素材xml结构
        /// </summary>
        /// <param name="doc"></param>
        private void CreateMaterialXml(XmlDocument doc)
        {
            XmlNode Wxlog = doc.SelectSingleNode("/Nodes/wxlog");
            XmlNode WxNo = doc.CreateElement(AppId.APPID);
            XmlNode newsmaterial = doc.CreateElement("newsmaterial");
            XmlNode othermaterial = doc.CreateElement("othermaterial");
            XmlNode sendall = doc.CreateElement("sendall");
            Wxlog.AppendChild(WxNo);
            WxNo.AppendChild(newsmaterial);
            WxNo.AppendChild(sendall);
            WxNo.AppendChild(othermaterial);
        }
        public string GetWxMaterial(string type, bool flag = true)
        {
            string data = "{\"type\":\"" + type + "\",\"offset\":0,\"count\":20}";
            string result = APIHelper.GetWebResult(apiurl + "material/batchget_material?access_token=" + AccessToken, "POST", data);
            if (result.Contains("errcode") && flag)
                ErroMsg(result);
            return result;
        }
        public string UpdateWxMaterial(string media_id, int index, string art)
        {
            string data = "{\"media_id\":\"" + media_id + "\",\"index\":" + index + ",\"articles\":" + art + "}";
            string result = APIHelper.GetWebResult(apiurl + "material/update_news?access_token=" + AccessToken, "POST", data);
            JObject jobj = JsonConvert.DeserializeObject<JObject>(result);
            if (!jobj["errcode"].ToString().Equals("0"))
                ErroMsg(result);
            UpdateMaterByLocal(media_id, index, art);
            return "OK";
        }
        /// <summary>
        /// 更新本地素材
        /// </summary>
        public void UpdateMaterByLocal(string media_id, int index, string curdata)
        {
            JArray newslist = JsonConvert.DeserializeObject<JArray>(GetWxConfig("newsmaterial"));
            JToken news = newslist.First(j => j["media_id"].ToString().Equals(media_id));
            JObject jcurdata = JsonConvert.DeserializeObject<JObject>(curdata);
            JArray contents = JsonConvert.DeserializeObject<JArray>(news["content"].ToString());
            contents[index]["title"] = jcurdata["title"];
            contents[index]["thumb_media_id"] = jcurdata["thumb_media_id"];
            contents[index]["content_source_url"] = jcurdata["content_source_url"];
            contents[index]["content"] = jcurdata["content"];
            news["content"] = JsonConvert.SerializeObject(contents);
            news["saveimg"] = "0";
            GetWxConfig("newsmaterial", JsonConvert.SerializeObject(newslist));
        }
        /// <summary>
        /// 删除微信素材
        /// </summary>
        /// <param name="media_id"></param>
        /// <returns></returns>
        public string DelWxMaterial(string media_id, string type)
        {
            string data = "{\"media_id\":\"" + media_id + "\"}";
            string result = APIHelper.GetWebResult(apiurl + "material/del_material?access_token=" + AccessToken, "POST", data);
            if (result.Contains("\"errcode\":0"))//判断是否删除成功
                DelWxMaterialByLocal(media_id, type);
            return result;
        }
        /// <summary>
        /// 删除存储在本地微信素材
        /// </summary>
        private void DelWxMaterialByLocal(string media_id, string type)
        {
            JArray jarry = JsonConvert.DeserializeObject<JArray>(GetWxConfig(type));
            jarry.Remove(jarry.First(d => d["media_id"].ToString().Equals(media_id)));
            GetWxConfig(type, JsonConvert.SerializeObject(jarry));
        }
        /// <summary>
        /// 上传永久图片素材,返回媒体ID给群发消息用
        /// </summary>
        public string UploadImg(Stream stream, string fname)
        {
            HttpHelper httpHelp = new HttpHelper();
            httpHelp.config = new HttpConfig() { IsAsync = false, fname = "media" };
            string url = apiurl + "material/add_material?access_token=" + AccessToken + "&type=image";
            HttpResult result = httpHelp.UploadFile(url, stream, fname);
            JObject imgobj = JsonConvert.DeserializeObject<JObject>(result.Html);
            if (!result.Html.Contains("\"errcode\""))
            {
                AddImgMaterialByLocal(imgobj["media_id"].ToString(), imgobj["url"].ToString());
            }
            else
            {
                ErroMsg(result.Html);
            }
            return result.Html;
        }
        /// <summary>
        /// 增加图片素材
        /// </summary>
        public void AddImgMaterialByLocal(string media_id, string url)
        {
            string othermaterial = GetWxConfig("othermaterial");
            JArray imgarray = null;
            if (string.IsNullOrEmpty(othermaterial))
                imgarray = new JArray();
            else
                imgarray = JsonConvert.DeserializeObject<JArray>(othermaterial);
            JObject imgobj = new JObject();
            imgobj.Add(new JProperty("media_id", media_id));
            imgobj.Add(new JProperty("name", Path.GetFileName(url)));
            imgobj.Add(new JProperty("url", url));
            imgobj.Add(new JProperty("update_time", 0));
            imgarray.Add(imgobj);
            GetWxConfig("othermaterial", JsonConvert.SerializeObject(imgarray));
        }
        #endregion
        #region 粉丝管理
        /// <summary>
        /// 获取微信分组
        /// </summary>
        public List<M_WxGroupInfo> GetWxGroup()
        {
            string result = APIHelper.GetWebResult(apiurl + "groups/get?access_token=" + AccessToken);
            M_WxGroup model = JsonConvert.DeserializeObject<M_WxGroup>(result);
            return model.groups;
        }
        /// <summary>
        /// 获取关注者列表
        /// </summary>
        public List<string> GetUserList()
        {
            string result = APIHelper.GetWebResult(apiurl + "user/get?access_token=" + AccessToken + "&next_openid=");
            M_WiKiUser usermodel = JsonConvert.DeserializeObject<M_WiKiUser>(result);
            return usermodel.data.openid;
        }
        /// <summary>
        /// 查询关注者信息
        /// </summary>
        public List<M_WX_User> SelAllUser()
        {
            List<M_WX_User> users = new List<M_WX_User>();
            List<string> openids = GetUserList();
            foreach (var item in openids)
            {
                users.Add(GetWxUserModel(item));
            }
            return users;
        }
        /// <summary>
        /// 获取粉丝信息模型,该接口需要公众号已认证,否则无法获取粉丝信息
        /// </summary>
        public M_WX_User GetWxUserModel(string openid)
        {
            M_WX_User userMod = new M_WX_User();
            userMod.OpenID = openid;
            string result = APIHelper.GetWebResult(apiurl + "user/info?access_token=" + AccessToken + "&openid=" + openid);
            JObject obj = JsonConvert.DeserializeObject<JObject>(result);
            try
            {
                userMod.Name = obj["nickname"].ToString();
                userMod.Sex = DataConverter.CLng(obj["sex"]);
                userMod.City = obj["city"].ToString();
                userMod.Province = obj["province"].ToString();
                userMod.Country = obj["country"].ToString();
                userMod.HeadImgUrl = obj["headimgurl"].ToString();
                userMod.Groupid = DataConverter.CLng(obj["groupid"]);
            }
            catch (Exception ex) { ZLLog.L("GetWxUserModel出错,原因:" + ex.Message + ",返回:" + result); }
            return userMod;
        }
        #endregion
        #region 菜单管理
        /// <summary>
        /// 创建微信菜单
        /// </summary>
        /// <param name="jsondata">{button:[{name:'菜单名',type:'菜单类型',key:'关键值,与推送事件匹配'},{name:菜单名,sub_button:[子菜单(格式与菜单一样)]}]}</param>
        /// <returns></returns>
        public string CreateWxMenu(string jsondata)
        {
            return APIHelper.GetWebResult(apiurl + "menu/create?access_token=" + AccessToken, "POST", jsondata);
        }
        /// <summary>
        /// 获取微信菜单
        /// </summary>
        /// <returns>{menu:{button:[{name:'菜单名',type:'菜单类型',key:'关键值,与推送事件匹配'},{name:菜单名,sub_button:[子菜单(格式与菜单一样)]}]}}</returns>
        public string GetWxMenu()
        {
            return APIHelper.GetWebResult(apiurl + "menu/get?access_token=" + AccessToken);
        }
        //删除全部微信菜单
        public string DelWxMenu()
        {
            return APIHelper.GetWebResult(apiurl + "menu/delete?access_token=" + AccessToken);
        }
        #endregion
        #region 模板消息
        /// <summary>
        /// 设置所属行业
        /// </summary>
        /// <param name="industry_id1">公众号模板消息所属行业编号</param>
        /// <param name="industry_id2">公众号模板消息所属行业编号</param>
        /// <returns></returns>
        public string Tlp_SetIndustry(int industry_id1, int industry_id2)
        {
            string data = "{ \"industry_id1\":\"" + industry_id1 + "\",\"industry_id2\":\"" + industry_id2 + "\" }";
            return APIHelper.GetWebResult(apiurl + "template/api_set_industry?access_token=" + AccessToken, "POST", data);
        }
        /// <summary>
        /// 获取设置的行业信息
        /// </summary>
        /// <returns>{"primary_industry":{"first_class":"运输与仓储","second_class":"快递"}（帐号设置的主营行业）,"secondary_industry":{"first_class":"IT科技","second_class":"互联网|电子商务"}}（帐号设置的副营行业）</returns>
        public string Tlp_GetIndustry()
        {
            return APIHelper.GetWebResult(apiurl + "template/get_industry?access_token=" + AccessToken);
        }
        /// <summary>
        /// 获得模板ID
        /// </summary>
        /// <param name="template_id_short"></param>
        /// <returns> {"errcode":0,"errmsg":"ok","template_id":"模板ID"}</returns>
        //public string AddTlp(string template_id_short)
        //{
        //    return APIHelper.GetWebResult(apiurl + "template/api_add_template?access_token=" + AccessToken, "POST", "{\"template_id_short\":\"" + template_id_short + "\"}");
        //}
        /// <summary>
        /// 获取已添加至帐号下所有模板列表
        /// </summary>
        /// <returns></returns>
        public List<M_WXTlp> Tlp_GetAllPteTlp()
        {
            //template_list
            string result = APIHelper.GetWebResult(apiurl + "template/get_all_private_template?access_token=" + AccessToken);
            JObject obj = JsonConvert.DeserializeObject<JObject>(result);
            return JsonConvert.DeserializeObject<List<M_WXTlp>>(obj["template_list"].ToString());
        }
        public M_WXTlp Tlp_GetTlpByID(string template_id)
        {
            List<M_WXTlp> tlps = Tlp_GetAllPteTlp();
            return tlps.FirstOrDefault(p => p.template_id == template_id);
        }
        /// <summary>
        /// 删除模板
        /// </summary>
        /// <returns></returns>
        public string Tlp_DelPteTlp(string template_id)
        {
            return APIHelper.GetWebResult(apiurl + "template/del_private_template?access_token=" + AccessToken, "POST", "{\"template_id\":\"" + template_id + "\"}");
        }
        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsondata">{ "touser":"OPENID","template_id":"模板ID","url":"","data":{"first": {"value":"参数值","color":"颜色"},..,remark{"value":"","color":""}}}</param>
        /// <returns></returns>
        public string Tlp_SendTlpMsg(string touser, string template_id, string url, string data)
        {
            List<M_WXTlp> tlps = Tlp_GetAllPteTlp();
            M_WXTlp tlpMod = tlps.FirstOrDefault(p => p.template_id == template_id);
            List<string> tlpParaNames = Tlp_GetTlpParaNames(tlpMod.content);
            string json = "{\"touser\":\"" + touser + "\",\"template_id\":\"" + template_id + "\",\"url\":\"" + url + "\",\"data\":" + data + "}";
            //支持序列化结构
            //在模版消息发送任务完成后，微信服务器会将是否送达成功作为通知，发送到开发者中心中填写的服务器配置地址中。
            return APIHelper.GetWebResult(apiurl + "message/template/send?access_token=" + AccessToken, "POST", json);
        }
        /// <summary>
        /// 获取指定模板内容的参数名称列表
        /// </summary>
        /// <param name="tlpContent"></param>
        /// <returns></returns>
        public List<string> Tlp_GetTlpParaNames(string tlpContent)
        {
            string[] paras = tlpContent.Split(new string[] { ".DATA" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> plist = new List<string>();
            foreach (string s in paras)
            {
                if (s.LastIndexOf("{") >= 0) { plist.Add(s.Substring(s.LastIndexOf("{") + 1)); }
            }
            return plist;
        }
        #endregion
        #region JSAPI
        /// <summary>
        /// 获取JSAPI Ticket,用于多选图片,上传图片等
        /// </summary>
        /// <param name="token">公众号的有效Token</param>
        /// <returns>授权号</returns>
        public string JSAPI_GetTicket(string token)
        {
            //{"errcode":0,"errmsg":"ok","ticket":"sM4AOVdWfPE4DxkXGEs8VEjmRiwQmjlaNL_HOyRiWg9U8Xv42jI-qEyGR-F8ap-ar7JkISzSrAl0fBFqDRzk_w","expires_in":7200}
            string url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + token + "&type=jsapi";
            string result = APIHelper.GetWebResult(url);
            JObject robj = JsonConvert.DeserializeObject<JObject>(result);
            if (robj["errcode"].ToString() == "0")
            {
                return robj["ticket"].ToString();
            }
            else { throw new Exception("获取JS授权失败,原因:" + result); }
        }
        /// <summary>
        /// 根据JSAPI Ticket生成签名,每一次使用都必须生成一次
        /// 签名用的noncestr和timestamp必须与wx.config中的nonceStr和timestamp相同。
        /// 签名用的url必须是调用JS接口页面的完整URL。
        /// 出于安全考虑，开发者必须在服务器端实现签名的逻辑。
        /// api.JSAPI_GetSign(api.JSAPITicket, noncestr, timeStamp, Request.Url.AbsoluteUri);
        /// </summary>
        /// <param name="url">http://mp.weixin.qq.com?params=value</param>
        /// <returns></returns>
        public string JSAPI_GetSign(string ticket, string noncestr, string timestamp, string url)
        {
            string str = "jsapi_ticket=" + ticket + "&noncestr=" + noncestr + "&timestamp=" + timestamp + "&url=" + url;
            return EncryptHelper.SHA1(str);
        }
        #endregion
        #region Helper
        /// <summary>
        /// 获取1970-当前的时间戮
        /// </summary>
        public static string HP_GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        /// <summary>
        /// 如处理微信浏览器,但未登录,则自动注册或登录
        /// </summary>
        public static void AutoSync(string url)
        {
            AutoSync(url, 0);
        }
        public static void AutoSync(string url, int appid)
        {
            if (DeviceHelper.GetBrower() == DeviceHelper.Brower.Micro)
            {
                B_User buser = new B_User();
                if (!buser.CheckLogin())
                {
                    WxAPI wxapi = null;
                    if (appid < 1) { wxapi = WxAPI.Code_Get(); }
                    else { wxapi = WxAPI.Code_Get(appid); }
                    string redirect_uri = SiteConfig.SiteInfo.SiteUrl + "/user/wxuser.aspx";
                    string api = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + wxapi.AppId.APPID + "&redirect_uri=" + redirect_uri + "&response_type=code&scope=snsapi_userinfo&state=" + url + "#wechat_redirect";
                    HttpContext.Current.Response.Redirect(api);
                }
            }
        }
        #endregion
    }
}