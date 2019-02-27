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
using ZoomLa.Components;

namespace User
{
    public partial class MessageSend : CustomerPageAction
    {
        B_Admin badmin = new B_Admin();
        B_User buser = new B_User();
        B_Message bmsg = new B_Message();
        M_Message m_msg = new M_Message();
        public DataTable UserDT
        {
            get
            {
                if (Session["UserDT"] == null)
                {
                    Session["UserDT"] = buser.Sel();
                }
                return Session["UserDT"] as DataTable;
            }
            set
            {
                Session["UserDT"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.user, "MessManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                if (!string.IsNullOrEmpty(base.Request.QueryString["id"]))
                {
                    m_msg = B_Message.GetMessByID(DataConverter.CLng(base.Request.QueryString["id"]));
                    if (!m_msg.IsNull)
                    {
                        this.InceptUser_T.Text = m_msg.Sender;
                        this.RadIncept3.Checked = true;
                        this.TxtTitle.Text = "回复:" + m_msg.Title;
                    }
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='UserManage.aspx'>用户管理</a></li><li><a href='MessageSend.aspx'>信息发送</a></li><li>发送短消息</li>"); ;
        }
        protected void Send_Btn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                M_Message messInfo = new M_Message();
                messInfo.Sender = badmin.GetAdminLogin().AdminId.ToString();
                messInfo.Title = this.TxtTitle.Text;
                messInfo.PostDate = DataConverter.CDate(DateTime.Now.ToLocalTime().ToString());
                messInfo.Content = this.EditorContent.Text;
                messInfo.Receipt = "";
                messInfo.MsgType = 1;
                messInfo.status = 1;
                messInfo.Attachment = Attach_Hid.Value;
                messInfo.Incept = InceptUser_Hid.Value.Trim(',');
                if (this.RadIncept1.Checked)
                {
                    DataTable dt = UserDT;
                    string id="";
                    foreach (DataRow dr in dt.Rows)
                        id += dr["UserID"].ToString() + ",";
                    id = id.TrimEnd(',');
                    if (dt != null)
                        dt.Dispose();
                    messInfo.Incept = id;
                    B_Message.Add(messInfo);
                    function.WriteSuccessMsg("短信息已成功发送！", "Message.aspx");
                }

                if (this.RadIncept3.Checked)
                {
                    if (string.IsNullOrEmpty(messInfo.Incept)) { function.WriteErrMsg("收件人不存在!"); }
                    string uname = this.InceptUser_T.Text;
                    if (!string.IsNullOrEmpty(uname))
                    {
                        B_Message.Add(messInfo);
                        function.WriteSuccessMsg("短信息已成功发送！", "Message.aspx");
                    }
                }
                
            }
        }
        protected void Draft_Btn_Click(object sender, EventArgs e)
        {
            M_Message messInfo = new M_Message();
            messInfo.Sender = buser.GetLogin().UserID.ToString();
            messInfo.Title = Server.HtmlEncode(TxtTitle.Text);
            messInfo.Content = EditorContent.Text;
            messInfo.Savedata = 1;
            messInfo.Receipt = "";
            messInfo.status = 0;
            bmsg.GetInsert(messInfo);
            function.WriteSuccessMsg("短信息已存入草稿箱！", "Message.aspx");
        }
    }
}