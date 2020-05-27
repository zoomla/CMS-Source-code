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
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.MIS.Ke
{
    public partial class DailyPlan_Add : System.Web.UI.Page
    {
        B_Blog_Sdl sdlBll = new B_Blog_Sdl();
        M_Blog_Sdl sdlMod = new M_Blog_Sdl();
        B_MisInfo typebll = new B_MisInfo();
        B_User buser = new B_User();
        public int TypeID { get { return DataConverter.CLng(Request.QueryString["TypeID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind() { }
        //获取指定月份下的数据
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            sdlMod.Name = string.IsNullOrEmpty(Name_T.Text) ? "无标题" : Name_T.Text;
            sdlMod.TaskType = TypeID;
            string startdate = txtBeginTime.Text.Trim() + " " + Request.Form["startHour"] + ":" + Request.Form["startMinitue"];
            sdlMod.StartDate = DataConverter.CDate(startdate);
            string enddate = txtEndTime.Text.Trim() + " " + Request.Form["endHour"] + ":" + Request.Form["endMinitue"];
            sdlMod.EndDate = DataConverter.CDate(enddate);
            if (sdlMod.StartDate > sdlMod.EndDate)
                function.WriteErrMsg("开始日期不能大于结束日期!");
            sdlMod.Describe = Request.Form["describe"].ToString();
            sdlMod.UserID = mu.UserID;
            sdlBll.Insert(sdlMod);
            function.Script(this, "refresh();");
        }
        protected void AddType_B_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            M_MisInfo typeMod = TypeID > 0 ? typebll.SelReturnModel(TypeID) : new M_MisInfo();
            typeMod.Title = Type_T.Text;
            typeMod.MID = mu.UserID;
            Type_T.Text = "";
            if (typeMod.ID > 0) { typebll.UpdateByID(typeMod); }
            else { typebll.insert(typeMod); }
            function.Script(this, "refresh();");
        }
    }
}