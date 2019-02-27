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
    using ZoomLa.Common;
    

    public partial class SpecContent : System.Web.UI.Page
    {
        private B_SpecInfo bll = new B_SpecInfo();
        private B_Spec bspec = new B_Spec();
        private int SpecID=0;
        private bool IsNoContent = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("ContentSpec"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                if (string.IsNullOrEmpty(base.Request.QueryString["SpecID"]))
                {
                    this.SpecID = this.bspec.GetFirstID();
                    if(this.SpecID==0)
                        function.WriteErrMsg("没有指定专题ID或没有建立专题");
                }
                else
                {
                    this.SpecID = DataConverter.CLng(base.Request.QueryString["SpecID"]);
                }
                this.ViewState["SpecID"] = this.SpecID.ToString();
                RepNodeBind();
            }
        }
        public void RepNodeBind()
        {
            this.SpecID = DataConverter.CLng(this.ViewState["SpecID"].ToString());
            DataTable dt = this.bll.GetSpecContent(this.SpecID);
            if (dt.Rows.Count > 0)
            {
                this.IsNoContent = false;                
            }
            else
            {
                this.IsNoContent = true;
            }
            this.nocontent.Visible = this.IsNoContent;
            this.Egv.DataSource = dt;
            this.Egv.DataKeyNames = new string[] { "SpecInfoID" };
            this.Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Egv.PageIndex = e.NewPageIndex;
            this.RepNodeBind();
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
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
                            
                if (e.CommandName == "Del")
                {
                    string Id = e.CommandArgument.ToString();
                    this.bll.Del(DataConverter.CLng(Id));
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
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    int itemID = Convert.ToInt32(Egv.DataKeys[i].Value);
                    this.bll.Del(itemID);
                }
            }
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
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    string itemID = Egv.Rows[i].Cells[1].Text;
                    if (string.IsNullOrEmpty(SpecInfo))
                        SpecInfo = itemID;
                    else
                        SpecInfo += "," + itemID;
                }
            }
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
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    string itemID = Egv.DataKeys[i].Value.ToString();
                    if (string.IsNullOrEmpty(SpecInfo))
                        SpecInfo = itemID;
                    else
                        SpecInfo += "," + itemID;
                }
            }
            Response.Redirect("MoveToSpec.aspx?specinfo=" + SpecInfo);
        }
}
}