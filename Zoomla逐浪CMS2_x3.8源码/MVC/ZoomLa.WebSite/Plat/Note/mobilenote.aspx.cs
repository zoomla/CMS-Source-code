using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Project;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Project;
using ZoomLa.SQLDAL;
using ZoomLa.Model.Plat;

namespace ZoomLaCMS.Plat.Note
{
    public partial class mobilenote : System.Web.UI.Page
    {
        public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        //public int NodeID { get { return DataConvert.CLng(Request.QueryString["nodeid"]); } }
        B_Pro_Step stepBll = new B_Pro_Step();
        B_User buser = new B_User();
        //http://player.youku.com/player.php/sid/XMTQ1OTU0NzMwOA==/v.swf
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {

            }
            if (!IsPostBack)
            {
                B_User.CheckIsLogged(Request.RawUrl);
                if (Mid > 0)
                {
                    M_UserInfo mu = buser.GetLogin();
                    M_Pro_Step model = stepBll.SelReturnModel(Mid);
                    M_Plat_Content conMod = new M_Plat_Content();
                    conMod.id = model.ID;
                    conMod.title = model.Title;
                    conMod.topimg = model.topimg;
                    //DataTable table = SqlHelper.ExecuteTable("SELECT * FROM ZL_C_YJMX WHERE ID=" + model.ItemID);
                    //if (table.Rows.Count < 1) { function.WriteErrMsg("游记不存在"); }
                    //if (!model.Inputer.Equals(mu.UserName)) { function.WriteErrMsg("你无权修改该游记"); }
                    if (!string.IsNullOrEmpty(model.comlist))
                    {
                        conMod.comlist = JsonConvert.DeserializeObject<List<M_Plat_Component>>(model.comlist);
                    }
                    conMod.mp3 = model.mp3;
                    conMod.pic = "";
                    Save_Hid.Value = JsonConvert.SerializeObject(conMod);
                }
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            int gid = SaveBy(Save_Hid.Value, 99);
            Response.Redirect("mobileview.aspx?id=" + gid);
        }
        private int SaveBy(string json, int status = 0)
        {
            M_UserInfo mu = buser.GetLogin();
            M_Pro_Step model = new M_Pro_Step();
            M_Plat_Content conMod = JsonConvert.DeserializeObject<M_Plat_Content>(json);
            if (conMod.id > 0)
            {
                model = stepBll.SelReturnModel(conMod.id);
            }
            model.Title = conMod.title;
            model.topimg = conMod.topimg;
            //model.Inputer = mu.UserName;
            model.CUser = mu.UserID;
            model.CUName = mu.UserName;
            model.ZStatus = status;
            model.comlist = JsonConvert.SerializeObject(conMod.comlist);
            //model.OrderID = stepBll.GetOrderID(NodeID);
            //model.ProID = NodeID;
            model.mp3 = conMod.mp3;
            //-------------
            if (model.ID > 0) { stepBll.UpdateByID(model); }
            else
            {
                //M_ModelInfo modelMod = new B_Model().SelReturnModel(46);
                //model.ModelID = 46;
                //model.TableName = "ZL_C_yjmx";//"ZL_C_yjmx"
                //model.NodeID = NodeID;
                //model.GeneralID = conBll.AddContent(table, model);
                model.ID = stepBll.Insert(model);
            }
            return model.ID;
        }
    }
}