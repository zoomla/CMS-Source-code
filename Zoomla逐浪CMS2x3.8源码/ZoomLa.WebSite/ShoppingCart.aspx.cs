using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using System.Xml;
using System.Data;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Globalization;

public partial class ShoppingCart : System.Web.UI.Page
{
    protected B_Product bll = new B_Product();
    protected B_User buser = new B_User();
    protected B_CartPro Cll = new B_CartPro();
    protected B_Cart ACl = new B_Cart();
    protected B_CartPro bpro = new B_CartPro();
    protected XmlDocument objXmlDoc = new XmlDocument();
    protected string filename;
    protected DataSet fileset = new DataSet();
    protected DataTable infotable;
    protected DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        ////获得提交过来的参数
        //M_UserInfo mu = buser.GetLogin(false);
        //string UserName = mu.UserName;
        //int Pronum = DataConverter.CLng(Request.Form["Pronum"]);
        //string prolist = (Request.Form["prolist"] == null) ? "" : Request.Form["prolist"];
        //prolists.Value = prolist;
        //int id = DataConverter.CLng(Request.Form["id"]);
        //string messages = Request.QueryString["message"];
        //hfproclass.Value = Request.QueryString["Proclass"];
        //#region 定义参数
        //int jifen = 0;
        //string meinfo = "";
        //string mestr = "";
        //#endregion
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
        //#region 判断是否匿名购物
        //if (UserName == null)
        //{
        //    UserName = message32;
        //}
        //#endregion
        //#region 显示信息提示
        //if (!string.IsNullOrEmpty(messages))
        //{
        //    Label1.Text = messages;
        //}
        //#endregion
        //if (Pronum > 0 && id > 0 && bll.GetproductByid(id).ID > 0)
        //{
        //    #region 业务实体
        //    M_Product proinfo = bll.GetproductByid(id);
        //    M_Cart OData = new M_Cart();
        //    M_CartPro Cdata = new M_CartPro();
        //    M_Product Pdata = new M_Product();
        //    #endregion
        //    if (!IsPostBack)
        //    {
        //        hfproclass.Value = proinfo.ProClass.ToString();
        //    }
        //    int Propeidsnum = proinfo.Propeid;//重载自选数量
        //    #region prolist--提交商品列表(,) 为空的为单个商品
        //    if (proinfo.Priority == 1 && proinfo.Propeid > 0)
        //    {
        //        if (Propeidsnum > 0)
        //        {
        //            if (!string.IsNullOrEmpty(prolist))//处理接收的自选商品列表
        //            {
        //                if (prolist.IndexOf(",") > -1)//多个绑定商品的列表
        //                {
        //                    string[] listpro = prolist.Split(new string[] { "," }, StringSplitOptions.None);

        //                    if (listpro.Length != Propeidsnum)
        //                    {
        //                        prolist = "";
        //                        function.WriteErrMsg(message30);
        //                    }
        //                    else
        //                    {
        //                        //判断是否存在绑定商品中
        //                        B_BindPro bds = new B_BindPro();
        //                        DataTable bindpros = bds.SelByProID(id, prolist); 
        //                        if (bindpros.Rows.Count != Propeidsnum) { function.WriteErrMsg(message31); }//不存在此商品
        //                        if (bindpros != null)
        //                            bindpros.Dispose();
        //                    }

        //                }
        //                else//单个商品
        //                {
        //                    if (Propeidsnum != 1)
        //                    {
        //                        prolist = "";
        //                        function.WriteErrMsg(message30);
        //                    }
        //                    else
        //                    {
        //                        B_BindPro bds = new B_BindPro();
        //                        DataTable bindpros = bds.SelByProID(id, prolist); 
        //                        if (bindpros.Rows.Count != Propeidsnum) { function.WriteErrMsg(message31); }//不存在此商品
        //                        if (bindpros != null)
        //                            bindpros.Dispose();
        //                    }
        //                }
        //            }
        //            else { function.WriteErrMsg(message30); }
        //        }
        //        else
        //        {
        //            prolist = "";
        //        }
        //    }
        //    else
        //    {
        //        prolist = "";
        //    }
        //    #endregion

        //    #region 初始化数据
        //    int Cid = 0;
        //    string Cartusername = "";
        //    string Cartcartid = "";
        //    int Cartuserid = 0;
        //    DateTime Cartaddtime = DateTime.Now;
        //    string Cartgetip = "";
        //    double Cartallmoney = 0;
        //    int Cartpronum = 0;
        //    int Userid = 0;
        //    if (UserName != message32)
        //    {
        //        M_UserInfo Nameinfo = buser.GetUserByName(UserName);
        //        Userid = Nameinfo.UserID;
        //    }
        //    #endregion

        //    #region 生成购物车编号
        //    string cartid = "";
        //    DataTable Cart=ACl.GetByCartID(buser.GetLogin().UserID.ToString());
        //    if (Cart == null || Cart.Rows.Count <= 0)
        //    {
        //        string OrderNo = "";
        //        OrderNo = "CT" + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Millisecond);
        //        string TOrderNo = OrderNo.ToString();
        //        string aaa = StringHelper.MD5A(TOrderNo, 10, 6);
        //        string nowtime = DateTime.Now.ToString();
        //        string bbb = StringHelper.MD5A(nowtime, 5, 10);
        //        string ccc = StringHelper.MD5A(UserName, 5, 10);
        //        cartid = aaa + "-" + bbb + "-" + ccc;
        //    }
        //    else
        //    {
        //        cartid = Cart.Rows[0]["cartid"].ToString();
        //    }
        //    Response.Cookies["PointShopby"]["PointOrderNo"] = cartid;
        //    string Cartno = cartid;//重载
        //    #endregion

