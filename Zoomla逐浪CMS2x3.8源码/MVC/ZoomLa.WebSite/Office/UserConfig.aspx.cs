using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.MIS.OA
{
    public partial class UserConfig : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Group groupBll = new B_Group();
        M_OA_UserConfig ucMod = new M_OA_UserConfig();
        B_OA_UserConfig ucBll = new B_OA_UserConfig();
        M_Uinfo uinfoMod = new M_Uinfo();
        M_UserInfo mu = new M_UserInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            mu = buser.GetLogin();
            if (!IsPostBack)
            {
                ucMod = ucBll.SelModelByUserID(mu.UserID);
                if (ucMod == null)//无对应权限,则是默认权限
                {

                }
                else//拥有权限
                {
                    SetChk();
                }
                uinfoMod = buser.GetUserBaseByuserid(mu.UserID);
                TrueName.Text = mu.HoneyName;
                if (uinfoMod.UserSex)
                    UserSex.SelectedValue = "1";
                else
                    UserSex.SelectedValue = "2";
                UserPhone.Text = uinfoMod.Mobile;
                UserTel.Text = uinfoMod.HomePhone;
                Branch.Text = groupBll.GetGroupNameByIDS(mu.GroupID.ToString());
                StatusT.Text = mu.State == 0 ? "在职" : "离职";
            }
        }
        //-------Tool
        private void SetChk()
        {
            for (int i = 0; i < leftChk.Items.Count; i++)
            {
                leftChk.Items[i].Selected = ucMod.HasAuth(ucMod.LeftChk, leftChk.Items[i].Value);
            }
            for (int i = 0; i < leftChk.Items.Count; i++)
            {
                mainChk.Items[i].Selected = ucMod.HasAuth(ucMod.MainChk, mainChk.Items[i].Value);
            }
            for (int i = 0; i < popChk.Items.Count; i++)
            {
                popChk.Items[i].Selected = ucMod.HasAuth(ucMod.PopChk, popChk.Items[i].Value);
            }
        }
        //-------
        protected void saveBtn_Click(object sender, EventArgs e)
        {
            ucMod.UserID = buser.GetLogin().UserID;
            for (int i = 0; i < this.leftChk.Items.Count; i++)
            {
                if (leftChk.Items[i].Selected)
                {
                    ucMod.LeftChk += leftChk.Items[i].Value + ",";
                }
            }
            for (int i = 0; i < this.mainChk.Items.Count; i++)
            {
                if (mainChk.Items[i].Selected)
                {
                    ucMod.MainChk += mainChk.Items[i].Value + ",";
                }
            }
            for (int i = 0; i < this.popChk.Items.Count; i++)
            {
                if (popChk.Items[i].Selected)
                {
                    ucMod.PopChk += popChk.Items[i].Value + ",";
                }
            }

            M_OA_UserConfig model = ucBll.SelModelByUserID(ucMod.UserID);
            if (model == null)
            {
                ucBll.Insert(ucMod);
            }
            else
            {
                ucMod.ID = model.ID;
                ucBll.UpdateByID(ucMod);
            }
            function.WriteSuccessMsg("修改成功");
        }
        protected void SetUserBtn_Click(object sender, EventArgs e)
        {
            mu = buser.GetLogin();
            uinfoMod = buser.GetUserBaseByuserid(mu.UserID);
            mu.TrueName = TrueName.Text.Trim();
            if (!string.IsNullOrEmpty(PassWord.Text.Trim()) && !string.IsNullOrEmpty(NewPassWord.Text.Trim()) && !string.IsNullOrEmpty(RNewPassWord.Text.Trim()))
            {
                if (StringHelper.MD5(PassWord.Text.Trim()) != mu.UserPwd)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('原密码错误!!')", true);
                    return;
                }
                else
                    mu.UserPwd = StringHelper.MD5(NewPassWord.Text.Trim());
            }
            uinfoMod.UserSex = UserSex.SelectedValue == "1" ? true : false;
            uinfoMod.Mobile = UserPhone.Text.Trim();
            uinfoMod.HomePhone = UserTel.Text.Trim();
            buser.UpDateUser(mu);
            buser.UpdateBase(uinfoMod);
            function.WriteSuccessMsg("修改成功", "Main.aspx");
        }
    }
}