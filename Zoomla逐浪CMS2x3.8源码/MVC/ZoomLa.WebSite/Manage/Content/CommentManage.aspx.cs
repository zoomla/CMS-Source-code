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
using System.Collections.Generic;

namespace ZoomLaCMS.Manage.I.ASCX
{
    public partial class CommentManage : CustomerPageAction
    {
        protected B_Content contentBll = new B_Content();
        protected B_Comment commentBll = new B_Comment();
        protected B_User userBll = new B_User();
        public int m_type;
        protected B_Role roleBll = new B_Role();
        protected B_NodeRole noderoleBll = new B_NodeRole();
        protected B_Admin adminBll = new B_Admin();
        protected B_Node nodeBll = new B_Node();
        protected int NodeID;
        protected PagedDataSource pds = new PagedDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "ComentManage");
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>" + Resources.L.工作台 + "</a></li><li><a href='ContentManage.aspx'>" + Resources.L.内容管理 + "</a></li><li class='active'><a href='" + Request.RawUrl + "'>" + Resources.L.评论管理 + "</a></li>" + Call.GetHelp(97));
                MyBind();
            }
        }
        protected void MyBind(string key = "")
        {
            string type = Request.QueryString["type"];
            int nodeid = DataConverter.CLng(Request.QueryString["NodeID"]);
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(type))
            {
                switch (type)
                {
                    case "0":
                        dt = commentBll.SeachAllComment();
                        break;
                    case "1":
                        dt = commentBll.SeachAllComment(true);
                        break;
                    case "2":
                        dt = commentBll.SeachAllComment(false);
                        break;
                }
            }
            else
            {
                dt = commentBll.SeachAllComment();
            }
            Egv.DataSource = dt;
            Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "del2":
                    string Id = e.CommandArgument.ToString();
                    this.commentBll.Delete(DataConverter.CLng(Id));
                    break;
                case "audit":
                    commentBll.Update_ByAudited_ID(DataConverter.CLng(e.CommandArgument.ToString()), true);
                    break;
                case "show":
                    Response.Redirect("CommentShow.aspx?id=" + e.CommandArgument.ToString());
                    break;
            }
            MyBind();
        }
        public string GetContent()
        {
            string str = Eval("Contents").ToString();
            return str.Length > 25 ? str.Substring(0, 25) + "..." : str;
        }
        public string GetUrl()
        {
            string gid = Eval("GeneralID").ToString();
            string link = Eval("HtmlLink").ToString();
            link = string.IsNullOrEmpty(link) ? "/Item/" + gid + ".aspx" : link;
            return link;
        }
        //获取状态
        public string getcommend(object aa)
        {
            string aaa = aa.ToString();
            switch (aaa)
            {
                case "True":
                    return Resources.L.已审核;

                case "False":
                    return Resources.L.待审核;
                default:
                    return Resources.L.待审核;
            }
        }
        //获取用户名
        public string GetUserName(string userid)
        {
            return userBll.SeachByID(DataConverter.CLng(userid)).UserName;
        }
        //批量处理
        protected void BtnSubmit1_Click(object sender, EventArgs e)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "ComentManage");
            //删除选定的评论            
            string ids = "";
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    ids += chkArr[i] + ",";
                    commentBll.Delete(Convert.ToInt32(chkArr[i]));
                }
            }
            ids = ids.Trim(',');
            MyBind();
        }
        protected void BtnSubmit2_Click(object sender, EventArgs e)
        {
            //审核通过选定的评论
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    commentBll.Update_ByAudited_ID(Convert.ToInt32(chkArr[i]), true);
                }
            }
            MyBind();
        }
        protected void BtnSubmit3_Click(object sender, EventArgs e)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "ComentManage");
            //取消审核选定的评论
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    commentBll.Update_ByAudited_ID(Convert.ToInt32(chkArr[i]), false);
                }
            }
            MyBind();
        }
        //获取选中的checkbox
        private string[] GetChecked()
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                string[] chkArr = Request.Form["idchk"].Split(',');
                return chkArr;
            }
            else
                return null;
        }
    }
}