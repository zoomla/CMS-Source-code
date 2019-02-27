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
using ZoomLa.Components;
using ZoomLa.Common;
using System.IO;
using ZoomLa.BLL;
using ZoomLa.Model;
namespace ZoomLa.WebSite.Manage.Template
{
    public partial class TemplateEdit : System.Web.UI.Page
    {
        protected string FilePath = string.Empty;
        protected string FileName = string.Empty;
        protected string ShowPath = string.Empty;
        private B_Label bll = new B_Label();
        private B_FunLabel bfun = new B_FunLabel();
        protected void Page_Load(object sender, EventArgs e)
        {

            string strPath = base.Request.QueryString["filepath"];
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("TemplateEdit"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                this.DDLCate.DataSource = this.bll.GetCateList();
                this.DDLCate.DataTextField = "LabelCate";
                this.DDLCate.DataValueField = "LabelCate";
                this.DDLCate.DataBind();
                this.DDLCate.Items.Insert(0, new ListItem("选择标签类型", ""));

                this.DDLField.DataSource = this.bll.GetSourceLabel();
                this.DDLField.DataTextField = "LabelName";
                this.DDLField.DataValueField = "LabelID";
                this.DDLField.DataBind();
                this.DDLField.Items.Insert(0,new ListItem("选择数据源标签",""));

                string TemplateDir = SiteConfig.SiteOption.TemplateDir;
                if (string.IsNullOrEmpty(strPath))
                {
                    strPath = "";
                    this.ShowPath = "/";
                }
                else
                {
                    this.ShowPath = strPath;
                }
                strPath = base.Request.PhysicalApplicationPath + TemplateDir + strPath;
                strPath = strPath.Replace("/", @"\");

                this.FilePath = strPath;
                string FileExp = Path.GetExtension(this.FilePath);
                if (!string.IsNullOrEmpty(FileExp))
                {
                    this.FileName = Path.GetFileName(this.FilePath);
                    this.lblFielName.Text = this.FileName;
                    this.TxtFilename.Text = this.FileName;
                    this.TxtFilename.Visible = false;
                    this.textContent.Text = FileSystemObject.ReadFile(this.FilePath);
                    this.ShowPath = this.ShowPath.Replace(this.FileName, "");
                    this.Hdnmethod.Value = "append";
                }
                else
                {
                    this.FileName = "";
                    this.TxtFilename.Text = "";
                    this.TxtFilename.Visible = true;
                    this.lblFielName.Text = "";
                    this.lblFielName.Visible = false;
                    this.Hdnmethod.Value = "add";
                }
                this.lblSys.Text = this.bfun.GetSysLabel();
                this.lblFun.Text = this.bfun.GetFunLabel();

                this.HdnFilePath.Value = this.FilePath;
                this.HdnShowPath.Value = this.ShowPath;
                this.textContent.Attributes.Add("onmouseup", "dragend(this)");
                this.textContent.Attributes.Add("onClick", "savePos(this)");
                this.textContent.Attributes.Add("onmousemove", "DragPos(this)");                
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                if (this.Hdnmethod.Value == "add")
                {
                    this.FilePath = this.HdnFilePath.Value + @"\" + this.TxtFilename.Text;
                    FileSystemObject.WriteFile(this.FilePath, this.textContent.Text);
                }
                if (this.Hdnmethod.Value == "append")
                {
                    this.FilePath = this.HdnFilePath.Value;
                    FileSystemObject.WriteFile(this.FilePath, this.textContent.Text);
                }
                string sPath = this.HdnShowPath.Value.Replace("/", "");
                base.Response.Redirect("TemplateManage.aspx?Dir=" + sPath);
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (this.Hdnmethod.Value == "add")
            {
                this.textContent.Text = "";
            }
            if (this.Hdnmethod.Value == "append")
            {
                string str = FileSystemObject.ReadFile(this.HdnFilePath.Value);
                this.textContent.Text = str;
            }
        }

        protected void ChangeSourceField(object sender, EventArgs e)
        {
            int LabelID = DataConverter.CLng(this.DDLField.SelectedValue);
            if (LabelID == 0)
                this.LblSourceField.Text = "";
            else
            {
                M_Label labelinfo = this.bll.GetLabel(LabelID);
                string lbl = SetLabelColumn(labelinfo.LabelField,labelinfo.LabelTable,labelinfo.LableName);
                this.LblSourceField.Text = lbl;
            }
        }
        private string SetLabelColumn(string sField,string TableName,string LabelName)
        {
            string[] arrField = sField.Split(',');
            string result = "";
            for (int i = 0; i < arrField.Length; i++)
            {
                if (arrField[i].IndexOf('.') > 0)
                {
                    if (arrField[i].Split('.')[1] == "*")
                    {
                        DataTable dt = this.bll.GetTableField(arrField[i].Split('.')[0]);
                        foreach (DataRow dr in dt.Rows)
                        {
                            result = result + "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code='{SField sid=\"" + LabelName + "\" FD=\"" + dr["fieldname"].ToString() + "\"/}'>{SField FD=\"" + dr["fieldname"].ToString() + "\"/}</div>";
                        }
                    }
                    else
                    {
                        result = result + "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code='{SField sid=\"" + LabelName + "\" FD=\"" + arrField[i].Split('.')[1] + "\"/}'>{SField FD=\"" + arrField[i].Split('.')[1] + "\"/}</div>";
                    }
                }
                else
                {
                    if (arrField[i] == "*")
                    {
                        DataTable dt = this.bll.GetTableField(TableName);
                        foreach (DataRow dr in dt.Rows)
                        {
                            result = result + "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code='{SField sid=\"" + LabelName + "\" FD=\"" + dr["fieldname"].ToString() + "\"/}'>{SField FD=\"" + dr["fieldname"].ToString() + "\"/}</div>";
                        }
                    }
                    else
                    {
                        result = result + "<div class=\"spanfixdiv\" outtype=\"0\" onclick=\"cit()\" code='{SField sid=\"" + LabelName + "\" FD=\"" + arrField[i] + "\"/}'>{SField FD=\"" + arrField[i] + "\"/}</div>";
                    }
                }
            }
            return result;
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
                    if (DataConverter.CLng(dr["LabelType"]) == 3)
                    {
                        if (string.IsNullOrEmpty(dr["LabelParam"].ToString()))
                        {
                            lblLabels = lblLabels + "<div class=\"spanfixdiv\" outtype=\"3\" onclick=\"cit()\" code=\"" + dr["LabelName"].ToString() + "\">" + dr["LabelName"].ToString() + "</div>";
                        }
                        else
                        {
                            string Param = dr["LabelParam"].ToString();

                            if (Param.IndexOf("|") < 0)
                            {
                                if (Param.Split(new char[] { ',' })[2] == "2")
                                {
                                    lblLabels = lblLabels + "<div class=\"spanfixdiv\" outtype=\"3\" onclick=\"cit()\" code=\"" + dr["LabelName"].ToString() + "\">" + dr["LabelName"].ToString() + "</div>";
                                }
                                else
                                {
                                    lblLabels = lblLabels + "<div class=\"spanfixdiv\" outtype=\"4\" onclick=\"cit()\" code=\"" + dr["LabelName"].ToString() + "\">" + dr["LabelName"].ToString() + "</div>";
                                }
                            }
                            else
                            {
                                lblLabels = lblLabels + "<div class=\"spanfixdiv\" outtype=\"4\" onclick=\"cit()\" code=\"" + dr["LabelName"].ToString() + "\">" + dr["LabelName"].ToString() + "</div>";
                            }
                        }
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
            }
            this.LblLabel.Text = lblLabels;
        }
    }
}