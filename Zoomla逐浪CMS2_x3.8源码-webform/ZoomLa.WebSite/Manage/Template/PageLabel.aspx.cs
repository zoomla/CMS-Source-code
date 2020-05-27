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
    public partial class PageLabel : CustomerPageAction
    {
        private B_Label bll = new B_Label();
        private B_FunLabel bfun = new B_FunLabel();
        private string m_LabelName;

        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
            if (!this.Page.IsPostBack)
            {
              
                if (!B_ARoleAuth.Check(ZoomLa.Model.ZLEnum.Auth.label, "LabelEdit"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                if (string.IsNullOrEmpty(this.Request.QueryString["LabelName"]))
                {
                    this.m_LabelName = "";
                }
                else
                {
                    this.m_LabelName = this.Request.QueryString["LabelName"];
                }

                if (this.m_LabelName !="")
                {
                    M_Label labelInfo = this.bll.GetLabelXML(this.m_LabelName);
                    this.HdnlabelID.Value = labelInfo.LabelID.ToString();
                    this.TxtLabelName.Text = labelInfo.LableName;
                    //this.LblType.Text = labelInfo.LabelCate;
                    
                    if (labelInfo.LabelCate == "内容分页导航")
                    {
                        this.DDLType.SelectedValue = "6";
                    }
                    else
                    {
                        this.DDLType.SelectedValue = "5";
                    }
                    this.TxtLabelIntro.Text = labelInfo.Desc;

                    this.TxtLabelTemplate.Text = labelInfo.Content;
                }
                else
                {
                    this.HdnlabelID.Value = "0";
                }
            }
            this.TxtLabelTemplate.Attributes.Add("onmouseup", "dragend(this)");
            this.TxtLabelTemplate.Attributes.Add("onClick", "savePos(this)");
            this.TxtLabelTemplate.Attributes.Add("onmousemove", "DragPos(this)");
            Call.SetBreadCrumb(Master, "<li>系统设置</li><li><a href='LabelManage.aspx'>标签管理</a></li><li>动态标签设置</li>");
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                M_Label LabelInfo = new M_Label();
                LabelInfo.LabelID = DataConverter.CLng(this.HdnlabelID.Value);
                LabelInfo.LableName = this.TxtLabelName.Text;
                LabelInfo.LableType = DataConverter.CLng(this.DDLType.SelectedValue);
                LabelInfo.LabelCate = this.DDLType.SelectedIndex == 0 ? "列表分页导航" : "内容分页导航";
                LabelInfo.Desc = this.TxtLabelIntro.Text;
                LabelInfo.Param = "";
                LabelInfo.LabelTable = "";
                LabelInfo.LabelField = "";
                LabelInfo.LabelWhere = "";
                LabelInfo.LabelOrder = "";

                LabelInfo.Content = this.TxtLabelTemplate.Text;
                LabelInfo.LabelCount = "0";
                if (LabelInfo.LabelID > 0)
                {
                    this.bll.UpdateLabelXML(LabelInfo);
                }
                else
                {
                    LabelInfo.LabelNodeID = 0;
                    this.bll.AddLabelXML(LabelInfo);
                }
                function.WriteSuccessMsg("添加成功!", "LabelManage.aspx");
                //Response.Redirect("LabelManage.aspx");
            }
        }
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (this.HdnlabelID.Value == "0")
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