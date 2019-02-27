using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using System.Data;
using System.IO;

namespace ZoomLaCMS.Plat.Task
{
    public partial class TaskDetail : System.Web.UI.Page
    {
        public M_Plat_Task taskMod = new M_Plat_Task();
        private B_Plat_Task taskBll = new B_Plat_Task();
        private B_Plat_TaskMsg msgBll = new B_Plat_TaskMsg();
        private B_User_Plat upBll = new B_User_Plat();
        public int TaskID
        {
            get { return Convert.ToInt32(Request.QueryString["ID"]); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    taskMod = taskBll.SelReturnModel(TaskID);
                    M_User_Plat upMod = upBll.SelReturnModel(taskMod.CreateUser);
                    userimg_img.Src = upMod.UserFace;
                    CreateUName_L.Text = upMod.TrueName.Length > 5 ? taskMod.CreateUName.Substring(0, 5) + ".." : taskMod.CreateUName;
                    TName_L.Text = taskMod.TaskName;
                    BTime_L.Text = taskMod.BeginTime.ToString("MM月dd日 HH:mm");
                    ETime_L.Text = taskMod.EndTime.ToString("MM月dd日 HH:mm");
                    TaskContent_T.Text = taskMod.TaskContent;
                    LeaderIDS.Text = GetUName(taskMod.LeaderIDS, 50);
                    PartTakeIDS.Text = GetUName(taskMod.PartTakeIDS, 500);
                    MyBind();
                }
            }
        }
        public string GetUName(string ids, int len)
        {
            string uname = upBll.SelInfoByIDS(ids);
            return uname.Length > len ? uname.Substring(0, len) + "..." : uname;
        }
        public void MyBind()
        {
            ShowTaskFile(taskMod);
            TaskMsg_Rep.DataSource = msgBll.SelByTask(TaskID);
            TaskMsg_Rep.DataBind();
        }
        protected void Del_Btn_Click(object sender, EventArgs e)
        {

        }
        protected void AddMsg_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MsgContent_T.Text))
            {
                M_Plat_TaskMsg msgMod = FillMsg();
                MsgContent_T.Text = "";
                msgBll.Insert(msgMod);
                MyBind();
            }
            else
            {
                function.Script(this, "alert('内容不能为空!!');");
            }
        }
        //添加一条留言
        public M_Plat_TaskMsg FillMsg()
        {
            M_User_Plat upMod = B_User_Plat.GetLogin();
            M_Plat_TaskMsg msgMod = new M_Plat_TaskMsg();
            msgMod.TaskID = TaskID;
            msgMod.UserID = upMod.UserID;
            msgMod.UserName = upMod.TrueName;
            msgMod.MsgContent = MsgContent_T.Text;
            msgMod.Attach = "";
            msgMod.CreateTime = DateTime.Now;
            msgMod.Status = 1;
            msgMod.MsgType = 1;
            return msgMod;
        }

        void ShowTaskFile(M_Plat_Task mp)
        {
            if (!string.IsNullOrEmpty(mp.Attach))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("ExtName"));
                dt.Columns.Add(new DataColumn("FileName"));
                dt.Columns.Add(new DataColumn("Path"));
                string[] fileurls = mp.Attach.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < fileurls.Length; i++)
                {
                    if (!string.IsNullOrEmpty(fileurls[i]))
                    {
                        string[] datas = new string[3];
                        datas[0] = GroupPic.GetExtNameMini(Path.GetExtension(fileurls[i]).Replace(".", ""));
                        string fname = Path.GetFileName(fileurls[i]);
                        datas[1] = fname.Length > 6 ? fname.Substring(0, 5) + "..." : fname;
                        datas[2] = fileurls[i];
                        dt.Rows.Add(datas);
                    }
                }
                RShowFilelist.DataSource = dt;
                RShowFilelist.DataBind();
            }
        }
        protected void upfilebt_Click(object sender, EventArgs e)
        {
            M_Plat_Task mp = taskBll.SelReturnModel(TaskID);
            if (!string.IsNullOrEmpty(Attach_Hid.Value))
            {
                mp.Attach = mp.Attach + Attach_Hid.Value + ",";
                taskBll.UpdateByID(mp);
            }
            Response.Redirect(Request.RawUrl);
        }
        protected void downBt_Click(object sender, EventArgs e)
        {
            M_Plat_Task mp = taskBll.SelReturnModel(TaskID);
            SafeSC.DownFile(mp.Attach);
        }
        protected void RShowFilelist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Down":
                    SafeSC.DownFile(e.CommandArgument.ToString());
                    break;
                default:
                    break;
            }
        }
        protected void Del_Link_Click(object sender, EventArgs e)
        {
            taskMod = taskBll.SelReturnModel(TaskID);
            if (taskMod.CreateUser != B_User_Plat.GetLogin().UserID)
            {
                function.Script(this, "alert('你无权删除该任务');");
            }
            else
            {
                taskBll.Del(TaskID);
            }
        }
    }
}