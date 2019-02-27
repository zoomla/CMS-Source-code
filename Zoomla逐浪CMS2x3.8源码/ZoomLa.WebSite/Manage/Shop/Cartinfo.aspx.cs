using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Text;
using ZoomLa.Components;
using System.Globalization;

public partial class manage_Shop_Cartinfo : CustomerPageAction
{
    private B_Cart bll = new B_Cart();
    private B_Model bmode = new B_Model();
    private B_CartPro cpl = new B_CartPro();
    private B_Product pll = new B_Product();
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin badmin = new B_Admin();
        if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "CartManage"))
        {
            function.WriteErrMsg("没有权限进行此项操作");
        }
        int id = DataConverter.CLng(Request.QueryString["id"]);
        Label1.Text = "购 物 车 信 息";

       
        Call.SetBreadCrumb(Master, "<li>商城管理</li><li><a href='CartManage.aspx'>购物车</a></li><li>购物车信息</li>");
    }


    public string getproimg(string proid)
    {
        int pid = DataConverter.CLng(proid);
        M_Product pinfo = pll.GetproductByid(pid);
        string restring, type;

        restring = ""; 
        type = pinfo.Thumbnails;
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

    public string getProUnit(string proid)
    {
        int pid = DataConverter.CLng(proid);
        M_Product pinfo = pll.GetproductByid(pid);
        return pinfo.ProUnit.ToString();
    }

    public string getjiage(string type, string proid)
    {
        int pid = DataConverter.CLng(proid);
        int ptype = DataConverter.CLng(type);
        M_Product pinfo = pll.GetproductByid(pid);
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

        return jiage;
    }

    public string formatnewstype(string proid)
    {
        int pid = DataConverter.CLng(proid);
        M_Product pinfo = pll.GetproductByid(pid);
        int type = pinfo.ProClass;
        int newtype;
        string restring;
        restring = "";
        newtype = DataConverter.CLng(type.ToString());

        if (newtype == 2)
        {
            restring = "<font color=red>特价</font>";
        }
        else if (newtype == 1)
        {
            restring = "<font color=blue>正常</font>";
        }
        return restring;
    }

    public string getprojia(string cid)
    {
        int pid = DataConverter.CLng(cid);
        string jiage = "";
        M_CartPro minfo = cpl.GetCartProByid(pid);
        //double jiag = System.Math.Round(minfo.Shijia * minfo.Pronum, 2);
        double jiag = System.Math.Round(minfo.AllMoney, 2);
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
}
