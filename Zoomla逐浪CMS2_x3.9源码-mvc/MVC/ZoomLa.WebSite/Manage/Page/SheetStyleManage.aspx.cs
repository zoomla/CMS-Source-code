using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using System.Data;
using ZoomLa.BLL.User.Develop;

namespace ZoomLaCMS.Manage.Page
{
    public partial class SheetStyleManage : CustomerPageAction
    {
        //B_Zone_SheetStyle bzss = new B_Zone_SheetStyle();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["menu"] == "del")
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    //bzss.Delete(id);
                }
                view();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='PageManage.aspx'>企业黄页</a></li><li>黄页标签管理</li>");
            }
        }
        protected void txtPage_TextChanged(object sender, EventArgs e)
        {

        }

        #region 通用分页过程
        /// <summary>
        /// 通用分页过程　by h.
        /// </summary>
        /// <param name="Cll"></param>
        public void Page_list(DataTable Cll)
        {
            Styleable.DataSource = Cll;
            Styleable.DataBind();
        }
        public void view()
        {
            DataTable dt = new DataTable();
            if (Request["classes"] != null)
            {
                //switch (Convert.ToInt32(Request["classes"]))
                //{
                //    case 1:
                //        dt = bzss.SelectAll();
                //        break;
                //    case 2:
                //        dt = bzss.selectPayTage(1);
                //        break;
                //    case 3:
                //        dt = bzss.selectPayTage(0);
                //        break;
                //}

            }
            //else if (Request["types"] != null)
            //{
            //    dt = bzss.SelByType(Convert.ToInt32(Request["types"]), Request.QueryString["names"]);
            //}
            //else
            //{
            //    dt = bzss.SelectAll();
            //}
            Page_list(dt);
        }
        #endregion
        protected void quicksouch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("SheetStyleManage.aspx?classes=" + quicksouch.SelectedValue);
        }
        protected void souchok_Click(object sender, EventArgs e)
        {
            Response.Redirect("SheetStyleManage.aspx?types=" + souchtable.SelectedValue + "&names=" + souchkey.Text.Trim());
        }
    }
}