using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Data.SqlClient;

namespace ZoomLaCMS.MIS.Memo
{
    public partial class MemoDetail : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_MisInfo bll = new B_MisInfo();
        B_Comment Bcom = new B_Comment();
        M_Comment Mcom = new M_Comment();
        M_MisInfo model = new M_MisInfo();
        M_UserInfo ui = new M_UserInfo();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request["MID"]);
            model = bll.SelReturnModel(id);
            if (!this.Page.IsPostBack)
            {
                this.lblWarns.Attributes.Add("onclick", "PopupDiv('div_warn','lblWarns', 'warn')");
                //this.txtShare.Value = model.IsShare;
                dt = bll.Sel(id);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lblInputer.Text = dt.Rows[0]["Inputer"].ToString();
                    lblCreateTime.Text = dt.Rows[0]["CreateTime"].ToString();
                    lblIsShare.Text = dt.Rows[0]["IsShare"].ToString();
                    lblContent.Text = dt.Rows[0]["Content"].ToString();
                    lblWarn.Text = dt.Rows[0]["IsWarn"].ToString();
                    lblWarnContent.Text = dt.Rows[0]["Content"].ToString();
                    lblPeson.Text = dt.Rows[0]["IsShare"].ToString();
                    lblWarnTime.Text = dt.Rows[0]["IsWarn"].ToString();
                    if (string.IsNullOrEmpty(dt.Rows[0]["IsShare"].ToString()))
                    {
                        this.lblShares.Visible = true;
                        this.LinkBtn.Visible = false;
                        this.lblShares.Attributes.Add("onclick", "PopupDiv('div_share','lblShares', 'share')");
                    }
                    else
                    {
                        this.lblShares.Visible = false;
                        this.LinkBtn.Visible = true;
                    }
                    //获取用户数据源
                }
                if (ViewState["PageIndex"] == null)
                    ViewState["PageIndex"] = 0;
                //ViewState["UserSource"] = buser.GetNamesList();
                UserDataBind();
                CommentLisDataBind();
            }
        }

        protected void CommentLisDataBind()
        {

        }

        protected void UserDataBind()
        {
            int index = Convert.ToInt32(ViewState["PageIndex"]);
            PagedDataSource pdsUsers = new PagedDataSource();
            pdsUsers.DataSource = (ViewState["UserSource"] as List<string>);
            pdsUsers.AllowPaging = true;
            pdsUsers.PageSize = 5;
            pdsUsers.CurrentPageIndex = index;
            rptUserLists.DataSource = pdsUsers;
            rptUserLists.DataBind();
            if (pdsUsers.IsLastPage)
                btnNext.Enabled = false;
        }

        protected void Repeater1_ItemCommmand(object sender, RepeaterCommandEventArgs e)
        {

        }

        protected string getUserInfo(string uid)
        {
            M_UserInfo mu = buser.GetSelect(DataConverter.CLng(uid));
            string str = "";
            if (mu != null && !mu.IsNull)
                str = mu.UserName;
            return str;
        }
        protected string getUserface(string uid)
        {
            return buser.GetSelect(DataConverter.CLng(uid)).UserFace;
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = Convert.ToInt32(ViewState["PageIndex"]) + 1;
            UserDataBind();
        }

        protected void BtnYes_Click(object sender, EventArgs e)
        {
        }

        protected void LinkBtn_Click(object sender, EventArgs e)
        {

        }
        protected void BtnWarns_Click(object sender, EventArgs e)
        {

        }
        protected void LinkEdit_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request["MID"]);
            Response.Redirect("AddMemo.aspx?ID=" + id);
        }

        protected void LinkDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request["MID"]);
            dt = bll.Sel(id);
            if (dt != null)
            {
                bll.Del(id);
                Response.Write("<script>alert('删除成功');location.href='Default.aspx'</script>");
            }
        }

        protected void LinkBtnBack_Click(object sender, EventArgs e)
        {

        }
        protected void SubBtn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request["MID"]);
            model = bll.SelReturnModel(id);
            ui = buser.GetUserIDByUserName(buser.GetLogin().UserName);
            Mcom.Title = model.Title;
            Mcom.GeneralID = model.ID;
            Mcom.CommentTime = DateTime.Now;
            Mcom.Contents = this.TxtComment.Text.Trim();
            Mcom.UserID = ui.UserID;
            Mcom.Type = 3;
            if (Bcom.insert(Mcom) > 0)
            {

                Response.Write("<script>alert('评论成功');location.href='MemoDetail.aspx?MID=" + id + "'</script>");
            }
            else
            {
                Response.Write("<script>alert('评论失败');location.href='MemoDetail.aspx?MID=" + id + "'</script>");
            }
            CommentLisDataBind();
        }

        protected string GetContents()
        {
            int id = Convert.ToInt32(this.HidCommentInfo.Value);
            Mcom = Bcom.SelReturnModel(id);
            return Mcom.Contents;
        }

        protected void BtnSub_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.HidCommentInfo.Value);
            int ids = Convert.ToInt32(Request["MID"]);
            Mcom = Bcom.SelReturnModel(id);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Contents", this.Txtco.Text) };

        }
    }
}