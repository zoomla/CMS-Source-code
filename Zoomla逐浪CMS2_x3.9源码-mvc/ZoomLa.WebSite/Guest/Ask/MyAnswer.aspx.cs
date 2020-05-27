using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Data;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.Components;
using ZoomLa.BLL.User;
namespace ZoomLaCMS.Guest.Ask
{
    public partial class MyAnswer : System.Web.UI.Page
    {
        protected M_UserInfo m_UserInfo = new M_UserInfo();
        B_User buser = new B_User();
        M_GuestAnswer m_guestanswer = new M_GuestAnswer();
        B_GuestAnswer b_guestanswer = new B_GuestAnswer();
        B_Ask askBll = new B_Ask();
        M_Ask _mAsk = new M_Ask();
        B_TempUser tpuserBll = new B_TempUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                M_UserInfo mu = tpuserBll.GetLogin();//buser.GetLogin(false);
                if (!string.IsNullOrEmpty(GuestConfig.GuestOption.WDOption.ReplyGroup))
                {//用户组回复权限
                    string groups = "," + GuestConfig.GuestOption.WDOption.ReplyGroup + ",";
                    if (!groups.Contains("," + mu.GroupID.ToString() + ","))
                        function.WriteErrMsg("您没有回复问题的权限!");
                }
                int userid = buser.GetLogin().UserID;
                int QuestionId = DataConverter.CLng(Request["ID"]);
                B_Ask ask = new B_Ask();
                _mAsk = ask.SelReturnModel(QuestionId);
                if (_mAsk.Status == 0)
                    function.WriteErrMsg("该问题未经过审核,无法对其答复!", "/Guest/Ask/List.aspx");
                if (_mAsk.UserName == buser.GetLogin().UserName)
                {
                    Response.Redirect("Interactive.aspx?ID=" + QuestionId);
                }
                if (_mAsk.Supplyment == "")
                {
                    supdiv.Visible = false;
                }

