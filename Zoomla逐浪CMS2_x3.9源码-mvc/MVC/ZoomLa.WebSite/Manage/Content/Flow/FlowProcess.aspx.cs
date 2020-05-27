using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;


namespace ZoomLaCMS.Manage.Content.Flow
{
    public partial class FlowProcess : CustomerPageAction
    {
        B_Role br = new B_Role();
        B_Process bp = new B_Process();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                this.lblFlow.InnerHtml = Request.QueryString["name"];
                if (id != null && !"".Equals(id))
                {
                    this.CheckBoxList1.DataSource = br.GetRoleAll();
                    this.CheckBoxList1.DataTextField = "RoleName";
                    this.CheckBoxList1.DataValueField = "RoleID";
                    this.CheckBoxList1.DataBind();
                }
            }

            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li><a href='FlowManager.aspx'>流程管理</a></li><li><a href='FlowProcessManager.aspx?id=" + Request.QueryString["id"] + "&name=" + Request.QueryString["name"] + "'>" + Request.QueryString["name"] + "</a></li><li class='active'>添加步骤</li>");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string name = this.txtPName.Text.Trim();
            string flowPrcess = this.FlowPrcess.Value.Trim();
            List<string> list = new List<string>();
            foreach (ListItem item in this.CheckBoxList1.Items)
            {
                if (item.Selected)
                {
                    list.Add(item.Value);
                }
            }
            int stateCode = 0;
            if (this.txtPCode.Value.Trim() != null && !"".Equals(this.txtPCode.Value.Trim()))
            {
                stateCode = Convert.ToInt32(this.txtPCode.Value.Trim());
            };
            string passName = this.txtPassCode.Text.Trim();
            string passCode = this.ddlPassCode.SelectedValue.Trim();
            string NoPassName = this.txtNoPassCode.Text.Trim();
            string NoPassCode = this.ddlNoPassCode.SelectedValue.Trim();
            string roleId = null;

            foreach (string str in list)
            {
                roleId += str + ",";
            }

            M_Process mp = new M_Process();
            mp.PName = name;
            mp.PDepcit = flowPrcess;
            if (roleId != null && !"".Equals(roleId))
                mp.PRole = roleId;
            else
                mp.PRole = string.Empty;
            if (bp.GetPCodeByPCode(stateCode, Convert.ToInt32(Request.QueryString["id"])).Rows.Count > 0)
            {
                function.WriteErrMsg("序列号重复请重新输入！");
            }
            else
            {
                mp.PCode = Convert.ToInt32(stateCode);
                mp.PPassName = passName;
                mp.PPassCode = Convert.ToInt32(passCode);
                mp.PNoPassName = NoPassName;
                mp.PNoPassCode = Convert.ToInt32(NoPassCode);
                mp.PFlowId = Convert.ToInt32(Request.QueryString["id"]);
                mp.NeedCode = DataConverter.CLng(needCode_Text.Text.Trim());
                if (bp.AddProcess(mp))
                {
                    function.WriteSuccessMsg("流程步骤添加成功！", customPath2 + "Content/FlowManager.aspx");
                }
                else
                {
                    function.WriteErrMsg("添加流程步骤失败！", customPath2 + "Content/AddFlow.aspx.aspx");
                }
            }

        }
    }
}