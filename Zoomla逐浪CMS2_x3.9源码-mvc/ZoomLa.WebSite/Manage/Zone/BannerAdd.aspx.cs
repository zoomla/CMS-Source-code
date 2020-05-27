using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Sns;

namespace ZoomLaCMS.Manage.Zone
{
    public partial class BannerAdd : CustomerPageAction
    {

        #region 业务对象
        B_StoreStyleTable sstbll = new B_StoreStyleTable();
        B_Admin abll = new B_Admin();
        B_Model mbll = new B_Model();
        BlogStyleTableBLL bstbll = new BlogStyleTableBLL();
        #endregion

        #region 初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='BannerManage.aspx'>栏目管理</a></li><li>添加会员组模型</li>");
            if (!IsPostBack)
            {

            }
        }
        #endregion

        #region 页面方法
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("BannerManage.aspx");
        }
        #endregion
    }
}