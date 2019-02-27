namespace ZoomLa.WebSite.Manage.Content
{
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

    public partial class SpecBatchSet : System.Web.UI.Page
    {
        private B_Spec bll = new B_Spec();

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
                this.LstSpec.DataSource = this.bll.GetSpecAll();
                this.LstSpec.DataBind();
            }
        }
        protected void EBtnBacthSet_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.LstSpec.Items.Count; i++)
            {
                if (this.LstSpec.Items[i].Selected)
                {
                    int specid = DataConverter.CLng(this.LstSpec.Items[i].Value);
                    M_Spec info = this.bll.GetSpec(specid);
                    if (this.ChkOpenType.Checked)
                    {
                        info.OpenNew = DataConverter.CBool(this.RBLOpenType.SelectedValue);
                    }
                    if (this.ChkTemp.Checked && (!string.IsNullOrEmpty(this.TxtTemplate.Text.Trim())))
                    {
                        info.ListTemplate=this.TxtTemplate.Text;
                    }
                    this.bll.UpdateSpec(info);
                }
            }
            function.WriteSuccessMsg("批量设置成功", "../Content/SpecialManage.aspx");
        }
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SpecialManage.aspx");
        }
}
}