                question.Text = _mAsk.Qcontent;
                supment.Text = _mAsk.Supplyment;
                if (_mAsk.IsNi.ToString() == "1") username.Text = "匿名";
                else username.Text = ask.SelReturnModel(QuestionId).UserName;
                addtime.Text = (ask.SelReturnModel(QuestionId).AddTime).ToString();
                DataTable dt = b_guestanswer.Sel(" QueId=" + QuestionId + " And supplymentid=0 AND status<>0", "", null);
                dBind(dt);
            }
        }
        private void dBind(DataTable dt)
        {

            this.Repeater1.DataSource = dt;
            this.Repeater1.DataBind();
            if (dt != null)
            {
                dt.Dispose();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            m_UserInfo = tpuserBll.GetLogin();
            if (GuestConfig.GuestOption.WDOption.IsReply && m_UserInfo.UserID <= 0)
                Response.Redirect("/User/Login.aspx?ReturnUrl=/Guest/Ask/MyAnswer.aspx");
            m_guestanswer.UserId = m_UserInfo.UserID;
            m_guestanswer.Content = Server.HtmlEncode(txtContent.Text);
            m_guestanswer.QueId = DataConverter.CLng(Request["ID"]);
            m_guestanswer.AddTime = DateTime.Now;
            m_guestanswer.Status = 0;
            m_guestanswer.UserName = m_UserInfo.UserID > 0 ? m_UserInfo.UserName : m_UserInfo.UserName + "[" + m_UserInfo.WorkNum + "]";
            m_guestanswer.supplymentid = 0;
            m_guestanswer.audit = 0;
            if (CkIsNi.Checked == true)
            {
                m_guestanswer.IsNi = 1;
            }
            b_guestanswer.insert(m_guestanswer);
            _mAsk = askBll.SelReturnModel(m_guestanswer.QueId);
            if (m_UserInfo.UserID > 0)
            {
                string questname = buser.SelReturnModel(_mAsk.UserId).UserName;
                if (string.IsNullOrEmpty(questname))
                    questname = "匿名用户";
                buser.ChangeVirtualMoney(m_UserInfo.UserID, new M_UserExpHis()
                {
                    score = GuestConfig.GuestOption.WDOption.WDPoint,
                    ScoreType = (int)((M_UserExpHis.SType)(Enum.Parse(typeof(M_UserExpHis.SType), GuestConfig.GuestOption.WDOption.PointType))),
                    detail = string.Format("{0} {1}在问答中心回答了{2}的问题,赠送{3}分", DateTime.Now, m_UserInfo.UserName, questname, GuestConfig.GuestOption.WDOption.WDPoint)
                });
            }
            function.WriteSuccessMsg("回答成功", "List.aspx");


        }
        protected void btnreply_Click(object sender, EventArgs e)
        {

            int userid = buser.GetLogin().UserID;
            string username = buser.GetLogin().UserName;
            m_guestanswer.QueId = DataConverter.CLng(Request["ID"]);
            m_guestanswer.Content = this.Request.Form["txtreply"];
            m_guestanswer.AddTime = DateTime.Now;
            m_guestanswer.UserId = userid;
            m_guestanswer.UserName = username;
            m_guestanswer.Status = 0;
            m_guestanswer.audit = 0;
            m_guestanswer.supplymentid = DataConverter.CLng(Request.Form["Rid"]);//Convert.ToInt32( hfsid.Value);

            b_guestanswer.insert(m_guestanswer);
            function.WriteSuccessMsg("回复成功!", "MyAnswer.aspx?ID=" + Request["ID"]);

        }
        public string GetUserName(string UserID)
        {
            return buser.SeachByID(DataConverter.CLng(UserID)).UserName;
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            int QuestionId = DataConverter.CLng(Request["ID"]);
            B_Ask ask = new B_Ask();

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)//指触发的类型为DadaList中的基本行或内容行
            {
                Repeater rep = e.Item.FindControl("Repeater2") as Repeater;//找到里层的repeater对象
                DataRowView rowv = (DataRowView)e.Item.DataItem;//找到分类Repeater关联的数据项 
                Label Isname = (Label)e.Item.FindControl("Isname");
                if (rowv["isNi"].ToString() == "1") { Isname.Text = "匿名"; }
                string Aid = (rowv["ID"]).ToString();
                hfsid.Value = Aid;
                M_Ask askMod = askBll.SelReturnModel(QuestionId);
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("id", hfsid.Value) };
                DataTable dts = b_guestanswer.Sel(" supplymentid=@id And Userid=" + askMod.UserId, "", sp);
                rep.DataSource = dts;
                rep.DataBind();
            }
        }

        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int QuestionId = DataConverter.CLng(Request["ID"]);
            M_Ask askMod = askBll.SelReturnModel(QuestionId);
            DataRowView rowv = (DataRowView)e.Item.DataItem;
            string Aid = (rowv["ID"]).ToString();
            string queid = (rowv["QueId"]).ToString();
            Repeater rep3 = e.Item.FindControl("Repeater3") as Repeater;
            Panel ReplyBtn = (Panel)e.Item.FindControl("ReplyBtn");
            DataTable dts3 = b_guestanswer.Sel(" QueId=" + queid + " and Userid<>" + askMod.UserId, "", null);
            DataTable dts4 = b_guestanswer.Sel(" QueId=" + queid + " and Userid=" + askMod.UserId, "", null);

            rep3.DataSource = b_guestanswer.Sel(" supplymentid=" + Aid + " And Userid<>" + askMod.UserId, "", null);
            rep3.DataBind();
            if (dts3.Rows.Count == dts4.Rows.Count)
            {
                ReplyBtn.Visible = false;
            }
        }
        protected string getstatus(string status)
        {
            switch (status)
            {
                case "1": return "推荐答案";
                case "0": return "";
                default: return "";
            }
        }
        public string GetCate(string CateID)
        {
            string re = "";
            return re;
        }

        protected string getstyle()
        {
            if (buser.CheckLogin())
            {
                return "display:inherit";
            }
            else return "display:none";
        }
        protected string getstyles()
        {
            if (buser.CheckLogin())
            {
                return "display:none";
            }
            else return "display:inherit";
        }
        /// <summary>
        /// 取已解决问题总数
        /// </summary>
        /// <returns></returns>
        public string getSolvedCount()
        {
            return askBll.IsExistInt("Status=2").ToString();
        }
        /// <summary>
        /// 取待解决问题总数
        /// </summary>
        /// <returns></returns>
        public string getSolvingCount()
        {
            return askBll.IsExistInt("Status=1").ToString();
        }
        /// <summary>
        /// 取得用户总数
        /// </summary>
        /// <returns></returns>
        public string getUserCount()
        {
            return buser.GetUserNameListTotal("").ToString();
        }
        /// <summary>
        /// 取得当前在线人数
        /// </summary>
        /// <returns></returns>
        public string getLogined()
        {
            DateTime dtNow = DateTime.Now.AddMinutes(-1);
            if (Application["online"] != null)
                return Application["online"].ToString();
            else
                return "";
        }
        ///<summary>
        ///取得最佳回答采纳率
        ///</summary>
        /// <returns></returns>
        public string getAdoption()
        {
            double adopCount = b_guestanswer.IsExistInt(2);
            double count = b_guestanswer.getnum();
            return ((adopCount / count) * 100).ToString("0.00") + "%";
        }
    }
}