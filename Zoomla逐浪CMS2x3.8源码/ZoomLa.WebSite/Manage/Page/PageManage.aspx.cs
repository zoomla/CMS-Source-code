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
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.BLL.Page;
using ZoomLa.Model.Page;
using System.Xml;
using System.Data.SqlClient;

public partial class manage_Page_PageManage : CustomerPageAction
{
    protected B_ModelField b_ModelField = new B_ModelField();
    protected B_User b_User = new B_User();
    protected B_Content b_Content = new B_Content();
    protected B_Model b_Model = new B_Model();
    protected B_Admin b_Admin = new B_Admin();
    protected B_PageReg b_PageReg = new B_PageReg();
    protected M_PageReg m_PageReg = new M_PageReg();
    protected B_PageStyle styleBll = new B_PageStyle();

    public int type = 0;    //0、全部；1、待审核；2、审核
    public string strWhere = string.Empty;
    private DataTable dt = new DataTable();
    private int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    private int SType { get { return DataConverter.CLng(Request["Type"]); } }
    private string KeyWord { get { return ViewState["KeyWord"] as string; } set { ViewState["KeyWord"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            DPBind();
            Option();
            MyBind();
            Call.HideBread(Master);
        }
    }
    public DataTable StyleDT
    {
        get
        {
            if (ViewState["StyleDT"] == null)
            {
                ViewState["StyleDT"] = styleBll.Sel();
            }
            return ViewState["StyleDT"] as DataTable;
        }
        set { ViewState["StyleDT"] = value; }
    }
    private void DPBind()
    {
        style_DP.DataSource = StyleDT;
        style_DP.DataTextField = "PageNodeName";
        style_DP.DataValueField = "PageNodeID";
        style_DP.DataBind();
    }
    // 操作
    public void Option()
    {
        if (!string.IsNullOrEmpty(Request["menu"]))
        {
            M_CommonData b_CommonData = new M_CommonData();
            switch (Request["menu"])
            {
                case "cancel":
                    m_PageReg = b_PageReg.SelReturnModel(Mid);
                    m_PageReg.Status = 0;
                    b_PageReg.UpdateByID(m_PageReg);
                    Response.Write("<script>alert('操作成功!');location.href='PageManage.aspx';</script>");
                    break;
                case "Audit":
                    m_PageReg = b_PageReg.SelReturnModel(Mid);
                    m_PageReg.Status = 99;
                    b_PageReg.UpdateByID(m_PageReg);
                    Response.Write("<script>alert('操作成功!');</script>");
                    Response.Redirect("PageManage.aspx");
                    break;
                case "noLevel":
                    m_PageReg = b_PageReg.SelReturnModel(Mid);
                    m_PageReg.Recommendation = "0";
                    b_PageReg.UpdateByID(m_PageReg);
                    Response.Write("<script>alert('取消推荐成功!');location.href='PageManage.aspx';</script>");
                    break;
                case "Level":
                    m_PageReg = b_PageReg.SelReturnModel(Mid);
                    m_PageReg.Recommendation = "1";
                    b_PageReg.UpdateByID(m_PageReg);
                    Response.Write("<script>alert('推荐成功!');location.href='PageManage.aspx';</script>");
                    break;
                case "del":
                    b_PageReg.Del(Mid);
                    Response.Write("<script>alert('操作成功!');location.href='PageManage.aspx';</script>");
                    break;
                default: break;
            }
        }
    }
    public void MyBind()
    {
        switch (SType)
        {
            case 1:
                dt = b_PageReg.SelByStatus("99", KeyWord);
                break;
            case 2:
                dt = b_PageReg.SelByStatus("0", KeyWord);
                break;
            case 0:
            default:
                dt = b_PageReg.SelByStatus("", KeyWord);
                break;
        }
        RPT.DataSource = dt;
        RPT.DataBind();
    }
    public string GetCurStatus()
    {
        return Eval("Status").ToString() == "99" ? "已审核" : "待审核";
    }
    // 审核状态
    public string GetStatus()
    {
        if (Eval("Status").ToString() == "99")
        {
            return "<a class='option_style' href='?menu=cancel&id=" + Eval("ID") + "' title='已审核'><i class='fa fa-flag'></i><span style='color:red'>取消审核</span></a>";
        }
        else
        {
            return "<a class='option_style' href='?menu=Audit&id=" + Eval("ID") + "' title='未审核' onclick='alert(\"新增黄页审核通过后，必须进入[黄页用户节点管理]分配或定义样式，否则用户无法添加黄页信息;系统将自动跳转到[黄页用户节点管理]\");'><i class='fa fa-flag'></i><span style='color:green'>通过审核</span></a>";
        }
    }
    // 推荐状态
    protected string GetRecommendation()
    {
        if (Eval("Recommendation").ToString() == "1")
        {
            return "<a class='option_style' href='?menu=noLevel&id=" + Eval("ID") + "' title='取消推荐'><i class='fa fa-thumb-tack'></i><span style='color:blue'>已推荐</span></a>";
        }
        else
        {
            return "<a class='option_style' href='?menu=Level&id=" + Eval("ID") + "' title='推荐'><i class='fa fa-thumb-tack'></i><span style='color:red'>未推荐</span></a>";
        }
    }
    //搜索
    protected void Button1_Click(object sender, EventArgs e)
    {
        KeyWord = keyword.Text;
        MyBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            if (b_PageReg.UpdateByField("[Status]", "99", Request.Form["idchk"]))
            {
                function.WriteSuccessMsg("操作成功");
            }
            else
            {
                function.WriteErrMsg("操作失败");
            }
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            if (b_PageReg.UpdateByField("[Status]", "0", Request.Form["idchk"]))
            {
                function.WriteSuccessMsg("操作成功!");
            }
            else
            {
                function.WriteSuccessMsg("操作失败!");
            }
        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            if (b_PageReg.DelByIDS(Request.Form["idchk"]))
            {
                Response.Write("<script>alert('操作成功!');location.href='PageManage.aspx';</script>");
            }
            else
            {
                Response.Write("<script>alert('操作失败!');location.href='PageManage.aspx';</script>");
            }
        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            if (b_PageReg.UpdateByField("[Recommendation]", "1", Request.Form["idchk"]))
            {
                Response.Write("<script>alert('操作成功!');location.href='PageManage.aspx';</script>");
            }
            else
            {
                Response.Write("<script>alert('操作失败!');location.href='PageManage.aspx';</script>");
            }
        }
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {

            if (b_PageReg.UpdateByField("[Recommendation]", "0", Request.Form["idchk"]))
            {
                Response.Write("<script>alert('操作成功!');location.href='PageManage.aspx';</script>");
            }
            else
            {
                Response.Write("<script>alert('操作失败!');location.href='PageManage.aspx';</script>");
            }
        }
    }
    protected void selcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("PageManage.aspx?tid=" + Request.QueryString["tid"] + "&type=" + SType + "&province=" + Request.Form["selprovince"] + "&city=" + Request.Form["selcity"]);
    }
    public string Getcurrent()
    {
        return CustomerPageAction.customPath2;
    }
    public string GetStyleList(int uid)
    {
        string result = "";
        string tlp = "[<a href='PageTemplate.aspx?StyleID={0}&UserID={2}'>{1}</a>]";
        foreach (DataRow dr in StyleDT.Rows)
        {
            result += string.Format(tlp, dr["PageNodeID"], dr["PageNodeName"], uid);
        }
        return result;
    }
}