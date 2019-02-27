namespace ZoomLa.WebSite.User.Content
{
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
    using ZoomLa.Components;

    public partial class AddContent : System.Web.UI.Page
    {
        protected B_Node bnode = new B_Node();
        protected B_Model bmode = new B_Model();
        protected B_ContentField bshow = new B_ContentField();
        protected B_ModelField bfield = new B_ModelField();
        protected B_Content bll = new B_Content();
        protected B_Templata tll = new B_Templata();
        public M_UserInfo UserInfo;
        private B_User buser = new B_User();
        protected B_Sensitivity sll = new B_Sensitivity();
        protected int NodeID;
        protected int ModelID;
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged();
            if (!IsPostBack)
            {
                this.UserInfo = buser.GetLogin();
                DataTable cmdinfo = bfield.SelectTableName("ZL_pagereg", "TableName like 'ZL_Reg_%' and UserName='" + UserInfo.UserName + "'");
                if (cmdinfo.Rows.Count == 0)
                {
                    Response.Redirect("PageInfo.aspx");
                }

                if (string.IsNullOrEmpty(base.Request.QueryString["ModelID"]))
                {
                    function.WriteErrMsg("没有指定要添加内容的模型ID!");
                }
                else
                {

                    this.NodeID = DataConverter.CLng(base.Request.QueryString["NodeID"]);
                    this.ModelID = DataConverter.CLng(base.Request.QueryString["ModelID"]);

                    this.lblNodeName.Text = "企业黄页";
                    lblAddContent.Text = "[<a href=\"AddContent.aspx?ModelID=" + ModelID.ToString() + "\">" + bmode.GetModelById(this.ModelID).ModelName.ToString() + "</a>]";
                }
                string nodename = "";
                if (this.NodeID > 0) { nodename = tll.Getbyid(this.NodeID).TemplateName.ToString(); }
                this.lblNodeName.Text = SiteConfig.SiteInfo.SiteName;
                this.Label1.Text = "向" + nodename + "节点添加" + bmode.GetModelById(this.ModelID).ModelName.ToString() + "";
                this.lblAddContent.Text = nodename;
                this.Label2.Text = nodename;
                this.HdnModel.Value = this.ModelID.ToString();
                this.HdnNode.Value = this.NodeID.ToString();
                ModelHtml.Text = bfield.InputallHtml(ModelID, NodeID, new ModelConfig()
                {
                    Source = ModelConfig.SType.Admin
                });

            }
        }
        //ModelID实为模板栏目ID
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            this.ModelID = DataConverter.CLng(this.HdnModel.Value);
            this.NodeID = DataConverter.CLng(this.HdnNode.Value);
            this.UserInfo = buser.GetLogin();
            M_Templata tempMod = tll.Getbyid(this.NodeID);
            Call commonCall = new Call();
            DataTable dt = this.bfield.GetModelFieldList(this.ModelID);
            DataTable table = commonCall.GetDTFromPage(dt, Page, ViewState);
            M_CommonData CData = new M_CommonData();
            CData.NodeID = this.NodeID;
            CData.ModelID = this.ModelID;
            CData.TableName = this.bmode.GetModelById(this.ModelID).TableName;
            CData.Title = this.txtTitle.Text;
            CData.Inputer = this.UserInfo.UserName;
            CData.EliteLevel = 0;
            CData.Status = SiteConfig.YPage.IsAudit ? 99: 0;
            CData.InfoID = "";
            CData.Template = "";
            CData.TagKey = "";
            CData.SpecialID = "";
            CData.PdfLink = "";
            CData.Titlecolor = "";
            CData.DefaultSkins = 0;
            CData.CreateTime = DateTime.Now;
            CData.UpDateTime = DateTime.Now;
            CData.FirstNodeID = GetFriestNode(this.NodeID);
            int newID = bll.AddContent(table, CData);
            function.WriteSuccessMsg("添加成功", "MyContent.aspx?ModelID=" + this.NodeID);
        }
        // 获得第一级节点ID
        public int GetFriestNode(int NodeID)
        {
            GetNo(NodeID);
            return FNodeID;
        }
        protected int FNodeID = 0;
        public void GetNo(int NodeID)
        {
            M_Node nodeinfo = bnode.GetNodeXML(NodeID);
            int ParentID = nodeinfo.ParentID;
            if (DataConverter.CLng(nodeinfo.ParentID) > 0)
            {
                GetNo(nodeinfo.ParentID);
            }
            else
            {
                FNodeID = nodeinfo.NodeID;
            }
        }
    }
}