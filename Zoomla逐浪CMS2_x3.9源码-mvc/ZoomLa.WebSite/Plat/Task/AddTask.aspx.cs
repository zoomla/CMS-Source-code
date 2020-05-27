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

namespace ZoomLaCMS.Plat.Task
{
    public partial class AddTask : System.Web.UI.Page
    {
        B_Plat_Task taskBll = new B_Plat_Task();
        M_Plat_Task taskMod = null;
        B_User buser = new B_User();
        private int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            if (Mid > 0)
            {
                taskMod = taskBll.SelReturnModel(Mid);
                TaskName_T.Text = taskMod.TaskName;
                TaskContent_T.Text = taskMod.TaskContent;
                EndTime_T.Text = taskMod.EndTime.ToString();
                AddColor_Hid.Value = taskMod.Color;
                manage_hid.Value = buser.SelByIDS(taskMod.LeaderIDS);
                member_hid.Value = buser.SelByIDS(taskMod.PartTakeIDS);
            }
            else
            {
                EndTime_T.Text = DateTime.Now.AddDays(7).ToString();
            }
        }
        protected void TaskAdd_Btn_Click(object sender, EventArgs e)
        {
            M_User_Plat upMod = B_User_Plat.GetLogin();
            M_Plat_Task taskMod = new M_Plat_Task();
            if (Mid > 0) { taskMod = taskBll.SelReturnModel(Mid); }
            taskMod.TaskName = TaskName_T.Text;
            taskMod.TaskContent = TaskContent_T.Text;
            taskMod.LeaderIDS = manage_hid.Value;
            taskMod.PartTakeIDS = member_hid.Value;
            taskMod.Color = AddColor_Hid.Value;
            taskMod.EndTime = Convert.ToDateTime(EndTime_T.Text);
            if (taskMod.ID > 0)
            {
                taskBll.UpdateByID(taskMod);
            }
            else
            {
                taskMod.TaskType = 1;
                taskMod.Status = 1;
                taskMod.BeginTime = DateTime.Now;
                taskMod.CreateUser = upMod.UserID;
                taskMod.CreateUName = upMod.UserName;
                taskBll.Insert(taskMod);
            }
            function.WriteSuccessMsg("操作成功", "/Plat/Task/Default.aspx");
        }
    }
}