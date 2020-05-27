using System;
using System.Data;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using Newtonsoft.Json;

namespace ZoomLaCMS.Common
{
    public partial class AddbindPro : System.Web.UI.Page
    {
        B_Product pll = new B_Product();
        B_BindPro bll = new B_BindPro();
        B_Content conBll = new B_Content();
        B_Admin badmin = new B_Admin();
        B_User buser = new B_User();
        //admin,user
        public string Filter
        {
            get
            {
                string _req = Request.QueryString["filter"];
                _req = string.IsNullOrEmpty(_req) ? "admin" : _req;
                return _req;
            }
        }
        public int ProID
        {
            get { return DataConverter.CLng(Request.QueryString["id"]); }
        }
        public string ProClass { get { return Request.QueryString["ProClass"] ?? ""; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }

        }
        private void MyBind(string keyword = "")
        {
            DataTable dt = new DataTable();
            switch (Filter)
            {
                case "admin":
                    {
                        if (!badmin.CheckLogin()) { function.WriteErrMsg("您还没有登录"); }
                        dt = pll.GetProductAll(0, 0, 0, 0, ProClass);
                    }
                    break;
                case "user":
                    {
                        M_UserInfo mu = buser.GetLogin();
                        if (mu.UserID < 1) { function.WriteErrMsg("您还没有登录"); }
                        M_CommonData storeMod = conBll.SelMyStore(mu.UserName);
                        if (storeMod == null) { function.WriteErrMsg("您还没有店铺"); }
                        dt = pll.GetProductAll(0, storeMod.GeneralID);//StoreID
                    }
                    break;
                default:
                    function.WriteErrMsg("参数错误");
                    break;
            }
            dt.DefaultView.RowFilter = "ID <> " + ProID + " AND ProName LIKE '%" + keyword + "%'";
            dt.DefaultView.Sort = "ID ASC";
            RPT.DataSource = dt;
            RPT.DataBind();
            if (ProClass.Equals("6")) { AddIDC_Btn.Visible = true; AddPro_Btn.Visible = false; }
        }
        public string getproimg(string type)
        {
            string restring;
            restring = "";
            if (!string.IsNullOrEmpty(type)) { type = type.ToLower(); }
            if (!string.IsNullOrEmpty(type) && (type.IndexOf(".gif") > -1 || type.IndexOf(".jpg") > -1 || type.IndexOf(".png") > -1))
            {
                restring = "<img src=/" + type + " border=0 width=60 height=45>";
            }
            else
            {
                restring = "<img src=/UploadFiles/nopic.gif border=0 width=60 height=45>";
            }
            return restring;

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>window.close();</script>");
        }
        protected void Skey_Btn_Click(object sender, EventArgs e)
        {
            MyBind(Skey_T.Text);
        }
        protected void AddIDC_Btn_Click(object sender, EventArgs e)
        {
            //string ids = Request.Form["idchk"];//SELECT ID,ServerType,ServerPeriod,LinPrice FROM ZL_Commodities
            //DataTable dt = pll.SelByIDS(ids, "id,Proname,ServerType,ServerPeriod,LinPrice");
            //dt.Columns.Add(new DataColumn("pronum", typeof(int)));
            //dt.Columns.Add(new DataColumn("peroid", typeof(string)));
            //dt.Columns.Add(new DataColumn("endtime", typeof(string)));
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    DataRow dr = dt.Rows[i];
            //    dr["pronum"] = DataConverter.CLng(Request.Form["pronum" + dr["id"]], 1);
            //    dr["peroid"] = pll.GetServerPeriod(Convert.ToInt32(dr["ServerPeriod"]), Convert.ToInt32(dr["ServerType"]));
            //    dr["endtime"] = pll.GetEndTime(Convert.ToInt32(dr["ServerPeriod"]), Convert.ToInt32(dr["ServerType"]), Convert.ToInt32(dr["pronum"]), DateTime.Now);
            //}
            //string json = JsonConvert.SerializeObject(dt);
            //function.Script(this, " parent.BindPro('" + json + "'); parent.CloseDiag();");
        }
    }
}