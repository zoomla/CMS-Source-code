using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class ProductController : ZLCtrl
    {
        public void Index()
        {
            Response.Redirect("/User/UserShop/ProductList");
        }
        public void ProductManage() { Response.Redirect("/User/UserShop/ProductList"); }
        public void MyProduct()
        {
            //PageSetting setting = new PageSetting();
            //return View(setting);
            Response.Redirect("/User/UserShop/ProductList");
        }
        public void AddProduct() { Response.Redirect("/User/UserShop/ProductList");}
    }
}
