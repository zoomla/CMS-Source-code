using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.BLL.Project;
using System.Data;
using ZoomLa.Model.Project;

namespace ZoomLaCMS.Plat.Note
{
    public partial class ProList : System.Web.UI.Page
    {
        //B_Pro_Project proBll = new B_Pro_Project();
        B_Pro_Step stepBll = new B_Pro_Step();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            M_UserInfo mu = buser.GetLogin();
            DataTable dt = stepBll.SelByUid(mu.UserID);
            ProList_RPT.DataSource = dt;
            ProList_RPT.DataBind();
            DataTable parDt = stepBll.SelByParUser(mu.UserID);
            Par_RPT.DataSource = parDt;
            Par_RPT.DataBind();
        }
        public string GetStatus()
        {
            int status = DataConverter.CLng(Eval("ZStatus"));
            switch (status)
            {
                case 0:
                    return "进行中";
                case -1:
                    return "暂停";
                case 99:
                    return "已完结";
                default:
                    return "进行中";
            }
        }
        public string GetLevel()
        {
            if (string.IsNullOrEmpty(Eval("ZLevel").ToString())) { return "C"; }
            return Eval("ZLevel").ToString();
        }
        public string GetProType()
        {
            string typestr = Eval("ZType").ToString();
            switch (typestr)
            {
                case "web":
                    return "网站";
                case "app":
                    return "APP";
                case "desktop":
                    return "桌面应用";
                default:
                    return "网站";
            }
        }
        public string GetNoteUrl(int id = 0)
        {
            if (Request.Browser.IsMobileDevice)
            {
                return "mobilenote.aspx?id=" + id;
            }
            return "note.aspx?id=" + id;
        }
        public string GetViewUrl()
        {
            if (Request.Browser.IsMobileDevice)
            {
                return "mobileview.aspx?id=" + Eval("ID");
            }
            return "view.aspx?id=" + Eval("ID");
        }
        protected void ProList_RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = DataConverter.CLng(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "del":
                    stepBll.Del(id);
                    MyBind();
                    break;
                //case "note":
                //    M_Pro_Step stepMod = stepBll.SelModelByProID(id);
                //    if (stepMod == null) { Response.Redirect("Note.aspx?NodeID=" + id); }
                //    else { Response.Redirect("Note.aspx?ID="+stepMod.ID+ "&NodeID=" + id); }
                //    break;
            }
        }
    }
}