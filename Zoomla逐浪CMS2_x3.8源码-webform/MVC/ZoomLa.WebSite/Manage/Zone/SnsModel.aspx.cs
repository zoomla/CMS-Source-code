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
using ZoomLa.Web;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Zone
{
    public partial class SnsModel : CustomerPageAction
    {
        private B_Model bll = new B_Model();
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li class='active'>学校会员模型管理</li>");
            if (!this.Page.IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.model, "ShopModelManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                this.DrpItemIcon.Attributes.Add("onchange", "ChangeImgItemIcon(this.value);ChangeTxtItemIcon(this.value);");
                this.TxtItemIcon.Attributes.Add("onchange", "ChangeImgItemIcon(this.value);");
                string id = base.Request.QueryString["ModelID"];
                if (!string.IsNullOrEmpty(id))
                {
                    this.HdnModelId.Value = id;
                    this.LTitle.Text = "修改商品模型";
                    M_ModelInfo info = this.bll.GetModelById(int.Parse(id));
                    this.TxtModelName.Text = info.ModelName;
                    this.LblTablePrefix.Visible = false;
                    this.TxtTableName.Text = info.TableName;
                    this.TxtTableName.Enabled = false;
                    this.TxtItemName.Text = info.ItemName;
                    this.TxtItemUnit.Text = info.ItemUnit;
                    string selectValue = string.IsNullOrEmpty(info.ItemIcon) ? "Default.gif" : info.ItemIcon;
                    this.ImgItemIcon.ImageUrl = "~/Images/ModelIcon/" + selectValue;
                    this.DrpItemIcon.SelectedValue = selectValue;
                    this.TxtItemIcon.Text = info.ItemIcon;
                    this.TxtDescription.Text = info.Description;
                }
                else
                {
                    this.HdnModelId.Value = "0";
                }
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                M_ModelInfo info = new M_ModelInfo();
                info.ModelID = DataConverter.CLng(this.HdnModelId.Value);
                info.ModelName = this.TxtModelName.Text;
                if (info.ModelID > 0)
                {
                    info.TableName = this.TxtTableName.Text;
                }
                else
                {
                    info.TableName = this.LblTablePrefix.Text + this.TxtTableName.Text;
                }
                info.ItemName = this.TxtItemName.Text;
                info.ItemUnit = this.TxtItemUnit.Text;
                info.ItemIcon = this.TxtItemIcon.Text;
                info.Description = this.TxtDescription.Text;
                info.ModelType = 2;
                if (info.ModelID == 0)
                {
                    if (this.bll.AddModel(info))
                        Response.Redirect("SnsStudentModel.aspx");
                }
                else
                {
                    if (this.bll.UpdateModel(info))
                        Response.Redirect("SnsStudentModel.aspx");
                }
            }
        }

    }
}