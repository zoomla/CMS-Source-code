using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model.User;
using ZoomLa.Model;
using Newtonsoft.Json;
using ZoomLa.Components;
using ZoomLa.SQLDAL;
//不与APP绑定,只可管理员使用
public partial class App_CreateLink : System.Web.UI.Page
{
    M_Temp tempMod = new M_Temp();
    B_Temp tempBll = new B_Temp();
    B_QrCode codeBll = new B_QrCode();
    B_App appBll = new B_App();
    //public int AppID { get { return DataConverter.CLng(Request.QueryString["appid"]); } }
    public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin.CheckIsLogged();
        if (!IsPostBack)
        {
            MyBind();
            //android_T.Text = "www.baidu.com";
            //iphone_T.Text = "bbs.z01.com";
            //ipad_T.Text = "http://www.1th.cn";
            //wphone_T.Text = "http://www.z01.com";
            //pc_T.Text = "demo.z01.com";
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='/APP/Default.aspx'>移动应用</a></li><li><a href='CLList.aspx'>APP颁发</a></li><li class='active'>颁发管理</li>");
        }
    }
    public void MyBind()
    {
        M_QrCode qrcodeMod = null;
        if (Mid > 0)
        {
            qrcodeMod = codeBll.SelReturnModel(Mid);
        }
        if (qrcodeMod != null)
        {
            Alias_T.Text = qrcodeMod.UserName;
            android_T.Text = codeBll.GetUrlByAgent(DeviceHelper.Agent.Android, qrcodeMod);
            iphone_T.Text = codeBll.GetUrlByAgent(DeviceHelper.Agent.iPhone, qrcodeMod);
            ipad_T.Text = codeBll.GetUrlByAgent(DeviceHelper.Agent.iPad, qrcodeMod);
            wphone_T.Text = codeBll.GetUrlByAgent(DeviceHelper.Agent.WindowsPhone, qrcodeMod);
            pc_T.Text = codeBll.GetUrlByAgent(DeviceHelper.Agent.PC, qrcodeMod);
            string url = StrHelper.UrlDeal(SiteConfig.SiteInfo.SiteUrl + "/app/url.aspx?id=" + qrcodeMod.ID);
            Link_L.Text = codeBll.GetUrl(qrcodeMod.ID);
            codediv.InnerHtml = "<img src='/Common/Common.ashx?url=" + url + "' class='codeimg'  />";
        }
    }
    protected void CreateLink_Btn_Click(object sender, EventArgs e)
    {
        M_QrCode codeMod = null;
        if (Mid > 0) { codeMod = codeBll.SelReturnModel(Mid); }
        if (codeMod == null) { codeMod = new M_QrCode(); }
        codeMod.UserName = Alias_T.Text.Trim();
        codeMod.Urls = DeviceHelper.Agent.Android + "$" + StrHelper.UrlDeal(android_T.Text.Trim()) + ","
                        + DeviceHelper.Agent.iPhone + "$" + StrHelper.UrlDeal(iphone_T.Text.Trim()) + ","
                        + DeviceHelper.Agent.iPad + "$" + StrHelper.UrlDeal(ipad_T.Text.Trim()) + ","
                        + DeviceHelper.Agent.WindowsPhone + "$" + StrHelper.UrlDeal(wphone_T.Text.Trim()) + ","
                        + DeviceHelper.Agent.PC + "$" + StrHelper.UrlDeal(pc_T.Text.Trim());
        //string url = StrHelper.UrlDeal(SiteConfig.SiteInfo.SiteUrl + "/app/url.aspx?id=" + qrcodeMod.ID);
        //qrcodeMod.ImageUrl = url;
        if (codeMod.ID <= 0) { codeMod.AppID = DataConverter.CLng(function.GetRandomString(6, 2)); codeMod.ID = Insert(codeMod); }
        else { codeBll.UpdateByID(codeMod); }
        Response.Redirect("CL.aspx?ID=" + codeMod.ID);
        //Link_L.Text = url;
        //codediv.InnerHtml = "<img src='/Common/Common.ashx?url=" + url + "' class='codeimg'  />";
    }
    public int Insert(M_QrCode model)
    {
        return Sql.insertID(codeBll.strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
    }
}