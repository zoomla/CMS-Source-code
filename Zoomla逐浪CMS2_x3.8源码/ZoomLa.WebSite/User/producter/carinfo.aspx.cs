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

public partial class User_producter_carinfo : System.Web.UI.Page
{
    private B_Cart bll = new B_Cart();
    private B_Node bNode = new B_Node();
    private B_Model bmode = new B_Model();
    private B_CartPro cpl = new B_CartPro();
    private B_Product pll = new B_Product();
    private B_OrderList oll = new B_OrderList();
    private B_PayPlat Pay = new B_PayPlat();
    protected B_Stock Sll = new B_Stock();
    private B_CartPro proo = new B_CartPro();
    private B_Product prod = new B_Product();
    protected void Page_Load(object sender, EventArgs e)
    {
        int id = DataConverter.CLng(Request.QueryString["id"]);
        Label1.Text = "订 单 信 息";
        if (oll.FondOrder(id) == true)
        {
            M_OrderList orderinfo = oll.GetOrderListByid(id);
            string OrderNo = orderinfo.OrderNo.ToString();
            Label1.Text = Label1.Text + "（订单编号：" + OrderNo + "）";
            string dingid = orderinfo.id.ToString();
            this.Reuser.Text = orderinfo.Reuser.ToString();
            this.Rename.Text = orderinfo.Rename.ToString();
            if (orderinfo.OrderStatus == 1)
            {
                OrderStatus.Text = "<font color=blue>已经确认</font>";

            }
            else
            {
                OrderStatus.Text = "<font color=red>等待确认</font>";

            }

            this.adddate.Text = orderinfo.AddTime.ToShortDateString();
            this.addtime.Text = orderinfo.AddTime.ToString();

            if (orderinfo.Invoiceneeds == 1)
            {
                this.Invoiceneeds.Text = "<font color=blue>√</font>";
            }
            else
            {
                this.Invoiceneeds.Text = "<font color=red>×</font>";
            }

            if (orderinfo.Developedvotes == 1)
            {
                this.Developedvotes.Text = "<font color=blue>√</font>";
            }
            else
            {
                this.Developedvotes.Text = "<font color=red>×</font>";
            }
            if (orderinfo.Paymentstatus == (int)M_OrderList.PayEnum.HasPayed)
            {
                this.Paymentstatus.Text = "<font color=blue>已经汇款</font>";
            }
            else
            {
                this.Paymentstatus.Text = "<font color=red>等待汇款</font>";

            }

            if (orderinfo.StateLogistics == 1)
            {
                this.StateLogistics.Text = "<font color=blue>已发货</font>";

            }
            else
            {
                this.StateLogistics.Text = "<font color=red>配送中</font>";

            }



            //if (orderinfo.OrderStatus == 1)
            //{
            //  Button5.Enabled=false;
            //}
            //else
            //{
            //   Button5.Enabled=true;
            //}




            int paymenst = orderinfo.Paymentstatus;
            int Settles = orderinfo.Settle;



            M_PayPlat ppay = Pay.GetPayPlatByid(orderinfo.Payment);

            this.Reusers.Text = orderinfo.Reuser.ToString();//订货人
            this.Jiedao.Text = orderinfo.Jiedao.ToString();//联系地址
            this.Email.Text = orderinfo.Email.ToString();//电子信箱
            this.Invoice.Text = orderinfo.Invoice.ToString();//发票信息
            //this.Outstock.Text = orderinfo.Outstock.ToString();//缺货处理
            if (orderinfo.Outstock == 1)
            {
                this.Outstock.Text = "缺货时，取消此订单";
            }
            else
            {
                this.Outstock.Text = "缺货时，将有货的商品发出，取消无货商品的订购";
            }

            //this.Ordertype.Text = orderinfo.Ordertype.ToString();//订单类型
            switch (orderinfo.Ordertype)
            {
                case 1:
                    this.Ordertype.Text = "正常订单";
                    break;
                case 2:
                    this.Ordertype.Text = "缺货的订单";
                    break;
                case 3:
                    this.Ordertype.Text = "有问题的订单";
                    break;
                default:
                    this.Ordertype.Text = "正常订单";
                    break;
            }
            this.Deliverytime.Text = orderinfo.Deliverytime.ToString();//送货时间
            switch (orderinfo.Deliverytime)
            {
                case 1:
                    this.Deliverytime.Text = "对送货时间没有特殊要求";
                    break;
                case 2:
                    this.Deliverytime.Text = "双休日或者周一至周五的晚上送达";
                    break;
                case 3:
                    this.Deliverytime.Text = "周一至周五的白天送达";
                    break;
                default:
                    break;
            }

            this.Phone.Text = orderinfo.Phone.ToString();//联系电话
            this.ZipCode.Text = orderinfo.ZipCode.ToString();//邮政编码
            this.Mobile.Text = orderinfo.Mobile.ToString();//手机
            try
            {
                this.Payment.Text = ppay.PayPlatName.ToString();//付款方式
            }
            catch 
            {

            }
            this.AddUser.Text = orderinfo.AddUser.ToString();//负责跟单人员
            this.Internalrecords.Text = orderinfo.Internalrecords.ToString();//内部记录
            this.Ordermessage.Text = orderinfo.Ordermessage.ToString();//订货留言

            DataTable cplist = cpl.GetCartProOrderID(id);
            procart.DataSource = cplist;
            procart.DataBind();
            DataTable newtable = cplist.DefaultView.ToTable(false, "Shijia", "Pronum");
            double allmoney = 0;
            for (int i = 0; i < newtable.Rows.Count; i++)
            {
                //allmoney = allmoney + DataConverter.CDouble(newtable.Rows[i][1].ToString()) * DataConverter.CDouble(newtable.Rows[i][0].ToString());
                allmoney = allmoney + DataConverter.CDouble(cplist.Rows[i]["Allmoney"]);
            }

            double jia = System.Math.Round(allmoney, 2);
            string jiage = jia.ToString();

            if (jiage.IndexOf(".") == -1)
            {
                jiage = jiage + ".00";
            }



            int Orderid = DataConverter.CLng(Request.QueryString["id"].ToString());
            M_OrderList aa = oll.GetOrderListByid(Orderid);
            Label2.Text = jiage.ToString();
            Label29.Text = jiage.ToString();
            double pp = orderinfo.Receivablesamount;
            pp = System.Math.Round(pp, 2);
            string temppp = pp.ToString();
            if (temppp.IndexOf(".") == -1)
            {
                temppp = temppp + ".00";
            }

            Label28.Text = temppp.ToString();

        }
        else
        {
            function.WriteErrMsg("找不到购物车信息");
        }
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
            restring = "<img src=../../" + type + " width=60 height=45>";
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


    public string beizhu(string proid)
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
            restring = "<font color=red>特价处理</font>";
        }
        else if (newtype == 1)
        {
            restring = "<font color=blue>正常销售</font>";
        }
        return restring;
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

