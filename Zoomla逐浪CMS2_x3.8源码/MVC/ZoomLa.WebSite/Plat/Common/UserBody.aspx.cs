using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using System.Data;

namespace ZoomLaCMS.Plat.Common
{
    public partial class UserBody : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_User_Plat platBll = new B_User_Plat();
        B_Structure struBll = new B_Structure();

        public string PreValue { get { return Request.QueryString["skey"] ?? ""; } }
        public int PSize { get { return DataConverter.CLng(Request.QueryString["psize"]); } }
        public int PIndex { get { return DataConverter.CLng(Request.QueryString["pindex"]); } }

        public int GroupID { get { return DataConverter.CLng(Request.QueryString["groupid"]); } }
        public string Char { get { return Request.QueryString["char"] ?? ""; } }
        public string Source { get { return Request.QueryString["source"] ?? "user"; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            //暂时禁用分页
            if (PIndex > 1) { return; }
            M_User_Plat upMod = B_User_Plat.GetLogin();
            DataTable dt = new DataTable();
            if (GroupID > 0)//按部门查询
            {
                dt = platBll.SelByGroup(upMod.CompID, GroupID);
            }
            else
            {
                dt = platBll.SelByCompany(upMod.CompID, PreValue);
            }
            UserPlat_RPT.DataSource = dt;
            UserPlat_RPT.DataBind();
        }
        public string GetHeadIcon()
        {
            return string.IsNullOrEmpty(Eval("salt").ToString()) ? "/images/userface/noface.png" : Eval("salt").ToString();
        }
    }
}