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
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Xml;
namespace ZoomLaCMS.Manage.User
{
    public partial class EditJobsinfos : CustomerPageAction
    {

        private B_User buser = new B_User();
        private B_ModelField bfield = new B_ModelField();
        private B_Model bmodel = new B_Model();
        private B_Group bgp = new B_Group();
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();
            if (!this.Page.IsPostBack)
            {
                int ModelID = string.IsNullOrEmpty(Request.QueryString["ModelID"]) ? 0 : DataConverter.CLng(Request.QueryString["ModelID"]);
                string ModelName = string.IsNullOrEmpty(Request.QueryString["ModelName"]) ? "none" : Request.QueryString["ModelName"].ToString();
                if (ModelID <= 0)
                    function.WriteErrMsg("缺少用户模型ID参数！");
                //jc:查找相应模版实体
                M_ModelInfo model = bmodel.GetModelById(ModelID);
                this.Label1.Value = model.ModelName;
                this.HdnModel.Value = ModelID.ToString();
                this.HdnModelName.Value = ModelName;

                int ID = string.IsNullOrEmpty(Request.QueryString["ID"]) ? 0 : DataConverter.CLng(Request.QueryString["ID"]);

                this.HdnID.Value = ID.ToString();

                DataTable UserData = new DataTable();
                UserData = buser.GetUserModeInfo(model.TableName, ID, 2);
                DataRow dr;
                if (UserData == null)
                    dr = null;
                else
                {
                    if (UserData.Rows.Count == 0)
                        dr = null;
                    else
                        dr = UserData.Rows[0];
                }
                if (dr == null)
                    this.LblModelName.Text = "添加" + model.ModelName;
                else
                {

                    this.LblModelName.Text = "修改" + model.ModelName;

                    this.HdnID.Value = dr["ID"].ToString();
                }


            }

            Call.SetBreadCrumb(Master, "<li>后台管理</li><li>人才系统管理</li><li><asp:Literal Text=''>" + Label1.Value + "</asp:Literal></li>");
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {

            if (this.Page.IsValid)
            {
                int ModelID = DataConverter.CLng(this.HdnModel.Value);
                M_ModelInfo model = bmodel.GetModelById(ModelID);
                int ID = DataConverter.CLng(this.HdnID.Value);
                DataTable dt = this.bfield.GetModelFieldListall(ModelID);

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
                    string ftype = dr["FieldType"].ToString();
                    if (ftype == "NumType")
                    {
                        string[] fd = dr["Content"].ToString().Split(new char[] { ',' });
                        string[] fdty = fd[1].Split(new char[] { '=' });

                        int numstyle = DataConverter.CLng(fdty[1]);
                        if (numstyle == 1)
                            ftype = "int";
                        if (numstyle == 2)
                            ftype = "float";
                        if (numstyle == 3)
                            ftype = "money";
                    }
                    row[1] = ftype;
                    string fvalue = this.Page.Request.Form["txt_" + dr["FieldName"].ToString()];
                    row[2] = fvalue;
                    table.Rows.Add(row);
                }
                if (ID > 0)
                {
                    if (buser.UpdateModelInfo(table, model.TableName, ID))
                    {

                        function.WriteSuccessMsg(model.ModelName + "修改完成！");
                        if (!string.IsNullOrEmpty(this.HdnModelName.Value))
                            Response.Redirect("Jobsinfos.aspx?modeid=" + this.HdnModelName.Value);
                        else
                            Response.Redirect("Jobsinfos.aspx?modeid=" + this.HdnModelName.Value);
                    }
                }

                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Jobsinfos.aspx?modeid=" + this.HdnModelName.Value);
        }
    }
}