        //    #region 处理购物车数据(存在则更新，不存在则添加)
        //    /////添加判断是否存在此ID;GetByCartID
        //    //DataTable Cart = ACl.FondCartno(Cartno);
        //    M_Cart M_C1 = ACl.GetCartByUserId(mu.UserID);
        //    Cart = ACl.GetByCartID (mu.UserID.ToString());
        //    if (Cart.Rows.Count < 1)//如果不存在
        //    { 
        //        //////添加购物车///////
        //        OData.ID = 0;
        //        OData.Username = UserName.ToString();
        //        OData.Cartid = Cartno.ToString();
        //        OData.userid = Userid;
        //        OData.Addtime = DateTime.Now;
        //        OData.Getip = getIP();
        //        OData.AllMoney = 0;
        //        OData.Pronum = 0;
        //        ACl.Add(OData);

        //        DataTable Cartend = ACl.FondCartno(Cartno);
        //        if (Cartend != null && Cartend.Rows.Count > 0)
        //        {
        //            Cid = DataConverter.CLng(Cartend.Rows[0]["id"].ToString());

        //            Cartusername = Cartend.Rows[0]["Username"].ToString();
        //            Cartcartid = Cartend.Rows[0]["Cartid"].ToString();
        //            Cartuserid = DataConverter.CLng(Cartend.Rows[0]["Userid"].ToString());
        //            Cartaddtime = DataConverter.CDate(Cartend.Rows[0]["Addtime"].ToString());
        //            Cartgetip = Cartend.Rows[0]["Getip"].ToString();
        //            Cartallmoney = DataConverter.CDouble(Cartend.Rows[0]["AllMoney"].ToString());
        //            Cartpronum = DataConverter.CLng(Cartend.Rows[0]["Pronum"].ToString());
        //            Cartend.Dispose();
        //        }
        //        ///////////////////////
        //    }
        //    else
        //    {
        //        //读取数据
        //        if (Cart != null && Cart.Rows.Count > 0)
        //        {
        //            Cid = DataConverter.CLng(Cart.Rows[0]["id"].ToString());
        //            Cartusername = Cart.Rows[0]["Username"].ToString();
        //            Cartcartid = Cart.Rows[0]["Cartid"].ToString();
        //            Cartuserid = DataConverter.CLng(Cart.Rows[0]["Userid"].ToString());
        //            Cartaddtime = DataConverter.CDate(Cart.Rows[0]["Addtime"].ToString());
        //            Cartgetip = Cart.Rows[0]["Getip"].ToString();
        //            Cartallmoney = DataConverter.CDouble(Cart.Rows[0]["AllMoney"].ToString());
        //            Cartpronum = DataConverter.CLng(Cart.Rows[0]["Pronum"].ToString());
        //        }
        //        //读取购物车ID
        //    }
        //    //////////////////以上为购物车信息////////////////
        //    #endregion

        //    double jiage = 0;
        //    double alljia = 0;
        //    double s1 = 0;    //最小批发数量
        //    double s2 = 0;
        //    double s3 = 0;    //最小批发数量

        //    int shuliang = Pronum;//重载

        //    jiage = GetShiPrice(proinfo.ID.ToString());
        //    if (proinfo.ProClass == 3)
        //    {
        //        jiage = proinfo.PointVal;
        //    }
        //    if (prolist == "")
        //    {
        //        #region 处理批发价格

        //        if (proinfo.Quota < Pronum)
        //        {
        //            if (proinfo.Quota != -1)
        //            {
        //                shuliang = proinfo.Quota;
        //                mestr = message1 + shuliang + "! ";
        //            }
        //        }
        //        else
        //        {
        //            if (proinfo.DownQuota > Pronum)
        //            {
        //                if (proinfo.DownQuota != -1)
        //                {
        //                    shuliang = proinfo.DownQuota;
        //                    mestr = message2 + shuliang + "! ";
        //                }
        //            }
        //        }

        //        /////////批发价格/////////////////
        //        int Wholesales = proinfo.Wholesales;
        //        string Wholesalesinfo = proinfo.Wholesalesinfo;



        //        string[] tempinfo = Wholesalesinfo.Split(new char[] { ',' });
        //        string u1 = "";
        //        string u2 = "";
        //        string u3 = "";
        //        if (tempinfo != null && tempinfo.Length > 0)
        //        {
        //            u1 = tempinfo[0];
        //        }
        //        if (tempinfo != null && tempinfo.Length > 1)
        //        {
        //            u2 = tempinfo[1];
        //        }
        //        if (tempinfo != null && tempinfo.Length > 2)
        //        {
        //            u3 = tempinfo[2];
        //        }
        //        string[] uu1 = u1.Split(new char[] { '|' });
        //        string[] uu2 = u2.Split(new char[] { '|' });
        //        string[] uu3 = u3.Split(new char[] { '|' });

        //        if (uu1 != null && uu1.Length > 0)
        //        {
        //            s1 = DataConverter.CDouble(uu1[0]);    //最小批发数量
        //        }
        //        if (uu2 != null && uu2.Length > 0)
        //        {
        //            s2 = DataConverter.CDouble(uu2[0]);
        //        }
        //        if (uu3 != null && uu3.Length > 0)
        //        {
        //            s3 = DataConverter.CDouble(uu3[0]);    //最小批发数量
        //        }

