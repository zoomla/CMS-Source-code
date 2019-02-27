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
using ZoomLa.SQLDAL;
using System.Xml;
using ZoomLa.BLL.Plat;
using ZoomLa.Model.Plat;

/*
 * 用于能力中心,仅显示能力中心,本公司的用户
 */ 
public partial class Plat_Common_SelUser : System.Web.UI.Page
{
    /*
       后期改为AJAX,分页获取数据
     */
    B_Admin badmin = new B_Admin();
    B_User_Plat upBll = new B_User_Plat();
    M_User_Plat upMod = new M_User_Plat();
    B_Plat_Group gpBll = new B_Plat_Group();
    int compID = 0;
    //需要过滤的UserID,格式:1,2,3
    public string FilterID
    {
        get
        {
            return ViewState["FilterID"] as string;
        }
        private set
        {
            ViewState["FilterID"] = value;
        }
    }
    public string GroupID
    {
        get
        {
            return GroupID_H.Value as string;
        }
        set { GroupID_H.Value = value; }
    }
    //当前关键词
    public string KeyWord
    {
        get
        {
            return ViewState["KeyWord"] as string;
        }
        set { ViewState["KeyWord"] = value; }
    }
    /*
     * 父页面需要实现三个JS方法,详见示例页
     * 关闭与清空chk方法，必须放在父页面调用,该页面只允许实现,选择会员，并返回Json数据.
     * 清空调用本页ClearChk();
     */
    protected void Page_Load(object sender, EventArgs e)
    {
        upMod = B_User_Plat.GetLogin();
        if ((upMod == null))
            function.WriteErrMsg("无权访问该页面");
        compID = upMod.CompID;
        if (!IsPostBack)
        {
            DataTable dt = gpBll.SelByCompID(compID);
            FilterID = Request.QueryString["fid"];
            dt = dt.DefaultView.ToTable();
            AllInfo_Div.Visible = true;
            //将空的单列一组
            string dli = "<li><a href='javascript:;' onclick='hiddenul(this);FilterTr(0,&quot;全部用户&quot;)'>全部用户</a></li>";
            dli += "<li><a href='javascript:;' onclick='hiddenul(this);FilterTr(-1,&quot;未分组&quot;)'>未分组</a></li>";
            AllInfo_Litral.Text = "<ul id='GroupSel'>" + dli + GetTable(dt, disType.AllInfo) + "</ul>";
            if (dt != null && dt.Rows.Count > 0)
            {
                GroupID = dt.Rows[0]["ID"].ToString();
            }
            MyBind();
        }
    }
    string chkTlp = "<input type='checkbox' value='{0}:{1}' onclick='SaveGroup(this);' class='GroupChk' />";
    public string GetTable(DataTable dt, disType type, int pid = 0)
    {
        string result = "";
        DataRow[] dr = dt.Select("PGroup=" + pid);
        for (int i = 0; i < dr.Length; i++)
        {
            dt.DefaultView.RowFilter = "PGroup=" + dr[i][0];
            result += "<li>{0}<a href='javascript:;' onclick='hiddenul(this);FilterTr(" + dr[i]["ID"] + ",\"" + dr[i]["GroupName"] + "\")'>" + dr[i]["GroupName"] + "</a>" + string.Format(chkTlp, dr[i]["ID"], dr[i]["GroupName"]);
            if (dt.DefaultView.ToTable().Rows.Count > 0)//是否还有子组，如无，则使用其他图片
                result = string.Format(result, "");
            else
                result = string.Format(result, "");

            result += "<ul style='padding-left:15px;'>" + GetTable(dt, type, Convert.ToInt32(dr[i][0])) + "</ul>";
            result += "</li>";
        }
        return result.Replace("<ul></ul>", "");
    }
    public enum disType { CheckBox, Radio, Null, AllInfo };
    private void MyBind()
    {
        DataTable dt = new DataTable();
        if (!string.IsNullOrEmpty(KeyWord))
        {
            dt = upBll.SelByCompany(compID,KeyWord);
        }
        else if (!string.IsNullOrEmpty(GroupID))
        {
            switch (GroupID)
            {
                default:
                    M_Plat_Group gpMod = gpBll.SelReturnModel(Convert.ToInt32(GroupID));
                    break;
            }
            dt = upBll.SelByGroup(compID, Convert.ToInt32(GroupID));
        }
        else
        {
            dt = upBll.SelByCompany(compID);
        }
        //----视图过滤
        if (!string.IsNullOrEmpty(FilterID))
        {
            dt.DefaultView.RowFilter = "UserID Not in(" + FilterID + ")";
            dt = dt.DefaultView.ToTable();
        }
        RPT.DataSource = dt;
        RPT.DataBind();
        ScriptManager.RegisterStartupScript(UserDiv,UserDiv.GetType(),"Event","EventBind();",true);
    }
    protected void ReBind_Btn_Click(object sender, EventArgs e)
    {
        KeyWord = "";
        MyBind();
    }
    protected void keyBtn_Click(object sender, EventArgs e)
    {
        GroupID = "";
        KeyWord = Skey_T.Text.Trim();
        MyBind();
    }
    protected void showAll_Btn_Click(object sender, EventArgs e)
    {
        GroupID = "";
        KeyWord = "";
        MyBind();
    }
    public string IsChecked(string str)
    {
        if (UserInfo_H.Value.Contains(str))
            return "checked=\"checked\"";
        else
            return "";
    }
}