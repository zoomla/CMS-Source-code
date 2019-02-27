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
using System.Collections.Generic;
using ZoomLa.Components;


public partial class User_UserShop_MyStoreSet : System.Web.UI.Page
{
    #region 业务对象
    B_User bubll = new B_User();
    B_UserStoreTable ustbll = new B_UserStoreTable();
    protected B_ModelField bfield = new B_ModelField();
    #endregion

    private int uid
    {
        get
        {
            if (HttpContext.Current.Request.Cookies["UserState"]["UserID"] != null)
                return int.Parse(HttpContext.Current.Request.Cookies["UserState"]["UserID"].ToString());
            else
                return 0;
        }
        set
        {
            uid = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["action"] = Request.QueryString["action"];
        if (!IsPostBack)
        {
            GetInit();
        }
    }

    #region 页面方法
    //初始化
    private void GetInit()
    {
        M_UserStoreTable ust = ustbll.GetStoreTableByUserID(uid);
       


                this.Nametxt.Text = ust.StoreName;
                //绑定商铺类型
                List<M_UserStoreTypeTable> list = ustbll.GetStoreType();
                this.DropDownList1.DataSource = list;
                this.DropDownList1.DataTextField = "TypeName";
                this.DropDownList1.DataValueField = "TypeName";
                this.DropDownList1.DataBind();
                DropDownList1.SelectedValue = ust.StoreType;


                //绑定省市
                //List<province> listp = ustbll.readProvince(Server.MapPath(@"~/User/UserShop/SystemData.xml"));
                //this.DropDownList2.DataSource = listp;
                //DropDownList2.DataTextField = "name";
                //DropDownList2.DataValueField = "code";
                //DropDownList2.DataBind();
                //DropDownList2.SelectedValue = ust.StoreProvince;

                //List<Pcity> listc = ustbll.ReadCity(Server.MapPath(@"~/User/UserShop/SystemData.xml"), ust.StoreProvince);
                //DropDownList3.DataSource = listc;
                //DropDownList3.DataTextField = "name";
                //DropDownList3.DataValueField = "code";
                //DropDownList3.DataBind();
                //DropDownList3.SelectedValue = ust.StoreCity;

                this.TEXTAREA1.Value = ust.StoreContent;

                //string tophtml = "<table width=\"100%\"><tr><td width=\"100px\"></td><td width = \"80%\"></td></tr>";
                //string endhtml = "</table>";
                //this.ModelHtml.Text = tophtml + this.mll.GetUpdateHtmlUser(this.ModelID, NodeID, dtContent) + endhtml;

            
        
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string pro = DropDownList2.SelectedValue;
        if (pro != "")
        {
            this.DropDownList3.Visible = true;
            //List<Pcity> listc = ustbll.ReadCity(Server.MapPath(@"~/User/UserShop/SystemData.xml"), pro);
            //DropDownList3.DataSource = listc;
            //DropDownList3.DataTextField = "name";
            //DropDownList3.DataValueField = "code";
            //DropDownList3.DataBind();
        }
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            M_UserStoreTable ust = ustbll.GetStoreTableByUserID(uid);

                ust.StoreName = this.Nametxt.Text;
                ust.UserID = uid;
                ust.StoreContent = this.TEXTAREA1.Value;
                ust.StoreType = this.DropDownList1.Text;
                ust.StoreProvince = this.DropDownList2.Text;
                ust.StoreCity = this.DropDownList3.Text;
                ustbll.UpdateStoreTable(ust);
                Response.Redirect("MyStoreSet.aspx");
        }
        catch (Exception ee)
        {
            function.WriteErrMsg(ee.Message);
        }

    }
    #endregion
}
