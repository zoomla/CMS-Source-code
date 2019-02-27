using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Data;

public partial class manage_Shop_ShowGroupUser : CustomerPageAction
{

    private B_Course bcourse = new B_Course();
    protected B_Product pro = new B_Product();
    protected B_GroupBuyList gll = new B_GroupBuyList();
    private B_User bu = new B_User();
    protected int id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin badmin = new B_Admin();
        badmin.CheckIsLogin();
        int proid = DataConverter.CLng(Request.QueryString["proid"]);
        id = proid;
        M_Product proinfo = pro.GetproductByid(proid);
        M_GroupBuyList mgbl = gll.GetSelect(id);
        Call.SetBreadCrumb(Master, "<li>商城管理</li><li>团购管理</li><li>" + proinfo.Proname + "<li>");
        if (Request.QueryString["menu"] != null)
        {
            string menu = Request.QueryString["menu"];
            if (menu == "delete")
            {
                int groupid = DataConverter.CLng(Request.QueryString["groupid"]);
                int proids = DataConverter.CLng(Request.QueryString["proid"]);
                gll.DeleteByGroupID(groupid);
                Response.Redirect("ShowGroupUser.aspx?proid=" + proids + "&Currentpage=0");
            }
        }       
        DataTable blist = gll.SelectGroupByProID(proid);
        if (Request["isbuy"] != null && Request["isbuy"] != "")
        {
            blist.DefaultView.RowFilter = "isbuy=" + Request["isbuy"];
        }
        if (Request["desp"] != null && Request["desp"] == "false")
        {
            blist.DefaultView.RowFilter = "payid=0";
        }
        this.Deposit.Text =formatcs((gll.GetDeposit(id).ToString()),"1","4") + "元";
        this.buymoney.Text=formatcs(gll.GetBuymoney(id).ToString(),"1","4")+ "元";           
        blist = blist.DefaultView.ToTable();
        if (blist != null)
        {
            Page_list(blist);
        }

    }
    public string GetGroupCount(string id)
    {
        int proid = DataConverter.CLng(id);
        B_GroupBuyList list = new B_GroupBuyList();
        return list.SelectGroupByProID(proid).Rows.Count.ToString();
    }

    protected B_ZL_GroupBuy gbuy = new B_ZL_GroupBuy();
    public string GetNowPirce(string proid)
    {
        double nowpirce = 0;
        int id = DataConverter.CLng(proid);
        DataTable gby = gbuy.GetGroupBuyByShopID(id);
        gby.DefaultView.Sort = "number";

        int GrouUserCount = DataConverter.CLng(GetGroupCount(proid));//获得当前参与人数
        if (gby != null && gby.Rows.Count > 0)
        {
            for (int i = 0; i < gby.Rows.Count; i++)
            {
                int townumber = DataConverter.CLng(gby.Rows[i]["number"]);
                if (GrouUserCount > townumber)
                {
                    nowpirce = DataConverter.CDouble(gby.Rows[i]["price"]);
                }
            }
        }

        if (nowpirce > 0)
        {
            return nowpirce.ToString();
        }
        else
        {
            return "--";
        }
    }

    #region 截取字符串
    /// <summary>
    /// 截取字符串
    /// </summary>
    /// <param name="money"></param>
    /// <param name="ProClass"></param>
    /// <param name="point"></param>
    /// <returns></returns>

    public string formatcs(string money, string ProClass, string point)
    {
        string outstr;
        double doumoney, tempmoney;
        doumoney = DataConverter.CDouble(money);
        tempmoney = System.Math.Round(doumoney, 2);
        outstr = tempmoney.ToString();
        if (ProClass != "3")
        {
            if (outstr.IndexOf(".") == -1)
            {
                outstr = outstr + ".00";
            }
        }
        else
        {
            outstr = point;
        }
        return outstr;
    }
    #endregion 


    #region 通用分页过程
    /// <summary>
    /// 通用分页过程　by h.
    /// </summary>
    /// <param name="Cll"></param>
    public void Page_list(DataTable Cll)
    {
        int CPage, temppage = 0;

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

        PagedDataSource cc = new PagedDataSource();
        //Cll.DefaultView.Sort = "PubCreateTime desc";
        cc.DataSource = Cll.DefaultView;
        cc.AllowPaging = true;
        if (Request.QueryString["txtPage"] != null && Request.QueryString["txtPage"] != "")
        {
            cc.PageSize = DataConverter.CLng(Request.QueryString["txtPage"]);
        }
        if (Request.Form["txtPage"] != null && Request.Form["txtPage"] != "")
        {
            cc.PageSize = DataConverter.CLng(Request.Form["txtPage"]);
        }
        cc.CurrentPageIndex = CPage - 1;
        Pagetable.DataSource = cc;
        Pagetable.DataBind();
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

        this.Toppage.Text = "<a href=?Currentpage=0&txtPage=" + cc.PageSize.ToString() + "&proid=" + id + ">首页</a>";
        this.Nextpage.Text = "<a href=?Currentpage=" + nextpagenum.ToString() + "&txtPage=" + cc.PageSize.ToString() + "&proid=" + id + " >上一页</a>";
        this.Downpage.Text = "<a href=?Currentpage=" + downpagenum.ToString() + "&txtPage=" + cc.PageSize.ToString() + "&proid=" + id + " >下一页</a>";
        this.Endpage.Text = "<a href=?Currentpage=" + Endpagenum.ToString() + "&txtPage=" + cc.PageSize.ToString() + "&proid=" + id + " >尾页</a>";
        this.Nowpage.Text = CPage.ToString();
        this.PageSize.Text = thispagenull.ToString();
        txtPage.Text = cc.PageSize.ToString();
        DropDownList1.Items.Clear();
        for (int i = 1; i <= thispagenull; i++)
        {
            DropDownList1.Items.Add(i.ToString());
        }
        DropDownList1.SelectedValue = Nowpage.Text;
    }

    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        ViewState["page"] = "1";
        DataTable dt = gll.SelectGroupByProID(DataConverter.CLng(Request.QueryString["proid"]));
        if (dt != null)
        {
            Page_list(dt);
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataTable dt = gll.SelectGroupByProID(DataConverter.CLng(Request.QueryString["proid"]));
        if (dt != null)
        {
            Page_list(dt);
        }
    }
    #endregion 

    #region 读取会员名字
    protected string GetUserName(string userid)
    {
        B_User ull = new B_User();
        return ull.GetUserByUserID(DataConverter.CLng(userid)).UserName;
    }
    #endregion

    #region 读取会员Email
    public string GetUserEmail(string id)
    {
        B_User ull = new B_User();
        return ull.GetUserByUserID(DataConverter.CLng(id)).Email;
    }
    #endregion

    #region 群发
    protected void Button1_Click(object sender, EventArgs e)
    {
        string CID = Request.Form["ItemUId"];
        if (!String.IsNullOrEmpty(CID))
        {
            Response.Write("<script language=javascript>location.href='../User/AddMail.aspx?item=" + Request.Form["ItemUId"] + "';</script>");
        }
        else
        {
            function.WriteErrMsg("未选择用户,请选择!");
        }
    }
    #endregion
    
    #region 批量删除
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button3_Click(object sender, EventArgs e)
    {
        string item = Request.Form["item"];
        if (item != null && item != "")
        {
            if (item.IndexOf(',') > -1)
            {
                string[] itemarr = item.Split(',');
                for (int i = 0; i < itemarr.Length; i++)
                {
                    gll.DeleteByGroupID(DataConverter.CLng(itemarr[i]));
                }
            }
            else
            {
                gll.DeleteByGroupID(DataConverter.CLng(item));
            }
        }
        function.WriteSuccessMsg("操作成功!", "ShowGroupUser.aspx?proid=" + id);
        int proid = DataConverter.CLng(Request.QueryString["proid"]);
        DataTable blist = gll.SelectGroupByProID(proid);
        Page_list(blist);
    }
    #endregion   
    /// <summary>
    /// 添加团购
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button6_Click(object sender, EventArgs e)
    {
        M_GroupBuyList mgbl = new M_GroupBuyList();
   
        mgbl.ProID = DataConverter.CLng(Request.QueryString["proid"]);

        //mgbl.UserID =bu.GetLogin().UserID; 
        M_UserInfo mui=bu.GetUserIDByUserName(this.Name.Text);
        if (mui.UserID <= 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(),"","<script>alert('用户名不存在!');</script>");
            return;
        }    
        mgbl.UserID = mui.UserID; 
        mgbl.Deposit = DataConverter.CDouble(this.ZFMoney.Text);
        if (this.ZFMoney.Text != null && this.ZFMoney.Text != "")
        {
            mgbl.PayID = 1;
        }
        else
        {
            mgbl.PayID = 0;
        }
        mgbl.buymoney =DataConverter.CDouble(this.money.Text);
        if (this.money.Text != null && this.money.Text != "")
        {
            mgbl.isbuy = 1;
        }
        else
        {
            mgbl.isbuy = 0;
        }
        mgbl.Snum =DataConverter.CLng(this.Num.Text);
        mgbl.buytime =DataConverter.CDate(this.ZFTime.Text);
        mgbl.DepositTime =DataConverter.CDate(this.ZFMoneyTime.Text);
        mgbl.Btime = DataConverter.CDate(this.TGTime.Text);
        int i = gll.GetInsert(mgbl);        
        if (i > 0)
        {
            function.WriteSuccessMsg("添加成功!", "ShowGroupUser.aspx?proid=" + mgbl.ProID);
        }
        else
        {
            function.WriteErrMsg("添加失败!");
        }
        

    }
    #region 显示是否收货
    protected void Pagetable_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == (ListItemType.Item | ListItemType.AlternatingItem))
        {
            LinkButton lb = e.Item.FindControl("lbIsReceipt") as LinkButton;
            DataRowView drv = e.Item.DataItem as DataRowView;
            if (lb != null)
            {
                lb.CommandName = drv["id"].ToString();
                if(DataConverter.CLng(drv["IsReceipt"].ToString())==0){
                    lb.Style.Add("color", "blue");
                    lb.Text="确认收货";
                }
                else
                {
                    lb.Style.Add("color", "#999999");
                    lb.Style.Add("text-decoration", "none");
                    lb.Enabled = true;
                    lb.OnClientClick = "return false;";
                    lb.Text="已收货";
                }
            }
        }
    }
    #endregion
    #region 更改收货状态
    public void lbIsReceipt_Click(object sender, EventArgs e)
    {
            LinkButton lb =sender as LinkButton;
            M_GroupBuyList mgbl = gll.GetSelect(DataConverter.CLng(lb.CommandName));
            if (mgbl.id>0)
            {
                mgbl.IsReceipt = 1;
                if (gll.GetUpdate(mgbl))
                {
                    int proid = DataConverter.CLng(Request.QueryString["proid"]);
                    DataTable blist = gll.SelectGroupByProID(proid);
                    Page_list(blist);
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改成功！');</script>");
                }
                else
                {
                    
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改失败！');</script>");
                }
            }
    }
    #endregion

}