using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;
using Newtonsoft.Json;


namespace ZoomLaCMS.Manage.Shop
{
    public partial class AddProduct : CustomerPageAction
    {
        B_Node bnode = new B_Node();
        B_Model bmode = new B_Model();
        B_ShowField bshow = new B_ShowField();
        B_ModelField bfield = new B_ModelField();
        B_Product bll = new B_Product();
        B_Content Cll = new B_Content();
        B_Stock Sll = new B_Stock();
        B_Promotions pro = new B_Promotions();
        B_Group gpBll = new B_Group();
        B_Admin badmin = new B_Admin();
        B_Shop_FareTlp fareBll = new B_Shop_FareTlp();
        B_Shop_RegionPrice regionBll = new B_Shop_RegionPrice();
        B_KeyWord keyBll = new B_KeyWord();
        public string ProGuid { get { return ViewState["ProGuid"].ToString(); } set { ViewState["ProGuid"] = value; } }
        public int ProID { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        public int NodeID
        {
            get
            {
                if (ViewState["NodeID"] == null)
                { ViewState["NodeID"] = Request.QueryString["NodeID"]; }
                return DataConverter.CLng(ViewState["NodeID"]);
            }
            set { ViewState["NodeID"] = value; }
        }
        public int ModelID
        {
            get
            {
                if (ViewState["ModelID"] == null)
                { ViewState["ModelID"] = Request.QueryString["ModelID"]; }
                return DataConverter.CLng(ViewState["ModelID"]);
            }
            set { ViewState["ModelID"] = value; }
        }
        public int ParentID { get { return DataConverter.CLng(Request.QueryString["pid"]); } }
        public int StoreID
        {
            get { return DataConverter.CLng(ViewState["StoreID"]); }
            set { ViewState["StoreID"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "ProductManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            else if (ProID < 1 && ModelID == 0)
            {
                function.WriteErrMsg("没有指定要添加内容的模型ID!");
            }
            else if (ProID < 1 && NodeID == 0)
            {
                function.WriteErrMsg("没有指定要添加内容的栏目节点ID!");
            }
            if (!IsPostBack)
            {
                ProGuid = System.Guid.NewGuid().ToString();
                Group_Hid.Value = JsonConvert.SerializeObject(DBCenter.SelWithField("ZL_Group", "GroupID,GroupName"));
                RangeValidator1.MinimumValue = Convert.ToString(Int32.MinValue);
                RangeValidator1.MaximumValue = Convert.ToString(Int32.MaxValue);
                RangeValidator2.MinimumValue = Convert.ToString(Int32.MinValue);
                RangeValidator2.MaximumValue = Convert.ToString(Int32.MaxValue);
                RangeValidator3.MinimumValue = Convert.ToString(Int32.MinValue);
                RangeValidator3.MaximumValue = Convert.ToString(Int32.MaxValue);
                RangeValidator4.MinimumValue = Convert.ToString(Int32.MinValue);
                RangeValidator4.MaximumValue = Convert.ToString(Int32.MaxValue);
                MyBind();
            }
        }
        private string GetProCode()
        {
            return bll.GetProCode(SiteConfig.ShopConfig.ItemRegular);
        }
        public void MyBind()
        {
            M_Product pinfo = null;
            if (ProID > 0) { pinfo = bll.GetproductByid(ProID); NodeID = pinfo.Nodeid; }
            //-------------------------------
            M_Node nodeMod = bnode.SelReturnModel(NodeID);
            if (nodeMod.IsNull) { function.WriteErrMsg("节点[" + NodeID + "]不存在"); }
            string bread1 = "<a href=\"ProductManage.aspx?NodeID=" + NodeID + "\">" + nodeMod.NodeName + "</a>", bread2 = "添加商品";
            NodeName_L.Text = "<a href=\"ProductManage.aspx?NodeID=" + NodeID + "\">" + nodeMod.NodeName + "</a>";

            UpdateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            AddTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ProCode.Text = GetProCode();
            OtherBind();//组,运费模板
            if (ProID > 0)
            {
                #region 修改
                StoreID = pinfo.UserShopID > 0 ? -1 : 0;
                ModelID = pinfo.ModelID;
                txtCountHits.Text = pinfo.AllClickNum.ToString();
                bread2 = "修改商品";
                ClickType.Value = "update";
                btnAdd.Visible = true;
                istrue_Chk.Checked = pinfo.Istrue == 1 ? true : false;
                Categoryid.Value = pinfo.Categoryid.ToString();
                ProCode.Text = pinfo.ProCode;
                BarCode.Text = pinfo.BarCode.ToString();
                Proname.Text = pinfo.Proname.ToString();
                Keywords.Text = pinfo.Kayword.ToString();
                ProUnit.Text = pinfo.ProUnit.ToString();
                Weight.Text = pinfo.Weight.ToString();
                restate_hid.Value = pinfo.GuessXML;
                Propeid.Text = pinfo.Propeid.ToString();
                Largesspirx.Text = pinfo.Largesspirx.ToString();
                Largess.Checked = pinfo.Largess == 1 ? true : false;
                txtRecommend.Text = pinfo.Recommend.ToString();
                //ServerPeriod.Text = pinfo.ServerPeriod.ToString();
                //ServerType.SelectedValue = pinfo.ServerType.ToString();
                //expRemindDP.SelectedValue = pinfo.ExpRemind.ToString();
                ProClass_Hid.Value = pinfo.ProClass.ToString();
                txtPoint.Text = pinfo.PointVal.ToString();
                Proinfo.Text = pinfo.Proinfo.ToString();
                procontent.Value = pinfo.Procontent.ToString();
                txt_Clearimg.Text = pinfo.Clearimg.ToString();
                txt_Thumbnails.Text = pinfo.Thumbnails.ToString();
                Quota.Text = pinfo.Quota.ToString();
                DownQuota.Text = pinfo.DownQuota.ToString();
                Stock.Text = pinfo.Stock.ToString();
                Stock_Hid.Value = pinfo.Stock.ToString();
                StockDown.Text = pinfo.StockDown.ToString();
                JisuanFs.Text = pinfo.JisuanFs.ToString();
                Rate.Text = pinfo.Rate.ToString();
                Rateset.SelectedValue = pinfo.Rateset.ToString();
                Dengji.Text = pinfo.Dengji.ToString();
                ShiPrice.Text = pinfo.ShiPrice.ToString();
                Brand.Text = pinfo.Brand.ToString();
                Producer.Text = pinfo.Producer.ToString();
                LinPrice.Text = pinfo.LinPrice.ToString();
                LinPrice_Purse_T.Text = JsonHelper.GetDBVal(pinfo.LinPrice_Json, "purse").ToString("f2");
                LinPrice_Sicon_T.Text = JsonHelper.GetDBVal(pinfo.LinPrice_Json, "sicon").ToString("f2");
                LinPrice_Point_T.Text = JsonHelper.GetDBVal(pinfo.LinPrice_Json, "point").ToString("f2");
                Wholesaleone.Checked = pinfo.Wholesaleone == 1 ? true : false;
                FareTlp_Rad.SelectedValue = pinfo.FarePrice;
                ChildPro_Hid.Value = pinfo.Wholesalesinfo; //多价格
                IDC_Hid.Value = pinfo.IDCPrice;
                if (!string.IsNullOrEmpty(pinfo.BindIDS))//捆绑商品
                {
                    DataTable dt = bll.SelByIDS(pinfo.BindIDS, "id,Thumbnails,Proname,LinPrice");
                    Bind_Hid.Value = JsonConvert.SerializeObject(dt);
                }
                if (!string.IsNullOrEmpty(pinfo.Preset))
                {
                    if (pinfo.Preset.IndexOf(",") > -1)
                    {
                        string[] presetarr = pinfo.Preset.Split(new string[] { "," }, StringSplitOptions.None);
                        for (int s = 0; s < presetarr.Length; s++)
                        {
                            M_Promotions proinfo = pro.GetPromotionsByid(DataConverter.CLng(presetarr[s]));
                            if (proinfo == null) continue;
                            OtherProject.Items.Add(new ListItem(proinfo.Promoname, proinfo.Id.ToString()));
                        }
                    }
                    else
                    {
                        M_Promotions proinfo = pro.GetPromotionsByid(DataConverter.CLng(pinfo.Preset));
                        if (proinfo != null)
                            OtherProject.Items.Add(new ListItem(proinfo.Promoname, proinfo.Id.ToString()));
                    }
                }

                if (OtherProject.Items.Count > 0)
                {
                    for (int d = 0; d < OtherProject.Items.Count; d++)
                    {
                        OtherProject.Items[d].Selected = true;
                    }
                }
                //填充显示会员价
                switch (pinfo.UserType)
                {
                    case 1:
                        UserPrice1_Rad.Checked = true;
                        Price_Member_T.Text = DataConverter.CDouble(pinfo.UserPrice).ToString("f2");
                        price_member_div.Attributes["style"] = "";
                        break;
                    case 2:
                        UserPrice2_Rad.Checked = true;
                        price_group_div.Attributes["style"] = "";
                        if (pinfo.UserPrice.Contains("[") && !pinfo.UserPrice.Equals("[]"))
                        {
                            DataTable updt = JsonConvert.DeserializeObject<DataTable>(pinfo.UserPrice);
                            foreach (RepeaterItem item in Price_Group_RPT.Items)
                            {
                                string gid = (item.FindControl("GroupID_Hid") as HiddenField).Value;
                                DataRow[] drs = updt.Select("gid='" + gid + "'");
                                if (drs.Length > 0)
                                {
                                    var text = item.FindControl("Price_Group_T") as TextBox;
                                    text.Text = DataConverter.CDouble(drs[0]["Price"]).ToString("f2");
                                }
                            }
                        }
                        break;
                    default:
                        UserPrice0_Rad.Checked = true;
                        break;
                }
                txtBookPrice.Text = pinfo.BookPrice.ToString("f2");
                txtBookDay.Text = pinfo.bookDay.ToString();
                txtDayPrice.Text = pinfo.FestlPrice.ToString("f2");
                if (pinfo.FestPeriod.Split('|') != null && pinfo.FestPeriod.Split('|').Length > 1)
                {
                    CheckInDate.Text = pinfo.FestPeriod.Split('|')[0];
                    CheckOutDate.Text = pinfo.FestPeriod.Split('|')[1];
                    serverdate.Value = CheckInDate.Text;
                    CheckOut.Value = CheckOutDate.Text;
                }
                //Integral.Text = pinfo.Integral.ToString();
                UpdateTime.Text = pinfo.UpdateTime.ToString();
                AddTime.Text = pinfo.AddTime.ToString();
                ModeTemplate_hid.Value = pinfo.ModeTemplate.ToString();
                //Wholesaleone.Checked = pinfo.Wholesaleone == 1 ? true : false;
                isnew.Checked = pinfo.Isnew == 1;//是否新品,热,等
                ishot.Checked = pinfo.Ishot == 1;
                isbest.Checked = pinfo.Isbest == 1;
                Sales_Chk.Checked = pinfo.Sales == 1;
                Allowed.Checked = pinfo.Allowed == 1;
                DataTable valueDT = bll.Getmodetable(pinfo.TableName.ToString(), DataConverter.CLng(pinfo.ItemID));
                if (valueDT != null && valueDT.Rows.Count > 0)
                {
                    ModelHtml.Text = bfield.InputallHtml(ModelID, NodeID, new ModelConfig()
                    {
                        ValueDT = valueDT
                    });
                }
                //IntegralNum.Text = pinfo.IntegralNum.ToString();
                switch (pinfo.ProjectType)
                {
                    case 1:
                        break;
                    case 2:
                        ProjectType2_Rad.Checked = true;
                        ProjectPronum2.Text = pinfo.ProjectPronum.ToString();
                        break;
                    case 3:
                        ProjectType3_Rad.Checked = true;
                        ProjectPronum3.Text = pinfo.ProjectPronum.ToString();
                        Productsname3.Text = pinfo.PesentNames.ToString();
                        HiddenField3.Value = pinfo.PesentNameid.ToString();
                        break;
                    case 4:
                        ProjectType4_Rad.Checked = true;
                        ProjectPronum4.Text = pinfo.ProjectPronum.ToString();
                        break;
                    case 5:
                        ProjectType5_Rad.Checked = true;
                        ProjectPronum5.Text = pinfo.ProjectPronum.ToString();
                        Productsname5.Text = pinfo.PesentNames.ToString();
                        HiddenField5.Value = pinfo.PesentNameid.ToString();
                        break;
                    case 6:
                        ProjectType6_Rad.Checked = true;
                        ProjectMoney7.Text = pinfo.ProjectMoney.ToString();
                        Productsname6.Text = pinfo.PesentNames.ToString();
                        HiddenField6.Value = pinfo.PesentNameid.ToString();
                        break;
                    case 7:
                        ProjectType7_Rad.Checked = true;
                        ProjectMoney7.Text = pinfo.ProjectMoney.ToString();
                        Productsname7.Text = pinfo.PesentNames.ToString();
                        HiddenField7.Value = pinfo.PesentNameid.ToString();
                        break;
                }
                #endregion
                #region 多区域价格
                ProGuid = pinfo.ID.ToString();
                M_Shop_RegionPrice regionMod = regionBll.SelModelByGuid(ProGuid);
                if (regionMod != null && !string.IsNullOrEmpty(regionMod.Info))
                {
                    function.Script(this, "region.fill(" + regionMod.Info + ");");
                }
                #endregion
            }
            else
            {
                isnew.Checked = true;
                Sales_Chk.Checked = true;
                ModelHtml.Text = bfield.InputallHtml(ModelID, NodeID, new ModelConfig()
                {
                    Source = ModelConfig.SType.Admin
                });
                btnAdd.Visible = false;
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li><li><a href='ProductManage.aspx?NodeID='>" + bread1 + "</a></li><li class='active'>" + bread2 + "</li>"
                                    + "<div class='pull-right hidden-xs'><span onclick=\"opentitle('../Content/EditNode.aspx?NodeID=" + NodeID + "','配置本节点');\" class='fa fa-cog' title='配置本节点' style='cursor:pointer;margin-left:5px;'></span></div>");
        }
        //保存
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            string adminname = badmin.GetAdminLogin().AdminName;
            DataTable dt = bfield.GetModelFieldList(ModelID);
            DataTable table = new Call().GetDTFromPage(dt, Page, ViewState);
            M_CommonData CCate = new M_CommonData();
            M_Product proMod = new M_Product();
            if (ProID > 0)
            {
                proMod = bll.GetproductByid(ProID);
            }
            CCate.Status = istrue_Chk.Checked ? 99 : 0;
            CCate.NodeID = NodeID;
            CCate.ModelID = ModelID;
            CCate.TableName = bmode.GetModelById(ModelID).TableName;
            CCate.Title = Proname.Text;
            CCate.Inputer = adminname;
            CCate.PdfLink = "";
            CCate.FirstNodeID = bnode.SelFirstNodeID(NodeID);
            CCate.EliteLevel = DataConverter.CLng(Dengji.SelectedValue) > 3 ? 1 : 0;
            CCate.InfoID = "";
            CCate.SpecialID = "";
            CCate.Template = ModeTemplate_hid.Value;
            CCate.DefaultSkins = 0;
            /*--------------proMod------------*/
            proMod.Istrue = 1;
            proMod.Class = 0;
            proMod.Nodeid = NodeID;
            proMod.ModelID = ModelID;
            proMod.Categoryid = DataConverter.CLng(Categoryid.Value);
            proMod.AddUser = string.IsNullOrEmpty(txtCountHits.Text) ? adminname : txtCountHits.Text;
            if (string.IsNullOrEmpty(proMod.ProCode))
            {
                proMod.ProCode = GetProCode();
            }
            proMod.BarCode = BarCode.Text.Trim();
            proMod.Proname = Proname.Text.Trim();
            proMod.Kayword = Request.Form["tabinput"];
            keyBll.AddKeyWord(proMod.Kayword, 1);
            proMod.ProUnit = ProUnit.Text;
            proMod.AllClickNum = DataConverter.CLng(Request.Form["AllClickNum"]);
            proMod.Weight = DataConverter.CLng(Weight.Text);
            //proMod.ServerPeriod = DataConverter.CLng(ServerPeriod.Text);
            //proMod.ServerType = DataConverter.CLng(ServerType.SelectedValue);
            //proMod.ExpRemind = Convert.ToInt32(expRemindDP.SelectedValue);
            proMod.ProClass = DataConverter.CLng(ProClass_Hid.Value);
            //proMod.ColoneDeposit = DataConverter.CDouble(txtColoneDeposit.Text);
            proMod.IDCPrice = IDC_Hid.Value;
            proMod.Properties = 0;
            proMod.PointVal = DataConverter.CLng(txtPoint.Text);
            proMod.Sales = Sales_Chk.Checked ? 1 : 2;
            proMod.Proinfo = Proinfo.Text;
            proMod.Procontent = procontent.Value;
            proMod.Clearimg = txt_Clearimg.Text;
            proMod.Thumbnails = txt_Thumbnails.Text;
            proMod.Producer = Producer.Text;
            proMod.Brand = Brand.Text;
            proMod.Wholesaleone = Wholesaleone.Checked ? 1 : 0;
            proMod.Quota = DataConverter.CLng(Quota.Text);
            proMod.DownQuota = DataConverter.CLng(DownQuota.Text);
            int proStock = DataConverter.CLng(Stock.Text);
            proMod.StockDown = DataConverter.CLng(StockDown.Text);
            proMod.JisuanFs = DataConverter.CLng(JisuanFs.SelectedValue);
            proMod.Rate = DataConverter.CDouble(Rate.Text);
            proMod.Rateset = DataConverter.CLng(Rateset.SelectedValue);
            proMod.Dengji = DataConverter.CLng(Dengji.SelectedValue);
            proMod.ShiPrice = DataConverter.CDouble(ShiPrice.Text);
            proMod.LinPrice = DataConverter.CDouble(LinPrice.Text);
            proMod.LinPrice_Json = JsonHelper.AddVal("purse,sicon,point".Split(','), LinPrice_Purse_T.Text, LinPrice_Sicon_T.Text, LinPrice_Point_T.Text);
            proMod.Preset = (OtherProject.SelectedValue == null) ? "" : OtherProject.SelectedValue;  //促销
            //proMod.Integral = DataConverter.CLng(Integral.Text);
            proMod.Propeid = DataConverter.CLng(Propeid.Text);
            proMod.Recommend = DataConverter.CLng(txtRecommend.Text);
            proMod.Recommend = proMod.Recommend < 1 ? 0 : proMod.Recommend;//不允许负数
            proMod.Largesspirx = DataConverter.CLng(Largesspirx.Text);
            proMod.AllClickNum = DataConverter.CLng(txtCountHits.Text);
            //更新时间，若没有指定则为当前时间
            proMod.UpdateTime = DataConverter.CDate(UpdateTime.Text);
            proMod.AddTime = DataConverter.CDate(AddTime.Text);
            proMod.ModeTemplate = ModeTemplate_hid.Value;
            proMod.FirstNodeID = CCate.FirstNodeID;
            proMod.bookDay = DataConverter.CLng(txtBookDay.Text);
            proMod.BookPrice = DataConverter.CDouble(txtBookPrice.Text);
            proMod.FestlPrice = DataConverter.CDouble(txtDayPrice.Text);
            proMod.FestPeriod = CheckInDate.Text + "|" + CheckOutDate.Text;
            proMod.UserType = DataConverter.CLng(Request.Form["ctl00$Content$UserPrice_Rad"]);
            proMod.ParentID = ParentID > 0 ? ParentID : proMod.ParentID;
            proMod.FarePrice = FareTlp_Rad.SelectedValue;
            if (UserPrice1_Rad.Checked)
            {
                proMod.UserPrice = Price_Member_T.Text.Trim();
            }
            else if (UserPrice2_Rad.Checked)
            {
                DataTable updt = new DataTable();
                updt.Columns.Add(new DataColumn("gid", typeof(int)));
                updt.Columns.Add(new DataColumn("price", typeof(double)));
                for (int i = 0; i < Price_Group_RPT.Items.Count; i++)
                {
                    DataRow dr = updt.NewRow();
                    dr["gid"] = Convert.ToInt32((Price_Group_RPT.Items[i].FindControl("GroupID_Hid") as HiddenField).Value);
                    dr["price"] = DataConverter.CDouble((Price_Group_RPT.Items[i].FindControl("Price_Group_T") as TextBox).Text);
                    updt.Rows.Add(dr);
                }
                proMod.UserPrice = JsonConvert.SerializeObject(updt);
            }
            proMod.AddUser = adminname;
            proMod.DownCar = 1;
            int ProjectTypeRad = 0;
            if (ProjectType2_Rad.Checked) { ProjectTypeRad = 2; }
            if (ProjectType3_Rad.Checked) { ProjectTypeRad = 3; }
            if (ProjectType4_Rad.Checked) { ProjectTypeRad = 4; }
            if (ProjectType5_Rad.Checked) { ProjectTypeRad = 5; }
            if (ProjectType6_Rad.Checked) { ProjectTypeRad = 6; }
            if (ProjectType7_Rad.Checked) { ProjectTypeRad = 7; }
            proMod.ProjectType = ProjectTypeRad;
            switch (proMod.ProjectType)
            {
                #region 促销
                case 1:
                    proMod.ProjectPronum = 0;
                    proMod.ProjectMoney = 0;
                    //proMod.IntegralNum = DataConverter.CLng(IntegralNum.Text);
                    proMod.PesentNames = "";
                    proMod.PesentNameid = 0;
                    break;
                case 2:
                    proMod.ProjectPronum = DataConverter.CLng(ProjectPronum2.Text);
                    proMod.ProjectMoney = 0;
                    //proMod.IntegralNum = DataConverter.CLng(IntegralNum.Text);
                    proMod.PesentNames = "";
                    proMod.PesentNameid = 0;
                    break;
                case 3:
                    proMod.ProjectPronum = DataConverter.CLng(ProjectPronum3.Text);
                    proMod.ProjectMoney = 0;
                    //proMod.IntegralNum = DataConverter.CLng(IntegralNum.Text);
                    proMod.PesentNames = Productsname3.Text;
                    proMod.PesentNameid = DataConverter.CLng(HiddenField3.Value);
                    break;
                case 4:
                    proMod.ProjectPronum = DataConverter.CLng(ProjectPronum4.Text);
                    proMod.ProjectMoney = 0;
                    //proMod.IntegralNum = DataConverter.CLng(IntegralNum.Text);
                    proMod.PesentNames = "";
                    proMod.PesentNameid = 0;
                    break;
                case 5:
                    proMod.ProjectPronum = DataConverter.CLng(ProjectPronum5.Text);
                    proMod.ProjectMoney = 0;
                    //proMod.IntegralNum = DataConverter.CLng(IntegralNum.Text);
                    proMod.PesentNames = Productsname5.Text;
                    proMod.PesentNameid = DataConverter.CLng(HiddenField5.Value);
                    break;
                case 6:
                    proMod.ProjectPronum = 0;
                    proMod.ProjectMoney = DataConverter.CDouble(ProjectMoney6.Text);
                    //proMod.IntegralNum = DataConverter.CLng(IntegralNum.Text);
                    proMod.PesentNames = Productsname6.Text;
                    proMod.PesentNameid = DataConverter.CLng(HiddenField6.Value);
                    break;
                case 7:
                    proMod.ProjectPronum = 0;
                    proMod.ProjectMoney = DataConverter.CDouble(ProjectMoney7.Text);
                    //proMod.IntegralNum = DataConverter.CLng(IntegralNum.Text);
                    proMod.PesentNames = Productsname7.Text;
                    proMod.PesentNameid = DataConverter.CLng(HiddenField7.Value);
                    break;
                    #endregion
            }
            //proMod.UpdateTime = DateTime.Now;
            proMod.TableName = bmode.GetModelById(ModelID).TableName;
            proMod.Istrue = istrue_Chk.Checked ? 1 : 0;
            proMod.Isgood = 0;
            proMod.MakeHtml = 0;
            proMod.Ishot = ishot.Checked ? 1 : 0;
            proMod.Isnew = isnew.Checked ? 1 : 0;
            proMod.Isbest = isbest.Checked ? 1 : 0;
            proMod.Allowed = Allowed.Checked ? 1 : 0;
            proMod.GuessXML = Request.Form["GuessXML"];
            proMod.Wholesalesinfo = ChildPro_Hid.Value;
            //捆绑商品
            if (!string.IsNullOrEmpty(Bind_Hid.Value))
            {
                //获取绑定商品
                DataTable binddt = JsonHelper.JsonToDT(Bind_Hid.Value);
                proMod.BindIDS = "";
                foreach (DataRow dr in binddt.Rows)
                {
                    proMod.BindIDS += dr["ID"] + ",";
                }
                proMod.BindIDS = proMod.BindIDS.TrimEnd(',');
            }
            else
            {
                proMod.BindIDS = "";
            }
            string danju = Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Millisecond);
            if (ClickType.Value == "update")
            {
                bll.Update(table, proMod, CCate);
                int stock = proStock - DataConverter.CLng(Stock_Hid.Value);
                if (stock != 0)
                {
                    M_Stock stockMod = new M_Stock()
                    {
                        proid = proMod.ID,
                        stocktype = stock > 0 ? 0 : 1,
                        proname = proMod.Proname,
                        adduser = adminname,
                        addtime = DateTime.Now,
                        Pronum = Math.Abs(stock),
                        content = stock > 0 ? "添加商品:" + Proname.Text + "入库" : "减少商品:" + Proname.Text + "出库",
                        danju = (stock > 0 ? "RK" : "CK") + danju
                    };
                    Sll.AddStock(stockMod);
                }
                Response.Redirect("ContentShow.aspx?id=" + ProID + "&ModelId=" + ModelID + "&NodeId=" + NodeID);
            }
            else
            {
                if (ClickType.Value.Equals("addasnew"))
                {
                    //添加为新商品
                    proMod.ProCode = GetProCode();
                    proMod.AddTime = proMod.UpdateTime;
                }
                proMod.Priority = 0;
                proMod.Nodeid = NodeID;
                proMod.ID = bll.Add(table, proMod, CCate);
                //多区域价格
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("guid", ProGuid) };
                SqlHelper.ExecuteSql("UPDATE ZL_Shop_RegionPrice SET [ProID]=" + proMod.ID + " WHERE [Guid]=@guid", sp);
                M_Stock SDatac = new M_Stock()
                {
                    proid = proMod.ID,
                    stocktype = 0,
                    proname = proMod.Proname,
                    adduser = adminname,
                    addtime = DateTime.Now,
                    Pronum = proStock,
                    content = "添加商品:" + Proname.Text + "入库",
                    danju = "RK" + danju
                };
                Sll.AddStock(SDatac);
                Response.Redirect("ContentShow.aspx?id=" + proMod.ID + "&ModelId=" + ModelID + "&NodeId=" + NodeID);
            }
        }
        //添加为新商品
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClickType.Value = "addasnew";
            EBtnSubmit_Click(sender, e);
        }
        // 会员价
        private void OtherBind()
        {
            Price_Group_RPT.DataSource = gpBll.GetGroupList();
            Price_Group_RPT.DataBind();
            FareTlp_Rad.DataSource = fareBll.Sel();
            FareTlp_Rad.DataBind();
            FareTlp_Rad.Items.Insert(0, new ListItem("免费", "0"));
            FareTlp_Rad.SelectedValue = "0";
        }
    }
}