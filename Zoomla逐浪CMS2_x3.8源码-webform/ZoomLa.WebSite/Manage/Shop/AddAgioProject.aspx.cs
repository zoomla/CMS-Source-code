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

public partial class manage_Shop_AddAgioProject : CustomerPageAction
{
    protected string type = "添加";
    B_Scheme bs = new B_Scheme();
    B_Product pll = new B_Product();
    B_Node bn = new B_Node();
    public int PID
    {
        get {
            if (ViewState["ID"] != null)
                return int.Parse(ViewState["ID"].ToString());
            else
                return 0;
            }
            set { ViewState["ID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ZoomLa.Common.function.AccessRulo();
        B_Admin ba = new B_Admin();
        ba.CheckIsLogin();
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                PID = int.Parse(Request.QueryString["ID"]);
                M_Scheme ms = bs.GetSelect(PID);
                txtName.Text = ms.SName;
                RadioButtonList1.SelectedValue = ms.SType.ToString();
                if (ms.SStartTime.ToString() == ms.SEndTime.ToString())
                {
                    txtEndTime.Text = "";
                    txtStartTime.Text = "";
                }
                else
                {
                    txtStartTime.Text = ms.SStartTime.Date.ToString();
                    txtEndTime.Text = ms.SEndTime.Date.ToString();
                }
                
                PromoBind(ms.SList);
                type = "修改";
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li><li><a href='PresentProject.aspx'>促销方案管理</a></li><li>" + type + "打折方案</li>");
        }
    }
    protected void Submit_B_Click(object sender, EventArgs e)
    {
        string str = Request.Form["PromoProlist"];
        if (!string.IsNullOrEmpty(str))
        {
            str = OrderNode(bn.SelByIDS(str), "", "");
            if (str.LastIndexOf(",") >0)
            {
                str = str.TrimEnd(',');
            }
            M_Scheme ms = bs.GetSelect(PID);
            ms.SStartTime = DataConverter.CDate(txtStartTime.Text);
            ms.SEndTime = DataConverter.CDate(txtEndTime.Text);
            ms.SType = DataConverter.CLng(RadioButtonList1.SelectedValue.ToString());
            ms.SName = txtName.Text;
            ms.SList = str;
            if (Request.QueryString["ID"] != null)
            {
                bs.GetUpdate(ms);
            }
            else
            {
                ms.SAddTime = DateTime.Now;
                bs.GetInsert(ms);
            }
            Response.Write("<script>location.href='AgioProject.aspx'</script>");
        }
        else
        {
            Response.Write("<script>window.alert('请选择打折商品！');</script>");
        }
    }
    private string OrderNode(DataTable dt, string str, string num)
    {
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string g = dr["ParentID"].ToString();
                bool b = str.Contains(dr["NodeID"].ToString());
                if (num != "")
                {
                    if (dr["ParentID"].ToString() == num && !str.Contains(dr["NodeID"].ToString()))
                    {
                        str += dr["NodeID"].ToString() + ",";
                        str = OrderNode(dt, str, dr["NodeID"].ToString());
                    }
                }
                else
                {
                    if (!str.Contains(dr["NodeID"].ToString()))
                    {
                        str += dr["NodeID"].ToString() + ",";
                        str = OrderNode(dt, str, dr["NodeID"].ToString());
                    }

                }
            }
        }
        return str;
    }
    private void PromoBind(string PromoProlist)
    {
        if (!string.IsNullOrEmpty(PromoProlist))
        {
            if (RadioButtonList1.SelectedValue == "1")
            {
                string[] listarr = PromoProlist.Split(',');
                for (int i = 0; i < listarr.Length; i++)
                {
                    M_Product proMod = pll.GetproductByid(DataConverter.CLng(listarr[i]));
                    Page.ClientScript.RegisterStartupScript(this.GetType(), i.ToString(), "SetPro('" + proMod.Proname + "','" + proMod.ID + "');", true);
                }
            }
            else
            {
                string[] listarr = PromoProlist.Split(new string[] { "," }, StringSplitOptions.None);
                for (int i = 0; i < listarr.Length; i++)
                {
                    M_Node nodeMod = bn.GetNodeXML(DataConverter.CLng(listarr[i]));
                    Page.ClientScript.RegisterStartupScript(this.GetType(), i.ToString(), "SetPro('" + nodeMod.NodeName + "','" + nodeMod.NodeID + "');", true);
                }
            }
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "selall", "selall();", true);
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            M_Scheme schMod = bs.GetSelect(Convert.ToInt32(Request.QueryString["ID"]));
            if (RadioButtonList1.SelectedValue == schMod.SType.ToString())
                PromoBind(schMod.SList);
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "clearall", "clearAll();", true);
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "clearall", "clearAll();", true);
        }
    }
}
