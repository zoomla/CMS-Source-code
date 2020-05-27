using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.Common;
using ZoomLa.BLL.Helper;

public partial class CallCounter : System.Web.UI.Page
{
    // <iframe style="display:none;" src="/CallCounter.aspx?ztype=mbh5&id=12&title=213123123"></iframe>
    B_User buser = new B_User();
    private string ZType { get { return Request.QueryString["ztype"] ?? ""; } }
    private string InfoID { get { return Request.QueryString["id"] ?? ""; } }
    private string InfoTitle { get { return Request.QueryString["title"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AddCounter();
            Response.Clear();
            Response.End();
        }
    }
    public void AddCounter()
    {
        M_Com_VisitCount visitMod = new M_Com_VisitCount();
        visitMod.IP = IPScaner.GetUserIP();
        visitMod.UserID = buser.GetLogin().UserID;
        visitMod.OSVersion = VisitCounter.User.Agent(2);
        visitMod.Source = Request.UrlReferrer.ToString();//访问源
        visitMod.BrowerVersion = DeviceHelper.GetBrower().ToString();
        visitMod.Device = VisitCounter.User.Agent(3); //设备
        visitMod.ZType = ZType;
        visitMod.InfoID = InfoID;
        visitMod.InfoTitle = InfoTitle;
        B_Com_VisitCount.Insert(visitMod);
    }
}