namespace ZoomLaCMS.Plugins.WebUploader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.Safe;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Helper;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using ZoomLa.Model;

    public partial class RemoteImg : System.Web.UI.Page
    {
        WebClient wb = new WebClient();
        HttpHelper httpHelper = new HttpHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_Admin.CheckLogByAU())
            {
                function.WriteErrMsg("无权访问该页面");
            }
        }
        protected void GetPic_Btn_Click(object sender, EventArgs e)
        {
            //http://h.hiphotos.baidu.com/zhidao/wh%3D450%2C600/sign=3dc4538262d0f703e6e79dd83dca7d0b/7a899e510fb30f24f570e996c895d143ac4b03b8.jpg
            if (string.IsNullOrEmpty(Remote_Url.Text)) return;
            string vdir = SiteConfig.SiteOption.UploadDir;
            M_Node nodeMod = new B_Node().GetNodeXML(DataConverter.CLng(NodeID_Hid.Value));
            vdir += nodeMod.NodeDir + "/" + DateTime.Now.ToString("yyyy/MM/");
            //-------------------
            string[] imgurl = Remote_Url.Text.Split('\r');
            string result = "";
            for (int i = 0; i < imgurl.Length; i++)
            {
                string imgname = GetFNameFromUrl(imgurl[i]);
                if (!SafeC.IsImage(imgname)) { continue; }
                string url = imgurl[i].ToLower().Replace(" ", "");
                if (string.IsNullOrEmpty(url) || (!url.Contains("http") && !url.Contains("https"))) { continue; }//function.WriteErrMsg("错误:" + url); 
                string vpath = vdir + RemoveChar(imgname);
                try { httpHelper.DownFile(url, vpath); }
                catch (Exception) { }//function.WriteErrMsg("抓取失败,原因:" + ex.Message);
                result += vpath + "|";
            }
            result = result.TrimEnd('|').Replace("//", "/");
            Remote_Url.Text = "";
            function.Script(this, "AddAttach('" + result + "');");
        }
        public string GetFNameFromUrl(string url)
        {
            int slen = url.LastIndexOf("/") + 1;
            return url.Substring(slen, (url.Length - slen));
        }
        public string RemoveChar(string str, int flag = 1)//移除不能用于创建本地文件的字符
        {
            string[] DirName = new string[] { "\\", "/", "?", ":", "：", "*", "<", ">", "|", "\"", "'", "&" };
            foreach (string s in DirName)
            {
                str = str.Replace(s, "");
            }
            return str;
        }
    }
}