using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Text;
using BDUBLL;
using BDUModel;
using ZoomLa.Sns;


namespace ZoomLaCMS.Manage.Zone
{
    public partial class ZoneApplyManage : CustomerPageAction
    {
        blogTableBLL btbll = new blogTableBLL();
        B_User ubll = new B_User();
        protected int NodeID;
        protected string flag;
        protected int tempnodeid;

        private int uid
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["UserState"]["UserID"] != null)
                    return int.Parse(HttpContext.Current.Request.Cookies["UserState"]["UserID"].ToString());
                else
                    return 0;
            }
            set
            {
                uid = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();

            if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "ZoneApply"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            badmin.CheckIsLogin();
            if (!IsPostBack)
            {
                GetInit();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "I/User/UserManage.aspx'>会员管理</a></li><li><a href='ZoneManage.aspx'>会员空间管理</a></li><li class='active'>审核用户空间</li>");
        }
        private void GetInit()
        {
            int CPage;
            int temppage;
            if (!string.IsNullOrEmpty(DropDownList1.SelectedValue))
            {
                temppage = Convert.ToInt32(DropDownList1.SelectedValue);
            }
            else
            {
                temppage = Convert.ToInt32(Request.QueryString["CurrentPage"]);
            }
            CPage = temppage;
            if (CPage <= 0)
            {
                CPage = 1;
            }

            List<blogTable> list = btbll.GetBlogTableByState(0);

            PagedDataSource cc = new PagedDataSource();
            cc.DataSource = list;
            cc.AllowPaging = true;
            cc.PageSize = 1;
            cc.CurrentPageIndex = CPage - 1;
            Productlist.DataSource = cc;
            Productlist.DataBind();

            Allnum.Text = list.Count.ToString();
            int thispagenull = cc.PageCount;//总页数
            int CurrentPage = cc.CurrentPageIndex;
            int nextpagenum = CPage - 1;//上一页
            int downpagenum = CPage + 1;//下一页
            int Endpagenum = thispagenull;
            if (thispagenull <= CPage)
            {
                downpagenum = thispagenull;
                Downpage.Enabled = false;
            }
            else
            {
                Downpage.Enabled = true;
            }
            if (nextpagenum <= 0)
            {
                nextpagenum = 0;
                Nextpage.Enabled = false;
            }
            else
            {
                Nextpage.Enabled = true;
            }

            Toppage.Text = "<a href=?Currentpage=0>首页</a>";
            Nextpage.Text = "<a href=?Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
            Downpage.Text = "<a href=?Currentpage=" + downpagenum.ToString() + ">下一页</a>";
            Endpage.Text = "<a href=?Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
            Nowpage.Text = CPage.ToString();
            PageSize.Text = thispagenull.ToString();
            pagess.Text = cc.PageSize.ToString();
            if (!this.Page.IsPostBack)
            {
                for (int i = 1; i <= thispagenull; i++)
                {
                    this.DropDownList1.Items.Add(i.ToString());
                }
            }
        }
        protected string GetState(string gid)
        {
            switch (gid)
            {
                case "0": return "普通";
                case "2": return "关闭";
                case "1": return "推荐";
                default: return "";
            }
        }

        protected string GetUsername(string userid)
        {
            return ubll.GetUserByUserID(int.Parse(userid)).UserName;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string list = Request.Form["Item"];
                if (!string.IsNullOrEmpty(list))
                {
                    string[] gidlist = list.Split(new char[] { ',' });

                    Button bt = sender as Button;
                    if (bt.CommandName == "5")
                    {
                        btbll.BatchUpdateState(list, int.Parse(bt.CommandName), "D");

                    }
                    else
                    {
                        btbll.BatchUpdateState(list, int.Parse(bt.CommandName), "A");

                    }
                }
                GetInit();
            }
            catch (Exception ee)
            {
                function.WriteErrMsg(ee.Message);
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            LinkButton lb = sender as LinkButton;
            btbll.DelBlogtable(int.Parse(lb.CommandName));
            GetInit();
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            LinkButton lb = sender as LinkButton;
            btbll.BatchUpdateState(lb.CommandName, 1, "A");
            GetInit();
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetInit();
        }
    }
}