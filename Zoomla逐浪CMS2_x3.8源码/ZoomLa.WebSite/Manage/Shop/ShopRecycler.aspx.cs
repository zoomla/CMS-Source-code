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
using ZoomLa.Common;
using ZoomLa.Model;
using System.Text;

public partial class manage_Shop_ShopRecycler : CustomerPageAction
{
    private B_Product bll = new B_Product();
    private B_Node bNode = new B_Node();
    private B_Model bmode = new B_Model();
    protected B_BindPro bd = new B_BindPro();
    public int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
    B_Admin badmin = new B_Admin();
    OrderCommon orderCom = new OrderCommon();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "ProductManage"))
        {
            function.WriteErrMsg("没有权限进行此项操作");
        }
        if (!IsPostBack)
        {
            DataBind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li><li class='active'>商品回收站</li>");
    }
    public void DataBind(string key = "")
    {
        DataTable dt = new DataTable();
        if (NodeID>0)
            dt = bll.GetProductnodidRecycler(NodeID);
        else
            dt = bll.GetProductAllRecycler();
        if (!string.IsNullOrEmpty(Request.QueryString["keyWord"]))
        {
            dt = bll.GetProductnodidRecycler(0);
            if (DataConverter.CLng(Request.QueryString["keyWord"]) > 0)
                dt.DefaultView.RowFilter = "ID=" + DataConverter.CLng(Request.QueryString["keyWord"]);
            else
                dt.DefaultView.RowFilter = "ProName like '%" + Request.QueryString["keyWord"] + "%'";
            dt = dt.DefaultView.ToTable();
        }
        Egv.DataSource = dt;
        Egv.DataBind();
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        DataBind();
    }
    protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int pid=Convert.ToInt32(e.CommandArgument.ToString());
        switch (e.CommandName.ToLower())
        {
            case "restore":
                if (bll.UpDeleteByID(pid))
                    function.WriteSuccessMsg("还原成功!");
                else
                 function.WriteSuccessMsg("还原失败!");
                break;
            case "del1":
                bll.RealDeleteByID(pid, bll.GetproductByid(pid));
                bd.SelByProID(pid,2);
                break;
            default :
                break;
        }
        DataBind();
    }
    public string formattype(string type)
    {
        int newtype;
        string restring;
        restring = "";
        newtype = DataConverter.CLng(type.ToString());
        if (newtype == 1)
        {
            restring = "<font color=blue>√</font>";
        }
        else if (newtype == 0)
        {
            restring = "<font color=red>×</font>";
        }
        return restring;
    }
    public string Stockshow(string id)
    {
        int cid;
        string str = "";
        cid = DataConverter.CLng(id);
        M_Product dd = bll.GetproductByid(cid);
        if (dd.Stock <= dd.StockDown)
        {
            str = "<font color=red>" + dd.Stock.ToString() + " [警]</font>";
        }
        else
        {
            str = dd.Stock.ToString();
        }
        return str;

    }
    public string formatnewstype(string type)
    {
        int newtype;
        string restring;
        restring = "";
        newtype = DataConverter.CLng(type.ToString());
        if (newtype == 2)
        {
            restring = "<font color=red>特价</font>";
        }
        else if (newtype == 1)
        {
            restring = "<font color=blue>待审核</font>";
        }
        return restring;
    }
    public string formatcs(string money)
    {
        string outstr;
        double doumoney, tempmoney;
        doumoney = DataConverter.CDouble(money);
        tempmoney = System.Math.Round(doumoney, 2);
        outstr = tempmoney.ToString();
        if (outstr.IndexOf(".") == -1)
        {
            outstr = outstr + ".00";
        }
        return outstr;
    }
    public string forisnew(string type)
    {
        int newtype;
        string restring;
        restring = "";
        newtype = DataConverter.CLng(type.ToString());
        if (newtype == 1)
        {
            restring = "<font color=green>新</font>";
        }
        else if (newtype == 0)
        {
            restring = "&nbsp;&nbsp;";
        }
        return restring;
    }
    public string forishot(string type)
    {
        int newtype;
        string restring;
        restring = "";
        newtype = DataConverter.CLng(type.ToString());
        if (newtype == 1)
        {
            restring = "<font color=red>热</font>";
        }
        else if (newtype == 0)
        {
            restring = "&nbsp;&nbsp;";
        }
        return restring;
    }
    public string forisbest(string type)
    {
        int newtype;
        string restring;
        restring = "";
        newtype = DataConverter.CLng(type.ToString());
        if (newtype == 1)
        {
            restring = "<font color=blue>精</font>";
        }
        else if (newtype == 0)
        {
            restring = "&nbsp;&nbsp;";
        }
        return restring;
    }
    public string getproimg()
    {
        return function.GetImgUrl(Eval("Thumbnails"));
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (string.IsNullOrEmpty(ids)) function.WriteErrMsg("未选中值");
        else { bll.RealDelByIDS(ids); function.WriteSuccessMsg("删除成功"); }
        DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string CID = Request.Form["idchk"];
        if (!string.IsNullOrEmpty(CID) && bll.setproduct(12, CID))
            function.WriteSuccessMsg("批量还原成功");
        DataBind();
    }
    //清空
    protected void Button4_Click(object sender, EventArgs e)
    {
        bll.setproduct(15, "1");
        function.WriteSuccessMsg("删除成功!");
    }
}
