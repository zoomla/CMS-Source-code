namespace ZoomLaCMS.Design.mbh5
{
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
                pageMod.CUser = mu.UserID;
                pageMod.UserID = mu.UserID;
                pageMod.UserName = mu.UserName;
                pageMod.ZType = 0;
                //预览图
                SFile_Up.SaveUrl = SiteConfig.SiteOption.UploadDir + "User/" + mu.UserName + mu.UserID + "/";
                pageMod.PreviewImg = SFile_Up.SaveFile();
                //初始化场景数据
                InitPage(pageMod);
                pageBll.Insert(pageMod);
                if (Device.Equals("pc"))
                {
                    function.Script(this, "top.CloseDiag();top.location='/design/h5/default.aspx?id=" + pageMod.guid + "';");
                }
                else
                {
                    Response.Redirect("/design/mbh5/default.aspx?id=" + pageMod.guid);
                }
            }
        }
        //初始化场景数据
        public void InitPage(M_Design_Page pageMod)
        {
            //页面上要显示的图片，默认为timg.jpg,当指定了图标时则显示图标
            string timgImg = "/UploadFiles/timg.jpg";
            if (!string.IsNullOrEmpty(pageMod.PreviewImg)) { timgImg = pageMod.PreviewImg; }
            //默认标题
            if (string.IsNullOrEmpty(pageMod.Title)) { pageMod.Title = "我的场景"; }
            //创建既有三个页面
            pageMod.page = @"{""bk"":""[{\""type\"":\""color\"",\""url\"":\""\"",\""color\"":\""" + BgColor_Hid.Value + @"\"",\""post\"":\""\"",\""pageid\"":\""1\""},{\""type\"":\""color\"",\""url\"":\""\"",\""color\"":\""" + BgColor_Hid.Value + @"\"",\""post\"":\""\"",\""pageid\"":\""2\""},{\""type\"":\""color\"",\""url\"":\""\"",\""color\"":\""" + BgColor_Hid.Value + @"\"",\""post\"":\""\"",\""pageid\"":\""3\""}]"",""scence_conf"":""{\""effect\"":\""slide\"",\""direction\"":\""vertical\"",\""autoplay\"":0,\""loop\"":false}""}";
            pageMod.scence = @"[{""id"":1,""title"":""第1页"",""order"":1,""swipe_index"":0},{""id"":2,""title"":""第2页"",""order"":2,""swipe_index"":1},{""id"":3,""title"":""第3页"",""order"":3,""swipe_index"":2}]";
            pageMod.comp = @"[""{\""dataMod\"":{\""src\"":\""" + timgImg + @"\""},\""config\"":{\""type\"":\""image\"",\""compid\"":\""image\"",\""css\"":\""candrag \"",\""style\"":\""position:absolute;top:456px;left:94px;width:438px;height:254px;\"",\""imgstyle\"":\""width: 440px; height:254px;\"",\""bodyid\"":\""mainBody1\"",\""contain_style\"":\""\"",\""animate\"":{\""enabled\"":true,\""duration\"":1,\""delay\"":\""0.8\"",\""effect\"":\""rollIn\"",\""count\"":1}}}"",""{\""dataMod\"":{\""text\"":\""\\n " + pageMod.Title + @"\\n\""},\""config\"":{\""type\"":\""text\"",\""compid\"":\""comp1\"",\""css\"":\""candrag \"",\""style\"":\""position:absolute;top:238px;left:0px;width:638px;height:93px;font-size:60px;text-align:center;\"",\""bodyid\"":\""mainBody1\"",\""contain_style\"":\""\"",\""animate\"":{\""enabled\"":true,\""duration\"":1,\""delay\"":0,\""effect\"":\""fadeInDown\"",\""count\"":1}}}"",""{\""dataMod\"":{\""src\"":\""" + timgImg + @"\""},\""config\"":{\""type\"":\""image\"",\""compid\"":\""image\"",\""css\"":\""candrag \"",\""style\"":\""position:absolute;top:0px;left:6px;width:628px;height:1034px;\"",\""imgstyle\"":\""width: 628px; height: 1034px;\"",\""bodyid\"":\""mainBody2\"",\""contain_style\"":\""\"",\""animate\"":{\""enabled\"":true,\""duration\"":1,\""delay\"":0,\""effect\"":\""zoomIn\"",\""count\"":1}}}"",""{\""dataMod\"":{\""src\"":\""/Design/res/img/qrcode.jpg\""},\""config\"":{\""type\"":\""image\"",\""compid\"":\""image\"",\""css\"":\""candrag fadeInUpBig \"",\""style\"":\""position:absolute;top:380px;left:170px;text-align:center;\"",\""imgstyle\"":\""width:300px;height:300px;\"",\""bodyid\"":\""mainBody3\"",\""contain_style\"":\""\"",\""animate\"":{\""enabled\"":true,\""duration\"":1,\""delay\"":0,\""effect\"":\""fadeInUpBig\"",\""count\"":1}}}"",""{\""dataMod\"":{\""text\"":\""逐浪CMS 微场景设计\""},\""config\"":{\""type\"":\""text\"",\""compid\"":\""comp1\"",\""css\"":\""candrag fadeInDown \"",\""style\"":\""position:absolute;top:230px;left:2px;width:638px;height:69px;font-size:48px;color:rgb(51, 51, 51);text-align:center;font-weight:100;\"",\""bodyid\"":\""mainBody3\"",\""contain_style\"":\""\"",\""animate\"":{\""enabled\"":true,\""duration\"":1,\""delay\"":0,\""effect\"":\""fadeInDown\"",\""count\"":1}}}""]";
        }
    }
}