        //        if (Wholesales == 1)
        //        {
        //            if (shuliang < s3)
        //            {
        //                if (shuliang < s2)
        //                {
        //                    if (shuliang < s1)
        //                    {
        //                        jiage = DataConverter.CDouble(jiage);
        //                    }
        //                    else
        //                    {
        //                        jiage = DataConverter.CDouble(uu1[1]);
        //                    }
        //                }
        //                else
        //                {
        //                    jiage = DataConverter.CDouble(uu2[1]);
        //                }
        //            }
        //            else
        //            {
        //                jiage = DataConverter.CDouble(uu3[1]);
        //            }

        //        }
        //        #endregion
        //    }
        //    else
        //    {
        //        #region 计算批量购物的价格//批量购物的商品单件打折无效，只能按包打折
        //        jiage = 0;
        //        if (prolist.IndexOf(",") > -1)
        //        {
        //            string[] prolistarr = prolist.Split(new string[] { "," }, StringSplitOptions.None);
        //            for (int polo = 0; polo < prolistarr.Length; polo++)
        //            {
        //                jiage = jiage + GetShiPrice(prolistarr[polo].ToString());
        //            }
        //        }
        //        else
        //        {
        //            jiage = jiage + GetShiPrice(prolist);
        //        }
        //        #endregion
        //    }

        //    jiage = jiage - jiage * ((double)proinfo.Recommend / 100);

        //    #region 计算税率价格
        //    double tempjia = jiage * shuliang;
        //    int sl = proinfo.Rateset;//税率计算方式
        //    double Rate = proinfo.Rate;//点数
        //    switch (sl)
        //    {
        //        case 1://含税，不开发票时有税率优惠
        //            alljia = tempjia - tempjia * (Rate / 100);
        //            break;
        //        case 2://含税，不开发票时没有税率优惠
        //            alljia = tempjia;
        //            break;
        //        case 3://不含税，开发票时需要加收税费
        //            alljia = tempjia + tempjia * (Rate / 100);
        //            break;
        //        case 4://不含税，开发票时不需要加收税费
        //            alljia = tempjia;
        //            break;
        //        default:
        //            break;
        //    }
        //    #endregion

        //    #region 单件商品实体数据定义
        //    //单件商品信息
        //    Cdata = Cll.GetSelect(proinfo.ID, Cid);
        //    Cdata.Addtime = DateTime.Now;
        //    Cdata.CartID = Cid; //购物车ID
        //    Cdata.ProID = proinfo.ID;
        //    Cdata.Shijia = jiage;   //////不含税点的价格
        //    Cdata.Danwei = proinfo.ProUnit;
        //    Cdata.Proname = proinfo.Proname;
        //    Cdata.Username = UserName;
        //    Cdata.Bindpro = prolist;//绑定的商品
        //    Cdata.Attribute = BaseClass.CheckInjection(Request.Form["Attribute"]);
        //    Cdata.Orderlistid = 0;

        //    bool type = false;  //添加商品类型：false为新商品,true为购物车已存在的商品
        //    if (Cdata == null || Cdata.ID <= 0)
        //    {
        //        Cdata.Pronum = shuliang;///////数量
        //        Cdata.AllMoney = alljia;
        //    }
        //    else
        //    {
        //        Cdata.Pronum = Cdata.Pronum + shuliang;///////数量
        //        Cdata.AllMoney = Cdata.AllMoney + alljia;
        //        type = true;
        //    }
        //    #endregion

        //    #region 提交购物数据处理
        //    int kucun = proinfo.Stock;
        //    if (proinfo.Sales == 1)
        //    {
        //        #region 检查库存数据
        //        if (proinfo.Stock <= 1 && kucun < 1)//库存,缺货
        //        {
        //            #region 是否允许缺货购买
        //            if (proinfo.Allowed != 1)
        //            {
        //                meinfo = message4;
        //            }
        //            else
        //            {
        //                if (proinfo.Wholesaleone == 1 && shuliang >= 1 && prolist == "")
        //                {
        //                    //允许单独购买
        //                    if (type)
        //                    {
        //                        Cll.Update(Cdata);
        //                    }
        //                    else
        //                    {
        //                        addPromotionShop(Cll.GetInsert(Cdata)); ;
        //                    }
        //                    bll.ProUpStock(proinfo.ID, kucun);
        //                    OData.ID = Cid;
        //                    OData.Username = Cartusername.ToString();
        //                    OData.Cartid = Cartcartid.ToString();
        //                    OData.userid = DataConverter.CLng(Cartuserid);
        //                    OData.Addtime = Cartaddtime;
        //                    OData.Getip = getIP();
        //                    OData.AllMoney = Cartallmoney + alljia;
        //                    OData.Pronum = Cartpronum + shuliang;
        //                    ACl.Update(OData);
        //                    meinfo = message3 + mestr + message5;
        //                }
        //                else
        //                {
        //                    if (proinfo.Wholesales == 1 && shuliang >= s1)
        //                    {
        //                        //允许批发购买
        //                        if (type)
        //                        {
        //                            Cll.Update(Cdata);
        //                        }
        //                        else
        //                        {
        //                            addPromotionShop(Cll.GetInsert(Cdata)); ;
        //                        }
        //                        bll.ProUpStock(proinfo.ID, kucun);
        //                        OData.ID = Cid;
        //                        OData.Username = Cartusername.ToString();
        //                        OData.Cartid = Cartcartid.ToString();
        //                        OData.userid = DataConverter.CLng(Cartuserid);
        //                        OData.Addtime = Cartaddtime;
        //                        OData.Getip = getIP();
        //                        OData.AllMoney = Cartallmoney + alljia;
        //                        OData.Pronum = Cartpronum + shuliang;
        //                        ACl.Update(OData);
        //                        meinfo = message3 + mestr + message6;
        //                    }
        //                    else
        //                    {
        //                        //不允许单独购买信息提示
        //                        meinfo = message7;
        //                    }

