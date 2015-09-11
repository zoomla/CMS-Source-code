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
using ZoomLa.SQLDAL;
using System.Text;

namespace User
{
    public partial class RoleManager : System.Web.UI.Page
    {
        //string[] powerList = string.Empty;
        //StringBuilder sb = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                ViewState["roleid"] = Request.QueryString["id"];
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
            }
        }
        //保存权限设置
        #region
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
            Response.Redirect("RoleManage.aspx");
        }
        #endregion
        //返回
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("RoleManage.aspx");
        }
    }
}