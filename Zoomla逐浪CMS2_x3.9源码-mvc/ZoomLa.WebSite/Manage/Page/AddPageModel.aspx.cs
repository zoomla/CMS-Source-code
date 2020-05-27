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
using ZoomLa.Components;

namespace ZoomLaCMS.Manage.Page
{
    public partial class AddPageModel : CustomerPageAction
    {
        private B_Model bll = new B_Model();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                if (!B_ARoleAuth.Check(ZLEnum.Auth.model, "AddPageModel"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                this.TxtItemIcon.Attributes.Add("onchange", "ChangeImgItemIcon(this.value);");
                string id = base.Request.QueryString["ModelID"];
                if (!string.IsNullOrEmpty(id))
                {
                    this.HdnModelId.Value = id;
                    LNav_Hid.Value = "修改内容模型";
                    this.LTitle.Text = "修改内容模型";
                    M_ModelInfo info = this.bll.GetModelById(int.Parse(id));
                    this.TxtModelName.Text = info.ModelName;
                    this.LblTablePrefix.Visible = false;
                    this.TxtTableName.Text = info.TableName;
                    this.TxtTableName.Enabled = false;
                    this.TxtItemName.Text = info.ItemName;
                    this.TxtItemUnit.Text = info.ItemUnit;
                    string selectValue = string.IsNullOrEmpty(info.ItemIcon) ? "Default.gif" : info.ItemIcon;
                    this.TxtItemIcon.Text = info.ItemIcon;
                    this.TxtDescription.Text = info.Description;
                }
                else
                {
                    this.HdnModelId.Value = "0";
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='PageManage.aspx'>企业黄页</a></li><li><a href='ModelManage.aspx'>黄页模型管理</a></li><li>" + LNav_Hid.Value + "</li>");
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)//保存
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
                info.ModelType = 4;
                if (info.ModelID == 0)
                {
                    //创建数据源标签
                    M_Label lab = new M_Label();
                    lab.LableName = info.ModelName + "数据源标签";
                    lab.LabelCate = "数据源标签";
                    lab.LableType = 3;
                    lab.LabelTable = "ZL_CommonModel left join " + info.TableName + " on ZL_CommonModel.ItemID=" + info.TableName + ".ID";
                    lab.LabelField = "ZL_CommonModel.*," + info.TableName + ".*";
                    lab.LabelWhere = "ZL_CommonModel.GeneralID=@InfoID";
                    lab.Param = "InfoID,0,2,内容ID";
                    lab.LabelOrder = "";
                    lab.LabelCount = "";
                    lab.Content = "";
                    lab.Desc = info.ModelName + "数据源标签";
                    lab.LabelNodeID = 0;
                    B_Label blab = new B_Label();
                    blab.AddLabelXML(lab);
                    //创建空白内容页模板并绑定到模型
                    string strPath = "默认" + info.ModelName + "内容页.html";
                    SafeSC.WriteFile(SiteConfig.SiteOption.TemplateDir + "/内容页/" + strPath, info.ModelName + "内容页");
                    this.bll.AddModel(info);
                    Response.Redirect("../Content/ModelManage.aspx?ModelType=4");
                }
                else
                {
                    this.bll.UpdateModel(info);
                    Response.Redirect("../Content/ModelManage.aspx?ModelType=4");
                }
            }
        }
    }
}