using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Model.Design;
using System.Web;
using ZoomLa.Model;

namespace ZoomLa.Ajax.WebAPI
{
    public class DesignController : ApiController
    {
        B_Design_Page pageBll = new B_Design_Page();
        [HttpPost]
        public string Save(M_Design_Page model)
        {
            //string token=Request.Headers.GetValues("token").First().ToString();
            //M_UserInfo mu = ZLCache.GetUser(token);
            //if (mu.IsNull) { return "用户未登录"; }
            //if (string.IsNullOrEmpty(model.guid))
            //{
            //    model.guid = System.Guid.NewGuid().ToString();
            //    model.comp = SafeSC.WriteFile(model.SaveDir + model.guid + ".tlp", model.comp);
            //    model.UserID = mu.UserID;
            //    model.UserName = mu.UserName;
            //    pageBll.Insert(model);
            //}
            //else
            //{
            //    M_Design_Page pageMod = pageBll.SelModelByGuid(model.guid);
            //    pageMod.page = model.page;
            //    pageMod.labelArr = model.labelArr;
            //    pageMod.UPDate = DateTime.Now;
            //    pageMod.comp = SafeSC.WriteFile(model.SaveDir + model.guid + ".tlp", model.comp);
            //    pageBll.UpdateByID(pageMod);
            //}
            //return model.guid;
            return "";
        }
    }
}
