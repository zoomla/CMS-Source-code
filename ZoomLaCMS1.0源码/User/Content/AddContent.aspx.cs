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

    public partial class AddContent : System.Web.UI.Page
    {
        protected B_Node bnode = new B_Node();
        protected B_Model bmode = new B_Model();
        protected B_ContentField bshow = new B_ContentField();
        protected B_ModelField bfield = new B_ModelField();
        protected B_Content bll = new B_Content();
        protected B_SpecInfo bspec = new B_SpecInfo();
        protected int NodeID;
        protected int ModelID;
        public M_UserInfo UserInfo;
        private B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                
                buser.CheckIsLogin();
                string uname = HttpContext.Current.Request.Cookies["UserState"]["LoginName"];
                this.UserInfo = buser.GetUserByName(uname);
                if (string.IsNullOrEmpty(base.Request.QueryString["ModelID"]))
                {
                    function.WriteErrMsg("没有指定要添加内容的模型ID!");
                }
                else
                {
                    this.ModelID = DataConverter.CLng(base.Request.QueryString["ModelID"]);
                }
                if (string.IsNullOrEmpty(base.Request.QueryString["NodeID"]))
                {
                    function.WriteErrMsg("没有指定要添加内容的栏目节点ID!");
                }
                else
                {
                    this.NodeID = DataConverter.CLng(base.Request.QueryString["NodeID"]);
                }

                this.Label1.Text = "向" + this.bnode.GetNode(this.NodeID).NodeName + "节点添加" + this.bmode.GetModelById(this.ModelID).ModelName;
                this.Label2.Text = this.bnode.GetNode(this.NodeID).NodeName;
                this.HdnModel.Value = this.ModelID.ToString();
                this.HdnNode.Value = this.NodeID.ToString();
                this.ModelHtml.Text = this.bfield.GetInputHtmlUser(this.ModelID, this.NodeID);
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                this.ModelID = DataConverter.CLng(this.HdnModel.Value);
                this.NodeID = DataConverter.CLng(this.HdnNode.Value);
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
                            DataRow row2 = table.NewRow();
                            row2[0] = sizefield;
                            row2[1] = "FileSize";
                            row2[2] = this.Page.Request.Form["txt_" + sizefield];
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
                            if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + sizefield]))
                            {
                                function.WriteErrMsg(dr["FieldAlias"].ToString() + "的缩略图不能为空!");
                            }
                            DataRow row1 = table.NewRow();
                            row1[0] = sizefield;
                            row1[1] = "ThumbField";
                            row1[2] = this.Page.Request.Form["txt_" + sizefield];
                            table.Rows.Add(row1);
                        }
                    }
                    DataRow row = table.NewRow();
                    row[0] = dr["FieldName"].ToString();
                    row[1] = dr["FieldType"].ToString();
                    row[2] = this.Page.Request.Form["txt_" + dr["FieldName"].ToString()];
                    table.Rows.Add(row);
                }
                string uname = HttpContext.Current.Request.Cookies["UserState"]["LoginName"];
                this.UserInfo = buser.GetUserByName(uname);

                M_CommonData CData = new M_CommonData();
                CData.NodeID = this.NodeID;
                CData.ModelID = this.ModelID;
                CData.TableName = this.bmode.GetModelById(this.ModelID).TableName;
                CData.Title = this.txtTitle.Text;
                CData.Inputer = this.UserInfo.UserName;
                CData.EliteLevel = 0;
                CData.Status = 0;
                CData.InfoID = "";
                CData.SpecialID = "";
                int newID = this.bll.AddContent(table, CData);
                //添加到专题
                //string specialid = this.HdnSpec.Value;
                //if (!string.IsNullOrEmpty(specialid))
                //{
                //    string[] arr = specialid.Split(new char[] { ',' });
                //    M_SpecInfo info = new M_SpecInfo();
                //    info.InfoID = newID;
                //    for (int i = 0; i < arr.Length; i++)
                //    {
                //        info.SpecialID = DataConverter.CLng(arr[i]);
                //        this.bspec.Add(info);
                //    }
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