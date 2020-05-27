using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.BLL.API;

namespace ZoomLaCMS.Manage.WeiXin
{
    public partial class EditWxMenu : System.Web.UI.Page
    {
        //返回：api unauthorized,微信公众号未认证,必须认证后才可使用菜单编辑功能
        B_WX_APPID appbll = new B_WX_APPID();
        public int AppId { get { return DataConverter.CLng(Request.QueryString["appid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                M_APIResult result = new M_APIResult();
                result.retcode = M_APIResult.Failed;
                WxAPI api = WxAPI.Code_Get(AppId);
                string action = Request["action"];
                //result.result = api.AccessToken;
                //RepToClient(result);
                try
                {
                    switch (action)
                    {
                        case "create":
                            string jsondata = "{\"button\":" + Request.Form["menus"] + "}";
                            result.result = api.CreateWxMenu(jsondata);
                            if (!result.result.Contains("errmsg")) { result.retcode = M_APIResult.Success; }
                            else { result.retmsg = result.result; }
                            break;
                        case "get":
                            result.result = api.GetWxMenu();
                            if (!result.result.Contains("errmsg")) { result.retcode = M_APIResult.Success; }
                            else { result.retmsg = result.result; }
                            break;
                        default:
                            result.retmsg = "接口[" + action + "]不存在";
                            break;
                    }
                }
                catch (Exception ex) { result.retmsg = ex.Message; }
                RepToClient(result);
            }

            if (!IsPostBack)
            {
                M_WX_APPID appmod = appbll.SelReturnModel(AppId);
                string alias = " [公众号:" + appmod.Alias + "]";
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "WeiXin/WxAppManage.aspx'>公众号管理</a></li><li class='active'>自定义菜单" + alias + "</li>");
            }
        }
        private void RepToClient(M_APIResult result)
        {
            Response.Write(result.ToString()); Response.Flush(); Response.End();
        }
    }
}