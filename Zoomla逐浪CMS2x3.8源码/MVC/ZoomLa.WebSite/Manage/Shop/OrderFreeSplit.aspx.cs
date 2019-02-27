using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class OrderFreeSplit : CustomerPageAction
    {
        private B_Cart bll = new B_Cart();
        private B_Model bmode = new B_Model();
        private B_CartPro cpl = new B_CartPro();
        private B_Product pll = new B_Product();
        private B_OrderList oll = new B_OrderList();
        private B_PayPlat Pay = new B_PayPlat();
        protected B_Stock Sll = new B_Stock();
        protected B_InvtoType binvtype = new B_InvtoType();
        protected B_OrderBaseField borderbasefiled = new B_OrderBaseField();
        protected B_User buser = new B_User();
        protected B_Group gll = new B_Group();
        protected int oid = 0;
        public string phone_sys = "";
        public string sendMsm_sys = "";
        public string sitename_sys = "";
        protected int PayClass = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOrder();
                Call.SetBreadCrumb(Master, "<li>商城管理</li><li>订单管理</li><li>自由拆分</li>");
            }
        }
        public void BindOrder()
        {
            int id = DataConverter.CLng(Request["id"]);

            DataTable cplist = cpl.GetCartProOrderID(id);
            procart.DataSource = cplist;
            procart.DataBind();
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
        public string getjiage(string proclass, string type, string proid)
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
            if (proclass == "3")
            {
                jiage = pinfo.PointVal.ToString();
            }
            return jiage;
        }
        public string shijia(string type, string ProID, string jiage)
        {
            double jia;
            jia = DataConverter.CDouble(jiage);
            double jiag = System.Math.Round(jia, 2);
            M_Product mpro = pll.GetproductByid(DataConverter.CLng(ProID));
            string jj = jiag.ToString();

            if (jj.IndexOf(".") == -1)
            {
                jj = jj + ".00";
            }
            if (type == "3")
            {
                jj = mpro.PointVal.ToString();
            }
            return jj;
        }
        public string getprojia(string pids, string cid)
        {
            int pid = DataConverter.CLng(cid);
            string jiage = "";
            M_CartPro minfo = cpl.SelReturnModel(pid);
            //double jiag = System.Math.Round(minfo.Shijia * minfo.Pronum, 2);
            double jiag = System.Math.Round(minfo.AllMoney, 2);
            jiage = jiag.ToString();
            if (jiage.IndexOf(".") == -1)
            {
                jiage = jiage + ".00";
            }
            M_Product pinfo = pll.GetproductByid(DataConverter.CLng(pids));
            if (pinfo != null && pinfo.ProClass == 3)
            {
                jiage = pinfo.PointVal.ToString();
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
        public string getProUnit(string proid)
        {
            int pid = DataConverter.CLng(proid);
            M_Product pinfo = pll.GetproductByid(pid);
            return pinfo.ProUnit.ToString();
        }
        //自由拆分订单
        protected void confirm_Click(object sender, EventArgs e)
        {
            M_OrderList list = new M_OrderList();
            int ordid = Convert.ToInt32(Request["id"]);
            M_OrderList row = oll.SelReturnModel(ordid);
            string ordno = row.OrderNo;
            string ordno1 = ordno + "01";
            string ordno2 = ordno + "02";
            DataTable dt = Sql.Sel("ZL_CartPro", "Orderlistid=" + ordid, "");
            if (dt.Rows.Count <= 1)
            {
                Response.Write("<script type=text/javascript>alert('只有一个商品，不能再拆分了！');</script>");
                return;
            }
            else
            {
                string id = Request["Item"];
                int ordlistid1 = 0;
                int ordlistid2 = 0;
                if (string.IsNullOrEmpty(Request["Item"]))
                {
                    return;
                }
                else
                {
                    string[] pid = id.Split(',');
                    if (CB_Yes.Checked)
                    {
                        for (int k = 0; k < dt.Rows.Count; k++)
                        {
                            string carpid = dt.Rows[k]["ProID"].ToString();
                            int cartpid = DataConverter.CLng(carpid);
                            int arr = Array.IndexOf(pid, carpid);
                            if (arr != -1)
                            {
                                //throw new Exception(Array.IndexOf(pid, carpid).ToString());


                            }
                            else
                            {

                            }
                        }
                        function.WriteSuccessMsg("恭喜，订单分拆成功!", "/manage/Shop/Orderlist.aspx?type=0");
                    }
                    else if (CB_No.Checked)
                    {
                        for (int k = 0; k < dt.Rows.Count; k++)
                        {
                            string carpid = dt.Rows[k]["ProID"].ToString();
                            int cartpid = DataConverter.CLng(carpid);
                            int arr = Array.IndexOf(pid, carpid);
                            if (arr != -1)
                            {
                                if (ordlistid1 == 0)
                                {
                                    row.OrderNo = ordno1;
                                    ordlistid1 = oll.Adds(row);
                                    Sql.Update("ZL_CartPro", "Orderlistid=" + ordlistid1, "Orderlistid=" + ordid + " and ProID=" + carpid, null);
                                }
                                else
                                {
                                    Sql.Update("ZL_CartPro", "Orderlistid=" + ordlistid1, "Orderlistid=" + ordid + " and ProID=" + carpid, null);
                                }

                            }
                            else
                            {
                                if (ordlistid2 == 0)
                                {
                                    row.OrderNo = ordno2;
                                    ordlistid2 = oll.Adds(row);
                                    Sql.Update("ZL_CartPro", "Orderlistid=" + ordlistid2, "Orderlistid=" + ordid + " and ProID=" + carpid, null);
                                }
                                else
                                {
                                    Sql.Update("ZL_CartPro", "Orderlistid=" + ordlistid2, "Orderlistid=" + ordid + " and ProID=" + carpid, null);
                                }
                            }
                        }
                        oll.UpdateByField("Aside", "1", ordid);
                        function.WriteSuccessMsg("恭喜，订单分拆成功!", "/manage/Shop/Orderlist.aspx?type=0");
                    }
                }
            }
        }
    }
}