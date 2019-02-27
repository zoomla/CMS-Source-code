using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Text;
using System.Xml;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Shop;
using Newtonsoft.Json;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class ShowProduct : CustomerPageAction
    {
        B_Node bnode = new B_Node();
        B_ShowField bshow = new B_ShowField();
        B_ModelField bfield = new B_ModelField();
        B_Product bll = new B_Product();
        B_Trademark Tradk = new B_Trademark();
        B_Stock Sll = new B_Stock();
        B_User bu = new B_User();
        M_Product CData = new M_Product();
        B_Order_IDC idcBll = new B_Order_IDC();
        B_Group gpBll = new B_Group();
        public int Mid { get { return Convert.ToInt32(Request.QueryString["id"]); } }
        public int StoreID { get { return DataConverter.CLng(ViewState["StoreID"]); } set { ViewState["StoreID"] = value; } }
        public int NodeID { get { return DataConverter.CLng(ViewState["NodeID"]); } set { ViewState["NodeID"] = value; } }
        void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_Product pinfo = bll.GetproductByid(Mid);
                M_Node nodeMod = bnode.SelReturnModel(pinfo.Nodeid);
                NodeID = nodeMod.NodeID;
                M_Product preMod = bll.GetNearID(pinfo.Nodeid, pinfo.ID, 0);
                M_Product nextMod = bll.GetNearID(pinfo.Nodeid, pinfo.ID, 1);
                if (preMod != null) { PrePro_Btn.Attributes.Remove("disabled"); PrePro_Btn.CommandArgument = preMod.ID.ToString(); }
                if (nextMod != null) { NextPro_Btn.Attributes.Remove("disabled"); NextPro_Btn.CommandArgument = nextMod.ID.ToString(); }

                lblCountHits.Text = pinfo.AllClickNum.ToString();
                this.nodename.Text = "<a href=\"ProductManage.aspx?NodeID=" + nodeMod.NodeID + "\">" + nodeMod.NodeName + "</a>";
                StoreID = pinfo.UserShopID > 0 ? -1 : 0;
                Categoryid.Value = pinfo.Categoryid.ToString();
                AddUser_L.Text = pinfo.AddUser;
                ProCode.Text = pinfo.ProCode;
                BarCode.Text = pinfo.BarCode;
                Proname.Text = pinfo.Proname;
                Kayword.Text = pinfo.Kayword;
                ProUnit.Text = pinfo.ProUnit;
                lblpoint.Text = pinfo.PointVal.ToString();
                Weight.Text = pinfo.Weight.ToString();
                this.Propeid.Text = pinfo.Propeid.ToString();
                this.Largesspirx.Text = pinfo.Largesspirx.ToString();
                this.Largess1.Text = pinfo.Largess == 1 ? "是" : "否";
                this.txtRecommend.Text = pinfo.Recommend.ToString();
                if (!string.IsNullOrEmpty(pinfo.IDCPrice))
                {
                    ProExtend_L.Text = JsonConvert.SerializeObject(idcBll.P_GetValid(pinfo.IDCPrice));
                }
                ProClass1.Text = pinfo.ProClass == 1 ? "正常销售" : "特价处理";
                Proinfo.Text = pinfo.Proinfo.ToString();
                Procontent.Text = pinfo.Procontent.ToString();
                Clearimg.Text = ComRE.Img_NoPic(function.GetImgUrl(pinfo.Clearimg));
                Thumbnails.Text = ComRE.Img_NoPic(function.GetImgUrl(pinfo.Thumbnails));
                DownQuota.Text = pinfo.DownQuota.ToString();
                Stock.Text = pinfo.Stock.ToString();
                StockDown.Text = pinfo.StockDown.ToString();
                JisuanFs1.Text = pinfo.JisuanFs == 0 ? "实际库存" : "虚拟库存";
                Rate.Text = pinfo.Rate.ToString();
                if (pinfo.Rateset == 1)
                {
                    Rateset1.Text = "含税，不开发票时有税率优惠";
                }
                if (pinfo.Rateset == 2)
                {
                    Rateset1.Text = "含税，不开发票时没有税率优惠";
                }
                if (pinfo.Rateset == 3)
                {
                    Rateset1.Text = "不含税，开发票时需要加收税费";
                }
                if (pinfo.Rateset == 4)
                {
                    Rateset1.Text = "不含税，开发票时不需要加收税费";
                }
                if (pinfo.Dengji == 1)
                {
                    Dengji1.Text = "★";
                }
                if (pinfo.Dengji == 2)
                {
                    Dengji1.Text = "★★";
                }
                if (pinfo.Dengji == 3)
                {
                    Dengji1.Text = "★★★";
                }
                if (pinfo.Dengji == 4)
                {
                    Dengji1.Text = "★★★★";
                }
                if (pinfo.Dengji == 5)
                {
                    Dengji1.Text = "★★★★★";
                }

                ShiPrice.Text = pinfo.ShiPrice.ToString();
                Brand.Text = pinfo.Brand.ToString();
                Producer.Text = pinfo.Producer.ToString();
                LinPrice.Text = pinfo.LinPrice.ToString();
                Wholesaleone1.Text = pinfo.Wholesaleone == 1 ? "是" : "否";
                if (pinfo.Istrue == 1)
                {
                    this.istrue1.Text = "审核通过";
                }
                else
                {
                    this.istrue1.Text = "审核未通过";
                }

                string presets = pinfo.Preset;
                if (!string.IsNullOrEmpty(presets))
                {
                    this.OtherProject.Text = presets;
                }
                else
                {
                    this.OtherProject.Text = "";
                }
                Stock.Enabled = false;
                Integral.Text = pinfo.Integral.ToString();
                UpdateTime.Text = pinfo.UpdateTime.ToString();
                ModeTemplate.Text = pinfo.ModeTemplate.ToString();
                if (pinfo.Isnew == 1) { this.istrue1.Text += "  新品"; }
                if (pinfo.Ishot == 1) { this.istrue1.Text += "  热销"; }
                if (pinfo.Isbest == 1) { this.istrue1.Text += "  精品"; }
                if (pinfo.Sales == 1) { Sales1.Text = "销售中"; }
                if (pinfo.Sales != 1) { Sales1.Text = "停销状态"; }
                if (pinfo.Allowed == 1) { Allowed.Text = "缺货时允许购买"; }
                if (pinfo.Allowed != 1) { Allowed.Text = "缺货时不允许购买"; }

                DataTable dr = bll.Getmodetable(pinfo.TableName.ToString(), DataConverter.CLng(pinfo.ItemID));
                this.ModelHtml.Text = this.bfield.InputallHtml(pinfo.ModelID, nodeMod.NodeID, new ModelConfig()
                {
                    ValueDT = dr,
                    Mode = ModelConfig.SMode.PreView
                });
                IntegralNum.Text = pinfo.IntegralNum.ToString();
                switch (pinfo.ProjectType)
                {
                    case 1:
                        ProjectType1.Text = "不促销";
                        break;
                    case 2:
                        ProjectType1.Text = "买" + pinfo.ProjectPronum.ToString() + "件此商品 可以送一件同样商品";
                        break;
                    case 3:
                        this.ProjectType1.Text = "买" + pinfo.ProjectPronum.ToString()
                            + "件此商品 可以送一件其他商品 赠品名称：" + pinfo.PesentNames.ToString();
                        this.HiddenField3.Value = pinfo.PesentNameid.ToString();
                        break;
                    case 4:
                        this.ProjectType1.Text = "送" + pinfo.ProjectPronum.ToString() + "件同样商品";
                        break;
                    case 5:
                        this.ProjectType1.Text = "送" + pinfo.ProjectPronum.ToString()
                            + "件其他商品 送一件赠品名称：" + pinfo.PesentNames.ToString();
                        this.HiddenField5.Value = pinfo.PesentNameid.ToString();
                        break;
                    case 6:
                        this.ProjectType1.Text = "加" + pinfo.ProjectMoney.ToString()
                            + "元钱送商品 送一件赠品名称：" + pinfo.PesentNames.ToString();
                        this.HiddenField6.Value = pinfo.PesentNameid.ToString();
                        break;
                    case 7:
                        this.ProjectType1.Text = "满" + pinfo.ProjectMoney.ToString()
                            + "元钱送商品 送一件赠品名称：" + pinfo.PesentNames.ToString();
                        this.HiddenField7.Value = pinfo.PesentNameid.ToString();
                        break;
                }
                //填充显示会员价
                BindUserPrice(pinfo);

                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li><li><a href='ProductManage.aspx'>商品管理</a></li><li><a href=\"ProductManage.aspx?NodeID="
                  + nodeMod.NodeID + "\">" + nodeMod.NodeName + "</a></li><li class='active'>预览商品</li>" + "<div class='pull-right hidden-xs'><span onclick=\"opentitle('../Content/EditNode.aspx?NodeID=" + NodeID + "','配置本节点');\" class='fa fa-cog' title='配置本节点' style='cursor:pointer;margin-left:5px;'></span></div>");
            }
        }
        private void BindUserPrice(M_Product pinfo)
        {
            switch (pinfo.UserType)
            {
                case 1:
                    Price_Member_T.Text = DataConverter.CDouble(pinfo.UserPrice).ToString("f2");
                    price_member_div.Attributes["style"] = "";
                    break;
                case 2:
                    price_group_div.Attributes["style"] = "";
                    break;
                default:
                    UserPri_L.Text = "未设置";
                    break;
            }

            DataTable gpdt = gpBll.GetGroupList();
            //附加会员价,限购数,最低购买数等限制
            gpdt.Columns.Add(new DataColumn("price", typeof(string)));
            if (pinfo != null && pinfo.ID > 0)
            {
                if (pinfo.UserPrice.Contains("["))
                {
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(pinfo.UserPrice);
                    if (dt.Columns.Contains("price")) { dt.Columns["price"].ColumnName = "value"; }
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow[] gps = gpdt.Select("GroupID='" + dr["gid"] + "'");
                        if (gps.Length > 0) { gps[0]["price"] = DataConverter.CDouble(dr["value"]).ToString("F2"); }
                    }
                }
            }
            Price_Group_RPT.DataSource = gpdt;
            Price_Group_RPT.DataBind();
        }
        public string getproimg(string type)
        {
            string restring;
            restring = "";
            if (!string.IsNullOrEmpty(type)) { type = type.ToLower(); }
            if (!string.IsNullOrEmpty(type) && (type.IndexOf(".gif") > -1 || type.IndexOf(".jpg") > -1 || type.IndexOf(".png") > -1))
            {
                restring = "<img src=../../" + type + " border=0 width=60 height=45>";
            }
            else
            {
                restring = "<img src=../../UploadFiles/nopic.gif border=0 width=60 height=45>";
            }
            return restring;
        }
        protected void NextPro_Btn_Click(object sender, EventArgs e)
        {
            string url = "ShowProduct.aspx?ID=" + (sender as Button).CommandArgument;
            Response.Redirect(url);
        }
    }
}