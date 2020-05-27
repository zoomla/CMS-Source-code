using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class CardManage : CustomerPageAction
    {
        B_Card bc = new B_Card();
        B_User bu = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();


            string menu = Request.QueryString["menu"];
            int pageid = DataConverter.CLng(Request.QueryString["id"]);

            switch (menu)
            {
                case "del":
                    if (bc.delcards(pageid.ToString()))
                    {
                        Response.Write("<script language=javascript>alert('删除成功!');location.href='CardManage.aspx';</script>");
                    }
                    else
                    {
                        Response.Write("<script language=javascript>alert('删除失败!请选择您要删除的数据');location.href='CardManage.aspx';</script>");
                    }
                    break;

                case "isuse":
                    bc.OpenOrStopCard(pageid, 1);

                    break;

                case "stop":
                    bc.OpenOrStopCard(pageid, 2);

                    break;
            }
            Bind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li> <li><a href='DeliverType.aspx'>商城设置</a></li><li>VIP卡管理</a></li><li><a href='AddCard.aspx'>[ 生成VIP卡 ]</a><a href='CardTypeManage.aspx'>[ 卡类型管理 ]</a><a href='Addcardtype.aspx'>[添加卡类型 ]</a></li>");
        }

        private void Bind()
        {
            int CPage, temppage;
            int CardState = 0;
            if (Request.QueryString["CardState"] != null)
            {
                CardState = DataConverter.CLng(Request.QueryString["CardState"].ToString());
            }


            if (Request.Form["DropDownList1"] != null)
            {
                temppage = Convert.ToInt32(Request.Form["DropDownList1"]);
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

            DataTable Cll = bc.SelectAll();

            if (CardState == 2)
            {
                Cll = bc.SelByAuid(2);
            }
            else if (CardState == 1)
            {
                Cll = bc.SelByAuid(1);
            }


            PagedDataSource cc = new PagedDataSource();
            cc.DataSource = Cll.DefaultView;
            cc.AllowPaging = true;
            cc.PageSize = 20;
            cc.CurrentPageIndex = CPage - 1;
            gvCard.DataSource = cc;
            gvCard.DataBind();

            Allnum.Text = Cll.DefaultView.Count.ToString();
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
                    DropDownList1.Items.Add(i.ToString());
                }
            }

        }

        protected string GetUserName(string uid)
        {
            if (DataConverter.CLng(uid) == 0)
            {
                return "暂无用户";
            }
            else
            {
                return bu.GetUserByUserID(int.Parse(uid)).UserName;
            }

        }

        protected string GetState(string str)
        {
            string state = "";
            switch (str)
            {
                case "2":
                    state = "  <span style=' color:red;'>√</span>";
                    break;
                case "1":
                    state = "×";
                    break;
            }
            return state;
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            string CID = Request.Form["Item"];
            if (!String.IsNullOrEmpty(CID) && bc.delcards(CID))
            {
                Response.Write("<script language=javascript>alert('批量删除成功!');location.href='CardManage.aspx';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('批量删除失败!请选择您要删除的数据');location.href='CardManage.aspx';</script>");
            }
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            string CID = Request.Form["Item"];
            if (!String.IsNullOrEmpty(CID) && bc.OpenOrStopCards(CID, 2))
            {
                Response.Write("<script language=javascript>alert('批量开启成功!');location.href='CardManage.aspx';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('批量开启失败');location.href='CardManage.aspx';</script>");
            }
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            string CID = Request.Form["Item"];
            if (!String.IsNullOrEmpty(CID) && bc.OpenOrStopCards(CID, 1))
            {
                Response.Write("<script language=javascript>alert('批量关闭成功!');location.href='CardManage.aspx';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('批量关闭失败');location.href='CardManage.aspx';</script>");
            }
        }

        public string showuse(string id)
        {
            int sid = DataConverter.CLng(id);
            string str = "";
            M_Card tp = bc.GetSelect(sid);
            if (tp.CardState == 1)
            {
                str = "<a href=?menu=stop&id=" + sid + ">启用</a>";
            }
            else
            {
                str = "<a href=?menu=isuse&id=" + sid + ">禁用</a>";
            }
            return str;
        }
    }
}