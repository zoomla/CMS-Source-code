using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;


public partial class Manage_Site_DomReg : CustomerPageAction
{
    //订单
    protected B_OrderList OCl = new B_OrderList();
    protected M_OrderList Odata = new M_OrderList();
    //购物车
    protected B_CartPro bcart = new B_CartPro();
    protected B_IDC_DomainTemp  domTempBll = new B_IDC_DomainTemp();
    protected B_IDC_DomainPrice domPriceBll = new B_IDC_DomainPrice();
    protected B_IDC_DomainOrder orderBll = new B_IDC_DomainOrder();
    protected DataTableHelper dtHelper = new DataTableHelper();
    
    protected B_User buser = new B_User();
    protected string clientID, apiPasswd;
    public string remote = "";
    public string prompt = "请输入需要查询的域名，回车换行";
    public string ext { get { return Server.HtmlEncode(Request.Form["ext"]); } }
    public string ext2 { get { return Server.HtmlEncode(Request.Form["ext2"]); } }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //此处控件未实例化，不能对控件进行操作
        if (!string.IsNullOrEmpty(Request.QueryString["remote"]) && Request.QueryString["remote"].ToLower().Equals("true"))
        {
            this.MasterPageFile = "~/Common/Common.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        remote = Server.HtmlEncode(Request.QueryString["remote"]);
        clientID = StationGroup.newNetClientID;
        apiPasswd = StationGroup.newNetApiPasswd;
        if (!IsPostBack)
        {
            if (DomCartDT != null && buser.CheckLogin() && Session["price"] != null)//登录后跳转回来的
            {
                twoStep(DomCartDT, Session["price"].ToString());
                ClearSession();
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["op"])&&Request.QueryString["op"].ToLower().Equals("viewcart")&&buser.CheckLogin())
            {
                //查看购物车
                twoStep(CookieCartDT,"500");
            }
            else//正常进入页面
            {
                string[] arr = StationGroup.DefaultDisplay.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < arr.Length; i++)//显示哪些后缀
                {
                    Ul1.InnerHtml += "<li><input type='checkbox' name='ext' value='" + Server.HtmlEncode(arr[i]) + "' id='Checkbox2'/><label>" + Server.HtmlEncode(arr[i]) + "</label></li><li>";
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "setDefaultCheck('" + StationGroup.DefaultCheck + "');", true);
            }
        }
    }
    private DataTable TrunToDataTable(Hashtable ht)
    {
        DataTable dt = new DataTable();
        DataTable priceDT = domPriceBll.Sel();
        dt.Columns.Add(new DataColumn("Index", typeof(int)));
        dt.Columns.Add(new DataColumn("domName", typeof(string)));
        dt.Columns.Add(new DataColumn("isRegAble", typeof(string)));//能否注册
        dt.Columns.Add(new DataColumn("DomPrice", typeof(string)));
        int num = DataConvert.CLng(ht["num"].ToString());
        for (int i = 1; i < num; i++)
        {
            DataRow dr = dt.NewRow();
            dr["Index"] = i;
            dr["domName"] = ht["name" + i].ToString();
            dr["isRegAble"] = ht["chk" + i].ToString();
            if (ht["err"] != null) dr["isRegAble"] = "-1";//信息配置错误
            if (dr["isRegAble"].Equals("100"))//能正常注册，加载价格
            {
                dr["DomPrice"] = domPriceBll.GetPrice(dr["domName"].ToString(), priceDT);
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }
    public string GetCheck(object o,object p)
    {
        string success = "checked='checked'";
        string failed = "disabled='disabled'";
        string result = failed;
        if (p == null || string.IsNullOrEmpty(p.ToString()))//未定价格，不允许勾选
        { }
        else if (o.ToString().Equals("100"))
            result = success;
        return result;
    }
    public string GetRegAble(object o)
    {
        string result = "";
        if (o.ToString().Equals("100"))
            result = "可以注册";
        else if (o.ToString().Equals("0"))
            result = "(无法注册)";
        else result = "(查询失败：请检测API配置是否正确)";
        return result;
    }
    public string GetIsOK(object o) 
    {
        string result = "<img src='/App_Themes/AdminDefaultTheme/PromptSkin/images/err.gif' title='信息不完整' /> ";
        if (Convert.ToBoolean(o))
            result = "<img src='/App_Themes/AdminDefaultTheme/PromptSkin/images/right.gif' title='信息完整'/>";
        return result;
    }
    //检测提供的注册信息，是否完整并正确
    public bool CheckIsOK(string value,string url,string year="1") 
    {
        //bool flag = false;
        string remind="";
        string key = "uname1,uname2,rname1,rname2,aname1,aname2,aemail,ucity1,ucity2,uaddr1,uaddr2,uzip,uteln";//,ateln,ufaxa,ufaxn
        Dictionary<string, string> dic =DomNameHelper.ConvertToHashMap(value);

        return IsEmpty(dic, ref remind, key.Split(','));
        //后期加入测试注册
        //string uname1 = dic["uname1"];
        //string uname2 = dic["uname2"];
        //string ucity1 = dic["prvinceDP"] + dic["cityText"];
        //string aemail = dic["aemail"];
        //string checksum = DomNameHelper.MD5("Register" + clientID + apiPasswd + url + aemail + uname2, 32);//以32位
        //List<QueryParam> param = new List<QueryParam>();
        //param.Add(new QueryParam("dn", url));//域名
        //param.Add(new QueryParam("enc", "E"));
        //param.Add(new QueryParam("client", clientID));
        //param.Add(new QueryParam("period", year));//1-10年,不填默认1年
        ////param.Add(new QueryParam("checksum", checksum));//***MD5加密摘要,,
        ////英文必须有空格
        ////----注册信信息
        //param.Add(new QueryParam("uname1", uname1));//注册人|单位名称 中|英名称    [国内域名必填]|[国际域名必须]
        //param.Add(new QueryParam("uname2", uname2));//注册人|单位负责人 中|英名称    [国内域名必填]|[国际域名必须]
        //param.Add(new QueryParam("rname1", dic["rname1"]));//注册人|单位负责人 中|英名称    [国内域名必填]|[国际域名必须]
        //param.Add(new QueryParam("rname2", dic["rname2"]));
        //param.Add(new QueryParam("aname1", dic["rname1"]));//管理联系人 中|英名称   [国内域名必填]|[国际域名必须],与上方用同一信息
        //param.Add(new QueryParam("aname2", dic["rname2"]));
        //param.Add(new QueryParam("aemail", aemail));//管理联系人电子邮件地址                [必须]
        //param.Add(new QueryParam("ucity1", ucity1));//注册人城市名称 中|英名称    [国内域名必填]|[国际域名必须]
        //param.Add(new QueryParam("ucity2", "Cheng shi"));
        //param.Add(new QueryParam("uaddr1", dic["uaddr1"]));//通讯地址,中|英 [国内域名必填]|[国际域名必须]
        //param.Add(new QueryParam("uaddr2", dic["uaddr2"]));//dic["uaddr2"]
        //param.Add(new QueryParam("uzip", dic["uzip"]));//注册人邮政编码                    [必须]
        //param.Add(new QueryParam("uteln", dic["uteln"]));//注册人电话号码
        //param.Add(new QueryParam("ateln", ""));//
        //param.Add(new QueryParam("ufaxa", ""));  //传真区号 dic["ufaxa"] 
        //param.Add(new QueryParam("ufaxn", ""));//不能超过8位,与API的不能超过12位不同 dic["ufaxn"]

        //return flag;
    }
    public bool IsEmpty(Dictionary<string,string>dic,ref string remind,params string[] key)
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
    //查询:第一步
    protected void checkBtn_Click(object sender, EventArgs e)
    {
        if (Request.Form["domainBody"].Equals(prompt) || string.IsNullOrEmpty(Request.Form["domainBody"]))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(),"a","alert('请先输入域名');",true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "b", "setDefaultCheck('" + ext + "," + ext2 + "');", true);
            return; 
        }
        else if (string.IsNullOrEmpty(ext + ext2))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "alert('请选定需要查询的后缀名!');", true);
            return; 
        }
        string[] netAdd = Request.Form["domainBody"].Trim().Split('\n');//用回车切割
        DataTable resultDT = new DataTable();
        for (int i = 0; i < netAdd.Length; i++)
        {
            string[] suffix = (ext + "," + ext2).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            List<QueryParam> param = new List<QueryParam>();
            param.Add(new QueryParam("name", netAdd[i]));
            param.Add(new QueryParam("enc", "E"));
            foreach (string s in suffix)
            {
                param.Add(new QueryParam("suffix", s));
            }
            param.Add(new QueryParam("suffix", ""));//必须加上个空的，否则其不会查最后一个后缀名
            param.Add(new QueryParam("client", clientID));
            DomNameHelper _XinNet = new DomNameHelper(ApiType.Check, param);
            resultDT.Merge(TrunToDataTable(_XinNet.HashtableResult));
        }
        //查询完毕,为结果加上价格
        if (resultDT.Rows.Count < 1)
        {
            stepOneBtn.Enabled = false;
        }
        else
        {
            domRepeater.DataSource = resultDT;
            domRepeater.DataBind();
            stepOneDiv.Visible = true;
            stepTwoDiv.Visible = false;
            //stepThreeDiv.Visible = false;
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "setDefaultCheck('" + ext + ext2 + "');", true);
    }
    //注册域名:第二步,加入至购物车
    protected void stepOneBtn_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.Form["domainChk"])) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(),"","alert('未选定需要购买的域名!!');",true);
            return;
        }
        string[] domNameArr = Request.Form["domainChk"].Split(',');
        decimal price = 0;
        DataTable resultDT = CreateCartColumns();//内存购物车表
        DataTable priceDT = domPriceBll.Sel();
        for (int i = 0; i < domNameArr.Length; i++)
        {
            DataRow dr = resultDT.NewRow();
            dr["DomName"] = domNameArr[i];
            dr["DomPrice"] = domPriceBll.GetPrice(domNameArr[i], priceDT);
            dr["Year"] = 1;
            dr["Money"] = dr["DomPrice"];
            price += DataConvert.CDecimal(dr["DomPrice"]);
            resultDT.Rows.Add(dr);
        }
        DomCartDT = resultDT;
        if (!buser.CheckLogin())//未登录
        {
            Session["price"] = price;//总计金额
            Response.Redirect("/User/Login.aspx?ReturnUrl=" + Request.Url.PathAndQuery);
        }
        else
        {
            twoStep(resultDT, price.ToString());
        }
    }
    //显示第二步DIV等操作，分离出来，接受user跳转
    private void twoStep(DataTable resultDT, string price)
    {
        string defautValue = domTempBll.SelValueByUserID(buser.GetLogin().UserID);
        resultDT.Merge(CookieCartDT);
        for (int i = 0; i < resultDT.Rows.Count; i++)//需要检测下是否能注册
        {
            DataRow dr = resultDT.Rows[i];
            dr["Index"] = i;
            dr["TempName"] = "默认值";
            dr["TempValue"] = defautValue;
            dr["IsOK"] = false;
        }
        resultDT=dtHelper.DistinctByField(resultDT, "DomName");
        CookieCartDT = resultDT;//写入Cookies中
        DomCartDT = resultDT;//存入购物车
        regDiv.Visible = false;
        stepOneDiv.Visible = false;
        stepTwoDiv.Visible = true;
        purseL.Text = buser.GetLogin().Purse.ToString();
        allMoneyL.Text = price;
        resultMoneyL.Text = price;
        twoRepeater.DataSource = resultDT;
        twoRepeater.DataBind();
    }
    private void ClearSession() 
    {
        Session.Remove("resultDT");
        Session.Remove("price");
    }
    //第三步,结算,确定注册信息,补完内存中的购物车，价格,年限
    protected void stepTwoBtn_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.Form["twoStepChk"]))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('未选定需要购买的域名!!');", true);
            return;
        }

        string[] domName = Request.Form["twoStepChk"].Split(',');
        string[] years = Request.Form["twoYearSelect"].Split(',');
        stepOneDiv.Visible = false;
        stepTwoDiv.Visible = false;
        //stepThreeDiv.Visible = true;
        FilterCartDT(Request.Form["twoStepChk"]);
        FilterCookieCart(Request.Form["twoHid"]);
        for (int i = 0; i < DomCartDT.Rows.Count; i++)//加上年限,计算价格
        {
            DataRow dr =DomCartDT.Rows[i];
            if (dr["DomName"].ToString().Equals(domName[i]))
            {
                dr["Year"] = years[i];
                dr["Money"] = Convert.ToDouble(dr["DomPrice"]) * Convert.ToInt32(dr["Year"]);
                dr["Isok"] = CheckIsOK(dr["TempValue"].ToString(), dr["DomName"].ToString());//这里加入检测模板数据是否齐全
            }
            else { function.WriteErrMsg("购物车与提交的数据不符"); }
        }//For End;
        DomCartDT = DomCartDT;
        //stepThreeDiv.Visible = false;
        fourthRepeater.DataSource = DomCartDT;
        fourthRepeater.DataBind();
        stepFourDiv.Visible = true;
    }
    //第四步,生成订单和购物车数据,跳转到支付页面(需要筛选)
    protected void FourthBtn_Click(object sender, EventArgs e)
    {
        FilterCartDT(Request.Form["FourthHid"]);
        for (int i = 0; i < DomCartDT.Rows.Count; i++)
        {
            DataRow dr = DomCartDT.Rows[i];
            if (!CheckIsOK(dr["TempValue"].ToString(), dr["DomName"].ToString()))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + dr["DomName"].ToString() + "信息不全!');", true);
                return;
            }
        }

        for (int i = 0; i < DomCartDT.Rows.Count; i++)//更新内存表
        {
            Odata.Ordersamount += Convert.ToDouble(DomCartDT.Rows[i]["Money"]);
        }
        Odata.OrderNo = orderBll.GenerateCodeNo();
        Odata.Ordertype = 5;
        Odata.Receiver = buser.GetLogin().UserName;
        Odata.Userid = buser.GetLogin().UserID;
        //添加订单，添加数据库购物车
        if (OCl.Add(Odata))
        {
            DataTable tempDT = OCl.GetOrderbyOrderNo(Odata.OrderNo);//获取刚插入的ID，这个需要改,应该插入时返回最新的ID
            if (tempDT != null & tempDT.Rows.Count > 0)
            {
                AddToCart(DomCartDT, Convert.ToInt32(tempDT.Rows[0]["ID"]));
                DomCartDT = null;//清空内存中的临时购物车
                Response.Redirect("~/PayOnline/Orderpay.aspx?OrderCode=" + Odata.OrderNo);
            }
            else
            {
                function.WriteErrMsg("目标订单:" + Odata.OrderNo + "不存在!!!");
            }
        }
    }
    protected void CheckFuncBtn_Click(object sender, EventArgs e)
    {
        FilterCartDT(Request.Form["FourthHid"]);
        FilterCookieCart(Request.Form["FourthHid"]);
        for (int i = 0; i < DomCartDT.Rows.Count; i++)
        {
            DataRow dr = DomCartDT.Rows[i];
            dr["IsOK"] = CheckIsOK(dr["TempValue"].ToString(),dr["DomName"].ToString());
        }
        DomCartDT=DomCartDT;
        fourthRepeater.DataSource = DomCartDT;
        fourthRepeater.DataBind();
    }
    //加入数据库,结算时需要
    private void AddToCart(DataTable dt, int orderID)
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr=dt.Rows[i];
            M_CartPro cartModel = new M_CartPro();
            cartModel.Orderlistid = orderID;
            cartModel.Addtime = DateTime.Now;
            cartModel.Proname = dr["DomName"].ToString();
            cartModel.Shijia =Convert.ToDouble(dr["DomPrice"]);
            cartModel.Pronum = Convert.ToInt32(dr["Year"]);
            cartModel.AllMoney = Convert.ToDouble(dr["Money"]);
            cartModel.type = 3;
            cartModel.Attribute=dr["TempValue"].ToString();//模板信息,直接存内容,方便注册时修改
            bcart.Add(cartModel);
        }
    }
    //购物车Sessino,原数据存在页面控件,现改由存在服务端内存
    public DataTable DomCartDT
    {
        get {return Session["DomCartDT"] as DataTable;}
        set { Session["DomCartDT"] = value; }
    }
    //存入Cookies中的购物车
    public DataTable CookieCartDT 
    { 
        get
        {
            DataTable resultDT = CreateCartColumns();
            if (Request.Cookies["Dom"]!=null && !string.IsNullOrEmpty(Request.Cookies["Dom"]["DomName"]))//Cookies中存了购物车信息
            {
                DataTable priceDT = domPriceBll.Sel();
                string[] domName = Request.Cookies["Dom"]["DomName"].Split(',');
                for (int i = 0; i < domName.Length; i++)
                {
                    try
                    {
                        DataRow dr = resultDT.NewRow();
                        dr["DomName"] = domName[i];
                        dr["DomPrice"] = domPriceBll.GetPrice(domName[i], priceDT);
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
            DataTable resultDT=value as DataTable;
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
    public string GetTempValueFromDT(DataTable dt, int tempID)
    {
        string result = "";
        dt.DefaultView.RowFilter = "ID = " + tempID;
        if (dt.DefaultView.ToTable().Rows.Count > 0)
        {
            result = dt.DefaultView.ToTable().Rows[0]["TempValue"] as string;
        }
        return result;
    }
    //过滤内存或Cookies中的购物车，筛除已移除的信息,并更新Index
    public void FilterCartDT(string domName) 
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
    public void FilterCookieCart(string domName) 
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
    public DataTable CreateCartColumns() 
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
    protected void clearCartBtn_Click(object sender, EventArgs e)
    {
        CookieCartDT = null;
        twoRepeater.DataSource = CreateCartColumns();
        twoRepeater.DataBind();
    }
}