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
using ZoomLa.Components;
using System.Globalization;

public partial class doShopCar : System.Web.UI.Page
{
    protected B_Product bll = new B_Product();
    protected B_User buser = new B_User();
    protected B_CartPro Cll = new B_CartPro();
    protected B_Cart ACl = new B_Cart();
    protected XmlDocument objXmlDoc = new XmlDocument();
    protected string filename;
    protected DataSet fileset = new DataSet();
    protected DataTable infotable;
    public string CookieOrderNo
    {
        get
        {
            return (Request.Cookies["Shopby"] == null || Request.Cookies["Shopby"]["OrderNo"] == null) ? "" : Request.Cookies["Shopby"]["OrderNo"];
        }
        set 
        {
            Response.Cookies["Shopby"]["OrderNo"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {     
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
        //int jifen = 0;string meinfo = "", mestr = "";//获得提交过来的参数
        //#region 有数据则加入购物车
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
        //    string messages = Request.QueryString["message"];
        //    Label1.Text = messages;
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
        //        //if (UserName != message32)
        //        //{
        //        //    M_UserInfo Nameinfo = buser.GetUserByName(UserName);
        //        //    Userid = Nameinfo.UserID;
        //        //}
        //        #endregion
        //        #region 生成购物车编号
        //        if (string.IsNullOrEmpty(CookieOrderNo))
        //        {
        //            M_Cart ca = ACl.GetCartByUserId(Userid);
        //            if (ca != null && ca.ID > 0)
        //            {
        //                CookieOrderNo = ca.Cartid;
        //            }
        //            else
        //            {
        //                string OrderNo = "";
        //                OrderNo = "CT" + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Millisecond);
        //                string TOrderNo = OrderNo.ToString();
        //                string aaa = StringHelper.MD5A(TOrderNo, 10, 6);
        //                string nowtime = DateTime.Now.ToString();
        //                string bbb = StringHelper.MD5A(nowtime, 5, 10);
        //                string ccc = StringHelper.MD5A("Zoomla!CMS", 5, 10);//UserName
        //                string cartid = aaa + "-" + bbb + "-" + ccc;
        //                CookieOrderNo = cartid;
        //            }
        //        }
        //        string Cartno = CookieOrderNo;
        //        #endregion
        //        #region 处理购物车数据(存在则更新，不存在则添加)
        //        /////添加判断是否存在此ID;
        //        DataTable Cart = ACl.FondCartno(Cartno);

        //        if (Cart.Rows.Count < 1)
        //        /////如果不存在
        //        {
        //            //////添加购物车///////
        //            OData.ID = 0;
        //            //OData.Username = UserName.ToString();
        //            OData.Cartid = Cartno.ToString();
        //            OData.userid = Userid;
        //            OData.Addtime = DateTime.Now;
        //            OData.Getip =EnviorHelper.GetUserIP();
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
        //        //double s1 = 0;    //最小批发数量
        //        //double s2 = 0;
        //        //double s3 = 0;    //最小批发数量

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
        //        //Cdata.Username = UserName;
        //        Cdata.Bindpro = "";//绑定的商品
        //        Cdata.ProSeller = proinfo.ProSeller;//
        //        Cdata.Attribute = Request["Attribute"];//商品属性

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
        //        OData.Getip = EnviorHelper.GetUserIP();
        //        OData.AllMoney = Cartallmoney + alljia;
        //        OData.Pronum = Cartpronum + shuliang;
        //        ACl.Update(OData);
        //        meinfo = message3 + mestr + message5;
        //    }
        //}
        //#endregion
        ////-------------------------------------------------------------------
        //#region 查询上次未提交的购物车
        //if (string.IsNullOrEmpty(CookieOrderNo))
        //{
        //    M_Cart ca = ACl.GetCartByUserId(buser.GetLogin().UserID);
        //    if (ca != null && !string.IsNullOrEmpty(ca.Cartid))
        //    {
        //        CookieOrderNo = ca.Cartid;
        //    }
        //}
        //#endregion
        //#region 定义分页数据
        //int CPage = 0, temppage;

        //if (Request.Form["DropDownList1"] != null)
        //{
        //    temppage = Convert.ToInt32(Request.Form["DropDownList1"]);
        //}
        //else
        //{
        //    temppage = Convert.ToInt32(Request.QueryString["CurrentPage"]);
        //}

        //if (temppage > 0)
        //{
        //    CPage = temppage;
        //}

        //if (CPage <= 0)
        //{
        //    CPage = 1;
        //}
        //#endregion
        //#region 显示商品列表
        ////显示商品列表;
        //string Cartnod = CookieOrderNo;
        ////添加判断是否存在此ID;
        //DataTable Cartlist = ACl.FondCartno(Cartnod);//ZL_Cart
        //if (Cartlist.Rows.Count > 0)
        //{
        //    int cartids = DataConverter.CLng(Cartlist.Rows[0]["id"]);//获得购物车ID
        //    DataTable cplist = Cll.GetCartProCartID(cartids);

        //    PagedDataSource cc = new PagedDataSource();
        //    cc.DataSource = cplist.DefaultView;

        //    cc.AllowPaging = true;
        //    cc.PageSize = 10;
        //    cc.CurrentPageIndex = CPage - 1;
        //    cartinfo.DataSource = cc;
        //    cartinfo.DataBind();

        //    Allnum.Text = cplist.DefaultView.Count.ToString();
        //    int thispagenull = cc.PageCount;//总页数
        //    int CurrentPage = cc.CurrentPageIndex;
        //    int nextpagenum = CPage - 1;//上一页
        //    int downpagenum = CPage + 1;//下一页
        //    int Endpagenum = thispagenull;
        //    if (thispagenull <= CPage)
        //    {
        //        downpagenum = thispagenull;
        //        Downpage.Enabled = false;
        //    }
        //    else
        //    {
        //        Downpage.Enabled = true;
        //    }
        //    if (nextpagenum <= 0)
        //    {
        //        nextpagenum = 0;
        //        Nextpage.Enabled = false;
        //    }
        //    else
        //    {
        //        Nextpage.Enabled = true;
        //    }
        //    Toppage.Text = "<a href=?Currentpage=0>" + message13 + "</a>";
        //    Nextpage.Text = "<a href=?Currentpage=" + nextpagenum.ToString() + ">" + message14 + "</a>";
        //    Downpage.Text = "<a href=?Currentpage=" + downpagenum.ToString() + ">" + message15 + "</a>";
        //    Endpage.Text = "<a href=?Currentpage=" + Endpagenum.ToString() + ">" + message16 + "</a>";
        //    Nowpage.Text = CPage.ToString();
        //    PageSize.Text = thispagenull.ToString();
        //    pagess.Text = cc.PageSize.ToString();


        //    if (!this.Page.IsPostBack)
        //    {
        //        for (int i = 1; i <= thispagenull; i++)
        //        {
        //            DropDownList1.Items.Add(i.ToString());
        //        }
        //    }
        //    int ccsd = cplist.Rows.Count;
        //    double ww = 0;
        //    string endjiage;

        //    for (int i = 0; i < ccsd; i++)
        //    {
        //        int tempnum = DataConverter.CLng(cplist.Rows[i]["Pronum"].ToString());
        //        ww = ww + Convert.ToDouble(cplist.Rows[i]["AllMoney"]);
        //    }
        //    ww = System.Math.Round(ww, 2);
        //    endjiage = ww.ToString();

        //    if (endjiage.IndexOf(".") == -1)
        //    {
        //        endjiage = endjiage + ".00";
        //    }
        //    this.alljiage.Text = endjiage.ToString();

        //    //遍历购物车中的所有商品,查找所有的促销设置
        //    string otherprojectinfo = "";
        //    string retnss = "";
        //    string allpresetlist = "";
        //    string allproductlist = "";
        //    for (int oo = 0; oo < cplist.Rows.Count; oo++)
        //    {
        //        #region 遍历购物车所有商品
        //        int readproid = DataConverter.CLng(cplist.Rows[oo]["ProID"].ToString());
        //        M_Product readproinfo = bll.GetproductByid(readproid);
        //        int ProjectTypes = readproinfo.ProjectType;

        //        switch (ProjectTypes)
        //        {
        //            case 1:
        //                //不促销
        //                break;
        //            case 2:
        //                otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　买 " + readproinfo.ProjectPronum.ToString() + " 件" + readproinfo.Proname + " 可以送一件" + readproinfo.Proname + "</font></ul>";
        //                break;
        //            case 3:
        //                otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　买 " + readproinfo.ProjectPronum.ToString() + " 件" + readproinfo.Proname + " 可以送一件" + readproinfo.PesentNames + "</font></ul>";
        //                break;
        //            case 4:
        //                otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　送 " + readproinfo.ProjectPronum.ToString() + " 件同样商品:" + readproinfo.Proname + "</font></ul>";
        //                break;
        //            case 5:
        //                otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　送 " + readproinfo.ProjectPronum.ToString() + " 件" + readproinfo.PesentNames + "</font></ul>";
        //                break;
        //            case 6:
        //                otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　加 " + readproinfo.ProjectMoney.ToString() + " 元钱 送一件" + readproinfo.PesentNames + "</font></ul>";
        //                break;
        //            case 7:
        //                otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　满 " + readproinfo.ProjectMoney.ToString() + " 元钱 送一件" + readproinfo.PesentNames + "</font></ul>";
        //                break;
        //        }
        //        if (readproinfo.IntegralNum > 0 && readproinfo.IntegralNum <= DataConverter.CLng(cplist.Rows[oo]["Pronum"].ToString()))
        //        {
        //            otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　购买" + readproinfo.IntegralNum.ToString() + "件 " + readproinfo.Proname + " 可以得到 " + readproinfo.Integral.ToString() + " 积分</font></ul>";

        //            jifen = jifen + (DataConverter.CLng(cplist.Rows[oo]["Pronum"]) / readproinfo.IntegralNum) * readproinfo.Integral;
        //        }

        //        //获得促销方案

        //        B_Promotions ption = new B_Promotions();
        //        string Presetinfo = readproinfo.Preset;

        //        string[] Presetinfoarr = Presetinfo.Split(new string[] { "," }, StringSplitOptions.None);
        //        string retns = "";


        //        for (int ccc = 0; ccc < Presetinfoarr.Length; ccc++)
        //        {
        //            string tempallpresetlist = "," + allpresetlist + ",";
        //            //不重复方案筛选
        //            if (tempallpresetlist.IndexOf("," + Presetinfoarr[ccc].ToString() + ",") == -1)
        //            {
        //                allpresetlist = allpresetlist + Presetinfoarr[ccc].ToString() + ",";
        //                M_Promotions Promotionsinfo = ption.GetPromotionsByid(DataConverter.CLng(Presetinfoarr[ccc].ToString()));
        //                double infoPricetop = Promotionsinfo.Pricetop;//价格上限
        //                double infoPriceend = Promotionsinfo.Priceend;//价格下限
        //                int infoGetPresent = Promotionsinfo.GetPresent;//可以得到礼品
        //                double infoPresentmoney = Promotionsinfo.Presentmoney;//可以以？金钱换购商品
        //                int infoIntegralTure = Promotionsinfo.IntegralTure;//可以得到积分
        //                int infoIntegral = Promotionsinfo.Integral;//积分
        //                string infoPromoProlist = Promotionsinfo.PromoProlist;//礼品列表
        //                DateTime infoPromostart = Promotionsinfo.Promostart;//起始时间
        //                DateTime infoPromoend = Promotionsinfo.Promoend;//结束时间

        //                if (DateTime.Now > infoPromostart && DateTime.Now < infoPromoend)
        //                {
        //                    if (ww > infoPricetop && ww < infoPriceend)
        //                    {
        //                        if (!string.IsNullOrEmpty(infoPromoProlist))
        //                        {
        //                            retns = "可以获得下礼品中任一款";
        //                            if (!string.IsNullOrEmpty(infoPromoProlist))
        //                            {
        //                                if (infoPromoProlist.IndexOf(",") > -1)
        //                                {
        //                                    string[] newlist = infoPromoProlist.Split(new string[] { "," }, StringSplitOptions.None);
        //                                    {
        //                                        for (int ibc = 0; ibc < newlist.Length; ibc++)
        //                                        {
        //                                            string tempprolistsd = "," + allproductlist + ",";
        //                                            if (tempprolistsd.IndexOf("," + newlist[ibc].ToString() + ",") == -1)
        //                                            {
        //                                                allproductlist = allproductlist + newlist[ibc].ToString() + ",";
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    string tempprolistsd = "," + allproductlist + ",";
        //                                    if (tempprolistsd.IndexOf("," + infoPromoProlist + ",") == -1)
        //                                    {
        //                                        allproductlist = allproductlist + infoPromoProlist + ",";
        //                                    }
        //                                }
        //                            }


        //                        }

        //                        if (infoPresentmoney > 0 && infoGetPresent == 1)
        //                        {
        //                            retns = "可以" + string.Format("{0:C}", infoPresentmoney) + "元超值换购以下礼品中任一款";
        //                        }

        //                        if (infoIntegralTure == 1)
        //                        {
        //                            if (!string.IsNullOrEmpty(retns)) { retns = retns + "并"; }
        //                            retns = retns + "获得 " + infoIntegral.ToString() + " 积分";
        //                            jifen = jifen + infoIntegral;
        //                        }
        //                        if (!string.IsNullOrEmpty(retns))
        //                        {
        //                            retnss += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　" + infoPricetop + "≤订单总价格＜" + infoPriceend + " 可享受 [" + Promotionsinfo.Promoname + "]: " + retns + "</font></ul>";
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        #endregion
        //    }

        //    this.project.Value = allpresetlist;
        //    this.jifen.Value = jifen.ToString();
        //}
        //#endregion
        //if (!IsPostBack)
        //{
        //    string menu = Request.QueryString["menu"];
        //    int sid = DataConverter.CLng(Request.QueryString["cid"]);
        //    switch (menu)
        //    {
        //        case "del":
        //            #region 删除购物车内商品
        //            M_CartPro Ts = Cll.GetCartProByid(sid);
        //            int ccid = Ts.CartID;
        //            int ccnum = Ts.Pronum;
        //            int ppid = Ts.ProID;
        //            M_Cart Ats = ACl.GetCartByid(ccid);
        //            int num = Ats.Pronum;
        //            int stocknum = num + ccnum;
        //            int tempjiages = num - ccnum;
        //            if (tempjiages < 0)
        //            {
        //                tempjiages = 0;
        //            }
        //            M_Cart Ctss = new M_Cart();
        //            Ctss.ID = Ats.ID;
        //            Ctss.Username = Ats.Username;
        //            Ctss.Cartid = Ats.Cartid;
        //            Ctss.userid = Ats.userid;
        //            Ctss.Addtime = Ats.Addtime;
        //            Ctss.Getip = EnviorHelper.GetUserIP();
        //            Ctss.AllMoney = Ats.AllMoney - Ts.AllMoney;
        //            Ctss.Pronum = tempjiages;
        //            ACl.Update(Ctss);
        //            Cll.DeleteByID(sid);
        //            meinfo = message17 + sid + message19;
        //            Response.Redirect("doShopCar.aspx?message=" + meinfo + "&ProClass=" + ProClass.Value);
        //            break;
        //            #endregion
        //        case "edit":
        //            break;
        //        case "editok":
        //            meinfo = message18 + sid + message19;
        //            Response.Redirect("doShopCar.aspx?message=" + meinfo + "&ProClass=" + ProClass.Value);
        //            break;
        //        default:
        //            break;
        //    }
        //}
       
    }
    #region 方法定义
    // 获取商品图片
    public string getproimg(string proid)
    {
        int pid = DataConverter.CLng(proid);
        M_Product pinfo = bll.GetproductByid(pid);
        string  type;
        type = pinfo.Thumbnails;
        return getproimgs(type);
    }
    public string getproimgs(string type)
    {
        string restring;
        restring = "";

        if (!string.IsNullOrEmpty(type)) { type = type.ToLower(); }
        if (!string.IsNullOrEmpty(type) && (type.IndexOf(".gif") > -1 || type.IndexOf(".jpg") > -1 || type.IndexOf(".png") > -1))
        {
            string delpath = SiteConfig.SiteOption.UploadDir.Replace("/", "") + "/";

            if (type.StartsWith("http://", true, CultureInfo.CurrentCulture) || type.StartsWith("/", true, CultureInfo.CurrentCulture) || type.StartsWith(delpath, true, CultureInfo.CurrentCulture))
                restring = "<img src=../../" + type + " width=60 height=45>";
            else
            {
                restring = "<img src=../../" + SiteConfig.SiteOption.UploadDir + "/" + type + " width=60 height=45>";
            }
        }
        else
        {
            restring = "<img src=../../UploadFiles/nopic.gif width=60 height=45>";
        }
        return restring;

    }
    // 获得商品类型是否绑定商品
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
    // 商品备注
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
    protected B_ModelField bfield = new B_ModelField();
    protected B_Model bmode = new B_Model();
    protected B_Node bnode = new B_Node();
    private int SetProduct()
    {
        int ComModelID = 0;
        M_Product CData = new M_Product();
        CData.Class = 0;
        CData.Proname = Request.Form["Proname"];
        CData.ServerType = 3;
        CData.ProClass = DataConverter.CLng(ProClass.Value) <= 0 ? 1 : DataConverter.CLng(ProClass.Value);
        CData.Properties = 0;
        CData.Sales = 1;
        CData.Proinfo = Request.Form["Proinfo"];
        CData.Procontent = Request.Form["ProUrl"];
        CData.ProSeller = Request.Form["ProSeller"];
        CData.Quota = -1;
        CData.DownQuota = -1;
        CData.Stock = (DataConverter.CLng(Request.Form["Stock"]) == 0) ? 0 : DataConverter.CLng(Request.Form["Stock"]);
        CData.StockDown = 1;
        CData.Rateset = 1;
        CData.Dengji = 3;
        if (CData.ProClass == 3)
        {
            CData.Wholesaleone = 0;
            CData.ShiPrice = 0;
            CData.LinPrice = 0;
            CData.PointVal = DataConverter.CLng(Request.Form["Wholesaleone"]);
        }
        else
        {
            CData.Wholesaleone = DataConverter.CLng(Request.Form["Wholesaleone"]);
            CData.ShiPrice = DataConverter.CDouble(Request.Form["Wholesaleone"]);
            CData.LinPrice = DataConverter.CDouble(Request.Form["Wholesaleone"]);
            CData.PointVal = 0;
        }

        CData.AllClickNum = 1;
        CData.UpdateTime = DateTime.Now;
        CData.DownCar = 0;
        CData.AddTime = DateTime.Now;
        CData.Istrue = 1;
        CData.Isgood = 0;
        CData.MakeHtml = 0;
        ComModelID = this.bll.AddCommodities(CData);
        return ComModelID;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        if (Allnum.Text == null)
        {
            function.WriteErrMsg("购物车没有需要支付的商品！");
            yhqtext.Text = null;
            yhqpwd.Text = null;
        }
        else if (Allnum.Text == "0"||string.IsNullOrEmpty(alljiage.Text.Trim()))
        {
            function.WriteErrMsg("购物车没有需要支付的商品！");
            yhqtext.Text = null;
            yhqpwd.Text = null;
        }
        else
        {
            string setPrice ="";
            decimal jia = Convert.ToDecimal(alljiage.Text);
            decimal price = Convert.ToDecimal(setPrice);
            if (jia < price||jia<0)
            {
                function.WriteErrMsg("抱歉，您的订单没有达到系统最小订单金额[" + price + "]元，点此返回首页继续购物。");
            }
            else
            {
                if (yhqtext.Text == "" && yhqpwd.Text == "")
                {
                    Response.Redirect("/User/Shopfee/Moneytop.aspx?ProClass=" + ProClass.Value);//"/User/Shopfee/UserOrderinfo.aspx?ProClass=" + ProClass.Value
                }
                else if (yhqtext.Text == "" || yhqpwd.Text == "")
                {
                    function.WriteErrMsg("请输入完整的优惠券信息！");
                    yhqtext.Text = null;
                    yhqpwd.Text = null;
                }
                else if (!(yhqtext.Text == "") && !(yhqpwd.Text == ""))
                {
                 
                }
            }
        }
    }
}