        //                }
        //            }
        //            #endregion
        //        }
        //        else
        //        {
        //            #region 正常购买!
        //            if (proinfo.Wholesaleone == 1 && shuliang >= 1)
        //            {
        //                //允许单独购买
        //                if (type)
        //                {
        //                    Cll.Update(Cdata);
        //                }
        //                else
        //                {
        //                    addPromotionShop(Cll.GetInsert(Cdata)); ;
        //                }
        //                bll.ProUpStock(proinfo.ID, kucun);
        //                OData.ID = Cid;
        //                OData.Username = Cartusername.ToString();
        //                OData.Cartid = Cartcartid.ToString();
        //                OData.userid = DataConverter.CLng(Cartuserid);
        //                OData.Addtime = Cartaddtime;
        //                OData.Getip = getIP();
        //                OData.AllMoney = Cartallmoney + alljia;
        //                OData.Pronum = Cartpronum + shuliang;
        //                ACl.Update(OData);
        //                meinfo = message3 + mestr + message5;
        //            }
        //            else
        //            {
        //                if (proinfo.Wholesales == 1 && shuliang >= s1)
        //                {
        //                    //允许批发购买
        //                    if (type)
        //                    {
        //                        Cll.Update(Cdata);
        //                    }
        //                    else
        //                    {
        //                        addPromotionShop(Cll.GetInsert(Cdata)); ;
        //                    }
        //                    bll.ProUpStock(proinfo.ID, kucun);
        //                    OData.ID = Cid;
        //                    OData.Username = Cartusername.ToString();
        //                    OData.Cartid = Cartcartid.ToString();
        //                    OData.userid = DataConverter.CLng(Cartuserid);
        //                    OData.Addtime = Cartaddtime;
        //                    OData.Getip = getIP();
        //                    OData.AllMoney = Cartallmoney + alljia;
        //                    OData.Pronum = Cartpronum + shuliang;
        //                    ACl.Update(OData);
        //                    meinfo = message3 + mestr + message9;
        //                }
        //                else//不允许单独购买
        //                {
        //                    meinfo = message3 + message10;
        //                }

        //            }
        //            #endregion
        //        }
        //        #endregion
        //    }
        //    else
        //    {
        //        meinfo = message11;
        //    }
        //    Response.Redirect("ShoppingCart.aspx?message=" + meinfo + "&Proclass=" + hfproclass.Value);
        //    #endregion
        //}
        //M_Cart M_C = ACl.GetCartByUserId(mu.UserID);
        //if (M_C != null)
        //{
        //    #region 定义分页数据
        //    int CPage = 0, temppage;

        //    if (Request.Form["DropDownList1"] != null)
        //    {
        //        temppage = Convert.ToInt32(Request.Form["DropDownList1"]);
        //    }
        //    else
        //    {
        //        temppage = Convert.ToInt32(Request.QueryString["CurrentPage"]);
        //    }

        //    if (temppage > 0)
        //    {
        //        CPage = temppage;
        //    }

        //    if (CPage <= 0)
        //    {
        //        CPage = 1;
        //    }
        //    #endregion

        //    #region 显示商品列表
        //    string Cartnod = M_C.Cartid;
        //    /////添加判断是否存在此ID;
        //    DataTable Cartlist = ACl.FondCartno(Cartnod);
        //    //if (Cartlist.Rows.Count > 0)
        //    {
        //        //int cartids = DataConverter.CLng(Cartlist.Rows[0]["id"]);//获得购物车ID
        //        int cartids = M_C.ID;
        //        //DataTable cplist = bpro.GetCartProCartID(cartids);//20111025
        //        DataTable cplist = bpro.GetCartProCartID(cartids);//20111025积分商品
        //        PagedDataSource cc = new PagedDataSource();
        //        cc.DataSource = cplist.DefaultView;
        //        cc.AllowPaging = true;
        //        cc.PageSize = 10;
        //        cc.CurrentPageIndex = CPage - 1;
        //        cartinfo.DataSource = cc;
        //        cartinfo.DataBind();

        //        Allnum.Text = cplist.DefaultView.Count.ToString();
        //        int thispagenull = cc.PageCount;//总页数
        //        int CurrentPage = cc.CurrentPageIndex;
        //        int nextpagenum = CPage - 1;//上一页
        //        int downpagenum = CPage + 1;//下一页
        //        int Endpagenum = thispagenull;
        //        if (thispagenull <= CPage)
        //        {
        //            downpagenum = thispagenull;
        //            Downpage.Enabled = false;
        //        }
        //        else
        //        {
        //            Downpage.Enabled = true;
        //        }
        //        if (nextpagenum <= 0)
        //        {
        //            nextpagenum = 0;
        //            Nextpage.Enabled = false;
        //        }
        //        else
        //        {
        //            Nextpage.Enabled = true;
        //        }
        //        Toppage.Text = "<a href=?Currentpage=0>" + message13 + "</a>";
        //        Nextpage.Text = "<a href=?Currentpage=" + nextpagenum.ToString() + ">" + message14 + "</a>";
        //        Downpage.Text = "<a href=?Currentpage=" + downpagenum.ToString() + ">" + message15 + "</a>";
        //        Endpage.Text = "<a href=?Currentpage=" + Endpagenum.ToString() + ">" + message16 + "</a>";
        //        Nowpage.Text = CPage.ToString();
        //        PageSize.Text = thispagenull.ToString();
        //        pagess.Text = cc.PageSize.ToString();


