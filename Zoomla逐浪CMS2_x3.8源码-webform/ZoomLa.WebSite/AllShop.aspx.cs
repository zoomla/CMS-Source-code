using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Data;
using System.Xml;
using System.Collections;

public partial class AllShop : System.Web.UI.Page
{
    #region 加载业务逻辑
    protected B_Product bll = new B_Product();
    protected B_User buser = new B_User();
    protected B_CartPro Cll = new B_CartPro();
    protected B_Cart ACl = new B_Cart();
    protected XmlDocument objXmlDoc = new XmlDocument();
    protected string filename;
    protected DataSet fileset = new DataSet();
    protected DataTable infotable;
    protected B_MoneyManage mll = new B_MoneyManage();
    protected B_Product blls = new B_Product();   
    protected B_CartPro Clls = new B_CartPro();
    private B_Card bc = new B_Card();
    protected B_OrderList OCl = new B_OrderList();
    protected B_PayPlat Pll = new B_PayPlat();
    protected string UserName = "";
    private B_CardType bcType = new B_CardType();
    private B_UserRecei buserrecei = new B_UserRecei();
    private B_OrderBaseField borderBaseField = new B_OrderBaseField();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //return;
        //this.HB.Visible = false;
        //this.JZ.Visible = true;
        //this.DD.Visible = false;
        /////////////////////////////////////UserOrderinfo.aspx.cs//////////////////////////
        //#region    
        //this.filename = Server.MapPath("/config/Cartinfo.xml");
        //fileset.ReadXml(this.filename);
        //this.infotable = fileset.Tables[0].DefaultView.Table;

        ////Request.Form[""]
        ////this.prodectid.Value =Request.Form["projuct"];
        ////this.projectjiage.Value = Request.Form["projiectjiage" + Request.Form["projuct"]];
        ////this.projiect.Value = Request.Form["project"];
        ////this.jifen.Value = Request.Form["jifen"];
        ////hfproclass.Value = Request.QueryString["ProClass"];
        //DataTable ptable = Pll.GetPayPlatAll();
        //DataTable newprolist = ptable.DefaultView.ToTable(false, "PayPlatName", "PayPlatID");

        //Hashtable ar = new Hashtable();

        //foreach (DataRow dr in newprolist.Rows)
        //{
        //    ar.Add(dr[1].ToString(), dr[0].ToString());
        //}


        //ModelHtml.Text = borderBaseField.GetHtmlOrder(0).Replace("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">", "<ul><li style='width:150px;text-align:right;'><b>")
        //   .Replace("<td>", "<li style='width:75%'>").Replace("</td>\r\n</tr>\r\n", "</li></ul>").Replace("</td>", "：</b></li>");
        ////Hashtable ars = new Hashtable();
        ////foreach (DataRow drs in Dlist.Rows)
        ////{
        ////    ars.Add(drs[1].ToString(), drs[0].ToString());
        ////}
        //if (!IsPostBack)
        //{
        //    this.Delivery.DataSource = null;
        //    this.Delivery.DataValueField = "id";

        //    this.Delivery.DataTextField = "deliname";
        //    this.Delivery.DataBind();

        //    this.Receiver.Text = buser.GetLogin().UserName;
        //    int userid = buser.GetLogin().UserID;
        //    // Invoice.Text = "";

        //    string city = "辽宁、吉林、黑龙江、河北、山西、陕西、甘肃、青海、山东、安徽、江苏、浙江、河南、湖北、湖南、江西、台湾、福建、云南、海南、四川、贵州、广东、内蒙古、新疆、广西、西藏、宁夏、北京、上海、天津、重庆、香港、澳门";
        //    string[] arrcity = city.Split(new char[] { '、' }, StringSplitOptions.RemoveEmptyEntries);
        //    M_Uinfo mu = buser.GetUserBaseByuserid(userid);
        //    this.Jiedao.Text = mu.Address;
        //    this.RadioButtonList1.Items[0].Text = mu.Address;

        //    GetYunFei(DataConverter.CLng(Delivery.SelectedValue));
        //    Binddddizhi();
        //}
        //#endregion
        ///////////////////////////////////////////////////////////////////////////////////

        //#region 获得提交过来的参数
        //string UserName = buser.GetLogin().UserName;

        //#region 读取XML说明数据
        //this.filename = Server.MapPath("/config/Cartinfo.xml");
        //fileset.ReadXml(this.filename);
        //this.infotable = fileset.Tables[0].DefaultView.Table;
        //string message1 = infotable.Rows[0]["message1"].ToString();
        //string message2 = infotable.Rows[0]["message2"].ToString();
        //string message3 = infotable.Rows[0]["message3"].ToString();
        //string message4 = infotable.Rows[0]["message4"].ToString();
        //string message5 = infotable.Rows[0]["message5"].ToString();
        //string message6 = infotable.Rows[0]["message6"].ToString();
        //string message7 = infotable.Rows[0]["message7"].ToString();
        //string message8 = infotable.Rows[0]["message8"].ToString();
        //string message9 = infotable.Rows[0]["message9"].ToString();
        //string message10 = infotable.Rows[0]["message10"].ToString();
        //string message11 = infotable.Rows[0]["message11"].ToString();
        //string message12 = infotable.Rows[0]["message12"].ToString();
        //string message13 = infotable.Rows[0]["message13"].ToString();
        //string message14 = infotable.Rows[0]["message14"].ToString();
        //string message15 = infotable.Rows[0]["message15"].ToString();
        //string message16 = infotable.Rows[0]["message16"].ToString();
        //string message17 = infotable.Rows[0]["message17"].ToString();
        //string message18 = infotable.Rows[0]["message18"].ToString();
        //string message19 = infotable.Rows[0]["message19"].ToString();
        //string message20 = infotable.Rows[0]["message20"].ToString();
        //string message21 = infotable.Rows[0]["message21"].ToString();
        //string message30 = infotable.Rows[0]["message30"].ToString();
        //string message31 = infotable.Rows[0]["message31"].ToString();
        //string message32 = infotable.Rows[0]["message32"].ToString();
        //#endregion
        //#region 定义参数
        //int jifen = 0;
        //string meinfo = "";
        //string mestr = "";
        //#endregion

