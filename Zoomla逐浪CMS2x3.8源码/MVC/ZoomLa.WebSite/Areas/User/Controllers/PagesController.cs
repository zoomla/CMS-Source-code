using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.Page;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Page;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class PagesController : ZLCtrl
    {
        B_ModelField fieldBll = new B_ModelField();
        B_Model modBll = new B_Model();
        B_Node nodeBll = new B_Node();
        B_Content conBll = new B_Content();
        B_PageReg prBll = new B_PageReg();
        B_Pub pubBll = new B_Pub();
        //黄页栏目节点
        B_Templata tempBll = new B_Templata();
        B_PageStyle styleBll = new B_PageStyle();
        public int NodeID
        {
            get
            {
                if (ViewBag.NodeID == null) { ViewBag.NodeID = DataConvert.CLng(Request.QueryString["NodeID"]); };
                return ViewBag.NodeID;
            }
            set { ViewBag.NodeID = value; }
        }
        public int ModelID
        {
            get
            {
                if (ViewBag.ModelID == null) { ViewBag.ModelID = DataConvert.CLng(Request.QueryString["ModelID"]); };
                return ViewBag.ModelID;
            }
            set { ViewBag.ModelID = value; }
        }
        public void Index() { Response.Redirect("Default"); return; }
        public ActionResult Default()
        {
            M_PageReg prMod = prBll.GetSelectByUserID(mu.UserID);
            if (prMod == null || prMod.Status == 0) { return RedirectToAction("PageInfo"); }
            return View("Default");
        }
        public ActionResult PageInfo()
        {
            //传递到页面的数据
            bool ShowRegPage = true;//显示表单
            string PageTitle = "", TableTitle = "添加企业黄页";
            int InfoID = 0;
            int SelModelID = 0; //对应申请类型，相关功能暂移除
            string menu = DataSecurity.FilterBadChar(Request.QueryString["menu"]);
            M_PageReg prMod = prBll.GetSelectByUserID(mu.UserID);
            if (prMod != null) { SelModelID = prMod.ModelID; }
            DataTable RegDT = fieldBll.SelectTableName("ZL_Pagereg", "TableName like 'ZL/_Reg/_%' escape '/' and UserID='" + mu.UserID + "'");
            DataTable UPageDT = new DataTable();
            string TableNames = "";
            if (RegDT.Rows.Count > 0)
            {
                TableNames = RegDT.Rows[0]["TableName"].ToString();
                SafeSC.CheckDataEx(TableNames);
                if (!modBll.IsExistTemplate(TableNames)) { function.WriteErrMsg("找不到系统黄页信息!请到后台创建用户模型"); return Content(""); }
                DataTable modeinfo = fieldBll.SelectTableName("ZL_Model", "TableName = '" + TableNames + "'");
                UPageDT = fieldBll.SelectTableName(TableNames, "UserID='" + mu.UserID + "'");
            }
            if (TableNames != "")
            {
                DataTable modetb = fieldBll.SelectTableName("ZL_Model", "TableName ='" + TableNames + "'");
                if (modetb.Rows.Count > 0)
                {
                    SelModelID = DataConverter.CLng(modetb.Rows[0]["ModelID"]);
                }
            }
            DataTable typeDt = modBll.GetListPage();
            if (SelModelID <= 0 && typeDt.Rows.Count > 0) { SelModelID = DataConverter.CLng(typeDt.Rows[0]["ModelID"]); }

            int UPageCount = UPageDT.Rows.Count;
            int RegCount = RegDT.Rows.Count;
            int PageStatus = 0;
            if (UPageCount > 0 && RegCount > 0) { PageStatus = DataConverter.CLng(RegDT.Rows[0]["Status"]); }
            if (UPageCount == 0 && RegCount == 0 && PageStatus != 99)//注册黄页
            {
                //ModelHtml = fieldBll.InputallHtml(SelModelID, 0, new ModelConfig() { Source = ModelConfig.SType.Admin });
            }
            else
            {
                if (UPageCount > 0 && RegCount > 0 && PageStatus != 99)//审核黄页
                {
                    #region 正在审核黄页
                    if (menu == "modifile")
                    {
                        TableTitle = "修改企业黄页";
                        //InfoID = DataConverter.CLng(RegDT.Rows[0]["InfoID"]);
                        //ModelHtml = fieldBll.InputallHtml(SelModelID, 0, new ModelConfig()
                        //{
                        //    ValueDT = UPageDT
                        //});
                    }
                    else
                    {
                        ShowRegPage = false;
                    }
                    #endregion
                }
                else if (UPageCount > 0 && RegCount > 0 && PageStatus == 99)//审核通过和注册
                {
                    #region 审核通过的用户(修改资料)
                    if (menu.Equals("modifile"))
                    {
                        TableTitle = "修改企业黄页";
                        InfoID = DataConverter.CLng(RegDT.Rows[0]["InfoID"]);
                        //显示域名绑定
                        //ModelHtml = fieldBll.InputallHtml(SelModelID, 0, new ModelConfig()
                        //{
                        //    ValueDT = UPageDT
                        //});
                    }
                    else
                    {
                        ShowRegPage = false;
                        RedirectToAction("PageInfo", "Pages", new { menu = "modifile" });
                    }
                    #endregion
                }
                else
                {
                    if (menu.Equals("modifile"))
                    {
                        DataTable dt1 = fieldBll.SelectTableName(RegDT.Rows[0]["TableName"].ToString(), "ID = " + RegDT.Rows[0]["GeneralID"] + "");
                        TableTitle = "修改企业黄页";
                        InfoID = DataConverter.CLng(RegDT.Rows[0]["InfoID"]);
                        DataTable tbinfo = fieldBll.SelectTableName(TableNames, "UserID = '" + mu.UserID + "'");
                        //ModelHtml = fieldBll.InputallHtml(SelModelID, 0, new ModelConfig() { ValueDT = UPageDT });
                    }
                    else
                    {
                        ShowRegPage = false;
                    }
                }
            }
            DataTable styleDt = styleBll.GetPagestylelist();
            styleDt.Columns["PageNodeid"].ColumnName = "TemplateID";
            styleDt.Columns["TemplateIndex"].ColumnName = "TemplateUrl";
            styleDt.Columns["TemplateIndexPic"].ColumnName = "TemplatePic";
            styleDt.Columns["PageNodeName"].ColumnName = "rname";
            ViewBag.PageTitle = PageTitle;
            ViewBag.TableTitle = TableTitle;
            ViewBag.ShowRegPage = ShowRegPage;
            if (UPageDT.Rows.Count > 0) { ViewBag.valuedr = UPageDT.Rows[0]; }
            else { ViewBag.valuedr = null; }
            //ViewBag.ModelHtml = ModelHtml;
            ViewBag.typeDt = typeDt;
            ViewBag.styleDt = styleDt;
            ViewBag.UserName = mu.UserName;
            ViewBag.ModelID = SelModelID;
            ViewBag.InfoID = InfoID;
            return View(prMod);
        }
        [ValidateInput(false)]
        public void Page_Apply()
        {
            string tempurl = Request.Form["templateUrl"];
            int InfoID = DataConverter.CLng(Request.Form["ID_Hid"]);
            int ModelID = DataConverter.CLng(Request.Form["ModelID_Hid"]);
            conBll.UpTemplata(InfoID, tempurl);
            DataTable dt = fieldBll.GetModelFieldList(ModelID);
            M_ModelInfo model = modBll.GetModelById(ModelID);
            string pagetemplate = "";
            if (model.ModelID > 0) { pagetemplate = model.ContentModule; }
            else { pagetemplate = ""; }
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
            DataRow rs1 = table.NewRow();
            rs1[0] = "UserID";
            rs1[1] = "int";
            rs1[2] = mu.UserID;
            DataRow rs2 = table.NewRow();
            rs2[0] = "UserName";
            rs2[1] = "TextType";
            rs2[2] = mu.UserName;
            DataRow rs3 = table.NewRow();
            //Styleid|黄页样式ID|数字|0
            rs3[0] = "Styleid";
            rs3[1] = "int";
            int styleid = DataConverter.CLng(Request.Form["TempleID_Hid"]);
            rs3[2] = styleid == 0 ? 1 : styleid; //未选择,则默认第一个
            table.Rows.Add(rs1);
            table.Rows.Add(rs2);
            table.Rows.Add(rs3);

            int styless = DataConverter.CLng(Request.Form["StyleID"]);
            string tablename = modBll.GetModelById(styless).TableName;
            M_PageReg crMod = prBll.GetSelectByUserID(mu.UserID);
            if (crMod == null) { crMod = new M_PageReg(); }
            crMod.CompanyName = Request.Form["CompanyName"];
            crMod.UserName = mu.UserName;
            crMod.UserID = mu.UserID;
            crMod.PageTitle = mu.UserName + "的黄页信息";
            crMod.PageInfo = Request.Form["PageInfo"];
            crMod.LOGO = Request.Form["LOGO"];
            crMod.NodeStyle = DataConverter.CLng(Request.Form["TempleID_Hid"]);
            crMod.Template = Request.Form["TempleUrl_Hid"];
            if (SiteConfig.SiteOption.RegPageStart) { crMod.Status = 0; }
            else { crMod.Status = 99; }
            crMod.TableName = tablename;
            crMod.ModelID = styless;
            crMod.CreationTime = DateTime.Now;
            if (crMod.ID > 0)
            {
                conBll.Page_Update(table, crMod);
                function.WriteSuccessMsg("修改提交成功", "PageInfo"); return;
            }
            else
            {
                crMod.TopWords = "";
                crMod.Style = 0;
                crMod.ParentPageUserID = 0;
                crMod.ParentPageID = 0;
                crMod.Keyword = "";
                crMod.InfoID = 0;
                crMod.Hits = 0;
                crMod.NavHeight = "0";
                crMod.NavColor = "";
                crMod.Description = "";
                crMod.BottonWords = "";
                int newID = conBll.Page_Add(table, crMod);
                function.WriteSuccessMsg("申请提交成功", "PageInfo"); return;
            }
        }
        #region 黄页内容
        public ActionResult AddContent()
        {
            M_CommonData Cdata = new M_CommonData();
            if (Mid > 0)
            {
                Cdata = conBll.GetCommonData(Mid);
                NodeID = Cdata.NodeID;
                ModelID = Cdata.ModelID;
            }
            else
            {
                Cdata.ModelID = ModelID;
                Cdata.NodeID = NodeID;
            }
            if ((ModelID < 1 && NodeID < 1) && Mid < 1) { function.WriteErrMsg("参数错误"); return Content(""); }
            M_ModelInfo model = modBll.GetModelById(ModelID);
            M_Templata nodeMod = tempBll.SelReturnModel(NodeID);
            if (Mid > 0)
            {
                if (mu.UserName != Cdata.Inputer) { function.WriteErrMsg("不能编辑不属于自己输入的内容!"); return Content(""); }
                DataTable dtContent = conBll.GetContent(Mid);
                //ViewBag.modelhtml = fieldBll.InputallHtml(Cdata.ModelID, Cdata.NodeID, new ModelConfig()
                //{
                //    Source = ModelConfig.SType.UserContent,
                //    ValueDT = dtContent
                //});
            }
            else
            {
                //ViewBag.modelhtml = fieldBll.InputallHtml(ModelID, NodeID, new ModelConfig()
                //{
                //    Source = ModelConfig.SType.UserContent
                //});
            }
            ViewBag.NodeName = nodeMod.TemplateName;
            ViewBag.ds = fieldBll.SelByModelID(ModelID, true);
            ViewBag.op = (Mid < 1 ? "添加" : "修改") + model.ItemName;
            ViewBag.tip = "向 <a href='MyContent?NodeId=" + nodeMod.TemplateID + "'>[" + nodeMod.TemplateName + "]</a> 节点" + ViewBag.op;
            return View(Cdata);
        }
        public void EditContent()
        {
            int gid = DataConvert.CLng(Request.QueryString["GeneralID"]);
            if (gid < 1) { gid = DataConvert.CLng(Request.QueryString["ID"]); }
            if (gid < 1) { function.WriteErrMsg("未指定需要修改的内容"); return; }
            Response.Redirect("AddContent?ID=" + gid); return;
        }
        [HttpPost]
        [ValidateInput(false)]
        public void Content_Add()
        {
            M_CommonData CData = new M_CommonData();
            if (Mid > 0)
            {
                CData = conBll.SelReturnModel(Mid);
                if (!CData.Inputer.Equals(mu.UserName)) { function.WriteErrMsg("你无权修改该内容"); return; }
                ModelID = CData.ModelID;
                NodeID = CData.NodeID;
            }
            Call commonCall = new Call();
            DataTable dt = fieldBll.SelByModelID(ModelID,false);
            DataTable table;
            try
            {
                table = commonCall.GetDTFromMVC(dt, Request);
            }
            catch (Exception e)
            {
                function.WriteErrMsg(e.Message); return;
            }
            CData.Title = Request.Form["Title"];
            CData.UpDateTime = DateTime.Now;
            if (CData.GeneralID > 0)
            {
                conBll.UpdateContent(table, CData);
            }
            else
            {
                CData.EliteLevel = 0;
                CData.Status = SiteConfig.YPage.IsAudit ? 99 : 0;
                CData.InfoID = "";
                CData.Template = "";
                CData.TagKey = "";
                CData.SpecialID = "";
                CData.PdfLink = "";
                CData.Titlecolor = "";
                CData.DefaultSkins = 0;
                CData.Inputer = mu.UserName;
                CData.NodeID = NodeID;
                CData.ModelID = ModelID;
                CData.TableName = modBll.GetModelById(ModelID).TableName;
                CData.CreateTime = DateTime.Now;
                CData.FirstNodeID = GetFriestNode(NodeID);
                CData.GeneralID = conBll.AddContent(table, CData);
            }
            function.WriteSuccessMsg("添加成功", "MyContent?NodeID=" + NodeID); return;
        }
        // 获得第一级节点ID
        private int GetFriestNode(int NodeID)
        {
            M_Node nodeinfo = nodeBll.GetNodeXML(NodeID);
            int ParentID = nodeinfo.ParentID;
            if (DataConverter.CLng(nodeinfo.ParentID) > 0)
            {
                return GetFriestNode(nodeinfo.ParentID);
            }
            else
            {
                return nodeinfo.NodeID;
            }
        }
        public ActionResult MyContent()
        {
            string Status = DataConverter.CStr(Request["status"]);
            if (NodeID != 0)
            {
                M_Node nod = nodeBll.GetNodeXML(NodeID);
                if (nod.NodeListType == 2) { return RedirectToAction("MyProduct","Product"); }
                M_Templata tempMod = tempBll.SelReturnModel(NodeID);
                string[] ModelID = tempMod.Modelinfo.Split(',');
                string AddContentlink = "";
                for (int i = 0; i < ModelID.Length; i++)
                {
                    M_ModelInfo infoMod = modBll.SelReturnModel(DataConverter.CLng(ModelID[i]));
                    if (infoMod == null) continue;
                    AddContentlink += "<a href='AddContent?NodeID=" + NodeID + "&ModelID=" + infoMod.ModelID + "' class='btn btn-info' style='margin-right:5px;'><i class='fa fa-plus'></i> 添加" + infoMod.ItemName + "</a>";
                }
                ViewBag.addhtml = AddContentlink;
            }
            DataTable RegDT = fieldBll.SelectTableName("ZL_Pagereg", "TableName like 'ZL/_Reg/_%' escape '/' and UserID='" + mu.UserID + "'");
            ViewBag.pageid = DataConverter.CLng(RegDT.Rows[0]["ID"]);
            ViewBag.NodeID = NodeID;
            ViewBag.Status = Status;
            ViewBag.PageID = prBll.GetSelectByUserID(mu.UserID).ID;
            PageSetting setting = conBll.SelContent(CPage, PSize, NodeID, Status, mu.UserName, Request["skey"], "ZL_Page");
            return View(setting);
        }
        public PartialViewResult Content_Data()
        {
            PageSetting setting = null;
            string action = Request["action"] ?? "";
            string status = action.Equals("recycle") ? ((int)ZLEnum.ConStatus.Recycle).ToString() : "";
            setting = conBll.SelContent(CPage, PSize, NodeID, status, mu.UserName, Request["skey"], "ZL_Page");
            ViewBag.PageID = prBll.GetSelectByUserID(mu.UserID).ID;
            return PartialView("MyContent_List", setting);
        }
        //移入回收站
        public int Content_Del(string ids)
        {
            string[] idArr = ids.Split(',');
            for (int i = 0; i < idArr.Length; i++)
            {
                conBll.SetDel(Convert.ToInt32(idArr[i]));
            }
            return 1;
        }
        //彻底删除
        public int Content_RealDel(string ids)
        {
            if (conBll.DelByIDS(ids)) return 1;
            else return 0;
        }
        //还原
        public int Content_Recover(string ids)
        {
            conBll.Reset(ids);
            return 1;
        }
        #endregion
        #region 栏目管理
        public ActionResult ClassManage()
        {
            DataTable Uptab = prBll.Sel("UserID=" + mu.UserID, "");
            if (Uptab.Rows.Count > 0)
            {
                if (Uptab.Rows[0]["Status"].ToString() != "99")
                {
                    function.WriteErrMsg("您的黄页还未通过审核！"); return Content("");
                }
            }
            else
            {
                function.WriteErrMsg("您还未注册黄页！"); return Content("");
            }
            PageSetting setting = tempBll.ReadUserall_Page(CPage, PSize, 0, mu.UserID, DataConverter.CLng(Uptab.Rows[0]["NodeStyle"]));
            return View(setting);
        }
        public ActionResult AddNode()
        {
            if (!SiteConfig.YPage.UserCanNode) { function.WriteErrMsg("不允许自建栏目!"); return Content(""); }
            M_Templata model = new M_Templata() { IsTrue = 1 };
            string op = "添加";
            string menu = Request["menu"] ?? "";
            int tempID = DataConverter.CLng(Request["ID"]);
            if (menu.ToLower().Equals("edit") && tempID >= 0)
            {
                op = "修改";
                model = tempBll.Getbyid(tempID) ?? new M_Templata();
            }
            DataTable nodes = nodeBll.Sel();
            ViewBag.ClassNode = GetNodeList(nodes);
            ViewBag.op = op;
            return View(model);
        }
        private List<SelectListItem> GetNodeList(DataTable dt)
        {
            List<SelectListItem> nodeList = new List<SelectListItem>();
            nodeList.Add(new SelectListItem { Text = "选择捆绑节点", Value = "0" });
            foreach (DataRow row in dt.Rows)
            {
                nodeList.Add(new SelectListItem { Text = row.Field<string>("NodeName"), Value = row.Field<int>("NodeID").ToString() });
            }
            return nodeList;
        }
        [HttpPost]
        [ValidateInput(false)]
        public void Node_Add()
        {
            M_PageReg pageinfo = prBll.GetSelectByUserID(mu.UserID);

            M_Templata model = new M_Templata();
            string menu = Request["menu"] ?? "";
            int tempID = DataConverter.CLng(Request["ID"]);
            if (menu.ToLower().Equals("edit") && tempID >= 0)
            {
                model = tempBll.Getbyid(tempID);
            }
            model.UserID = mu.UserID;
            model.Username = mu.UserName;
            model.Addtime = DateTime.Now;
            model.ContentFileEx = "html";
            model.IsTrue = DataConverter.CLng(Request.Form["IsTrue"]);
            model.NodeFileEx = "html";
            model.OpenType = "_blank";
            model.OrderID = DataConverter.CLng(Request.Form["OrderID"]);
            model.PageMetakeyinfo = Request.Form["PageMetakeyinfo"];
            model.PageMetakeyword = Request.Form["PageMetakeyword"];
            model.TemplateName = Request.Form["TemplateName"];
            model.TemplateType = 2;
            model.TemplateUrl = "";
            model.ParentNode = DataConverter.CLng(Request.Form["ParentNode"]);
            model.Modelinfo = nodeBll.GetNodeXML(model.ParentNode).ContentModel;
            model.UserGroup = pageinfo.NodeStyle.ToString();
            model.Nodeimgtext = Request.Form["Nodeimgtext"];

            if (menu.ToLower().Equals("edit") && tempID >= 0)
            {
                tempBll.Update(model);
                function.WriteSuccessMsg("修改成功!", "ClassManage"); return;
            }
            else
            {
                tempBll.Add(model);
                function.WriteSuccessMsg("添加成功!", "ClassManage"); return;
            }
        }
        public PartialViewResult Node_Data()
        {
            DataTable Uptab = prBll.Sel("UserID=" + mu.UserID, "");
            PageSetting setting = tempBll.ReadUserall_Page(CPage, PSize, 0, mu.UserID, DataConverter.CLng(Uptab.Rows[0]["NodeStyle"]));
            return PartialView("ClassManage_List", setting);
        }
        //黄页栏目批量删除
        public int Node_Del(string id)
        {
            if (tempBll.DelByIDS(id)) return 1;
            else return 0;
        }
        //黄页栏目批量隐藏
        public int Node_Hide(string id)
        {
            if (tempBll.ChangeStatus(id, 0)) return 1;
            else return 0;
        }
        public ActionResult NodeTree()
        {
            string hasChild = "<a href='MyContent?NodeID={0}' id='a{0}' class='list1' target='main_right1'>{2}<span class='list_span'>{1}</span></a>";
            string noChild = "<a href='MyContent?NodeID={0}' target='main_right1'>{2}{1}</a>";
            M_PageReg regMod = prBll.SelModelByUid(mu.UserID);
            DataTable nodeDT = tempBll.SelByStyleAndPid(regMod.NodeStyle);
            ViewBag.NodeHtml = "<ul class='tvNav'><li><a  class='list1' id='a0' href='javascript:;' ><span class='list_span'>全部节点</span><span class='fa fa-list'></span></a>" + B_Node.GetLI(nodeDT, hasChild, noChild) + "</li></ul>";
            return View();
        }
        #endregion
        #region 互动模块
        //注册企业黄页
        public ActionResult Modifyinfo()
        {
            M_PageReg regMod = prBll.GetSelectByUserID(mu.UserID);
            if (regMod == null) { function.WriteErrMsg("您还未注册黄页！"); return Content(""); }
            else if (regMod.Status != (int)ZLEnum.ConStatus.Audited) { function.WriteErrMsg("您的黄页还未通过审核！"); return Content(""); }
            return View(regMod);
        }
        [HttpPost]
        public void Modifyinfo_Edit()
        {
            M_PageReg regMod = prBll.GetSelectByUserID(mu.UserID);
            regMod.Keyword = Request.Form["Keyword"];
            regMod.PageTitle = Request.Form["PageTitle"];
            regMod.Description = Request.Form["Description"];
            regMod.NavColor = Request.Form["NavColor"];
            regMod.NavBackground = Request.Form["NavBackground"];
            regMod.NavHeight = Request.Form["NavHeight"];
            regMod.TopWords = Request.Form["TopWords"];
            regMod.BottonWords = Request.Form["BottonWords"];
            prBll.Update(regMod);
            function.WriteSuccessMsg("修改成功", "Modifyinfo"); return;
        }
        public ActionResult PageApply()
        {
            return View();
        }
        public ActionResult PubView()
        {
            int TopID = DataConverter.CLng(Request["menu"]);
            int PubID = DataConverter.CLng(Request["pubid"]);
            string Small = Request["small"] ?? "";
            int ID = DataConverter.CLng(Request["ID"]);
            if (ID <= 0) { function.WriteErrMsg("缺少ID参数"); return Content(""); }
            M_Pub pubMod = pubBll.GetSelect(PubID);
            int ModelID = pubMod == null ? 0 : pubMod.PubModelID;
            if (ModelID <= 0) { function.WriteErrMsg("缺少用户模型ID参数!"); return Content(""); }
            M_ModelInfo model = modBll.GetModelById(DataConverter.CLng(ModelID));
            DataTable DataDt = buser.GetUserModeInfo(model.TableName, ID, 18);
            DataTable newDt = new DataTable();
            if (DataDt.Rows.Count > 0)
            {
                DataRow DataDr = DataDt.Rows[0];
                DataTable FieldDt = fieldBll.GetModelFieldList(ModelID);
                newDt.Columns.Add("Title");
                newDt.Columns.Add("Content");
                DataRow dr1 = newDt.NewRow(); dr1["Title"] = "ID"; dr1["Content"] = DataDr["ID"]; newDt.Rows.Add(dr1);
                DataRow dr2 = newDt.NewRow(); dr2["Title"] = "用户名"; dr2["Content"] = DataDr["PubUserName"]; newDt.Rows.Add(dr2);
                DataRow dr3 = newDt.NewRow(); dr3["Title"] = "标题"; dr3["Content"] = DataDr["PubTitle"]; newDt.Rows.Add(dr3);
                DataRow dr4 = newDt.NewRow(); dr4["Title"] = "内容"; dr4["Content"] = DataDr["PubContent"]; newDt.Rows.Add(dr4);
                DataRow dr5 = newDt.NewRow(); dr5["Title"] = "IP地址"; dr5["Content"] = DataDr["PubIP"]; newDt.Rows.Add(dr5);
                DataRow dr6 = newDt.NewRow(); dr6["Title"] = "添加时间"; dr6["Content"] = DataDr["PubAddTime"]; newDt.Rows.Add(dr6);
                foreach (DataRow dr in FieldDt.Rows)
                {
                    DataRow row = newDt.NewRow();
                    row["Title"] = dr["FieldAlias"];
                    row["Content"] = DataDr[dr["FieldName"].ToString()];
                    newDt.Rows.Add(row);
                }
            }
            ViewBag.Details = newDt;
            ViewBag.ReturnUrl = Small.ToLower().Equals("small") ? "ViewSmallPub?pubid=" + PubID + "&ID=" + TopID : "ViewPub?pubid=" + PubID;
            ViewBag.AddUrl = "AddPub?Pubid=" + PubID + "&Parentid=" + ID;
            return View();
        }
        public ActionResult ViewPub()
        {
            int PubID = DataConverter.CLng(Request["pubid"]);
            M_Pub pubMod = pubBll.GetSelect(PubID);
            int ModelID = pubMod == null ? 0 : pubMod.PubModelID;
            M_ModelInfo model = modBll.GetModelById(ModelID);
            PageSetting setting = new PageSetting();
            setting = buser.GetUserModeInfo_Page(CPage, PSize, model.TableName, mu.UserID, 16);
            ViewBag.PubID = PubID;
            ViewBag.PubType = pubMod.PubType;
            ViewBag.TableName = model.TableName;
            if (Request.IsAjaxRequest())
            {
                return PartialView("ViewPub_List", setting);
            }
            return View(setting);
        }
        public ActionResult ViewSmallPub()
        {
            int PubID = DataConverter.CLng(Request["pubid"]);
            int ID = DataConverter.CLng(Request["ID"]);
            int Type = DataConverter.CLng(Request["type"] ?? "1");
            M_Pub pubMod = pubBll.GetSelect(PubID);
            int ModelID = pubMod == null ? 0 : pubMod.PubModelID;
            M_ModelInfo model = modBll.GetModelById(ModelID);
            PageSetting setting = new PageSetting();
            switch (Type)
            {
                case 2:
                    setting = buser.GetUserModeInfo_Page(CPage, PSize, model.TableName, ID, 19);
                    break;
                case 3:
                    setting = buser.GetUserModeInfo_Page(CPage, PSize, model.TableName, ID, 20);
                    break;
                default:
                    setting = buser.GetUserModeInfo_Page(CPage, PSize, model.TableName, ID, 13);
                    break;
            }
            ViewBag.PubID = PubID;
            ViewBag.PubType = pubMod.PubType;
            ViewBag.ID = ID;
            ViewBag.TableName = model.TableName;
            if (Request.IsAjaxRequest())
            {
                return PartialView("ViewSmallPub_List", setting);
            }
            return View(setting);
        }
        //审核互动信息
        public void EditPubstart()
        {
            int ID = DataConverter.CLng(Request["ID"]);
            int PubID = DataConverter.CLng(Request["PubID"]);
            string menu = Request["menu"] ?? "";
            string small = Request["small"] ?? "";
            if (PubID < 1) { function.WriteErrMsg("缺少用户ID参数！"); return; }
            M_Pub pubMod = pubBll.GetSelect(PubID);
            int ModelID = pubMod.PubModelID;
            M_ModelInfo model = modBll.GetModelById(ModelID);

            string returnUrl = "ViewPub?PubID=" + PubID;
            if (small.Equals("small")) { returnUrl = "ViewSmallPub?PubID=" + PubID + "&ID=" + ID; }
            if (menu.ToLower().Equals("false"))
            {
                if (buser.DelModelInfo2(model.TableName, ID, 12)) { function.WriteSuccessMsg("审核成功!", returnUrl); return; }
                else { function.WriteErrMsg("审核失败", returnUrl); return; }
            }
            else
            {
                if (buser.DelModelInfo2(model.TableName, ID, 13)) { function.WriteSuccessMsg("取消审核成功!", returnUrl); return; }
                else { function.WriteErrMsg("取消审核失败!", returnUrl); return; }
            }
        }
        public ActionResult AddPub()
        {
            int PubID = DataConverter.CLng(Request["pubid"]);
            int ParentID = DataConverter.CLng(Request["parentid"]);
            int ID = DataConverter.CLng(Request["ID"]);
            if (PubID < 1 || ParentID < 1) { function.WriteErrMsg("缺少用户参数"); return Content(""); }
            M_Pub pubMod = pubBll.GetSelect(PubID);
            int ModelID = pubMod.PubModelID;
            M_ModelInfo model = modBll.GetModelById(ModelID);
            ViewBag.ModelHtml = fieldBll.InputallHtml(ModelID, 0, new ModelConfig() { Source = ModelConfig.SType.UserBase });
            ViewBag.ModelName = "添加" + model.ModelName;
            return View();
        }
        //删除互动信息
        public ActionResult Delpub()
        {
            int ID = DataConverter.CLng(Request["id"]);
            int pubid = DataConverter.CLng(Request["pubid"]);
            string small = Request["small"] ?? "";
            if (pubid <= 0) { function.WriteErrMsg("缺少用户ID参数！"); return Content(""); }
            int ModelID = pubBll.GetSelect(pubid).PubModelID;
            if (buser.DelModelInfo(modBll.GetModelById(ModelID).TableName, ID))
            {

                if (!string.IsNullOrEmpty(small)) { return RedirectToAction("ViewSmallPub", new { pubid = pubid, id = small }); }
                else { function.WriteSuccessMsg("删除成功!", "ViewPub?pubid=" + pubid); return Content(""); }
            }
            else
            {
                if (!string.IsNullOrEmpty(small)) { return RedirectToAction("ViewSmallPub", new { modelid = ModelID, id = small }); }
                else { function.WriteErrMsg("删除失败!", "ViewPub?id=" + ModelID); return Content(""); }
            }
        }
        public void Pub_Add()
        {
            int PubID = DataConverter.CLng(Request["pubid"]);
            int ParentID = DataConverter.CLng(Request["parentid"]);
            int ID = DataConverter.CLng(Request["ID"]);
            M_Pub pubMod = pubBll.GetSelect(PubID);
            int ModelID = pubMod.PubModelID;
            M_ModelInfo model = modBll.GetModelById(ModelID);
            DataTable fieldDt = fieldBll.GetModelFieldListall(ModelID);
            DataTable userDt = buser.GetUserModeInfo(model.TableName, ParentID, 18);
            string ContentID = "1";
            if (userDt.Rows.Count > 0) { ContentID = userDt.Rows[0]["PubContent"].ToString(); }
            else { function.WriteErrMsg("输入无效参数！"); return; }

            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("FieldValue", typeof(string)));

            DataRow rowa = table.NewRow();
            rowa[0] = "PubTitle";
            rowa[1] = "TextType";
            rowa[2] = Request.Form["PubTitle"];
            table.Rows.Add(rowa);

            DataRow rowa1 = table.NewRow();
            rowa1[0] = "PubContent";
            rowa1[1] = "MultipleTextType";
            rowa1[2] = Request.Form["PubContent"];
            table.Rows.Add(rowa1);

            DataRow rowa2 = table.NewRow();
            rowa2[0] = "PubIP";
            rowa2[1] = "TextType";
            rowa2[2] = Request.UserHostAddress;
            table.Rows.Add(rowa2);

            DataRow rowa3 = table.NewRow();
            rowa3[0] = "PubAddTime";
            rowa3[1] = "DateType";
            rowa3[2] = Request.UserHostAddress;
            table.Rows.Add(rowa3);

            DataRow rowa4 = table.NewRow();
            rowa4[0] = "Pubupid";
            rowa4[1] = "int";
            rowa4[2] = PubID;
            table.Rows.Add(rowa4);

            DataRow rowa5 = table.NewRow();
            rowa5[0] = "PubContentid";
            rowa5[1] = "int";
            rowa5[2] = ContentID;
            table.Rows.Add(rowa5);

            DataRow rowa6 = table.NewRow();
            rowa6[0] = "Parentid";
            rowa6[1] = "int";
            rowa6[2] = ParentID;
            table.Rows.Add(rowa6);

            M_UserInfo aa = buser.GetLogin();

            DataRow rowa7 = table.NewRow();
            rowa7[0] = "PubUserName";
            rowa7[1] = "TextType";
            rowa7[2] = aa.UserName;
            table.Rows.Add(rowa7);

            DataRow rowa8 = table.NewRow();
            rowa8[0] = "PubUserID";
            rowa8[1] = "TextType";
            rowa8[2] = aa.UserID;
            table.Rows.Add(rowa8);

            foreach (DataRow dr in fieldDt.Rows)
            {
                if (DataConverter.CBool(dr["IsNotNull"].ToString()))
                {
                    if (string.IsNullOrEmpty(Request.Form["txt_" + dr["FieldName"].ToString()]))
                    {
                        function.WriteErrMsg(dr["FieldAlias"].ToString() + "不能为空!"); return;
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
                            function.WriteErrMsg(dr["FieldAlias"].ToString() + "的缩略图不能为空!"); return;
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
                if (ftype == "NumType")
                {
                    string[] fd = dr["Content"].ToString().Split(new char[] { ',' });
                    string[] fdty = fd[1].Split(new char[] { '=' });

                    int numstyle = DataConverter.CLng(fdty[1]);
                    if (numstyle == 1)
                        ftype = "int";
                    if (numstyle == 2)
                        ftype = "float";
                    if (numstyle == 3)
                        ftype = "money";
                }
                row[1] = ftype;
                string fvalue = Request.Form["txt_" + dr["FieldName"].ToString()];
                row[2] = fvalue;
                table.Rows.Add(row);
            }
            try
            {
                if (buser.AddUserModel(table, model.TableName))
                {
                    function.WriteSuccessMsg("添加成功!", "ViewSmallPub?Pubid=" + PubID + "&ID=" + ParentID); return;
                }
            }
            catch
            {

                Response.Redirect("ViewSmallPub?Pubid=" + PubID + "&ID=" + ParentID); return;
            }
        }
        public int Pub_Del(string id)
        {
            string TableName = Request["TableName"] ?? "";
            if (!string.IsNullOrEmpty(TableName) && buser.DelModelInfoAllo(TableName, id)) { return 1; }
            else { return 0; }
        }
        #endregion
    }
}
