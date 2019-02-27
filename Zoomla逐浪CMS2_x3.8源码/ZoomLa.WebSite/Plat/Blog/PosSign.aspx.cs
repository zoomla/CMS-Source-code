using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Plat;

public partial class Plat_Blog_PosSign : System.Web.UI.Page
{
    B_Blog_Msg msgBll = new B_Blog_Msg();
    public string Source { get { return Request.QueryString["S"]; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            B_User.CheckIsLogged();
            function.Script(this, "GetCurrent();");
        }
    }
    protected void Sure_Btn_Click(object sender, EventArgs e)
    {
        string addr= Pos_Hid.Value + " " + Request.Form["add_rad"];
        string content = "<i class='fa fa-map-marker'></i><a href='/Plat/Blog?Skey=" + HttpUtility.UrlEncode("地理位置签到") + "' title='话题浏览'>地理位置签到</a>：";
        content += "<a href='/Plat/Blog/MapByPoint.aspx?Point=" + Point_Hid.Value + "' class='hidden-lg hidden-md'>" + addr + "</a>";
        content += "<a href=\"javascript:;\" onclick=\'ShowByPoint(" + Point_Hid.Value + ");\' class='hidden-xs hidden-sm'>" + addr + "</a>";
        content += "<br />" + HttpUtility.HtmlEncode(Msg_T.Text);
        M_Blog_Msg msgMod = FillMsg(content);
        msgBll.Insert(msgMod);
        switch (Source)
        {
            case "mobile":
                Response.Redirect("/Plat/Blog/Default.aspx");
                break;
            case "pc":
            default:
                function.Script(this, "Signed();");
                break;
        }
    }
    public M_Blog_Msg FillMsg(string msg)
    {
        M_User_Plat upMod = B_User_Plat.GetLogin();
        M_Blog_Msg model = new M_Blog_Msg();
        model.MsgType = 1;
        model.Status = 1;
        model.CUser = upMod.UserID;
        model.CUName = upMod.TrueName;
        msg = msg.Replace("\r\n", "<br/>");
        model.MsgContent = msg;
        model.pid = 0;
        model.ReplyID = 0;
        model.CompID = upMod.CompID;
        model.GroupIDS = "";
        //model.Location = Location_Hid.Value;
        return model;
    }
}