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

namespace ZoomLaCMS.Manage.Content
{
    public partial class EditListContent : CustomerPageAction
    {
        private B_User buser = new B_User();
        private B_ModelField bfield = new B_ModelField();
        private B_Model bmodel = new B_Model();
        private B_Group bgp = new B_Group();
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li class='active'>内容批量修改</li>");
            if (!this.Page.IsPostBack)
            {
                int ModelID = string.IsNullOrEmpty(Request.QueryString["ModelID"]) ? 0 : DataConverter.CLng(Request.QueryString["ModelID"]);

                string ID = string.IsNullOrEmpty(Request.QueryString["ID"]) ? "" : Request.QueryString["ID"].ToString();

                string ModelName = string.IsNullOrEmpty(Request.QueryString["ModelName"]) ? "qiye" : Request.QueryString["ModelName"].ToString();
                if (ID == "")
                {
                    function.WriteErrMsg("请选择内容ID！");
                }

                if (ModelID <= 0)
                {
                    function.WriteErrMsg("缺少用户模型ID参数！");
                }

                M_ModelInfo model = bmodel.GetModelById(ModelID);

                this.HdnModel.Value = ModelID.ToString();
                this.HdnModelName.Value = ModelName;
                this.HdnID.Value = ID.ToString();

                //string[] modarr = ID.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (ID.IndexOf("|") > -1)
                {
                    ID = ID.Replace("|", ",");
                }
                itemlist.Value = ID;
                int NodeID = DataConverter.CLng(Request.Form["NodeID"]);
                string Html = this.bfield.GetInputHtmlUser(ModelID, NodeID);
                ModelHtml.Text = Html;
                // Response.Redirect("ModelInfoList.aspx?ModelID=" + ModelID.ToString());
                DataBind();
            }
        }
        protected void DataBind(string key = "")
        {
            DataTable dt = this.bfield.GetModelFieldList(Convert.ToInt32(HdnModel.Value));
            Call commCall = new Call();
            DataTable table = commCall.GetDTFromPage(dt, Page, ViewState, null, false);
            Egv.DataSource = table;
            Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            DataBind();
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                int ModelID = DataConverter.CLng(this.HdnModel.Value);
                M_ModelInfo model = bmodel.GetModelById(ModelID);

                string ID = this.HdnID.Value;

                string[] modarr = ID.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                DataTable dt = this.bfield.GetModelFieldList(ModelID);

                Call commCall = new Call();
                DataTable table = commCall.GetDTFromPage(dt, Page, ViewState, null, false);
                GetTable(table);
                //DataTable table = new DataTable();
                //table.Columns.Add(new DataColumn("FieldName", typeof(string)));
                //table.Columns.Add(new DataColumn("FieldType", typeof(string)));
                //table.Columns.Add(new DataColumn("FieldValue", typeof(string)));
                //foreach (DataRow dr in dt.Rows)
                //{
                //    if (DataConverter.CBool(dr["IsNotNull"].ToString()))
                //    {
                //        if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + dr["FieldName"].ToString()]))
                //        {
                //            function.WriteErrMsg(dr["FieldAlias"].ToString() + "不能为空!");
                //        }
                //    }
                //    if (dr["FieldType"].ToString() == "FileType")
                //    {
                //        string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
                //        bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
                //        string sizefield = Sett[1].Split(new char[] { '=' })[1];
                //        if (chksize && sizefield != "")
                //        {
                //            DataRow row2 = table.NewRow();
                //            row2[0] = sizefield;
                //            row2[1] = "FileSize";
                //            row2[2] = this.Page.Request.Form["txt_" + sizefield];
                //            table.Rows.Add(row2);
                //        }
                //    }
                //    if (dr["FieldType"].ToString() == "MultiPicType")
                //    {
                //        string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
                //        bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
                //        string sizefield = Sett[1].Split(new char[] { '=' })[1];
                //        if (chksize && sizefield != "")
                //        {
                //            if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + sizefield]))
                //            {
                //                function.WriteErrMsg(dr["FieldAlias"].ToString() + "的缩略图不能为空!");
                //            }
                //            DataRow row1 = table.NewRow();
                //            row1[0] = sizefield;
                //            row1[1] = "ThumbField";
                //            row1[2] = this.Page.Request.Form["txt_" + sizefield];
                //            table.Rows.Add(row1);
                //        }
                //    }
                //    DataRow row = table.NewRow();
                //    row[0] = dr["FieldName"].ToString();
                //    string ftype = dr["FieldType"].ToString();
                //    if (ftype == "NumType")
                //    {
                //        string[] fd = dr["Content"].ToString().Split(new char[] { ',' });
                //        string[] fdty = fd[1].Split(new char[] { '=' });

                //        int numstyle = DataConverter.CLng(fdty[1]);
                //        if (numstyle == 1)
                //            ftype = "int";
                //        if (numstyle == 2)
                //            ftype = "float";
                //        if (numstyle == 3)
                //            ftype = "money";
                //    }

                //    row[1] = ftype;
                //    string fvalue = this.Page.Request.Form["txt_" + dr["FieldName"].ToString()];

                //    row[2] = fvalue;
                //    table.Rows.Add(row);
                //}
                B_Content bcc = new B_Content();
                for (int i = 0; i < modarr.Length; i++)
                {
                    M_CommonData mc = bcc.GetCommonData(DataConverter.CLng(modarr[i]));
                    bool result = buser.UpdateModelInfo(table, model.TableName, mc.ItemID);

                }
                function.WriteSuccessMsg("修改完成", "../Content/ContentManage.aspx?NodeID=" + this.HdnModelName.Value);

                //if (!string.IsNullOrEmpty(this.HdnModelName.Value))
                //    Response.Redirect("ContentManage.aspx?NodeID=" + this.HdnModelName.Value);
                //else
                //    Response.Redirect();



            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ContentManage.aspx?NodeID=" + this.HdnModelName.Value);
        }
        public DataTable GetTable(DataTable table)
        {
            string str = "";
            if (!string.IsNullOrEmpty(Request.Form["chkSel"]))//生成字段
            {
                string[] chkArr = Request.Form["chkSel"].Split(',');
                for (int i = 0; i < chkArr.Length; i++)
                {
                    str += "'" + chkArr[i] + "',";
                }
                str = str.Trim(',');
            }
            if (str != "")
            {
                DataRow[] dr = table.Select("FieldName not in (" + str + ")");//进行筛选
                for (int j = 0; j < dr.Length; j++)
                {
                    table.Rows.Remove(dr[j]);
                }
            }
            else
                table = new DataTable();
            return table;
        }
    }
}