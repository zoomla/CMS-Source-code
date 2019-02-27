using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

namespace ZoomLaCMS.Controllers
{
    public class SiteController : Controller
    {
        B_IDC_DomainPrice idpBll = new B_IDC_DomainPrice();
        B_IDC_DomainTemp idtBll = new B_IDC_DomainTemp();
        B_IDC_DomainOrder idoBll = new B_IDC_DomainOrder();
        DataTableHelper dtHelper = new DataTableHelper();
        B_CartPro cartBll = new B_CartPro();
        B_OrderList olistBll = new B_OrderList();
        B_User buser = new B_User();
        //购物车Sessino,原数据存在页面控件,现改由存在服务端内存
        private DataTable DomCartDT
        {
            get { return Session["DomCartDT"] as DataTable; }
            set { Session["DomCartDT"] = value; }
        }
        //存入Cookies中的购物车
        private DataTable CookieCartDT
        {
            get
            {
                DataTable resultDT = CreateCartColumns();
                if (Request.Cookies["Dom"] != null && !string.IsNullOrEmpty(Request.Cookies["Dom"]["DomName"]))//Cookies中存了购物车信息
                {
                    DataTable priceDT = idpBll.Sel();
                    string[] domName = Request.Cookies["Dom"]["DomName"].Split(',');
                    for (int i = 0; i < domName.Length; i++)
                    {
                        try
                        {
                            DataRow dr = resultDT.NewRow();
                            dr["DomName"] = domName[i];
                            dr["DomPrice"] = idpBll.GetPrice(domName[i], priceDT);
                            dr["Year"] = 1;
                            dr["Money"] = dr["DomPrice"];
                            resultDT.Rows.Add(dr);
                        }
                        catch { }
                    }
                }
                return resultDT;
            }
            set
            {
                DataTable resultDT = value as DataTable;
                string v = "";
                if (value != null)//为null则清空
                {
                    for (int i = 0; i < resultDT.Rows.Count; i++)
                    {
                        DataRow dr = resultDT.Rows[i];
                        v += dr["DomName"].ToString() + ",";
                    }
                    v = v.TrimEnd(',');
                }
                Response.Cookies["Dom"]["DomName"] = v;
                Response.Cookies["Dom"].Expires = DateTime.Now.AddDays(1d);
            }
        }
        private string DomainStep { get { return DataConverter.CStr(Session["DomainStep"]); } set { ViewBag.step = Session["DomainStep"] = value; } }
        private string Strli
        {
            get
            {
                string[] arr = StationGroup.DefaultDisplay.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string strli = "";
                for (int i = 0; i < arr.Length; i++)//显示哪些后缀
                {
                    strli += "<li><input type='checkbox' name='ext' value='" + Server.HtmlEncode(arr[i]) + "' id='Checkbox2'/><label>" + Server.HtmlEncode(arr[i]) + "</label></li><li>";
                }
                return strli;
            }
        }
        public ActionResult Default()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(SiteConfig.SiteOption.ProjectServer + "/api/gettemplate.aspx?menu=getprojectinfo");
            DataTable dt = ds.Tables[0];
            DataTable tlplist = dt.Clone();
            int count = 30;
            foreach (DataRow dr in dt.Rows)
            {
                if (count <= 0) continue;
                tlplist.Rows.Add(dr.ItemArray);
                count--;
            }
            ViewBag.tlplist = tlplist;
            ViewBag.showreg = Request["showreg"];
            ViewBag.domname = Request["domname"];
            ViewBag.msg = Request["msg"];
            return View();
        }
        public void Search()
        {
            string action = Request["action"];
            string domname = Request.Form["DomName_T"].Trim(' ');
            if (action.Equals("start")) { domname = Request.Form["DomName2_T"].Trim(' '); }
            if (buser.CheckLogin())
            {
                Session["domNameL"] = domname + StationGroup.TDomName;
                Response.Redirect("/Plugins/Domain/CreateSite.aspx?url=" + domname);
            }
            else
            {
                Response.Redirect("Default?showreg=showreg&domname=" + domname);
            }
        }
        public void UserReg()
        {
            M_UserInfo mu = new M_UserInfo();
            mu.UserName = Request.Form["username_t"].Replace(" ", "");
            mu.UserPwd = StringHelper.MD5(Request.Form["userpwd_t"].Trim(' '));
            string msg = "";
            if (string.IsNullOrEmpty(mu.UserName) || string.IsNullOrEmpty(mu.UserPwd))
            {
                msg = "用户名与密码不能为空!!";
            }
            else if (!buser.IsExistUName(mu.UserName))
            {
                buser.AddModel(mu);
                buser.SetLoginState(mu, "Day");
                Response.Redirect("/Plugins/Domain/CreateSite.aspx"); return;
            }
            else
            {
                msg = "用户已存在!!";
            }
            Response.Redirect("Default?showreg=showreg&msg=" + msg);
        }
        public void UserLogin()
        {
            string uname = Request.Form["uname_t"];
            string upwd = Request.Form["upwd_t"];
            string msg = "";
            if (!string.IsNullOrEmpty(uname) || !string.IsNullOrEmpty(upwd))
            {
                M_UserInfo mu = buser.AuthenticateUser(uname, upwd);
                if (mu.IsNull)
                    msg = "用户名或密码错误!!!";
                else
                {
                    buser.SetLoginState(mu, "Day");
                    Response.Redirect("/Plugins/Domain/CreateSite.aspx"); return;
                }
            }
            else
            {
                msg = "用户名与密码不能为空!!!";
            }
            Response.Redirect("Default?showreg=showreg&msg=" + msg);
        }
        #region 域名注册
        public ActionResult Domain()
        {
            string remote = Request["remote"];
            string clientID = StationGroup.newNetClientID;
            string apiPasswd = StationGroup.newNetApiPasswd;
            if (DomCartDT != null && buser.CheckLogin() && Session["price"] != null)//登录后跳转回来的
            {
                return RedirectToAction("TwoStep", new { action = "logon" });
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["op"]) && Request.QueryString["op"].ToLower().Equals("viewcart") && buser.CheckLogin())
            {
                return RedirectToAction("TwoStep", new { action = "viewcart" });
            }
            else
            {
                ViewBag.setdef = StationGroup.DefaultCheck;
                ViewBag.strli = Strli;
                DomainStep = "reg";
                return View();
            }
        }
        //查询:第一步
        public ActionResult OneStep()
        {
            string domainBody = Request.Form["domainBody"];
            string ext = Server.HtmlEncode(Request.Form["ext"]);
            string ext2 = Server.HtmlEncode(Request.Form["ext2"]);
            string[] netArr = domainBody.Trim().Split('\n');
            DataTable resultDt = new DataTable();
            for (int i = 0; i < netArr.Length; i++)
            {
                string[] suffix = (ext + "," + ext2).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                List<QueryParam> param = new List<QueryParam>();
                param.Add(new QueryParam("name", netArr[i]));
                param.Add(new QueryParam("enc", "E"));
                foreach (string s in suffix)
                {
                    param.Add(new QueryParam("suffix", s));
                }
                param.Add(new QueryParam("suffix", "")); //必须加上个空的，否则其不会查最后一个后缀名
                param.Add(new QueryParam("client", StationGroup.newNetClientID));
                DomNameHelper _XinNet = new DomNameHelper(ApiType.Check, param);
                resultDt.Merge(TrunToDataTable(_XinNet.HashtableResult));
            }
            //查询完毕,为结果加上价格
            if (resultDt.Rows.Count < 1)
            {
                ViewBag.strli = Strli;
                DomainStep = "reg";
                return View("Domain");
            }
            else
            {
                DomainStep = "one";
                return View("Domain", resultDt);
            }
        }
        //注册域名:第二步,加入至购物车
        public ActionResult TwoStep()
        {
            DataTable resultDt = new DataTable();
            decimal price = 0;
            string action = Request["action"];
            switch (action)
            {
                case "logon":
                    {
                        resultDt = DomCartDT;
                        price = DataConverter.CLng(Session["price"]);
                    }
                    break;
                case "viewcart":
                    {
                        resultDt = CookieCartDT;
                        price = 500;
                    }
                    break;
                case "clear":
                    {
                        CookieCartDT = null;
                        DomCartDT = null;
                    }
                    break;
                default:
                    {
                        string[] domNameArr = Request.Form["domainChk"].Split(',');
                        resultDt = CreateCartColumns();//内存购物车表
                        DataTable priceDT = idpBll.Sel();
                        for (int i = 0; i < domNameArr.Length; i++)
                        {
                            DataRow dr = resultDt.NewRow();
                            dr["DomName"] = domNameArr[i];
                            dr["DomPrice"] = idpBll.GetPrice(domNameArr[i], priceDT);
                            dr["Year"] = 1;
                            dr["Money"] = dr["DomPrice"];
                            price += DataConverter.CDecimal(dr["DomPrice"]);
                            resultDt.Rows.Add(dr);
                        }
                        DomCartDT = resultDt;
                        if (!buser.CheckLogin())//未登录
                        {
                            Session["price"] = price;//总计金额
                            Response.Redirect("/User/Login?ReturnUrl=" + Request.Url.PathAndQuery);
                        }
                    }
                    break;
            }
            //twoStep(resultDT, price.ToString());
            string defautValue = idtBll.SelValueByUserID(buser.GetLogin().UserID);
            resultDt.Merge(CookieCartDT);
            for (int i = 0; i < resultDt.Rows.Count; i++)//需要检测下是否能注册
            {
                DataRow dr = resultDt.Rows[i];
                dr["Index"] = i;
                dr["TempName"] = "默认值";
                dr["TempValue"] = defautValue;
                dr["IsOK"] = false;
            }
            resultDt = dtHelper.DistinctByField(resultDt, "DomName");
            CookieCartDT = resultDt;//写入Cookies中
            DomCartDT = resultDt;//存入购物车
            DomainStep = "two";
            ViewBag.purse = buser.GetLogin().Purse;
            ViewBag.price = price;
            return View("Domain", resultDt);
        }
        //第三步,结算,确定注册信息,补完内存中的购物车，价格,年限
        public ActionResult ThreeStep()
        {
            string[] domName = Request.Form["twoStepChk"].Split(',');
            string[] years = Request.Form["twoYearSelect"].Split(',');
            FilterCartDT(Request.Form["twoStepChk"]);
            FilterCookieCart(Request.Form["twoHid"]);
            for (int i = 0; i < DomCartDT.Rows.Count; i++)//加上年限,计算价格
            {
                DataRow dr = DomCartDT.Rows[i];
                if (dr["DomName"].ToString().Equals(domName[i]))
                {
                    dr["Year"] = years[i];
                    dr["Money"] = Convert.ToDouble(dr["DomPrice"]) * Convert.ToInt32(dr["Year"]);
                    dr["Isok"] = CheckIsOK(dr["TempValue"].ToString(), dr["DomName"].ToString());//这里加入检测模板数据是否齐全
                }
                else { function.WriteErrMsg("购物车与提交的数据不符"); }
            }
            DomainStep = "three";
            return View("Domain", DomCartDT);
        }
        //第四步,生成订单和购物车数据,跳转到支付页面(需要筛选)
        public void FourStep()
        {
            FilterCartDT(Request.Form["FourthHid"]);
            for (int i = 0; i < DomCartDT.Rows.Count; i++)
            {
                DataRow dr = DomCartDT.Rows[i];
                if (!CheckIsOK(dr["TempValue"].ToString(), dr["DomName"].ToString()))
                {
                    function.WriteErrMsg(dr["DomName"].ToString() + "信息不全!"); return;
                }
            }
            M_OrderList Odata = new M_OrderList();
            for (int i = 0; i < DomCartDT.Rows.Count; i++)//更新内存表
            {
                Odata.Ordersamount += Convert.ToDouble(DomCartDT.Rows[i]["Money"]);
            }
            Odata.OrderNo = idoBll.GenerateCodeNo();
            Odata.Ordertype = 5;
            Odata.Receiver = buser.GetLogin().UserName;
            Odata.Userid = buser.GetLogin().UserID;
            //添加订单，添加数据库购物车
            if (olistBll.Add(Odata))
            {
                DataTable tempDT = olistBll.GetOrderbyOrderNo(Odata.OrderNo);//获取刚插入的ID，这个需要改,应该插入时返回最新的ID
                if (tempDT != null & tempDT.Rows.Count > 0)
                {
                    AddToCart(DomCartDT, Convert.ToInt32(tempDT.Rows[0]["ID"]));
                    DomCartDT = null;//清空内存中的临时购物车
                    Response.Redirect("/PayOnline/Orderpay.aspx?OrderCode=" + Odata.OrderNo); return;
                }
                else
                {
                    function.WriteErrMsg("目标订单:" + Odata.OrderNo + "不存在!!!");
                }
            }
        }
        private DataTable TrunToDataTable(Hashtable ht)
        {
            DataTable dt = new DataTable();
            DataTable priceDT = idpBll.Sel();
            dt.Columns.Add(new DataColumn("Index", typeof(int)));
            dt.Columns.Add(new DataColumn("domName", typeof(string)));
            dt.Columns.Add(new DataColumn("isRegAble", typeof(string)));//能否注册
            dt.Columns.Add(new DataColumn("DomPrice", typeof(string)));
            int num = DataConverter.CLng(ht["num"].ToString());
            for (int i = 1; i < num; i++)
            {
                DataRow dr = dt.NewRow();
                dr["Index"] = i;
                dr["domName"] = ht["name" + i].ToString();
                dr["isRegAble"] = ht["chk" + i].ToString();
                if (ht["err"] != null) dr["isRegAble"] = "-1";//信息配置错误
                if (dr["isRegAble"].Equals("100"))//能正常注册，加载价格
                {
                    dr["DomPrice"] = idpBll.GetPrice(dr["domName"].ToString(), priceDT);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private DataTable CreateCartColumns()
        {
            DataTable resultDT = new DataTable();
            resultDT.Columns.Add(new DataColumn("Index", typeof(string)));
            resultDT.Columns.Add(new DataColumn("DomName", typeof(string)));
            resultDT.Columns.Add(new DataColumn("DomPrice", typeof(decimal)));
            resultDT.Columns.Add(new DataColumn("Year", typeof(int)));
            resultDT.Columns.Add(new DataColumn("Money", typeof(decimal)));
            resultDT.Columns.Add(new DataColumn("TempName", typeof(string)));
            resultDT.Columns.Add(new DataColumn("TempValue", typeof(string)));
            resultDT.Columns.Add(new DataColumn("IsOK", typeof(string)));//注册用的数据是否集全
            return resultDT;
        }
        //过滤内存或Cookies中的购物车，筛除已移除的信息,并更新Index
        private void FilterCartDT(string domName)
        {
            //已删除
            string sql = "DomName in(";
            foreach (string s in domName.Split(','))
            {
                sql += "'" + s + "',";
            }
            sql = sql.TrimEnd(','); sql += ")";
            DomCartDT.DefaultView.RowFilter = sql;
            DomCartDT = DomCartDT.DefaultView.ToTable();
            //重新生成索引
            for (int i = 0; i < DomCartDT.Rows.Count; i++)
            {
                DomCartDT.Rows[i]["Index"] = i;
            }
            DomCartDT = DomCartDT;
        }
        private void FilterCookieCart(string domName)
        {
            DataTable cartDT = CookieCartDT;
            string sql = "DomName in(";
            foreach (string s in domName.Split(','))
            {
                sql += "'" + s + "',";
            }
            sql = sql.TrimEnd(','); sql += ")";
            cartDT.DefaultView.RowFilter = sql;
            CookieCartDT = cartDT.DefaultView.ToTable();
        }//前方勾用于控是内存购物车,后方垃圾桶用于控制Cookie购物车
         //检测提供的注册信息，是否完整并正确
        private bool CheckIsOK(string value, string url, string year = "1")
        {
            //bool flag = false;
            string remind = "";
            string key = "uname1,uname2,rname1,rname2,aname1,aname2,aemail,ucity1,ucity2,uaddr1,uaddr2,uzip,uteln";//,ateln,ufaxa,ufaxn
            Dictionary<string, string> dic = DomNameHelper.ConvertToHashMap(value);
            return IsEmpty(dic, ref remind, key.Split(','));
        }
        private bool IsEmpty(Dictionary<string, string> dic, ref string remind, params string[] key)
        {
            for (int i = 0; i < key.Length; i++)
            {
                try
                {
                    if (string.IsNullOrEmpty(dic[key[i]]))
                    {
                        remind = key[i];
                        return false;
                    }
                }
                catch (KeyNotFoundException) { remind = key[i]; return false; }
            }
            return true;
        }
        //加入数据库,结算时需要
        private void AddToCart(DataTable dt, int orderID)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                M_CartPro cartModel = new M_CartPro();
                cartModel.Orderlistid = orderID;
                cartModel.Addtime = DateTime.Now;
                cartModel.Proname = dr["DomName"].ToString();
                cartModel.Shijia = Convert.ToDouble(dr["DomPrice"]);
                cartModel.Pronum = Convert.ToInt32(dr["Year"]);
                cartModel.AllMoney = Convert.ToDouble(dr["Money"]);
                cartModel.type = 3;
                cartModel.Attribute = dr["TempValue"].ToString();//模板信息,直接存内容,方便注册时修改
                cartBll.Add(cartModel);
            }
        }
        #endregion
    }
}
