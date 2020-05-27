using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using System.IO;
using System.Text;

namespace ZoomLaCMS.App.APPTip
{
    public partial class Design : System.Web.UI.Page
    {
        public B_App_AppTlp tlpBll = new B_App_AppTlp();
        B_User buser = new B_User();
        //为防止用户任意修改模板,限定为必须以 /App/AppTlp/开头
        public string VPath
        {
            get
            {
                ///App/AppTlp/
                string _vpath = (Request.QueryString["vpath"] ?? "").ToLower();
                _vpath = SafeSC.PathDeal(_vpath);
                if (string.IsNullOrEmpty(_vpath)) { function.WriteErrMsg("未指定模板"); }
                if (!_vpath.StartsWith("/app/apptlp/")) { function.WriteErrMsg("根路径不正确,只允许修改APP模板"); }
                return _vpath;
            }
        }
        public int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            buser.CheckIsLogin();
            if (!IsPostBack)
            {
                if (Mid > 0)
                {
                    M_UserInfo mu = buser.GetLogin();
                    M_APP_APPTlp tlpMod = tlpBll.SelReturnModel(Mid);
                    if (tlpMod.UserID != mu.UserID) { function.WriteErrMsg("你无权编辑该模板!"); }
                }
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            M_APP_APPTlp tlpMod = new M_APP_APPTlp();
            string saveurl = "", fname = "";
            //--------------------------------
            string head = HttpUtility.UrlDecode(Head_Hid.Value);
            string html = AllHtml_Hid.Value;
            int start = html.IndexOf(Call.Boundary) + Call.Boundary.Length;
            int len = html.Length - start;
            html = "<body>" + html.Substring(start, len);//处理iframe中标签错位Bug
            head = head.Replace(Call.Boundary, html);
            if (Mid > 0)//更新
            {
                tlpMod = tlpBll.SelReturnModel(Mid);
                tlpMod.Alias = TlpName_T.Text;
                tlpBll.UpdateByID(tlpMod);
                SafeSC.WriteFile(tlpMod.TlpUrl, head);
            }
            else
            {
                saveurl = "/App/AppTlp/Users/" + mu.UserName + mu.UserID + "/";//存储模板路径
                fname = DateTime.Now.ToString("yyyyMMddhhmm") + function.GetRandomString(4) + Path.GetExtension(VPath);
                tlpMod.CDate = DateTime.Now;
                tlpMod.Alias = TlpName_T.Text;
                tlpMod.TlpUrl = saveurl + fname;
                tlpMod.UserID = mu.UserID;
                tlpBll.Insert(tlpMod);
                SafeSC.WriteFile(saveurl + fname, head);
            }
            function.WriteSuccessMsg("模板保存成功", "/App/AppTlp/MyTlpList.aspx");
        }
    }
}