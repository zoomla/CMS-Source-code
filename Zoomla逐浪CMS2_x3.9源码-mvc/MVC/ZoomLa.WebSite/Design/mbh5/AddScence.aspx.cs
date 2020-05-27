using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Design;


namespace ZoomLaCMS.Design.mbh5
{
    public partial class AddScence : System.Web.UI.Page
    {

        B_User buser = new B_User();
        B_Design_Scence pageBll = new B_Design_Scence();
        B_Design_Tlp tlpBll = new B_Design_Tlp();
        B_Design_Scence scenceBll = new B_Design_Scence();
        public string Device { get { return Request.QueryString["device"] ?? "mb"; } }
        public string Guid { get { return Request.QueryString["id"] ?? ""; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Device.Equals("pc"))
                {
                    function.Script(this, "showinpc();");
                }
                MyBind();
            }
        }

        private void MyBind()
        {
            if (!string.IsNullOrEmpty(Guid))
            {
                M_Design_Page pageMod = pageBll.SelModelByGuid(Guid);
                M_UserInfo mu = buser.GetLogin();
                if (pageMod.UserID != mu.UserID) { function.WriteErrMsg("你无权修改该场景"); }
                Title_T.Text = pageMod.Title;
                SFile_Up.FileUrl = pageMod.PreviewImg;
                Title_L.Text = "修改场景";
                Add_B.Text = "保存修改";
                function.Script(this, "editscence();");
            }
        }

        protected void Add_B_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            M_Design_Page pageMod = new M_Design_Page();
            if (!string.IsNullOrEmpty(Guid))
            {
                pageMod = pageBll.SelModelByGuid(Guid);
                pageMod.Title = Title_T.Text;
                SFile_Up.SaveUrl = SiteConfig.SiteOption.UploadDir + "User/" + mu.UserName + mu.UserID + "/";
                pageMod.PreviewImg = SFile_Up.SaveFile();
                pageBll.UpdateByID(pageMod);
                Response.Redirect("/design/user/default2.aspx");
            }
            else
            {
                pageMod.guid = System.Guid.NewGuid().ToString();
                pageMod.Title = Title_T.Text;
                pageMod.UserID = mu.UserID;
                pageMod.UserName = mu.UserName;
                pageMod.ZType = 0;
                //预览图
                SFile_Up.SaveUrl = SiteConfig.SiteOption.UploadDir + "User/" + mu.UserName + mu.UserID + "/";
                pageMod.PreviewImg = SFile_Up.SaveFile();
                //初始化一个页面，设置背景颜色
                string page = @"{""bk"":""[{\""type\"":\""color\"",\""url\"":\""\"",\""color\"":\""" + BgColor_Hid.Value + @"\"",\""post\"":\""\"",\""pageid\"":\""1\""}]"",""scence_conf"":""{\""effect\"":\""slide\"",\""direction\"":\""vertical\"",\""autoplay\"":0,\""loop\"":false}""}";
                string scence = @"[{""id"":1,""title"":""第1页"",""order"":1,""swipe_index"":0}]";
                pageMod.page = page;
                pageMod.scence = scence;
                if (Device.Equals("pc"))
                {
                    pageBll.Insert(pageMod);
                    function.Script(this, "top.CloseDiag();top.location='/design/h5/default.aspx?id=" + pageMod.guid + "';");
                }
                else
                {

                    if (string.IsNullOrEmpty(pageMod.PreviewImg)) { pageMod.PreviewImg = "/UploadFiles/timg.jpg"; }
                    if (string.IsNullOrEmpty(pageMod.Title)) { pageMod.Title = "我的场景"; }
                    string comp = "[";
                    comp += @"""{\""dataMod\"":{\""src\"":\""" + pageMod.PreviewImg + @"\""},\""config\"":{\""type\"":\""image\"",\""compid\"":\""image\"",\""css\"":\""candrag rollIn\"",\""style\"":\""position:absolute;top:448px;left:106px;width:438px;height:254px;\"",\""imgstyle\"":\""width: 440px; height:254px;\"",\""bodyid\"":\""mainBody1\"",\""contain_style\"":\""\"",\""animate\"":{\""enabled\"":true,\""duration\"":1,\""delay\"":\""0.8\"",\""effect\"":\""rollIn\"",\""count\"":1}}}"",";
                    comp += @"""{\""dataMod\"":{\""text\"":\""\\n " + pageMod.Title + @"\\n\""},\""config\"":{\""type\"":\""text\"",\""compid\"":\""comp1\"",\""css\"":\""candrag \"",\""style\"":\""position:absolute;top:284px;left:100px;font-size:60px;\"",\""bodyid\"":\""mainBody1\"",\""contain_style\"":\""\"",\""animate\"":{\""enabled\"":true,\""duration\"":1,\""delay\"":0,\""effect\"":\""fadeInDown\"",\""count\"":1}}}""";
                    comp += "]";
                    pageMod.comp = comp;
                    pageBll.Insert(pageMod);
                    Response.Redirect("/design/mbh5/default.aspx?id=" + pageMod.guid);
                }
            }
        }
    }
}