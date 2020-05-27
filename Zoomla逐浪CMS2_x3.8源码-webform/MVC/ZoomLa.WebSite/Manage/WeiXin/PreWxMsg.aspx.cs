using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.WeiXin
{
    public partial class PreWxMsg : System.Web.UI.Page
    {
        public int AppID { get { return DataConverter.CLng(Request.QueryString["appid"]); } }
        public string MediaID { get { return Request.QueryString["media_id"]; } }
        B_WX_APPID appbll = new B_WX_APPID();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.HideBread(this.Master);
            }
        }
        public void MyBind()
        {
            WxAPI wxBll = WxAPI.Code_Get(AppID);
            JArray newslist = JsonConvert.DeserializeObject<JArray>(wxBll.GetWxConfig("newsmaterial"));
            JToken news = newslist.First(j => j["media_id"].ToString().Equals(MediaID));
            News_Hid.Value = JsonConvert.SerializeObject(news);
        }
    }
}