namespace ZoomLa.WebSite.Manage.Content
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

    public partial class BatchNode : System.Web.UI.Page
    {
        private B_Node bll = new B_Node();
        private B_Model bllmodel = new B_Model();

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
                DataTable dt = this.bllmodel.GetList();
                this.Repeater1.DataSource = dt;
                this.Repeater1.DataBind();

                this.LstNodes.DataSource = this.bll.GetNodeListContain(0);
                this.LstNodes.DataTextField = "NodeName";
                this.LstNodes.DataValueField = "NodeID";
                this.LstNodes.DataBind();
            }
        }
        protected void EBtnBacthSet_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.LstNodes.Items.Count; i++)
            {
                if (this.LstNodes.Items[i].Selected)
                {
                    int nodeid = DataConverter.CLng(this.LstNodes.Items[i].Value);
                    M_Node node = this.bll.GetNode(nodeid);
                    if (this.ChkOpenType.Checked)
                    {
                        node.OpenNew = DataConverter.CBool(this.RBLOpenType.SelectedValue);
                    }
                    if (this.ChkItemOpen.Checked)
                    {
                        node.ItemOpenType = DataConverter.CBool(this.RBLItemOpenType.SelectedValue);
                    }
                    if (this.ChkPurview.Checked)
                    {
                        node.PurviewType = DataConverter.CBool(this.RBLPurviewType.SelectedValue);
                    }
                    if (this.ChkComment.Checked)
                    {
                        node.CommentType = DataConverter.CBool(this.RBLCommentType.SelectedValue);
                    }
                    if (this.ChkHits.Checked)
                    {
                        node.HitsOfHot = DataConverter.CLng(this.TxtHitsOfHot.Text);
                    }
                    if (this.ChkTemp.Checked)
                    {
                        node.ListTemplateFile = this.TxtTemplate.Text;
                    }
                    if (this.ChkITemp.Checked)
                    {
                        node.IndexTemplate = this.TxtIndexTemplate.Text;
                    }
                    if (this.ChkModelID.Checked)
                    {
                        string modellist = this.Page.Request.Form["ChkModel"];                        
                        node.ContentModel = modellist;
                        string[] ModelArr = modellist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        this.bll.DelModelTemplate(node.NodeID, modellist);
                        for (int j = 0; j < ModelArr.Length; j++)
                        {
                            if (!string.IsNullOrEmpty(this.Page.Request.Form["TxtModelTemplate_" + ModelArr[j]].Trim()))
                            {
                                //将模型模板设定写入数据库
                                string temp = this.Page.Request.Form["TxtModelTemplate_" + ModelArr[j]].Trim();
                                if (this.bll.IsExistTemplate(node.NodeID, DataConverter.CLng(ModelArr[j])))
                                {
                                    this.bll.UpdateModelTemplate(node.NodeID, DataConverter.CLng(ModelArr[j]), temp);
                                }
                                else
                                {
                                    this.bll.AddModelTemplate(node.NodeID, DataConverter.CLng(ModelArr[j]), temp);
                                }
                            }
                        }
                    }
                    if (this.ChkListEx.Checked)
                    {
                        node.ListPageHtmlEx = DataConverter.CLng(this.RBLListEx.SelectedValue);
                    }
                    if (this.ChkContentEx.Checked)
                    {
                        node.ContentFileEx = DataConverter.CLng(this.RBLContentEx.SelectedValue);                        
                    }
                    if (this.ChkContentRule.Checked)
                    {
                        node.ContentPageHtmlRule = DataConverter.CLng(this.DDLContentRule.SelectedValue);
                    }
                    this.bll.UpdateNode(node);
                }
            }
            function.WriteSuccessMsg("批量设置成功", "../Content/ContentManage.aspx");
        }
        protected void BtnCancel_Click(object sender, EventArgs e)
        {

        }
}

}