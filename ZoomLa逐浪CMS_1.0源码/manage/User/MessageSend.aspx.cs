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
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;


namespace User
{
    public partial class MessageSend : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("MessManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                if (!string.IsNullOrEmpty(base.Request.QueryString["id"]))
                {
                    M_Message messInfo = B_Message.GetMessByID(DataConverter.CLng(base.Request.QueryString["id"]));
                    if (!messInfo.IsNull)
                    {
                        this.TxtInceptUser.Text = messInfo.Sender;
                        this.RadIncept3.Checked = true;
                        this.TxtTitle.Text = "回复:" + messInfo.Title;
                    }
                }
            }
        }

        //发送
        protected void BtnSend_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                M_Message messInfo = new M_Message();
                B_User bll = new B_User();
                messInfo.Sender = "admin";
                messInfo.Title = this.TxtTitle.Text;
                messInfo.PostDate = DataConverter.CDate(DateTime.Now.ToShortDateString());
                messInfo.Content = this.EditorContent.Text;
                if (this.RadIncept1.Checked)
                {
                    DataTable dt = bll.GetUserInfos();
                    foreach (DataRow dr in dt.Rows)
                    {
                        //if (dr["UserName"].ToString() != "admin")
                        //{
                        messInfo.Incept = dr["UserName"].ToString();
                        B_Message.Add(messInfo);
                        //}
                    }
                }
                if (this.RadIncept3.Checked)
                {
                    string uname = this.TxtInceptUser.Text;
                    if (!string.IsNullOrEmpty(uname))
                    {
                        string[] namearr = uname.Split(new char[] { ',' });
                        for (int i = 0; i < namearr.Length; i++)
                        {
                            messInfo.Incept = namearr[i];
                            B_Message.Add(messInfo);
                        }
                    }
                }
            }
        }
        //清除
        protected void BtnReset_Click(object sender, EventArgs e)
        {
            this.EditorContent.Text = "";
        }
    }
}