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
using ZoomLa.Model.Plat;
using ZoomLa.Model.Project;
using ZoomLa.SQLDAL;

public partial class User_Content_Note_mobileview : System.Web.UI.Page
{
    B_Pro_Step stepBll = new B_Pro_Step();
    B_Pro_Progress progBll = new B_Pro_Progress();
    B_User buser = new B_User();
    public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Mid < 1) { function.WriteErrMsg("未指定项目"); }
            M_Pro_Step model = stepBll.SelReturnModel(Mid);
            M_Plat_Content conMod = new M_Plat_Content();
            conMod.id = model.ID;
            conMod.title = model.Title;
            conMod.topimg = model.topimg;
            if (!string.IsNullOrEmpty(model.comlist))
            {
                conMod.comlist = JsonConvert.DeserializeObject<List<M_Plat_Component>>(model.comlist);
            }
            conMod.mp3 = model.mp3;
            //conMod.mp3 = DataConvert.CStr(table.Rows[0]["music"]);
            //conMod.pic = DataConvert.CStr(table.Rows[0]["pic"]);
            Save_Hid.Value = JsonConvert.SerializeObject(conMod);
            //RPT.DataSource = progBll.SelByProID(model.ProID);
            //RPT.DataBind();
        }
    }
}