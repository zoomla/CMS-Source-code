namespace ZoomLa.WebSite.Comment
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
    using ZoomLa.Common;
    
    using ZoomLa.Model;

    public partial class CommentFor : System.Web.UI.Page
    {
        private B_Content bll = new B_Content();
        private B_Comment bcomment = new B_Comment();
        private B_User buser = new B_User();
        private int m_ItemID;
        private string m_Title;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    m_ItemID = DataConverter.CLng(Request.QueryString["ID"]);
                }
                else
                {
                    function.WriteErrMsg("没有指定内容ID!");
                }
                this.HdnItemID.Value = m_ItemID.ToString();
                int p = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["p"]))
                {
                    p = DataConverter.CLng(Request.QueryString["p"]);
                }
                m_Title = bll.GetCommonData(m_ItemID).Title;
                this.HdnTitle.Value = m_Title;                
                RepBind();
            }
        }
        /// <summary>
        /// 绑定评论到Repeater
        /// </summary>
        private void RepBind()
        {
            DataTable dt = this.bcomment.SeachComment_ByIDTitle(m_ItemID, m_Title);
            this.Label1.Text = dt.Rows.Count.ToString();
            this.Label2.Text = this.bcomment.GetComment_CountIDTitlePK(m_ItemID, m_Title, true).ToString();
            this.Label3.Text = this.bcomment.GetComment_CountIDTitlePK(m_ItemID, m_Title, false).ToString();
            this.Repeater1.DataSource = dt;
            this.Repeater1.DataBind();
        }
        /// <summary>
        /// 发表评论
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            if(this.Page.IsValid)
            {
                if (!buser.CheckLogin())
                {
                    function.WriteErrMsg("只有登陆用户才能发表评论！");
                }
                string UserID = HttpContext.Current.Request.Cookies["UserState"]["UserID"];
                M_Comment comment = new M_Comment();
                comment.CommentID = 0;
                comment.GeneralID = DataConverter.CLng(this.HdnItemID.Value);
                m_ItemID = DataConverter.CLng(this.HdnItemID.Value);
                comment.UserID = DataConverter.CLng(UserID);
                comment.Title = this.HdnTitle.Value;
                m_Title = this.HdnTitle.Value;
                comment.Contents = this.TxtContents.Text.Trim();
                comment.Score = DataConverter.CLng(this.DDLScore.SelectedValue);
                comment.PK = DataConverter.CBool(this.RBLPK.SelectedValue);
                comment.Audited = true;
                comment.CommentTime = DateTime.Now;
                bcomment.Add(comment);
                Response.Write("<script language=\"javascript\" type=\"text/javascript\">alert(\"评论添加成功\")</script>");
                RepBind();
            }
        }

        public string GetPK(string pk)
        {
            if (DataConverter.CBool(pk))
            {
                return "我支持";
            }
            else
            {
                return "我反对";
            }
        }
        public string GetUserName(string uid)
        {
            return buser.SeachByID(DataConverter.CLng(uid)).UserName;
        }
    }
}