        //#endregion
        //#region 提交过程
        //ProClass.Value = string.IsNullOrEmpty(Request.Form["ProClass"]) ? Request.QueryString["ProClass"] : Request.Form["ProClass"];
        //if (!string.IsNullOrEmpty(Request.Form["Stock"]) && !string.IsNullOrEmpty(Request.Form["Proname"]) && !string.IsNullOrEmpty(Request.Form["Proinfo"]) && !string.IsNullOrEmpty(Request.Form["Wholesaleone"]) && !string.IsNullOrEmpty(Request.Form["ProUrl"]) && !string.IsNullOrEmpty(Request.Form["ProSeller"]))
        //{
        //    if (!string.IsNullOrEmpty(Request.Form["Type"]) && DataConverter.CLng(Request.Form["Type"]) == 2)
        //    {
        //        lblProinfo.Text = "燃油机械费";
        //    }
        //    int id = SetProduct();//添加商品
        //    if (id > 0 && !(string.IsNullOrEmpty(Request.Form["Type"])))
        //    {
        //        this.projuct.Value = id.ToString();
        //    }
        //    int Pronum = DataConverter.CLng(Request.Form["Stock"]);
        //    //string prolist = (Request.Form["prolist"] == null) ? "" : Request.Form["prolist"];
        //    //int id = DataConverter.CLng(Request.Form["id"]);
        //    string messages = Request.QueryString["message"];

        //    #region 判断是否匿名购物
        //    if (UserName == null)
        //    {
        //        UserName = message32;
        //    }
        //    #endregion

        //    #region 显示信息提示
        //    if (!string.IsNullOrEmpty(messages))
        //    {
        //        Label1.Text = messages;
        //    }
        //    #endregion

        //    if (Pronum > 0 && id > 0 && bll.GetproductByid(id).ID > 0)
        //    {
        //        #region 业务实体
        //        M_Product proinfo = bll.GetproductByid(id);
        //        M_Cart OData = new M_Cart();
        //        M_CartPro Cdata = new M_CartPro();
        //        M_Product Pdata = new M_Product();
        //        #endregion

        //        int Propeidsnum = proinfo.Propeid;//重载自选数量

        //        #region 初始化数据
        //        int Cid;
        //        string Cartusername;
        //        string Cartcartid;
        //        int Cartuserid;
        //        DateTime Cartaddtime;
        //        string Cartgetip;
        //        double Cartallmoney;
        //        int Cartpronum;
        //        int Userid = 0;
        //        if (UserName != message32)
        //        {
        //            M_UserInfo Nameinfo = buser.GetUserByName(UserName);
        //            Userid = Nameinfo.UserID;
        //        }
        //        #endregion

        //        #region 生成购物车编号

        //        if (HttpContext.Current.Request.Cookies["Shopby"] == null || HttpContext.Current.Request.Cookies["Shopby"]["OrderNo"] == null || HttpContext.Current.Request.Cookies["Shopby"]["OrderNo"] == "")
        //        {
        //            M_Cart ca = ACl.GetCartByUserId(Userid);
        //            if (ca != null && ca.ID > 0)
        //            {
        //                string ordNo = ca.Cartid;
        //                HttpContext.Current.Response.Cookies["Shopby"]["OrderNo"] = ordNo;
        //            }
        //        }

        //        if (HttpContext.Current.Request.Cookies["Shopby"] == null || HttpContext.Current.Request.Cookies["Shopby"]["OrderNo"] == null || HttpContext.Current.Request.Cookies["Shopby"]["OrderNo"] == "")
        //        {
        //            string OrderNo = "";
        //            OrderNo = "CT" + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Millisecond);
        //            string TOrderNo = OrderNo.ToString();
        //            string aaa = StringHelper.MD5A(TOrderNo, 10, 6);
        //            string nowtime = DateTime.Now.ToString();
        //            string bbb = StringHelper.MD5A(nowtime, 5, 10);
        //            string ccc = StringHelper.MD5A(UserName, 5, 10);
        //            string cartid = aaa + "-" + bbb + "-" + ccc;
        //            HttpContext.Current.Response.Cookies["Shopby"]["OrderNo"] = cartid;
        //        }

        //        string Cartno = HttpContext.Current.Request.Cookies["Shopby"]["OrderNo"];//重载
        //        #endregion

        //        #region 处理购物车数据(存在则更新，不存在则添加)
        //        /////添加判断是否存在此ID;
        //        DataTable Cart = ACl.FondCartno(Cartno);

