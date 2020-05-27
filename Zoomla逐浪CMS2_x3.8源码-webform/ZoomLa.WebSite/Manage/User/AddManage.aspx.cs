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
    using ZoomLa.Web;
    using System.Data.SqlClient;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.API;
    public partial class AddManage : CustomerPageAction
    {
        private B_User buser = new B_User();
        private B_Admin badmin = new B_Admin();
        private M_AdminInfo admin = new M_AdminInfo();
        private B_UserBaseField ubfbll = new B_UserBaseField();
        public int AdminID { get { return DataConvert.CLng(ViewState["AdminID"]); } set { ViewState["AdminID"] = value; } }
        private int RoleID { get { return DataConvert.CLng(ViewState["RoleID"]); } set { ViewState["RoleID"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                //稍后处理,验证管理员身份再允许切换场景
                string action = Request.Form["action"];
                int result = 1;
                switch (action)
                {
                    case "scene":
                        {
                            int adminid = Convert.ToInt32(Request.Form["adminid"]);
                            string config = Request.Form["config"];
                            M_AdminInfo adminMod = B_Admin.GetAdminByAdminId(adminid);
                            adminMod.Theme = config;
                            adminMod.StructureID = Request.Form["model"];
                            B_Admin.Update(adminMod);
                            result = 1;
                        }
                        break;
                }
                Response.Write(result); Response.Flush(); Response.End();
            }
            if (!IsPostBack)
            {
                AdminID = DataConvert.CLng(Request.QueryString["id"]);
                RoleID = DataConvert.CLng(Request.QueryString["RoleID"]);
                //无管理员编辑权限,则只能修改自己
                if (!B_ARoleAuth.Check(ZLEnum.Auth.user, "AdminEdit") && (badmin.GetAdminLogin().AdminId != AdminID))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                Bind();//绑定显示角色
                if (B_Admin.IsExist(AdminID))
                {
                    #region 修改管理员
                    showmenu.Visible = false;
                    SameNameDiv.Visible = false;
                    admin = B_Admin.GetAdminByAdminId(AdminID);
                    tbdName.Text = admin.AdminName;
                    tbdName.Enabled = false;
                    txtAdminTrueName.Text = admin.AdminTrueName;
                    //hfNode.Value = admin.StructureID;
                    SetModel_Div.Visible = true;
                    curmodel_hid.Value = admin.StructureID;
                    HdnPwd.Value = admin.AdminPassword;
                    Theme_L.Text = string.IsNullOrEmpty(admin.Theme) ? "<span class='alert alert-info'>默认场景</span>" : "<span class='alert alert-danger'>自定义场景</span>";
                    SPwd_T.Text = admin.RandNumber;
                    if (string.IsNullOrEmpty(admin.RandNumber))
                        SPwd_T.Text = "123456";
                    tbPwd.Text = "";
                    DefaultStart_DP.SelectedValue = admin.DefaultStart.ToString();
                    for (int m = 0; m < cblRoleList.Items.Count; m++)
                    {
                        cblRoleList.Items[m].Selected = false;
                    }
                    string[] arr = admin.RoleList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < arr.Length; i++)
                    {
                        for (int j = 0; j < cblRoleList.Items.Count; j++)
                        {

                            if (DataConverter.CLng(cblRoleList.Items[j].Value) == DataConverter.CLng(arr[i].ToString()))
                            {

                                cblRoleList.Items[j].Selected = true;
                            }

                        }
                    }
                    //单选框设置
                    cb1.Checked = admin.EnableMultiLogin;
                    cb2.Checked = admin.EnableModifyPassword;
                    cb3.Checked = admin.IsLock;
                    cb4.Checked = admin.IsTable;
                    CheckBox1.Checked = admin.PubRole == 1 ? true : false;
                    //if (admin.StructureID != null && admin.StructureID != "")
                    //{
                    //    B_Structure bstruct = new B_Structure();
                    //    string StructID = admin.StructureID;
                    //    string strs = "";
                    //    string[] stids = StructID.Split(new Char[] { ',' });
                    //    if (stids.Length > 0)
                    //    {
                    //        for (int i = 0; i < stids.Length; i++)
                    //        {
                    //            if (stids[i] != "" || stids[i] != null)
                    //            {
                    //                DataTable dt = bstruct.SelByField("ID", stids[i]);
                    //                if (dt != null && dt.Rows.Count > 0)
                    //                {
                    //                    strs += dt.Rows[0]["Name"].ToString() + " ";
                    //                }
                    //            }
                    //        }
                    //    }
                    //    txtNode.Text = strs;
                    //}
                    #endregion
                }
                else
                {
                    SPwd_T.Text = "123456";
                    for (int m = 0; m < cblRoleList.Items.Count; m++)
                    {
                        cblRoleList.Items[m].Selected = (RoleID.ToString() == cblRoleList.Items[m].Value);
                    }
                }
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='UserManage.aspx'>用户管理</a></li><li> <a href='AdminManage.aspx'>管理员管理</a></li><li>编辑管理员</li>");
            }
        }
        private void Bind()
        {
            DataTable dt = B_Role.GetRoleName();
            cblRoleList.DataSource = dt;
            cblRoleList.DataTextField = "RoleName";
            cblRoleList.DataValueField = "RoleId";
            cblRoleList.DataBind();
            if (dt != null)
            {
                dt.Dispose();
            }
        }
        // 提交管理员信息
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (AdminID > 0)
            {
                admin = B_Admin.GetAdminByAdminId(AdminID);
                string oldadminpassword = admin.AdminPassword;
                if (!string.IsNullOrEmpty(txtAdminTrueName.Text.Trim()))
                {
                    admin.AdminTrueName = txtAdminTrueName.Text.Trim();
                }
                if (!string.IsNullOrEmpty(hfNode.Value.Trim()))
                {
                    admin.StructureID = hfNode.Value.Trim();
                }
            }
            else
            {
                string pwd = tbPwd.Text.Trim().Replace(" ", "");
                if (!Manager_Validator()) { function.WriteErrMsg("管理员名称重复！"); }
                if (pwd.Length < 3) { function.WriteErrMsg("密码长度不能少于3位"); }
                admin.AdminPassword = StringHelper.MD5(tbPwd.Text);
                admin.UserName = tbdName.Text;
                admin.StructureID = hfNode.Value.Trim();
            }
            //获取管理员角色
            string role = "";
            foreach (ListItem item in cblRoleList.Items)
            {
                if (item.Selected)
                {
                    role += item.Value + ",";
                }
            }
            if (admin.IsSuperAdmin(admin.RoleList))
                role = "0," + role;
            admin.RoleList = role;
            admin.AdminName = tbdName.Text;
            admin.AdminTrueName = txtAdminTrueName.Text.Trim();
            admin.EnableMultiLogin = cb1.Checked;
            admin.EnableModifyPassword = cb2.Checked;
            admin.IsLock = cb3.Checked;
            admin.IsTable = cb4.Checked;
            admin.LastLoginIP = "";
            admin.LastLoginTime = DateTime.Now;
            admin.LastLogoutTime = DateTime.Now;
            admin.LastModifyPasswordTime = DateTime.Now;
            admin.LoginTimes = 0;
            admin.RandNumber = SPwd_T.Text;
            admin.ManageNode = "";
            admin.DefaultStart = DataConverter.CLng(DefaultStart_DP.SelectedValue);
            admin.PubRole = CheckBox1.Checked ? 1 : 0;
            admin.StructureID = curmodel_hid.Value;
            if (AdminID > 0)
            {
                if (!string.IsNullOrEmpty(tbPwd.Text.Trim()))
                {
                    admin.AdminPassword = StringHelper.MD5(tbPwd.Text.Trim());
                    B_Admin.Update(admin);
                    if (admin.AdminId == B_Admin.GetLogin().AdminId)
                    {
                        function.WriteSuccessMsg("密码修改成功,系统现在自动注销，请使用新密码登录管理!", CustomerPageAction.customPath2 + "/SignOut.aspx");
                    }
                    function.WriteSuccessMsg("密码修改成功!", "AdminManage.aspx");
                }
                else
                {
                    B_Admin.Update(admin);
                    function.WriteSuccessMsg("修改成功", "AdminManage.aspx");
                }
            }
            else
            {
                //新增
                B_Admin.Add(admin);
                M_AdminInfo adminInfo = B_Admin.GetAdminByAdminName(admin.AdminName);
                #region 添加会员
                if (AddUser_Chk.Checked)
                {
                    string username = tbdName.Text.Trim();
                    M_UserInfo userinfo = buser.GetUserByName(username);
                    Response.Write(userinfo.UserID.ToString());
                    if (userinfo.UserID > 0)
                    {
                        userinfo.UserPwd = StringHelper.MD5(tbPwd.Text);
                        userinfo.Email = tbEmail.Text;
                        buser.UpDateUser(userinfo);
                    }
                    else
                    {
                        B_Group gpBll = new B_Group();
                        M_UserInfo uinfo = new M_UserInfo();
                        uinfo.UserName = tbdName.Text;
                        uinfo.UserPwd = tbPwd.Text;
                        uinfo.Question = Question_DP.SelectedItem.Text;
                        uinfo.Answer = tbAnswer.Text.Trim();
                        uinfo.Email = tbEmail.Text.Trim();
                        uinfo.GroupID = gpBll.DefaultGroupID();
                        uinfo.LastLoginIP = HttpContext.Current.Request.UserHostAddress;
                        uinfo.LastLockTime = DateTime.Now;
                        uinfo.LastLoginTimes = DateTime.Now;
                        uinfo.LastPwdChangeTime = DateTime.Now;
                        uinfo.LoginTimes = 0;
                        uinfo.RegTime = DateTime.Now;
                        uinfo.ConsumeExp = 0;
                        uinfo.IsConfirm = 0;
                        uinfo.VIP = 0;
                        uinfo.UserPwd = StringHelper.MD5(uinfo.UserPwd);
                        uinfo.UserID = buser.AddModel(uinfo);
                        M_Uinfo binfo = new M_Uinfo();
                        binfo.UserId = uinfo.UserID;
                        buser.AddBase(binfo);
                        adminInfo.AddUserID = uinfo.UserID;
                        B_Admin.Update(adminInfo);
                    }
                }
                #endregion
                Response.Redirect("AdminDetail.aspx?id=" + adminInfo.AdminId + "&roleid=" + adminInfo.NodeRole);
            }
        }
        // 验证管理员是否已存在
        protected bool Manager_Validator()
        {
            string adminName = tbdName.Text;
            bool re = false;
            if (B_Admin.IsExist(adminName))
            {
                re = false;
            }
            else
            {
                re = true;
            }
            return re;
        }

        protected void AddUser_Chk_CheckedChanged(object sender, EventArgs e)
        {
            SameNameDiv.Visible = AddUser_Chk.Checked;
        }
    }
}