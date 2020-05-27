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
using ZoomLa.Common;
namespace ZoomLaCMS.Manage.AddOn
{
    public partial class SelectSource : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //function.AccessRulo();
            B_Admin badmin = new B_Admin();
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["flag"] != null)
                {

                    InitPage(Request.QueryString["flag"].Trim());
                }
                //MyBind();

            }

        }
        /// <summary>
        /// 作者绑定
        /// </summary>
        /// <param name="flag"></param>
        public void InitPage(string flag)
        {
            if ("author" == flag)
            {
                B_Author bauthor = new B_Author();
                DataTable dt = bauthor.GetSourceAll();//GetSourceAll()
                RepFiles.DataSource = dt;
                RepFiles.DataBind();
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
            else if ("source" == flag)
            {
                MyBind();
            }
            else
            {
                BindKeyword();
            }




        }
        /// <summary>
        /// 来源绑定
        /// </summary>
        protected void MyBind()
        {
            B_Source bsource = new B_Source();
            DataTable dt = bsource.GetSourceAll();
            RepFiles.DataSource = dt;
            RepFiles.DataBind();
            if (dt != null)
            {
                dt.Dispose();
            }
        }
        //关键字绑定
        protected void BindKeyword()
        {
            B_KeyWord bkeyword = new B_KeyWord();
            DataTable dt = bkeyword.GetKeyWordAll();
            RepKeyword.DataSource = dt;
            RepKeyword.DataBind();
            if (dt != null)
            {
                dt.Dispose();
            }
        }
        protected void RepFiles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataRowView dataItem = (DataRowView)e.Item.DataItem;
                if ((dataItem["name"].ToString() == "") || (dataItem["name"].ToString() == ""))
                {
                    e.Item.Visible = false;
                }
            }
        }
    }
}