using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
//微信消息模型与微信普通模型
namespace ZoomLa.BLL
{
    //--------------------------------------微信模型
    /// <summary>
    /// 微信分组
    /// </summary>
    public class M_WxGroup
    {
        public List<M_WxGroupInfo> groups = new List<M_WxGroupInfo>();
    }
    /// <summary>
    /// 微信组信息
    /// </summary>
    public class M_WxGroupInfo
    {
        public int id;
        public string name;
        public int count;
    }
    //获取微信用户模型(openid)
    public class M_WiKiUser
    {
        public int total;
        public int count;
        public string next_openid;
        public M_WiKiData data = new M_WiKiData();
    }
    public class M_WiKiData
    {
        public List<string> openid = new List<string>();
    }
    public class WXMsgText//删
    {
        public string content;
    }
    public class M_WXMsgMedia
    {
        public string media_id;
    }
    //new WXMsgMedia();
    public class M_WXFiter
    {
        public bool is_to_all = false;
        public string group_id;
    }
    /*-------------------------消息模型------------------------------------------*/
    /// <summary>
    /// 微信信息基类,支持转Json(提交服务端)与XML(服务端返回)
    /// </summary>
    public abstract class M_WxBaseMsg
    {
        public string ToUserName;
        public string FromUserName;//OPenID,用于绑定用户等
        public long CreateTime;
        public string MsgType;
        public string Content;
        public M_WxBaseMsg() { CreateTime = DateTime.Now.Millisecond; }
        public M_WxBaseMsg(string xml)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            this.MsgType = xdoc.SelectSingleNode("/xml/MsgType").InnerText;
            this.ToUserName = xdoc.SelectSingleNode("/xml/ToUserName").InnerText;
            this.FromUserName = xdoc.SelectSingleNode("/xml/FromUserName").InnerText;
            this.CreateTime = Convert.ToInt64(xdoc.SelectSingleNode("/xml/CreateTime").InnerText);
            if (xdoc.SelectSingleNode("/xml/Content") != null)
            { this.Content = xdoc.SelectSingleNode("/xml/Content").InnerText; }
        }
    }
    //菜单跳转时推送
    //    <xml>
    //<ToUserName><![CDATA[toUser]]></ToUserName>
    //<FromUserName><![CDATA[FromUser]]></FromUserName>
    //<CreateTime>123456789</CreateTime>
    //<MsgType><![CDATA[event]]></MsgType>
    //<Event><![CDATA[VIEW]]></Event>
    //<EventKey><![CDATA[www.qq.com]]></EventKey>
    //</xml>
    /// <summary>
    /// 自定义菜单事件消息模型&文本消息模型
    /// </summary>
    public class M_WxTextMsg : M_WxBaseMsg
    {
        public int MsgId;
        public string Event;
        public string EventKey;
        public M_WxTextMsg() { }
        public M_WxTextMsg(string xml)
            : base(xml)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            if (this.MsgType.Equals("event"))
            {
                this.Event = xdoc.SelectSingleNode("/xml/Event").InnerText;
                XmlNode node = xdoc.SelectSingleNode("/xml/EventKey");
                if (node != null)
                    this.EventKey = node.InnerText;
            }
        }
        //服务端接收消息后被动回复
        public string ToXML()
        {
            string xml = "<xml>"
                        + "<ToUserName><![CDATA[{0}]]></ToUserName>"
                        + "<FromUserName><![CDATA[{1}]]></FromUserName>"
                        + "<CreateTime>{3}</CreateTime>"
                        + "<MsgType><![CDATA[text]]></MsgType>"
                        + "<Content><![CDATA[{2}]]></Content></xml>";
            return string.Format(xml, ToUserName, FromUserName, Content, CreateTime);
        }

        public string ToJson()
        {
            JsonSerializerSettings setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented, setting);
        }
    }
    // 图文消息
    public class M_WxImgMsg : M_WxBaseMsg
    {
        public int ArticleCount { get { return Articles.Count; } }
        public List<M_WXImgItem> Articles = new List<M_WXImgItem>();
        public M_WxImgMsg() { }
        public M_WxImgMsg(string xml) : base(xml) { }
        //用于回复
        public string ToXML()
        {
            string xml = " <xml>"
            + "<ToUserName><![CDATA[" + ToUserName + "]]></ToUserName>"
            + "<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>"
            + "<CreateTime>12345678</CreateTime>"
            + "<MsgType><![CDATA[news]]></MsgType>"
            + "<ArticleCount>" + Articles.Count + "</ArticleCount>"
            + "<Articles>{0}"
            + "</Articles></xml>";
            string items = "";
            foreach (M_WXImgItem model in Articles)
            {
                items += "<item><Title><![CDATA[" + model.Title + "]]></Title> "
                 + "<Description><![CDATA[" + model.Description + "]]></Description>"
                 + "<PicUrl><![CDATA[" + model.PicUrl + "]]></PicUrl>"
                 + "<Url><![CDATA[" + model.Url + "]]></Url></item>";
            }
            return string.Format(xml, items);
        }
        //用于客服单发
        public string ToJson()
        {
            string jsonTlp = "{\"touser\":\"" + ToUserName + "\",\"msgtype\":\"news\",\"news\":{\"articles\": [";
            for (int i = 0; i < Articles.Count && i < 10; i++)
            {
                jsonTlp += "{\"title\":\"" + Articles[i].Title + "\",\"description\":\"" + Articles[i].Description + "\",\"url\":\"" + Articles[i].Url + "\",\"picurl\":\"" + Articles[i].PicUrl + "\"},";
            }
            jsonTlp = jsonTlp.TrimEnd(',');
            jsonTlp += "]}";
            return jsonTlp;
            //JsonSerializerSettings setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            //return JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented, setting);
        }
    }
    // 内容文章条目模型
    public class M_WXImgItem
    {
        [JsonProperty("title")]
        public string Title = "";
        [JsonProperty("description")]
        public string Description = "";
        [JsonProperty("picurl")]
        public string PicUrl = "";
        [JsonProperty("url")]
        public string Url = "";
        //是否仅为文本类型
        public bool IsText
        {
            get { return (string.IsNullOrEmpty(PicUrl) && string.IsNullOrEmpty(Url)); }
        }
    }
    /// <summary>
    /// 群发多图文模型
    /// </summary>
    public class M_WXNewsItem
    {
        public string thumb_media_id = "";//图文消息缩略图的media_id，可以在基础支持-上传多媒体文件接口中获得
        public string author = "";
        public string title = "";
        public string content_source_url = "";//在图文消息页面点击“阅读原文”后的页面
        public string content = "";//图文消息页面的内容，支持HTML标签
        public string digest = "";//图文消息的描述
        public string show_cover_pic = "1";//是否显示封面，1为显示，0为不显示
    }
    /// <summary>
    /// 群发信息模型
    /// </summary>
    public class M_WXAllMsg
    {
        public M_WXFiter filter = new M_WXFiter();
        public string msgtype;
        public WXMsgText text = null;//文本,图文,图片
        public M_WXMsgMedia mpnews = null;
        public M_WXMsgMedia image = null;
        public string ToJson()
        {
            JsonSerializerSettings setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented, setting);
        }
    }

    /// <summary>
    /// 行业消息模板模型
    /// </summary>
    public class M_WXTlp
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public string template_id { get; set; }
        /// <summary>
        /// 模板标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 模板所属行业的一级行业
        /// </summary>
        public string primary_industry { get; set; }
        /// <summary>
        /// 模板所属行业的二级行业
        /// </summary>
        public string deputy_industry { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 模板示例
        /// </summary>
        public string example { get; set; }
    }
}
