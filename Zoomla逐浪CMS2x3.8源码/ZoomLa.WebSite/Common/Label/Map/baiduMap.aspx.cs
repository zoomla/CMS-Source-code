using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;

public partial class User_UserShop_baiduMap : System.Web.UI.Page
{
    //灰色不显示地图,坐标不正确导致 var point = new BMap.Point(116.404, 39.915);
    public string Field { get { return Request.QueryString["Field"]; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
         
        }
    }
    //添加标志时是由组件完成事件的处理
    //重加载标志时,则是循环输出后绑定事件
}