using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Project;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Project;
using ZoomLa.SQLDAL;

public partial class test_view : System.Web.UI.Page
{
    B_Pro_Step stepBll = new B_Pro_Step();
    B_Pro_Progress progBll = new B_Pro_Progress();
    B_User buser = new B_User();
    public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    B_Content conBll = new B_Content();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Mid < 1) { function.WriteErrMsg("未指定项目"); }
            M_Pro_Step model = stepBll.SelReturnModel(Mid);
            Title_L.Text = "项目管理-" + model.Title;
            if (buser.GetLogin().UserID == model.CUser) { Edit_Span.Visible = true; }
            M_Content conMod = new M_Content();
            conMod.id = model.ID;
            conMod.title = model.Title;
            conMod.topimg = model.topimg;
            if (!string.IsNullOrEmpty(model.comlist))
            {
                conMod.comlist = JsonConvert.DeserializeObject<List<M_Component>>(model.comlist);
            }
            conMod.mp3 = model.mp3;
            //conMod.mp3 = DataConvert.CStr(table.Rows[0]["music"]);
            //conMod.pic = DataConvert.CStr(table.Rows[0]["pic"]);
            Save_Hid.Value = JsonConvert.SerializeObject(conMod);

            //RPT.DataSource = progBll.SelByProID(model.ProID);
            //RPT.DataBind();
        }
    }
    public class M_Content
    {
        public int id = 0;
        public string title = "";
        public string topimg = "";
        //封面图片(暂定为取第一张图)
        public string pic = "";
        //背景音乐
        public string mp3 = "";
        ////成员控件列表,用于后期修改
        public List<M_Component> comlist = new List<M_Component>();
    }
    public class M_Component
    {
        public string id = "";
        public string content = "";
        public string type = "";
        public string videoType = "";
        public string text = "";
        public bool openText = false;
        public string title = "";
        public int orderID = 0;
    }
}