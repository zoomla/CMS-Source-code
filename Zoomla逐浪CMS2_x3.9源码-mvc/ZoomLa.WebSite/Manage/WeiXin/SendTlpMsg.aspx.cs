using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.WeiXin
{
    public partial class SendTlpMsg : System.Web.UI.Page
    {
        WxAPI api = null;
        public string TlpID { get { return Request.QueryString["id"] ?? ""; } }
        public int AppID { get { return DataConverter.CLng(Request.QueryString["appid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            api = WxAPI.Code_Get(AppID);
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(TlpID)) { function.WriteErrMsg("未指定模板ID"); }
                MyBind();
                LoadParas();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "WeiXin/WxAppManage.aspx'>公众号管理</a></li><li class='active'>模板消息</li>");
            }
        }
        public void MyBind()
        {
            M_WXTlp tlpMod = api.Tlp_GetTlpByID(TlpID);
            title_L.Text = tlpMod.title;
            content_L.Text = tlpMod.content;
            data_L.Text = GetDataEx(tlpMod);
            ex_L.Text = tlpMod.example.Replace("\n", "<br />");
        }
        public string GetDataEx(M_WXTlp tlpMod)
        {
            string data = "{";
            List<string> paraNames = api.Tlp_GetTlpParaNames(tlpMod.content);
            for (int i = 0; i < paraNames.Count; i++)
            {
                data += "\"" + paraNames[i] + "\":{\"value\":\"" + paraNames[i] + "参数值\",\"color\":\"#000\"},";
            }
            return data.Trim(',');
        }
        public void LoadParas()
        {
            M_WXTlp tlpMod = api.Tlp_GetTlpByID(TlpID);
            List<string> paraNames = api.Tlp_GetTlpParaNames(tlpMod.content);
            string html = "";
            string tlp = "<tr><td><strong>@paraName</strong></td><td><input type='text' class='col_t form-control text_200_auto' name='@paraName' placeholder='@paraName'/><input type='text' class='col_t form-control' style='width:100px' name='col_@paraName' style='color:#000;' value='#000' /><input style='position: absolute; left: 3000px; ' class='col_c' id='col_@paraName' type='color' /></td></tr>";
            foreach (string s in paraNames)
            {
                html += tlp.Replace("@paraName", s);
            }
            ParasTr.InnerHtml = html;
        }
        protected void Send_B_Click(object sender, EventArgs e)
        {
            M_WXTlp tlpMod = api.Tlp_GetTlpByID(TlpID);
            string url = Url_T.Text;
            //{"first": {"value":"恭喜你购买成功！","color":"#173177" },"keynote1":{"value":"巧克力","color":"#173177" },..}
            string data = "{";
            List<string> paraNames = api.Tlp_GetTlpParaNames(tlpMod.content);
            for (int i = 0; i < paraNames.Count; i++)
            {
                data += "\"" + paraNames[i] + "\":{\"value\":\"" + Request.Form[paraNames[i]] + "\",\"color\":\"" + Request.Form["col_" + paraNames[i]] + "\"},";
            }
            data = data.Trim(',') + "}";
            function.WriteErrMsg(data);
            api.Tlp_SendTlpMsg(api.AppId.APPID, TlpID, url, data);
        }
    }
}