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

namespace ZoomLaCMS.Manage.Content
{
    public partial class AddEditModel : CustomerPageAction
    {
        public B_Model bll = new B_Model();
        B_ModelField bf = new B_ModelField();
        B_Pub pll = new B_Pub();
        B_Admin badmin = new B_Admin();
        public int ModelID { get { return DataConverter.CLng(Request.QueryString["ModelID"]); } }
        public int ModelType { get { return DataConverter.CLng(Request.QueryString["ModelType"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                string action = Request.Form["action"];
                string result = "";
                switch (action)
                {
                    case "checkname":
                        if (bll.isExistTableName("ZL_C_" + Request.Form["tbname"]))
                            result = "-1";
                        else
                            result = "0";
                        break;
                    default:
                        break;
                }
                Response.Write(result); Response.Flush(); Response.End();
                return;
            }
            if (!IsPostBack)
            {
                B_ARoleAuth.CheckEx(ZLEnum.Auth.model, "ModelEdit");
                switch (ModelType)
                {
                    //内容模型
                    case 1:
                        this.LblTablePrefix.Text = "ZL_C_";
                        break;
                    //商城模型
                    case 2:
                        this.LblTablePrefix.Text = "ZL_P_";
                        break;
                    //用户模型
                    case 3:
                        this.RBLMultiFlag1.Visible = true;
                        this.LblTablePrefix.Text = "ZL_U_";
                        break;
                    //黄页内容模型管理
                    case 4:
                        this.LblTablePrefix.Text = "ZL_Page_";
                        break;
                    //店铺模型
                    case 5:
                        this.LblTablePrefix.Text = "ZL_S_";
                        break;
                    //店铺申请设置
                    case 6:
                        this.LblTablePrefix.Text = "ZL_Store_";
                        break;
                    //互动模型管理
                    case 7:
                        this.PubType1.Visible = true;
                        this.LblTablePrefix.Text = "ZL_Pub_";
                        break;
                    //功能模型
                    case 8:
                        this.LblTablePrefix.Text = "ZL_C_";
                        break;
                    case 11://CRM模型
                        LblTablePrefix.Text = "ZL_CRM_";
                        break;
                    case 12:
                        this.LblTablePrefix.Text = "ZL_OAC_";
                        break;
                }
                if (ModelID > 0)
                {
                    Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ModelManage.aspx?ModelType=" + Request.QueryString["ModelType"] + "'>模型管理</a></li><li class='active'>修改" + bll.GetModelType(Convert.ToInt32(Request["ModelType"])) + "</li>");
                    this.LTitle.Text = "修改" + bll.GetModelType(Convert.ToInt32(Request["ModelType"]));
                    M_ModelInfo info = this.bll.GetModelById(ModelID);
                    this.TxtModelName.Text = info.ModelName;
                    this.LblTablePrefix.Visible = false;
                    this.TxtTableName.Text = info.TableName;
                    this.TxtTableName.Enabled = false;
                    this.TxtItemName.Text = info.ItemName;
                    this.rblCopy.Checked = info.SysModel == 1 ? true : false;
                    rblIslotsize.Checked = info.Islotsize;
                    FileFactory.Checked = info.Thumbnail == "1" ? true : false;
                    this.TxtItemUnit.Text = info.ItemUnit;
                    string selectValue = string.IsNullOrEmpty(info.ItemIcon) ? "Default.gif" : info.ItemIcon;
                    this.TxtItemIcon.Text = info.ItemIcon;
                    this.TxtDescription.Text = info.Description;
                    switch (Request["ModelType"])
                    {
                        case "3":
                            this.RBLMultiFlag.SelectedValue = info.MultiFlag ? "1" : "0";
                            break;
                        case "7":
                            break;
                    }
                }
                else
                {
                    Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='ModelManage.aspx?ModelType=" + ModelType + "'>模型管理</a></li><li class='active'>添加" + bll.GetModelType(ModelType) + "</li>");
                    this.LTitle.Text = "添加" + bll.GetModelType(ModelType);
                }
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.TxtTableName.Text))
            {
                string str = this.TxtTableName.Text;
                bool b = this.bll.isExistTableName(LblTablePrefix.Text + str);
                if (b)
                {
                    function.WriteErrMsg("数据库表名已存在,请重新输入！", CustomerPageAction.customPath + "/Content/AddEditModel.aspx");
                    return;
                }
            }
            M_ModelInfo info = new M_ModelInfo();
            if (ModelID < 1)
            {
                info.ModelID = 0;
                info.TableName = this.LblTablePrefix.Text + this.TxtTableName.Text;
            }
            else
            {
                info = bll.SelReturnModel(ModelID);
                info.TableName = this.TxtTableName.Text;
            }
            info.ModelName = this.TxtModelName.Text;
            info.ItemName = this.TxtItemName.Text;
            info.ItemUnit = this.TxtItemUnit.Text;
            info.ItemIcon = this.TxtItemIcon.Text;
            info.Description = this.TxtDescription.Text;
            info.ModelType = ModelType;
            info.MultiFlag = true;
            info.SysModel = rblCopy.Checked ? 1 : 0;
            info.Islotsize = rblIslotsize.Checked;
            info.Thumbnail = FileFactory.Checked ? "1" : "0";
            if (info.ModelID < 1)
            {
                info.FromModel = 0;
                switch (ModelType.ToString())
                {
                    case "3"://用户模型和黄页申请模型
                        info.MultiFlag = DataConverter.CBool(this.RBLMultiFlag.SelectedValue);
                        this.bll.AddUserModel(info);
                        break;
                    case "6"://店铺申请模型
                        this.bll.AddStoreModel(info);
                        break;
                    case "7"://互动模型
                        M_Pub pubmodel = new M_Pub();
                        pubmodel.PubName = info.ModelName;
                        pubmodel.PubTableName = info.TableName;
                        pubmodel.PubType = DataConverter.CLng(this.PubType.SelectedValue);
                        pll.CreateModelInfo(pubmodel);
                        break;
                    case "8"://功能模型
                        this.bll.AddFunModel(info);
                        break;
                    case "12"://OA办公模型
                              //this.bll.AddModel(info);//基于内容模型,增加自定义字段
                              //bf.AddModelField(info, "content", "内容", "MultipleHtmlType","text");
                              //string[] fieldArr = { "Secret:机密", "Urgency:紧急程度", "Importance:重要性", "Attach:附件", "UserGroupT:所属部门" };
                              //for (int i = 0; i < fieldArr.Length; i++)
                              //{
                              //    bf.AddModelField(info, fieldArr[i].Split(':')[0], fieldArr[i].Split(':')[1], "TextType", "nvarchar");
                              //}
                        break;
                    default://内容模型、商城模型、黄页内容模型、店铺商品模型
                        CreateDataLabel(info);
                        this.bll.AddModel(info);
                        break;
                }
                Response.Redirect("ModelManage.aspx?ModelType=" + ModelType);
            }
            else
            {
                switch (ModelType.ToString())
                {
                    case "3":
                        info.MultiFlag = DataConverter.CBool(this.RBLMultiFlag.SelectedValue);
                        break;
                    case "7":
                        break;
                }
                this.bll.UpdateModel(info);
                Response.Redirect("ModelManage.aspx?ModelType=" + ModelType);
            }
        }
        private void CreateDataLabel(M_ModelInfo info)
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
            //blab.AddLabel(lab);
            blab.AddLabelXML(lab);
            //创建空白内容页模板并绑定到模型
            string fname = "默认" + info.ModelName + "内容页.html";
            info.ContentModule = "内容页/" + fname;
            string vpath = SiteConfig.SiteOption.TemplateDir + "/内容页/" + fname;
            string strcontent = @"{ZL.Source id=""[ZL_Modelname/]"" /}
<!DOCTYPEHTML>
<html>
<head>
<meta http-equiv=""Content-Type"" content=""text/html;charset=utf-8""/>
<title>{SField sid=""[ZL_Modelname/]"" FD=""Title"" page=""0""/}</title>
{ZL:Boot()/}
<meta name=""Keywords"" content=""{SField sid=""[ZL_Modelname/]"" FD=""Tagkey"" page=""0"" /}"">
<style>
.container-fulid{
height:50vh;padding-top:10%;background:#ccc;text-align:center;}
span{font-size:10em;}
</style>
</head>
<body>
<div class=""container-fulid"">
<div class=""row"">
<span class=""fa fa-info-circle""></span><h1>模板内容放这里！</h1><a href=""/hlep.html"" target=""_blank"">快速帮助></a>
</div>
</div>
</body>
</html>";
            strcontent = strcontent.Replace("[ZL_Modelname/]", info.ModelName + "数据源标签");
            SafeSC.WriteFile(vpath, strcontent);
        }
    }
}