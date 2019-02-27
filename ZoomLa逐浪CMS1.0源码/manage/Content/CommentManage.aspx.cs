namespace ZoomLa.WebSite.Manage.Content
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
    using ZoomLa.Model;
    using ZoomLa.Components;
    using ZoomLa.Common;

    public partial class CommentManage : System.Web.UI.Page
    {
        private B_Content bll = new B_Content();
        private B_Comment bfav = new B_Comment();
        private B_User buser = new B_User();
        private int m_type;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!this.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("ComentManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                if (string.IsNullOrEmpty(base.Request.QueryString["cType"]))
                {
                    this.m_type = 0;
                }
                else
                {
                    this.m_type = DataConverter.CLng(base.Request.QueryString["cType"]);
                }
                this.ViewState["cType"] = this.m_type.ToString();
                RepNodeBind();
            }

        }
        /// <summary>
        /// 绑定数据到GridView
        /// </summary>
        private void RepNodeBind()
        {
            this.m_type = DataConverter.CLng(this.ViewState["cType"].ToString());
            DataTable dt = new DataTable();
            if (this.m_type == 0)
            {
                dt = this.bfav.SeachComment();
            }
            if (this.m_type == 1)
            {
                dt = this.bfav.SeachCommentAudit(true);
            }
            if (this.m_type == 2)
            {
                dt = this.bfav.SeachCommentAudit(false);
            }
            this.Egv.DataSource = dt;
            this.Egv.DataKeyNames = new string[] { "CommentID" };
            this.Egv.DataBind();
        }
        /// <summary>
        /// 绑定的行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Egv_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Display the company name in italics.
                e.Row.Cells[1].Text = "<i>" + e.Row.Cells[1].Text + "</i>";

            }
        }
        /// <summary>
        /// GridView 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Egv.PageIndex = e.NewPageIndex;
            RepNodeBind();
        }
        /// <summary>
        /// 全部选择控件设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (CheckBox2.Checked == true)
                {
                    cbox.Checked = true;
                }
                else
                {
                    cbox.Checked = false;
                }
            }
        }
        public string GetUrl(string infoid)
        {
            int p = DataConverter.CLng(infoid);
            M_CommonData cinfo = this.bll.GetCommonData(p);
            if (cinfo.IsCreate == 1)
                return SiteConfig.SiteInfo.SiteUrl + cinfo.HtmlLink;
            else
                return "~/Content.aspx?ItemID=" + p;
        }
        public string GetTitle(string infoid)
        {
            int p = DataConverter.CLng(infoid);
            M_CommonData cinfo = this.bll.GetCommonData(p);
            return cinfo.Title;
        }
        public string GetUserName(string userid)
        {
            return this.buser.SeachByID(DataConverter.CLng(userid)).UserName;
        }
        protected void BtnAllComment_Click(object sender, EventArgs e)
        {
            //所有评论
            this.m_type = 0;
            this.ViewState["cType"] = this.m_type.ToString();
            RepNodeBind();
        }
        protected void BtnUNAuditedComment_Click(object sender, EventArgs e)
        {
            //有待审核评论的内容
            this.m_type = 2;
            this.ViewState["cType"] = this.m_type.ToString();
            RepNodeBind();
        }
        protected void BtnAuditedComment_Click(object sender, EventArgs e)
        {
            //评论已全部审核的内容
            this.m_type = 1;
            this.ViewState["cType"] = this.m_type.ToString();
            RepNodeBind();
        }



        protected void BtnSubmit1_Click(object sender, EventArgs e)
        {
            //删除选定的评论
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    int itemID = Convert.ToInt32(Egv.DataKeys[i].Value);
                    this.bfav.Delete(itemID);
                }
            }
            RepNodeBind();
        }
        protected void BtnSubmit2_Click(object sender, EventArgs e)
        {
            //审核通过选定的评论
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    int itemID = Convert.ToInt32(Egv.DataKeys[i].Value);
                    this.bfav.Update_ByAudited_ID(itemID, true);
                }
            }
            RepNodeBind();
        }
        protected void BtnSubmit3_Click(object sender, EventArgs e)
        {
            //取消审核选定的评论
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    int itemID = Convert.ToInt32(Egv.DataKeys[i].Value);
                    this.bfav.Update_ByAudited_ID(itemID, false);
                }
            }
            RepNodeBind();
        }

        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            
                if (e.CommandName == "Audit")
                {
                    string Id = e.CommandArgument.ToString();
                    this.bfav.Update_ByAudited_ID(DataConverter.CLng(Id), true);
                }
                if (e.CommandName == "Del")
                {
                    string Id = e.CommandArgument.ToString();
                    this.bfav.Delete(DataConverter.CLng(Id));
                }
                RepNodeBind();
           
        }
    }
}