        //        if (Cart.Rows.Count < 1)
        //        /////如果不存在
        //        {
        //            //////添加购物车///////
        //            OData.ID = 0;
        //            OData.Username = UserName.ToString();
        //            OData.Cartid = Cartno.ToString();
        //            OData.userid = Userid;
        //            OData.Addtime = DateTime.Now;
        //            OData.Getip = getIP();
        //            OData.AllMoney = 0;
        //            OData.Pronum = 0;

        //            ACl.Add(OData);

        //            DataTable Cartend = ACl.FondCartno(Cartno);
        //            Cid = DataConverter.CLng(Cartend.Rows[0]["id"].ToString());

        //            Cartusername = Cartend.Rows[0]["Username"].ToString();
        //            Cartcartid = Cartend.Rows[0]["Cartid"].ToString();
        //            Cartuserid = DataConverter.CLng(Cartend.Rows[0]["Userid"].ToString());
        //            Cartaddtime = DataConverter.CDate(Cartend.Rows[0]["Addtime"].ToString());
        //            Cartgetip = Cartend.Rows[0]["Getip"].ToString();
        //            Cartallmoney = DataConverter.CDouble(Cartend.Rows[0]["AllMoney"].ToString());
        //            Cartpronum = DataConverter.CLng(Cartend.Rows[0]["Pronum"].ToString());
        //            ///////////////////////
        //        }
        //        ////否则
        //        else
        //        {
        //            //读取数据
        //            Cid = DataConverter.CLng(Cart.Rows[0]["id"].ToString());
        //            Cartusername = Cart.Rows[0]["Username"].ToString();
        //            Cartcartid = Cart.Rows[0]["Cartid"].ToString();
        //            Cartuserid = DataConverter.CLng(Cart.Rows[0]["Userid"].ToString());
        //            Cartaddtime = DataConverter.CDate(Cart.Rows[0]["Addtime"].ToString());
        //            Cartgetip = Cart.Rows[0]["Getip"].ToString();
        //            Cartallmoney = DataConverter.CDouble(Cart.Rows[0]["AllMoney"].ToString());
        //            Cartpronum = DataConverter.CLng(Cart.Rows[0]["Pronum"].ToString());

        //            //读取购物车ID
        //        }
        //        //////////////////以上为购物车信息////////////////
        //        #endregion

        //        double jiage = 0;
        //        double alljia = 0;
        //        int shuliang = Pronum;//重载

        //        jiage = proinfo.LinPrice;
        //        if (proinfo.ProClass == 3)
        //        {
        //            jiage = proinfo.PointVal;
        //        }

        //        jiage = jiage - jiage * ((double)proinfo.Recommend / 100);

        //        #region 计算税率价格
        //        double tempjia = jiage * shuliang;
        //        int sl = proinfo.Rateset;//税率计算方式
        //        double Rate = proinfo.Rate;//点数
        //        switch (sl)
        //        {
        //            case 1://含税，不开发票时有税率优惠
        //                alljia = tempjia - tempjia * (Rate / 100);
        //                break;
        //            case 2://含税，不开发票时没有税率优惠
        //                alljia = tempjia;
        //                break;
        //            case 3://不含税，开发票时需要加收税费
        //                alljia = tempjia + tempjia * (Rate / 100);
        //                break;
        //            case 4://不含税，开发票时不需要加收税费
        //                alljia = tempjia;
        //                break;
        //            default:
        //                break;
        //        }
        //        #endregion

        //        #region 单件商品实体数据定义
        //        //单件商品信息
        //        Cdata.Addtime = DateTime.Now;
        //        Cdata.CartID = Cid; //购物车ID
        //        Cdata.ProID = proinfo.ID;
        //        Cdata.Pronum = shuliang;///////数量
        //        Cdata.Shijia = jiage;   //////不含税点的价格
        //        Cdata.Danwei = proinfo.ProUnit;
        //        Cdata.Proname = proinfo.Proname;
        //        Cdata.AllMoney = alljia;
        //        Cdata.Username = UserName;
        //        Cdata.Bindpro = "";//绑定的商品
        //        Cdata.ProSeller = proinfo.ProSeller;//

        //        #endregion
        //        int kucun = proinfo.Stock;

        //        //允许单独购买
        //        Cll.Add(Cdata);
        //        bll.ProUpStock(proinfo.ID, kucun);
        //        OData.ID = Cid;
        //        OData.Username = Cartusername.ToString();
        //        OData.Cartid = Cartcartid.ToString();
        //        OData.userid = DataConverter.CLng(Cartuserid);
        //        OData.Addtime = Cartaddtime;
        //        OData.Getip = getIP();
        //        OData.AllMoney = Cartallmoney + alljia;
        //        OData.Pronum = Cartpronum + shuliang;
        //        ACl.Update(OData);
        //        meinfo = message3 + mestr + message5;
        //    }
        //    else
        //    {
        //        #region 信息提示的显示
        //        //if (string.IsNullOrEmpty(messages))
        //        //{
        //        //    Button1.Enabled = false;
        //        //    Label1.Text = message12;
        //        //}

        //        #endregion
        //    }
        //}
        //#endregion

        //#region 查询上次未提交的购物车
        //if (HttpContext.Current.Request.Cookies["Shopby"] == null || HttpContext.Current.Request.Cookies["Shopby"]["OrderNo"] == null || HttpContext.Current.Request.Cookies["Shopby"]["OrderNo"] == "")
        //{
        //    M_Cart ca = ACl.GetCartByUserId(buser.GetLogin().UserID);
        //    if (ca != null && ca.ID > 0)
        //    {
        //        string ordNo = ca.Cartid;
        //        HttpContext.Current.Response.Cookies["Shopby"]["OrderNo"] = ordNo;
        //    }

