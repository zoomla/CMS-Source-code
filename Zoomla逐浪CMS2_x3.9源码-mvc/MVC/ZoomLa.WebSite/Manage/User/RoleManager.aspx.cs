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
using System.Data.SqlClient;
using ZoomLa.Web;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Text;

/*
 * 权限备注:
 * 内容管理--私有管理:除超级管理员外,只能看到和修改自己的文章
 */

namespace ZoomLaCMS.Manage.User
{
    public partial class RoleManager : CustomerPageAction
    {
        private B_Admin badmin = new B_Admin();
        private M_CrmAuth crmModel = new M_CrmAuth();
        private B_CrmAuth crmBll = new B_CrmAuth();
        private B_Pub pubBll = new B_Pub();
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
            if (!Page.IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.user, "RoleMange"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                else if (string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    function.WriteErrMsg("未选择操作角色");
                }
                int RoleID = DataConverter.CLng(Request.QueryString["id"]);
                ViewState["roleid"] = Request.QueryString["id"];
                PubManageBind();
                //默认显示该角色信息
                M_RoleInfo role = B_Role.GetRoleById(DataConverter.CLng(ViewState["roleid"]));
                this.LblRoleName.Text = role.RoleName;
                this.LblDescription.Text = role.Description;
                //填充该角色所具有的权限
                string Power = "";
                string RoleStr = B_Role.GetPowerInfo(role.RoleID);
                for (int t = 0; t < this.CheckBoxList1.Items.Count; t++)
                {
                    Power = this.CheckBoxList1.Items[t].Value;
                    if (RoleStr.IndexOf(Power) >= 0)
                        this.CheckBoxList1.Items[t].Selected = true;
                }
                for (int y = 0; y < this.CheckBoxList2.Items.Count; y++)
                {
                    Power = this.CheckBoxList2.Items[y].Value;
                    if (RoleStr.IndexOf(Power) >= 0)
                        this.CheckBoxList2.Items[y].Selected = true;
                }
                for (int x = 0; x < this.CheckBoxList3.Items.Count; x++)
                {
                    Power = this.CheckBoxList3.Items[x].Value;
                    if (RoleStr.IndexOf(Power) >= 0)
                        this.CheckBoxList3.Items[x].Selected = true;
                }
                for (int s = 0; s < this.CheckBoxList4.Items.Count; s++)
                {
                    Power = this.CheckBoxList4.Items[s].Value;
                    if (RoleStr.IndexOf(Power) >= 0)
                        this.CheckBoxList4.Items[s].Selected = true;
                }
                for (int s = 0; s < this.CheckBoxList5.Items.Count; s++)
                {
                    Power = this.CheckBoxList5.Items[s].Value;
                    if (RoleStr.IndexOf(Power) >= 0)
                        this.CheckBoxList5.Items[s].Selected = true;
                }
                for (int s = 0; s < this.CheckBoxList6.Items.Count; s++)
                {
                    Power = this.CheckBoxList6.Items[s].Value;
                    if (RoleStr.IndexOf(Power) >= 0)
                        this.CheckBoxList6.Items[s].Selected = true;
                }
                for (int s = 0; s < this.CheckBoxList7.Items.Count; s++)
                {
                    Power = this.CheckBoxList7.Items[s].Value;
                    if (RoleStr.IndexOf(Power) >= 0)
                        this.CheckBoxList7.Items[s].Selected = true;
                }
                for (int s = 0; s < this.CheckBoxList8.Items.Count; s++)
                {
                    Power = this.CheckBoxList8.Items[s].Value;
                    if (RoleStr.IndexOf(Power) >= 0)
                        this.CheckBoxList8.Items[s].Selected = true;
                }
                for (int s = 0; s < this.CheckBoxList9.Items.Count; s++)
                {
                    Power = this.CheckBoxList9.Items[s].Value;
                    if (RoleStr.IndexOf(Power) >= 0)
                        this.CheckBoxList9.Items[s].Selected = true;
                }
                for (int s = 0; s < this.PubManage.Items.Count; s++)
                {
                    Power = this.PubManage.Items[s].Value;
                    if (RoleStr.IndexOf(Power) >= 0)
                        this.PubManage.Items[s].Selected = true;
                }
                if (crmBll.IsExist(RoleID))
                {
                    crmModel = crmBll.GetSelect(RoleID);
                    crmAuthChk.Items[0].Selected = crmModel.AllowOption == "1" ? true : false;
                    crmAuthChk.Items[1].Selected = crmModel.AllowOptionValue == "1" ? true : false;
                    crmAuthChk.Items[2].Selected = crmModel.AllowExcel == "1" ? true : false;
                    crmAuthChk.Items[3].Selected = crmModel.AllowAddClient == "1" ? true : false;
                    crmAuthChk.Items[4].Selected = crmModel.AllCustomer == "1" ? true : false;
                    crmAuthChk.Items[5].Selected = crmModel.AssignFPMan == "1" ? true : false;
                    crmAuthChk.Items[6].Selected = crmModel.AllowFPAll == "1" ? true : false;
                    crmAuthChk.Items[7].Selected = crmModel.IsSalesMan == "1" ? true : false;
                }
            }
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li><a href='AdminManage.aspx'>管理员管理</a></li><li><a href='RoleManage.aspx'>角色管理</a></li><li><a href='RoleManager.aspx?ID=<%= Request.QueryString[" + ID + "] %>'>权限管理</a></li>");
        }
        //保存权限设置
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string power;
            int RoleID = DataConverter.CLng(ViewState["roleid"]);
            //内容管理
            for (int m = 0; m < this.CheckBoxList1.Items.Count; m++)
            {
                power = this.CheckBoxList1.Items[m].Value;
                if (this.CheckBoxList1.Items[m].Selected)
                {
                    if (!B_Role.IsExistPower(RoleID, power))
                        B_Role.SavePower(RoleID, power);
                }
                else
                {
                    if (B_Role.IsExistPower(RoleID, power))
                        B_Role.DelPower(RoleID, power);
                }
            }
            for (int t = 0; t < this.CheckBoxList2.Items.Count; t++)
            {
                power = this.CheckBoxList2.Items[t].Value;
                if (this.CheckBoxList2.Items[t].Selected)
                {
                    if (!B_Role.IsExistPower(RoleID, power))
                        B_Role.SavePower(RoleID, power);
                }
                else
                {
                    if (B_Role.IsExistPower(RoleID, power))
                        B_Role.DelPower(RoleID, power);
                }
            }
            for (int y = 0; y < this.CheckBoxList3.Items.Count; y++)
            {
                power = this.CheckBoxList3.Items[y].Value;
                if (this.CheckBoxList3.Items[y].Selected)
                {
                    if (!B_Role.IsExistPower(RoleID, power))
                        B_Role.SavePower(RoleID, power);
                }
                else
                {
                    if (B_Role.IsExistPower(RoleID, power))
                        B_Role.DelPower(RoleID, power);
                }
            }
            for (int v = 0; v < this.CheckBoxList4.Items.Count; v++)
            {
                power = this.CheckBoxList4.Items[v].Value;
                if (this.CheckBoxList4.Items[v].Selected)
                {
                    if (!B_Role.IsExistPower(RoleID, power))
                        B_Role.SavePower(RoleID, power);
                }
                else
                {
                    if (B_Role.IsExistPower(RoleID, power))
                        B_Role.DelPower(RoleID, power);
                }
            }
            for (int v = 0; v < this.CheckBoxList5.Items.Count; v++)
            {
                power = this.CheckBoxList5.Items[v].Value;
                if (this.CheckBoxList5.Items[v].Selected)
                {
                    if (!B_Role.IsExistPower(RoleID, power))
                        B_Role.SavePower(RoleID, power);
                }
                else
                {
                    if (B_Role.IsExistPower(RoleID, power))
                        B_Role.DelPower(RoleID, power);
                }
            }
            for (int v = 0; v < this.CheckBoxList6.Items.Count; v++)
            {
                power = this.CheckBoxList6.Items[v].Value;
                if (this.CheckBoxList6.Items[v].Selected)
                {
                    if (!B_Role.IsExistPower(RoleID, power))
                        B_Role.SavePower(RoleID, power);
                }
                else
                {
                    if (B_Role.IsExistPower(RoleID, power))
                        B_Role.DelPower(RoleID, power);
                }
            }
            for (int v = 0; v < this.CheckBoxList7.Items.Count; v++)
            {
                power = this.CheckBoxList7.Items[v].Value;
                if (this.CheckBoxList7.Items[v].Selected)
                {
                    if (!B_Role.IsExistPower(RoleID, power))
                        B_Role.SavePower(RoleID, power);
                }
                else
                {
                    if (B_Role.IsExistPower(RoleID, power))
                        B_Role.DelPower(RoleID, power);
                }
            }
            for (int v = 0; v < this.CheckBoxList8.Items.Count; v++)
            {
                power = this.CheckBoxList8.Items[v].Value;
                if (this.CheckBoxList8.Items[v].Selected)
                {
                    if (!B_Role.IsExistPower(RoleID, power))
                        B_Role.SavePower(RoleID, power);
                }
                else
                {
                    if (B_Role.IsExistPower(RoleID, power))
                        B_Role.DelPower(RoleID, power);
                }
            }
            for (int v = 0; v < this.CheckBoxList9.Items.Count; v++)
            {
                power = this.CheckBoxList9.Items[v].Value;
                if (this.CheckBoxList9.Items[v].Selected)
                {
                    if (!B_Role.IsExistPower(RoleID, power))
                        B_Role.SavePower(RoleID, power);
                }
                else
                {
                    if (B_Role.IsExistPower(RoleID, power))
                        B_Role.DelPower(RoleID, power);
                }
            }
            //互动权限
            for (int i = 0; i < this.PubManage.Items.Count; i++)
            {
                power = this.PubManage.Items[i].Value;
                if (this.PubManage.Items[i].Selected)
                {
                    if (!B_Role.SavePower(RoleID, power))
                        B_Role.SavePower(RoleID, power);
                }
                else
                {
                    if (B_Role.IsExistPower(RoleID, power))
                        B_Role.DelPower(RoleID, power);
                }
            }
            //Crm权限
            crmModel.RoleID = RoleID;
            crmModel.Add_Date = DateTime.Now;
            crmModel.Add_Man = badmin.GetAdminLogin().AdminId.ToString();

