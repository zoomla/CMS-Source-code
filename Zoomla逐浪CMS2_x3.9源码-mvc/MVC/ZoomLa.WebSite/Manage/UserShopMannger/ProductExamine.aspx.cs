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

namespace ZoomLaCMS.Manage.UserShopMannger
{
    public partial class ProductExamine : CustomerPageAction
    {
        private B_User buser = new B_User();
        private B_Product bll = new B_Product();
        private B_Node bNode = new B_Node();
        private B_Model bmode = new B_Model();
        B_UserStoreTable ustbll = new B_UserStoreTable();

        protected int NodeID;
        protected string flag;
        protected int tempnodeid;

        private int uid
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["UserState"]["UserID"] != null)
                    return int.Parse(HttpContext.Current.Request.Cookies["UserState"]["UserID"].ToString());
                else
                    return 0;
            }
            set
            {
                uid = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            if (!B_ARoleAuth.Check(ZLEnum.Auth.store, "StoreProductManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }

            int quicksouch;
            tempnodeid = DataConverter.CLng(base.Request.QueryString["NodeID"]);


            if (string.IsNullOrEmpty(base.Request.QueryString["NodeID"]))
            {
                this.NodeID = 20;
            }
            else
            {
                this.NodeID = DataConverter.CLng(base.Request.QueryString["NodeID"]);
                if (this.NodeID == 0)
                    this.NodeID = 20;
            }

            if (this.NodeID == 0)
                function.WriteErrMsg("没有指定栏目节点ID或没有建立栏目节点");

            this.flag = string.IsNullOrEmpty(base.Request.QueryString["type"]) ? "" : base.Request.QueryString["type"];
            this.ViewState["NodeID"] = this.NodeID.ToString();
            this.ViewState["flag"] = this.flag;


            M_Node nod = this.bNode.GetNodeXML(this.NodeID);
            string[] ModelID = null;
            string ModeIDList = nod.ContentModel;
            if (ModeIDList != null)
            {
                ModelID = ModeIDList.Split(new char[] { ',' });
            }
            string AddContentlink = "";

            string menu;
            menu = Request.QueryString["menu"];
            switch (menu)
            {
                case "delete":
                    int pid;
                    string messageinfos;
                    pid = DataConverter.CLng(Request.QueryString["id"]);
                    bool delok = bll.DeleteByID(pid, bll.GetproductByid(pid));
                    if (delok == true)
                    {
                        messageinfos = "操作成功!";
                    }
                    else
                    {
                        messageinfos = "操作失败!";
                    }

                    Response.Write("<script language='javascript'>alert('" + messageinfos + "');location.href='ProductExamine.aspx?NodeID=" + this.NodeID + "';</script>");


                    break;
            }

            int CPage;
            int temppage;
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




            if (Request.Form["quicksouch"] == null)
            {
                quicksouch = DataConverter.CLng(Request.QueryString["quicksouch"]);
            }
            else
            {
                quicksouch = DataConverter.CLng(Request.Form["quicksouch"]);
            }

            this.quicksouch.Text = quicksouch.ToString();

            //搜索产品
            string souchtable = "";
            int stype;
            souchtable = Server.HtmlEncode(Request.QueryString["key"]);
            stype = DataConverter.CLng(Request.QueryString["souchtype"]);

            DataTable Cll = new DataTable();
            if (souchtable == "" || souchtable == null || quicksouch > 0)
            {
                //switch (quicksouch)
                //{
                //    case 1:
                //Cll = bll.ProductApply();
                //        break;
                //    case 2:
                //        Cll = bll.ProductSalesare(1, uid);
                //        break;
                //    case 3:
                //        Cll = bll.ProductNotsale(uid);
                //        break;
                //    case 4:
                //        Cll = bll.Normalsales(uid);
                //        break;
                //    case 5:
                //        Cll = bll.Special(uid);
                //        break;
                //    case 6:
                //        Cll = bll.ProductSold(0, uid);
                //        break;
                //    case 7:
                //        Cll = bll.ProductNew(0, uid);
                //        break;
                //    case 8:
                //        Cll = bll.ProductFine(0, uid);
                //        break;
                //    case 9:
                //        Cll = bll.ProductPreset(uid);
                //        break;
                //    case 10:
                //        Cll = bll.ProductAlarm(uid);
                //        break;
                //    case 11:
                //        Cll = bll.ProductCategory(uid);
                //        break;
                //    case 12:
                //        Cll = bll.Productsoldout(0, uid);
                //        break;
                //    case 13:
                //        Cll = bll.ProductWholesale(uid);
                //        break;
                //    default:
                //        Cll = bll.GetUserProductAll(uid);
                //        break;
                //}


                //推荐商品
                //string fla;
                //fla = Request.QueryString["flag"];
                //if (fla == "Elite")
                //{
                //    Cll = bll.Productjian(uid);
                //}

            }
            else
            {
                //Cll = bll.ProductSearch(stype, souchtable, uid);
            }

            PagedDataSource cc = new PagedDataSource();
            cc.DataSource = Cll.DefaultView;
            cc.AllowPaging = true;
            cc.PageSize = 10;
            cc.CurrentPageIndex = CPage - 1;
            Productlist.DataSource = cc;
            Productlist.DataBind();

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

            Toppage.Text = "<a href=?NodeID=" + this.tempnodeid + "&quicksouch=" + quicksouch + "&souchtype=" + stype + "&key=" + souchtable + "&Currentpage=0>首页</a>";
            Nextpage.Text = "<a href=?NodeID=" + this.tempnodeid + "&quicksouch=" + quicksouch + "&souchtype=" + stype + "&key=" + souchtable + "&Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
            Downpage.Text = "<a href=?NodeID=" + this.tempnodeid + "&quicksouch=" + quicksouch + "&souchtype=" + stype + "&key=" + souchtable + "&Currentpage=" + downpagenum.ToString() + ">下一页</a>";
            Endpage.Text = "<a href=?NodeID=" + this.tempnodeid + "&quicksouch=" + quicksouch + "&souchtype=" + stype + "&key=" + souchtable + "&Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
            Nowpage.Text = CPage.ToString();
            PageSize.Text = thispagenull.ToString();
            pagess.Text = cc.PageSize.ToString();

            if (!this.Page.IsPostBack)
            {
                for (int i = 1; i <= thispagenull; i++)
                {
                    DropDownList1.Items.Add(i.ToString());
                }
                if (ModelID != null)
                {
                    for (int i = 0; i < ModelID.Length; i++)
                    {
                        AddContentlink = AddContentlink + "&nbsp;|&nbsp;<a href=\"ProductAdd.aspx?ModelID=" + ModelID[i] + "&NodeID=" + this.NodeID + "\">添加" + this.bmode.GetModelById(DataConverter.CLng(ModelID[i])).ItemName + "</a>";
                    }
                }
                //this.lblAddContent.Text = AddContentlink;
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='" + customPath2 + "Shop/StoreManage.aspx'>店铺管理</a></li><li>商品列表</li>");
            }
        }

        public string formattype(string type)
        {
            int newtype;
            string restring;
            restring = "";
            newtype = DataConverter.CLng(type.ToString());
            if (newtype == 1)
            {
                restring = "<font color=blue>√</font>";
            }
            else if (newtype == 0)
            {
                restring = "<font color=red>×</font>";
            }
            return restring;
        }
        protected string GetUsername(string userid)
        {
            return buser.GetUserByUserID(int.Parse(userid)).UserName;
        }
        protected string GetUserStore(string userid)
        {
            return ustbll.GetStoreTableByUserID(int.Parse(userid)).StoreName;
        }

        public string Stockshow(string id)
        {
            int cid;
            string str = "";
            cid = DataConverter.CLng(id);
            M_Product dd = bll.GetproductByid(cid);
            if (dd.Stock <= dd.StockDown)
            {
                str = "<font color=red>" + dd.Stock.ToString() + " [警]</font>";
            }
            else
            {
                str = dd.Stock.ToString();
            }
            return str;

        }

        public string formatnewstype(string type)
        {
            int newtype;
            string restring;
            restring = "";
            newtype = DataConverter.CLng(type.ToString());
            if (newtype == 2)
            {
                restring = "<font color=red>待审核</font>";
            }
            else if (newtype == 1)
            {
                restring = "<font color=blue>正常</font>";
            }
            return restring;
        }

        public string formatcs(string money)
        {
            string outstr;
            double doumoney, tempmoney;
            doumoney = DataConverter.CDouble(money);
            tempmoney = System.Math.Round(doumoney, 2);
            outstr = tempmoney.ToString();
            if (outstr.IndexOf(".") == -1)
            {
                outstr = outstr + ".00";
            }
            return outstr;
        }

        public string forisnew(string type)
        {
            int newtype;
            string restring;
            restring = "";
            newtype = DataConverter.CLng(type.ToString());
            if (newtype == 1)
            {
                restring = "<font color=green>新</font>";
            }
            else if (newtype == 0)
            {
                restring = "&nbsp;&nbsp;";
            }
            return restring;
        }

        public string forishot(string type)
        {
            int newtype;
            string restring;
            restring = "";
            newtype = DataConverter.CLng(type.ToString());
            if (newtype == 1)
            {
                restring = "<font color=red>热</font>";
            }
            else if (newtype == 0)
            {
                restring = "&nbsp;&nbsp;";
            }
            return restring;
        }
        public string forisbest(string type)
        {
            int newtype;
            string restring;
            restring = "";
            newtype = DataConverter.CLng(type.ToString());
            if (newtype == 1)
            {
                restring = "<font color=blue>精</font>";
            }
            else if (newtype == 0)
            {
                restring = "&nbsp;&nbsp;";
            }
            return restring;
        }


        public string getproimg(string type)
        {
            string restring;
            restring = "";

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

        protected void souchok_Click(object sender, EventArgs e)
        {
            string souchtype = souchtable.Text;
            string key = souchkey.Text;
            Response.Redirect("ProductExamine.aspx?souchtype=" + souchtype + "&key=" + Server.UrlDecode(key));
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            string CID = Request.Form["Item"];
            if (!String.IsNullOrEmpty(CID) && bll.setproduct(1, CID))
            {
                Response.Write("<script language=javascript>alert('批量设置为开始销售成功!');location.href='Productlist.aspx?NodeID=" + this.NodeID + "';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('批量设置为开始销售失败!请选择您要设置的数据');location.href='Productlist.aspx?NodeID=" + this.NodeID + "';</script>");
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            string CID = Request.Form["Item"];
            if (!String.IsNullOrEmpty(CID) && bll.setproduct(2, CID))
            {
                Response.Write("<script language=javascript>alert('批量设置为热卖成功!');location.href='Productlist.aspx?NodeID=" + this.NodeID + "';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('批量设置为热卖失败!请选择您要设置的数据');location.href='Productlist.aspx?NodeID=" + this.NodeID + "';</script>");
            }
        }
        protected void Button6_Click(object sender, EventArgs e)
        {
            string CID = Request.Form["Item"];
            if (!String.IsNullOrEmpty(CID) && bll.setproduct(3, CID))
            {
                Response.Write("<script language=javascript>alert('批量设为精品成功!');location.href='Productlist.aspx?NodeID=" + this.NodeID + "';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('批量设为精品失败!请选择您要设置的数据');location.href='Productlist.aspx?NodeID=" + this.NodeID + "';</script>");
            }
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            string CID = Request.Form["Item"];
            if (!String.IsNullOrEmpty(CID) && bll.setproduct(4, CID))
            {
                Response.Write("<script language=javascript>alert('批量设为新品成功!');location.href='Productlist.aspx?NodeID=" + this.NodeID + "';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('批量设为新品失败!请选择您要设置的数据');location.href='Productlist.aspx?NodeID=" + this.NodeID + "';</script>");
            }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            string CID = Request.Form["Item"];
            if (!String.IsNullOrEmpty(CID) && bll.setproduct(5, CID))
            {
                Response.Write("<script language=javascript>alert('批量审核成功!');location.href='ProductExamine.aspx?NodeID=" + this.NodeID + "';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('批量删除失败!请选择您要删除的数据');location.href='Productlist.aspx?NodeID=" + this.NodeID + "';</script>");
            }
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            string CID = Request.Form["Item"];
            if (!String.IsNullOrEmpty(CID) && bll.setproduct(6, CID))
            {
                Response.Write("<script language=javascript>alert('批量停止销售成功!');location.href='Productlist.aspx?NodeID=" + this.NodeID + "';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('批量停止销售失败!请选择您要设置的数据');location.href='Productlist.aspx?NodeID=" + this.NodeID + "';</script>");
            }
        }
        protected void Button7_Click(object sender, EventArgs e)
        {
            string CID = Request.Form["Item"];
            if (!String.IsNullOrEmpty(CID) && bll.setproduct(7, CID))
            {
                Response.Write("<script language=javascript>alert('批量取消热卖成功!');location.href='Productlist.aspx?NodeID=" + this.NodeID + "';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('批量取消热卖失败!请选择您要设置的数据');location.href='Productlist.aspx?NodeID=" + this.NodeID + "';</script>");
            }
        }
        protected void Button8_Click(object sender, EventArgs e)
        {
            string CID = Request.Form["Item"];
            if (!String.IsNullOrEmpty(CID) && bll.setproduct(8, CID))
            {
                Response.Write("<script language=javascript>alert('批量取消精品成功!');location.href='Productlist.aspx?NodeID=" + this.NodeID + "';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('批量取消精品失败!请选择您要设置的数据');location.href='Productlist.aspx?NodeID=" + this.NodeID + "';</script>");
            }
        }
        protected void Button9_Click(object sender, EventArgs e)
        {
            string CID = Request.Form["Item"];
            if (!String.IsNullOrEmpty(CID) && bll.setproduct(9, CID))
            {
                Response.Write("<script language=javascript>alert('批量取消新品成功!');location.href='Productlist.aspx?NodeID=" + this.NodeID + "';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('批量取消新品失败!请选择您要设置的数据');location.href='Productlist.aspx?NodeID=" + this.NodeID + "';</script>");
            }
        }
        protected void Button3_Click1(object sender, EventArgs e)
        {
            string CID = Request.Form["Item"];
            if (!String.IsNullOrEmpty(CID) && bll.setproduct(10, CID))
            {
                Response.Write("<script language=javascript>alert('批量审核成功!');location.href='ProductExamine.aspx?NodeID=" + this.NodeID + "';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('批量审核失败!请选择您要删除的数据');location.href='Productlist.aspx?NodeID=" + this.NodeID + "';</script>");
            }
        }
    }
}