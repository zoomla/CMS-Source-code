using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;
using ZoomLaCMS.Areas.User.Models.UserShop;
using ZoomLaCMS.Models.Product;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class UserShopController : ZLCtrl
    {
        // GET: /User/UserShop/
        OrderCommon orderCom = new OrderCommon();
        B_Content conBll = new B_Content();
        B_Model modBll = new B_Model();
        B_ModelField fieldBll = new B_ModelField();
        B_StoreStyleTable sstbll = new B_StoreStyleTable();
        B_CartPro cartProBll = new B_CartPro();
        B_Product proBll = new B_Product();
        B_Node nodeBll = new B_Node();
        B_Stock stockBll = new B_Stock();
        B_OrderList orderBll = new B_OrderList();
        B_Shop_FareTlp fareBll = new B_Shop_FareTlp();
        B_Payment payBll = new B_Payment();
        B_KeyWord keyBll = new B_KeyWord();
        public string Action { get { return DataConvert.CStr(Request.QueryString["Action"]).ToLower(); } }
        #region 店铺申请|信息修改
        public void Default() { Response.Redirect("Index"); return; }
        public ActionResult Index()
        {
            M_CommonData storeMod = conBll.SelMyStore(mu.UserName);
            if (storeMod == null)
            {
                return RedirectToAction("StoreApply");//申请店铺
            }
            else if (storeMod.Status != 99)//等待审核
            {
                return RedirectToAction("StoreEdit");
            }
            else
            {
                DataTable cmdinfo = conBll.GetContent(storeMod.GeneralID);
                if (cmdinfo.Rows.Count < 1) { function.WriteErrMsg("店铺信息不完整"); return Content(""); }
                DataRow dr = cmdinfo.Rows[0];
                DataTable sstDT = sstbll.GetStyleByModel(Convert.ToInt32(dr["StoreModelID"]), 1);
                M_StoreStyleTable sst = sstbll.GetStyleByID(Convert.ToInt32(dr["StoreStyleID"]));
                ViewBag.sstdp = MVCHelper.ToSelectList(sstDT, "StyleName", "ID", dr["StoreStyleID"].ToString());
                ViewBag.dr = dr;
                ViewBag.sstimg = sst == null ? "" : function.GetImgUrl(sst.StylePic);
                ViewBag.modelhtml = fieldBll.InputallHtml(DataConvert.CLng(dr["StoreModelID"]), 0, new ModelConfig()
                {
                    ValueDT = cmdinfo
                });
            }
            return View(storeMod);
        }
        [ValidateInput(false)]
        //审核完成后,用户可自由修改店铺信息
        public void UserShop_Edit()
        {
            M_CommonData storeMod = conBll.SelMyStore(mu.UserName);
            DataTable cmdinfo = conBll.GetContent(storeMod.GeneralID);
            //----------
            DataTable dt = fieldBll.GetModelFieldList(Convert.ToInt32(cmdinfo.Rows[0]["StoreModelID"]));
            Call commonCall = new Call();
            DataTable table;
            try
            {
                table = commonCall.GetDTFromMVC(dt, Request);
            }
            catch (Exception e)
            {
                function.WriteErrMsg(e.Message); return;
            }
            DataRow rs4 = table.NewRow();
            rs4[0] = "StoreName";
            rs4[1] = "TextType";
            rs4[2] = Request.Form["StoreName_T"];
            table.Rows.Add(rs4);
            DataRow rs5 = table.NewRow();
            rs5[0] = "StoreStyleID";
            rs5[1] = "int";
            rs5[2] = Convert.ToInt32(Request.Form["SSTDownList"]);
            table.Rows.Add(rs5);
            M_StoreStyleTable sst = sstbll.GetStyleByID(Convert.ToInt32(Request.Form["SSTDownList"]));
            if (sst != null)
            {
                storeMod.Template = sst.StyleUrl;
            }
            storeMod.Title = Request.Form["StoreName_T"];
            storeMod.IP = IPScaner.GetUserIP();
            conBll.UpdateContent(table, storeMod);
            function.WriteSuccessMsg("提交成功", "Index"); return;
        }
        public void StoreEdit() { Response.Redirect("StoreAuditing"); }
        public ActionResult StoreAuditing() { return View(); }
        [ValidateInput(false)]
        public ActionResult StoreApply()
        {
            DataTable moddt = modBll.GetListStore();
            M_CommonData storeMod = conBll.SelMyStore(mu.UserName);
            if (moddt.Rows.Count < 1) { function.WriteErrMsg("管理员未指定店铺申请模型"); return Content(""); }
            ViewBag.moddp = MVCHelper.ToSelectList(moddt, "ModelName", "ModelID");
            switch (Action)
            {
                case "edit":
                    {
                        if (storeMod == null) { function.WriteErrMsg("店铺申请不存在"); return null; }
                        //DataTable dtContent = conBll.GetContent(storeMod.GeneralID);
                        //ViewBag.modhtml = fieldBll.InputallHtml(storeMod.ModelID, 0, new ModelConfig()
                        //{
                        //    ValueDT = dtContent
                        //});
                    }
                    break;
                default:
                    {
                        //ViewBag.modhtml = fieldBll.InputallHtml(DataConvert.CLng(moddt.Rows[0]["ModelID"]), 0, new ModelConfig()
                        //{
                        //    Source = ModelConfig.SType.Admin
                        //});
                        //如果未开通则填写,已申请未审核则显示提示,已开通跳回index页
                        if (storeMod != null)
                        {
                            if (storeMod.Status != (int)ZLEnum.ConStatus.Audited) { return View("StoreAuditing"); }
                            else { RedirectToAction("Store_Edit"); }
                        }
                        else { storeMod = new M_CommonData() { ModelID = DataConvert.CLng(moddt.Rows[0]["ModelID"]) }; }
                    }
                    break;
            }
            return View(storeMod);
        }
        [ValidateInput(false)]
        public ActionResult Apply_Add()
        {
            int modelid = DataConvert.CLng(Request.Form["model_dp"]);
            string store = Request.Form["store_t"];
            M_CommonData CData = conBll.SelMyStore(mu.UserName);
            if (CData == null) { CData = new M_CommonData(); }
            //----------------------------------------
            if (string.IsNullOrEmpty(store)) { function.WriteErrMsg("店铺名称不能为空"); return Content(""); }
            M_StoreStyleTable sst = sstbll.GetNewStyle(modelid);
            if (sst.ID == 0) { function.WriteErrMsg("后台没有为该模型绑定可用的模板!"); return Content(""); }
            DataTable dt = fieldBll.GetModelFieldList(modelid);
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("FieldValue", typeof(string)));

            foreach (DataRow dr in dt.Rows)
            {
                if (DataConverter.CBool(dr["IsNotNull"].ToString()))
                {
                    if (string.IsNullOrEmpty(Request.Form["txt_" + dr["FieldName"].ToString()]))
                    {
                        function.WriteErrMsg(dr["FieldAlias"].ToString() + "不能为空!"); return Content("");
                    }
                }
                if (dr["FieldType"].ToString() == "FileType")
                {
                    string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
                    bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
                    string sizefield = Sett[1].Split(new char[] { '=' })[1];
                    if (chksize && sizefield != "")
                    {
                        DataRow row2 = table.NewRow();
                        row2[0] = sizefield;
                        row2[1] = "FileSize";
                        row2[2] = Request.Form["txt_" + sizefield];
                        table.Rows.Add(row2);
                    }
                }
                if (dr["FieldType"].ToString() == "MultiPicType")
                {
                    string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
                    bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
                    string sizefield = Sett[1].Split(new char[] { '=' })[1];
                    if (chksize && sizefield != "")
                    {
                        if (string.IsNullOrEmpty(Request.Form["txt_" + sizefield]))
                        {
                            function.WriteErrMsg(dr["FieldAlias"].ToString() + "的缩略图不能为空!"); return Content("");
                        }
                        DataRow row1 = table.NewRow();
                        row1[0] = sizefield;
                        row1[1] = "ThumbField";
                        row1[2] = Request.Form["txt_" + sizefield];
                        table.Rows.Add(row1);
                    }
                }

                DataRow row = table.NewRow();
                row[0] = dr["FieldName"].ToString();
                string ftype = dr["FieldType"].ToString();
                row[1] = ftype;
                string fvalue = Request.Form["txt_" + dr["FieldName"]];
                row[2] = fvalue;
                table.Rows.Add(row);
            }

            DataRow rs1 = table.NewRow();
            rs1[0] = "UserID";
            rs1[1] = "int";
            rs1[2] = mu.UserID;
            table.Rows.Add(rs1);

            DataRow rs2 = table.NewRow();
            rs2[0] = "UserName";
            rs2[1] = "TextType";
            rs2[2] = mu.UserName;
            table.Rows.Add(rs2);

            DataRow rs3 = table.NewRow();
            rs3[0] = "StoreModelID";
            rs3[1] = "int";
            rs3[2] = DataConverter.CLng(modelid);
            table.Rows.Add(rs3);

            if (!string.IsNullOrEmpty(sst.StyleName))
            {

                DataRow rs5 = table.NewRow();
                rs5[0] = "StoreStyleID";
                rs5[1] = "int";
                rs5[2] = sst.ID;
                table.Rows.Add(rs5);

                DataRow rs4 = table.NewRow();
                rs4[0] = "StoreName";
                rs4[1] = "TextType";
                rs4[2] = store;
                table.Rows.Add(rs4);

                CData.ModelID = modelid;
                CData.NodeID = 0;
                CData.TableName = modBll.GetModelById(CData.ModelID).TableName;
                CData.Title = store;
                CData.Inputer = mu.UserName;
                CData.EliteLevel = 0;
                CData.Status = 0;
                CData.InfoID = "";
                CData.SpecialID = "";
                CData.Template = sst.StyleUrl;
                CData.TagKey = "";
                CData.PdfLink = "";
                CData.Titlecolor = "";
                CData.IP = IPScaner.GetUserIP();
                CData.UpDateTime = DateTime.Now;
                CData.CreateTime = DateTime.Now;
                if (CData.GeneralID > 0) { conBll.UpdateContent(table, CData); }
                else { conBll.AddContent(table, CData); }
            }
            return View("StoreAuditing");
        }
        public void ViewMyStore()
        {
            M_CommonData storeMod = conBll.SelMyStore_Ex(ref err);
            if (!string.IsNullOrEmpty(err)) { function.WriteErrMsg(err); return; }
            Response.Redirect("/Store/StoreIndex?id=" + storeMod.GeneralID); return;
        }
        #endregion
        #region 库存管理
        public ActionResult StockList()
        {
            M_CommonData storeMod = conBll.SelMyStore_Ex(ref err);
            if (!string.IsNullOrEmpty(err)) { function.WriteErrMsg(err); return null; }
            PageSetting setting = stockBll.SelPage(CPage, PSize, storeMod.GeneralID);
            if (Request.IsAjaxRequest())
            {
                return PartialView("StockList_List", setting);
            }
            return View(setting);
        }
        public ActionResult StockAdd()
        {
            int ProID = DataConvert.CLng(Request.QueryString["ProID"]);
            string action = DataConverter.CStr(Request.QueryString["action"]);
            M_CommonData storeMod = conBll.SelMyStore_Ex(ref err);
            if (!string.IsNullOrEmpty(err)) { function.WriteErrMsg(err); return null; }
            M_Product proMod = Stock_GetProByID(mu, ProID);
            return View(proMod);
        }
        public string Stock_Add()
        {
            int ProID = DataConvert.CLng(Request["ProID"]);
            string action = DataConverter.CStr(Request.QueryString["action"]);
            M_CommonData storeMod = conBll.SelMyStore_Ex(ref err);
            if (!string.IsNullOrEmpty(err)) { function.WriteErrMsg(err); return ""; }
            M_Product proMod = Stock_GetProByID(mu, ProID);
            M_Stock stockMod = new M_Stock();
            stockMod.proid = ProID;
            stockMod.proname = proMod.Proname;
            stockMod.Pronum = DataConverter.CLng(Request.Form["Pronum"]);
            stockMod.stocktype = DataConverter.CLng(Request.Form["stocktype_rad"]);
            string code = storeMod.GeneralID + DateTime.Now.ToString("yyyyMMddHHmmss");
            stockMod.danju = (stockMod.stocktype == 0 ? "RK" : "CK") + code;
            stockMod.UserID = mu.UserID;
            stockMod.adduser = mu.UserName;
            stockMod.StoreID = storeMod.GeneralID;
            if (stockMod.Pronum < 1) { function.WriteErrMsg("出入库数量不能小于1"); return ""; }
            switch (stockMod.stocktype)
            {
                case 0:
                    proMod.Stock += stockMod.Pronum;
                    break;
                case 1:
                    proMod.Stock -= stockMod.Pronum;
                    if (proMod.Stock < 0) { function.WriteErrMsg("出库数量不能大于库存!"); return ""; }
                    break;
                default:
                    throw new Exception("出入库操作错误");
            }
            stockBll.insert(stockMod);
            proBll.updateinfo(proMod);
            if (action.Equals("addpro")) {
                int num = stockMod.stocktype == 0 ? stockMod.Pronum : -stockMod.Pronum;
                return "<script>parent.addStock(" + num + ");</script>";
            }
            function.WriteSuccessMsg("库存操作成功", "StockList"); return "";
        }
        private M_Product Stock_GetProByID(M_UserInfo mu, int ProID)
        {
            M_Product proMod = proBll.GetproductByid(ProID);
            if (proMod == null) { function.WriteErrMsg("商品不存在"); return null; }
            if (proMod.UserID != mu.UserID) { function.WriteErrMsg("你无权操作该商品库存"); return null; }
            return proMod;
        }
        #endregion
        #region 订单管理
        public ActionResult OrderList()
        {
            VM_OrderList model = new VM_OrderList(CPage, PSize, mu, Request, ref err);
            if (!string.IsNullOrEmpty(err)) { function.WriteErrMsg(err); return null; }
            return View(model);
        }
        public PartialViewResult Order_Data()
        {
            VM_OrderList model = new VM_OrderList(CPage, PSize, mu, Request, ref err);
            return PartialView("OrderList_List", model);
        }
        #endregion
        #region 送货管理
        public ActionResult DeliverType()
        {
            PageSetting setting = fareBll.SelPage(CPage, PSize, mu.UserID);
            if (Request.IsAjaxRequest()) { return PartialView("DeliverType_List", setting); }
            else { return View("DeliverType", setting); }
        }
        public ActionResult AddDeliverType()
        {
            M_Shop_FareTlp fareMod = new M_Shop_FareTlp();
            if (Mid > 0)
            {
                fareMod = fareBll.SelReturnModel(Mid);
                if (mu.UserID != fareMod.UserID) { function.WriteErrMsg("你无权限修改该模板!"); return Content(""); }
            }
            return View(fareMod);
        }
        public void Deliver_Add()
        {
            M_Shop_FareTlp fareMod = new M_Shop_FareTlp();
            if (Mid > 0) { fareMod = fareBll.SelReturnModel(Mid); }
            fareMod.TlpName = Request.Form["TlpName_T"];
            fareMod.PriceMode = Convert.ToInt32(Request.Form["pricemod_rad"]);
            fareMod.Express = Request.Form["Fare_Hid"];
            fareMod.UserID = mu.UserID;
            //fareMod.Mail = "";
            fareMod.Remind = Request.Form["Remind_T"];
            fareMod.Remind2 = Request.Form["Remind2_T"];
            if (Mid > 0) { fareBll.UpdateByID(fareMod); }
            else { fareBll.Insert(fareMod); }
            function.WriteSuccessMsg("操作成功", "DeliverType"); return;
        }
        public int Deliver_Del(string ids)
        {
            fareBll.DelByIDS(ids, mu.UserID);
            return Success;
        }
        #endregion
        #region 商品相关
        public ActionResult AddProduct()
        {
            M_CommonData storeMod = conBll.SelMyStore_Ex(ref err);
            if (!string.IsNullOrEmpty(err)) { function.WriteErrMsg(err); return null; }
            M_Product pinfo = new M_Product();
            if (Mid > 0)
            {
                pinfo = proBll.GetproductByid(Mid);
                if (pinfo.UserShopID != storeMod.GeneralID) { function.WriteErrMsg("你无权修改该商品"); return Content(""); }
            }
            VM_Product model = new VM_Product(pinfo, Request);
            return View(model);
        }
        public ActionResult ProductSaleList() { return View(); }
        public ActionResult ProductList()
        {
            int NodeID = DataConvert.CLng(Request["NodeID"]);
            int filter = DataConvert.CLng(Request["quicksouch"]);
            M_CommonData storeMod = conBll.SelMyStore_Ex(ref err);
            if (!string.IsNullOrEmpty(err)) { function.WriteErrMsg(err); return null; }
            PageSetting setting = proBll.SelPage(CPage, PSize, storeMod.GeneralID, NodeID, Request["KeyWord"], filter);
            if (Request.IsAjaxRequest())
            {
                return PartialView("ProductList_List", setting);
            }
            else
            {
                string str = "";
                string ModeIDList = "";
                string[] ModelID = null;
                if (NodeID > 0)
                {
                    M_Node nod = nodeBll.GetNodeXML(NodeID);
                    ModeIDList = nod.ContentModel;
                    ModelID = ModeIDList.Split(',');
                    string tlp = " <div class='btn-group'><button type='button' class='btn btn-default dropdown-toggle' data-toggle='dropdown'>{0}管理<span class='caret'></span></button><ul class='dropdown-menu' role='menu'><li><a href='AddProduct.aspx?ModelID={1}&NodeID={2}'>添加{0}</a></li><li><a href='javascript:;' onclick='ShowImport();'>导入{0}</a></li></ul></div>";
                    if (ModelID != null)
                    {
                        for (int i = 0; i < ModelID.Length; i++)
                        {
                            M_ModelInfo model = modBll.GetModelById(DataConverter.CLng(ModelID[i]));
                            if (!string.IsNullOrEmpty(model.ItemName))
                            {
                                str += string.Format(tlp, model.ItemName, ModelID[i], NodeID);
                            }
                        }
                    }
                }
                ViewBag.addhtml = str;
                return View(setting);
            }
        }
        public ActionResult SelShopNode()
        {
            int nid = DataConvert.CLng(Request["NodeID"]);
            if (nid > 0)
            {
                M_Node nodeMod = nodeBll.SelReturnModel(nid);
                int mid = DataConvert.CLng(nodeMod.ContentModel.Split(',')[0]);
                //return RedirectToAction("AddProduct?NodeID=" + nid + "&ModelID=" + mid, new { nodeid = nid, modelid = mid });
                Response.Redirect("AddProduct?NodeID=" + nid + "&ModelID=" + mid); return null;
            }
            return View();
        }
        [ValidateInput(false)]
        public void Product_Add()
        {
            int NodeID = DataConvert.CLng(Request["NodeID"]);
            int ModelID = DataConvert.CLng(Request["ModelID"]);
            string adminname = mu.UserName;
            DataTable dt = fieldBll.GetModelFieldList(ModelID);
            DataTable table;
            try
            {
                table = new Call().GetDTFromMVC(dt, Request);
            }
            catch (Exception e)
            {
                function.WriteErrMsg(e.Message); return;
            }
            M_CommonData CCate = new M_CommonData();
            M_Product proMod = new M_Product();
            if (Mid > 0)
            {
                proMod = proBll.GetproductByid(Mid);
                NodeID = proMod.Nodeid;
                ModelID = proMod.ModelID;
            }
            else
            {
                proMod.Nodeid = NodeID;
                proMod.ModelID = ModelID;
                //------------------------
                CCate.NodeID = proMod.Nodeid;
                CCate.ModelID = proMod.ModelID;
                CCate.Inputer = adminname;
                CCate.TableName = modBll.GetModelById(ModelID).TableName;
                CCate.FirstNodeID = nodeBll.SelFirstNodeID(NodeID);
            }
            /*--------------proMod------------*/
            proMod.Istrue = 1;
            proMod.Properties = 0;
            proMod.Class = 0;
            proMod.Isgood = 0;
            proMod.MakeHtml = 0;
            proMod.DownCar = 1;
            proMod.AddUser = adminname;
            if (string.IsNullOrEmpty(proMod.ProCode))
            {
                proMod.ProCode = B_Product.GetProCode();
            }
            proMod.BarCode = Request.Form["BarCode"];
            proMod.Proname = Request.Form["Proname"];
            proMod.Kayword = Request.Form["tabinput"];
            keyBll.AddKeyWord(proMod.Kayword, 1);
            proMod.ProUnit = Request.Form["ProUnit"];
            proMod.AllClickNum = DataConverter.CLng(Request.Form["AllClickNum"]);
            proMod.Weight = DataConverter.CLng(Request.Form["Weight"]);
            proMod.ProClass = DataConverter.CLng(Request.Form["ProClass_Hid"]);
            proMod.IDCPrice = Request.Form["IDC_Hid"];
            proMod.PointVal = DataConverter.CLng(Request.Form["PointVal_T"]);
            proMod.Proinfo = Request.Form["Proinfo"];
            proMod.Procontent = Request.Form["procontent"];
            proMod.Clearimg = Request.Form["txt_Clearimg"];
            proMod.Thumbnails = Request.Form["txt_Thumbnails"];
            proMod.Producer = Request.Form["Producer"];
            proMod.Brand = Request.Form["Brand"];
            proMod.Quota = DataConverter.CLng(Request.Form["Quota"]);
            proMod.DownQuota = DataConverter.CLng(Request.Form["DownQuota"]);
            proMod.StockDown = DataConverter.CLng(Request.Form["StockDown"]);
            proMod.JisuanFs = DataConverter.CLng(Request.Form["JisuanFs"]);
            proMod.Rate = DataConverter.CDouble(Request.Form["Rate"]);
            proMod.Rateset = DataConverter.CLng(Request.Form["Rateset"]);
            proMod.Dengji = DataConverter.CLng(Request.Form["Dengji"]);
            proMod.ShiPrice = DataConverter.CDouble(Request.Form["ShiPrice"]);
            proMod.LinPrice = DataConverter.CDouble(Request.Form["LinPrice"]);
            proMod.LinPrice_Json = JsonHelper.AddVal("purse,sicon,point".Split(','), Request.Form["LinPrice_Purse_T"], Request.Form["LinPrice_Sicon_T"], Request.Form["LinPrice_Point_T"]);
            proMod.Propeid = DataConverter.CLng(Request.Form["Propeid"]);
            proMod.Recommend = DataConverter.CLng(Request.Form["Recommend_T"]);
            proMod.Recommend = proMod.Recommend < 1 ? 0 : proMod.Recommend;//不允许负数
            proMod.Largesspirx = DataConverter.CLng(Request.Form["Largesspirx"]);
            proMod.AllClickNum = DataConverter.CLng(Request.Form["AllClickNum_T"]);
            //更新时间，若没有指定则为当前时间
            proMod.UpdateTime = DataConverter.CDate(Request.Form["UpdateTime"]);
            proMod.AddTime = DataConverter.CDate(Request.Form["AddTime"]);
            //proMod.ModeTemplate = ModeTemplate_hid.Value;
            proMod.FirstNodeID = CCate.FirstNodeID;
            proMod.bookDay = DataConverter.CLng(Request.Form["BookDay_T"]);
            proMod.BookPrice = DataConverter.CDouble(Request.Form["BookPrice_T"]);
            proMod.FarePrice = Request.Form["FareTlp_Rad"];
            proMod.UserType = DataConverter.CLng(Request.Form["UserPrice_Rad"]);
            //--------------------------------------
            CCate.Status = 99;
            CCate.Title = proMod.Proname;
            CCate.PdfLink = "";
            CCate.EliteLevel = DataConverter.CLng(Request.Form["Dengji"]) > 3 ? 1 : 0;
            CCate.InfoID = "";
            CCate.SpecialID = "";
            //CCate.Template = ModeTemplate_hid.Value;
            CCate.DefaultSkins = 0;
            switch (proMod.UserType)
            {
                case 1:
                    proMod.UserPrice = DataConvert.CDouble(Request["Price_Member_T"]).ToString("F2");
                    break;
                case 2:
                    DataTable updt = new DataTable();
                    updt.Columns.Add(new DataColumn("gid", typeof(int)));
                    updt.Columns.Add(new DataColumn("price", typeof(double)));
                    string[] prices = Request.Form["Price_Group_T"].Split(',');
                    string[] gids = Request.Form["GroupID_Hid"].Split(',');
                    for (int i = 0; i < gids.Length; i++)
                    {
                        DataRow dr = updt.NewRow();
                        dr["gid"] = Convert.ToInt32(gids[i]);
                        dr["price"] = DataConverter.CDouble(prices[i]);
                        updt.Rows.Add(dr);
                    }
                    proMod.UserPrice = JsonConvert.SerializeObject(updt);
                    break;
            }
            proMod.TableName = modBll.GetModelById(ModelID).TableName;
            proMod.Sales = string.IsNullOrEmpty(Request.Form["Sales_Chk"]) ? 2 : 1;
            proMod.Ishot = string.IsNullOrEmpty(Request.Form["ishot_chk"]) ? 0 : 1;
            proMod.Isnew = string.IsNullOrEmpty(Request.Form["isnew_chk"]) ? 0 : 1;
            proMod.Isbest = string.IsNullOrEmpty(Request.Form["isbest_chk"]) ? 0 : 1;
            proMod.Allowed = string.IsNullOrEmpty(Request.Form["Allowed"]) ? 0 : 1;
            proMod.GuessXML = Request.Form["GuessXML"];
            proMod.Wholesalesinfo = Request.Form["ChildPro_Hid"];
            //捆绑商品
            if (!string.IsNullOrEmpty(Request.Form["Bind_Hid"]))
            {
                //获取绑定商品
                DataTable binddt = JsonHelper.JsonToDT(Request.Form["Bind_Hid"]);
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
            string ClickType = Request.Form["ClickType"];
            string danju = proMod.UserShopID + DateTime.Now.ToString("yyyyMMddHHmmss");
            {
                //店铺专用
                M_CommonData storeMod = conBll.SelMyStore_Ex(ref err);
                if (!string.IsNullOrEmpty(err)) { function.WriteErrMsg(err); return; }
                proMod.UserShopID = storeMod.GeneralID;
                proMod.UserID = mu.UserID;
            }
            if (proMod.ID < 1 || ClickType.Equals("addasnew"))
            {
                proMod.Priority = 0;
                proMod.Nodeid = NodeID;
                proMod.AddTime = DateTime.Now;
                proMod.UpdateTime = DateTime.Now;
                proMod.ID = proBll.Add(table, proMod, CCate);
                //多区域价格
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("guid", Request.Form["ProGuid"]) };
                SqlHelper.ExecuteSql("UPDATE ZL_Shop_RegionPrice SET [ProID]=" + proMod.ID + " WHERE [Guid]=@guid", sp);
                M_Stock stockMod = new M_Stock()
                {
                    proid = proMod.ID,
                    proname = proMod.Proname,
                    adduser = adminname,
                    StoreID = proMod.UserShopID,
                };
                int proStock = DataConverter.CLng(Request.Form["Stock"]);
                if (proStock > 0)
                {
                    stockMod.proid = proMod.ID;
                    stockMod.stocktype = 0;
                    stockMod.Pronum = proStock;
                    stockMod.danju = "RK" + danju;
                    stockMod.content = "添加商品:" + proMod.Proname + "入库";
                    stockBll.AddStock(stockMod);
                }
            }
            else
            {
                proBll.Update(table, proMod, CCate);
            }
            function.WriteSuccessMsg("操作成功", "ProductList"); return;
        }
        [HttpPost]
        public string ShopNode_API()
        {
            M_APIResult retMod = new M_APIResult(M_APIResult.Success);
            int nid = DataConvert.CLng(Request["nid"]);
            //DataTable dt = DBCenter.SelWithField("ZL_Node", "NodeID,NodeName", "ParentID=" + nid);
            DataTable dt = nodeBll.GetNodeListUserShop(nid);
            dt = dt.DefaultView.ToTable(false, "NodeID", "NodeName");
            retMod.result = JsonConvert.SerializeObject(dt);
            return retMod.ToString();
        }
        public int Product_OP(string ids)
        {
            string action = Request["a"];
            if (string.IsNullOrEmpty(ids)) { return Failed; }
            switch (action)
            {
                case "addsale":
                    proBll.setproduct(1, ids);
                    break;
                case "addhot":
                    proBll.setproduct(2, ids);
                    break;
                case "addgood":
                    proBll.setproduct(3, ids);
                    break;
                case "addnew":
                    proBll.setproduct(4, ids);
                    break;
                case "removesale":
                    proBll.setproduct(6, ids);
                    break;
                case "removehot":
                    proBll.setproduct(7, ids);
                    break;
                case "removegood":
                    proBll.setproduct(8, ids);
                    break;
                case "removenew":
                    proBll.setproduct(9, ids);
                    break;
            }
            return Success;
        }
        public int Product_Del(string ids)
        {
            if (!String.IsNullOrEmpty(ids))
            {
                proBll.setproduct(13, ids);
            }
            return Success;
        }
        #endregion
        public ActionResult SaleList()
        {
            PageSetting setting = payBll.SelPage(CPage, PSize, mu.UserID);
            if (Request.IsAjaxRequest())
            {
                return PartialView("SaleList_List", setting);
            }
            else
            {
                object allmoney, MoneyTrue;
                allmoney = setting.dt.Compute("Sum(MoneyPay)", "");
                MoneyTrue = setting.dt.Compute("Sum(MoneyTrue)", "");
                return View(setting);
            }
        }
    }
}
