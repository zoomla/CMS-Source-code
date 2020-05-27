using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Manage_WeiXin_AddReply : System.Web.UI.Page
{
    B_WX_ReplyMsg rpBll = new B_WX_ReplyMsg();
    B_WX_APPID appBll = new B_WX_APPID();
    // 公众号的数据库ID
    public int AppId { get { return DataConverter.CLng(Request.QueryString["appid"]); } }
    public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (AppId <= 0) { function.WriteErrMsg("请先配置公众号信息,再进入该页面,[<a href='WxAppManage.aspx'>前往配置</a>]"); }
        if (!IsPostBack)
        {
            M_WX_APPID appmod = appBll.SelReturnModel(AppId);
            string alias = " [公众号:" + appmod.Alias + "]";
            if (Mid > 0)
            {
                M_WX_ReplyMsg rpMod = rpBll.SelReturnModel(Mid);
                filter_T.Text = rpMod.fiter;
                M_WXImgItem item = JsonConvert.DeserializeObject<M_WXImgItem>(rpMod.Content);
                Title_T.Text = item.Title;
                Content_T.Text = item.Description;
                PicUrl_T.Text = item.PicUrl;
                Url_T.Text = item.Url;
                function.Script(this, "SetRadVal('msgtype_rad','" + rpMod.MsgType + "');");
                IsDefault_Chk.Checked = rpMod.IsDefault == 1;
            }
            Call.SetBreadCrumb(Master, "<li><a href='Home.aspx'>移动微信</a></li><li class='active'><a href='ReplyList.aspx'>回复管理</a></li></li><li class='active'>添加回复" + alias + "</li>");
        }
    }
    protected void Save_B_Click(object sender, EventArgs e)
    {
        M_WX_ReplyMsg rpMod = new M_WX_ReplyMsg();
        if (Mid > 0) { rpMod = rpBll.SelReturnModel(Mid); }
        M_WXImgItem item = new M_WXImgItem() { Title = Title_T.Text, Description = Content_T.Text, PicUrl = PicUrl_T.Text, Url = Url_T.Text };
        rpMod.fiter = filter_T.Text.Trim();
        rpMod.Content = JsonConvert.SerializeObject(item);
        rpMod.MsgType = item.IsText ? "0" : "1";
        rpMod.AppId = AppId;
        rpMod.MsgType = Request.Form["msgtype_rad"];
        rpMod.IsDefault = IsDefault_Chk.Checked ? 1 : 0;
        if (Mid > 0)
        {
            rpBll.UpdateByID(rpMod);
        }
        else { rpBll.Insert(rpMod); }
        Response.Redirect("ReplyList.aspx?appid=" + AppId);
    }
}