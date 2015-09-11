namespace ZoomLaManage.WebSite.Manage.User
{
    using System;
    using System.Text;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using ZoomLa.Components;
    using ZoomLa.SQLDAL;
    using ZoomLa.Model;
    using ZoomLa.DALFactory;
    using ZoomLa.Web;
    using ZoomLa.IDAL;
    using System.Data.SqlClient;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    public partial class AddManage : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private B_Admin bll = new B_Admin();
        private B_User buser = new B_User();
        M_AdminInfo admin = new M_AdminInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.bll.CheckMulitLogin();
                if (!bll.ChkPermissions("AdminEdit"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                Bind();
                //验证操作员身份,暂略，是否为超级管理员或普通管理员是否有添加权限。
                ViewState["AdminID"] = Request.QueryString["id"];
                //判断当前管理员是否存在
                if (B_Admin.IsExist(DataConverter.CLng(ViewState["AdminID"])))
                {
                    this.lbTitle.Text = "修改管理员";
                    admin = B_Admin.GetAdminByAdminId(DataConverter.CLng(ViewState["AdminID"]));
                    this.tbdName.Text = admin.AdminName;
                    this.tbdName.Enabled = false;
                    this.HdnPwd.Value = admin.AdminPassword;
                    this.tbPwd.Text = "";
                    //string[] roleID = admin.RoleList.Split();
                    for (int m = 0; m < cblRoleList.Items.Count; m++)
                    {
                        this.cblRoleList.Items[m].Selected = false;
                    }
                    for (int i = 0; i < admin.RoleList.Length; i++)
                    {
                        for (int j = 0; j < this.cblRoleList.Items.Count; j++)
                        {
                            if (this.cblRoleList.Items[j].Value == admin.RoleList[i].ToString())
                            {
                                this.cblRoleList.Items[j].Selected = true;
                            }
                            else
                            {
                                this.cblRoleList.Items[j].Selected = false;
                            }
                        }
                    }
                    //单选框设置
                    if (admin.EnableMultiLogin)
                    {
                        this.cb1.Checked = true;
                    }
                    else
                    {
                        this.cb1.Checked = false;
                    }
                    if (admin.EnableModifyPassword)
                    {
                        this.cb2.Checked = true;
                    }
                    else
                    {
                        this.cb2.Checked = false;
                    }
                    if (admin.IsLock)
                    {
                        this.cb3.Checked = true;
                    }
                    else
                    {
                        this.cb3.Checked = false;
                    }
                }
                else
                {
                    this.lbTitle.Text = "添加管理员";
                }                
            }
        }
        private void Bind()
        {
            DataTable dt = B_Role.GetRoleName();
            this.cblRoleList.DataSource = dt;
            this.cblRoleList.DataTextField = "RoleName";
            this.cblRoleList.DataValueField = "RoleId";
            this.cblRoleList.DataBind();
        }
        #endregion
        /// <summary>
        /// 提交管理员信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                switch (this.lbTitle.Text)
                {
                    case ("添加管理员"):
                        if (!Manager_Validator())
                        {
                            function.WriteErrMsg("管理员名或前台用户名重复！");
                        }
                        admin.AdminName = this.tbdName.Text;
                        string errmsg="";
                        if (string.IsNullOrEmpty(this.tbPwd.Text.Trim()))
                        {
                            errmsg = "密码不能为空";
                        }
                        else
                        {
                            string pwd = this.tbPwd.Text.Trim();
                            if (pwd.Length < 6)
                                errmsg = "密码长度不能少于6位";
                        }
                        if (!string.IsNullOrEmpty(errmsg))
                        {
                            function.WriteErrMsg(errmsg);
                        }
                        admin.AdminPassword = StringHelper.MD5(this.tbPwd.Text);
                        admin.UserName = this.tbdName.Text;
                        //获取管理员角色
                        StringBuilder sb = new StringBuilder();
                        foreach (ListItem item in cblRoleList.Items)
                        {
                            if (item.Selected)
                            {                                
                                string append = item.Value;
                                StringHelper.AppendString(sb, append);                                
                            }
                        }
                        admin.RoleList = sb.ToString();
                        admin.EnableMultiLogin = this.cb1.Checked;
                        admin.EnableModifyPassword = this.cb2.Checked;
                        admin.IsLock = this.cb3.Checked;
                        admin.LastLoginIP = "";
                        admin.LastLoginTime = DateTime.Now;
                        admin.LastLogoutTime = DateTime.Now;
                        admin.LastModifyPasswordTime = DateTime.Now;
                        admin.LoginTimes = 0;
                        admin.RandNumber = "";
                        admin.Theme = "";
                        B_Admin.Add(admin);
                        Response.Write("<script type=\"text/javascript\">alert(\"添加成功！\")</script>");
                        Response.Redirect("AdminManage.aspx");
                        break;

                    case ("修改管理员"):
                        admin = B_Admin.GetAdminByAdminId(DataConverter.CLng(ViewState["AdminID"]));
                        
                        if (!string.IsNullOrEmpty(this.tbPwd.Text.Trim()))
                        {
                            admin.AdminPassword = StringHelper.MD5(this.tbPwd.Text.Trim());
                        }
                        //获取管理员角色
                        StringBuilder sb1 = new StringBuilder();
                        foreach (ListItem item in cblRoleList.Items)
                        {
                            if (item.Selected)
                            {                                
                                string append = cblRoleList.SelectedValue;
                                StringHelper.AppendString(sb1, append);                                
                            }
                        }
                        string role = sb1.ToString();
                        if (admin.IsSuperAdmin)
                            role = "0," + role;
                        admin.RoleList = role;

                        admin.EnableMultiLogin = this.cb1.Checked;
                        admin.EnableModifyPassword = this.cb2.Checked;
                        admin.IsLock = this.cb3.Checked;
                        B_Admin.Update(admin);
                        Response.Write("<script type=\"text/javascript\">alert(\"修改成功！\")</script>");
                        Response.Redirect("AdminManage.aspx");
                        break;
                }
            }
        }
        #endregion
        /// <summary>
        /// 取消提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            switch (this.lbTitle.Text)
            {
                case ("添加管理员"):
                    this.tbdName.Text = "";                    
                    this.tbPwd1.Text = "";
                    this.tbPwd.Text = "";
                    for (int m = 0; m < cblRoleList.Items.Count; m++)
                    {
                        this.cblRoleList.Items[m].Selected = false;
                    }
                    this.cb3.Checked = false;
                    this.cb2.Checked = false;
                    this.cb1.Checked = false;
                    break;
                case ("修改管理员"):
                    admin = B_Admin.GetAdminByAdminId(DataConverter.CLng(ViewState["AdminID"]));
                    this.tbdName.Text = admin.AdminName;                    
                    this.tbPwd.Text = "";
                    this.tbPwd1.Text = "";
                    //string[] roleID = admin.RoleList.Split();
                    for (int m = 0; m < cblRoleList.Items.Count; m++)
                    {
                        this.cblRoleList.Items[m].Selected = false;
                    }
                    for (int i = 0; i < admin.RoleList.Length; i++)
                    {
                        for (int j = 0; j < this.cblRoleList.Items.Count; j++)
                        {
                            if (this.cblRoleList.Items[j].Value == admin.RoleList[i].ToString())
                            {
                                this.cblRoleList.Items[j].Selected = true;
                            }
                            else
                            {
                                this.cblRoleList.Items[j].Selected = false;
                            }
                        }
                    }
                    //单选框设置
                    if (admin.EnableMultiLogin)
                    {
                        this.cb1.Checked = true;
                    }
                    else
                    {
                        this.cb1.Checked = false;
                    }
                    if (admin.EnableModifyPassword)
                    {
                        this.cb2.Checked = true;
                    }
                    else
                    {
                        this.cb2.Checked = false;
                    }
                    if (admin.IsLock)
                    {
                        this.cb3.Checked = true;
                    }
                    else
                    {
                        this.cb3.Checked = false;
                    }
                    break;
            }
        }
        #endregion
        /// <summary>
        /// 验证管理员是否已存在
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected bool Manager_Validator()
        {
            string adminName = this.tbdName.Text;
            bool re = false;
            if (B_Admin.IsExist(adminName))
            {
                re = false;
                
            }
            else
            {
                if (this.buser.IsExit(adminName))
                {
                    re = false;
                   
                }
                else
                    re = true;
            }
            return re;
        }
}
}