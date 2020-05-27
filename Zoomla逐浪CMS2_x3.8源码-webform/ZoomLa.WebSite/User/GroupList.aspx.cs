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


public partial class User_GroupList : System.Web.UI.Page
{
    protected B_User ull = new B_User();
    protected B_Product pro = new B_Product();
    protected B_GroupBuyList gll = new B_GroupBuyList();
    private B_Payment bpay = new B_Payment();
    protected int id = 0;
    public string Start { get { return HttpUtility.HtmlEncode(Request.QueryString["start"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        int proid = DataConverter.CLng(Request.QueryString["proid"]);
        id = proid;
        if (Request.QueryString["menu"] != null) 
        {
            string menu = Request.QueryString["menu"];
            if (menu == "delete")
            {
                int groupid = Convert.ToInt32(Request.QueryString["groupid"]);
                int proids = Convert.ToInt32(Request.QueryString["proid"]);
                gll.DeleteByGroupID(groupid);
                Response.Redirect("GroupList.aspx?start=false");
            }
        }
        M_UserInfo userinfo = ull.GetLogin();
        DataTable blist = gll.SelectGroupByProUserID(userinfo.UserID);
        if (!string.IsNullOrEmpty(Start))
        {
            blist = gll.SelectGroupByDes(userinfo.UserID, DataConverter.CBool(Start));
         
            if (blist != null)
            {
				blist = blist.DefaultView.ToTable();
                Page_list(blist);
            }
        }
    }
    public string GetGroupBuy(string pid)
    {
        M_Payment mpay = bpay.GetPamentByID(DataConverter.CLng(pid));
        if (mpay != null && mpay.PaymentID>0)
        {
            if (mpay.Status == 3 || mpay.Status == 5)
            {
                return "已支付";
            }
            else
            {
                return "未支付";
            }
        }
        else
        {
            return "未支付";
        }
    }

    public string Getproname(string proid)
    {
        int pid = DataConverter.CLng(proid);
        M_Product proinfo = pro.GetproductByid(pid);
        return proinfo.Proname;
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

    #region 通用分页过程
    /// <summary>
    /// 通用分页过程　by h.
    /// </summary>
    /// <param name="Cll"></param>
    public void Page_list(DataTable Cll)
    {
        Pagetable.DataSource = Cll;
        Pagetable.DataBind();
    }

    protected string getactionbutton(string gid)
    {
        int id = DataConverter.CLng(gid);
        M_GroupBuyList ginfo = gll.GetSelect(id);
        string returntxt = "";
        M_Payment mpay = bpay.GetPamentByID(DataConverter.CLng(ginfo.DepositPayID));
        if (mpay != null)
        {
            if (mpay.Status != 3 && mpay.Status != 5)
            {
                returntxt += "<a href=../ColonePay.aspx?GID=" + id + " target=_blank>支付订金</a> <a href=?menu=delete&groupid=" + id + " onclick=\"return confirm('取消订购后无法还原!确定要取消吗?')\">取消团定</a>";
            }
            else
            {
                returntxt += "已付订金 ";
                M_Payment mpays = bpay.GetPamentByID(ginfo.PayID);
                if (mpays != null && mpays.PaymentID > 0)
                {
                    if (mpays.Status != 3 && mpays.Status != 5)
                    {
                        if (ginfo.OrderID <= 0)
                        {
                            returntxt += "<a href=../ColoneOrderCart.aspx?id=" + id + " target=_blank>支付购买</a>";
                        }
                        else
                        {
                            returntxt += "<a href=../UserGroupBuy.aspx?id=" + id + " target=_blank>支付购买</a>";
                        }
                    }
                    else
                    {
                        returntxt += "已购买";
                    }
                }
                else
                {
                    if (ginfo.OrderID <= 0)
                    {
                        returntxt += "<a href=../ColoneOrderCart.aspx?id=" + id + " target=_blank>支付购买</a>";
                    }
                    else
                    {
                        returntxt += "<a href=../UserGroupBuy.aspx?id=" + id + " target=_blank>支付购买</a>";
                    }
                }
            }
        }
        else
        {
            returntxt += "<a href=../ColonePay.aspx?GID=" + id + " target=_blank>支付订金</a> <a href=?menu=delete&groupid=" + id + " onclick=\"return confirm('取消订购后无法还原!确定要取消吗?')\">取消团定</a>";
        }

        //支付订金    支付购买
        return returntxt;
    }

    protected string GetUserName(string userid)
    {
        B_User ull = new B_User();
        return ull.GetUserByUserID(DataConverter.CLng(userid)).UserName;
    }
    #endregion
}