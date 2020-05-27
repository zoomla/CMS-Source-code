using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace ZoomLaCMS.Manage.WeiXin
{
    public partial class SendWx : CustomerPageAction
    {
        B_WX_APPID appBll = new B_WX_APPID();
        WxAPI api = null;
        public int AppID { get { return DataConverter.CLng(Request.QueryString["appid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            api = WxAPI.Code_Get(AppID);
            if (!IsPostBack)
            {
                MyBind();
                string alias = " [公众号:" + api.AppId.Alias + "]";
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "WeiXin/WxAppManage.aspx'>公众号管理</a></li><li class='active'>文本群发" + alias + "</li>");
            }
        }
        public void MyBind()
        {
            WxApp_RPT.DataSource = appBll.Sel();
            WxApp_RPT.DataBind();
            List<M_WxGroupInfo> groups = api.GetWxGroup();
            foreach (M_WxGroupInfo item in groups)
            {
                ListItem li = new ListItem(item.name, item.id.ToString());
                WxGroup_D.Items.Add(li);
            }
            WxGroup_D.Items.Insert(0, "所有分组");
        }
        protected void SendAll_B_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtContent.Text.Trim())) { function.WriteErrMsg("文本内容不能为空"); }
            //往多个公众号下的用户发送信息
            if (!string.IsNullOrEmpty(Request.Form["appids"]))
            {
                string[] appids = Request.Form["appids"].Split(',');
                foreach (string id in appids)
                {
                    WxAPI itemApi = WxAPI.Code_Get(Convert.ToInt32(id));
                    string result = api.SendAllBySingle(TxtContent.Text);
                }
                function.WriteSuccessMsg("群体发送成功!");
            }
            else
            {
                function.WriteErrMsg("请选择公众号!");
            }
            #region 通过群发接口发送 disuse
            //switch (Request.Form["msgtype_rad"])
            //{
            //    case "image":
            //        if (!Path.GetExtension(image_up.FileName).ToLower().Equals(".jpg"))
            //        {
            //            function.WriteErrMsg("仅支持jpg图片");
            //        }
            //        else if (image_up.FileContent.Length < 100 || image_up.FileContent.Length > (1000 * 1024))
            //        {
            //            function.WriteErrMsg("文件的大小不符合规范");
            //        }
            //        string msg = wxBll.UploadImg(image_up.FileContent, Path.GetFileName(image_up.FileName));
            //        JObject jmsg = JsonConvert.DeserializeObject<JObject>(msg);
            //        if (jmsg["media_id"] != null)
            //        {
            //            string gid = "";
            //            M_WXAllMsg model = new M_WXAllMsg() { filter = new WXFiter() { group_id = gid, is_to_all = true }, msgtype = "image", image = new WXMsgMedia() { media_id = jmsg["media_id"].ToString() } };
            //            result = wxBll.SendAll(model);
            //        }
            //        break;
            //    default:
            //        if (string.IsNullOrEmpty(TxtContent.Text.Trim())) { function.WriteErrMsg("文本内容不能为空"); }
            //        result = wxBll.SendAll(TxtContent.Text, WxGroup_D.SelectedValue);
            //        break;
            //}
            //function.WriteErrMsg("发送结果:" + result); 
            //JObject jobj = JsonConvert.DeserializeObject<JObject>(result);
            //if (Convert.ToInt32(jobj["errcode"]) == 0)
            //{
            //    function.WriteSuccessMsg("群发送成功!");
            //}
            //else { function.WriteErrMsg("发送失败,原因:" + result); }
            #endregion
        }
        //protected void SendNews_Btn_Click(object sender, EventArgs e)
        //{
        //    M_WxImgMsg imgMsg = new M_WxImgMsg();
        //    imgMsg.Articles.Add(new M_WXImgItem() { Title = Title_T.Text, Description = Content_T.Text, PicUrl = PicUrl_T.Text, Url = Url_T.Text });
        //    string result=wxBll.SendAllBySingle(imgMsg);
        //    function.WriteSuccessMsg("图文信息发送完成","",0);
        //}
    }
}