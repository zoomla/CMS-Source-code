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
using ZoomLa.Web;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Xml;
using System.Text.RegularExpressions;
using System.Globalization;
using ZoomLa.BLL.Shop;

public partial class User_UserShop_ProductList : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Product bll = new B_Product();
    B_Model bmode = new B_Model();
    B_BindPro bd = new B_BindPro();
    B_ModelField bfield = new B_ModelField();
    B_BindFlolar bindflolar = new B_BindFlolar();
    B_Product buserpro = new B_Product();
    B_Node nodeBll = new B_Node();
    B_Content conBll = new B_Content();
    public int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
    public int Pid { get { return DataConverter.CLng(Request.QueryString["pid"]); } }
    public int QuickSouch
    {
        get
        {
            if (!string.IsNullOrEmpty(Request.QueryString["quicksouch"]))
            {
                quicksouch.SelectedValue = Request.QueryString["quicksouch"];
                return DataConverter.CLng(Request.QueryString["quicksouch"]);
            }
            else { return DataConverter.CLng(quicksouch.SelectedValue); }
        }
    }
    public string KeyWord
    {
        get
        {
            if (ViewState["KeyWord"] == null)
            { ViewState["KeyWord"] = string.IsNullOrEmpty(Request.QueryString["KeyWord"]) ? "" : Request.QueryString["KeyWord"]; }
            return ViewState["KeyWord"].ToString();
        }
        set { ViewState["KeyWord"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string ModeIDList = "";
            string str = "";
            string[] ModelID = null;
            if (NodeID > 0)
            {
                M_Node nod = nodeBll.GetNodeXML(NodeID);
                ModeIDList = nod.ContentModel;
                ModelID = ModeIDList.Split(',');
            }
            string tlp = " <div class='btn-group'><button type='button' class='btn btn-default dropdown-toggle' data-toggle='dropdown'>{0}管理<span class='caret'></span></button><ul class='dropdown-menu' role='menu'><li><a href='AddProduct.aspx?ModelID={1}&NodeID={2}'>添加{0}</a></li><li><a href='javascript:;' onclick='ShowImport();'>导入{0}</a></li></ul></div>";
            if (ModelID != null)
            {
                for (int i = 0; i < ModelID.Length; i++)
                {
                    M_ModelInfo model = bmode.GetModelById(DataConverter.CLng(ModelID[i]));
                    if (!string.IsNullOrEmpty(model.ItemName))
                    {
                        str += string.Format(tlp, model.ItemName, ModelID[i], NodeID);
                    }
                }
            }
            lblAddContent.Text = str;
            MyBind();
        }
    }
    public void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        M_CommonData storeMod = conBll.SelMyStore_Ex();
        DataTable dt = new DataTable();
        if (string.IsNullOrEmpty(KeyWord))
        {
            dt = bll.GetProductAll(NodeID, storeMod.GeneralID, QuickSouch, Pid);
        }
        else
        {
            if (DataConverter.CLng(KeyWord) > 0)
                dt = bll.ProductSearch(10, KeyWord, storeMod.GeneralID);
            else
                dt = bll.ProductSearch(0, KeyWord, storeMod.GeneralID);
        }
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;
            e.Row.Attributes.Add("ondblclick", "location='ShowProduct.aspx?menu=edit&ModelID=" + dr["ModelID"] + "&NodeID=" + dr["NodeID"] + "&id=" + dr["ID"] + "';");
        }
    }
    protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        M_Product product = new M_Product();
        M_Product productPre = new M_Product();
        int proID = DataConverter.CLng(e.CommandArgument);
        switch (e.CommandName.ToLower())
        {
            case "upmove":
                product = bll.GetproductByid(proID);
                productPre = bll.GetNearID(NodeID, product.OrderID, 1,mu.UserID);
                if (productPre.OrderID != 0)
                {
                    int CurrOrder = product.OrderID;
                    product.OrderID = productPre.OrderID;
                    productPre.OrderID = CurrOrder;
                    bll.UpdateOrder(product);
                    bll.UpdateOrder(productPre);
                }
                break;
            case "downmove":
                product = bll.GetproductByid(proID);
                productPre = bll.GetNearID(NodeID, product.OrderID, 0, mu.UserID);
                if (productPre != null)
                {
                    int CurrOrder = product.OrderID;
                    product.OrderID = productPre.OrderID;
                    productPre.OrderID = CurrOrder;
                    bll.UpdateOrder(product);
                    bll.UpdateOrder(productPre);
                }
                break;
            case "del1":
                bll.DeleteByID(proID, bll.GetproductByid(proID));
                break;
            default:
                break;
        }
        MyBind();
    }
    public string getNodeName()
    {
        M_Node nod = nodeBll.GetNodeXML(DataConverter.CLng(base.Request.QueryString["NodeID"]));
        return nod != null ? nod.NodeName : "";
    }
    public string formatnewstype(string type)
    {
        int newtype = DataConverter.CLng(type);
        string restring = "";
        if (newtype == 2)
        {
            restring = "<font color=red>特价</font>";
        }
        else if (newtype == 3)
        {
            restring = "<font color=blue>积分商品</font>";
        }
        if (!Eval("IsTrue","").Equals("1"))
        {
            restring = "<font color=#999999>待审核</font>";
        }
        if (!DataConverter.CBool(Eval("Recycler","")))
        {
            restring = "<font color=blue>正常</font>";
        }
        else
        {
            restring = "<font color=#999999>已删除</font>";
        }
        return restring;
    }
    public string GetPrice()
    {
        //return OrderHelper.GetPriceStr(Convert.ToDouble(Eval("LinPrice")), Eval("LinPrice_Json").ToString());
        return Convert.ToDouble(Eval("LinPrice")).ToString("f2");
    }
    public string forisnew(string type)
    {
        return type.Equals("1") ? "<span style='color:green;'>新</span>" : "&nbsp;&nbsp;";
    }
    public string forishot(string type)
    {
        return type.Equals("1") ? "<span style='color:red;'>热</span>" : "&nbsp;&nbsp;";
    }
    public string forisbest(string type)
    {
        return type.Equals("1") ? "<span style='color:blue;'>精</span>" : "&nbsp;&nbsp;";
    }
    public string formattype(string type)
    {
        return type.Equals("1") ? "<span style='color:blue;'>√</span>" : "<span style='color:red;'>×</span>";
    }
    public string getproimg()
    {
        return function.GetImgUrl(Eval("Thumbnails"));
    }
    protected void souchok_Click(object sender, EventArgs e)
    {
        string souchtype = souchtable.SelectedValue;
        KeyWord = souchkey.Text.Trim();
        if (!string.IsNullOrEmpty(KeyWord) && !string.IsNullOrEmpty(souchtype))
        {
            MyBind();
        }
    }
    //开始销售
    protected void Button1_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (!String.IsNullOrEmpty(ids))
        {
            bll.setproduct(1, ids);
        }
        function.WriteSuccessMsg("操作成功");
    }
    //热卖
    protected void Button2_Click(object sender, EventArgs e)
    {
        string CID = Request.Form["idchk"];
        if (!String.IsNullOrEmpty(CID))
        {
            bll.setproduct(2, CID);
        }
        function.WriteSuccessMsg("批量设置为热卖成功");
    }
    //精品
    protected void Button6_Click(object sender, EventArgs e)
    {
        string CID = Request.Form["idchk"];
        if (!String.IsNullOrEmpty(CID))
        {
            bll.setproduct(3, CID);
        }
        function.WriteSuccessMsg("批量设为精品成功");
    }
    //新品
    protected void Button5_Click(object sender, EventArgs e)
    {
        string CID = Request.Form["idchk"];
        if (!String.IsNullOrEmpty(CID))
        {
            bll.setproduct(4, CID);
        }
        function.WriteSuccessMsg("批量设为新品成功");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string CID = Request.Form["idchk"];
        if (!String.IsNullOrEmpty(CID))
        {
            bll.setproduct(13, CID);
        }
        function.WriteSuccessMsg("批量删除成功");
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        string CID = Request.Form["idchk"];
        if (!String.IsNullOrEmpty(CID))
        {
            bll.setproduct(6, CID);
        }
        function.WriteSuccessMsg("批量停止销售成功");
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        string CID = Request.Form["idchk"];
        if (!String.IsNullOrEmpty(CID))
        {
            bll.setproduct(7, CID);
        }
        function.WriteSuccessMsg("批量取消热卖成功");
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        string CID = Request.Form["idchk"];
        if (!String.IsNullOrEmpty(CID))
        {
            bll.setproduct(8, CID);
        }
        function.WriteSuccessMsg("批量取消精品成功");
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        string CID = Request.Form["idchk"];
        if (!String.IsNullOrEmpty(CID))
        {
            bll.setproduct(9, CID);
        }
        function.WriteSuccessMsg("批量取消新品成功");
    }
}