        //        if (!this.Page.IsPostBack)
        //        {
        //            for (int i = 1; i <= thispagenull; i++)
        //            {
        //                DropDownList1.Items.Add(i.ToString());
        //            }
        //        }


        //        int ccsd = cplist.Rows.Count;
        //        double ww = 0;
        //        string endjiage;

        //        for (int i = 0; i < ccsd; i++)
        //        {
        //            int tempnum = DataConverter.CLng(cplist.Rows[i]["Pronum"].ToString());
        //            ww = ww + Convert.ToDouble(cplist.Rows[i]["AllMoney"]);
        //        }

        //        ww = System.Math.Round(ww, 2);
        //        endjiage = ww.ToString();

        //        if (endjiage.IndexOf(".") == -1)
        //        {
        //            endjiage = endjiage + ".00";
        //        }
        //        this.alljiage.Text = endjiage.ToString();


        //        //遍历购物车中的所有商品,查找所有的促销设置
        //        string otherprojectinfo = "";
        //        string retnss = "";
        //        string allpresetlist = "";
        //        string allproductlist = "";
        //        for (int oo = 0; oo < cplist.Rows.Count; oo++)
        //        {
        //            #region 遍历购物车所有商品
        //            int readproid = DataConverter.CLng(cplist.Rows[oo]["ProID"].ToString());
        //            M_Product readproinfo = bll.GetproductByid(readproid);
        //            int ProjectTypes = readproinfo.ProjectType;

        //            switch (ProjectTypes)
        //            {
        //                case 1:
        //                    //不促销
        //                    break;
        //                case 2:
        //                    otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　买 " + readproinfo.ProjectPronum.ToString() + " 件" + readproinfo.Proname + " 可以送一件" + readproinfo.Proname + "</font></ul>";
        //                    break;
        //                case 3:
        //                    otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　买 " + readproinfo.ProjectPronum.ToString() + " 件" + readproinfo.Proname + " 可以送一件" + readproinfo.PesentNames + "</font></ul>";
        //                    break;
        //                case 4:
        //                    otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　送 " + readproinfo.ProjectPronum.ToString() + " 件同样商品:" + readproinfo.Proname + "</font></ul>";
        //                    break;
        //                case 5:
        //                    otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　送 " + readproinfo.ProjectPronum.ToString() + " 件" + readproinfo.PesentNames + "</font></ul>";
        //                    break;
        //                case 6:
        //                    otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　加 " + readproinfo.ProjectMoney.ToString() + " 元钱 送一件" + readproinfo.PesentNames + "</font></ul>";
        //                    break;
        //                case 7:
        //                    otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　满 " + readproinfo.ProjectMoney.ToString() + " 元钱 送一件" + readproinfo.PesentNames + "</font></ul>";
        //                    break;
        //            }
        //            if (readproinfo.IntegralNum > 0 && readproinfo.IntegralNum <= DataConverter.CLng(cplist.Rows[oo]["Pronum"].ToString()))
        //            {
        //                otherprojectinfo += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　购买" + readproinfo.IntegralNum.ToString() + "件 " + readproinfo.Proname + " 可以得到 " + readproinfo.Integral.ToString() + " 积分</font></ul>";

        //                jifen = jifen + (DataConverter.CLng(cplist.Rows[oo]["Pronum"]) / readproinfo.IntegralNum) * readproinfo.Integral;
        //            }

        //            //获得促销方案

        //            B_Promotions ption = new B_Promotions();
        //            string Presetinfo = readproinfo.Preset;

        //            string[] Presetinfoarr = Presetinfo.Split(new string[] { "," }, StringSplitOptions.None);
        //            string retns = "";


        //            for (int ccc = 0; ccc < Presetinfoarr.Length; ccc++)
        //            {
        //                string tempallpresetlist = "," + allpresetlist + ",";
        //                //不重复方案筛选
        //                if (tempallpresetlist.IndexOf("," + Presetinfoarr[ccc].ToString() + ",") == -1)
        //                {
        //                    allpresetlist = allpresetlist + Presetinfoarr[ccc].ToString() + ",";
        //                    M_Promotions Promotionsinfo = ption.GetPromotionsByid(DataConverter.CLng(Presetinfoarr[ccc].ToString()));
        //                    double infoPricetop = Promotionsinfo.Pricetop;//价格上限
        //                    double infoPriceend = Promotionsinfo.Priceend;//价格下限
        //                    int infoGetPresent = Promotionsinfo.GetPresent;//可以得到礼品
        //                    double infoPresentmoney = Promotionsinfo.Presentmoney;//可以以？金钱换购商品
        //                    int infoIntegralTure = Promotionsinfo.IntegralTure;//可以得到积分
        //                    int infoIntegral = Promotionsinfo.Integral;//积分
        //                    string infoPromoProlist = Promotionsinfo.PromoProlist;//礼品列表
        //                    DateTime infoPromostart = Promotionsinfo.Promostart;//起始时间
        //                    DateTime infoPromoend = Promotionsinfo.Promoend;//结束时间

