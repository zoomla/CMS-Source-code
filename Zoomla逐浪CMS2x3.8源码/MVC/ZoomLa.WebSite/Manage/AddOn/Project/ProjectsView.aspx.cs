using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Data;


namespace ZoomLaCMS.Manage.AddOn.Project
{
    public partial class ProjectsView : System.Web.UI.Page
    {
        M_UserInfo mu = new M_UserInfo();
        B_User bu = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();
            if (!IsPostBack)
            {
                Bind();
            }
        }
        //项目管理所有信息
        private void Bind()
        {
            DataTable dt = new DataTable();
            // dt = bpt.Select_All();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow rows in dt.Rows)
                {
                    int id = DataConverter.CLng(rows["ID"]);
                    string projectsName = rows["Name"].ToString();
                    DateTime time = Convert.ToDateTime(rows["ApplicationTime"]);
                    string leader = rows["Leader"].ToString();
                }
            }
            if (dt != null)
            {
                dt.Dispose();
            }
        }
    }
}