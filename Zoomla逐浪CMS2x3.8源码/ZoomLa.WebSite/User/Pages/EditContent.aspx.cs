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

    public partial class EditContent : System.Web.UI.Page
    {
        protected B_Node bnode = new B_Node();
        protected B_Model bmode = new B_Model();
        protected B_ContentField bshow = new B_ContentField();
        protected B_Content bll = new B_Content();
        protected M_UserInfo UserInfo;
        protected B_User buser = new B_User();
        protected B_ModelField mll = new B_ModelField();
        protected B_Templata tll = new B_Templata();
        protected int NodeID;
        protected int ModelID;
        protected int GeneralID;
        protected B_Sensitivity sll = new B_Sensitivity();

        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged();
            if (!IsPostBack)
            {
                this.UserInfo = buser.GetLogin();
                lblNodeName.Text = "企业黄页";

                DataTable cmdinfo = mll.SelectTableName("ZL_PageReg", " TableName like 'ZL_Reg_%' and UserName='" + UserInfo.UserName + "'");
                if (cmdinfo!=null&&cmdinfo.Rows.Count == 0)
                    {
                        Response.Redirect("RegProUser.aspx");
                    }
      

                if (string.IsNullOrEmpty(base.Request.QueryString["GeneralID"]))
                {
                    function.WriteErrMsg("没有指定要修改的内容ID!");
                }
                else
                {
                    this.GeneralID = DataConverter.CLng(base.Request.QueryString["GeneralID"]);
                }


                M_CommonData Cdata = this.bll.GetCommonData(this.GeneralID);
                if (UserInfo.UserName != Cdata.Inputer)
                {
                    function.WriteErrMsg("不能编辑不属于自己输入的内容!");
                }

                this.NodeID = Cdata.NodeID;
                this.ModelID = Cdata.ModelID;
                string ModelName = this.bmode.GetModelById(this.ModelID).ModelName;
                lblAddContent.Text =  ModelName;

                this.Label1.Text = "修改" + Cdata.Title.ToString();
                this.txtTitle.Text = Cdata.Title;
                this.HdnItem.Value = this.GeneralID.ToString();
                DataTable dtContent = this.bll.GetContent(this.GeneralID);

                string nodename = "";
                if (this.NodeID > 0)
                {
                    nodename = tll.Getbyid(this.NodeID).TemplateName.ToString();
                }
                this.Label3.Text = nodename;
                ModelHtml.Text = mll.InputallHtml(ModelID, NodeID, new ModelConfig()
                {
                    ValueDT = dtContent
                });
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                this.GeneralID = DataConverter.CLng(this.HdnItem.Value);
                M_CommonData CData = this.bll.GetCommonData(this.GeneralID);
                this.NodeID = CData.NodeID;
                this.ModelID = CData.ModelID;
                this.UserInfo = buser.GetLogin();
                M_Templata tempMod = tll.Getbyid(this.NodeID);
                CData.Title = this.txtTitle.Text;
                //CData.EliteLevel = this.ChkAudit.Checked ? 1 : 0;
                CData.InfoID = "";
                CData.SpecialID = "";
                CData.UpDateTime = DateTime.Now;
                CData.FirstNodeID = GetFriestNode(CData.NodeID);
                DataTable dt = this.mll.GetModelFieldList(this.ModelID);
                Call commonCall = new Call();
                DataTable table = commonCall.GetDTFromPage(dt, Page, ViewState);
                string uname = buser.GetLogin().UserName;
                this.bll.UpdateContent(table, CData);
                int status = SiteConfig.YPage.IsAudit ? 0 : 99;
                this.bll.SetAudit(GeneralID, status);
                function.WriteSuccessMsg("修改成功","MyContent.aspx?ModelID=" + this.NodeID);
            }
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyContent.aspx?ModelID=" + this.ModelID);
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