        //}
        //#endregion
        //if (HttpContext.Current.Request.Cookies["Shopby"] != null && HttpContext.Current.Request.Cookies["Shopby"]["OrderNo"] != null)
        //{
        //    if (!string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["Shopby"]["OrderNo"].ToString()))
        //    {
        //        #region 定义分页数据
        //        int CPage = 0, temppage;

        //        if (Request.Form["DropDownList1"] != null)
        //        {
        //            temppage = Convert.ToInt32(Request.Form["DropDownList1"]);
        //        }
        //        else
        //        {
        //            temppage = Convert.ToInt32(Request.QueryString["CurrentPage"]);
        //        }

        //        if (temppage > 0)
        //        {
        //            CPage = temppage;
        //        }

        //        if (CPage <= 0)
        //        {
        //            CPage = 1;
        //        }
        //        #endregion

        //        #region 显示商品列表
        //        //显示商品列表;
        //        //cartinfo.DataSource
        //        string Cartnod = HttpContext.Current.Request.Cookies["Shopby"]["OrderNo"].ToString();
        //        /////添加判断是否存在此ID;

        //        DataTable Cartlist = ACl.FondCartno(Cartnod);

        //        if (Cartlist.Rows.Count > 0)
        //        {
        //            int cartids = DataConverter.CLng(Cartlist.Rows[0]["id"]);//获得购物车ID
        //            DataTable cplist = Cll.GetCartProCartID(cartids);

        //            PagedDataSource cc = new PagedDataSource();
        //            cc.DataSource = cplist.DefaultView;

        //            cc.AllowPaging = true;
        //            cc.PageSize = 10;
        //            cc.CurrentPageIndex = CPage - 1;
        //            cartinfo.DataSource = cc;
        //            cartinfo.DataBind();

        //            Allnum.Text = cplist.DefaultView.Count.ToString();
        //            int thispagenull = cc.PageCount;//总页数
        //            int CurrentPage = cc.CurrentPageIndex;
        //            int nextpagenum = CPage - 1;//上一页
        //            int downpagenum = CPage + 1;//下一页
        //            int Endpagenum = thispagenull;
        //            if (thispagenull <= CPage)
        //            {
        //                downpagenum = thispagenull;
        //                Downpage.Enabled = false;
        //            }
        //            else
        //            {
        //                Downpage.Enabled = true;
        //            }
        //            if (nextpagenum <= 0)
        //            {
        //                nextpagenum = 0;
        //                Nextpage.Enabled = false;
        //            }
        //            else
        //            {
        //                Nextpage.Enabled = true;
        //            }
        //            Toppage.Text = "<a href=?Currentpage=0>" + message13 + "</a>";
        //            Nextpage.Text = "<a href=?Currentpage=" + nextpagenum.ToString() + ">" + message14 + "</a>";
        //            Downpage.Text = "<a href=?Currentpage=" + downpagenum.ToString() + ">" + message15 + "</a>";
        //            Endpage.Text = "<a href=?Currentpage=" + Endpagenum.ToString() + ">" + message16 + "</a>";
        //            Nowpage.Text = CPage.ToString();
        //            PageSize.Text = thispagenull.ToString();
        //            pagess.Text = cc.PageSize.ToString();


        //            if (!this.Page.IsPostBack)
        //            {
        //                for (int i = 1; i <= thispagenull; i++)
        //                {
        //                    DropDownList1.Items.Add(i.ToString());
        //                }
        //            }
        //            int ccsd = cplist.Rows.Count;
        //            double ww = 0;
        //            string endjiage;

        //            for (int i = 0; i < ccsd; i++)
        //            {
        //                int tempnum = DataConverter.CLng(cplist.Rows[i]["Pronum"].ToString());
        //                ww = ww + Convert.ToDouble(cplist.Rows[i]["AllMoney"]);
        //            }
        //            ww = System.Math.Round(ww, 2);
        //            endjiage = ww.ToString();

        //            if (endjiage.IndexOf(".") == -1)
        //            {
        //                endjiage = endjiage + ".00";
        //            }
        //            this.alljiage.Text = endjiage.ToString();



        //            //遍历购物车中的所有商品,查找所有的促销设置
        //            string otherprojectinfo = "";
        //            string retnss = "";
        //            string allpresetlist = "";
        //            string allproductlist = "";
        //            for (int oo = 0; oo < cplist.Rows.Count; oo++)
        //            {
        //                #region 遍历购物车所有商品
        //                int readproid = DataConverter.CLng(cplist.Rows[oo]["ProID"].ToString());
        //                M_Product readproinfo = bll.GetproductByid(readproid);
        //                int ProjectTypes = readproinfo.ProjectType;

