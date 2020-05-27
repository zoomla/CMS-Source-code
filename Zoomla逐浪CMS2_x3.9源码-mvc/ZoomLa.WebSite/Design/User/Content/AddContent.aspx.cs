using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Design.User.Content
{
    public partial class AddContent : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Content contentBll = new B_Content();
        B_Content_WordChain wordBll = new B_Content_WordChain();
        B_Model modelBll = new B_Model();
        B_ModelField mfieldBll = new B_ModelField();
        B_Node nodeBll = new B_Node();
        B_Spec spbll = new B_Spec();
        B_KeyWord keyBll = new B_KeyWord();
        public int SiteID { get { return DataConvert.CLng(Request.QueryString["SiteID"]); } }
        public int NodeID
        {
            get { return DataConvert.CLng(ViewState["NodeID"]); }
            set { ViewState["NodeID"] = value; }
        }
        public int ModelID
        {
            get { return DataConvert.CLng(ViewState["ModelID"]); }
            set { ViewState["ModelID"] = value; }
        }
        public int GeneralID { get { return DataConvert.CLng(Request.QueryString["GeneralID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_CommonData commdata = new M_CommonData();
                if (GeneralID > 0)
                {
                    commdata = contentBll.GetCommonData(this.GeneralID);
                    NodeID = commdata.NodeID;
                    ModelID = commdata.ModelID;
                }
                else
                {
                    NodeID = DataConvert.CLng(Request.QueryString["NodeID"]);
                    ModelID = DataConvert.CLng(Request.QueryString["ModelID"]);
                }
                if ((ModelID < 1 && NodeID < 1) && GeneralID < 1) { function.WriteErrMsg("参数错误"); }
                if (!string.IsNullOrEmpty(Request.QueryString["Source"])) { function.Script(this, "SetContent();"); }
                //-------------------------------------------
                DataTable ds = mfieldBll.SelByModelID(ModelID, true);
                bt_txt.Text = B_Content.GetFieldAlias("Title", ds);
                gjz_txt.Text = B_Content.GetFieldAlias("Tagkey", ds);
                Label4.Text = B_Content.GetFieldAlias("Subtitle", ds);
                py.Text = B_Content.GetFieldAlias("PYtitle", ds);
                //-------------------------------------------
                M_ModelInfo model = modelBll.GetModelById(ModelID);
                string optionstr = GeneralID > 0 ? "修改" : "添加";
                Label1.Text = optionstr + model.ItemName;
                EBtnSubmit.Text = optionstr + model.ItemName;
                //-------------------------------------------
                if (spbll.GetSpecList().Rows.Count > 0)
                { SpecInfo_Li.Text = "<button type='button' class='btn btn-info btn-sm' onclick='ShowSpDiag()'>添加至专题</button>"; }
                else
                { SpecInfo_Li.Text = "<div style='margin:5px;' class='btn btn-default disabled'><span class='fa fa-info-circle'></span> 尚未定义专题</div>"; }
                //-------------------------------------------
                M_Node nodeMod = nodeBll.SelReturnModel(NodeID);
                CreateHTML.Visible = nodeMod.ListPageHtmlEx < 3;
                Tips_L.Text = "向" + nodeMod.NodeName + "节点" + optionstr + model.ItemName;
                Node_L.Text = nodeMod.NodeName;
                if (GeneralID > 0)
                {
                    MyBind(commdata);
                }
                else
                {
                    ModelHtml.Text = mfieldBll.InputallHtml(ModelID, NodeID, new ModelConfig()
                    {
                        Source = ModelConfig.SType.UserContent
                    });
                }
                if (nodeMod.Contribute != 1) { function.Script(this, "ShowSys();"); }
            }
        }
        public void MyBind(M_CommonData Cdata)
        {
            M_UserInfo mu = buser.GetLogin();
            if (mu.UserName != Cdata.Inputer) { function.WriteErrMsg("不能编辑不属于自己输入的内容!"); }
            txtTitle.Text = Cdata.Title;
            Keywords.Text = Cdata.TagKey;
            PYtitle.Text = Cdata.PYtitle;
            DataTable dtContent = contentBll.GetContent(this.GeneralID);
            ModelHtml.Text = mfieldBll.InputallHtml(Cdata.ModelID, Cdata.NodeID, new ModelConfig()
            {
                Source = ModelConfig.SType.UserContent,
                ValueDT = dtContent
            });

        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)//添加文章
        {
            M_UserInfo mu = buser.GetLogin();
            DataTable dt = mfieldBll.SelByModelID(ModelID, false);
            M_Node nodeMod = nodeBll.SelReturnModel(NodeID);
            IList<string> content = new List<string>();
            M_CommonData CData = new M_CommonData();
            Call commonCall = new Call();
            DataTable table = commonCall.GetDTFromPage(dt, Page, ViewState, content);
            if (GeneralID > 0) { CData = contentBll.GetCommonData(GeneralID); }
            else
            {
                CData.NodeID = NodeID;
                CData.ModelID = ModelID;
                CData.TableName = modelBll.GetModelById(CData.ModelID).TableName;
            }
            CData.Title = txtTitle.Text.Trim();
            CData.EliteLevel = 0;
            CData.InfoID = "";
            CData.UpDateType = 2;
            CData.TagKey = Request.Form["tabinput"];
            CData.Status = (int)ZLEnum.ConStatus.Audited;
            CData.DefaultSkins = 0;
            string parentTree = "";
            CData.FirstNodeID = nodeBll.SelFirstNodeID(NodeID, ref parentTree);
            CData.ParentTree = parentTree;
            CData.TitleStyle = ThreadStyle.Value;
            CData.TopImg = Request.Form["selectpic"];//首页图片
            CData.Subtitle = Subtitle.Text;
            CData.PYtitle = PYtitle.Text;
            CData.RelatedIDS = RelatedIDS_Hid.Value;
            CData.IsComm = 1;
            if (GeneralID > 0)//修改内容
            {
                contentBll.UpdateContent(table, CData);
            }
            else
            {
                CData.Inputer = mu.UserName;
                CData.GeneralID = contentBll.AddContent(table, CData);//插入信息给两个表，主表和从表:CData-主表的模型，table-从表
            }
            function.WriteSuccessMsg("操作成功!", "List.aspx?NodeID=" + NodeID + "&SiteID=" + SiteID);
        }
        //草稿
        protected void DraftBtn_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            DataTable dt = mfieldBll.GetModelFieldList(ModelID);
            Call commonCall = new Call();
            DataTable table = commonCall.GetDTFromPage(dt, Page, ViewState);
            M_CommonData CData = new M_CommonData();
            if (GeneralID > 0) { CData = contentBll.GetCommonData(GeneralID); }
            else
            {
                CData.NodeID = NodeID;
                CData.ModelID = ModelID;
                CData.TableName = modelBll.GetModelById(ModelID).TableName;
            }
            CData.Title = txtTitle.Text.Trim();
            CData.Status = (int)ZLEnum.ConStatus.Draft;
            CData.Inputer = mu.UserName;
            CData.InfoID = "";
            CData.PdfLink = "";
            CData.TagKey = Request.Form["tabinput"];
            CData.Subtitle = Subtitle.Text;
            CData.PYtitle = PYtitle.Text;
            CData.TitleStyle = Request.Form["ThreadStyle"];
            string parentTree = "";
            CData.FirstNodeID = nodeBll.SelFirstNodeID(NodeID, ref parentTree);
            CData.ParentTree = parentTree;
            if (GeneralID > 0) { contentBll.UpdateContent(table, CData); }
            else { contentBll.AddContent(table, CData); }
            Response.Redirect("List.aspx?NodeID=" + CData.NodeID + "&SiteID=" + SiteID);
        }
        //显示前台浏览按钮
        public string GetOpenView()
        {
            string outstr = "", strurl = string.Empty;
            strurl = "Class_" + NodeID + "/Default.aspx";
            outstr = " <a href='/" + strurl + "'  target='_blank' title='前台浏览'><span class='fa fa-eye'></span></a>";
            return outstr;
        }
    }
}