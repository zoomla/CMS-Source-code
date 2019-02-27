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

    public partial class EditContent : System.Web.UI.Page
    {
        protected B_Node bnode = new B_Node();
        protected B_Model bmode = new B_Model();
        protected B_ContentField bshow = new B_ContentField();
        protected B_ModelField bfield = new B_ModelField();
        protected B_Content bll = new B_Content();
        
        protected int NodeID;
        protected int ModelID;
        protected int GeneralID;
        public M_UserInfo UserInfo;
        private B_User buser = new B_User();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                buser.CheckIsLogin();
                string uname = HttpContext.Current.Request.Cookies["UserState"]["LoginName"];
                this.UserInfo = buser.GetUserByName(uname);

                if (string.IsNullOrEmpty(base.Request.QueryString["GeneralID"]))
                {
                    function.WriteErrMsg("没有指定要修改的内容ID!");
                }
                else
                {
                    this.GeneralID = DataConverter.CLng(base.Request.QueryString["GeneralID"]);
                }
                M_CommonData Cdata = this.bll.GetCommonData(this.GeneralID);
                if (uname != Cdata.Inputer)
                {
                    function.WriteErrMsg("不能编辑不属于自己输入的内容!");
                }
                this.NodeID = Cdata.NodeID;
                this.ModelID = Cdata.ModelID;
                this.lblNodeName.Text = this.bnode.GetNode(this.NodeID).NodeName;
                string ModelName = this.bmode.GetModelById(this.ModelID).ModelName;
                this.Label1.Text = "修改" + ModelName;
                this.lblAddContent.Text = ModelName;
                this.Label2.Text = this.lblNodeName.Text;
                this.txtTitle.Text = Cdata.Title;
                
                this.HdnItem.Value = this.GeneralID.ToString();
                
                DataTable dtContent = this.bll.GetContent(this.GeneralID);
                this.ModelHtml.Text = this.bfield.GetUpdateHtmlUser(this.ModelID, NodeID, dtContent);
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
                CData.Title = this.txtTitle.Text;
                //CData.EliteLevel = this.ChkAudit.Checked ? 1 : 0;
                CData.InfoID = "";
                CData.SpecialID = "";

                DataTable dt = this.bfield.GetModelFieldList(this.ModelID).Tables[0];

                DataTable table = new DataTable();
                table.Columns.Add(new DataColumn("FieldName", typeof(string)));
                table.Columns.Add(new DataColumn("FieldType", typeof(string)));
                table.Columns.Add(new DataColumn("FieldValue", typeof(string)));

                foreach (DataRow dr in dt.Rows)
                {

                    if (DataConverter.CBool(dr["IsNotNull"].ToString()))
                    {
                        if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + dr["FieldName"].ToString()]))
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
                            DataRow row1 = table.NewRow();
                            row1[0] = sizefield;
                            row1[1] = "FileSize";
                            row1[2] = this.Page.Request.Form["txt_" + sizefield];
                            table.Rows.Add(row1);
                        }
                    }
                    if (dr["FieldType"].ToString() == "MultiPicType")
                    {
                        string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
                        bool chkthumb = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
                        string ThumbField = Sett[1].Split(new char[] { '=' })[1];
                        if (chkthumb && ThumbField != "")
                        {
                            if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + ThumbField]))
                            {
                                function.WriteErrMsg(dr["FieldAlias"].ToString() + "的缩略图不能为空!");
                            }
                            DataRow row2 = table.NewRow();
                            row2[0] = ThumbField;
                            row2[1] = "ThumbField";
                            row2[2] = this.Page.Request.Form["txt_" + ThumbField];
                            table.Rows.Add(row2);
                        }
                    }
                    DataRow row = table.NewRow();
                    row[0] = dr["FieldName"].ToString();
                    row[1] = dr["FieldType"].ToString();
                    row[2] = this.Page.Request.Form["txt_" + dr["FieldName"].ToString()];
                    table.Rows.Add(row);
                }
                this.bll.UpdateContent(table, CData);
                //string SpecID = this.HdnSpec.Value;
                //if (!string.IsNullOrEmpty(SpecID))
                //{
                //    this.bspec.UpdateSpec(SpecID, this.GeneralID);
                //}
                Response.Redirect("MyContent.aspx?NodeID=" + this.NodeID);
            }
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyContent.aspx?NodeID=" + this.NodeID);
        }
    }
}