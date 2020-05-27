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
using ZoomLa.SQLDAL;


namespace ZoomLaCMS.Manage.Content
{
    public partial class SpecContent : CustomerPageAction
    {
        private B_Spec bspec = new B_Spec();
        B_Content conbll = new B_Content();
        protected M_Spec specMod = new M_Spec();
        private bool IsNoContent = true;
        public int SpecID { get { return DataConvert.CLng(Request.QueryString["SpecID"]); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            string sname = "";
            B_Admin badmin = new B_Admin();
            if (!B_ARoleAuth.Check(ZLEnum.Auth.content, "ContentSpec"))
                function.WriteErrMsg("没有权限进行此项操作");
            if (!this.Page.IsPostBack)
            {
                specMod = bspec.SelReturnModel(SpecID);
                if (specMod != null)
                    sname = specMod.SpecName;
                this.ViewState["SpecID"] = this.SpecID.ToString();
                this.ViewState["type"] = 1;

                RepNodeBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='SpecialManage.aspx'>专题管理</a></li><li class='active'>" + sname + "</li>" + Call.GetHelp(98));
        }
        public void RepNodeBind()
        {
            string order = this.ddlOrder.SelectedValue;
            string conditions = this.ddlConditions.SelectedValue;
            int type = DataConverter.CLng(this.ViewState["type"].ToString());
            DataTable dt = this.bspec.GetSpecContent(this.SpecID, order, conditions);
            if (dt.Rows.Count > 0)
            {
                this.IsNoContent = false;
            }
            else
            {
                this.IsNoContent = true;
            }
            this.nocontent.Visible = this.IsNoContent;
            this.ExGridView1.DataSource = dt;
            this.ExGridView1.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ExGridView1.PageIndex = e.NewPageIndex;
            RepNodeBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string Id = e.CommandArgument.ToString();
                M_CommonData content = conbll.GetCommonData(DataConvert.CLng(Id));
                content.SpecialID = content.SpecialID.Replace("," + ViewState["SpecID"] + ",", "");
                conbll.UpdateByID(content);
                RepNodeBind();
            }

        }
        public string GetElite(string Elite)
        {
            if (DataConverter.CLng(Elite) > 0)
                return "推荐";
            else
                return "未推荐";
        }
        public string GetStatus(string status)
        {
            int s = DataConverter.CLng(status);
            if (s == 0)
                return "待审核";
            if (s == 99)
                return "已审核";
            if (s == -2)
                return "回收站";
            return "退档";
        }
        public string GetCteate(string IsCreate)
        {
            int s = DataConverter.CLng(IsCreate);
            if (s != 1)
                return "<font color=red>×</font>";
            else
                return "<font color=green>√</font>";
        }
        /// <summary>
        /// 从所在专题删除所有选中项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            conbll.DelSpecID(Request.Form["idchk"], DataConvert.CLng(ViewState["SpecID"]));
            RepNodeBind();
        }
        /// <summary>
        /// 添加到其他专题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddTo_Click(object sender, EventArgs e)
        {
            string SpecInfo = "";
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    SpecInfo += chkArr[i] + ",";
                }
            }
            SpecInfo = SpecInfo.TrimEnd(',');
            Response.Redirect("AddToSpec.aspx?InfoIDs=" + SpecInfo);
        }
        /// <summary>
        /// 移动到其他专题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMoveTo_Click(object sender, EventArgs e)
        {
            string SpecInfo = "";
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    SpecInfo += chkArr[i] + ",";
                }
            }
            SpecInfo = SpecInfo.TrimEnd(',');
            Response.Redirect("MoveToSpec.aspx?specinfo=" + SpecInfo);
        }
        //protected void LbtnAllPub_Click(object sender, EventArgs e)
        //{
        //    this.ViewState["type"] = 1;
        //    RepNodeBind();
        //}
        //protected void LbtnUNAuditedPub_Click(object sender, EventArgs e)
        //{
        //    this.ViewState["type"] = 2;
        //    RepNodeBind();
        //}
        //protected void LbtnuditedPub_Click(object sender, EventArgs e)
        //{
        //    this.ViewState["type"] = 3;
        //    RepNodeBind();
        //}
        protected void ddlConditions_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RepNodeBind();
        }
        protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RepNodeBind();
        }
        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("ondblclick", "change(" + e.Row.Cells[1].Text + ")");
                e.Row.Attributes.Add("style", "cursor:pointer;");
                e.Row.Attributes.Add("title", "双击修改");
            }
        }
        //获取选中的checkbox
        private string[] GetChecked()
        {
            if (!string.IsNullOrEmpty(Request.Form["chkSel"]))
            {
                string[] chkArr = Request.Form["chkSel"].Split(',');
                return chkArr;
            }
            else
                return null;
        }
    }
}