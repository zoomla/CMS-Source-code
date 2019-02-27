using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Sns;

public partial class Zone_ChatStart : System.Web.UI.Page
{
    #region 业务对象
    ChatLogBLL clbll = new ChatLogBLL();
    #endregion

    #region 页面初始化
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ViewState["id"] = base.Request.QueryString["id"];
            MyBind();
        }
    }
    #endregion

    #region 页面方法
    private int id
    {
        get
        {
            if (ViewState["id"] != null)
                return int.Parse(ViewState["id"].ToString());
            else
                return 0;
        }
        set
        {
            id = value;
        }
    }
    private string Name
    {
        get
        {
            if (ViewState["Name"] != null)
                return ViewState["Name"].ToString();
            else
                return string.Empty;
        }
        set
        {
            ViewState["Name"] = value;
        }
    }
    private string Sex
    {
        get
        {
            if (ViewState["Sex"] != null)
                return ViewState["Sex"].ToString();
            else
                return string.Empty;
        }
        set
        {
            ViewState["Sex"] = value;
        }
    }
    private void MyBind()
    {
        List<ChatLog> list = clbll.GetCharLog(id);
        EGV.DataSource = list;
        EGV.DataBind();
    }    
    protected void Button1_Click(object sender, EventArgs e)
    {
        ViewState["Sex"] = this.DropDownList1.Text;
        ViewState["Name"] = this.txtName.Text;
        this.MultiView1.ActiveViewIndex = 1;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        this.MultiView1.ActiveViewIndex = 0;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ChatLog cl = new ChatLog();
        cl.ByID = id;
        cl.Name = Name;
        cl.Sex = Sex;
        cl.ChatContent = this.TextBox1.Text;
        cl.ChatType = this.typeLinkButton.Text;
        clbll.InsertLog(cl);
        this.TextBox1.Text = "";
        MyBind();
    }
    protected string GetType(string ss)
    {
        if (ss == Name)
            return "自言自语";
        else
            return "对"+ss;
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        this.typeLinkButton.Text = lb.CommandName;
    }
    
    protected void typeLinkButton_Click(object sender, EventArgs e)
    {
        this.typeLinkButton.Text = "所有人";
    }
 
    protected void Button3_Click1(object sender, EventArgs e)
    {
        clbll.DelChat();
        MyBind();
    }
    #endregion
}