        //                    if (DateTime.Now > infoPromostart && DateTime.Now < infoPromoend)
        //                    {
        //                        if (ww > infoPricetop && ww < infoPriceend)
        //                        {
        //                            if (!string.IsNullOrEmpty(infoPromoProlist))
        //                            {
        //                                retns = "可以获得下礼品中任一款";
        //                                if (!string.IsNullOrEmpty(infoPromoProlist))
        //                                {
        //                                    if (infoPromoProlist.IndexOf(",") > -1)
        //                                    {
        //                                        string[] newlist = infoPromoProlist.Split(new string[] { "," }, StringSplitOptions.None);
        //                                        {
        //                                            for (int ibc = 0; ibc < newlist.Length; ibc++)
        //                                            {
        //                                                string tempprolistsd = "," + allproductlist + ",";
        //                                                if (tempprolistsd.IndexOf("," + newlist[ibc].ToString() + ",") == -1)
        //                                                {
        //                                                    allproductlist = allproductlist + newlist[ibc].ToString() + ",";
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        string tempprolistsd = "," + allproductlist + ",";
        //                                        if (tempprolistsd.IndexOf("," + infoPromoProlist + ",") == -1)
        //                                        {
        //                                            allproductlist = allproductlist + infoPromoProlist + ",";
        //                                        }
        //                                    }
        //                                }


        //                            }

        //                            if (infoPresentmoney > 0 && infoGetPresent == 1)
        //                            {
        //                                retns = "可以" + string.Format("{0:C}", infoPresentmoney) + "元超值换购以下礼品中任一款";
        //                            }

        //                            if (infoIntegralTure == 1)
        //                            {
        //                                if (!string.IsNullOrEmpty(retns)) { retns = retns + "并"; }
        //                                retns = retns + "获得 " + infoIntegral.ToString() + " 积分";
        //                                jifen = jifen + infoIntegral;
        //                            }
        //                            if (!string.IsNullOrEmpty(retns))
        //                            {
        //                                retnss += "<ul style=\"width:100%;text-align:left; height:22px\"><font color=green>●　" + infoPricetop + "≤订单总价格＜" + infoPriceend + " 可享受 [" + Promotionsinfo.Promoname + "]: " + retns + "</font></ul>";
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            #endregion
        //        }

        //        this.project.Value = allpresetlist;
        //        this.jifen.Value = jifen.ToString();

        //        if (cplist != null && cplist.Rows.Count > 0)
        //        {
        //            this.hfproclass.Value = cplist.Rows[0]["proclass"].ToString();
        //        }
        //        allproductlist = allproductlist.TrimStart(new char[] { ',' });
        //        allproductlist = allproductlist.TrimEnd(new char[] { ',' });
        //        if (!string.IsNullOrEmpty(allproductlist))
        //        {
        //            DataTable productprolist = bll.Souchprolist(allproductlist);
        //            Repeater1.DataSource = productprolist.DefaultView;
        //            Repeater1.DataBind();
        //            if (productprolist != null)
        //                productprolist.Dispose();
        //        }
        //        Label2.InnerHtml = otherprojectinfo + retnss;
        //        if (cplist != null)
        //            cplist.Dispose();
        //    }
        //    if (Cartlist != null)
        //        Cartlist.Dispose();
        //    #endregion
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
        //        Response.Redirect("ShoppingCart.aspx?message=" + meinfo + "&Proclass=" + hfproclass.Value);
        //        break;
        //        #endregion
        //    case "edit":
        //        break;
        //    case "update":

