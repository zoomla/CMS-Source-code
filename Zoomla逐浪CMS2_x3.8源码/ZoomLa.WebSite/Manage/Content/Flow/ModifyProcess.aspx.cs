namespace ZoomLa.WebSite.Manage.Content
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using System.Data;

    public partial class ModifyProcess : CustomerPageAction
    {
        B_Role br = new B_Role();
        B_Process bp = new B_Process();
        B_AuditingState ba = new B_AuditingState();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                string flowId = Request.QueryString["flowId"];             
                this.lblFlow.InnerHtml = Request.QueryString["flowName"];                               
                if (id != null && !"".Equals(id))
                {
                    int ids = Convert.ToInt32(id);
                    BindDatas(ids);
                }
                Call.SetBreadCrumb(Master, "<li>系统设置</li><li><a href='FlowManager.aspx'>流程管理</a></li><li><a href='ModifyFlow.aspx?id="+flowId+"'>修改流程步骤</a></li>");
            }
        }

        int stateCode = 0;
        private void BindDatas(int id)
        {
            M_Process mp = bp.SelReturnModel(Convert.ToInt32(id));
            this.txtPName.Text = mp.PName;
            this.FlowPrcess.Value = mp.PDepcit;
            this.txtPassCode.Text = mp.PPassName;
            this.ddlPassCode.SelectedValue = mp.PPassCode.ToString();
            this.txtNoPassCode.Text = mp.PNoPassName;
            this.ddlNoPassCode.SelectedValue = mp.PNoPassCode.ToString();
            stateCode = mp.PCode;
            this.txtPCode.Value = mp.PCode.ToString();
            this.txtNoPassCode.Text = mp.PNoPassName;
            this.needCode_Text.Text = mp.NeedCode.ToString();
            BindCheckBoxList(mp.PRole);
        }
        private void BindCheckBoxList(string roles)
        {
            cblRole.DataSource = br.GetRoleAll();
            cblRole.DataTextField = "RoleName";
            cblRole.DataValueField = "RoleID";
            cblRole.DataBind();
            if (string.IsNullOrEmpty(roles)) return;
            else { roles = "," + roles; }
            for (int i = 0; i < cblRole.Items.Count; i++)
            {
                string value=","+cblRole.Items[i].Value+",";
                if (roles.Contains(value)) { cblRole.Items[i].Selected = true; }
            }
        }
        protected void btnModify_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            string name = this.txtPName.Text.Trim();
            string flowPrcess = this.FlowPrcess.Value.Trim();
            List<string> list = new List<string>();
            foreach (ListItem item in this.cblRole.Items)
            {
                if (item.Selected)
                {
                    list.Add(item.Value);
                }
            }
            if (this.txtPCode.Value.Trim() != null && !"".Equals(this.txtPCode.Value.Trim()))
            {
                stateCode = Convert.ToInt32(this.txtPCode.Value.Trim());
            }
            string passName = this.txtPassCode.Text.Trim();
            string passCode = this.ddlPassCode.SelectedValue.Trim();
            string NoPassName = this.txtNoPassCode.Text.Trim();
            string NoPassCode = this.ddlNoPassCode.SelectedValue.Trim();

            string roleId = null;

            foreach (string str in list)
            {
                roleId += str + ",";
            }

            M_Process mp = bp.GetProcessById(Convert.ToInt32(id));
            mp.PName = name;
            mp.PDepcit = flowPrcess;
            if (roleId != null && !"".Equals(roleId))
                mp.PRole = roleId;
            else
                mp.PRole = string.Empty;

            DataTable dt = bp.GetPCodeByPCode(stateCode, mp.PFlowId);
            if (dt.Rows.Count < 1 || (dt.Rows.Count == 1 && Convert.ToInt32(dt.Rows[0]["PCode"]) == mp.PCode))
            {
                mp.PCode = stateCode;
                mp.PPassName = passName;
                mp.PPassCode = Convert.ToInt32(passCode);
                mp.PNoPassName = NoPassName;
                mp.PNoPassCode = Convert.ToInt32(NoPassCode);
                mp.NeedCode = Convert.ToInt32(needCode_Text.Text.Trim());
                if (bp.UpdateByID(mp))
                {
                    function.WriteSuccessMsg("流程步骤修改成功！", customPath2 + "Content/FlowManager.aspx");
                }
                else
                {
                    function.WriteErrMsg("流程步骤修改失败！", customPath2 + "Content/FlowManager.aspx");
                }
            }
            else
            {
                function.WriteErrMsg("序列号重复请重新输入！");
            }
        }

        protected void lbStateCode_DataBound(object sender, EventArgs e)
        {
            //foreach (string str in stateCode.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            //{
            //    foreach (ListItem item in this.lbStateCode.Items)
            //    {
            //        if (item.Value == str.ToString() || str.ToString().Equals(item.Value))
            //        {
            //            item.Selected = true;
            //        }
            //    }
            //}

        }
    }
}