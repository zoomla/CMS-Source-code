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
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.SQLDAL;

public partial class manage_iServer_FiServer : Page
{
    B_IServer Serverbll = new B_IServer();
    B_User buser = new B_User();
    string[] typeArray ={"","咨询","投诉","建议","要求","界面使用", "bug报告", "订单", "财务", "域名", "主机" , "邮局" , "DNS", "MSSQL"
                            ,"MySQL", "IDC", "网站推广", "网站制作", "其它"};
    string menu = "";
    string orderId = "";
    int type = 0;
    int currentUser = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = buser.GetLogin().UserID;
        if (!IsPostBack)
        {
            string url = Request.CurrentExecutionFilePath;
            DataTable list = Sql.Sel("ZL_User", "UserID=" + currentUser, "");
            if (list.Rows.Count != 0)
            {
                string str = list.Rows[0]["seturl"].ToString();
                string[] strarr = str.Split(',');
                for (int i = 0; i <= strarr.Length - 1; i++)
                {
                    if (strarr[i].ToLower() == url.ToLower())
                    {
                        DV_show.Visible = false;
                        Login.Visible = true;
                        return;
                    }
                }
            }            
            MyBind();
        }        
    }
    protected void sure_Click(object sender, EventArgs e)
    {
        M_UserInfo info = buser.GetLogin();
        string PWD = Second.Text;

        if (StringHelper.MD5(PWD) == info.PayPassWord)
        {
            DV_show.Visible = true;
            Login.Visible = false;
        }
        else
        {
            Response.Write("<script>alert('密码错误,请重新输入！');</script>");
            DV_show.Visible = false;
            Login.Visible = true;            ;
        }
    }
    public void MyBind()
    {
        B_User buser = new B_User();
        M_UserInfo info = buser.GetLogin();
        string state = "1";
        string num = Request.QueryString["num"] == null ? "" : Request.QueryString["num"];
        switch (num)
        {
            case "1":
                state = "未解决";
                break;
            case "2":
                state = "处理中";
                break;
            case "3":
                state = "已解决";
                break;
            case "4":
                state = "已锁定";
                break;
            default:
                state = "未解决";
                break;
        }
        GetQueryString();
        lblAllNum.Text = Serverbll.getiServerNum("", info.UserID, menu, typeArray[type], DataConvert.CLng(orderId)).ToString();
        lblNum_ch.Text = Serverbll.getiServerNum("处理中", info.UserID, menu, typeArray[type], DataConvert.CLng(orderId)).ToString();
        lblnum_w.Text = Serverbll.getiServerNum("未解决", info.UserID, menu, typeArray[type], DataConvert.CLng(orderId)).ToString();
        lblnum_y.Text = Serverbll.getiServerNum("已解决", info.UserID, menu, typeArray[type], DataConvert.CLng(orderId)).ToString();
        lblSock.Text = Serverbll.getiServerNum("已锁定", info.UserID, menu, typeArray[type], DataConvert.CLng(orderId)).ToString();

        if (DataConverter.CLng(lblnum_w.Text.ToString()) >= 0)
            panel_w.Visible = true;
        if (DataConverter.CLng(lblNum_ch.Text.ToString()) >= 0)
            panel_ch.Visible = true;
        if (DataConverter.CLng(lblnum_y.Text.ToString()) >= 0)
            panel_y.Visible = true;
        if (DataConverter.CLng(lblSock.Text.ToString()) >= 0)
            panel_sock.Visible = true;

        resultsRepeater_w.DataSource = Serverbll.SeachTop(state, info.UserID, menu, typeArray[type], DataConvert.CLng(orderId));
        resultsRepeater_w.DataBind();

        //读取用户提交的问题分类
        repSeachBtn.DataSource = Serverbll.GetSeachUserIdType(info.UserID);
        repSeachBtn.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string strTitle = this.TextBox1.Text.ToString();
        GetQueryString();
        if(menu!="")
            Response.Redirect("SelectiServer.aspx?num=-1&menu=" + menu + "&strTitle=" + strTitle);
        else
            Response.Redirect("SelectiServer.aspx?num=-1&strTitle=" + strTitle);
    }
    private void GetQueryString()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["menu"]))
        {
            menu = Request.QueryString["menu"];
        }
        if (!string.IsNullOrEmpty(Request.QueryString["OrderID"]))
        {
            orderId = Request.QueryString["OrderID"];
        }
        if (!string.IsNullOrEmpty(Request.QueryString["type"]))
        {
            type = DataConverter.CLng(Request.QueryString["type"]);
        }
    }
    protected int returnType(object typeName)
    {
        int index = 0;
        for (int i = 0; i < typeArray.Length; i++)
        {
            if (typeName.ToString().Trim() == typeArray[i])
            {
                index = i;
                break;
            }
        }
        return index;
    }
}
