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

    //��ҳ������
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
        int thispagenull = cc.PageCount;//��ҳ��
        int CurrentPage = cc.CurrentPageIndex;
        int nextpagenum = CPage - 1;//��һҳ
        int downpagenum = CPage + 1;//��һҳ
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

        Toppage.Text = "<a href=MagMyCarList.aspx?Currentpage=0>��ҳ</a>";
        Nextpage.Text = "<a href=MagMyCarList.aspx?Currentpage=" + nextpagenum.ToString() + ">��һҳ</a>";
        Downpage.Text = "<a href=MagMyCarList.aspx?Currentpage=" + downpagenum.ToString() + ">��һҳ</a>";
        Endpage.Text = "<a href=MagMyCarList.aspx?Currentpage=" + Endpagenum.ToString() + ">βҳ</a>";
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

    //����
    protected void DataList1_EditCommand(object source, DataListCommandEventArgs e)
    {
        int carid=Convert.ToInt32(DataList1.DataKeys[e.Item.ItemIndex].ToString());
        //����û��Ƿ��Ѿ�����ÿ��
        if (pl.CheckCar(carid,currentUser ))
        {
            P_CarList carlist = pl.GetCar(Convert.ToInt32(DataList1.DataKeys[e.Item.ItemIndex].ToString()));
            if (Convert.ToDouble(ViewState["Purse"].ToString()) >= Convert.ToDouble(carlist.P_car_money))
            {
                if (true)
                {
                    //����û�������
                    ZL_Sns_MyCar mcar = new ZL_Sns_MyCar();
                    mcar.P_uid = currentUser;
                    mcar.Pid = carlist.Pid;
                    mcar.P_last_user = null;
                    mcar.P_last_uid = 0;

                    pl.AddMyCar(mcar);

                    //����û���������־��Ϣ

                    ZL_Sns_CarLog cl = new ZL_Sns_CarLog();
                    cl.P_content = "������" + carlist.P_car_money + "ԪǮ������һ��" + carlist.P_car_name;
                    cl.P_uid = currentUser;
                    pl.AddCarLog(cl);
                    Response.Redirect("Default.aspx");
                }
            }
        }
    }
}

