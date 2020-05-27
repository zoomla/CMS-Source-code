namespace ZoomLaCMS.Common.Label.Map
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.SQLDAL;
    public partial class BaiduMapView : System.Web.UI.Page
    {
        B_Content conBll = new B_Content();
        //用于浏览百度地图
        //浏览模式,前端,简单,完全版
        public string Type { get { return Request.QueryString["Type"]; } }
        public string Field { get { return Request.QueryString["field"]; } }
        public int Gid { get { return DataConvert.CLng(Request.QueryString["Gid"]); } }
        //仅用于简单模式
        public string Point
        {
            get
            {
                string _point = Request.QueryString["Point"];
                if (string.IsNullOrEmpty(_point)) { _point = "116.404,39.915"; }
                return _point;
            }
        }
        //是否预览模式(预览模式下,出遮罩层,点击出对话框)
        public int IsPre { get { return DataConvert.CLng(Request.QueryString["IsPre"]); } }
        //完全版比较复杂,不过通过地址栏传值,而是通过(Field_Hid)读取
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IsPre == 1) { function.Script(this, "preMode()"); }
                if (Gid > 0 && !string.IsNullOrEmpty(Field))
                {
                    M_CommonData model = conBll.SelReturnModel(Gid);
                    if (model == null) { function.WriteErrMsg("指定的数据不存在"); }
                    DataTable dt = SqlHelper.ExecuteTable("SELECT * FROM " + model.TableName + " WHERE ID=" + model.ItemID);
                    if (dt.Rows.Count < 1 || !dt.Columns.Contains(Field)) { function.WriteErrMsg("指定的地图数据不存在"); }
                    MapData_Hid.Value = dt.Rows[0][Field].ToString();
                }
            }
        }
    }
}