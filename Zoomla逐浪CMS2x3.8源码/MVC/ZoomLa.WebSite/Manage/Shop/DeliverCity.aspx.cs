using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
namespace ZoomLaCMS.Manage.Shop
{
    public partial class DeliverCity : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();

            if (!this.IsPostBack)
            {
                string city = "辽宁、吉林、黑龙江、河北、山西、陕西、甘肃、青海、山东、安徽、江苏、浙江、河南、湖北、湖南、江西、台湾、福建、云南、海南、四川、贵州、广东、内蒙古、新疆、广西、西藏、宁夏、北京、上海、天津、重庆、香港、澳门";
                string[] arrcity = city.Split(new char[] { '、' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < arrcity.Length; i++)
                {
                    tx_city.Items.Add(arrcity[i].Trim().ToString());
                }
                Call.SetBreadCrumb(Master, "<li>商城管理</li><li><a href='DeliverType.aspx'>送货方式管理</a></li><li><a href='AddDeliverType.aspx'>默认地区</a></li>");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
        }
    }
}