using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Content;
using ZoomLa.BLL.CreateJS;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Content;
using ZoomLa.PdoApi.CopyRight;
using ZoomLa.SQLDAL;
/*
 * 1,目标模型必须有content字段
 * 2,默认为资迅中心的内容模型
 * 3,是否增加批量选择,然后添加版权印功能
 */

namespace ZoomLaCMS.Manage.Copyright
{
    public partial class AddWorks : System.Web.UI.Page
    {
        public int GeneralID { get { return DataConvert.CLng(Request.QueryString["Gid"]); } }
        private int NodeID { get { return DataConvert.CLng(ViewState["NodeID"]); } set { ViewState["NodeID"] = value; } }
        private int ModelID { get { return DataConvert.CLng(ViewState["ModelID"]); } set { ViewState["ModelID"] = value; } }
        B_Node nodeBll = new B_Node();
        B_Model modelBll = new B_Model();
        B_ModelField fieldBll = new B_ModelField();
        B_Content conBll = new B_Content();
        B_CodeModel artBll = null;
        B_Content_CR crBll = new B_Content_CR();
        C_CopyRight copyBll = new C_CopyRight();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                C_CopyRight.CheckLogin();
                CheckHasRule();
                if (GeneralID > 0)
                {
                    M_CommonData conMod = conBll.SelReturnModel(GeneralID);
                    NodeID = conMod.NodeID;
                    ModelID = conMod.ModelID;
                    Title_T.Text = conMod.Title;
                    DataTable conDT = DBCenter.Sel(conMod.TableName, "ID=" + conMod.ItemID);
                    if (conDT.Rows.Count > 0 && conDT.Columns.Contains("content")) { content_t.Text = conDT.Rows[0]["content"].ToString(); }
                }
                else { NodeID = 1; ModelID = 2; }
                M_Node nodeMod = nodeBll.SelReturnModel(NodeID);
                M_ModelInfo model = modelBll.SelReturnModel(ModelID);
                string bread = "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>" + Resources.L.工作台 + "</a></li><li><a href='ContentManage.aspx'>" + Resources.L.内容管理 + "</a></li><li><a href='ContentManage.aspx?NodeID=" + nodeMod.NodeID + "'>" + nodeMod.NodeName + "</a></li>";
                bread += "<li class='active'>[向本栏目添加" + model.ItemName + "]</li>";
                Call.SetBreadCrumb(Master, bread);
            }
        }
        protected void Add_Btn_Click(object sender, EventArgs e)
        {
            M_CommonData CData = new M_CommonData();
            M_AdminInfo adminMod = B_Admin.GetLogin();
            M_ModelInfo model = modelBll.SelReturnModel(ModelID);
            artBll = new B_CodeModel(model.TableName);
            if (GeneralID > 0)
            {
                CData = conBll.SelReturnModel(GeneralID);
            }
            else
            {
                CData.NodeID = NodeID;
                CData.ModelID = ModelID;
                CData.TableName = model.TableName;
                CData.Inputer = adminMod.AdminName;
                string parentTree = "";
                CData.FirstNodeID = nodeBll.SelFirstNodeID(NodeID, ref parentTree);
                CData.ParentTree = parentTree;
            }
            //------------------------
            CData.Title = Title_T.Text.Trim();
            switch ((sender as Button).CommandArgument)
            {
                case "add":
                    CData.Status = (int)ZLEnum.ConStatus.Audited;
                    break;
                case "draft":
                    CData.Status = (int)ZLEnum.ConStatus.Draft;
                    break;
                default:
                    break;
            }
            if (GeneralID > 0)
            {
                DataRow dr = artBll.SelByID(CData.ItemID);
                dr["Content"] = content_t.Text;
                artBll.UpdateByID(dr, "ID");
                conBll.UpdateByID(CData);
            }
            else
            {
                DataRow dr = artBll.NewModel();
                dr["Content"] = content_t.Text;
                CData.ItemID = artBll.Insert(dr);
                CData.GeneralID = conBll.insert(CData);
            }
            //----------------同步版权印
            double repPrice = DataConverter.CDouble(RepPrice_T.Text.Trim());
            double matPrice = DataConverter.CDouble(MatPrice_T.Text.Trim());
            string content = StringHelper.StripHtml(content_t.Text);
            M_Content_CR crMod = crBll.CreateFromContent(CData, content, repPrice, matPrice);

            string result = copyBll.Create(crMod);
            JObject obj = JsonConvert.DeserializeObject<JObject>(result);
            crMod.Status = DataConverter.CLng(obj["value"]);
            crMod.WorksID = obj["data"].ToString();
            crBll.Insert(crMod);
            if (crMod.Status == 1) { function.WriteSuccessMsg("操作成功,文章编号为:" + crMod.WorksID, "WorksList.aspx"); }
            else { function.WriteErrMsg("操作失败:" + obj["msg"]); }
        }
        public void CheckHasRule()
        {
            string result = copyBll.GetRule(SiteConfig.SiteInfo.SiteName);
            try
            {
                JObject obj = JsonConvert.DeserializeObject<JObject>(result);
                if (obj["data"] == null || obj["data"]["rules"] == null || obj["data"]["rules"].ToString().Equals("[]"))
                {
                    copyBll.SetDefPriceRule();
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message + "::" + result); }
        }
    }
}