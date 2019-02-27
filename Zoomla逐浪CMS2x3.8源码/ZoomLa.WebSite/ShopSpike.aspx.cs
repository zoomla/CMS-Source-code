using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Xml;
using System.Text;
using System.Data;

public partial class ShopSpike : System.Web.UI.Page
{
    protected B_User bubll = new B_User();
    protected B_Product bpbll = new B_Product();
    protected B_Cart ACl = new B_Cart();
    protected B_CartPro Cll = new B_CartPro();
    protected string Unit;
    private int PID
    {
        get { return DataConverter.CLng(ViewState["pid"]); }
        set { ViewState["pid"] = value; }
    }
    private int Num
    {
        get { return DataConverter.CLng(ViewState["num"]); }
        set { ViewState["num"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    //秒杀
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}
