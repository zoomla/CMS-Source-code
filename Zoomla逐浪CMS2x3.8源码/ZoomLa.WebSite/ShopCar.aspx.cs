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

public partial class ShopCar : System.Web.UI.Page
{
    #region 加载业务逻辑
    protected B_Product bll = new B_Product();
    protected B_User buser = new B_User();
    protected B_CartPro Cll = new B_CartPro();
    protected B_Cart ACl = new B_Cart();

    protected B_Cart cartBll = new B_Cart();
    protected B_CartPro cartproBll = new B_CartPro();

    protected XmlDocument objXmlDoc = new XmlDocument();
    protected string filename;
    protected DataSet fileset = new DataSet();
    protected DataTable infotable;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

      
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
        if (type == "3")
        {
            jiage = pinfo.PointVal.ToString();
        }
        if (jiage.IndexOf(".") == -1)
        {
            if (jiage == "")
            {
                jiage = "0.00";
            }
            else
            {
                jiage = jiage + ".00";
            }
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

        double jiag = System.Math.Round(minfo.AllMoney);



        if (proinfo.ProClass == 3)
        {
            jiag = minfo.Shijia * minfo.Pronum;
        }
        #region 打折信息
        double dazhe = double.Parse(getproscheme(cid));
        jiag = jiag * (dazhe * 0.1);
        #endregion
        //throw new Exception(jiag.ToString());
        jiage = jiag.ToString("F2");
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
    private int SetProduct()
    {
       //buser.CheckIsLogin();
        string adminname = "";
        int ComModelID = 0;
        //string adminname = StringHelper.Base64StringDecode(buser.GetLogin().UserName);
        M_Product CData = new M_Product();

        CData.Class = 0;
        CData.Proname = Server.HtmlEncode(Request.Form["Proname"]);
        CData.ServerType = 3;
        CData.ProClass = DataConverter.CLng(ProClass.Value) <= 0 ? 1 : DataConverter.CLng(ProClass.Value);
        CData.Properties = 0;
        CData.Sales = 1;
        CData.Proinfo = Server.HtmlEncode(Request.Form["Proinfo"]);
        CData.Procontent = Server.HtmlEncode(Request.Form["ProUrl"]);
        CData.ProSeller = Server.HtmlEncode(Request.Form["ProSeller"]);
       // CData.BarCode = string.IsNullOrEmpty(Request.Form["BarCode"]) ? Request.QueryString["BarCode"] : Request.Form["BarCode"];
        CData.BarCode = Server.HtmlEncode(Request.Params["BarCode"]);

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
        CData.AddUser = adminname;
        CData.DownCar = 0;
        CData.AddTime = DateTime.Now;

        CData.Istrue = 1;
        CData.Isgood = 0;
        CData.MakeHtml = 0;
        CData.Thumbnails = Request["Thumbnails"];
        ComModelID = this.bll.AddCommodities(CData);
        return ComModelID;
    }
    #endregion
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Allnum.Text) || Allnum.Text.Equals("0"))
        {
            Response.Write("<script>alert('购物车没有需要支付的商品！');location.href=window.location.href;</script>");
            yhqtext.Text = null;
            yhqpwd.Text = null;
            return;
        }
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
                if (Allnum.Text == null || Allnum.Text == "")
                {
                    Response.Write("<script>alert('购物车没有需要支付的商品！');location.href=window.location.href;</script>");
                    yhqtext.Text = null;
                    yhqpwd.Text = null;
                }
                else if (Allnum.Text == "0")
                {
                    Response.Write("<script>alert('购物车没有需要支付的商品！');location.href=window.location.href;</script>");
                    yhqtext.Text = null;
                    yhqpwd.Text = null;
                }
                else
                {
                    string setPrice = "";

                    if (!string.IsNullOrEmpty(alljiage.Text) && !string.IsNullOrEmpty(setPrice))
                    { }
                    decimal jia = Convert.ToDecimal(alljiage.Text);
                    decimal price = Convert.ToDecimal(setPrice);

                    if (jia < price || jia < 0)
                    {
                        Response.Write("<script>alert('抱歉，您的订单没有达到系统最小订单金额[" + price + "]元，点此返回首页继续购物。');location.href=window.location.href;</script>");
                    }
                    else
                    {

                        if (yhqtext.Text == "" && yhqpwd.Text == "")
                        {
                            Response.Redirect("/User/Shopfee/Moneytop.aspx?ProClass=" + ProClass.Value);
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
                                    Response.Write("<script>alert('" + tt + "'); location='/User/Shopfee/Moneytop.aspx?ProClass=" + ProClass.Value + "';</script>");
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
        else
        {
            if (Allnum.Text == null)
            {
                Response.Write("<script>alert('购物车没有需要支付的商品！');location.href=window.location.href;</script>");
                yhqtext.Text = null;
                yhqpwd.Text = null;
            }
            else if (Allnum.Text == "0")
            {
                Response.Write("<script>alert('购物车没有需要支付的商品！');location.href=window.location.href;</script>");
                yhqtext.Text = null;
                yhqpwd.Text = null;
            }
            else
            {
                 string setPrice = "";
                decimal jia = Convert.ToDecimal(alljiage.Text);
                decimal price = Convert.ToDecimal(setPrice);
                if (jia < price || jia < 0)
                {
                    Response.Write("<script>alert('抱歉，您的订单没有达到系统最小订单金额[" + price + "]元，点此返回首页继续购物。');location.href=window.location.href;</script>");
                }
                else
                {
                    if (yhqtext.Text == "" && yhqpwd.Text == "")
                    {
                        Response.Redirect("/User/Shopfee/Moneytop.aspx?ProClass=" + ProClass.Value );
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
                                Response.Write("<script>alert('" + tt + "'); location='/User/Shopfee/Moneytop.aspx?ProClass=" + ProClass.Value + "';</script>");
                                b.UpdateState(yt);
                                b.UpdateUseTime(yt);
                            }
                            else
                            {
                                Response.Write("<script>alert('优惠券价格高于商品总价，无法使用！')</script>");
                            }
                        }
                    }
                }
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
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
                    Response.Redirect("/User/Shopfee/AdUserOrderinfo.aspx?ProClass=" + ProClass.Value);
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
                            Response.Write("<script>alert('" + tt + "'); location='/User/Shopfee/Moneytop.aspx?ProClass=" + ProClass.Value + "';</script>");
                            b.UpdateState(yt);
                            b.UpdateUseTime(yt);
                        }
                        else
                        {
                            Response.Write("<script>alert('优惠券价格高于商品总价，无法使用！')</script>");
                        }
                    }
                }
            }
    }
}