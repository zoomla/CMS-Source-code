using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
/*
 * 用于逐浪快发,中转
 */ 
namespace ZoomLaCMS.Common
{
    public partial class Hub : System.Web.UI.Page
    {
        RegexHelper regHelper = new RegexHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request["action"];
            string value = Request["value"];
            switch (action)
            {
                case "edit"://修改内容或商品
                    int gid = regHelper.GetGidByUrl(value);
                    if (value.Contains("/item/"))
                    {
                        Response.Redirect(CustomerPageAction.customPath2 + "Content/EditContent.aspx?GeneralID=" + gid);
                    }
                    else if (value.Contains("/shop/"))
                    {
                        Response.Redirect(CustomerPageAction.customPath2 + "Shop/AddProduct.aspx?menu=edit&id=" + gid);
                    }
                    else { function.WriteErrMsg(value + "不提供修改"); }
                    break;
            }
        }
    }
}