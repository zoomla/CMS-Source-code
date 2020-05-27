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

namespace ZoomLaCMS.Guest.Ask
{
    public partial class Interactive : System.Web.UI.Page
    {
        B_User buser = new B_User();
        M_Ask m_Ask = new M_Ask();
        B_Ask askBll = new B_Ask();
        M_GuestAnswer m_guestanswer = new M_GuestAnswer();
        B_GuestAnswer b_guestanswer = new B_GuestAnswer();
        DataTable dt = new DataTable();
        public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (buser.CheckLogin())
                {
                    M_UserInfo mu = buser.GetLogin();
                    this.user.Text = buser.GetLogin().UserName;
                }
                M_Ask askmod = askBll.SelReturnModel(Mid);
                if (askmod.Status == 0) function.WriteErrMsg("该问题未经过审核!");
                if (askmod.Supplyment == "")
                {
                    supdiv.Visible = false;
                }
                question.Text = askmod.Qcontent;
                supment.Text = askmod.Supplyment;
                if (askmod.IsNi.ToString() == "1") username.Text = "匿名";
                else username.Text = "<a href='../../ShowList.aspx?id=" + askmod.UserId + "' target='_blank'>" + askmod.UserName + "</a>";
                addtime.Text = (askmod.AddTime).ToString();
                Txtsupment.Text = (askmod.Supplyment).ToString();
                dt = b_guestanswer.Sel(" QueId=" + Mid + " And supplymentid=0", " Status desc", null);
                dBind(dt);

            }
        }

        private void dBind(DataTable dt)
        {

            this.Repeater1.DataSource = dt;
            this.Repeater1.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                M_GuestBook info = new M_GuestBook();
                if (buser.CheckLogin())
                {
                    m_guestanswer.UserId = DataConverter.CLng(buser.GetLogin().UserID);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('登录后才能对问题进行追问！');location.href='/User/Login.aspx?ReturnUrl='+location.href;</script>");
                    Response.End();
                }
            }
            m_guestanswer.QueId = DataConverter.CLng(Request["ID"]);
            m_guestanswer.Content = this.Request.Form["txtSupplyment"];
            m_guestanswer.AddTime = DateTime.Now;
            m_guestanswer.UserId = buser.GetLogin().UserID;
            m_guestanswer.UserName = buser.GetLogin().UserName;
            m_guestanswer.Status = 0;
            m_guestanswer.audit = 0;
            m_guestanswer.supplymentid = DataConverter.CLng(Request.Form["Rid"]);
            b_guestanswer.insert(m_guestanswer);
            function.WriteSuccessMsg("追问成功!", "Interactive.aspx?ID=" + Request["ID"]);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            askBll.UpdateByField("Supplyment", Txtsupment.Text.Trim(), Mid);
            function.WriteSuccessMsg("提交成功");
        }
        public string GetUserName(string UserID)
        {
            return buser.SeachByID(DataConverter.CLng(UserID)).UserName;
        }
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "recomand")
            {
                m_guestanswer = b_guestanswer.SelReturnModel(id);
                M_UserInfo answeuser = buser.SelReturnModel(m_guestanswer.UserId);//回答人用户模型
                m_guestanswer.Status = 1;
                b_guestanswer.UpdateByID(m_guestanswer);
                M_Ask mask = askBll.SelReturnModel(Mid);
                mask.Status = 2;      //问题状态设置为已解决
                if (askBll.SelReturnModel(Convert.ToInt32(Request["ID"])).Score > 0)
                {
                    buser.ChangeVirtualMoney(m_guestanswer.UserId, new M_UserExpHis()//悬赏积分
                    {
                        score = mask.Score,
                        ScoreType = (int)M_UserExpHis.SType.Point,
                        detail = answeuser.UserName + "对问题[" + mask.Qcontent + "]的回答被推荐为满意答案,增加悬赏积分+" + mask.Score.ToString()
                    });
                }
                buser.ChangeVirtualMoney(m_guestanswer.UserId, new M_UserExpHis()//问答积分
                {
                    score = GuestConfig.GuestOption.WDOption.WDRecomPoint,
                    ScoreType = (int)((M_UserExpHis.SType)(Enum.Parse(typeof(M_UserExpHis.SType), GuestConfig.GuestOption.WDOption.PointType))),
                    detail = answeuser.UserName + "对问题[" + mask.Qcontent + "]的回答被推荐为满意答案,增加问答积分+" + GuestConfig.GuestOption.WDOption.WDRecomPoint
                });
                askBll.UpdateByID(mask);
                function.WriteSuccessMsg("推荐成功！", "Interactive.aspx?ID=" + Mid);
            }
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)//指触发的类型为DadaList中的基本行或内容行
            {
                Repeater rep = e.Item.FindControl("Repeater2") as Repeater;//找到里层的repeater对象
                DataRowView rowv = (DataRowView)e.Item.DataItem;//找到分类Repeater关联的数据项 
                Label Isname = (Label)e.Item.FindControl("Isname");
                if (rowv["isNi"].ToString() == "1")
                { Isname.Text = "匿名"; }
                string Aid = (rowv["ID"]).ToString();
                hfsid.Value = Aid;
                M_Ask askMod = askBll.SelReturnModel(Mid);
                //获取填充子类的id 
                SqlParameter[] sp1 = new SqlParameter[] { new SqlParameter("@id", hfsid.Value) };
                SqlParameter[] sp2 = new SqlParameter[] { new SqlParameter("@id", Aid) };
                DataTable dts = b_guestanswer.Sel(" supplymentid=@id And Userid=" + askMod.UserId, "", sp1);
                DataTable dts2 = b_guestanswer.Sel(" supplymentid=@id And Userid=" + askMod.UserId, "", sp2);
                rep.DataSource = dts;
                rep.DataBind();
                Panel ReplyBtn1 = (Panel)e.Item.FindControl("ReplyBtn1");
                LinkButton lb = (LinkButton)e.Item.FindControl("recommand");
                if (askMod.Status == 2)//当问题为已解决时
                {
                    ReplyBtn1.Visible = false;
                    lb.Visible = false;
                }
            }
        }
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int QuestionId = DataConverter.CLng(Request["ID"]);
            B_Ask ask = new B_Ask();
            DataTable dt = ask.Sel(" ID=" + QuestionId, "", null);
            DataRowView rowv = (DataRowView)e.Item.DataItem;
            Repeater rep3 = e.Item.FindControl("Repeater3") as Repeater;
            string Aid = (rowv["ID"]).ToString();
            SqlParameter[] sp2 = new SqlParameter[] { new SqlParameter("@id", Aid) };
            DataTable dts2 = b_guestanswer.Sel(" supplymentid=@id And Userid=" + dt.Rows[0]["Userid"], "", sp2);
            rep3.DataSource = b_guestanswer.Sel(" supplymentid=@id And Userid<>" + dt.Rows[0]["Userid"], "", sp2);
            rep3.DataBind();
        }
        protected string getstatus(string status)
        {
            switch (status)
            {
                case "2": return "推荐答案";
                case "1": return "";
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
                return "";
            }
            else return "display:none";
        }
        protected string getstyles()
        {
            if (buser.CheckLogin())
            {
                return "display:none";
            }
            else return "";
        }
        // 取已解决问题总数
        public string getSolvedCount()
        {
            return askBll.GetCountByStatus(1).ToString();
        }
        /// 取待解决问题总数
        public string getSolvingCount()
        {
            return askBll.GetCountByStatus(0).ToString();
        }
        // 取得用户总数
        public string getUserCount()
        {
            return buser.Sel().Rows.Count.ToString();
        }
        // 取得当前在线人数
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
            double adopCount = Convert.ToDouble(b_guestanswer.GetCountByStatus(1));
            double count = Convert.ToDouble(b_guestanswer.Sel().Rows.Count);
            return ((adopCount / count) * 100).ToString("0.00") + "%";
        }
    }
}