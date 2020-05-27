using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.User.Mail
{
    public partial class AddMailTemp : CustomerPageAction
    {
        B_Admin abll = new B_Admin();
        B_Model mbll = new B_Model();
        B_MailTemp bll = new B_MailTemp();
        public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Mid > 0)
                {
                    M_MailTemp tlpMod = bll.SelReturnModel(Mid);
                    TxtTempName.Text = tlpMod.TempName;
                    drType.Text = tlpMod.Type.ToString();
                    TxtContent.Value = tlpMod.Content;
                }
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>用户管理</a></li><li><a href='MailTemplist.aspx'>邮件模板</a></li><li class='active'>创建邮件模板</li>");
            }
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            M_MailTemp tlpMod = new M_MailTemp();
            if (Mid > 0) { tlpMod = bll.SelReturnModel(Mid); }
            tlpMod.TempName = TxtTempName.Text;
            tlpMod.Type = Convert.ToInt32(drType.SelectedValue);
            tlpMod.Content = TxtContent.Value;
            tlpMod.AddUser = abll.GetAdminLogin().AdminName;
            if (Mid > 0)
            {
                tlpMod.ID = Mid;
                bll.UpdateByID(tlpMod);
            }
            else
            {
                tlpMod.CreateTime = DateTime.Now;
                bll.insert(tlpMod);
            }
            function.WriteSuccessMsg("操作成功", "MailTemplist.aspx");
        }
    }
}