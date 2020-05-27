using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class UserShopController : Controller
    {
        // GET: /User/UserShop/
        B_User buser = new B_User();
        B_Content conBll = new B_Content();
        B_Model modBll = new B_Model();
        B_ModelField mfBll = new B_ModelField();
        B_StoreStyleTable sstbll = new B_StoreStyleTable();
        M_UserInfo mu = null;
        public string Action { get { return DataConvert.CStr(Request.QueryString["Action"]).ToLower(); } }
        public void Default() { Response.Redirect("Index"); }
        public void Index()
        {
            M_CommonData storeMod = new M_CommonData();
            mu = buser.GetLogin();
            storeMod = conBll.SelMyStore(mu.UserName);
            Response.Redirect("StoreApply");
        }
       
        [ValidateInput(false)]
        public ActionResult StoreApply()
        {           
            mu = buser.GetLogin();
            DataTable moddt = modBll.GetListStore();
            M_CommonData storeMod=conBll.SelMyStore(mu.UserName);
            if (moddt.Rows.Count < 1) { function.WriteErrMsg("管理员未指定店铺申请模型"); }
            ViewBag.moddp = MVCHelper.CreateDPList(moddt, "ModelName", "ModelID");
            switch (Action)
            {
                case "edit":
                    {
                        if (storeMod == null) { function.WriteErrMsg("店铺申请不存在"); }
                        DataTable dtContent = conBll.GetContent(storeMod.GeneralID);
                        ViewBag.modhtml = mfBll.InputallHtml(storeMod.ModelID, 0, new ModelConfig()
                        {
                            ValueDT=dtContent
                        });
                    }
                    break;
                default:
                    {
                        ViewBag.modhtml = mfBll.InputallHtml(DataConvert.CLng(moddt.Rows[0]["ModelID"]), 0, new ModelConfig()
                        {
                            Source = ModelConfig.SType.Admin
                        });
                        //如果未开通则填写,已申请未审核则显示提示,已开通跳回index页
                        if (storeMod != null)
                        {
                            if (storeMod.Status != (int)ZLEnum.ConStatus.Audited) { return View("StoreAuditing"); }
                            else { RedirectToAction("Store_Edit"); }
                        }
                        else { storeMod = new M_CommonData(); }
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
            M_CommonData CData = new M_CommonData();
            mu = buser.GetLogin();
            //----------------------------------------
            if (string.IsNullOrEmpty(store)) { function.WriteErrMsg("店铺名称不能为空"); }
            M_StoreStyleTable sst = sstbll.GetNewStyle(modelid);
            if (sst.ID == 0) { function.WriteErrMsg("后台没有为该模型绑定可用的模板!"); }
            DataTable dt = mfBll.GetModelFieldList(modelid);
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
                        function.WriteErrMsg(dr["FieldAlias"].ToString() + "不能为空!");
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
                            function.WriteErrMsg(dr["FieldAlias"].ToString() + "的缩略图不能为空!");
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
                conBll.AddContent(table, CData);
            }
            return View("StoreAuditing");
        }
        public ActionResult Store_Edit() { return View(); }

    }
    public class MVCHelper
    {
        public static SelectList CreateDPList(DataTable dt, string text, string value, string selected = "")
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new SelectListItem() { Text = DataConvert.CStr(dr[text]), Value = DataConvert.CStr(dr[value]) });
            }
            SelectList slist = new SelectList(list, "Value", "Text", selected);
            return slist;
        }
    }
}
