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

public partial class User_Pages_AddPub : System.Web.UI.Page
{
    private B_User buser = new B_User();
    private B_ModelField bfield = new B_ModelField();
    private B_Model bmodel = new B_Model();
    private B_Pub bpub = new B_Pub();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            string Pubid = string.IsNullOrEmpty(Request.QueryString["Pubid"]) ? "0" : Request.QueryString["Pubid"].ToString();
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
            //DataRow dr = null;
            this.LblModelName.Text = "添加" + model.ModelName;
            string Html = bfield.InputallHtml(ModelID, 0, new ModelConfig()
            {
                Source = ModelConfig.SType.UserBase
            });
            ModelHtml.Text = Html;
        }
    }

    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        if (this.Page.IsValid)
        {
            int ModelID = DataConverter.CLng(this.HdnModel.Value);
            M_ModelInfo model = bmodel.GetModelById(ModelID);
            int ID = DataConverter.CLng(this.HdnID.Value);
            DataTable dt = this.bfield.GetModelFieldListall(ModelID);
            DataTable dda = buser.GetUserModeInfo(model.TableName, DataConverter.CLng(this.HiddenParentid.Value), 18);
            string Contentid = "1";
            if (dda.Rows.Count > 0)
            {
                Contentid = dda.Rows[0]["PubContentid"].ToString();
            }
            else
            {
                function.WriteErrMsg("输入无效参数11！");
            }
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("FieldValue", typeof(string)));

            DataRow rowa = table.NewRow();
            rowa[0] = "PubTitle";
            rowa[1] = "TextType";
            rowa[2] = this.Page.Request.Form["TextBox1"];
            table.Rows.Add(rowa);

            DataRow rowa1 = table.NewRow();
            rowa1[0] = "PubContent";
            rowa1[1] = "MultipleTextType";
            rowa1[2] = this.Page.Request.Form["tx_PubContent"];
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

            M_UserInfo aa = buser.GetLogin();

            DataRow rowa7 = table.NewRow();
            rowa7[0] = "PubUserName";
            rowa7[1] = "TextType";
            rowa7[2] = aa.UserName;
            table.Rows.Add(rowa7);

            DataRow rowa8 = table.NewRow();
            rowa8[0] = "PubUserID";
            rowa8[1] = "TextType";
            rowa8[2] = aa.UserID;
            table.Rows.Add(rowa8);

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
            try
            {
                if (buser.AddUserModel(table, model.TableName))
                {
                    //function.WriteErrMsg("添加成功", "ViewSmallPub.aspx?ModelID=" + this.HdnModel.Value + "&ID=" + this.HiddenParentid.Value);
                    Response.Write("<script>alert('添加成功!');window.opener.location.reload();</script>");
                }
            }
            catch
            {

                Response.Redirect("ViewSmallPub.aspx?Pubid=" + this.HdnPubid.Value + "&ID=" + this.HiddenParentid.Value);
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewSmallPub.aspx?Pubid=" + this.HdnPubid.Value + "&ID=" + this.HiddenParentid.Value);
    }
}
