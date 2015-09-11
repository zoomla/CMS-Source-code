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

public partial class manage_Content_AddSpec : System.Web.UI.Page
{
    protected B_Spec bll = new B_Spec();
    protected B_SpecCate bspeccate = new B_SpecCate();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckMulitLogin();
            if (!badmin.ChkPermissions("SpecManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            string act = base.Request.QueryString["Action"];
            string id = "0";
            if (string.IsNullOrEmpty(act))
            {
                function.WriteErrMsg("缺少编辑模式！");
            }
            else
            {
                if (act == "Add")
                {
                    id = base.Request.QueryString["CateID"];
                    if (string.IsNullOrEmpty(id))
                    {
                        function.WriteErrMsg("没有指定专题类别ID！");
                    }
                    this.HdnCateID.Value = id;
                    this.HdnSpecID.Value = "0";

                }
                if (act == "Modify")
                {
                    id = base.Request.QueryString["SpecID"];
                    if (string.IsNullOrEmpty(id))
                    {
                        function.WriteErrMsg("没有指定要修改的专题ID！");
                    }
                    M_Spec info = this.bll.GetSpec(DataConverter.CLng(id));
                    if (!info.IsNull)
                    {
                        this.HdnCateID.Value = info.SpecCate.ToString();
                        this.HdnSpecID.Value = info.SpecID.ToString();
                        this.TxtSpecName.Text = info.SpecName;
                        this.TxtSpecDir.Text = info.SpecDir;
                        this.TxtSpecDesc.Text = info.SpecDesc;
                        this.RBLOpenType.SelectedValue = DataConverter.CLng(info.OpenNew).ToString();
                        this.RBLFileExt.SelectedValue = info.ListFileExt.ToString();
                        this.RBLListFileRule.SelectedValue = info.ListFileRule.ToString();
                        this.TxtListTemplate.Text = info.ListTemplate;
                    }
                }
                M_SpecCate cate = this.bspeccate.GetCate(DataConverter.CLng(id));
                this.lblCate.Text = cate.SpecCateName;
            }
        }
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        if (this.Page.IsValid)
        {
            M_Spec info = new M_Spec();
            info.SpecID = DataConverter.CLng(this.HdnSpecID.Value);
            info.SpecName = this.TxtSpecName.Text;
            info.SpecDir = this.TxtSpecDir.Text;
            info.SpecDesc = this.TxtSpecDesc.Text;
            info.SpecCate = DataConverter.CLng(this.HdnCateID.Value);
            info.OpenNew = DataConverter.CBool(this.RBLOpenType.SelectedValue);
            info.ListFileExt = DataConverter.CLng(this.RBLFileExt.SelectedValue);
            info.ListFileRule = DataConverter.CLng(this.RBLListFileRule.SelectedValue);
            info.ListTemplate = this.TxtListTemplate.Text;
            if (info.SpecID > 0)
            {
                this.bll.AddSpec(info);
            }
            else
            {
                this.bll.UpdateSpec(info);
            }
            Response.Redirect("SpecList.aspx?CateID=" + this.HdnCateID.Value);
        }
    }
}
