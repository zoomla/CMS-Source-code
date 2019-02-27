using System.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Room;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Room;
namespace ZoomLaCMS.MIS.Ke
{
    public partial class SchConfig : System.Web.UI.Page
    {
        //1,别人共享的课程,如老师分享给学生,非本人不可修改
        B_EDU_School schBll = new B_EDU_School();
        B_ClassRoom classBll = new B_ClassRoom();
        B_User buser = new B_User();
        public int Cid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            M_UserInfo mu = buser.GetLogin();
            M_EDU_School schMod = new M_EDU_School();
            DataTable classDT = classBll.SelByUid(mu.UserID);
            ClassRPT.DataSource = classDT;
            ClassRPT.DataBind();
            if (classDT.Rows.Count < 1) { Empty_SP.Visible = true; ClassRPT.Visible = false; }
            if (Cid > 0)
            {
                schMod = schBll.SelReturnModel(Convert.ToInt32(Cid));
                TermName_L.Text = "[" + schMod.TermName + "]";
                //SchoolName_T.Text = schMod.SchoolName;
                TermName_T.Text = schMod.TermName;
                WeekDay_DP.SelectedValue = schMod.weekday.ToString();
                PreMoning_DP.SelectedValue = schMod.premoning.ToString();
                Moring_DP.SelectedValue = schMod.moring.ToString();
                Afternoon_DP.SelectedValue = schMod.afternoon.ToString();
                Evening_DP.SelectedValue = schMod.evening.ToString();
                if (mu.UserID != schMod.UserID)
                {
                    op_table.Visible = false;
                    BanEdit_Hid.Value = "1";
                }
                function.Script(this, "SetChkVal('ClassIDS_chk','" + schMod.ClassIDS + "');");
            }
            else
            {
                TermName_T.Text = DateTime.Now.ToString("yyyy-MM-dd") + "_" + function.GetRandomString(4, 2);
                schMod.premoning = 1;
                schMod.moring = 4;
                schMod.afternoon = 3;
                schMod.evening = 2;
                schMod.weekday = 5;
                Json_Hid.Value = JsonConvert.SerializeObject(schMod);
            }
            Json_Hid.Value = JsonConvert.SerializeObject(schMod);
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_EDU_School schMod = JsonConvert.DeserializeObject<M_EDU_School>(Json_Hid.Value);
            schMod.SchoolName = SchoolName_T.Text;
            schMod.UserID = buser.GetLogin().UserID;
            schMod.TermName = TermName_T.Text;
            schMod.ClassIDS = Request.Form["ClassIDS_chk"];
            schMod.premoning = Convert.ToInt32(PreMoning_DP.SelectedValue);
            schMod.moring = Convert.ToInt32(Moring_DP.SelectedValue);
            schMod.afternoon = Convert.ToInt32(Afternoon_DP.SelectedValue);
            schMod.evening = Convert.ToInt32(Evening_DP.SelectedValue);
            //schMod.SchoolType = Request.Form["schoolType_rad"];
            //schMod.StudySelf = StudySelf_Chk.Checked ? 1 : 0;
            //schMod.SelectCourse = SelectCourse_Chk.Checked ? 1 : 0;
            if (schMod.ID > 0) { schBll.UpdateByID(schMod); }
            else { schBll.Insert(schMod); }
            function.WriteSuccessMsg("保存成功!", "ConfigList.aspx");
        }
    }
}