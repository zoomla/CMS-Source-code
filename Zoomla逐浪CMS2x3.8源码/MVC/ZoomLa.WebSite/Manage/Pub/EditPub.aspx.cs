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

namespace ZoomLaCMS.Manage.Pub
{
    public partial class EditPub : CustomerPageAction
    {
        private B_User buser = new B_User();
        private B_ModelField bfield = new B_ModelField();
        private B_Model bmodel = new B_Model();
        private B_Pub bpub = new B_Pub();
        private M_Pub pubMod = new M_Pub();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            if (!this.Page.IsPostBack)
            {
                string Pubid = string.IsNullOrEmpty(Request.QueryString["Pubid"].ToString()) ? "0" : Request.QueryString["Pubid"].ToString();
                pubMod = bpub.SelReturnModel(DataConverter.CLng(Pubid));
                string prowinfo = B_Role.GetPowerInfoByIDs(badmin.GetAdminLogin().RoleList);
                if (!badmin.GetAdminLogin().RoleList.Contains(",1,") && !prowinfo.Contains("," + pubMod.PubTableName + ","))
                {
                    function.WriteErrMsg("无权限管理该互动信息!!");
                }
                int ModelID = DataConverter.CLng(bpub.GetSelect(DataConverter.CLng(Pubid)).PubModelID.ToString());
                this.HdnPubid.Value = Pubid.ToString();
                this.HdnType.Value = string.IsNullOrEmpty(Request.QueryString["small"]) ? null : Request.QueryString["small"].ToString();
                if (DataConverter.CLng(Pubid) <= 0)
                    function.WriteErrMsg("缺少用户模型ID参数！");
                M_ModelInfo model = bmodel.GetModelById(ModelID);
                this.HdnModel.Value = ModelID.ToString();
                int ID = string.IsNullOrEmpty(Request.QueryString["ID"]) ? 0 : DataConverter.CLng(Request.QueryString["ID"]);
                this.HdnID.Value = ID.ToString();
                DataTable UserData = new DataTable();
                UserData = buser.GetUserModeInfo(model.TableName, ID, 12);
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
                    this.TextBox1.Text = dr["PubTitle"].ToString();
                    this.tx_PubContent.Text = dr["PubContent"].ToString();

                    this.HdnID.Value = dr["ID"].ToString();
                }
                string Html = bfield.InputallHtml(ModelID, 0, new ModelConfig()
                {
                    ValueDR = dr
                });
                ModelHtml.Text = Html;
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='pubmanage.aspx'>互动管理</a></li><li>修改信息</li>");
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                int ModelID = DataConverter.CLng(this.HdnModel.Value);
                M_ModelInfo model = bmodel.GetModelById(ModelID);
                int ID = DataConverter.CLng(this.HdnID.Value);

                DataTable table = new DataTable();
                table.Columns.Add(new DataColumn("FieldName", typeof(string)));
                table.Columns.Add(new DataColumn("FieldType", typeof(string)));
                table.Columns.Add(new DataColumn("FieldValue", typeof(string)));

                DataRow rowa = table.NewRow();
                rowa[0] = "PubTitle";
                rowa[1] = "TextType";
                rowa[2] = TextBox1.Text;
                table.Rows.Add(rowa);

                DataRow rowa1 = table.NewRow();
                rowa1[0] = "PubContent";
                rowa1[1] = "MultipleTextType";
                rowa1[2] = tx_PubContent.Text;
                table.Rows.Add(rowa1);

                DataTable dt = this.bfield.GetModelFieldListall(ModelID);
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
                    try
                    {
                        if (buser.UpdateModelInfo(table, model.TableName, ID))
                        {
                            if (!string.IsNullOrEmpty(this.HdnType.Value))
                            {
                                if (this.HdnType.Value == "pubs")
                                    Response.Redirect("Pubsinfo.aspx?ModeID=" + this.HdnModel.Value + "&type=0");
                                else
                                    Response.Redirect("ViewSmallPub.aspx?ModelID=" + this.HdnModel.Value + "&ID=" + this.HdnType.Value + "&type=0");
                            }
                            else
                            {

                                Response.Redirect("ViewPub.aspx?id=" + this.HdnModel.Value + "&type=0");
                            }

                        }
                    }
                    catch
                    {
                        if (!string.IsNullOrEmpty(this.HdnType.Value))
                        {
                            if (this.HdnType.Value == "pubs")
                                Response.Redirect("Pubsinfo.aspx?Pubid=" + this.HdnPubid.Value + "&type=0");
                            else
                                Response.Redirect("ViewSmallPub.aspx?Pubid=" + this.HdnPubid.Value + "&ID=" + this.HdnType.Value + "&type=0");
                        }
                        else
                        {

                            Response.Redirect("ViewPub.aspx?Pubid=" + this.HdnPubid.Value + "&type=0");
                        }
                    }


                }


            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.HdnType.Value))
            {
                if (this.HdnType.Value == "pubs")
                    Response.Redirect("Pubsinfo.aspx?Pubid=" + this.HdnPubid.Value + "&type=0");
                else
                    Response.Redirect("ViewSmallPub.aspx?Pubid=" + this.HdnPubid.Value + "&ID=" + this.HdnType.Value + "&type=0");
            }
            else
            {

                Response.Redirect("ViewPub.aspx?Pubid=" + this.HdnPubid.Value + "&type=0");
            }
        }
    }
}