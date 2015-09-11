namespace ZommLa.WebSite.Manage.Content
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
    using ZoomLa.Web;
    using ZoomLa.BLL;
    using ZoomLa.DALFactory;
    using ZoomLa.Common;
    using ZoomLa.Model;
    

    public partial class EditNode : System.Web.UI.Page
    {
        private B_Node bll = new B_Node();
        private B_Model bllmodel = new B_Model();
        private string ModelArr = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("NodeEdit"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                string mNodeID = base.Request.QueryString["NodeID"];
                if (string.IsNullOrEmpty(mNodeID))
                {
                    function.WriteErrMsg("没有指定要编辑的节点ID");
                }
                //DataTable dt = this.bllmodel.GetList();
                //this.ChkModelList.DataSource = dt;
                //this.ChkModelList.DataTextField = "ModelName";
                //this.ChkModelList.DataValueField = "ModelID";
                //this.ChkModelList.DataBind();
                M_Node node = this.bll.GetNode(DataConverter.CLng(mNodeID));
                if (node.IsNull)
                {
                    function.WriteErrMsg("指定要编辑的节点不存在");
                }
                else
                {
                    if (node.ParentID == 0)
                        this.LblNodeName.Text = "根节点";
                    else
                    {
                        M_Node node1 = this.bll.GetNode(node.ParentID);
                        this.LblNodeName.Text = node1.NodeName;
                    }
                    this.HdnNodeID.Value = mNodeID;
                    this.HdnDepth.Value = node.Depth.ToString();
                    this.HdnParentId.Value = node.ParentID.ToString();
                    this.HdnOrderID.Value = node.OrderID.ToString();
                    this.HdnChild.Value = node.Child.ToString();
                    this.TxtNodeName.Text = node.NodeName;
                    this.TxtNodeDir.Text = node.NodeDir;
                    this.TxtNodePicUrl.Text = node.NodePic;
                    this.TxtTips.Text = node.Tips;
                    this.TxtMetaKeywords.Text = node.Meta_Keywords;
                    this.TxtMetaDescription.Text = node.Meta_Description;
                    this.RBLOpenType.SelectedValue = DataConverter.CLng(node.OpenNew).ToString();
                    this.RBLPurviewType.SelectedValue = DataConverter.CLng(node.PurviewType).ToString();
                    this.RBLCommentType.SelectedValue = DataConverter.CLng(node.CommentType).ToString();
                    this.TxtHitsOfHot.Text = node.HitsOfHot.ToString();
                    this.TxtTemplate.Text = node.ListTemplateFile;
                    this.TxtIndexTemplate.Text = node.IndexTemplate;
                    this.RBLListEx.SelectedValue = node.ListPageHtmlEx.ToString();
                    this.RBLContentEx.SelectedValue = node.ContentFileEx.ToString();
                    this.DDLContentRule.SelectedValue = node.ContentPageHtmlRule.ToString();
                    this.ModelArr = node.ContentModel;
                    DataTable dt = this.bllmodel.GetList();
                    this.Repeater1.DataSource = dt;
                    this.Repeater1.DataBind();                    
                }
            }
        }
        public string GetChk(string mid)
        {
            string result = "";
            string[] arr = this.ModelArr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (IsInModel(mid, arr))
            {
                result = "<input type=\"checkbox\" id=\"ChkModel\" name=\"ChkModel\" value=\"" + mid + "\" checked />";
            }
            else
            {
                result = "<input type=\"checkbox\" id=\"ChkModel\" name=\"ChkModel\" value=\"" + mid + "\" />";
            }
            return result;
        }
        public string GetTemplate(string mid)
        {
            string result = "";
            string[] arr = this.ModelArr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (IsInModel(mid, arr))
            {
                result = this.bll.GetModelTemplate(DataConverter.CLng(this.HdnNodeID.Value),DataConverter.CLng(mid));
                if(string.IsNullOrEmpty(result))
                    result = this.bllmodel.GetModelById(DataConverter.CLng(mid)).ContentModule;
            }
            else
            {
                result = this.bllmodel.GetModelById(DataConverter.CLng(mid)).ContentModule;
            }
            return result;
        }
        public bool IsInModel(string modelid, string[] array)
        {
            bool flag = false;
            for (int i = 0; i < array.Length; i++)
            {
                if (modelid == array[i])
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                M_Node node = new M_Node();
                node.NodeID = DataConverter.CLng(this.HdnNodeID.Value);
                node.NodeName = this.TxtNodeName.Text;
                node.NodeType = 1;
                node.NodePic = this.TxtNodePicUrl.Text;
                node.NodeDir = this.TxtNodeDir.Text;
                node.NodeUrl = "";
                node.ParentID = DataConverter.CLng(this.HdnParentId.Value);
                node.Child = DataConverter.CLng(this.HdnChild.Value);
                node.Depth = DataConverter.CLng(this.HdnDepth.Value);
                node.OrderID = DataConverter.CLng(this.HdnOrderID.Value);
                node.Tips = this.TxtTips.Text;
                node.Description = this.TxtDescription.Text;
                node.Meta_Keywords = this.TxtMetaKeywords.Text;
                node.Meta_Description = this.TxtMetaDescription.Text;
                node.OpenNew = DataConverter.CBool(this.RBLOpenType.SelectedValue);
                node.ItemOpenType = DataConverter.CBool(this.RBLItemOpenType.SelectedValue);
                node.PurviewType = DataConverter.CBool(this.RBLPurviewType.SelectedValue);
                node.CommentType = DataConverter.CBool(this.RBLCommentType.SelectedValue);
                node.HitsOfHot = DataConverter.CLng(this.TxtHitsOfHot.Text);
                node.ListTemplateFile = this.TxtTemplate.Text;
                node.IndexTemplate = this.TxtIndexTemplate.Text;
                string modellist = this.Page.Request.Form["ChkModel"];
                if (modellist == null)
                    modellist = "";
                //for (int i = 0; i < this.ChkModelList.Items.Count; i++)
                //{
                //    if (this.ChkModelList.Items[i].Selected)
                //        modellist += this.ChkModelList.Items[i].Value + ",";
                //}
                //modellist = modellist.Remove(modellist.Length - 1, 1);
                node.ContentModel = modellist;
                node.ListPageHtmlEx = DataConverter.CLng(this.RBLListEx.SelectedValue);
                node.ContentFileEx = DataConverter.CLng(this.RBLContentEx.SelectedValue);
                node.ContentPageHtmlRule = DataConverter.CLng(this.DDLContentRule.SelectedValue);
                this.bll.UpdateNode(node);
                string[] ModelArr = modellist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                this.bll.DelModelTemplate(node.NodeID, modellist);
                for (int i = 0; i < ModelArr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(this.Page.Request.Form["TxtModelTemplate_" + ModelArr[i]].Trim()))
                    {
                        //将模型模板设定写入数据库
                        string temp = this.Page.Request.Form["TxtModelTemplate_" + ModelArr[i]].Trim();
                        if (this.bll.IsExistTemplate(node.NodeID, DataConverter.CLng(ModelArr[i])))
                        {
                            this.bll.UpdateModelTemplate(node.NodeID, DataConverter.CLng(ModelArr[i]), temp);
                        }
                        else
                        {
                            this.bll.AddModelTemplate(node.NodeID, DataConverter.CLng(ModelArr[i]), temp);
                        }                        
                    }
                }
                Response.Redirect("NodeManage.aspx");
            }
        }
    }
}