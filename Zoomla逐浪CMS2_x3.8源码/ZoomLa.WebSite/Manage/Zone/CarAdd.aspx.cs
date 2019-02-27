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
using ZoomLa.Sns;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class manage_Zone_CarAdd : CustomerPageAction 
{
    Parking_BLL pl = new Parking_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin admin = new B_Admin();
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>会员管理</a></li><li><a href='ZoneManage.aspx'>会员空间管理</a></li><li><a href='CarManage.aspx'>车辆列表</a></li><li class='active'>添加车辆<a href='CarRuleManage.aspx'>[抢车位规则管理]</a></li>");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        P_CarList cl = new P_CarList();
        cl.P_car_img_logo = "~/"+txtCarLog.Text;
        cl.P_car_img = "~/" + txtCarImg.Text;
        cl.P_car_money = int.Parse(txtCarMoney.Text);
        cl.P_car_name = txtCarName.Text;
        cl.P_car_content = txtCarContext.Text;
        cl.P_car_check = 1;
        cl.P_car_num = 0;
        cl.P_car_surplus = 0;
        cl.P_car_old = 0;
        pl.AddCarList(cl);
        Response.Redirect("CarManage.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("CarManage.aspx");
    }
}
