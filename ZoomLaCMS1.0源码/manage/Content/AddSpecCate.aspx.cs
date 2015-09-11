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
namespace ZoomLa.WebSite.Manage.Content
{
    public partial class AddSpecCate : System.Web.UI.Page
    {
        protected B_SpecCate bll = new B_SpecCate();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("SpecCateManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                string id = base.Request.QueryString["id"];

                if (!string.IsNullOrEmpty(id))
                {
                    M_SpecCate speccate = this.bll.GetCate(DataConverter.CLng(id));
                    if (!speccate.IsNull)
                    {
                        this.TxtSpecCateName.Text = speccate.SpecCateName;
                        this.TxtSpecCateDir.Text = speccate.SpecCateDir;
                        this.TxtSpecCateDesc.Text = speccate.SpecCateDesc;
                        this.RBLOpenType.SelectedValue = DataConverter.CLng(speccate.OpenNew).ToString();
                        this.RBLFileExt.SelectedValue = speccate.FileExt.ToString();
                        this.TxtListTemplate.Text = speccate.ListTemplate;
                    }
                }
                else
                {
                    id = "0";
                }
                this.HdnSpecCateID.Value = id;
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                M_SpecCate info = new M_SpecCate();
                info.SpecCateID = DataConverter.CLng(this.HdnSpecCateID.Value);
                info.SpecCateName = this.TxtSpecCateName.Text;
                info.SpecCateDir = this.TxtSpecCateDir.Text;
                info.SpecCateDesc = this.TxtSpecCateDesc.Text;
                info.OpenNew = DataConverter.CBool(this.RBLOpenType.SelectedValue);
                info.FileExt = DataConverter.CLng(this.RBLFileExt.SelectedValue);
                info.ListTemplate = this.TxtListTemplate.Text;
                if (info.SpecCateID > 0)
                {
                    this.bll.AddSpecCate(info);
                }
                else
                {
                    this.bll.UpdateCate(info);
                }
                Response.Redirect("SpecialManage.aspx");
            }
        }
    }
}