        //        break;
        //    case "editok":
        //        meinfo = message18 + sid + message19;
        //        Response.Redirect("ShoppingCart.aspx?message=" + meinfo + "&Proclass=" + hfproclass.Value);
        //        break;
        //    default:
        //        break;
        //}

    }
    public void addPromotionShop(int id)
    {
        if (id <= 0) return;
        //推广数据string解码
        Security security = new Security();
        string strUserId = Request["sus"];
        string strUrl = Request["refurl"];
        if (strUserId != null && strUserId.Trim() != "")
        {
            strUserId = security.DecryptQueryString(strUserId);
            strUrl = security.DecryptQueryString(Request["refurl"] == null ? "" : Request["refurl"]);
            if (DataConverter.CLng(strUserId) <= 0)
            {
                return;
            }
            B_ArticlePromotion bap = new B_ArticlePromotion();
            M_ArticlePromotion map = new M_ArticlePromotion();
            M_UserInfo mui = new B_User().GetUserByUserID(DataConverter.CLng(strUserId));

            map = bap.GetSelectBySqlParams("select * from ZL_ArticlePromotion where cartproid=" + id, null);
            if (map.Id > 0)
            {
                bap.DeleteByGroupID(map.Id);
                map = new M_ArticlePromotion();
            }
            else
            {
                map = new M_ArticlePromotion();
            }

            if (mui.UserID > 0)
            {
                map.CartProId = id;
                map.PromotionUserId = mui.UserID;
                map.PromotionUrl = strUrl;
                map.IsBalance = false;
                map.IsEnable = false;
                map.AddTime = DateTime.Now;
                map.BalanceTime = DateTime.Now;

                bap.GetInsert(map);
            }
        }
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

        string url = SiteConfig.SiteInfo.SiteUrl;
        if (url.Substring(url.Length - 1) == "/")
        {
            url = url.Substring(0, url.Length - 1);
        }
        if (url.LastIndexOf('\\') > -1)
        {
            url = url.Substring(0, url.Length - 1);
        }
        url = url + "/Shop.aspx?ItemID=" + proid; 
        if (!string.IsNullOrEmpty(type)) { type = type.ToLower(); }
        if (!string.IsNullOrEmpty(type) && (type.IndexOf(".gif") > -1 || type.IndexOf(".jpg") > -1 || type.IndexOf(".png") > -1))
        {
            string delpath = SiteConfig.SiteOption.UploadDir.Replace("/", "") + "/";

            if (type.StartsWith("http://", true, CultureInfo.CurrentCulture) || type.StartsWith("/", true, CultureInfo.CurrentCulture) || type.StartsWith(delpath, true, CultureInfo.CurrentCulture))
                restring = "<a href='" + url + "'  target='_blank'><img src=../../" + type + " width=60 height=45></a>";
            else
            {
                restring = "<a href='" + url + "'  target='_blank'><img src=../../" + SiteConfig.SiteOption.UploadDir + "/" + type + " width=60 height=45></a>";
            }
        }
        else
        {
            restring = "<a href='" + url + "'  target='_blank'><img src=../../UploadFiles/nopic.gif width=60 height=45></a>";
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

    public string getjiage(string proclass, string type, string proid, string pronum)
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
            double jia = System.Math.Round(GetShiPrice(proid), 2);
            jiage = jia.ToString();
            if (pinfo.Wholesales == 1)
            {

                string[] pris = pinfo.Wholesalesinfo.Split(',');
                string[] pri1 = pris[0].Split('|');
                string[] pri2 = pris[1].Split('|');
                string[] pri3 = pris[2].Split('|');
                int num = DataConverter.CLng(pronum);
                if (num >= DataConverter.CLng((pri1[0])))
                {
                    jiage = pri1[1];
                }

                if (num >= DataConverter.CLng((pri2[0])))
                {
                    jiage = pri2[1];
                }

                if (num >= DataConverter.CLng((pri3[0])))
                {
                    jiage = pri3[1];
                }
            }
        }
        if (jiage.IndexOf(".") == -1)
        {
            jiage = jiage + ".00";
        }
        if (proclass == "3")
        {
            jiage = pinfo.PointVal.ToString();
        }

        return jiage;
    }


    /// <summary>
    ///  获取商品实际价格
    /// </summary>
    /// <param name="pid">商品ID</param>
    /// <returns></returns>
    private double GetShiPrice(string pid)
    {
        M_Product pinfo = bll.GetproductByid(DataConverter.CLng(pid));
        double price = 0;
        if (pinfo != null && pinfo.ID > 0)
        {
            string[] days = pinfo.FestPeriod.Split('|');
            price = pinfo.LinPrice;
            if (days != null && days.Length > 1)
            {
                if (DataConverter.CDate(days[0]) <= DateTime.Now && DataConverter.CDate(days[1]) >= DateTime.Now && pinfo.FestlPrice > 0)  //符合节日时间
                {
                    price = pinfo.FestlPrice;
                }
                else if (DateTime.Now.AddDays(pinfo.bookDay) >= DataConverter.CDate(days[0]) && DateTime.Now.AddDays(pinfo.bookDay) <= DataConverter.CDate(days[1]) && pinfo.BookPrice > 0)  //符合预订价
                {
                    price = pinfo.BookPrice;
                }
                else   //节日价与预订价都不是
                {
                    if (!string.IsNullOrEmpty(pinfo.UserPrice) && buser.CheckLogin())  //会员价不为空并且为登录用户
                    {
                        if (pinfo.UserType == 0 && DataConverter.CDouble(pinfo.UserPrice) > 0)  //会员价
                        {
                            price = DataConverter.CDouble(pinfo.UserPrice);
                        }
                        else  //会员组价
                        {
                            string[] userpri = pinfo.UserPrice.Split(',');
                            if (userpri != null && userpri.Length > 0)
                            {
                                for (int i = 0; i < userpri.Length; i++)
                                {
                                    string[] groupPrice = userpri[i].Split('|');
                                    if (groupPrice != null && groupPrice.Length > 1 && DataConverter.CLng(groupPrice[0]) == buser.GetLogin().GroupID && DataConverter.CDouble(groupPrice[1]) > 0)  //当前会员组
                                    {
                                        price = DataConverter.CDouble(groupPrice[1]);   //会员组价
                                    }
                                }
                            }
                            else
                            {
                                price = pinfo.LinPrice;//零售价格
                            }
                        }
                    }
                    else
                    {
                        price = pinfo.LinPrice;  //零售价格
                    }
                }
            }
            else
            {
                price = pinfo.LinPrice;  //零售价格
            }
        }
        return price;
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
        int sch = 0;
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

    public string getprojia(string cid, string pronum)
    {
        int pid = DataConverter.CLng(cid);
        string jiage = "";
        M_CartPro minfo = Cll.GetCartProByid(pid);
        M_Product proinfo = bll.GetproductByid(minfo.ProID);
        double dd = DataConverter.CDouble(proinfo.Rate);
        double tempdd = (double)1 / 100;


        if (proinfo.Wholesales == 1)
        {
            string[] pris = proinfo.Wholesalesinfo.Split(',');
            string[] pri1 = pris[0].Split('|');
            string[] pri2 = pris[1].Split('|');
            string[] pri3 = pris[2].Split('|');
            int num = DataConverter.CLng(pronum);
            if (num >= DataConverter.CLng((pri1[0])))
            {
                minfo.Shijia = Convert.ToDouble(pri1[1]);
            }

            if (num >= DataConverter.CLng((pri2[0])))
            {
                minfo.Shijia = Convert.ToDouble(pri2[1]);
            }

            if (num >= DataConverter.CLng((pri3[0])))
            {
                minfo.Shijia = Convert.ToDouble(pri3[1]);
            }
        }

        double jiag = System.Math.Round(minfo.Shijia * minfo.Pronum + minfo.Shijia * (double)(minfo.Pronum) * dd * tempdd, 2);



        if (proinfo.ProClass == 3)
        {
            jiag = minfo.Shijia * minfo.Pronum;
        }

        #region 打折信息
        double dazhe = double.Parse(getproscheme(cid));
        if (dazhe > 0)
        {
            jiag = jiag * (dazhe * 0.1);
        }
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

    public void cartinfo_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater rpColumnNews = (Repeater)e.Item.FindControl("Repeater2");
            DataRowView rowv = (DataRowView)e.Item.DataItem;
            string proid = rowv["Bindpro"].ToString();
            if (!string.IsNullOrEmpty(proid))
            {
                DataTable ddinfos = bll.Souchprolist(proid);
                rpColumnNews.DataSource = ddinfos.DefaultView;
                rpColumnNews.DataBind();
                if (ddinfos != null)
                    ddinfos.Dispose();
            }


        }
    }
    #endregion
    protected void Button1_Click(object sender, EventArgs e)
    {
        B_User.CheckIsLogged(Request.RawUrl);
        if (DataConverter.CLng(hfproclass.Value) == 3)
        {
            M_UserInfo mu = buser.GetLogin(false);
            double point = mu.UserExp;
            if (DataConverter.CDouble(alljiage.Text) > point)
            {
                function.WriteErrMsg("对不起,您积分不够,赶快去充值吧！");
            }
            else
            {
                if (Allnum.Text == null)
                {
                    Response.Write("<script>alert('购物车没有需要支付的商品！')</script>");
                    yhqtext.Text = null;
                    yhqpwd.Text = null;
                }
                else if (Allnum.Text == "")
                {
                    Response.Write("<script>alert('购物车没有需要支付的商品！')</script>");
                    yhqtext.Text = null;
                    yhqpwd.Text = null;
                }
                else
                {
                    if (yhqtext.Text == "" && yhqpwd.Text == "")
                    {
                        Response.Redirect("UserOrderlist.aspx?Proclass=" + hfproclass.Value);
                    }
                    else if (yhqtext.Text == "" || yhqpwd.Text == "")
                    {
                        Response.Write("<script>alert('请输入完整的优惠券信息！')</script>");
                        yhqtext.Text = null;
                        yhqpwd.Text = null;
                    }
                    else if (!(yhqtext.Text == "") && !(yhqpwd.Text == ""))
                    {
                        string yt = yhqtext.Text.Trim();
                        string yp = yhqpwd.Text.Trim();
                        B_Arrive b = new B_Arrive();
                        M_UserInfo muser = buser.GetLogin();
                        decimal mianzhi = 0;
                        if (mianzhi == 0)
                        {
                            Response.Write("<script>alert('优惠券编号或密码错误！')</script>");
                            yhqtext.Text = null;
                            yhqpwd.Text = null;
                        }
                        else
                        {
                            if (Convert.ToDecimal(alljiage.Text) >= mianzhi)
                            {
                                string t = (Convert.ToDecimal(alljiage.Text) - mianzhi).ToString();
                                string tt = "优惠券使用成功！" + "您的优惠券面值为：" + mianzhi + "。商品原价为：" + alljiage.Text + "。现价为：" + t;
                                Response.Write("<script>alert('" + tt + "'); location=' /User/Shopfee/UserOrderinfo.aspx?ProClass=';</script>");
                                b.UpdateState(yt);
                                b.UpdateUseTime(yt);
                            }
                            else
                            {
                                Response.Write("<script>alert('优惠券面值高于商品总价，无法使用！')</script>");
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (Allnum.Text == null)
            {
                Response.Write("<script>alert('购物车没有需要支付的商品！')</script>");
                yhqtext.Text = null;
                yhqpwd.Text = null;
            }
            else if (Allnum.Text == "0")
            {
                Response.Write("<script>alert('购物车没有需要支付的商品！')</script>");
                yhqtext.Text = null;
                yhqpwd.Text = null;
            }
            else
            {
                if (yhqtext.Text == "" && yhqpwd.Text == "")
                {
                    Response.Redirect("UserOrderlist.aspx?Proclass=" + hfproclass.Value);
                }
                else if (yhqtext.Text == "" || yhqpwd.Text == "")
                {
                    Response.Write("<script>alert('请输入完整的优惠券信息！')</script>");
                    yhqtext.Text = null;
                    yhqpwd.Text = null;
                }
                else if (!(yhqtext.Text == "") && !(yhqpwd.Text == ""))
                {
                    string yt = yhqtext.Text.Trim();
                    string yp = yhqpwd.Text.Trim();
                    B_Arrive b = new B_Arrive();
                    M_UserInfo muser = buser.GetLogin();
                    decimal mianzhi = 0;
                    if (mianzhi == 0)
                    {
                        Response.Write("<script>alert('优惠券编号或密码错误！')</script>");
                        yhqtext.Text = null;
                        yhqpwd.Text = null;
                    }
                    else
                    {
                        if (Convert.ToDecimal(alljiage.Text) >= mianzhi)
                        {
                            string t = (Convert.ToDecimal(alljiage.Text) - mianzhi).ToString();
                            string tt = "优惠券使用成功！" + "您的优惠券面值为：" + mianzhi + "。商品原价为：" + alljiage.Text + "。现价为：" + t;
                            Response.Write("<script>alert('" + tt + "'); location=' /User/Shopfee/UserOrderinfo.aspx?ProClass=';</script>");
                            b.UpdateState(yt);
                            b.UpdateUseTime(yt);
                        }
                        else
                        {
                            Response.Write("<script>alert('优惠券面值高于商品总价，无法使用！')</script>");
                        }
                    }
                }
            }
        }
    }
}