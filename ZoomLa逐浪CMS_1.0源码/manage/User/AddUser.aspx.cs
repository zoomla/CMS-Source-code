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
using ZoomLa.Web;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
namespace User
{
    public partial class AddUser : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        M_UserInfo userInfo = new M_UserInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckMulitLogin();
            if (!badmin.ChkPermissions("UserManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
        }

        #region
        /// <summary>
        /// 提交会员信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            B_User bll = new B_User();
            userInfo.UserName = this.tbUserName.Text;
            userInfo.UserPwd = this.tbPwd.Text;
            userInfo.Question = this.tbQuestion.Text;
            userInfo.Answer = this.tbAnswer.Text;
            userInfo.Email = this.tbEmail.Text;
            userInfo.FaceHeight = Convert.ToInt32(this.tbPhoHeight.Text);
            userInfo.FaceWidth = Convert.ToInt32(this.tbPhoWidth.Text);
            userInfo.UserFace = this.tbPhotoPlace.Text;
            userInfo.RegTime = DataConverter.CDate(DateTime.Now.ToShortTimeString());
            userInfo.Sign = this.tbUserWrite.Text;
            userInfo.PrivacySetting = 0;
            bll.Add(userInfo);
        }
        #endregion

        #region
        /// <summary>
        /// 判断会员名是否已存在
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            B_User bll = new B_User();
            string userName = this.tbUserName.Text;
            if (bll.IsExit(userName))
            {
                args.IsValid = false;
                this.CustomValidator1.Visible = true;
                this.tbUserName.Text = "";
            }
            else
            {
                args.IsValid = true;
            }
        }
        #endregion

        #region
        /// <summary>
        /// 重置清空所有记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.tbUserName.Text = "";
            this.tbPwd.Text = "";
            this.tbEmail.Text = "";
            this.tbAnswer.Text = "";
            this.tbQuestion.Text = "";
            this.tbPhoHeight.Text = "";
            this.tbPhotoPlace.Text = "";
            this.tbPhoWidth.Text = "";
            this.tbUserWrite.Text = "";
        }
        #endregion
    }
}