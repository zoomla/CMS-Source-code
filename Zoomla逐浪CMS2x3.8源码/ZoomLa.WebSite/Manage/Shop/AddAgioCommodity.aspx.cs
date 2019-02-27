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
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class manage_Shop_AddAgioCommodity : CustomerPageAction
{
    B_Product pll = new B_Product();
    B_BindPro bll = new B_BindPro();
    private B_Node bn = new B_Node();
    string path = CustomerPageAction.customPath2 + "Content/";
    string hasChild, noChild;
    protected void Page_Load(object sender, EventArgs e)
    {
        hasChild = "<input type=\"hidden\" id=\"Pronames{0}\" value=\"{1}\" /><img src=\"/Images/TreeLineImages/t.gif\" border=\"0\" /><input name=\"Item\" id=\"Item_{2}_{0}\" type=\"checkbox\" onclick=\"SetNodeSelect(this,{0});\" value=\"{0}\"><img src=\"/Images/TreeLineImages/plus.gif\" border=\"0\" /><a href='javascript:;' id='a{0}' class='list1'><span class='list_span'>{1}</span></a>";
        noChild = "<input type=\"hidden\" id=\"Pronames{0}\" value=\"{1}\" /><img src=\"/Images/TreeLineImages/t.gif\" border=\"0\" /><input name=\"Item\" id=\"Item_{2}_{0}\" type=\"checkbox\" onclick=\"SetNodeSelect(this,{0});\" value=\"{0}\"><img src=\"/Images/ModelIcon/Article.gif\" border=\"0\" /><a href='javascript:;'>{1}</a>";
        BindNode();
        if(!IsPostBack)
        {
            string type = Request.QueryString["type"];
            if (type == "1")
            {
                DataTable perminfo = pll.GetProductAll(0);
                tr1.Visible = true;
                tr2.Visible = false;
                Add_B.Attributes.Add("OnClick", "GetCheckvalue();return false;");
                DataBind();
            }
            else
            {
                tr1.Visible = false;
                tr2.Visible = true;
                Add_B.Attributes.Add("OnClick", "GetCheckNode();return false;");
            }
        }        
        Call.SetBreadCrumb(Master, "<li>商城管理</li><li>促销管理</li><li><a href='AddagioCommodity.aspx'>促销商品列表</a></li>");
    }
    public void DataBind(string key="")
    {
        DataTable dt = new DataTable();//pll.GetProductAll(0);
        if(!string.IsNullOrEmpty(key))
            pll.ProductSearch(0, key);
        else
            dt = pll.GetProductAll(0);
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected void txtPageFunc(string size)
    {
        int pageSize;
        if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
        {
            pageSize = EGV.PageSize;
        }
        else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
        {
            pageSize = EGV.PageSize;
        }
        EGV.PageSize = pageSize;
        EGV.PageIndex = 0;//改变后回到首页
        size = pageSize.ToString();
        DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        DataBind();
    }
    public string getproimg(string type)
    {
        string restring;
        restring = "";

        if (!string.IsNullOrEmpty(type)) { type = type.ToLower(); }
        if (!string.IsNullOrEmpty(type) && (type.IndexOf(".gif") > -1 || type.IndexOf(".jpg") > -1 || type.IndexOf(".png") > -1))
        {
            restring = "<img src=/" + type + " border=0 width=60 height=45>";
        }
        else
        {
            restring = "<img src=/UploadFiles/nopic.gif border=0 width=60 height=45>";
        }
        return restring;

    }
    protected void Cancel_Add_B_Click(object sender, EventArgs e)
    {
        Response.Write("<script language=javascript>window.close();</script>");
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        string KeyWord = TxtKeyWord.Text.Trim();
        DataBind(KeyWord);
    }
    protected void BindNode()
    {
        DataTable dt = this.bn.GetAllUserShopNode(); 
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["NodeName"].ToString().Length > 9)
            {
                dr["NodeName"] = dr["NodeName"].ToString().Substring(0, 9) + "..";
            }
        }
        nodeHtml.Text = "<ul class='tvNav'><li><a  class='list1' id='a0' href='javascript:;' ><span class='list_span'>全部内容</span><span class='fa fa-list'></span></a>" + GetLI(dt) + "</li></ul>";
    }
    public string GetLI(DataTable dt, int pid = 0)
    {
        string result = "";
        DataRow[] dr = dt.Select("ParentID='" + pid + "'");
        for (int i = 0; i < dr.Length; i++)
        {
            result += "<li>";
            if (dt.Select("ParentID='" + Convert.ToInt32(dr[i]["NodeID"]) + "'").Length > 0)
            {
                result += string.Format(hasChild, dr[i]["NodeID"], dr[i]["NodeName"], dr[i]["ParentID"]);
                result += "<ul class='tvNav tvNav_ul' style='display:none;'>" + GetLI(dt, Convert.ToInt32(dr[i]["NodeID"])) + "</ul>";
            }
            else
            {
                result += string.Format(noChild, dr[i]["NodeID"], dr[i]["NodeName"], dr[i]["ParentID"]);
            }
            result += "</li>";
        }
        return result;
    }
}
