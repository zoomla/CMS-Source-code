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
namespace ZoomLa.WebSite.Manage.Template
{
    public partial class LabelHtml : System.Web.UI.Page
    {
        private B_Label bll = new B_Label();
        private B_FunLabel bfun = new B_FunLabel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("LabelEdit"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                this.DDLCate.DataSource = this.bll.GetCateList();
                this.DDLCate.DataTextField = "LabelCate";
                this.DDLCate.DataValueField = "LabelCate";
                this.DDLCate.DataBind();
                this.DDLCate.Items.Insert(0, new ListItem("选择标签分类", ""));

                this.DropLabelType.DataSource = this.bll.GetCateList();
                this.DropLabelType.DataTextField = "LabelCate";
                this.DropLabelType.DataValueField = "LabelCate";
                this.DropLabelType.DataBind();
                this.DropLabelType.Items.Insert(0, new ListItem("选择标签分类", ""));

                if (!string.IsNullOrEmpty(this.Request.QueryString["LabelID"]))
                {
                    this.HdnLabelID.Value = this.Request.QueryString["LabelID"];
                    int LabelID = DataConverter.CLng(this.Request.QueryString["LabelID"]);
                    M_Label label = this.bll.GetLabel(LabelID);
                    if (label.LableType >= 2)
                        Response.Redirect("LabelSql.aspx?LabelID=" + LabelID.ToString());
                    this.TxtLabelName.Text = label.LableName;
                    this.TxtLabelName.Enabled = false;
                    this.TxtLabelType.Text = label.LabelCate;
                    this.DropLabelType.SelectedValue = label.LabelCate;
                    this.TxtLabelIntro.Text = label.Desc;
                    this.textContent.Text = label.Content;
                }
                else
                    this.HdnLabelID.Value = "0";

                this.LblSysLabel.Text = this.bfun.GetSysLabel();
                this.LblFunLabel.Text = this.bfun.GetFunLabel();
            }
            this.textContent.Attributes.Add("onmouseup", "dragend(this)");
            this.textContent.Attributes.Add("onClick", "savePos(this)");
            this.textContent.Attributes.Add("onmousemove", "DragPos(this)");
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                M_Label label = new M_Label();
                label.LableName = this.TxtLabelName.Text;
                label.LableType = 1;
                label.LabelCate = this.TxtLabelType.Text;
                label.Desc = this.TxtLabelIntro.Text;
                label.Content = this.textContent.Text;
                label.Param = "";
                label.LabelTable = "";
                label.LabelField = "";
                label.LabelWhere = "";
                label.LabelOrder = "";
                label.LabelCount = "";
                label.LabelID = DataConverter.CLng(this.HdnLabelID.Value);
                if (label.LabelID == 0)
                {
                    this.bll.AddLabel(label);
                }
                else
                {
                    this.bll.UpdateLabel(label);
                }
                Response.Redirect("LabelManage.aspx");
            }
        }
        protected void ChangeCate(object sender, EventArgs e)
        {
            string LabelCate = this.DDLCate.SelectedValue;
            DataTable dt = this.bll.GetLabelListByCate(LabelCate);
            string lblLabels = "";
            foreach (DataRow dr in dt.Rows)
            {
                if (DataConverter.CLng(dr["LabelType"]) == 1)
                {
                    lblLabels = lblLabels + "<div class=\"spanfixdiv\" outtype=\"1\" onclick=\"cit()\" code=\"" + dr["LabelName"].ToString() + "\">" + dr["LabelName"].ToString() + "</div>";
                }
                else
                {
                    if (string.IsNullOrEmpty(dr["LabelParam"].ToString()))
                    {
                        lblLabels = lblLabels + "<div class=\"spanfixdiv\" outtype=\"1\" onclick=\"cit()\" code=\"" + dr["LabelName"].ToString() + "\">" + dr["LabelName"].ToString() + "</div>";
                    }
                    else
                    {
                        string Param = dr["LabelParam"].ToString();

                        if (Param.IndexOf("|") < 0)
                        {
                            if (Param.Split(new char[] { ',' })[2] == "2")
                            {
                                lblLabels = lblLabels + "<div class=\"spanfixdiv\" outtype=\"1\" onclick=\"cit()\" code=\"" + dr["LabelName"].ToString() + "\">" + dr["LabelName"].ToString() + "</div>";
                            }
                            else
                            {
                                lblLabels = lblLabels + "<div class=\"spanfixdiv\" outtype=\"2\" onclick=\"cit()\" code=\"" + dr["LabelName"].ToString() + "\">" + dr["LabelName"].ToString() + "</div>";
                            }
                        }
                        else
                        {
                            lblLabels = lblLabels + "<div class=\"spanfixdiv\" outtype=\"2\" onclick=\"cit()\" code=\"" + dr["LabelName"].ToString() + "\">" + dr["LabelName"].ToString() + "</div>";
                        }
                    }
                }
            }
            this.LblLabel.Text = lblLabels;
        }
        protected void SelectCate(object sender, EventArgs e)
        {
            this.TxtLabelType.Text = this.DropLabelType.SelectedValue;
        }
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (this.HdnLabelID.Value == "0")
            {
                string lblname = args.Value.Trim();

                if (string.IsNullOrEmpty(lblname) || this.bll.IsExist(lblname))
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            else
            {
                args.IsValid = true;
            }
        }
}
}