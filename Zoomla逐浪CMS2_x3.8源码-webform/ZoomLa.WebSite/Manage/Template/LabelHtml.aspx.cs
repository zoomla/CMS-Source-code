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
namespace ZoomLa.WebSite.Manage.I.Template
{
    public partial class LabelHtml : CustomerPageAction
    {
        B_Label bll = new B_Label();
        B_FunLabel bfun = new B_FunLabel();
        private string LabelName { get { return (Request.QueryString["LabelName"] ?? ""); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZoomLa.Model.ZLEnum.Auth.label, "LabelEdit"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                this.DDLCate.DataSource = this.bll.GetLabelCateListXML();
                this.DDLCate.DataTextField = "name";
                this.DDLCate.DataValueField = "name";
                this.DDLCate.DataBind();
                this.DDLCate.Items.Insert(0, new ListItem("选择标签分类", ""));

                this.DropLabelType.DataSource = this.bll.GetLabelCateListXML();
                this.DropLabelType.DataTextField = "name";
                this.DropLabelType.DataValueField = "name";
                this.DropLabelType.DataBind();
                this.DropLabelType.Items.Insert(0, new ListItem("选择标签分类", ""));
                LabelName_L.Text = "添加新标签";
                if (!string.IsNullOrEmpty(LabelName))
                {
                    M_Label labelMod = bll.GetLabelXML(LabelName);
                    if (labelMod.LableType >= 2) { Response.Redirect("LabelSql.aspx?LabelName=" + Server.UrlEncode(LabelName.ToString())); }
                    this.TxtLabelName.Text = labelMod.LableName;
                    LabelName_L.Text = "当前标签：[" + labelMod.LableName + "]";
                    this.TxtLabelType.Text = labelMod.LabelCate;
                    this.DropLabelType.SelectedValue = labelMod.LabelCate;
                    this.TxtLabelIntro.Text = labelMod.Desc;
                    this.textContent.Text = labelMod.Content;
                    this.boolmodel.Checked = (labelMod.IsOpen == 1);
                    this.addroot.SelectedValue = labelMod.addroot;
                    this.falsecontent.Text = labelMod.FalseContent;
                    this.Modeltypeinfo.Text = labelMod.Modeltypeinfo;
                    this.Modelvalue.Text = labelMod.Modelvalue;
                    this.setroot.SelectedValue = labelMod.setroot;
                    this.Valueroot.Text = labelMod.Valueroot;
                }
                this.LblSysLabel.Text = this.bfun.GetSysLabel();
                this.LblFunLabel.Text = this.bfun.GetFunLabel();
            }
            //判断模式
            if (boolmodel.Checked == true)
            {
                this.isbool.Visible = true;
                this.s1.Visible = true;
                this.s2.Visible = true;
                switch (Modeltypeinfo.SelectedValue)
                {
                    case "计数判断":

                        this.addroot.Visible = true;
                        this.Valueroot.Visible = false;
                        this.setroot.Visible = true;
                        this.Modelvalue.Visible = true;
                        this.Label3.Visible = true;
                        if (this.addroot.SelectedValue == "循环计算")
                        {
                            this.Label3.Text = "计数器将自动清零,仅限包含<font color=blue>循环标签</font>有效";
                        }
                        else if (this.addroot.SelectedValue == "一直累加,仅限包含<font color=blue>循环标签</font>有效")
                        {
                            this.Label3.Text = "计数器一直累加";
                        }
                        break;
                    case "用户登录判断":
                        this.addroot.Visible = false;
                        this.Valueroot.Visible = false;
                        this.setroot.Visible = false;
                        this.Modelvalue.Visible = false;
                        this.Label3.Text = "判断用户是否登录";
                        break;
                    case "参数判断":
                        this.addroot.Visible = false;
                        this.Valueroot.Visible = true;
                        this.setroot.Visible = true;
                        this.Modelvalue.Visible = true;
                        this.Label3.Text = "判断参数是否满足条件";
                        break;
                }
            }
            else
            {
                this.s1.Visible = false;
                this.s2.Visible = false;
                this.isbool.Visible = false;
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            M_Label labelMod = new M_Label();
            if (!string.IsNullOrEmpty(LabelName)) { labelMod = bll.GetLabelXML(LabelName); }
            if (!LabelName.ToLower().Equals(TxtLabelName.Text.ToLower()))
            {
                bll.CheckLabelXML(TxtLabelName.Text);
            }
            labelMod.LableName = this.TxtLabelName.Text;
            labelMod.LableType = 1;
            labelMod.LabelCate = this.TxtLabelType.Text;
            labelMod.Desc = this.TxtLabelIntro.Text;
            labelMod.Content = this.textContent.Text;
            labelMod.Param = "";
            labelMod.LabelTable = "";
            labelMod.LabelField = "";
            labelMod.LabelWhere = "";
            labelMod.LabelOrder = "";
            labelMod.LabelCount = "";

            if (string.IsNullOrEmpty(LabelName))
            {
                labelMod.LabelNodeID = 0;
                labelMod.Modeltypeinfo = this.Modeltypeinfo.Text;
                labelMod.Modelvalue = this.Modelvalue.Text;
                labelMod.setroot = this.setroot.SelectedValue;
                labelMod.Valueroot = this.Valueroot.Text;
                labelMod.IsOpen = this.boolmodel.Checked ? 1 : 0;
                labelMod.FalseContent = this.falsecontent.Text;
                labelMod.addroot = this.addroot.SelectedValue;
                bll.AddLabelXML(labelMod);
                function.WriteSuccessMsg("添加成功", "LabelManage.aspx");
            }
            else
            {
                labelMod.Modeltypeinfo = this.Modeltypeinfo.Text;
                labelMod.Modelvalue = this.Modelvalue.Text;
                labelMod.setroot = this.setroot.SelectedValue;
                if (this.Valueroot.Text == "这里放入标签")
                {
                    labelMod.Valueroot = "";
                }
                else
                {
                    labelMod.Valueroot = this.Valueroot.Text;
                }
                labelMod.FalseContent = this.falsecontent.Text;
                labelMod.addroot = this.addroot.SelectedValue;
                labelMod.IsOpen = this.boolmodel.Checked ? 1 : 0;
                //如果修改了名称
                if (!labelMod.LableName.ToLower().Equals(LabelName.ToLower()))
                {
                    bll.DelLabelXML(LabelName);
                    bll.AddLabelXML(labelMod);
                }
                else
                {
                    bll.UpdateLabelXML(labelMod);
                }
                function.WriteSuccessMsg("修改成功", "LabelManage.aspx");
            }
        }
        protected void ChangeCate(object sender, EventArgs e)
        {
            indexStatu_Hid.Value = "1";
            string LabelCate = this.DDLCate.SelectedValue;
            DataTable dt = this.bll.SelAllLabel(LabelCate);
            string lblLabels = "";
            foreach (DataRow dr in dt.Rows)
            {
                M_Label labelinfo = this.bll.GetLabelXML(dr["LabelName"].ToString().Split('.')[0].ToString());

                if (DataConverter.CLng(labelinfo.LableType) == 1)//静态标签
                {
                    lblLabels = lblLabels + "<div class='spanfixdivchechk text-left' outtype='1' onclick='cit(this)' code='" + labelinfo.LableName + "'><a onclick=opentitle('LabelSql.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName) + "','修改标签') href='javascript:void(0)' title='修改标签'><span class='fa fa-edit'></span></a><span>" + labelinfo.LableName + "</span></div>";
                }
                else if (DataConverter.CLng(labelinfo.LableType) == 3)//数据源
                {
                    if (string.IsNullOrEmpty(labelinfo.Param))
                    {
                        lblLabels = lblLabels + "<div class='spanfixdivchechk text-left' outtype='3' onclick='cit(this)' code='" + labelinfo.LableName + "'><a onclick=opentitle('LabelSql.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName) + "','修改标签') href='javascript:void(0)' title='修改标签'><span class='fa fa-edit'></span></a><span>" + labelinfo.LableName + "</span></div>";
                    }
                    else
                    {
                        string Param = labelinfo.Param;

                        if (Param.IndexOf("|") < 0)
                        {
                            if (Param.Split(new char[] { ',' })[2] == "2")
                            {
                                lblLabels = lblLabels + "<div  outtype='3' onclick='cit(this)' code='" + labelinfo.LableName + "' class='spanfixdivchechk text-left'><a onclick=opentitle('LabelSql.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName) + "','修改标签') href='javascript:void(0)' title='修改标签'><span class='fa fa-edit'></span></a><span>" + labelinfo.LableName + "</span></div>";
                            }
                            else
                            {
                                //带参数数据源
                                lblLabels = lblLabels + "<div outtype='2' onclick='cit(this)' code='" + labelinfo.LableName + "' class='spanfixdivchechk text-left'><a onclick=opentitle('LabelSql.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName) + "','修改标签') href='javascript:void(0)' title='修改标签'><span class='fa fa-edit'></span></a><span>" + labelinfo.LableName + "</span></div>";
                            }
                        }
                        else
                        {
                            string[] dd = Param.Split(new char[] { '|' });
                            int tempd = 0;
                            for (int cd = 0; cd < dd.Length; cd++)
                            {
                                if (dd[cd].Split(new char[] { ',' })[2] == "2")
                                {
                                    tempd = 1;
                                }
                                else
                                {
                                    tempd = 0;
                                }
                            }

                            if (tempd > 0)
                            {
                                lblLabels = lblLabels + "<div class='spanfixdivchechk text-left' outtype='3' onclick='cit(this)' code='" + labelinfo.LableName + "'><a onclick=opentitle('LabelSql.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName) + "','修改标签') href='javascript:void(0)' title='修改标签'><span class='fa fa-edit'></span></a><span>" + labelinfo.LableName + "</span></div>";
                            }
                            else
                            {
                                lblLabels = lblLabels + "<div class='spanfixdivchechk text-left' outtype='4' onclick='cit(this)' code='" + labelinfo.LableName + "'><a onclick=opentitle('LabelSql.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName) + "','修改标签') href='javascript:void(0)' title='修改标签'><span class='fa fa-edit'></span></a><span>" + labelinfo.LableName + "</span></div>";
                            }

                        }
                    }
                }
                else if (DataConverter.CLng(labelinfo.LableType) >= 5)//分页标签
                {
                    if (DataConverter.CLng(labelinfo.LableType) == 5)
                    {
                        lblLabels = lblLabels + "<div class='spanfixdivchechk text-left' outtype='5' onclick='cit(this)' code='" + labelinfo.LableName + "'><a onclick=opentitle('LabelSql.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName) + "','修改标签') href='javascript:void(0)' title='修改标签'><span class='fa fa-edit'></span></a><span>" + labelinfo.LableName + "</span></div>";
                    }
                    else
                    {
                        lblLabels = lblLabels + "<div class='spanfixdivchechk text-left' outtype='6' onclick='cit(this)' code='" + labelinfo.LableName + "'><a onclick=opentitle('LabelSql.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName) + "','修改标签') href='javascript:void(0)' title='修改标签'><span class='fa fa-edit'></span></a><span>" + labelinfo.LableName + "</span></div>";
                    }
                }
                else if (DataConverter.CLng(labelinfo.LableType) < 5)
                {
                    if (string.IsNullOrEmpty(labelinfo.Param))
                    {
                        lblLabels = lblLabels + "<div class='spanfixdivchechk text-left' outtype='1' onclick='cit(this)' code='" + labelinfo.LableName + "'><a onclick=opentitle('LabelSql.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName) + "','修改标签') href='javascript:void(0)' title='修改标签'><span class='fa fa-edit'></span></a><span>" + labelinfo.LableName + "</span></div>";
                    }
                    else
                    {
                        string Param = labelinfo.Param;

                        if (Param.IndexOf("|") < 0)
                        {
                            if (Param.Split(new char[] { ',' })[2] == "2")
                            {
                                lblLabels = lblLabels + "<div class='spanfixdivchechk text-left' outtype='1' onclick='cit(this)' code='" + labelinfo.LableName + "'><a onclick=opentitle('LabelSql.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName) + "','修改标签') href='javascript:void(0)' title='修改标签'><span class='fa fa-edit'></span></a><span>" + labelinfo.LableName + "</span></div>";
                            }
                            else
                            {
                                lblLabels = lblLabels + "<div class='spanfixdivchechk text-left' outtype='2' onclick='cit(this)' code='" + labelinfo.LableName + "'><a onclick=opentitle('LabelSql.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName) + "','修改标签') href='javascript:void(0)' title='修改标签'><span class='fa fa-edit'></span></a><span>" + labelinfo.LableName + "</span></div>";
                            }
                        }
                        else
                        {
                            lblLabels = lblLabels + "<div class='spanfixdivchechk text-left' outtype='2' onclick='cit(this)' code='" + labelinfo.LableName + "'><a onclick=opentitle('LabelSql.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName) + "','修改标签') href='javascript:void(0)' title='修改标签'><span class='fa fa-edit'></span></a><span>" + labelinfo.LableName + "</span></div>";
                        }
                    }
                }
                else
                {
                    lblLabels = lblLabels + "<div class='spanfixdivchechk text-left' outtype='3' onclick='cit(this)' code='" + labelinfo.LableName + "'><a onclick=opentitle('LabelSql.aspx?LabelName=" + Server.UrlEncode(labelinfo.LableName) + "','修改标签') href='javascript:void(0)' title='修改标签'><span class='fa fa-edit'></span></a><span>" + labelinfo.LableName + "</span></div>";
                }
            }
            this.LblLabel.Text = lblLabels;

            if (dt != null)
            {
                dt.Dispose();
            }
        }
        protected void SelectCate(object sender, EventArgs e)
        {
            this.TxtLabelType.Text = this.DropLabelType.SelectedValue;
        }
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(LabelName))
            {
                string lblname = args.Value.Trim();
                if (string.IsNullOrEmpty(lblname) || this.bll.IsExistXML(lblname))
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