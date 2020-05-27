namespace ZoomLaCMS.Plugins.cropper
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL.Helper;
    using ZoomLa.Common;
    public partial class se_bk : System.Web.UI.Page
    {
        public string Url { get { return Request.QueryString["url"]; } }
        private string savePath = "/UploadFiles/User/cutpic/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        //{"x":80.00000000000006,"y":76.50000000000001,"width":480,"height":855.0000000000001,"rotate":0,"scaleX":1,"scaleY":1}
        //其提交的数值是经过计算后还原的实际值,所以不需要再计算宽高比率.
        protected void Cut_Btn_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Server.MapPath(savePath))) { Directory.CreateDirectory(Server.MapPath(savePath)); }
            //图片是50%压缩的,所以宽高都需要处理,然后再执行裁剪方法
            JObject jobj = JsonConvert.DeserializeObject<JObject>(Cut_Hid.Value);
            string warning = "";
            string result = ImageDealLib.imgcrop(Url, savePath + "123.jpg", (int)jobj["x"], (int)jobj["y"], (int)jobj["width"], (int)jobj["height"], ImageDealLib.FileCache.Save, out warning);
            function.Script(this, "top.diy_bk.setbk('" + result + "');");
        }
    }
}