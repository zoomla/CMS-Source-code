using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.CreateJS;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Site
{
    public partial class HostAdd : System.Web.UI.Page
    {
        B_CodeModel hostBll = new B_CodeModel("ZL_FZ_User");
        private string FzUserName { get { return HttpUtility.UrlDecode(Request["Name"] ?? ""); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", FzUserName) };
                DataTable dt = hostBll.SelByWhere("Name=@name", "Name", sp);
                if (dt.Rows.Count < 1) { function.WriteErrMsg("主机[" + FzUserName + "]不存在"); }
                DataRow dr = dt.Rows[0];
                Name_T.Text = DataConverter.CStr(dr["Name"]);
                UserPwd_T.Text = DataConverter.CStr(dr["UserPwd"]);
                CDate_T.Text = DataConverter.CStr(dr["CDate"]);
                EndDate_T.Text = DataConverter.CDate(dr["EndDate"]).ToString("yyyy/MM/dd");
                SiteInfo_T.Text = DataConverter.CStr(dr["SiteInfo"]);
                 Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>站群管理</a></li><li><a href='" + Request.RawUrl + "'>主机管理</a></li><li>主机信息</li>");
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", FzUserName) };
            DataRow dr = hostBll.SelByWhere("Name=@name", "Name", sp).Rows[0];
            dr["CDate"] = DataConverter.CDate(CDate_T.Text);
            dr["EndDate"] = DataConverter.CDate(EndDate_T.Text);
            dr["SiteInfo"] = SiteInfo_T.Text.Trim();
            hostBll.UpdateByID(dr,"Name");
            function.WriteSuccessMsg("操作成功", "HostList.aspx");
        }
    }
}