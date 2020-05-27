using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Data;

public partial class User_producter_ProducterManage : System.Web.UI.Page
{
    B_OrderList boll = new B_OrderList();
    private B_User buser = new B_User();
    private B_ModelField mll = new B_ModelField();
    private B_CartPro cll = new B_CartPro();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!buser.CheckLogin())
            {
                function.WriteErrMsg("请先登录！", "/User/Login.aspx");

            }
            else
            {
                string menu = Request.QueryString["menu"] == null ? "0" : Request.QueryString["menu"].ToString();

     

                //this.LblSiteName.Text = SiteConfig.SiteInfo.SiteName;
                M_UserInfo uinfo = buser.GetLogin();
                DataTable cmdinfo = mll.SelectTableName("ZL_CommonModel", "TableName like 'ZL_Reg_%' and Inputer='" + uinfo.UserName + "'");

                if (cmdinfo.Rows.Count > 0)
                {
                    string tablename = cmdinfo.Rows[0]["TableName"].ToString();
                    DataTable cc = new DataTable();
                    cc= buser.GetUserModeInfo(tablename, uinfo.UserID, 19);
                    if (cc!=null)
                    {
                        DataTable pro = boll.GetProAndOrder(cc.Rows[0]["name"].ToString());
                        Bind(pro);

                        int shu=0;
                        int shu2 = 0;
                        double money = 0;
                        double money2 = 0;
                        foreach(DataRow dd in pro.Rows)
                        {
                            shu = shu + DataConverter.CLng(dd["Pronum"]);
                            money = money + DataConverter.CDouble(dd["diiprice"]) * DataConverter.CLng(dd["Pronum"]);
                            if (DataConverter.CLng(dd["sended"]) == 1)
                            {
                                shu2 = shu2 + DataConverter.CLng(dd["Pronum"]);
                                money2 = money2 + DataConverter.CDouble(dd["diiprice"]) * DataConverter.CLng(dd["Pronum"]);
                            }

                        }
                        this.Label2.Text = "你一共出售" + shu + "件商品。共计" + money + "元";
                        this.Label3.Text = "<span style='color:red;'>你已发出" + shu2 + "件商品。共计" + money2 + "元</span>";
                        this.Label4.Text = "<a href='Cash.aspx?zong=" + money2.ToString() + "'>申请提现</a>";
                     
                    }
                    else
                    {
                        function.WriteErrMsg("没有开通此功能");
                    }
                }
                else
                {
                    function.WriteErrMsg("没有开通此功能");
                }
            }


        }
    }
    private void Bind(DataTable dd)
    {
        int CPage, temppage;

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

        DataTable Cll = dd;

        PagedDataSource cc = new PagedDataSource();
        cc.DataSource = Cll.DefaultView;
        cc.AllowPaging = true;
        cc.PageSize = 12;
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

    public string getsend(object aa)
    {
        int cc = DataConverter.CLng(aa);
        if (cc == 0)
        {
            return "未发货";
        }
        else
        {
            return "<span style='color:red;'>以发货</span>";  
        }

    }

    public string getpay(object Paymentstatus)
    {
        int cc = DataConverter.CLng(Paymentstatus);
        if (cc == 0)
        {
            return "未付款";
        }
        else
        {
            return "<span style='color:red;'>已付款</span>";  
        }

    }
    public string getAllMoney(object aa, object cc)
    {
        string all = "0";
        double price = DataConverter.CDouble(aa);
        int num = DataConverter.CLng(cc);
        all = (price * num).ToString();
        return all;
    }
    
}
