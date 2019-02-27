namespace ZoomLa.WebSite
{
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
    using ZoomLa.Model;
    using ZoomLa.Components;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;

    public partial class SearchList : System.Web.UI.Page
    {
        private B_Content bll = new B_Content();
        private B_Node bnode = new B_Node();
        private B_Model mll = new B_Model();
        private B_Product pll = new B_Product();
        public string key = string.Empty;
        public string[] keys = null;
        string[] keys1 = null;
        int count = 0;
        int t = 0;
        /// <summary>
        /// 递归查询节点 for 商城
        /// </summary>
        /// <param name="Pid"></param>
        private void GetShopNode(int Pid)
        {
            DataTable templist = this.bnode.GetAllShopNode(Pid);

            for (int i = 0; i < templist.Rows.Count; i++)
            {
                string Spanstr = new string('　', t);
                t = t + 1;
                this.DDLNode.Items.Add(new ListItem(Spanstr + templist.Rows[i]["NodeName"].ToString(), templist.Rows[i]["NodeID"].ToString()));
                GetShopNode(DataConverter.CLng(templist.Rows[i]["NodeID"].ToString())); //递归查询
                t = t - 1;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string node = "0";
            int Cpage = 0;
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(base.Request.QueryString["node"]))
                    node = base.Request.QueryString["node"];
                if (string.IsNullOrEmpty(base.Request.QueryString["keyword"]) || Server.UrlDecode(Request.QueryString["keyword"]) == "请输入关键字")
                {
                    nonemsg.Visible = true;
                    return;
                }
                else
                {
                    key = Request.QueryString["keyword"].Replace("&quot", "\"");
                    this.TxtKeyword.Text = key;
                    //    int i = key.IndexOf('"');
                    //    int j = key.IndexOf('"', i + 1);
                    //    string key1 = string.Empty;
                    //    if (j > i && i >= 0)
                    //    {
                    //        key1 = key.Substring(i + 1, j - i - 1);
                    //        keys1 = key1.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    //        if (key.Substring(0, i) != "" && key.Substring(j + 1) != "")
                    //        {
                    //            key1 = key.Substring(0, i) + " " + key.Substring(j + 1);
                    //            keys = key1.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        keys = key.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    //    }
                    //}


                }
                this.sitename.Text = SiteConfig.SiteInfo.SiteName;
                DDLNode.Items.Clear();
                GetShopNode(0);
                ListItem item = new ListItem("所有栏目", "0");
                this.DDLNode.Items.Insert(0, item);
                if (!string.IsNullOrEmpty(base.Request.QueryString["p"]))
                    Cpage = DataConverter.CLng(base.Request.QueryString["p"]);
                else
                    Cpage = 1;
                //int PageSize = 20;
                DataTable dt = new B_Product().ProductSearch(DataConvert.CLng(node),key); 
                int Total = dt.Rows.Count;
                count = dt.Rows.Count;
                if (count > 0)
                {
                    this.PShow.Visible = false;
                    this.Repeater1.DataSource = dt;
                    this.Repeater1.DataBind();
                }
                if (dt.Rows.Count == 0)
                {
                    this.PShow.Visible = true;
                }
                //this.Pager1.InnerHtml = function.ShowPage(Total, PageSize, Cpage, true, "项");
                this.DDLNode.SelectedValue = node.ToString();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.TxtKeyword.Text.Trim() != "" && this.TxtKeyword.Text.Trim() != "请输入关键字")
            {
                string keyword = this.TxtKeyword.Text.Trim();
                string searchurl = "ShopList.aspx";
                string nodeid = this.DDLNode.SelectedValue;
                Response.Write("<script>alert('"+keyword+"');</script>");
                searchurl = searchurl + "?node=" + nodeid + "&keyword=" + Server.UrlEncode(keyword);
                Response.Redirect(searchurl);
            }
            else
            {
                Response.Redirect("ShopList.aspx");
            }
        }
        // 跳转页面地址
        public string GetUrl(string itemid)
        {
            //string modename;
            //string urls = string.Empty;
            //M_CommonData cinfo = this.bll.GetCommonData(DataConverter.CLng(itemid));
            //if (cinfo.IsCreate == 1)
            //{
            //    return SiteConfig.SiteInfo.SiteUrl + cinfo.HtmlLink;
            //}
            //else
            //{
            //    M_CommonData commonData = bll.GetCommonData(DataConverter.CLng(itemid));
            //    modename = commonData.TableName.Substring(0, 5);

            //    if (modename == "ZL_C_")
            //    {
            //        urls = "/Shop.aspx?ItemID=" + itemid;
            //    }
            //    else if (modename == "ZL_P_")
            //    {
            //        DataTable protable = pll.SelectProByCmdID(DataConverter.CLng(itemid));

            //        if (protable != null && protable.Rows.Count > 0)//当内容表与商品表同步时，取商品地址
            //        {
            //            urls = "/Shop.aspx?ItemID=" + protable.Rows[0]["ID"];
            //        }
            //        else//否则取内容地址
            //        {
            //            urls = "/Shop.aspx?ItemID=" + itemid;
            //        }
            //    }
            //    return urls;
            //}
            string urls = "/Shop/" + itemid+".aspx";
            return urls;

        }

        /// <summary>
        /// 返回查询得到的数目
        /// </summary>
        /// <returns></returns>
        public int GetProductsCount()
        {
            return count;
        }

        /// <summary>
        /// 查询获得的关键字
        /// </summary>
        /// <returns></returns>
        public string getkeys()
        {
            string lbrhtml = string.Empty;
            if (keys1 != null)
            {
                lbrhtml += "[<font style='color:Blue'>" + keys1[0] + "</font>]";
                for (int i = 1; i < keys1.Length; i++)
                {
                    lbrhtml += "和[<font style='color:Blue'>" + keys1[i] + "</font>]";
                }
            }
            if (keys != null)
            {
                lbrhtml += "[<font style='color:Blue'>" + keys[0] + "</font>]";
                for (int i = 1; i < keys.Length; i++)
                {
                    lbrhtml += "或[<font style='color:Blue'>" + keys[i] + "</font>]";
                }
            }
            return lbrhtml;
        }
        // 将结果中关键字替换为红色
        public string toRed(string de)
        {
            string[] keyArr = key.Replace('"', ' ').Split(new string[] { " " },

StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < keyArr.Length; i++)
            {
                de = de.Replace(keyArr[i], "<font style='color:Red'>" + keyArr[i] + "</font>");
            }
            return de;
        }

    }
}