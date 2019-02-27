using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Data;

namespace ZoomLaCMS.Manage.Shop.Arrive
{
    public partial class ArriveManage : CustomerPageAction
    {
        B_Arrive avBll = new B_Arrive();
        B_Admin badmin = new B_Admin();
        public int State { get { return DataConverter.CLng(Request.QueryString["state"], -100); } }
        public int ZType { get { return DataConverter.CLng(Request.QueryString["type"], -100); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='../ProductManage.aspx'>商城管理</a></li><li>优惠劵管理   <a href=\"AddArrive.aspx?menu=add\">[新增优惠劵]</a></li>");
            }
        }
        private void MyBind()
        {
            EGV.DataSource = avBll.Sel(ZType, State, "", Name_T.Text.Trim(), AgainTime_T.Text, EndTime_T.Text);
            EGV.DataBind();
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                avBll.DelByIDS(ids);
            }
            function.WriteSuccessMsg("操作成功");
        }
        protected void batActive_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                avBll.ActiveByIDS(ids);
            }
            function.WriteSuccessMsg("操作成功");
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
                    avBll.GetDelete(id);
                    break;
            }
            MyBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                e.Row.Attributes.Add("ondblclick", "location='AddArrive.aspx?ID=" + dr["ID"] + "'");
            }
        }
        public string GetState() { return avBll.GetStateStr(Convert.ToInt32(Eval("state"))); }
        protected void Skey_Btn_Click(object sender, EventArgs e)
        {
            MyBind();
        }
    }
}