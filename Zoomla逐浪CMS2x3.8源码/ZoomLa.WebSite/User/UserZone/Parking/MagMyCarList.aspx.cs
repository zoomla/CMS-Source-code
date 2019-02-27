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
using System.Data.SqlClient;
using System.Collections.Generic;
using ZoomLa.Sns;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Model;

public partial class MagMyCarList : Page
{
    Parking_BLL pl = new Parking_BLL ();
    B_User ubll = new B_User();
    int currentUser = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = ubll.GetLogin().UserID;
        ubll.CheckIsLogin();
        if (!IsPostBack)
        {
            M_UserInfo uinfo = ubll.GetUserByUserID(currentUser);
            ViewState["Purse"] = uinfo.DummyPurse;
            Bind();
        }
    }

    //分页绑定数据
    private void Bind()
    {
        
        int CPage;
        int temppage;
        List<P_CarList> list = pl.GetCarList(); ;
        if (Request.Form["DropDownList1"] != null)
        {
            temppage = Convert.ToInt32(Request.Form["DropDownList1"]);
        }
        else
        {
            temppage = Convert.ToInt32(Request.QueryString["CurrentPage"]);
        }
        CPage = temppage;
        if (CPage <= 0)
        {
            CPage = 1;
        }
        PagedDataSource cc = new PagedDataSource();
        cc.DataSource = list;
        cc.AllowPaging = true;
        cc.PageSize = 8;
        cc.CurrentPageIndex = CPage - 1;
        DataList1.DataSource = cc;
        DataList1.DataBind();

        Allnum.Text = list.Count.ToString();
        int thispagenull = cc.PageCount;//总页数
        int CurrentPage = cc.CurrentPageIndex;
        int nextpagenum = CPage - 1;//上一页
        int downpagenum = CPage + 1;//下一页
        int Endpagenum = thispagenull;
        if (thispagenull <= CPage)
        {
            downpagenum = thispagenull;
            Downpage.Enabled = false;
        }
        else
        {
            Downpage.Enabled = true;
        }
        if (nextpagenum <= 0)
        {
            nextpagenum = 0;
            Nextpage.Enabled = false;
        }
        else
        {
            Nextpage.Enabled = true;
        }

        Toppage.Text = "<a href=MagMyCarList.aspx?Currentpage=0>首页</a>";
        Nextpage.Text = "<a href=MagMyCarList.aspx?Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
        Downpage.Text = "<a href=MagMyCarList.aspx?Currentpage=" + downpagenum.ToString() + ">下一页</a>";
        Endpage.Text = "<a href=MagMyCarList.aspx?Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
        Nowpage.Text = CPage.ToString();
        PageSize.Text = thispagenull.ToString();
        pagess.Text = cc.PageSize.ToString();
        for (int i = 1; i <= thispagenull; i++)
        {
            DropDownList1.Items.Add(i.ToString());
        }

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("MagMyCarList.aspx?Currentpage=" + this.DropDownList1.Text);
    }

    //购买
    protected void DataList1_EditCommand(object source, DataListCommandEventArgs e)
    {
        int carid=Convert.ToInt32(DataList1.DataKeys[e.Item.ItemIndex].ToString());
        //检查用户是否已经购买该款车辆
        if (pl.CheckCar(carid,currentUser ))
        {
            P_CarList carlist = pl.GetCar(Convert.ToInt32(DataList1.DataKeys[e.Item.ItemIndex].ToString()));
            if (Convert.ToDouble(ViewState["Purse"].ToString()) >= Convert.ToDouble(carlist.P_car_money))
            {
                if (true)
                {
                    //添加用户购买车辆
                    ZL_Sns_MyCar mcar = new ZL_Sns_MyCar();
                    mcar.P_uid = currentUser;
                    mcar.Pid = carlist.Pid;
                    mcar.P_last_user = null;
                    mcar.P_last_uid = 0;

                    pl.AddMyCar(mcar);

                    //添加用户购买车辆日志信息

                    ZL_Sns_CarLog cl = new ZL_Sns_CarLog();
                    cl.P_content = "花费了" + carlist.P_car_money + "元钱购买了一辆" + carlist.P_car_name;
                    cl.P_uid = currentUser;
                    pl.AddCarLog(cl);
                    Response.Redirect("Default.aspx");
                }
            }
        }
    }
}