        //                switch (ProjectTypes)
        //                {
        //                    case 1:
        //                        //不促销
        //                        break;
        //                    case 2:
        //                        otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　买 " + readproinfo.ProjectPronum.ToString() + " 件" + readproinfo.Proname + " 可以送一件" + readproinfo.Proname + "</font></ul>";
        //                        break;
        //                    case 3:
        //                        otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　买 " + readproinfo.ProjectPronum.ToString() + " 件" + readproinfo.Proname + " 可以送一件" + readproinfo.PesentNames + "</font></ul>";
        //                        break;
        //                    case 4:
        //                        otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　送 " + readproinfo.ProjectPronum.ToString() + " 件同样商品:" + readproinfo.Proname + "</font></ul>";
        //                        break;
        //                    case 5:
        //                        otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　送 " + readproinfo.ProjectPronum.ToString() + " 件" + readproinfo.PesentNames + "</font></ul>";
        //                        break;
        //                    case 6:
        //                        otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　加 " + readproinfo.ProjectMoney.ToString() + " 元钱 送一件" + readproinfo.PesentNames + "</font></ul>";
        //                        break;
        //                    case 7:
        //                        otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　满 " + readproinfo.ProjectMoney.ToString() + " 元钱 送一件" + readproinfo.PesentNames + "</font></ul>";
        //                        break;
        //                }
        //                if (readproinfo.IntegralNum > 0 && readproinfo.IntegralNum <= DataConverter.CLng(cplist.Rows[oo]["Pronum"].ToString()))
        //                {
        //                    otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　购买" + readproinfo.IntegralNum.ToString() + "件 " + readproinfo.Proname + " 可以得到 " + readproinfo.Integral.ToString() + " 积分</font></ul>";

        //                    jifen = jifen + (DataConverter.CLng(cplist.Rows[oo]["Pronum"]) / readproinfo.IntegralNum) * readproinfo.Integral;
        //                }

        //                //获得促销方案

        //                B_Promotions ption = new B_Promotions();
        //                string Presetinfo = readproinfo.Preset;

        //                string[] Presetinfoarr = Presetinfo.Split(new string[] { "," }, StringSplitOptions.None);
        //                string retns = "";


        //                for (int ccc = 0; ccc < Presetinfoarr.Length; ccc++)
        //                {
        //                    string tempallpresetlist = "," + allpresetlist + ",";
        //                    //不重复方案筛选
        //                    if (tempallpresetlist.IndexOf("," + Presetinfoarr[ccc].ToString() + ",") == -1)
        //                    {
        //                        allpresetlist = allpresetlist + Presetinfoarr[ccc].ToString() + ",";
        //                        M_Promotions Promotionsinfo = ption.GetPromotionsByid(DataConverter.CLng(Presetinfoarr[ccc].ToString()));
        //                        double infoPricetop = Promotionsinfo.Pricetop;//价格上限
        //                        double infoPriceend = Promotionsinfo.Priceend;//价格下限
        //                        int infoGetPresent = Promotionsinfo.GetPresent;//可以得到礼品
        //                        double infoPresentmoney = Promotionsinfo.Presentmoney;//可以以？金钱换购商品
        //                        int infoIntegralTure = Promotionsinfo.IntegralTure;//可以得到积分
        //                        int infoIntegral = Promotionsinfo.Integral;//积分
        //                        string infoPromoProlist = Promotionsinfo.PromoProlist;//礼品列表
        //                        DateTime infoPromostart = Promotionsinfo.Promostart;//起始时间
        //                        DateTime infoPromoend = Promotionsinfo.Promoend;//结束时间

        //                        if (DateTime.Now > infoPromostart && DateTime.Now < infoPromoend)
        //                        {
        //                            if (ww > infoPricetop && ww < infoPriceend)
        //                            {
        //                                if (!string.IsNullOrEmpty(infoPromoProlist))
        //                                {
        //                                    retns = "可以获得下礼品中任一款";
        //                                    if (!string.IsNullOrEmpty(infoPromoProlist))
        //                                    {
        //                                        if (infoPromoProlist.IndexOf(",") > -1)
        //                                        {
        //                                            string[] newlist = infoPromoProlist.Split(new string[] { "," }, StringSplitOptions.None);
        //                                            {
        //                                                for (int ibc = 0; ibc < newlist.Length; ibc++)
        //                                                {
        //                                                    string tempprolistsd = "," + allproductlist + ",";
        //                                                    if (tempprolistsd.IndexOf("," + newlist[ibc].ToString() + ",") == -1)
        //                                                    {
        //                                                        allproductlist = allproductlist + newlist[ibc].ToString() + ",";
        //                                                    }
        //                                                }
        //                                            }
        //                                        }
        //                                        else
        //                                        {
        //                                            string tempprolistsd = "," + allproductlist + ",";
        //                                            if (tempprolistsd.IndexOf("," + infoPromoProlist + ",") == -1)
        //                                            {
        //                                                allproductlist = allproductlist + infoPromoProlist + ",";
        //                                            }
        //                                        }
        //                                    }


        //                                }

        //                                if (infoPresentmoney > 0 && infoGetPresent == 1)
        //                                {
        //                                    retns = "可以" + string.Format("{0:C}", infoPresentmoney) + "元超值换购以下礼品中任一款";
        //                                }

        //                                if (infoIntegralTure == 1)
        //                                {
        //                                    if (!string.IsNullOrEmpty(retns)) { retns = retns + "并"; }
        //                                    retns = retns + "获得 " + infoIntegral.ToString() + " 积分";
        //                                    jifen = jifen + infoIntegral;
        //                                }
        //                                if (!string.IsNullOrEmpty(retns))
        //                                {
        //                                    retnss += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　" + infoPricetop + "≤订单总价格＜" + infoPriceend + " 可享受 [" + Promotionsinfo.Promoname + "]: " + retns + "</font></ul>";
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                #endregion
        //            }

