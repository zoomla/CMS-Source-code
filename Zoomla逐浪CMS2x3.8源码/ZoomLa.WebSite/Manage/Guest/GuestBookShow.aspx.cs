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
using System.Data.SqlClient;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Manage_I_Guest_GuestBookShow : CustomerPageAction
{
    protected B_GuestBook guestBll = new B_GuestBook();
    protected M_GuestBook guestMod = new M_GuestBook();
    protected void Page_Load(object sender, EventArgs e)
    {
        string str = "分类名称";
        function.AccessRulo();
        Egv.txtFunc = txtPageFunc;
        if (!this.Page.IsPostBack)
        {
            if(!string.IsNullOrEmpty(Request.QueryString["CateID"]))
            {
                guestMod = guestBll.GetQuest(Convert.ToInt32(Request.QueryString["CateID"]));
                if (guestMod != null)
                    str = guestMod.Title;
            }
            int GID = string.IsNullOrEmpty(Request.QueryString["GID"]) ? 0 : DataConverter.CLng(Request.QueryString["GID"]);
            if (GID <= 0)
                function.WriteErrMsg("留言ID不正确！", "../Plus/Default.aspx");
            else
            {
                this.HdnGID.Value = GID.ToString();
                dBind();
                DataBind();
            }
            
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='WdCheck.aspx'>百科问答</a></li><li><a href='GuestCateMana.aspx'>留言分类管理</a></li><li><a href='Default.aspx?CateID=" + Request.QueryString["CateID"] + "'>" + str + "</a></li><li class='active'>留言内容</li>");
    }
    //获取单个留言
    public static DataTable GetReply(int GID)
    {
        string strSql = "select * from ZL_GuestBook where GID=@GID order by GDate Desc";
        SqlParameter[] sp = new SqlParameter[] { 
            new SqlParameter("@GID", SqlDbType.Int)
        };
        sp[0].Value = GID;
        return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
    }
    public void DataBind(string key = "")
    {
        int GID = Convert.ToInt32(Request.QueryString["GID"]);
        DataTable dt = GetAllReply(GID);
        Egv.DataSource = dt;
        Egv.DataBind();
    }
    protected void txtPageFunc(string size)
    {
        int pageSize;
        if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
        {
            pageSize = Egv.PageSize;
        }
        else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
        {
            pageSize = Egv.PageSize;
        }
        Egv.PageSize = pageSize;
        Egv.PageIndex = 0;//改变后回到首页
        size = pageSize.ToString();
        DataBind();
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        DataBind();
    }    
    private void dBind()
    {
        int GID = DataConverter.CLng(this.HdnGID.Value);
        DataTable dt = GetReply(GID);
        int Total = B_GuestBook.GetTipsTotal(GID);
        this.Repeater1.DataSource = dt;
        this.Repeater1.DataBind();
    }
    public string GetUserName(string UserID)
    {
        B_User buser = new B_User();
        return buser.SeachByID(DataConverter.CLng(UserID)).UserName;
    }
    protected void repFileReName_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            string Id = e.CommandArgument.ToString();
            B_GuestBook.DelTips(DataConverter.CLng(Id));
            dBind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx?CateID=" + Request.QueryString["CateID"]);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReplyGuest.aspx?CateID=" + Request.QueryString["CateID"] +"&GID="+ this.HdnGID.Value);
    }

    //------显示回复列表
    public DataTable GetAllReply(int GID)
    {
        string strSql = "select * from ZL_GuestBook where ParentID=@GID order by GDate Desc";
        SqlParameter[] sp = new SqlParameter[] { 
            new SqlParameter("@GID", SqlDbType.Int)
        };
        sp[0].Value = GID;
        return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
    }
    //删除等操作
    protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
    {
        if (this.Page.IsValid)
        {
            if (e.CommandName == "Del")
            {
                string Id = e.CommandArgument.ToString();
                B_GuestBook.DelTips(DataConverter.CLng(Id));
                DataBind();
            }
        }
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        string[] chkArr = GetChecked();
        if (chkArr != null)
        {
            for (int i = 0; i < chkArr.Length; i++)
            {
                int itemID = Convert.ToInt32(chkArr[i]);
                B_GuestBook.DelTips(itemID);
            }
        }
        DataBind();
        Page.ClientScript.RegisterStartupScript(this.GetType(),"wt","location=location;",true);
    }
    private string[] GetChecked()
    {
        if (!string.IsNullOrEmpty(Request.Form["chkSel"]))
        {
            string[] chkArr = Request.Form["chkSel"].Split(',');
            return chkArr;
        }
        else
            return null;
    }
}