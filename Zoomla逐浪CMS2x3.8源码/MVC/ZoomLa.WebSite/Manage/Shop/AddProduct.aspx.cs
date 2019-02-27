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
        B_Admin badmin = new B_Admin();
        B_Model modBll = new B_Model();
        B_ModelField fieldBll = new B_ModelField();
        B_Node nodeBll = new B_Node();
        B_Product proBll = new B_Product();
        B_Promotions promoBll = new B_Promotions();
        B_Group gpBll = new B_Group();
        B_Stock stockBll = new B_Stock();
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
                RangeValidator4.MinimumValue = Convert.ToString(Int32.MinValue);
                RangeValidator4.MaximumValue = Convert.ToString(Int32.MaxValue);
                MyBind();
            }
        }
        public void MyBind()
        {
            M_Product pinfo = null;
            if (ProID > 0) { pinfo = proBll.GetproductByid(ProID); NodeID = pinfo.Nodeid; }
            //-------------------------------
            M_Node nodeMod = nodeBll.SelReturnModel(NodeID);
            if (nodeMod.IsNull) { function.WriteErrMsg("节点[" + NodeID + "]不存在"); }
            string bread1 = "<a href=\"ProductManage.aspx?NodeID=" + NodeID + "\">" + nodeMod.NodeName + "</a>", bread2 = "添加商品";
            NodeName_L.Text = "<a href=\"ProductManage.aspx?NodeID=" + NodeID + "\">" + nodeMod.NodeName + "</a>";

            UpdateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            AddTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ProCode.Text = B_Product.GetProCode();
            OtherBind(pinfo);//组,运费模板
            if (ProID > 0)
            {
                #region 修改
                StoreID = pinfo.UserShopID > 0 ? -1 : 0;
                ModelID = pinfo.ModelID;
                AllClickNum_T.Text = pinfo.AllClickNum.ToString();
                bread2 = "修改商品";
                ClickType.Value = "update";
                btnAdd.Visible = true;
                istrue_chk.Checked = pinfo.Istrue == 1 ? true : false;
                ProCode.Text = pinfo.ProCode;
                BarCode.Text = pinfo.BarCode.ToString();
                Proname.Text = pinfo.Proname.ToString();
                Keywords.Text = pinfo.Kayword.ToString();
                ProUnit.Text = pinfo.ProUnit.ToString();
                Weight.Text = pinfo.Weight.ToString();
                restate_hid.Value = pinfo.GuessXML;
                Propeid.Text = pinfo.Propeid.ToString();
                //Largesspirx.Text = pinfo.Largesspirx.ToString();
                Largess.Checked = pinfo.Largess == 1 ? true : false;
                Recommend_T.Text = pinfo.Recommend.ToString();
                //ServerPeriod.Text = pinfo.ServerPeriod.ToString();
                //ServerType.SelectedValue = pinfo.ServerType.ToString();
                //expRemindDP.SelectedValue = pinfo.ExpRemind.ToString();
                ProClass_Hid.Value = pinfo.ProClass.ToString();
                PointVal_T.Text = pinfo.PointVal.ToString();
                Proinfo.Text = pinfo.Proinfo.ToString();
                procontent.Value = pinfo.Procontent.ToString();
                txt_Clearimg.Text = pinfo.Clearimg.ToString();
                txt_Thumbnails.Text = pinfo.Thumbnails.ToString();
                //Quota.Text = pinfo.Quota.ToString();
                //DownQuota.Text = pinfo.DownQuota.ToString();
                Stock.Text = pinfo.Stock.ToString();
                SetStock_B.Visible = true;
                StockDown.Text = pinfo.StockDown.ToString();
                function.Script(this, "SetRadVal('JisuanFs','" + pinfo.JisuanFs + "');");
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
                FareTlp_Rad.SelectedValue = pinfo.FarePrice;
                ChildPro_Hid.Value = pinfo.Wholesalesinfo; //多价格
                IDC_Hid.Value = pinfo.IDCPrice;
                Stock.ReadOnly = true;
                if (!string.IsNullOrEmpty(pinfo.BindIDS))//捆绑商品
                {
                    DataTable dt = proBll.SelByIDS(pinfo.BindIDS, "id,Thumbnails,Proname,LinPrice");
                    Bind_Hid.Value = JsonConvert.SerializeObject(dt);
                }
                if (!string.IsNullOrEmpty(pinfo.Preset))
                {
                    if (pinfo.Preset.IndexOf(",") > -1)
                    {
                        string[] presetarr = pinfo.Preset.Split(new string[] { "," }, StringSplitOptions.None);
                        for (int s = 0; s < presetarr.Length; s++)
                        {
                            M_Promotions proinfo = promoBll.GetPromotionsByid(DataConverter.CLng(presetarr[s]));
                            if (proinfo == null) continue;
                            OtherProject.Items.Add(new ListItem(proinfo.Promoname, proinfo.Id.ToString()));
                        }
                    }
                    else
                    {
                        M_Promotions proinfo = promoBll.GetPromotionsByid(DataConverter.CLng(pinfo.Preset));
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
                        Price_Member_T.Text = DataConverter.CDouble(pinfo.UserPrice).ToString("f2");
                        price_member_div.Attributes["style"] = "";
                        break;
                    case 2:
                        price_group_div.Attributes["style"] = "";
                        break;
                    default:
                        break;
                }
                function.Script(this, "SetRadVal('UserPrice_Rad','" + pinfo.UserType + "');");
                function.Script(this, "SetRadVal('DownQuota_Rad','" + pinfo.DownQuota + "');");
                function.Script(this, "SetRadVal('Quota_Rad','" + pinfo.Quota + "');");
                DownCar_T.Text = pinfo.DownCar.ToString();
                BookPrice_T.Text = pinfo.BookPrice.ToString("f2");
                BookDay_T.Text = pinfo.bookDay.ToString();
                //Integral.Text = pinfo.Integral.ToString();
                UpdateTime.Text = pinfo.UpdateTime.ToString();
                AddTime.Text = pinfo.AddTime.ToString();
                ModeTemplate_hid.Value = pinfo.ModeTemplate.ToString();
                isnew_chk.Checked = pinfo.Isnew == 1;//是否新品,热,等
                ishot_chk.Checked = pinfo.Ishot == 1;
                isbest_chk.Checked = pinfo.Isbest == 1;
                Sales_Chk.Checked = pinfo.Sales == 1;
                Allowed.Checked = pinfo.Allowed == 1;
                DataTable valueDT = proBll.Getmodetable(pinfo.TableName.ToString(), pinfo.ItemID);
                if (valueDT != null && valueDT.Rows.Count > 0)
                {
                    ModelHtml.Text = fieldBll.InputallHtml(ModelID, NodeID, new ModelConfig()
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
                isnew_chk.Checked = true;
                Sales_Chk.Checked = true;
                ModelHtml.Text = fieldBll.InputallHtml(ModelID, NodeID, new ModelConfig()
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
            DataTable dt = fieldBll.GetModelFieldList(ModelID);
            DataTable gpdt = gpBll.GetGroupList();
            DataTable table = new Call().GetDTFromPage(dt, Page, ViewState);
            M_CommonData CCate = new M_CommonData();
            M_Product proMod = new M_Product();
            if (ProID > 0)
            {
                proMod = proBll.GetproductByid(ProID);
            }
            CCate.Status = istrue_chk.Checked ? 99 : 0;
            CCate.NodeID = NodeID;
            CCate.ModelID = ModelID;
            CCate.TableName = modBll.GetModelById(ModelID).TableName;
            CCate.Title = Proname.Text;
            CCate.Inputer = adminname;
            CCate.PdfLink = "";
            CCate.FirstNodeID = nodeBll.SelFirstNodeID(NodeID);
            CCate.EliteLevel = DataConverter.CLng(Dengji.SelectedValue) > 3 ? 1 : 0;
            CCate.InfoID = "";
            CCate.SpecialID = "";
            CCate.Template = ModeTemplate_hid.Value;
            CCate.DefaultSkins = 0;
            /*--------------proMod------------*/
            proMod.Class = 0;
            proMod.Nodeid = NodeID;
            proMod.ModelID = ModelID;
            proMod.Properties = 0;
            proMod.Isgood = 0;
            proMod.MakeHtml = 0;
            proMod.AddUser = adminname;
            if (string.IsNullOrEmpty(proMod.ProCode))
            {
                proMod.ProCode = B_Product.GetProCode();
            }
            proMod.BarCode = BarCode.Text.Trim();
            proMod.Proname = Proname.Text.Trim();
            proMod.Kayword = Request.Form["tabinput"];
            keyBll.AddKeyWord(proMod.Kayword, 1);
            proMod.ProUnit = ProUnit.Text;
            proMod.AllClickNum = DataConverter.CLng(Request.Form["AllClickNum"]);
            proMod.Weight = DataConverter.CLng(Weight.Text);
            proMod.ProClass = DataConverter.CLng(ProClass_Hid.Value);
            proMod.IDCPrice = IDC_Hid.Value;
            proMod.PointVal = DataConverter.CLng(PointVal_T.Text);
            proMod.Proinfo = Proinfo.Text;
            proMod.Procontent = procontent.Value;
            proMod.Clearimg = txt_Clearimg.Text;
            proMod.Thumbnails = txt_Thumbnails.Text;
            proMod.Producer = Producer.Text;
            proMod.Brand = Brand.Text;
            //proMod.Quota = DataConverter.CLng(Quota.Text);
            //proMod.DownQuota = DataConverter.CLng(DownQuota.Text);
            proMod.StockDown = DataConverter.CLng(StockDown.Text);
            proMod.JisuanFs = DataConverter.CLng(Request.Form["JisuanFs"]);
            proMod.Rate = DataConverter.CDouble(Rate.Text);
            proMod.Rateset = DataConverter.CLng(Rateset.SelectedValue);
            proMod.Dengji = DataConverter.CLng(Dengji.SelectedValue);
            proMod.ShiPrice = DataConverter.CDouble(ShiPrice.Text);
            proMod.LinPrice = DataConverter.CDouble(LinPrice.Text);
            proMod.LinPrice_Json = JsonHelper.AddVal("purse,sicon,point".Split(','), LinPrice_Purse_T.Text, LinPrice_Sicon_T.Text, LinPrice_Point_T.Text);
            proMod.Preset = (OtherProject.SelectedValue == null) ? "" : OtherProject.SelectedValue;  //促销
            //proMod.Integral = DataConverter.CLng(Integral.Text);
            proMod.Propeid = DataConverter.CLng(Propeid.Text);
            proMod.Recommend = DataConverter.CLng(Recommend_T.Text);
            proMod.Recommend = proMod.Recommend < 1 ? 0 : proMod.Recommend;//不允许负数
            //proMod.Largesspirx = DataConverter.CLng(Largesspirx.Text);
            proMod.Largess=Largess.Checked?1:0;
            proMod.AllClickNum = DataConverter.CLng(AllClickNum_T.Text);
            //更新时间，若没有指定则为当前时间
            proMod.UpdateTime = DataConverter.CDate(UpdateTime.Text);
            proMod.AddTime = DataConverter.CDate(AddTime.Text);
            proMod.ModeTemplate = ModeTemplate_hid.Value;
            proMod.FirstNodeID = CCate.FirstNodeID;
            proMod.bookDay = DataConverter.CLng(BookDay_T.Text);
            proMod.BookPrice = DataConverter.CDouble(BookPrice_T.Text);
            proMod.FarePrice = FareTlp_Rad.SelectedValue;
            proMod.UserType = DataConverter.CLng(Request.Form["UserPrice_Rad"]);
            proMod.Quota = DataConvert.CLng(Request.Form["Quota_Rad"]);
            proMod.DownQuota = DataConvert.CLng(Request.Form["DownQuota_Rad"]);
            switch (proMod.UserType)
            {
                case 1:
                    proMod.UserPrice = Price_Member_T.Text.Trim();
                    break;
                case 2:
                    proMod.UserPrice = Request.Form["Price_Group_Hid"];
                    break;
            }
            switch (proMod.Quota)
            {
                case 0:
                    break;
                case 2:
                    proMod.Quota_Json = Request.Form["Quota_Group_Hid"];
                    break;
            }
            switch (proMod.DownQuota)
            {
                case 0:
                    break;
                case 2:
                    proMod.DownQuota_Json = Request.Form["DownQuota_Group_Hid"];
                    break;
            }
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
            proMod.TableName = modBll.GetModelById(ModelID).TableName;
            proMod.Sales = Sales_Chk.Checked ? 1 : 2;
            proMod.Istrue = istrue_chk.Checked ? 1 : 0;
            proMod.Ishot = ishot_chk.Checked ? 1 : 0;
            proMod.Isnew = isnew_chk.Checked ? 1 : 0;
            proMod.Isbest = isbest_chk.Checked ? 1 : 0;
            proMod.Allowed = Allowed.Checked ? 1 : 0;
            proMod.GuessXML = Request.Form["GuessXML"];
            proMod.Wholesalesinfo = ChildPro_Hid.Value;
            proMod.DownCar=DataConvert.CLng(DownCar_T.Text);
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
            string danju = proMod.UserShopID + DateTime.Now.ToString("yyyyMMddHHmmss");
            if (proMod.ID < 1 || ClickType.Value.Equals("addasnew"))
            {
                proMod.Priority = 0;
                proMod.Nodeid = NodeID;
                proMod.AddTime = DateTime.Now;
                proMod.UpdateTime = DateTime.Now;
                proMod.ID = proBll.Add(table, proMod, CCate);
                proMod.Stock = DataConverter.CLng(Stock.Text);
                //多区域价格
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("guid", ProGuid) };
                SqlHelper.ExecuteSql("UPDATE ZL_Shop_RegionPrice SET [ProID]=" + proMod.ID + " WHERE [Guid]=@guid", sp);
                M_Stock stockMod = new M_Stock()
                {
                    proid = proMod.ID,
                    proname = proMod.Proname,
                    adduser = adminname,
                    StoreID = proMod.UserShopID,
                };
                int proStock = DataConverter.CLng(Stock.Text);
                if (proStock > 0)
                {
                    stockMod.proid = proMod.ID;
                    stockMod.stocktype = 0;
                    stockMod.Pronum = proStock;
                    stockMod.danju = "RK" + danju;
                    stockMod.content = "添加商品:" + proMod.Proname + "入库";
                    stockBll.AddStock(stockMod);
                }
                Response.Redirect("ContentShow.aspx?id=" + proMod.ID + "&ModelId=" + proMod.ModelID + "&NodeId=" + proMod.Nodeid);
            }
            else
            {
                proBll.Update(table, proMod, CCate);
                //int stock = proStock - DataConverter.CLng(Stock_Hid.Value);
                //if (stock != 0)
                //{
                //    stockMod.stocktype = stock > 0 ? 0 : 1;
                //    stockMod.Pronum = Math.Abs(stock);
                //    stockMod.danju = (stockMod.stocktype > 0 ? "RK" : "CK") + danju;
                //    stockMod.content = stockMod.stocktype > 0 ? "添加商品:" + proMod.Proname + "入库" : "减少商品:" + proMod.Proname + "出库";
                //    stockBll.AddStock(stockMod);
                //}
                Response.Redirect("ContentShow.aspx?id=" + proMod.ID + "&ModelId=" + proMod.ModelID + "&NodeId=" + proMod.Nodeid);
            }
        }
        //添加为新商品
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClickType.Value = "addasnew";
            EBtnSubmit_Click(sender, e);
        }
        // 会员价
        private void OtherBind(M_Product pinfo)
        {
            DataTable gpdt = gpBll.GetGroupList();
            //附加会员价,限购数,最低购买数等限制
            gpdt.Columns.Add(new DataColumn("price", typeof(string)));
            gpdt.Columns.Add(new DataColumn("quota", typeof(string)));
            gpdt.Columns.Add(new DataColumn("downquota", typeof(string)));
            if (pinfo != null && pinfo.ID > 0)
            {
                if (pinfo.UserPrice.Contains("["))
                {
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(pinfo.UserPrice);
                    if (dt.Columns.Contains("price")) { dt.Columns["price"].ColumnName = "value"; }
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow[] gps = gpdt.Select("GroupID='" + dr["gid"] + "'");
                        if (gps.Length > 0) { gps[0]["price"] = DataConvert.CDouble(dr["value"]).ToString("F2"); }
                    }
                }
                if (pinfo.Quota_Json.Contains("["))
                {
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(pinfo.Quota_Json);
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow[] gps = gpdt.Select("GroupID='" + dr["gid"] + "'");
                        if (gps.Length > 0) { gps[0]["quota"] = DataConvert.CLng(dr["value"]); }
                    }
                }
                if (pinfo.DownQuota_Json.Contains("["))
                {
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(pinfo.DownQuota_Json);
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow[] gps = gpdt.Select("GroupID='" + dr["gid"] + "'");
                        if (gps.Length > 0) { gps[0]["downquota"] = DataConvert.CLng(dr["value"]); }
                    }
                }
            }
            Price_Group_RPT.DataSource = gpdt;
            Price_Group_RPT.DataBind();
            Quota_RPT.DataSource = gpdt;
            Quota_RPT.DataBind();
            DownQuota_RPT.DataSource = gpdt;
            DownQuota_RPT.DataBind();
            //-----------------------------------------------------------
            FareTlp_Rad.DataSource = fareBll.Sel();
            FareTlp_Rad.DataBind();
            FareTlp_Rad.Items.Insert(0, new ListItem("免费", "0"));
            FareTlp_Rad.SelectedValue = "0";
        }
    }
}