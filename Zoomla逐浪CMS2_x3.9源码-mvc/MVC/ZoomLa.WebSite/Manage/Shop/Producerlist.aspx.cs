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
using System.Collections.Generic;
using ZoomLa.BLL;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class Producerlist : CustomerPageAction
    {
        protected int i;
        protected string m_allUser;
        protected string m_allUserName;
        protected string m_UserInput;
        protected int PageSize;
        protected int CurrentPageIndex;
        private B_Manufacturers bll = new B_Manufacturers();

        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();

            function.SetPageNoCache();

            this.m_UserInput = base.Request.QueryString["OpenerText"];

            this.PageSize = 20;
            if (string.IsNullOrEmpty(base.Request.QueryString["p"]))
            {
                this.CurrentPageIndex = 1;
            }
            else
            {
                this.CurrentPageIndex = DataConverter.CLng(base.Request.QueryString["p"]);
            }
            this.CheckKeyword();
            Call.SetBreadCrumb(Master, "<li>商城配置</li><li>产商选择</li>");
        }

        private void BindData(string keyword)
        {
            DataTable list = bll.GetManufacturersAll("");

            if (list.Rows.Count == 0)
            {
                this.DivUserName.Visible = true;
            }
            else
            {
                this.DivUserName.Visible = false;
            }
            this.RepUser.DataSource = list.DefaultView;
            int RecordCount = list.Rows.Count;

            this.RepUser.DataBind();
        }

        private void CheckKeyword()
        {
            string str = DataSecurity.FilterBadChar(base.Request.Form["TxtKeyWord"]);
            this.BindData(str);
        }
        protected void RepUser_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                if (string.IsNullOrEmpty(this.m_allUser))
                {
                    this.m_allUser = e.Item.DataItem.ToString();
                }
                else
                {
                    this.m_allUser = this.m_allUser + "," + e.Item.DataItem.ToString();
                }
            }
        }
    }
}