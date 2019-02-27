namespace Zoomla.Website.manage.Shop
{
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
    using ZoomLa.Components;

    public partial class Stock : CustomerPageAction
    {
        protected B_Model bmode = new B_Model();
        protected B_ShowField bshow = new B_ShowField();
        protected B_ModelField bfield = new B_ModelField();
        protected B_Stock bll = new B_Stock();
        protected B_Product pll = new B_Product();
        protected M_Product proMod = new M_Product();
        protected int NodeID;
        protected int ModelID;

        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "StockManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                string menu = Request.QueryString["menu"];
                string danju;
                RangeValidator1.MinimumValue = Convert.ToString(Int32.MinValue);
                RangeValidator1.MaximumValue = Convert.ToString(Int32.MaxValue);
                DataTable Prolist = pll.GetProductAll(0);
                DataTable newprolist = Prolist.DefaultView.ToTable(false, "ID", "Proname");

                Hashtable ar = new Hashtable();
                foreach (DataRow dr in newprolist.Rows)
                {
                    ar.Add(dr[0].ToString(), dr[1].ToString());
                }
                this.productid.DataSource = ar;
                this.productid.DataValueField = "key";
                this.productid.DataTextField = "value";
                this.productid.DataBind();

                string adminname = badmin.GetAdminLogin().AdminName; 
                calendars.Text = Convert.ToString(DateTime.Now);
                if (!this.Page.IsPostBack)
                {
                    danju = Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Millisecond);
                    danjuhaobak.Value = danju;
                    danjuhao.Text = "RK" + danju;
                    danjuhaos.Value = "RK" + danju;
                    adduser.Text = adminname;
                }
                if (menu == "edit")
                {
                    int id = DataConverter.CLng(Request.QueryString["id"]);
                    M_Stock stocktable = bll.GetStockByid(id);
                    stocktyle.SelectedIndex = stocktable.stocktype;
                    danjuhao.Text = stocktable.danju.ToString();
                    this.danjuhaos.Value = stocktable.danju.ToString();
                    this.danjuhaobak.Value = stocktable.danju.ToString().Replace("CK", "").Replace("RK", "");
                    this.adduser.Text = stocktable.adduser.ToString();
                    this.stockcontent.Text = stocktable.content.ToString();
                    this.stockid.Value = stocktable.id.ToString();
                    this.calendars.Text = stocktable.addtime.ToString();
                    this.productid.Text = stocktable.proid.ToString();
                    this.Pronum.Text = stocktable.Pronum.ToString();
                    this.Pronum.Enabled = false;
                    Button1.Text = "修改";
                    Label1.Text = "修改入库出库单";
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li><li><a href='StockManage.aspx'>库存管理</a></li><li class='active'><a href='" + Request.RawUrl + "'>添加入库出库记录</a></li>");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                M_Stock CData = new M_Stock();
                CData.id = DataConverter.CLng(stockid.Value);
                CData.proid = DataConverter.CLng(productid.SelectedValue.ToString());
                string temptype = stocktyle.SelectedValue.ToString();
                if (temptype == "RK")
                {
                    CData.stocktype = 0;
                }
                else
                {
                    CData.stocktype = 1;
                }
                CData.proname = pll.GetproductByid(CData.proid).Proname;
                CData.danju = stocktyle.Text + this.danjuhaobak.Value;
                // CData.adduser = this.adduser.Text;  //adminname
                CData.adduser = adduser.Text.Trim();
                CData.addtime = DataConverter.CDate(calendars.Text);
                CData.content = stockcontent.Text;
                CData.Pronum = DataConverter.CLng(Pronum.Text);
                if (Button1.Text == "修改")
                {
                    bll.EditStock(CData);
                    function.WriteSuccessMsg("单据修改成功!请继续操作", "StockManage.aspx");
                }
                else
                {
                    bll.AddStock(CData);
                    function.WriteSuccessMsg("单据添加成功!请继续添加");
                }
            }
        }
    }
}