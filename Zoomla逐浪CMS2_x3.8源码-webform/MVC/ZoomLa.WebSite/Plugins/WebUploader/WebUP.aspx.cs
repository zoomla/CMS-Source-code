namespace ZoomLaCMS.Plugins.WebUploader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Newtonsoft.Json;
    using System.Net;
    using System.IO;
    using ZoomLa.Common;
    using ZoomLa.Safe;
    using ZoomLa.BLL;
    public partial class WebUP : System.Web.UI.Page
    {
        // 是否开启单片上传 True:开启
        public bool IsChunk { get { return DataConverter.CLng(Request["chunk"]) == 1; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (new B_User().CheckLogin() || new B_Admin().CheckLogin())
            {

            }
            else
            {
                function.WriteErrMsg("该页面必须登录后才能访问");
            }
        }
    }
}