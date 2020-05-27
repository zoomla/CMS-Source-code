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
namespace ZoomLaCMS.Manage.Shop
{
    public partial class Model : CustomerPageAction
    {
        private B_Model bll = new B_Model();
        private B_ModelField bmf = new B_ModelField();
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();

                if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "ShopModelManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                foreach (DataRow row in FileSystemObject.GetDirectoryInfos(HttpContext.Current.Server.MapPath("~/Images/ModelIcon/"), FsoMethod.File).Rows)
                {
                    this.DrpItemIcon.Items.Add(new ListItem(row["name"].ToString(), row["name"].ToString()));
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
                Call.SetBreadCrumb(Master, "<li>商城管理</li><li><a href='ModelManage.aspx'>商品模型管理</a></li><li>添加商品模型</li>");
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                if (!String.IsNullOrEmpty(this.TxtTableName.Text))
                {
                    string str = this.TxtTableName.Text;
                    bool b = this.bll.isExistTableName("ZL_P_" + str);
                    if (b)
                    {
                        function.WriteErrMsg("数据库表名已存在,请重新输入！");
                        return;
                    }
                }
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
                info.MultiFlag = true;

                if (info.ModelID == 0)
                {
                    //创建数据源标签
                    M_Label lab = new M_Label();
                    lab.LableName = info.ModelName + "数据源标签";
                    lab.LabelCate = "数据源标签";
                    lab.LableType = 3;
                    lab.LabelTable = "ZL_Commodities left join " + info.TableName + " on ZL_Commodities.ItemID=" + info.TableName + ".ID";
                    lab.LabelField = "ZL_Commodities.*," + info.TableName + ".*";
                    lab.LabelWhere = "ZL_Commodities.ID=@InfoID";
                    lab.Param = "InfoID,0,2,商品ID";
                    lab.LabelOrder = "";
                    lab.LabelCount = "";
                    lab.Content = "";
                    lab.Desc = info.ModelName + "数据源标签";
                    lab.LabelNodeID = 0;
                    B_Label blab = new B_Label();
                    //blab.AddLabel(lab);
                    blab.AddLabelXML(lab);
                    //创建空白内容页模板并绑定到模型
                    string strPath = "默认" + info.ModelName + "内容页.html";
                    info.ContentModule = "/内容页/" + strPath;
                    SafeSC.WriteFile(SiteConfig.SiteOption.TemplateDir + "/内容页/" + strPath, info.ModelName + "内容页");
                    bool iscrate = this.bll.AddModel(info);
                    if (iscrate)
                    {
                        Response.Redirect("ModelManage.aspx");
                    }
                }
                else
                {
                    if (this.bll.UpdateModel(info))
                        Response.Redirect("ModelManage.aspx");
                }
            }
            Response.Redirect("ModelManage.aspx");
        }

    }
}