using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class MIS_Mail_MailSet : System.Web.UI.Page
{
        B_MailSet bll = new B_MailSet();
        M_MailSet model = new M_MailSet();
        B_User buser = new B_User();
        DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin(Request.Url.LocalPath);
        if (!IsPostBack)
        {
            if (function.IsNumeric(Request["ID"]))
            {
                tbxSmtpServer.Enabled = false;
                tbxPOP3Server.Enabled = false;
                tbxUserMail.Enabled = false;
                int id = DataConvert.CLng(Request["ID"]);
                dt = bll.Sel(id);
                if (dt != null && dt.Rows.Count > 0)
                {
                    tbxSmtpServer.Text = dt.Rows[0]["Smtp"].ToString();
                    tbxPOP3Server.Text = dt.Rows[0]["POP3"].ToString();
                    tbxUserMail.Text = dt.Rows[0]["UserMail"].ToString();
                    txbPassword.Text = dt.Rows[0]["Password"].ToString();
                }
                if (Request["type"] == "View")
                { 
                    Labpwd.Visible = false;
                    txbPassword.Visible = false;
                    Button1.Text = "编辑";
                }
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (function.IsNumeric(Request["ID"]))
        {
            int id = DataConvert.CLng(Request["ID"]);
            dt = bll.Sel(id);
        }
           
        model.UserID = DataConverter.CLng(buser.GetLogin().UserID);
        model.Smtp = tbxSmtpServer.Text;
        model.POP3 = tbxPOP3Server.Text;
        model.UserMail = tbxUserMail.Text;
        model.Password = txbPassword.Text;
        DataTable dt2 = bll.SelByUid(buser.GetLogin().UserID);
        if (dt2.Rows.Count > 0) 
            model.IsDefault = false;
       else model.IsDefault = true;
        if (dt != null && dt.Rows.Count > 0)
        {
            model.ID = DataConverter.CLng(dt.Rows[0]["ID"]);
            bll.UpdateByID(model);
            function.WriteSuccessMsg("修改成功！", "MailSetList.aspx");
        }
        else
        {
            model.CreateTime = DateTime.Now;
            if (bll.insert(model) == 1)
            {
                Response.Write("<script>alert('添加成功！！！');location.href='MailSetList.aspx';</script>");
            }
        }
    }
}