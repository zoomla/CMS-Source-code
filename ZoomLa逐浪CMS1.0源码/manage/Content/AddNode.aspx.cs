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
    using ZoomLa.Web;
    using ZoomLa.BLL;
    using ZoomLa.DALFactory;
    using ZoomLa.Common;
    using ZoomLa.Model;

    public partial class AddNode : System.Web.UI.Page, ICallbackEventHandler
    {
        private B_Node bll = new B_Node();
        private B_Model bllmodel = new B_Model();
        protected string callBackReference;
        protected string result;

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
                string mParentID = base.Request.QueryString["ParentID"];
                int ParentID;
                if (string.IsNullOrEmpty(mParentID))
                    ParentID = 0;
                else
                    ParentID = DataConverter.CLng(mParentID);
                if (ParentID == 0)
                    this.LblNodeName.Text = "根节点";
                else
                {
                    M_Node node = this.bll.GetNode(ParentID);
                    if (node.IsNull)
                        this.LblNodeName.Text = "根节点";
                    else
                        this.LblNodeName.Text = node.NodeName;
                }
                this.HdnParentId.Value = ParentID.ToString();

                DataTable dt = this.bllmodel.GetList();
                this.Repeater1.DataSource = dt;
                this.Repeater1.DataBind();

                string ModelIDArr="";
                foreach (DataRow dr in dt.Rows)
                {
                    if (string.IsNullOrEmpty(ModelIDArr))
                    {
                        ModelIDArr = dr["ModelID"].ToString();
                    }
                    else
                    {
                        ModelIDArr += "," + dr["ModelID"].ToString();
                    }
                }
                this.HdnModeID.Value = ModelIDArr;
                //this.ChkModelList.DataSource = dt;
                //this.ChkModelList.DataTextField = "ModelName";
                //this.ChkModelList.DataValueField = "ModelID";
                //this.ChkModelList.DataBind();
                this.HdnDepth.Value = this.bll.GetDepth(ParentID).ToString();
                this.HdnOrderID.Value = this.bll.GetOrder(ParentID).ToString();
                this.callBackReference = this.Page.ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerData", "context");
            }
        }
        /// <summary>
        /// 给节点的模型数组绑定复选框
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public string GetChk(string mid)
        {
            string result = "";
            if (DataConverter.CLng(this.HdnParentId.Value) != 0)
            {
                string[] arr = this.bll.GetNode(DataConverter.CLng(this.HdnParentId.Value)).ContentModel.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (IsInModel(mid, arr))
                {
                    result = "<input type=\"checkbox\" id=\"ChkModel\" name=\"ChkModel\" value=\"" + mid + "\" checked />";
                }
                else
                {
                    result = "<input type=\"checkbox\" id=\"ChkModel\" name=\"ChkModel\" value=\"" + mid + "\" />";
                }
            }
            else
            {
                result = "<input type=\"checkbox\" id=\"ChkModel\" name=\"ChkModel\" value=\"" + mid + "\" />";
            }
            return result;
        }
        /// <summary>
        /// 绑定模型的模板
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public string GetTemplate(string mid)
        {
            string result = "";
            if (DataConverter.CLng(this.HdnParentId.Value) != 0)
            {
                string[] arr = this.bll.GetNode(DataConverter.CLng(this.HdnParentId.Value)).ContentModel.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (IsInModel(mid, arr))
                {
                    result = this.bll.GetModelTemplate(DataConverter.CLng(this.HdnParentId.Value), DataConverter.CLng(mid));
                    if (string.IsNullOrEmpty(result))
                        result = this.bllmodel.GetModelById(DataConverter.CLng(mid)).ContentModule;
                }
                else
                {
                    result = this.bllmodel.GetModelById(DataConverter.CLng(mid)).ContentModule;
                }
            }
            else
            {
                result = this.bllmodel.GetModelById(DataConverter.CLng(mid)).ContentModule;
            }
            return result;
        }
        /// <summary>
        /// 判断模型是否被选中
        /// </summary>
        /// <param name="modelid"></param>
        /// <param name="array"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 保存节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                M_Node node = new M_Node();
                node.NodeID = 0;
                node.NodeName = this.TxtNodeName.Text;
                node.NodeType = 1;
                node.NodePic = this.TxtNodePicUrl.Text;
                node.NodeDir = this.TxtNodeDir.Text;
                node.NodeUrl = "";
                node.ParentID = DataConverter.CLng(this.HdnParentId.Value);
                node.Child = 0;
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
                node.ContentModel = modellist;
                node.ListPageHtmlEx = DataConverter.CLng(this.RBLListEx.SelectedValue);
                node.ContentFileEx = DataConverter.CLng(this.RBLContentEx.SelectedValue);
                node.ContentPageHtmlRule = DataConverter.CLng(this.DDLContentRule.SelectedValue);
                int newNode=this.bll.AddNode(node);
                string[] ModelArr = modellist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //this.bll.DelTemplate(newNode);
                for (int i = 0; i < ModelArr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(this.Page.Request.Form["TxtModelTemplate_" + ModelArr[i]].Trim()))
                    {
                        //将模型模板设定写入数据库
                        string temp = this.Page.Request.Form["TxtModelTemplate_" + ModelArr[i]].Trim();
                        this.bll.AddModelTemplate(newNode, DataConverter.CLng(ModelArr[i]), temp);
                    }
                }
                if (node.ParentID > 0)
                    this.bll.SetChildAdd(node.ParentID);
                Response.Redirect("NodeManage.aspx");
            }
        }
        public string CallBackReference
        {
            get { return this.callBackReference; }
        }
        #region ICallbackEventHandler 成员

        string ICallbackEventHandler.GetCallbackResult()
        {
            return this.result;
        }

        void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
        {
            this.result= StringHelper.ChineseToPY(eventArgument);
        }

        #endregion
    }
}