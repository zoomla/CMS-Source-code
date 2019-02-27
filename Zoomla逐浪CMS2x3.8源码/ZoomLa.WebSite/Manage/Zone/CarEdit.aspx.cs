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

public partial class manage_Zone_CarEdit : Page
{
    Parking_BLL pl = new Parking_BLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        ZoomLa.Common.function.AccessRulo();
        B_Admin admin = new B_Admin();
        
        if (!IsPostBack)
        {
            P_CarList cl = pl.GetCar(int.Parse(Request.QueryString["id"].ToString()));
            txtCarName.Text = cl.P_car_name;
            txtCarMoney.Text = cl.P_car_money.ToString();
            txtCarLog.Text = cl.P_car_img_logo;
            txtCarImg.Text = cl.P_car_img;
            txtCarContext.Text = cl.P_car_content;
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "User/UserManage.aspx'>会员管理</a></li><li><a href='ZoneManage.aspx'>会员空间管理</a></li><li><a href='CarManage.aspx'>车辆列表</a></li><li class='active'>修改车辆信息<a href='CarRuleManage.aspx'>[抢车位规则管理]</a></li>");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        P_CarList cl = pl.GetCar(int.Parse(Request.QueryString["id"].ToString()));
        cl.P_car_img_logo = txtCarLog.Text;
        cl.P_car_img = txtCarImg.Text;
        cl.P_car_money = int.Parse(txtCarMoney.Text);
        cl.P_car_name = txtCarName.Text;
        cl.P_car_content = txtCarContext.Text;
        pl.UpdateCarList(cl);
        Response.Redirect("CarManage.aspx");
    }
}
