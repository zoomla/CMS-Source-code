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
using ZoomLa.Components;
using ZoomLa.Common;
using System.Globalization;
using ZoomLa.Model;
using System.Text;
public partial class manage_Boss_Bosstree : CustomerPageAction
{
    B_BossInfo b_Boss = new B_BossInfo();
    M_BossInfo m_Boss = new M_BossInfo();
    B_User u = new B_User();
    B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li>  <li>加盟商管理[<a href='AddBoss.aspx?nodeid=1'>添加加盟商 </a>]</li>");
        if (!this.Page.IsPostBack)
        {
            if (Request.QueryString["type"] != null && Request.QueryString["id"] != null)
            {
                if (Request.QueryString["type"].ToString() == "del")
                {
                    b_Boss.GetDelete(DataConverter.CLng(Request.QueryString["id"].ToString()));
                }
            }
            B_Node bll = new B_Node();
            string menu = string.IsNullOrEmpty(Request.QueryString["menu"]) ? "0" : Request.QueryString["menu"].ToString();
          
       
        
        }
        if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "DeliverType"))
        {
            function.WriteErrMsg("没有权限进行此项操作");
        }
        DataTable dt = b_Boss.Sel();
        RPT.DataSource = dt;
        RPT.DataBind();
        string types = Request.QueryString["types"];
        string Nid = Request.QueryString["nodeid"];
        if (types != null)
        {
            b_Boss.DelByNodeId(Convert.ToInt32(Nid));
            function.WriteSuccessMsg("删除成功!", "Bosstree.aspx");
        }
        


     }
    private string GetManagePath()
    {
        return SiteConfig.SiteOption.ManageDir.ToLower(CultureInfo.CurrentCulture);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        m_Boss = b_Boss.GetSelectName(TextBox1.Text);
        if (m_Boss.nodeid > 0)
        {
            Response.Redirect("Bosstree.aspx?nodeid=" + m_Boss.nodeid + "&menu=shuai");
        }
        else
        {
            Response.Redirect("Bosstree.aspx?");
        }
    }
    public string formatcs(string money)
    {
        string outstr;
        double doumoney, tempmoney;
        doumoney = DataConverter.CDouble(money);
        tempmoney = System.Math.Round(doumoney, 2);
        outstr = tempmoney.ToString();
       return outstr;
        
    }
    public string UserNames(string uid)
    {
        string Uname = "";
        M_UserInfo mu = u.GetSelect(Convert.ToInt32(uid));
        if (!mu.IsNull) Uname = mu.UserName;
        return Uname;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string itemdata = Request.Form["idchk"];
      
         if (!string.IsNullOrEmpty(itemdata) && b_Boss.DeleteByList(itemdata) == true)
        {
            Response.Write("<script language=javascript>alert('批量删除成功!');location.href='Bosstree.aspx';</script>");
        }
        else
        {
            Response.Write("<script language=javascript>alert('批量删除失败!');location.href='Bosstree.aspx';</script>");
        }
    }
}