            crmModel.AllowOption = crmAuthChk.Items[0].Selected ? "1" : "0";
            crmModel.AllowOptionValue = crmAuthChk.Items[1].Selected ? "1" : "0";
            crmModel.AllowExcel = crmAuthChk.Items[2].Selected ? "1" : "0";
            crmModel.AllowAddClient = crmAuthChk.Items[3].Selected ? "1" : "0";
            crmModel.AllCustomer = crmAuthChk.Items[4].Selected ? "1" : "0";
            crmModel.AssignFPMan = crmAuthChk.Items[5].Selected ? "1" : "0";
            crmModel.AllowFPAll = crmAuthChk.Items[6].Selected ? "1" : "0";
            crmModel.IsSalesMan = crmAuthChk.Items[7].Selected ? "1" : "0";
            if (crmBll.IsExist(RoleID))
            {
                crmBll.UpdateModel(crmModel);
            }
            else
            {
                crmBll.insert(crmModel);
            }
            function.WriteSuccessMsg("操作成功", "RoleManage.aspx");
            //Response.Redirect("RoleManage.aspx");
        }
        //返回
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("RoleManage.aspx");
        }
        /// <summary>
        /// 选择全部
        /// </summary>
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CheckBox1.Checked == true)
            {
                for (int v = 0; v < this.CheckBoxList1.Items.Count; v++)
                {
                    this.CheckBoxList1.Items[v].Selected = true;
                }
                for (int v = 0; v < this.CheckBoxList2.Items.Count; v++)
                {
                    this.CheckBoxList2.Items[v].Selected = true;
                }
                for (int v = 0; v < this.CheckBoxList3.Items.Count; v++)
                {
                    this.CheckBoxList3.Items[v].Selected = true;
                }
                for (int v = 0; v < this.CheckBoxList4.Items.Count; v++)
                {
                    this.CheckBoxList4.Items[v].Selected = true;
                }
                for (int v = 0; v < this.CheckBoxList5.Items.Count; v++)
                {
                    this.CheckBoxList5.Items[v].Selected = true;
                }
                for (int v = 0; v < this.CheckBoxList6.Items.Count; v++)
                {
                    this.CheckBoxList6.Items[v].Selected = true;
                }
                for (int v = 0; v < this.CheckBoxList7.Items.Count; v++)
                {
                    this.CheckBoxList7.Items[v].Selected = true;
                }
                for (int v = 0; v < this.CheckBoxList8.Items.Count; v++)
                {
                    this.CheckBoxList8.Items[v].Selected = true;
                }
                for (int v = 0; v < this.CheckBoxList9.Items.Count; v++)
                {
                    this.CheckBoxList9.Items[v].Selected = true;
                }
                this.CheckBox2.Checked = true;
                this.CheckBox3.Checked = true;
                this.CheckBox4.Checked = true;
                this.CheckBox5.Checked = true;
                this.CheckBox6.Checked = true;
                this.CheckBox7.Checked = true;
                this.CheckBox8.Checked = true;
                this.CheckBox9.Checked = true;
                this.CheckBox10.Checked = true;
            }
            else
            {
                for (int v = 0; v < this.CheckBoxList1.Items.Count; v++)
                {
                    this.CheckBoxList1.Items[v].Selected = false;
                }
                for (int v = 0; v < this.CheckBoxList2.Items.Count; v++)
                {
                    this.CheckBoxList2.Items[v].Selected = false;
                }
                for (int v = 0; v < this.CheckBoxList3.Items.Count; v++)
                {
                    this.CheckBoxList3.Items[v].Selected = false;
                }
                for (int v = 0; v < this.CheckBoxList4.Items.Count; v++)
                {
                    this.CheckBoxList4.Items[v].Selected = false;
                }
                for (int v = 0; v < this.CheckBoxList5.Items.Count; v++)
                {
                    this.CheckBoxList5.Items[v].Selected = false;
                }
                for (int v = 0; v < this.CheckBoxList6.Items.Count; v++)
                {
                    this.CheckBoxList6.Items[v].Selected = false;
                }
                for (int v = 0; v < this.CheckBoxList7.Items.Count; v++)
                {
                    this.CheckBoxList7.Items[v].Selected = false;
                }
                for (int v = 0; v < this.CheckBoxList8.Items.Count; v++)
                {
                    this.CheckBoxList8.Items[v].Selected = false;
                }
                for (int v = 0; v < this.CheckBoxList9.Items.Count; v++)
                {
                    this.CheckBoxList9.Items[v].Selected = false;
                }
                this.CheckBox2.Checked = false;
                this.CheckBox3.Checked = false;
                this.CheckBox4.Checked = false;
                this.CheckBox5.Checked = false;
                this.CheckBox6.Checked = false;
                this.CheckBox7.Checked = false;
                this.CheckBox8.Checked = false;
                this.CheckBox9.Checked = false;
                this.CheckBox10.Checked = false;
            }
        }
        /// <summary>
        /// 全选内容管理
        /// </summary>
        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CheckBox2.Checked == true)
            {
                for (int v = 0; v < this.CheckBoxList1.Items.Count; v++)
                {
                    this.CheckBoxList1.Items[v].Selected = true;
                }
            }
            else
            {
                for (int v = 0; v < this.CheckBoxList1.Items.Count; v++)
                {
                    this.CheckBoxList1.Items[v].Selected = false;
                }
            }
        }
        /// <summary>
        /// 全选模型节点管理
        /// </summary>
        protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CheckBox3.Checked == true)
            {
                for (int v = 0; v < this.CheckBoxList2.Items.Count; v++)
                {
                    this.CheckBoxList2.Items[v].Selected = true;
                }
            }
            else
            {
                for (int v = 0; v < this.CheckBoxList2.Items.Count; v++)
                {
                    this.CheckBoxList2.Items[v].Selected = false;
                }
            }
        }
        /// <summary>
        /// 全选模板标签管理
        /// </summary>
        protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CheckBox4.Checked == true)
            {
                for (int v = 0; v < this.CheckBoxList3.Items.Count; v++)
                {
                    this.CheckBoxList3.Items[v].Selected = true;
                }
            }
            else
            {
                for (int v = 0; v < this.CheckBoxList3.Items.Count; v++)
                {
                    this.CheckBoxList3.Items[v].Selected = false;
                }
            }
        }
        /// <summary>
        /// 全选用户管理
        /// </summary>
        protected void CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CheckBox5.Checked == true)
            {
                for (int v = 0; v < this.CheckBoxList4.Items.Count; v++)
                {
                    this.CheckBoxList4.Items[v].Selected = true;
                }
            }
            else
            {
                for (int v = 0; v < this.CheckBoxList4.Items.Count; v++)
                {
                    this.CheckBoxList4.Items[v].Selected = false;
                }
            }
        }
        /// <summary>
        /// 全选商城管理
        /// </summary>
        protected void CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CheckBox6.Checked == true)
            {
                for (int v = 0; v < this.CheckBoxList5.Items.Count; v++)
                {
                    this.CheckBoxList5.Items[v].Selected = true;
                }
            }
            else
            {
                for (int v = 0; v < this.CheckBoxList5.Items.Count; v++)
                {
                    this.CheckBoxList5.Items[v].Selected = false;
                }
            }
        }
        /// <summary>
        /// 全选黄页管理
        /// </summary>
        protected void CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CheckBox7.Checked == true)
            {
                for (int v = 0; v < this.CheckBoxList6.Items.Count; v++)
                {
                    this.CheckBoxList6.Items[v].Selected = true;
                }
            }
            else
            {
                for (int v = 0; v < this.CheckBoxList6.Items.Count; v++)
                {
                    this.CheckBoxList6.Items[v].Selected = false;
                }
            }
        }
        /// <summary>
        /// 全选店铺管理
        /// </summary>
        protected void CheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CheckBox8.Checked == true)
            {
                for (int v = 0; v < this.CheckBoxList7.Items.Count; v++)
                {
                    this.CheckBoxList7.Items[v].Selected = true;
                }
            }
            else
            {
                for (int v = 0; v < this.CheckBoxList7.Items.Count; v++)
                {
                    this.CheckBoxList7.Items[v].Selected = false;
                }
            }
        }
        /// <summary>
        /// 全选空间管理
        /// </summary>
        protected void CheckBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CheckBox9.Checked == true)
            {
                for (int v = 0; v < this.CheckBoxList8.Items.Count; v++)
                {
                    this.CheckBoxList8.Items[v].Selected = true;
                }
            }
            else
            {
                for (int v = 0; v < this.CheckBoxList8.Items.Count; v++)
                {
                    this.CheckBoxList8.Items[v].Selected = false;
                }
            }
        }
        /// <summary>
        /// 全选其他管理
        /// </summary>
        protected void CheckBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CheckBox10.Checked == true)
            {
                for (int v = 0; v < this.CheckBoxList9.Items.Count; v++)
                {
                    this.CheckBoxList9.Items[v].Selected = true;
                }
            }
            else
            {
                for (int v = 0; v < this.CheckBoxList9.Items.Count; v++)
                {
                    this.CheckBoxList9.Items[v].Selected = false;
                }
            }
        }
        protected void PubManageAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.PubManage.Items.Count; i++)
            {
                this.PubManage.Items[i].Selected = PubManageAll.Checked;
            }
        }
        protected void PubManageBind()
        {
            DataTable dt = pubBll.Select_All();
            PubManage.DataSource = dt;
            PubManage.DataTextField = "PubName";
            PubManage.DataValueField = "PubTableName";
            PubManage.DataBind();
        }
    }
}