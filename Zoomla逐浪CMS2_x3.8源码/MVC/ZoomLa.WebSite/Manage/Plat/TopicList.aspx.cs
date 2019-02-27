using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Plat
{
    public partial class TopicList : CustomerPageAction
    {
        B_Plat_Topic topicBll = new B_Plat_Topic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                //Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='PlatInfoManage.aspx'>能力中心</a></li><li class='active'><a href='" + Request.RawUrl + "'>话题管理</a></li>");
            }
        }
        private void MyBind()
        {
            EGV.DataSource = topicBll.SelWith(Skey_T.Text);
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del2":
                    int id = Convert.ToInt32(e.CommandArgument);
                    topicBll.Del(id);
                    break;
            }
            MyBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    DataRowView dr = e.Row.DataItem as DataRowView;
            //    e.Row.Attributes.Add("ondblclick", "location='AddEnglishQuestion.aspx?ID=" + dr["ID"] + "'");
            //}
        }
        protected void SetToStar_Btn_Click(object sender, EventArgs e)
        {
            topicBll.UpdateStatus(Request.Form["idchk"], "star", 1);
            MyBind();
        }
        protected void CancelStar_Btn_Click(object sender, EventArgs e)
        {
            topicBll.UpdateStatus(Request.Form["idchk"], "star", 0);
            MyBind();
        }
        protected void SetToSystem_Btn_Click(object sender, EventArgs e)
        {
            topicBll.UpdateStatus(Request.Form["idchk"], "system", 1);
            MyBind();
        }
        protected void CancelSystem_Btn_Click(object sender, EventArgs e)
        {
            topicBll.UpdateStatus(Request.Form["idchk"], "system", 0);
            MyBind();
        }
        //--------------------------
        public string GetMsgContgent()
        {
            string msg = StringHelper.StripHtml(Eval("MsgContent", ""));
            return StringHelper.SubStr(StrHelper.RemoveBySE(msg));
        }
        public string GetStatus(object status)
        {
            if (DataConverter.CLng(status) == 1) { return ComRE.Icon_OK; }
            else { return ""; }
        }
        protected void Search_B_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Skey_T.Text.Trim()))
            {
                sel_box.Attributes.Add("style", "display:inline;");
                EGV.Attributes.Add("style", "margin-top:44px;");
            }
            else
            {
                sel_box.Attributes.Add("style", "display:none;");
                EGV.Attributes.Add("style", "margin-top:0px;");
            }
            MyBind();
        }
    }
}