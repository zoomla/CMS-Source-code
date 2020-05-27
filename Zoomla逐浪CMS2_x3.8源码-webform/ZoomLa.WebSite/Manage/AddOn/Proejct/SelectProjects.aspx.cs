using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Components;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Web;
using ZoomLa.Common;
using ZoomLa.BLL;

public partial class manage_AddOn_SelectProjects : System.Web.UI.Page
{
      M_AdminInfo manager = new M_AdminInfo();
        /// <summary>
        /// 页面初次加载,绑定数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();


            if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "select")
            {
                int id = DataConverter.CLng(Request.QueryString["id"]);
                M_AdminInfo adminInfo = B_Admin.GetAdminByAdminId(id);
                string scripttxt = "setvalue('Leader','" + adminInfo.AdminTrueName + "');";
                 //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>" + scripttxt + "</script>");

            }
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 变量说明：flag判断记录是否选中，chkCount选种记录数量
        //protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "SelectAdmin")
        //    {   
        //        function.Script(this, "parent.getUser(" + e.CommandArgument.ToString() + ");");
        //     }
        //}
        public string Getroleid()
        {
            int id = DataConverter.CLng(ViewState["id"]);
            if (id > 0)
            {
                return "?roleid="+id;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void MyBind()
        {
            Egv.DataSource = new B_Admin().Sel();
            Egv.DataKeyNames = new string[] { "AdminID" };
            Egv.DataBind();
        }
       
       
        /// <summary>
        /// 绑定的行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Egv_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = "<i>" + e.Row.Cells[1].Text + "</i>";
                e.Row.Attributes.Add("onmouseover", "this.className='tdbgmouseover'");
                e.Row.Attributes.Add("onmouseout", "this.className='tdbg'");
            }
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Egv.PageIndex = e.NewPageIndex;
            MyBind();
        }
}
