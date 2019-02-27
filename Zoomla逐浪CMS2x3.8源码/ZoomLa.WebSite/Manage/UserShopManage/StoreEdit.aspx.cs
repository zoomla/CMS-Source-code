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
using ZoomLa.Common;
using ZoomLa.Web;
using ZoomLa.BLL;
using System.Collections.Generic;
using ZoomLa.Model;

public partial class manage_UserShopManage_StoreEdit : CustomerPageAction
{
    #region 业务对象
    B_UserStoreTable ustbll = new B_UserStoreTable();
    B_ModelField mfbll = new B_ModelField();
    B_User bubll = new B_User();
    B_Model mbll = new B_Model();
    B_Content cbll = new B_Content();
    B_Node bn = new B_Node();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        ZoomLa.Common.function.AccessRulo();
        B_Admin badmin = new B_Admin();
        
        if (!B_ARoleAuth.Check(ZLEnum.Auth.store, "StoreManage"))
        {
            function.WriteErrMsg("没有权限进行此项操作");
        }
        ShowGrid();
        M_CommonData mc = cbll.GetCommonData(DataConverter.CLng(Request.QueryString["Id"]));
        int i = mc.ModelID;
        DataTable UserData = cbll.GetContentByItems(mc.TableName, Convert.ToInt32(Request.QueryString["Id"]));
        if (UserData.Rows.Count > 0)
        {
            DetailsView1.DataSource = UserData.DefaultView;
            DetailsView1.DataBind();
        }
        if (!IsPostBack)
        {
            ViewState["id"] = Request.QueryString["id"];
            mc = cbll.GetCommonData(DataConverter.CLng(Request.QueryString["Id"]));
            M_Node nnn = bn.GetNodeXML(DataConverter.CLng(UserData.Rows[0]["NodeID"]));
            mc.Hits = mc.Hits + 1;
            cbll.Update(mc);          
            GetInit();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../Shop/ProductManage.aspx'>商城管理</a></li><li><a href='StoreManage.aspx'>店铺管理</a></li><li class='active'>查看店铺信息</li>");
    }
    #region 页面方法

    private int sid
    {
        get
        {
            if (ViewState["id"] != null)
            {
                return int.Parse(ViewState["id"].ToString());
            }
            else
            {
                return 0;
            }
        }
        set
        {
            sid = value;
        }
    }
    //初始化
    private void GetInit()
    {
        M_CommonData dtinfo = cbll.GetCommonData(sid);
        DataTable dt = cbll.GetContent(sid);
        this.Namelabel.Text = dtinfo.Title;
        this.Label1.Text = dtinfo.Inputer;
        string tablename = dtinfo.TableName;
        B_ModelField mfll = new B_ModelField();
        SafeSC.CheckDataEx(tablename);
        DataTable tableinfo = mfll.SelectTableName("ZL_Model", "tablename = '" + tablename + "'");
        B_Model mll = new B_Model();
        if (tableinfo.Rows.Count > 0)
        {
            this.Label6.Text = mbll.GetModelById(DataConverter.CLng(dt.Rows[0]["StoreModelID"].ToString())).ModelName;           
        }
        else {
            this.Label6.Text = "查询错误!请核实此模型是否存在";
        }
        //string tophtml = "<tr><td width=\"20%\"></td><td width = \"80%\"></td>";
        //string endhtml = "</tr>";     
        //this.ModelHtml.Text = tophtml + this.mfbll.GetUpdateHtmlUser(int.Parse(dt.Rows[0]["StoreModelID"].ToString()), 0, dt) + endhtml;
        //this.ModelHtml.Text = this.mfbll.GetUpdateHtmlUser(int.Parse(dt.Rows[0]["StoreModelID"].ToString()), 0, dt);
        
    }
    #endregion

    private DataTable ShowGrid()
    {
        M_CommonData mc = cbll.GetCommonData(DataConverter.CLng(Request.QueryString["Id"]));
        int i = mc.ModelID;
        DataTable dt = this.mfbll.GetModelFieldList(DataConverter.CLng(i));
        DetailsView1.AutoGenerateRows = false;
        DataControlFieldCollection dcfc = DetailsView1.Fields;
        dcfc.Clear();
        foreach (DataRow dr in dt.Rows)
        {
            BoundField bf = new BoundField();
            bf.HeaderText = dr["FieldAlias"].ToString();
            bf.DataField = dr["FieldName"].ToString();
            bf.HeaderStyle.Width = Unit.Percentage(15);
            bf.HeaderStyle.CssClass = "tdbgleft";
            bf.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bf.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            bf.HtmlEncode = false;
            dcfc.Add(bf);
        }
        return dt;
    }
}
