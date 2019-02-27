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
using ZoomLa.Components;

namespace ZoomLaCMS.Manage.Page
{
    public partial class EditContent : CustomerPageAction
    {
        protected B_Node bnode = new B_Node();
        protected B_Model bmode = new B_Model();
        protected B_ShowField bshow = new B_ShowField();
        protected B_ModelField bfield = new B_ModelField();
        protected B_Content bll = new B_Content();
        protected int NodeID;
        protected int ModelID;
        protected int GeneralID;
        protected B_Sensitivity sll = new B_Sensitivity();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                if (!B_ARoleAuth.Check(ZoomLa.Model.ZLEnum.Auth.page, "PageContent"))
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
                this.txtTitle.Text = Cdata.Title;
                if (Cdata.EliteLevel > 0)    //是否推荐1-推荐，0-不推荐
                    this.ChkAudit.Checked = true;
                else
                    this.ChkAudit.Checked = false;
                this.HdnItem.Value = this.GeneralID.ToString();
                this.HdnNode.Value = this.NodeID.ToString();
                this.TxtTemplate_hid.Value = Cdata.Template;//指定内容模板  
                DataTable dtContent = this.bll.GetContent(this.GeneralID);
                ModelHtml.Text = bfield.InputallHtml(ModelID, NodeID, new ModelConfig()//生成页面内容
                {
                    ValueDT = dtContent
                });
                Call.SetBreadCrumb(Master, "<li>后台管理</li><li>企业黄页</li><li><a href='PageContent.aspx'>黄页内容管理</a></li><li>编辑内容</li>");
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
                int elite = this.ChkAudit.Checked ? 1 : 0;
                if (CData.EliteLevel == 0 && elite == 1)
                {
                    if (SiteConfig.UserConfig.RecommandRule > 0)
                    {
                        B_User buser = new B_User();
                        M_UserInfo muser = buser.GetUserByName(CData.Inputer);
                        if (!muser.IsNull)
                        {
                            buser.ChangeVirtualMoney(muser.UserID, new M_UserExpHis()
                            {
                                score = SiteConfig.UserConfig.InfoRule,
                                detail = "修改内容:" + this.txtTitle.Text + "增加积分",
                                ScoreType = (int)M_UserExpHis.SType.Point
                            });
                        }
                    }
                }
                CData.EliteLevel = elite;
                CData.InfoID = "";
                CData.SpecialID = "";
                CData.PdfLink = "";
                CData.Template = this.TxtTemplate_hid.Value;

                DataTable dt = this.bfield.GetModelFieldList(this.ModelID);

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
                    string ftype = dr["FieldType"].ToString();
                    row[1] = ftype;
                    string fvalue = this.Page.Request.Form["txt_" + dr["FieldName"].ToString()];
                    if (ftype == "TextType" || ftype == "MultipleTextType" || ftype == "MultipleHtmlType")
                    {
                        fvalue = sll.ProcessSen(fvalue);
                    }
                    row[2] = fvalue;
                    table.Rows.Add(row);
                }
                this.bll.UpdateContent(table, CData);

                Response.Redirect("PageContent.aspx?ModelID=" + this.ModelID);
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PageContent.aspx");
        }
    }
}