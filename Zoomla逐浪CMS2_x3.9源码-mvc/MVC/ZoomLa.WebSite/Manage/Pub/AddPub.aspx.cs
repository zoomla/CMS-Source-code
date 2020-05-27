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
    public partial class AddPub : CustomerPageAction
    {
        private B_User buser = new B_User();
        private B_ModelField bfield = new B_ModelField();
        private B_Model bmodel = new B_Model();
        private B_Pub bpub = new B_Pub();
        private M_Pub pubMod = new M_Pub();
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
            if (!this.Page.IsPostBack)
            {
                string Pubid = string.IsNullOrEmpty(Request.QueryString["Pubid"]) ? "0" : Request.QueryString["Pubid"].ToString();
                pubMod = bpub.SelReturnModel(DataConverter.CLng(Pubid));
                string prowinfo = B_Role.GetPowerInfoByIDs(badmin.GetAdminLogin().RoleList);
                if (!badmin.GetAdminLogin().RoleList.Contains(",1,") && !prowinfo.Contains("," + pubMod.PubTableName + ","))
                {
                    function.WriteErrMsg("无权限管理该互动信息!!");
                }
                string Parentid = string.IsNullOrEmpty(Request.QueryString["Parentid"]) ? "0" : Request.QueryString["Parentid"].ToString();
                int ModelID = DataConverter.CLng(bpub.GetSelect(DataConverter.CLng(Pubid)).PubModelID.ToString());
                this.HdnType.Value = string.IsNullOrEmpty(Request.QueryString["small"]) ? null : Request.QueryString["small"].ToString();
                if (DataConverter.CLng(Pubid) <= 0 || DataConverter.CLng(Parentid) <= 0)
                    function.WriteErrMsg("缺少用户参数！");
                //jc:查找相应模版实体
                M_ModelInfo model = bmodel.GetModelById(ModelID);
                this.HdnPubid.Value = Pubid;
                this.HiddenParentid.Value = Parentid;
                this.HdnModel.Value = ModelID.ToString();
                int ID = string.IsNullOrEmpty(Request.QueryString["ID"]) ? 0 : DataConverter.CLng(Request.QueryString["ID"]);
                this.HdnID.Value = ID.ToString();
                this.LblModelName.Text = "回复" + model.ItemName;
                string Html = bfield.InputallHtml(ModelID, 0, new ModelConfig());
                //GetPubModelHtmlall
                ModelHtml.Text = Html;



                ///////////
                DataTable UserData = new DataTable();
                DataTable aas = ShowGrid();

                int zong = aas == null ? 0 : aas.Rows.Count;
                int sID = string.IsNullOrEmpty(Request.QueryString["Parentid"]) ? 0 : DataConverter.CLng(Request.QueryString["Parentid"]);
                UserData = buser.GetUserModeInfo(model.TableName, sID, 18);
                DetailsView1.DataSource = UserData.DefaultView;
                DetailsView1.DataBind();
                //////////////
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='pubmanage.aspx'>互动管理</a></li><li>添加信息</li>");

            }
        }

        private DataTable ShowGrid()
        {
            string Pubid = string.IsNullOrEmpty(Request.QueryString["Pubid"]) ? "0" : Request.QueryString["Pubid"].ToString();
            int ModelID = DataConverter.CLng(bpub.GetSelect(DataConverter.CLng(Pubid)).PubModelID.ToString());

            DetailsView1.AutoGenerateRows = false;
            DataControlFieldCollection dcfc = DetailsView1.Fields;

            dcfc.Clear();


            BoundField bf2 = new BoundField();
            bf2.HeaderText = "ID";
            bf2.DataField = "ID";
            bf2.HeaderStyle.Width = Unit.Percentage(15);
            bf2.HeaderStyle.CssClass = "tdbgleft";
            bf2.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bf2.ItemStyle.HorizontalAlign = HorizontalAlign.Left;

            dcfc.Add(bf2);

            BoundField bf5 = new BoundField();
            bf5.HeaderText = "用户名";
            bf5.DataField = "PubUserName";
            bf5.HeaderStyle.CssClass = "tdbgleft";
            bf5.HeaderStyle.Width = Unit.Percentage(15);
            bf5.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bf5.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            dcfc.Add(bf5);

            BoundField bf1 = new BoundField();
            bf1.HeaderText = "标题";
            bf1.DataField = "PubTitle";
            bf1.HeaderStyle.CssClass = "tdbgleft";
            bf1.HeaderStyle.Width = Unit.Percentage(15);
            bf1.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bf1.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            bf1.HtmlEncode = false;
            dcfc.Add(bf1);

            BoundField bfa = new BoundField();
            bfa.HeaderText = "内容";
            bfa.DataField = "PubContent";
            bfa.HeaderStyle.CssClass = "tdbgleft";
            bfa.HeaderStyle.Width = Unit.Percentage(15);
            bfa.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bfa.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            bfa.HtmlEncode = false;
            dcfc.Add(bfa);

            BoundField bf3 = new BoundField();
            bf3.HeaderText = "IP地址";
            bf3.DataField = "PubIP";
            bf3.HeaderStyle.CssClass = "tdbgleft";
            bf3.HeaderStyle.Width = Unit.Percentage(15);
            bf3.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bf3.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            bf3.HtmlEncode = false;
            dcfc.Add(bf3);

            BoundField bf4 = new BoundField();
            bf4.HeaderText = "添加时间";
            bf4.DataField = "PubAddTime";
            bf4.HeaderStyle.CssClass = "tdbgleft";
            bf4.HeaderStyle.Width = Unit.Percentage(15);
            bf4.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bf4.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            bf4.HtmlEncode = false;
            dcfc.Add(bf4);


            DataTable dt = this.bfield.GetModelFieldList(ModelID);
            foreach (DataRow dr in dt.Rows)
            {
                BoundField bf = new BoundField();
                bf.HeaderText = dr["FieldAlias"].ToString();
                bf.DataField = dr["FieldName"].ToString();
                bf.HeaderStyle.Width = Unit.Percentage(15);
                bf.HeaderStyle.CssClass = "tdbgleft";
                bf.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                bf.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                bf.HtmlEncode = false;
                dcfc.Add(bf);
            }
            return dt;
        }


        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                int ModelID = DataConverter.CLng(this.HdnModel.Value);
                M_ModelInfo model = bmodel.GetModelById(ModelID);
                int ID = DataConverter.CLng(this.HdnID.Value);


                DataTable dda = buser.GetUserModeInfo(model.TableName, DataConverter.CLng(this.HiddenParentid.Value), 18);
                string Contentid = "1";
                if (dda.Rows.Count > 0)
                {
                    Contentid = dda.Rows[0]["PubContentid"].ToString();
                }
                else
                {
                    function.WriteErrMsg("输入无效参数！");
                }
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

                DataRow rowa2 = table.NewRow();
                rowa2[0] = "PubIP";
                rowa2[1] = "TextType";
                rowa2[2] = this.Page.Request.UserHostAddress;
                table.Rows.Add(rowa2);

                DataRow rowa3 = table.NewRow();
                rowa3[0] = "PubAddTime";
                rowa3[1] = "DateType";
                rowa3[2] = this.Page.Request.UserHostAddress;
                table.Rows.Add(rowa3);

                DataRow rowa4 = table.NewRow();
                rowa4[0] = "Pubupid";
                rowa4[1] = "int";
                rowa4[2] = DataConverter.CLng(this.HdnPubid.Value);
                table.Rows.Add(rowa4);

                DataRow rowa5 = table.NewRow();
                rowa5[0] = "PubContentid";
                rowa5[1] = "int";
                rowa5[2] = Contentid;
                table.Rows.Add(rowa5);

                DataRow rowa6 = table.NewRow();
                rowa6[0] = "Parentid";
                rowa6[1] = "int";
                rowa6[2] = this.HiddenParentid.Value;
                table.Rows.Add(rowa6);
                B_Admin badmin = new B_Admin();
                M_AdminInfo ad = badmin.GetAdminLogin();

                DataRow rowa7 = table.NewRow();
                rowa7[0] = "PubUserName";
                rowa7[1] = "TextType";
                rowa7[2] = ad.AdminName;
                table.Rows.Add(rowa7);

                DataRow rowa8 = table.NewRow();
                rowa8[0] = "PubUserID";
                rowa8[1] = "int";
                rowa8[2] = ad.AdminId;
                table.Rows.Add(rowa8);


                DataRow rowa9 = table.NewRow();
                rowa9[0] = "Pubstart";
                rowa9[1] = "int";
                rowa9[2] = 1;
                table.Rows.Add(rowa9);

                DataTable dt = this.bfield.GetModelFieldListall(ModelID);

                foreach (DataRow dr in dt.Rows)
                {
                    //if (DataConverter.CBool(dr["IsNotNull"].ToString()))
                    //{
                    //    if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + dr["FieldName"].ToString()]))
                    //    {
                    //        function.WriteErrMsg(dr["FieldAlias"].ToString() + "不能为空!");
                    //    }
                    //}

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
                try
                {
                    if (buser.AddUserModel(table, model.TableName))
                    {
                        Response.Redirect("ViewSmallPub.aspx?ModelID=" + this.HdnModel.Value + "&ID=" + this.HiddenParentid.Value + "&type=" + Request["type"]);
                    }
                }
                catch
                {
                    Response.Redirect("ViewSmallPub.aspx?Pubid=" + this.HdnPubid.Value + "&ID=" + this.HiddenParentid.Value + "&type=" + Request["type"]);
                }

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(this.HdnType.Value))
            //{
            //    Response.Redirect("ViewSmallPub.aspx?Pubid=" + this.HdnPubid.Value + "&ID=" + this.HiddenParentid.Value);
            //}
            //else
            //{
            //    Response.Redirect("Pubsinfo.aspx?Pubid=" + this.HdnPubid.Value );
            //}
        }
    }
}