    public string getshichangjiage(string cid)
    {
        int pid = DataConverter.CLng(cid);
        string jiage = "";
        M_Product mpro = pll.GetproductByid(pid);

        double jiag = System.Math.Round(mpro.ShiPrice, 2);

        jiage = jiag.ToString();
        if (jiage.IndexOf(".") == -1)
        {
            jiage = jiage + ".00";
        }
        return jiage;
    }


    public string qixian(string cid)
    {
        int pid = DataConverter.CLng(cid);
        string m1 = "", m2 = "";
        M_Product mpro = pll.GetproductByid(pid);
        m1 = mpro.ServerPeriod.ToString();
        int m3 = mpro.ServerType;
        switch (m3)
        {
            case 0:
                m2 = " 年";
                break;
            case 1:
                m2 = " 月";
                break;
            case 2:
                m2 = " 日";
                break;
            default:
                break;
        }

        return m1 + m2;
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



    protected string Getprotype(string proid)
    {
        int id = DataConverter.CLng(proid);
        if (pll.GetproductByid(id).Priority == 1 && pll.GetproductByid(id).Propeid > 0)
        {
            return "<font color=green>[绑]</font>";
        }
        else
        {
            return "";
        }
    }




    protected void Button6_Click(object sender, EventArgs e)//订单作废
    {
        int id = DataConverter.CLng(Request.QueryString["id"]);
        string str = "Aside=1";
        oll.UpOrderinfo(str, id);
        Response.Write("<script language=javascript>location.href='Orderlistinfo.aspx?id=" + id + "';</script>");

    }


    public void cartinfo_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater rpColumnNews = (Repeater)e.Item.FindControl("Repeater1");
            DataRowView rowv = (DataRowView)e.Item.DataItem;
            string proid = rowv["Bindpro"].ToString();
            if (!string.IsNullOrEmpty(proid))
            {
                DataTable ddinfos = pll.Souchprolist(proid);
                rpColumnNews.DataSource = ddinfos.DefaultView;
                rpColumnNews.DataBind();
            }
        }
    }
}
