using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;

namespace ZoomLaCMS.Controllers
{
    public class DAIController : Controller
    {
        //
        // GET: /DAI/
        public ActionResult Default()
        {
            return View();
        }
        public ActionResult Cammera()
        {
            return View();
        }
        public void DownImg()
        {
            string base64 = Request.Form["img_hid"];
            if (string.IsNullOrEmpty(base64)) { Response.Write("数据为空,无法生成图片"); Response.End(); return; }
            if (base64.Contains(",")) { base64 = base64.Split(',')[1]; }
            ImgHelper imgHelper = new ImgHelper();
            string vpath = "/UploadFiles/dai/" + function.GetRandomString(6) + ".jpg";
            imgHelper.Base64ToImg(vpath, base64);
            SafeSC.DownFile(vpath, "glassed.jpg");
        }
        public ActionResult ReBack()
        {
            string data = "[{&quot;ID&quot;:12,&quot;price&quot;:200.0,&quot;sptp&quot;:&quot;res/20120608glass_12.png&quot;,&quot;spm&quot;:&quot;蝙蝠侠_BM97004 B1(透明黑)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:11,&quot;price&quot;:300.0,&quot;sptp&quot;:&quot;res/20120608glass_11.png&quot;,&quot;spm&quot;:&quot;蝙蝠侠_BM97004 B6(绅士银)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:10,&quot;price&quot;:600.0,&quot;sptp&quot;:&quot;res/20120608glass_10.png&quot;,&quot;spm&quot;:&quot;蝙蝠侠_BM95002 C9D(绅士黑)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:9,&quot;price&quot;:300.0,&quot;sptp&quot;:&quot;res/20120608glass_9.png&quot;,&quot;spm&quot;:&quot;沙漠之鹰_R5137 C16(绅士黑)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:8,&quot;price&quot;:120.0,&quot;sptp&quot;:&quot;res/20120608glass_8.png&quot;,&quot;spm&quot;:&quot;沙漠之鹰_R5152 CCG(绅士银)&quot;,&quot;sptp2&quot;:&quot;/Plugins/tryin/res/glass_7_2.png&quot;},{&quot;ID&quot;:7,&quot;price&quot;:150.0,&quot;sptp&quot;:&quot;res/20120608glass_7.png&quot;,&quot;spm&quot;:&quot;沙漠之鹰_R5152 C16(绅士黑)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:6,&quot;price&quot;:200.0,&quot;sptp&quot;:&quot;res/20120608glass_6.png&quot;,&quot;spm&quot;:&quot;毕加索_55-2051 C11(荧光红)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:5,&quot;price&quot;:100.0,&quot;sptp&quot;:&quot;res/20120608glass_5.png&quot;,&quot;spm&quot;:&quot;毕加索_55-2068 C11(荧光红)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:4,&quot;price&quot;:120.0,&quot;sptp&quot;:&quot;res/20120608glass_4.png&quot;,&quot;spm&quot;:&quot;毕加索_55-2001 C6(绅士黑)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:3,&quot;price&quot;:150.0,&quot;sptp&quot;:&quot;res/20120608glass_2.png&quot;,&quot;spm&quot;:&quot;佐腾樱花_ZTYH-010(蓝色)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:2,&quot;price&quot;:200.0,&quot;sptp&quot;:&quot;res/20120608glass_3.png&quot;,&quot;spm&quot;:&quot;毕加索_55-2062 C6(绅士黑)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:1,&quot;price&quot;:100.0,&quot;sptp&quot;:&quot;res/20120608glass_1.png&quot;,&quot;spm&quot;:&quot;佐腾樱花_ZTYH-001(豹纹色)&quot;,&quot;sptp2&quot;:&quot;&quot;}]";
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(data);
            return View(dt);
        }
    }
}
