using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class Manage_WeiXin_AutoReply : System.Web.UI.Page
{
    B_WX_ReplyMsg wxBll = new B_WX_ReplyMsg();
    public int AppId { get { return DataConverter.CLng(Request.QueryString["appid"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "WeiXin/Default.aspx'>微信管理</a></li><li class='active'>自动回复配置</li>");
        }
    }
    protected void Save_B_Click(object sender, EventArgs e)
    {
        M_WX_ReplyMsg replymsg = new M_WX_ReplyMsg();
        replymsg.fiter = GuestMsg_T.Text;
        replymsg.Content = Content_T.Text;
        wxBll.Insert(replymsg);
    }
}