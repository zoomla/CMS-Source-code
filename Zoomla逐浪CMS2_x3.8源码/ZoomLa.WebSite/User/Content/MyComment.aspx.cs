namespace ZoomLa.WebSite.User.Content
{
    using System;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using SQLDAL;
    public partial class MyComment : System.Web.UI.Page
    {
        B_Content conBll = new B_Content();
        B_User buser = new B_User();
        B_Comment cmtBll = new B_Comment();
        public int NodeID {get{return DataConvert.CLng(Request.QueryString["NodeID"]);} }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            M_UserInfo mu = buser.GetLogin();
            EGV.DataSource = cmtBll.SelPage(1, int.MaxValue, NodeID, 0, mu.UserID).dt;
            EGV.DataKeyNames = new string[] { "CommentID" };
            EGV.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void Bat_Del_Click(object sender, EventArgs e)
        {
            M_UserInfo mu=buser.GetLogin();
            cmtBll.U_Del(mu.UserID,Request.Form["idchk"]);
            function.WriteSuccessMsg("删除评论成功");
        }
    }
}