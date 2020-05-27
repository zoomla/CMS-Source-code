using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Controls;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class ContentController : ZLCtrl
    {
        B_Favorite favBll = new B_Favorite();
        B_Content conBll = new B_Content();
        B_Node nodeBll = new B_Node();
        B_Model modBll = new B_Model();
        B_ModelField fieldBll = new B_ModelField();
        B_UserPromotions upBll = new B_UserPromotions();
        public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        public int NodeID
        {
            get
            {
                if (ViewBag.NodeID == null) { ViewBag.NodeID = DataConvert.CLng(Request["NodeID"]); }
                return DataConvert.CLng(ViewBag.NodeID);
            }
            set { ViewBag.NodeID = value; }
        }
        public int ModelID
        {
            get
            {
                if (ViewBag.ModelID == null) { ViewBag.ModelID = DataConvert.CLng(Request["ModelID"]); }
                return DataConvert.CLng(ViewBag.ModelID);
            }
            set { ViewBag.ModelID = value; }
        }
        public ActionResult MyContent()
        {
            string Status = Request.QueryString["Status"] ?? "";
            DataTable nodeDT = nodeBll.SelByPid(0, true);
            string nodeids = upBll.GetNodeIDS(mu.GroupID);
            if (!string.IsNullOrEmpty(nodeids))
            {
                nodeDT.DefaultView.RowFilter = "NodeID in(" + nodeids + ")";
            }
            else
            {
                nodeDT.DefaultView.RowFilter = "1>2";//无权限，则去除所有
            }
            nodeDT = nodeDT.DefaultView.ToTable();
            C_TreeView treeMod = new C_TreeView()
            {
                NodeID = "NodeID",
                NodeName = "NodeName",
                NodePid = "ParentID",
                DataSource = nodeDT,
                liAllTlp = "<a href='MyContent'>全部内容</a>",
                LiContentTlp = "<a href='MyContent?NodeID=@NodeID'>@NodeName</a>",
                SelectedNode = NodeID.ToString()
            };
            if (NodeID != 0)
            {
                M_Node nod = nodeBll.GetNodeXML(NodeID);
                if (nod.NodeListType == 2)
                {
                    Response.Redirect("Myproduct?NodeID=" + NodeID);//跳转到商城
                }
                string ModeIDList = nod.ContentModel;
                string[] ModelID = ModeIDList.Split(',');
                string AddContentlink = "";
                for (int i = 0; i < ModelID.Length; i++)
                {
                    M_ModelInfo infoMod = modBll.SelReturnModel(DataConverter.CLng(ModelID[i]));
                    if (infoMod == null) continue;
                    if (infoMod.ModelType != 5)
                    {
                        AddContentlink += "<a href='AddContent?NodeID=" + NodeID + "&ModelID=" + infoMod.ModelID + "' class='btn btn-info' style='margin-right:5px;'><i class='fa fa-plus'></i> 添加" + infoMod.ItemName + "</a>";
                    }
                }
                ViewBag.addhtml = AddContentlink;
            }

            PageSetting setting = SelContent(CPage, PSize, NodeID, Status, mu.UserName, Request["skey"]);
            ViewBag.Status = Status;
            ViewBag.treeMod = treeMod;
            ViewBag.setting = setting;
            return View();
        }
        public ActionResult ShowContent()
        {
            M_CommonData conMod = conBll.SelReturnModel(Mid);
            if (conMod == null) { function.WriteErrMsg("内容不存在"); }
            if (!mu.UserName.Equals(conMod.Inputer)) { function.WriteErrMsg("你无权查看该内容"); }
            M_Node nodeMod = nodeBll.SelReturnModel(conMod.NodeID);
            DataTable contentDT = conBll.GetContentByItems(conMod.TableName, conMod.GeneralID);
            ViewBag.nodeMod = nodeMod;
            ViewBag.modelhtml = fieldBll.InputallHtml(conMod.ModelID, conMod.NodeID, new ModelConfig()
            {
                ValueDT = contentDT,
                Mode = ModelConfig.SMode.PreView
            });
            return View(conMod);
        }
        public ActionResult AddContent() 
        {
            M_CommonData Cdata = new M_CommonData();
            NodeID = DataConvert.CLng(Request.QueryString["NodeID"]);
            ModelID = DataConvert.CLng(Request.QueryString["ModelID"]);
            if (Mid > 0)
            {
                Cdata = conBll.GetCommonData(Mid);
                NodeID = Cdata.NodeID;
                ModelID = Cdata.ModelID;
            }
            if ((ModelID < 1 && NodeID < 1) && Mid < 1) { function.WriteErrMsg("参数错误"); }
            M_ModelInfo model = modBll.GetModelById(ModelID);
            M_Node nodeMod = nodeBll.SelReturnModel(NodeID);
            if (Mid > 0)
            {
                if (mu.UserName != Cdata.Inputer) { function.WriteErrMsg("不能编辑不属于自己输入的内容!"); }
                DataTable dtContent = conBll.GetContent(Mid);
                ViewBag.modelhtml = fieldBll.InputallHtml(Cdata.ModelID, Cdata.NodeID, new ModelConfig()
                {
                    Source = ModelConfig.SType.UserContent,
                    ValueDT = dtContent
                });
            }
            else
            {
                ViewBag.modelhtml = fieldBll.InputallHtml(ModelID, NodeID, new ModelConfig()
                {
                    Source = ModelConfig.SType.UserContent
                });
            }
            ViewBag.ds = fieldBll.SelByModelID(ModelID, true);
            ViewBag.op = (Mid < 1 ? "添加" : "修改") + model.ItemName;
            ViewBag.tip = "向 <a href='MyContent?NodeId=" + nodeMod.NodeID + "'>[" + nodeMod.NodeName + "]</a> 节点" + ViewBag.op;
            return View(Cdata);
        }
        //其不是根据名称,而是根据顺序来取值
        public PartialViewResult Content_Data()
        {
            string Status = Request["Status"] ?? "";
            PageSetting setting = SelContent(CPage, PSize, NodeID, Status, mu.UserName, Request["skey"]);
            return PartialView("MyContent_List", setting);
        }
        [HttpPost]
        [ValidateInput(false)]
        public void Content_Add() 
        {
            M_Node nodeMod = nodeBll.SelReturnModel(NodeID);
            M_CommonData CData = new M_CommonData();
            if (Mid > 0)
            {
                CData = conBll.GetCommonData(Mid);
                if (!CData.Inputer.Equals(mu.UserName)) { function.WriteErrMsg("你无权修改该内容"); }
            }
            else
            {
                CData.NodeID = NodeID;
                CData.ModelID = ModelID;
                CData.TableName = modBll.GetModelById(CData.ModelID).TableName;
            }
            DataTable dt = fieldBll.SelByModelID(CData.ModelID, false);
            Call commonCall = new Call();
            DataTable table = commonCall.GetDTFromMVC(dt, Request);
            CData.Title = Request.Form["title"];
            CData.Subtitle = Request["Subtitle"];
            CData.PYtitle = Request["PYtitle"];
            CData.TagKey = Request.Form["tagkey"];
            CData.Status = nodeMod.SiteContentAudit;
            string parentTree = "";
            CData.FirstNodeID = nodeBll.SelFirstNodeID(CData.NodeID, ref parentTree);
            CData.ParentTree = parentTree;
            CData.TitleStyle = Request.Form["TitleStyle"];
            //CData.TopImg = Request.Form["selectpic"];//首页图片
            if (Mid > 0)//修改内容
            {
                conBll.UpdateContent(table, CData);
            }
            else
            {
                CData.DefaultSkins = 0;
                CData.EliteLevel = 0;
                CData.UpDateType = 2;
                CData.InfoID = "";
                CData.RelatedIDS = "";
                CData.Inputer = mu.UserName;
                CData.GeneralID = conBll.AddContent(table, CData);//插入信息给两个表，主表和从表:CData-主表的模型，table-从表
            }
            if (Mid <= 0)//添加时增加积分
            {
                //积分
                if (SiteConfig.UserConfig.InfoRule > 0)
                {
                    buser.ChangeVirtualMoney(mu.UserID, new M_UserExpHis()
                    {
                        UserID = mu.UserID,
                        detail = "添加内容:" + CData.Title + "增加积分",
                        score = SiteConfig.UserConfig.InfoRule,
                        ScoreType = (int)M_UserExpHis.SType.Point
                    });
                }
            }
            function.WriteSuccessMsg("操作成功!", "MyContent?NodeID=" + CData.NodeID);
        }
        public int Content_Del(string id)
        {
            conBll.UpdateStatus(id, (int)ZLEnum.ConStatus.Recycle, mu.UserName);
            return Success;
        }
        public int Content_RealDel(string id)
        {
            conBll.DelContent(id, mu.UserName);
            return Success;
        }
        public int Content_Recover(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                conBll.UpdateStatus(id, 0);
            }
            return Success;
        }
        public ActionResult MyFavori()
        {
            PageSetting setting = favBll.SelPage(CPage, PSize, mu.UserID, DataConvert.CLng(Request["type"], -100));
            return View(setting);
        }
        public PartialViewResult Favour_Data()
        {
            PageSetting setting = favBll.SelPage(CPage, PSize, mu.UserID, DataConvert.CLng(Request["type"], -100));
            return PartialView("MyFavori_List", setting);
        }
        public int Favour_Del(string id)
        {
            favBll.DelByIDS(id, mu.UserID);
            return Success;
        }
        private PageSetting SelContent(int cpage, int psize, int nodeid, string status, string inputer, string skey)
        {
            string where = "";
            if (status.Equals("-100")) { where = "1=1 "; }//全部|除回收站|指定状态
            else if (string.IsNullOrEmpty(status)) { where = "Status!=" + (int)ZLEnum.ConStatus.Recycle + " "; }
            else { SafeSC.CheckIDSEx(status); where = "Status IN (" + status + ") "; }
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(skey)) { where += " AND Title LIKE @skey"; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            if (!string.IsNullOrEmpty(inputer)) { where += " AND Inputer=@inputer"; sp.Add(new SqlParameter("inputer", inputer)); }
            if (nodeid > 0) { where += " AND NodeID=" + nodeid; }
            PageSetting setting = PageSetting.Single(cpage, psize, "ZL_CommonModel", "GeneralID", where, "", sp); 
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}
