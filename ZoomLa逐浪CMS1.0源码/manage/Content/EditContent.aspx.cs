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
using System.Text;
namespace ZoomLa.WebSite.Manage
{
    public partial class EditContent : System.Web.UI.Page
    {
        protected B_Node bnode = new B_Node();
        protected B_Model bmode = new B_Model();
        protected B_ShowField bshow = new B_ShowField();
        protected B_ModelField bfield = new B_ModelField();
        protected B_Content bll = new B_Content();
        protected B_SpecInfo bspec = new B_SpecInfo();
        protected int NodeID;
        protected int ModelID;
        protected int GeneralID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("ContentEdit"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
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
                this.NodeID = Cdata.NodeID;
                this.ModelID = Cdata.ModelID;
                this.lblNodeName.Text = "<a href=\"ContentManage.aspx?NodeID=" + this.NodeID + "\">" + this.bnode.GetNode(this.NodeID).NodeName + "</a>";
                this.Label1.Text = "修改" + this.bmode.GetModelById(this.ModelID).ModelName;
                this.txtTitle.Text = Cdata.Title;
                if (Cdata.EliteLevel > 0)
                    this.ChkAudit.Checked = true;
                else
                    this.ChkAudit.Checked = false;
                this.HdnItem.Value = this.GeneralID.ToString();
                this.HdnNode.Value = this.NodeID.ToString();
                string SpecId = this.bspec.GetSpec(this.GeneralID);
                this.HdnSpec.Value = SpecId;
                this.TxtTemplate.Text = Cdata.Template;
                ShowSpec(SpecId);
                DataTable dtContent = this.bll.GetContent(this.GeneralID);
                this.ModelHtml.Text = this.bfield.GetUpdateHtml(this.ModelID, NodeID, dtContent);
            }
        }
        private void ShowSpec(string SpecID)
        {
            if (SpecID == "")
            {
                this.lblSpec.InnerHtml = "<span id='SpecialSpanId0'>无专题<br /></span>";
            }
            else
            {
                string[] SpecArr = SpecID.Split(new char[] { ',' });
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < SpecArr.Length; i++)
                {
                    sb.Append("<span id='SpecialSpanId" + SpecArr[i] + "'>" + this.bspec.GetSpecName(DataConverter.CLng(SpecArr[i])));
                    sb.Append(" <input type=\"button\" class=\"button\" value=\"从此专题中移除\" onclick=\"DelSpecial(" + SpecArr[i] + ");\">");
                    sb.Append("<br /></span>");
                }
                this.lblSpec.InnerHtml = sb.ToString();
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
                CData.EliteLevel = this.ChkAudit.Checked ? 1 : 0;
                CData.InfoID = "";
                CData.SpecialID = "";
                CData.Template = this.TxtTemplate.Text;

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
                string SpecID = this.HdnSpec.Value;
                if (!string.IsNullOrEmpty(SpecID))
                {
                    this.bspec.UpdateSpec(SpecID, this.GeneralID);
                }
                Response.Redirect("ContentManage.aspx?NodeID=" + this.NodeID);
            }
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ContentManage.aspx?NodeID=" + this.HdnNode.Value);
        }
    }
}