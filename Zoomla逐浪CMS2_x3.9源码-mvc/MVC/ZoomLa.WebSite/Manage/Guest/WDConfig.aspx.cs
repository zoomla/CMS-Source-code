namespace ZoomLaCMS.Manage.Guest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using ZoomLa.BLL;
    using ZoomLa.Model;
    using System.Data;
    public partial class WDConfig : System.Web.UI.Page
    {
        B_Group groupBll = new B_Group();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "AskManage");
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx" + "'>工作台</a></li><li><a href='WdCheck.aspx'>问答管理</a></li><li class='active'>问答配置</li>");
            }
        }
        public void MyBind()
        {
            DataTable dt = groupBll.Select_All();
            selGroup_Rpt.DataSource = dt;
            selGroup_Rpt.DataBind();
            ReplyGroup_Rpt.DataSource = dt;
            ReplyGroup_Rpt.DataBind();
            QuestGroup_Rpt.DataSource = dt;
            QuestGroup_Rpt.DataBind();
            IsLogin.Checked = GuestConfig.GuestOption.WDOption.IsLogin;
            IsReply.Checked = GuestConfig.GuestOption.WDOption.IsReply;
            Point_T.Text = GuestConfig.GuestOption.WDOption.WDPoint.ToString();//SiteConfig.UserConfig.WDPoint.ToString();
            GPoint_T.Text = GuestConfig.GuestOption.WDOption.WDRecomPoint.ToString(); //SiteConfig.UserConfig.WDRecomPoint.ToString();
            QuestPoint_T.Text = GuestConfig.GuestOption.WDOption.QuestPoint.ToString();//SiteConfig.UserConfig.QuestPoint.ToString();
            PointType_R.SelectedValue = GuestConfig.GuestOption.WDOption.PointType;//SiteConfig.UserConfig.PointType;
        }
        //获取选中状态
        public string GetCheck(int type)
        {
            string ids = "";
            switch (type)
            {
                case 1:
                    ids = "," + GuestConfig.GuestOption.WDOption.selGroup + ",";
                    break;
                case 2:
                    ids = "," + GuestConfig.GuestOption.WDOption.ReplyGroup + ",";
                    break;
                case 3:
                    ids = "," + GuestConfig.GuestOption.WDOption.QuestGroup + ",";
                    break;
                default:
                    break;
            }
            return ids.Contains("," + Eval("GroupID").ToString() + ",") ? "checked=checked" : "";
        }
        protected void Save_B_Click(object sender, EventArgs e)
        {
            GuestConfigInfo guestinfo = GuestConfig.GuestOption;
            guestinfo.WDOption.selGroup = Request.Form["selGroup"];
            guestinfo.WDOption.ReplyGroup = Request.Form["replyGroup"];
            guestinfo.WDOption.QuestGroup = Request.Form["questGroup"];
            guestinfo.WDOption.WDPoint = DataConverter.CLng(Point_T.Text);
            guestinfo.WDOption.WDRecomPoint = DataConverter.CLng(GPoint_T.Text);
            guestinfo.WDOption.QuestPoint = DataConverter.CLng(QuestPoint_T.Text);
            guestinfo.WDOption.PointType = PointType_R.SelectedValue;
            guestinfo.WDOption.IsLogin = IsLogin.Checked;
            guestinfo.WDOption.IsReply = IsReply.Checked;
            GuestConfig config = new GuestConfig();
            config.Update(guestinfo);
            function.WriteSuccessMsg("保存问答配置成功!");
        }
    }
}