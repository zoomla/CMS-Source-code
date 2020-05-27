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
using System.Web.UI.HtmlControls;
using ZoomLa.Components;


namespace ZoomLaCMS.Guest.Ask
{
    public partial class SearchDetails : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Ask b_Ask = new B_Ask();
        M_GuestAnswer m_guestanswer = new M_GuestAnswer();
        B_GuestAnswer b_guestanswer = new B_GuestAnswer();
        M_AskCommon m_Askcom = new M_AskCommon();
        B_AskCommon b_Askcom = new B_AskCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin(false);
            if (!this.Page.IsPostBack)
            {
                int QuestionId = DataConverter.CLng(Request["ID"]);
                B_Ask ask = new B_Ask();
                question.Text = ask.SelReturnModel(QuestionId).Qcontent;
                if (ask.SelReturnModel(QuestionId).Supplyment == "")
                {
                    supdiv.Visible = false;
                }
                supment.Text = ask.SelReturnModel(QuestionId).Supplyment;
                if (ask.SelReturnModel(QuestionId).IsNi.ToString() == "1") username.Text = "匿名";
                else username.Text = "<a href='../../ShowList.aspx?id=" + ask.SelReturnModel(QuestionId).UserId + "' target='_blank'>" + ask.SelReturnModel(QuestionId).UserName + "</a>";
                addtime.Text = (ask.SelReturnModel(QuestionId).AddTime).ToString();
                DataTable dt = b_guestanswer.Sel(" QueId=" + QuestionId + " And Status=1", " Status desc", null);
                dBind(dt);
                dt = b_guestanswer.Sel(" QueId=" + QuestionId + " And Status=0", " AddTime desc", null);
                dBind2(dt);
                dt = b_Ask.Sel("Status=1", "Addtime desc", null);
                dBind3(dt);
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
        private void dBind2(DataTable dt)
        {

            this.Repeater2.DataSource = dt;
            this.Repeater2.DataBind();
            if (dt != null)
            {
                dt.Dispose();
            }
        }
        private void dBind3(DataTable dt)
        {

            this.Repeater3.DataSource = dt;
            this.Repeater3.DataBind();
            if (dt != null)
            {
                dt.Dispose();
            }
        }

        public string GetUserName(string UserID)
        {
            return buser.SeachByID(DataConverter.CLng(UserID)).UserName;
        }
        protected void Repeater1_ItemDataBound1(object sender, RepeaterItemEventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            DataRowView rowv = (DataRowView)e.Item.DataItem;//找到分类Repeater关联的数据项 
            Label Isname = (Label)e.Item.FindControl("Isname");
            if (rowv["isNi"].ToString() == "1")
            { Isname.Text = "匿名"; }
            LinkButton lbtApproval = e.Item.FindControl("lbtApproval") as LinkButton;
            string Aid = (rowv["ID"]).ToString();
            hfsid.Value = Aid;
            DataTable dt = b_Askcom.U_SelByAnswer(mu.UserID, DataConvert.CLng(rowv["ID"]), 0);
            if (dt != null && dt.Rows.Count > 0)
            {
                lbtApproval.Text = dt.Rows.Count.ToString();
            }
            else { lbtApproval.Text = "0"; }
            DataTable dt1 = b_Askcom.U_SelByAnswer(mu.UserID, DataConvert.CLng(rowv["ID"]), 1);
            Repeater RepeaterC = e.Item.FindControl("RepeaterC") as Repeater;
            RepeaterC.DataSource = dt1;
            RepeaterC.DataBind();
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
                return "display:inline-table";
            }
            else return "display:none";
        }
        protected string getstyles()
        {
            if (buser.CheckLogin())
            {
                return "display:none";
            }
            else return "display:inline-table";
        }
        /// <summary>
        /// 取已解决问题总数
        /// </summary>
        /// <returns></returns>
        public string getSolvedCount()
        {
            return b_guestanswer.IsExistInt(2).ToString();
        }
        /// <summary>
        /// 取待解决问题总数
        /// </summary>
        /// <returns></returns>
        public string getSolvingCount()
        {
            return b_guestanswer.IsExistInt(1).ToString();
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
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "subComment")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                DataTable dt = b_guestanswer.Sel("ID=" + id, "", null);
                TextBox txtSupplyment = e.Item.FindControl("txtSupplyment") as TextBox;
                if (insertCom(id, Convert.ToInt32(dt.Rows[0]["QueId"]), 1, txtSupplyment.Text) == 1)
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('评论成功！');location.href='SearchDetails.aspx?ID=" + Request["ID"] + "';</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('评论失败，请重试！');</script>");
                }
            }
            if (e.CommandName == "Approval")
            {
                M_UserInfo mu = buser.GetLogin();
                int id = Convert.ToInt32(e.CommandArgument);
                DataTable dt1 = b_Askcom.U_SelByAnswer(id, mu.UserID, 0);
                if (dt1.Rows.Count > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('您已经赞同过了~~');</script>");
                }
                else
                {
                    DataTable dt = b_guestanswer.Sel("ID=" + id, "", null);
                    if (insertCom(id, Convert.ToInt32(dt.Rows[0]["QueId"]), 0, "赞同") == 1)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('已赞同！');location.href='SearchDetails.aspx?ID=" + Request["ID"] + "';</script>");
                        int QuestionId = DataConverter.CLng(Request["ID"]);
                        DataTable dt2 = b_guestanswer.Sel(" QueId=" + QuestionId + " And supplymentid=0", " Status desc", null);
                        dBind(dt2);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('赞同失败，请重试！');</script>");
                    }
                }
            }
        }
        protected void RepeaterC_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView drv = e.Item.DataItem as DataRowView;
            M_UserInfo muser = buser.SeachByID(Convert.ToInt32(drv.Row["UserID"]));
            HtmlGenericControl lbUser = e.Item.FindControl("lbUser") as HtmlGenericControl;
            lbUser.InnerHtml = muser.UserName.ToString() + ":";
        }

        protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "subComment")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                DataTable dt = b_guestanswer.Sel("ID=" + id, "", null);
                TextBox txtSupplyment = e.Item.FindControl("txtSupplyment") as TextBox;
                if (insertCom(id, Convert.ToInt32(dt.Rows[0]["QueId"]), 1, txtSupplyment.Text) == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('评论成功！');location.href='SearchDetails.aspx?ID=" + Request["ID"] + "';showComment(" + id + ")</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('评论失败，请重试！');</script>");
                }
            }
            if (e.CommandName == "Approval")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                DataTable dt1 = b_Askcom.U_SelByAnswer(id, buser.GetLogin().UserID, 0);
                if (dt1.Rows.Count > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('您已经赞同过了~~');</script>");
                }
                else
                {
                    DataTable dt = b_guestanswer.Sel("ID=" + id, "", null);
                    if (insertCom(id, Convert.ToInt32(dt.Rows[0]["QueId"]), 0, "赞同") == 1)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('已赞同！');location.href='SearchDetails.aspx?ID=" + Request["ID"] + "';showComment(" + id + ")</script>");
                        int QuestionId = DataConverter.CLng(Request["ID"]);
                        DataTable dt2 = b_guestanswer.Sel(" QueId=" + QuestionId + " And supplymentid=0", " Status desc", null);
                        dBind2(dt2);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('赞同失败，请重试！');</script>");
                    }
                }
            }
        }
        /// <summary>
        /// 获取回答数
        /// </summary>
        /// <returns></returns>
        protected string getanswer(string uid)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("uid", uid) };
            DataTable dt = b_guestanswer.Sel("QueID=@uid", "", sp);

            return dt.Rows.Count.ToString();
        }
        //切字
        public string GetLeftString(string str, int length)
        {
            if (str.Length <= length)
                return str;
            return str.Substring(0, length) + "..."; ;
        }

        public int insertCom(int id, int qid, int Type, string supp)
        {
            m_Askcom.AskID = qid;
            m_Askcom.AswID = id;
            m_Askcom.UserID = buser.GetLogin().UserID;
            m_Askcom.Content = supp;
            m_Askcom.AddTime = DateTime.Now;
            m_Askcom.Type = Type;
            int flag = b_Askcom.insert(m_Askcom);
            return flag;
        }
    }
}