namespace ZoomLa.WebSite.Manage
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
    using ZoomLa.Web;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;

    public partial class AddModelField : System.Web.UI.Page
    {
        private B_ModelField bll = new B_ModelField();
        private B_Model bModel = new B_Model();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("ModelEdit"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                if (string.IsNullOrEmpty(base.Request.QueryString["ModelID"]))
                {
                    function.WriteErrMsg("没有指定要添加字段的模型ID!");
                }
                int ModelID = DataConverter.CLng(base.Request.QueryString["ModelID"]);
                this.HdfModelID.Value = ModelID.ToString();
                M_ModelInfo model = this.bModel.GetModelById(ModelID);
                this.lblModel.Text = model.ModelName;
                this.HdfTableName.Value = model.TableName;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int ModelID = DataConverter.CLng(this.HdfModelID.Value);
            this.GetIsOk();
            string text = this.Name.Text;
            string str2 = this.Alias.Text;
            string str3 = this.Description.Text;
            bool flag = DataConverter.CBool(this.IsNotNull.SelectedValue);
            bool flag2 = DataConverter.CBool(this.IsSearchForm.SelectedValue);
            string fieldType = "nvarchar";
            string defaultValue = "";
            string selectedValue = this.Type.SelectedValue;
            string str7 = "";
            string tempFieldName = "";
            string tempFieldType = "";
            switch (selectedValue)
            {
                //单行文本
                case "TextType":
                    str7 = "TitleSize=" + this.TitleSize.Text + ",IsPassword=" + this.IsPassword.Text + ",DefaultValue=" + this.TextType_DefaultValue.Text + "";
                    fieldType = "nvarchar";
                    break;
                //多行文本(不支持Html)
                case "MultipleTextType":
                    str7 = "Width=" + this.MultipleTextType_Width.Text + ",Height=" + this.MultipleTextType_Height.Text + "";
                    fieldType = "ntext";
                    break;
                //多行文本(支持Html)
                case "MultipleHtmlType":
                    str7 = "Width=" + this.MultipleHtmlType_Width.Text + ",Height=" + this.MultipleHtmlType_Height.Text + ",IsEditor=" + this.IsEditor.SelectedValue + "";
                    fieldType = "ntext";
                    break;
                //单选项
                case "OptionType":
                    str7 = "" + this.RadioType_Type.SelectedValue + "=" + this.RadioType_Content.Text.Trim().Replace(" ", "").Replace("\r\n", "|") + ",Property=" + this.RadioType_Property.Text + ",Default=" + this.RadioType_Default.Text + "";
                    fieldType = "nvarchar";
                    defaultValue = this.RadioType_Default.Text;
                    break;
                //多选项
                case "ListBoxType":
                    str7 = "" + this.ListBoxType_Type.SelectedValue + "=" + this.ListBoxType_Content.Text.Trim().Replace(" ", "").Replace("\r\n", "|") + "";
                    fieldType = "ntext";
                    break;
                //数字
                case "NumType":
                    str7 = "TitleSize=" + this.NumberType_TitleSize.Text + ",NumberType=" + this.NumberType_Style.SelectedValue + ",DefaultValue=" + this.NumberType_DefaultValue.Text + "";
                    int numstyle = DataConverter.CLng(this.NumberType_Style.SelectedValue);
                    if (numstyle == 1)
                        fieldType = "int";
                    if (numstyle == 2)
                        fieldType = "float";
                    if (numstyle == 3)
                        fieldType = "money";
                    break;
                //日期时间
                case "DateType":
                    str7 = "";
                    fieldType = "datetime";
                    break;
                //图片
                case "PicType":
                    str7 = "Warter=" + this.RBLPicWaterMark.SelectedValue + ",MaxPicSize=" + this.TxtSPicSize.Text + ",PicFileExt=" + this.TxtPicExt.Text;
                    fieldType = "nvarchar";
                    break;
                //多图片
                case "MultiPicType":
                    str7 = "ChkThumb=" + (this.ChkThumb.Checked ? "1" : "0") + ",ThumbField=" + this.TxtThumb.Text + ",Warter=" + this.RBLPicWaterMark.SelectedValue + ",MaxPicSize=" + this.TxtSPicSize.Text + ",PicFileExt=" + this.TextImageType.Text;
                    if (this.ChkThumb.Checked)
                    {
                        tempFieldName = this.TxtThumb.Text;
                        tempFieldType = "nvarchar";
                    }
                    fieldType = "ntext";
                    break;
                //文件
                case "FileType":
                    str7 = "ChkFileSize=" + (this.ChkFileSize.Checked ? "1" : "0") + ",FileSizeField=" + this.TxtFileSizeField.Text + ",MaxFileSize=" + this.TxtMaxFileSize.Text + ",UploadFileExt=" + this.TxtUploadFileType.Text;
                    if (this.ChkFileSize.Checked)
                    {
                        tempFieldName = this.TxtFileSizeField.Text;
                        tempFieldType = "nvarchar";
                    }
                    fieldType = "ntext";
                    break;
                //运行平台
                case "OperatingType":
                    str7 = "TitleSize=" + this.OperatingType_TitleSize.Text + ",OperatingList=" + this.TxtOperatingOption.Text.Trim().Replace(" ", "").Replace("\r\n", "|") + ",DefaultValue=" + this.OperatingType_DefaultValue.Text;
                    fieldType = "nvarchar";
                    break;
                //超链接
                case "SuperLinkType":
                    str7 = "TitleSize=" + this.SuperLinkType_TitleSize.Text + ",DefaultValue=" + this.SuperLinkType_DefaultValue.Text;
                    fieldType = "nvarchar";
                    break;
                default:
                    str7 = "";
                    break;
            }
            M_ModelField modelfield = new M_ModelField();
            modelfield.ModelID = ModelID;
            modelfield.FieldID = 0;
            modelfield.FieldName = text;
            modelfield.FieldAlias = str2;
            modelfield.Description = str3;
            modelfield.FieldTips = this.Tips.Text.Trim();
            modelfield.FieldType = selectedValue;
            modelfield.Content = str7;
            modelfield.IsNotNull = flag;
            modelfield.IsSearchForm = flag2;
            //modelfield.OrderID = this.bll.GetMaxOrder(ModelID)+1;

            this.bll.Add(modelfield);
            if (!string.IsNullOrEmpty(tempFieldName) && !string.IsNullOrEmpty(tempFieldType))
            {
                this.bll.AddField(this.HdfTableName.Value, tempFieldName, tempFieldType, "");
            }
            this.bll.AddField(this.HdfTableName.Value, text, fieldType, defaultValue);
            //this.bll.ModelFieldHtml(ModelID);
            Response.Redirect("ModelField.aspx?ModelID=" + ModelID.ToString());
        }
        private void GetIsOk()
        {
            string text = this.Name.Text;
            string str2 = this.Alias.Text;
            string selectedValue = this.Type.SelectedValue;
            if (text == "")
            {
                function.WriteErrMsg(string.Concat(new object[] { "<li>字段名不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
            }
            if (!DataValidator.IsValidUserName(text))
            {
                function.WriteErrMsg(string.Concat(new object[] { "<li>字段名必须由字母、数字、下划线组成</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
            }
            if (this.bll.IsExists(DataConverter.CLng(this.HdfModelID.Value), text))
            {
                function.WriteErrMsg(string.Concat(new object[] { "<li>该模型已有相同字段名的字段</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
            }
            if (str2 == "")
            {
                function.WriteErrMsg(string.Concat(new object[] { "<li>字段别名不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
            }
            switch (selectedValue)
            {
                case "TextType":
                    if (this.TitleSize.Text == "")
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>文本框长度不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    if (!DataValidator.IsNumber(this.TitleSize.Text))
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>文本框长度必须是整形数字</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    break;

                case "MultipleTextType":
                    if (this.MultipleTextType_Width.Text == "")
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>文本框显示的宽度不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    if (!DataValidator.IsNumber(this.MultipleTextType_Width.Text))
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>文本框显示的宽度必须是整形数字</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    if (this.MultipleTextType_Height.Text == "")
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>文本框显示的高度不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    if (!DataValidator.IsNumber(this.MultipleTextType_Height.Text))
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>文本框显示的高度必须是整形数字</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    break;

                case "MultipleHtmlType":
                    if (this.MultipleHtmlType_Width.Text == "")
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>文本框显示的宽度不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    if (!DataValidator.IsNumber(this.MultipleHtmlType_Width.Text))
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>文本框显示的宽度必须是整形数字</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    if (this.MultipleHtmlType_Height.Text == "")
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>文本框显示的高度不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    if (!DataValidator.IsNumber(this.MultipleHtmlType_Height.Text))
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>文本框显示的高度必须是整形数字</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    break;
                case "OptionType":
                    if (this.RadioType_Content.Text == "")
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>单选项的选项不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    break;
                case "ListBoxType":
                    if (this.ListBoxType_Content.Text == "")
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>多选项的选项不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    break;
                case "NumType":
                    if (this.NumberType_TitleSize.Text == "")
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>输入数字的文本框长度不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    if (!DataValidator.IsNumber(this.NumberType_TitleSize.Text))
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>输入数字的文本框长度必须是整形数字</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    break;
                case "PicType":
                    if (this.TxtSPicSize.Text == "")
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>图片文件大小限制不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    if (this.TxtPicExt.Text == "")
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>允许上传的图片类型不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    break;
                case "MultiPicType":
                    if (this.ChkThumb.Checked && this.TxtThumb.Text.Trim() == "")
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>选择保存缩略图时，图片缩略图保存字段名不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    if (this.TxtPicSize.Text == "")
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>图片文件大小限制不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    if (this.TextImageType.Text == "")
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>允许上传的图片类型不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    break;
                case "FileType":
                    if (this.ChkFileSize.Checked && this.TxtFileSizeField.Text == "")
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>选择保存文件大小时，文件大小保存字段名不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    if (this.TxtMaxFileSize.Text == "")
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>允许上传的文件大小限制不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    if (this.TxtUploadFileType.Text == "")
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>允许上传的文件类型不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    break;
                case "OperatingType":
                    if (this.OperatingType_TitleSize.Text == "")
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>输入运行平台的文本框长度不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    if (this.TxtOperatingOption.Text == "")
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>运行平台选项不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    break;
                case "SuperLinkType":
                    if (this.SuperLinkType_TitleSize.Text == "")
                    {
                        function.WriteErrMsg(string.Concat(new object[] { "<li>输入超链接的文本框长度不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                    }
                    break;
            }
        }
    }
}