        //            this.project.Value = allpresetlist;
        //            this.jifen.Value = jifen.ToString();
        //        }
        //        #endregion
        //    }
        //}

        //string menu = Request.QueryString["menu"];
        //int sid = DataConverter.CLng(Request.QueryString["cid"]);
        //switch (menu)
        //{
        //    case "del":
        //        #region 删除购物车内商品
        //        M_CartPro Ts = Cll.GetCartProByid(sid);
        //        int ccid = Ts.CartID;
        //        int ccnum = Ts.Pronum;
        //        int ppid = Ts.ProID;
        //        M_Cart Ats = ACl.GetCartByid(ccid);
        //        int num = Ats.Pronum;
        //        int stocknum = num + ccnum;
        //        //M_Product Pts = bll.GetproductByid(ppid);
        //        //int ppnum = Pts.Stock + ccnum;
        //        //bll.ProUpStock(ppid, ppnum);//更新产品
        //        int tempjiages = num - ccnum;
        //        if (tempjiages < 0)
        //        {
        //            tempjiages = 0;
        //        }
        //        M_Cart Ctss = new M_Cart();
        //        Ctss.ID = Ats.ID;
        //        Ctss.Username = Ats.Username;
        //        Ctss.Cartid = Ats.Cartid;
        //        Ctss.userid = Ats.userid;
        //        Ctss.Addtime = Ats.Addtime;
        //        Ctss.Getip = getIP();
        //        Ctss.AllMoney = Ats.AllMoney - Ts.AllMoney;
        //        Ctss.Pronum = tempjiages;
        //        ACl.Update(Ctss);
        //        Cll.DeleteByID(sid);
        //        meinfo = message17 + sid + message19;
        //        Response.Redirect("AllShop.aspx?message=" + meinfo + "&ProClass=" + ProClass.Value);
        //        break;
        //        #endregion
        //    case "edit":
        //        break;
        //    case "editok":
        //        meinfo = message18 + sid + message19;
        //        Response.Redirect("AllShop.aspx?message=" + meinfo + "&ProClass=" + ProClass.Value);
        //        break;
        //    default:
        //        break;
        //}
    }

    #region 方法定义
    /// <summary>
    /// 获取Ip地址
    /// </summary>
    /// <returns></returns>
    public string getIP()
    {
        string result = String.Empty;
        result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (null == result || result == String.Empty)
        {
            result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        if (null == result || result == String.Empty)
        {
            result = HttpContext.Current.Request.UserHostAddress;
        }
        if (null == result || result == String.Empty)
        {
            return "0.0.0.0";
        }
        return result;

    }


    /// <summary>
    /// 获取商品图片
    /// </summary>
    /// <param name="proid"></param>
    /// <returns></returns>
    public string getproimg(string proid)
    {
        int pid = DataConverter.CLng(proid);
        M_Product pinfo = bll.GetproductByid(pid);
        string restring, type;
        restring = "";
        type = pinfo.Thumbnails;
        if (!string.IsNullOrEmpty(type)) { type = type.ToLower(); }
        if (!string.IsNullOrEmpty(type) && (type.IndexOf(".gif") > -1 || type.IndexOf(".jpg") > -1 || type.IndexOf(".png") > -1))
        {
            restring = "<img src=../../" + type + " width=60 height=45>";
        }
        else
        {
            restring = "<img src=../../UploadFiles/nopic.gif width=60 height=45>";
        }
        return restring;

    }

    /// <summary>
    /// 获得商品类型是否绑定商品
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string GetProtype(string id)
    {
        int sid = DataConverter.CLng(id);
        if (bll.GetproductByid(sid).Priority == 1 && bll.GetproductByid(sid).Propeid > 0)
        {
            return "<font color=green>[绑]</font>";
        }
        else
        {
            return "";
        }
    }


    public string Getprojectjia(string proid)
    {
        B_Promotions pos = new B_Promotions();
        string projectlist = this.project.Value;
        projectlist = projectlist.TrimEnd(new char[] { ',' });
        projectlist = projectlist.TrimStart(new char[] { ',' });
        string retrunstr = "";
        if (!string.IsNullOrEmpty(projectlist))
        {
            retrunstr = "<select name=\"projiectjiage" + proid + "\">";
            if (projectlist.IndexOf(",") > -1)
            {
                string[] dd = projectlist.Split(new string[] { "," }, StringSplitOptions.None);
                for (int ii = 0; ii < dd.Length; ii++)
                {
                    M_Promotions prosinfo = pos.GetPromotionsByid(DataConverter.CLng(dd[ii].ToString()));
                    string PromoProlist = "," + prosinfo.PromoProlist + ",";
                    if (PromoProlist.IndexOf("," + proid + ",") > -1)
                    {
                        retrunstr = retrunstr + "<option value=" + prosinfo.Presentmoney.ToString() + ">" + string.Format("{0:C}", prosinfo.Presentmoney) + "</option>";
                    }
                }
            }
            else
            {
                M_Promotions prosinfo = pos.GetPromotionsByid(DataConverter.CLng(projectlist));
                string PromoProlist = "," + prosinfo.PromoProlist + ",";
                if (PromoProlist.IndexOf("," + proid + ",") > -1)
                {
                    retrunstr = retrunstr + "<option value=" + prosinfo.Presentmoney.ToString() + ">" + string.Format("{0:C}", prosinfo.Presentmoney) + "</option>";
                }
            }
            retrunstr = retrunstr + "</select>";
        }
        return retrunstr;
    }

    public string getProUnit(string proid)
    {
        int pid = DataConverter.CLng(proid);
        M_Product pinfo = bll.GetproductByid(pid);
        return pinfo.ProUnit;
    }
    /// <summary>
    /// 商品备注
    /// </summary>
    /// <param name="proid"></param>
    /// <returns></returns>
    public string getProinfo(string proid)
    {
        int pid = DataConverter.CLng(proid);
        M_Product pinfo = bll.GetproductByid(pid);
        return pinfo.Proinfo;
    }

    public string getjiage(string type, string proid)
    {
        int pid = DataConverter.CLng(proid);
        int ptype = DataConverter.CLng(type);
        M_Product pinfo = bll.GetproductByid(pid);
        string jiage;
        jiage = "";

        if (type == "1")
        {
            double jia = System.Math.Round(pinfo.ShiPrice, 2);
            jiage = jia.ToString();

        }
        else if (type == "2")
        {
            double jia = System.Math.Round(pinfo.LinPrice, 2);
            jiage = jia.ToString();
        }
        if (jiage.IndexOf(".") == -1)
        {
            jiage = jiage + ".00";
        }
        if (type == "3")
        {
            jiage = pinfo.PointVal.ToString();
        }
        return jiage;
    }

    public string getShu(string num)
    {
        return num;
    }

    public string formatnewstype(string proid)
    {
        int pid = DataConverter.CLng(proid);
        M_Product pinfo = bll.GetproductByid(pid);
        int type = pinfo.ProClass;
        int newtype;
        string restring;

        restring = "";
        newtype = DataConverter.CLng(type.ToString());

        if (newtype == 2)
        {
            restring = "<font color=red>" + this.infotable.Rows[0]["message20"].ToString() + "</font>";
        }
        else if (newtype == 1)
        {
            restring = "<font color=blue>" + this.infotable.Rows[0]["message21"].ToString() + "</font>";
        }
        else if (newtype == 3)
        {
            restring = "<font color=blue>" + this.infotable.Rows[0]["message33"].ToString() + "</font>";
        }
        return restring;
    }

    #region 打折信息
    public string getproscheme(string cid)
    {
        int pid = DataConverter.CLng(cid);
        M_CartPro minfo = Cll.GetCartProByid(pid);
        M_Product proinfo = bll.GetproductByid(minfo.ProID);
        B_Scheme bs = new B_Scheme();
        B_SchemeInfo bsi = new B_SchemeInfo();
        DataTable dt = new DataTable();
        int sch = 100;
        //根据商品D查询商品打折信息
        DataTable Sdt = bs.GetID(proinfo.ID.ToString());

        if (Sdt.Rows.Count > 0)
        {
            dt = bsi.SelectAgioList(Sdt.Rows[0]["ID"].ToString(), minfo.Pronum);
        }
        else
        {
            //根据模型ID查询商品打折信息
            Sdt = bs.GetID(proinfo.ModelID.ToString());
            if (Sdt.Rows.Count > 0)
                dt = bsi.SelectAgioList(Sdt.Rows[0]["ID"].ToString(), minfo.Pronum);
        }
        if (dt.Rows.Count > 0)
        {
            sch = DataConverter.CLng(dt.Rows[0]["SIAgio"].ToString());
        }
        return (sch / 10).ToString();
    }
    #endregion

    public string getprojia(string cid)
    {
        int pid = DataConverter.CLng(cid);
        string jiage = "";
        M_CartPro minfo = Cll.GetCartProByid(pid);
        M_Product proinfo = bll.GetproductByid(minfo.ProID);
        double dd = DataConverter.CDouble(proinfo.Rate);
        double tempdd = (double)1 / 100;

        double jiag = System.Math.Round(minfo.Shijia * minfo.Pronum + minfo.Shijia * (double)(minfo.Pronum) * dd * tempdd, 2);

        #region 打折信息
        double dazhe = double.Parse(getproscheme(cid));
        jiag = jiag * (dazhe * 0.1);
        #endregion

        jiage = jiag.ToString();
        if (jiage.IndexOf(".") == -1)
        {
            jiage = jiage + ".00";
        }
        return jiage;
    }

    public string shijia(string jiage)
    {
        double jia;
        jia = DataConverter.CDouble(jiage);
        double jiag = System.Math.Round(jia, 2);
        string jj = jiag.ToString();

        if (jj.IndexOf(".") == -1)
        {
            jj = jj + ".00";
        }
        return jj;
    }
    #endregion

    #region
    protected B_ModelField bfield = new B_ModelField();
    protected B_Model bmode = new B_Model();
    protected B_Node bnode = new B_Node();
    //private int SetProduct()
    //{
       

    //}


    #endregion


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (DataConverter.CLng(ProClass.Value) == 3)
        {
            B_User.CheckIsLogged(Request.RawUrl);
            M_UserInfo mu = buser.GetLogin(false);
            double point = mu.UserExp;
            if (DataConverter.CDouble(alljiage.Text) > point)
            {
                function.WriteErrMsg("对不起,您积分不够,赶快去充值吧！");
            }
            else
            {
                this.HB.Visible = true;
                this.JZ.Visible = false;
                Response.Redirect("/User/Shopfee/UserOrderinfo.aspx?ProClass=" + ProClass.Value);
            }
        }
        else
        {
            this.HB.Visible = true;
            this.JZ.Visible = false;
            //Response.Redirect("AllShop.aspx?ProClass=" + ProClass.Value);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        this.DD.Visible = true;
        this.JZ.Visible = false;
        //Response.Redirect("AllShop.aspx?ProClass=" + ProClass.Value);
    }



    #region UserOrderinfo.aspx.cs
    ///////////////////////////////////////UserOrderinfo.aspx.cs////////////////////////////
        private void GetYunFei(int id)
        {
        }

        private void Binddddizhi()
        {
            M_UserInfo info = buser.GetLogin();
            if (info != null && info.UserID > 0)
            {
                int sid = 0;
                dddizhi.Visible = true;
                DataTable dt = buserrecei.Select_userID(info.UserID, -1);
                if (dt != null && dt.Rows.Count > 0)
                {
                    sid = DataConverter.CLng(dt.Rows[0]["id"].ToString());
                    foreach (DataRow dr in dt.Rows)
                    {
                        ListItem li = new ListItem();
                        li.Value = dr["id"].ToString();
                        li.Text = dr["Street"].ToString();
                        if (dr["isDefault"].ToString().Trim() == "0")
                        {
                            sid = DataConverter.CLng(dr["id"].ToString());
                        }
                        dddizhi.Items.Add(li);
                    }
                }
                M_UserRecei rec = buserrecei.GetSelect(sid);
                if (rec != null && rec.ID > 0)
                {
                    dddizhi.SelectedValue = sid.ToString();
                    Receiver.Text = rec.ReceivName;
                    string[] pa = rec.Provinces.Split(' ');
                    string pro = "";
                    if (pa != null && pa.Length > 1)
                    {
                        pro = pa[0] + "省" + pa[1] + "市";
                    }
                    Jiedao.Text = pro + rec.Street;
                    Email.Text = rec.Email;
                    if (!string.IsNullOrEmpty(rec.phone) && rec.phone.Trim() != "-" && rec.phone.Trim() != "--")
                    {
                        Phone.Text = rec.phone;
                    }
                    else if (!string.IsNullOrEmpty(rec.MobileNum))
                    {
                        Phone.Text = rec.MobileNum;
                    }
                    ZipCode.Text = rec.Zipcode;
                }

            }
            else
            {
                dddizhi.Visible = false;
            }
        }



        /// <summary>
        /// 加入我的地址薄
        /// </summary>
        public void Address()
        {
            if (cbAdd.Checked)
            {
                M_UserInfo info = buser.GetLogin();
                if (info != null)
                {
                    M_UserRecei userrec = new M_UserRecei();
                    userrec.ReceivName = Receiver.Text;
                    userrec.Email = Email.Text;
                    userrec.isDefault = 1;
                    userrec.phone = Phone.Text;
                    if (!string.IsNullOrEmpty(Jiedao.Text))
                    {
                        userrec.Provinces = Jiedao.Text.Split('省')[0];
                        if (Jiedao.Text.Split('市') != null && Jiedao.Text.Split('市').Length > 1)
                        {
                            userrec.Street = Jiedao.Text.Split('市')[1];
                            if (Jiedao.Text.Split('市')[0].Split('省') != null && Jiedao.Text.Split('市')[0].Split('省').Length > 1)
                            {
                                userrec.Provinces += " " + Jiedao.Text.Split('市')[0].Split('省')[1];
                            }
                        }
                        else
                        {
                            userrec.Street = Jiedao.Text;
                        }
                    }
                    userrec.UserID = info.UserID;
                    userrec.Zipcode = ZipCode.Text;
                    buserrecei.GetInsert(userrec);
                }
            }
        }




        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int userid = buser.GetLogin().UserID;

            M_Uinfo mu = buser.GetUserBaseByuserid(userid);

            if (RadioButtonList1.SelectedValue.Equals("addre2"))
            {
                this.Jiedao.Text = mu.Address;
            }
            else if (RadioButtonList1.SelectedValue.Equals("addre1"))
            {
                this.Jiedao.Text = "";
            }
        }
        protected void Delivery_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetYunFei(DataConverter.CLng(Delivery.SelectedValue));
        }


        protected void dddizhi_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sid = DataConverter.CLng(dddizhi.SelectedValue);
            M_UserRecei rec = buserrecei.GetSelect(sid);
            if (rec != null && rec.ID > 0)
            {
                dddizhi.SelectedValue = sid.ToString();
                Receiver.Text = rec.ReceivName;
                string[] pa = rec.Provinces.Split(' ');
                string pro = "";
                if (pa != null && pa.Length > 1)
                {
                    pro = pa[0] + "省" + pa[1] + "市";
                }
                Jiedao.Text = pro + rec.Street;
                Email.Text = rec.Email;
                if (!string.IsNullOrEmpty(rec.phone) && rec.phone.Trim() != "--" && rec.phone.Trim() != "-")
                {
                    Phone.Text = rec.phone;
                }
                else if (!string.IsNullOrEmpty(rec.MobileNum))
                {
                    Phone.Text = rec.MobileNum;
                }
                if (Phone.Text == "-")
                {
                    Phone.Text = "";
                }
                ZipCode.Text = rec.Zipcode;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
        }
    //////////////////////////////////////////////////////////////////////////////////////
    #endregion

}