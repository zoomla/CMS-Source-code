namespace ZoomLa.WebSite.Manage.Page
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
    using System.Text.RegularExpressions;


    public partial class UpdatePageField : CustomerPageAction
    {
        private B_ModelField bll = new B_ModelField();
        private B_Model bModel = new B_Model();
        private string m_mdid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                if (!B_ARoleAuth.Check(ZLEnum.Auth.model, "ModelEdit"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                if (string.IsNullOrEmpty(base.Request.QueryString["FieldID"]))
                {
                    function.WriteErrMsg("没有指定要修改字段的字段ID!");
                }
                int FieldID = DataConverter.CLng(base.Request.QueryString["FieldID"]);
                int ModelID = DataConverter.CLng(base.Request.QueryString["ModelID"]);

                this.HdfFieldID.Value = FieldID.ToString();
                M_ModelField field = this.bll.GetModelByID(ModelID.ToString(),FieldID);
                //int ModelID = field.ModelID;
                this.m_mdid = ModelID.ToString();
                M_ModelInfo model = this.bModel.GetModelById(ModelID);
                this.Literal1.Text = model.ModelName;
                Label1.Text = "当前表:" + model.TableName;
                this.HdfModelID.Value = ModelID.ToString();
                this.GradeOptionType_Cate.DataSource = B_GradeOption.GetCateList();
                this.GradeOptionType_Cate.DataTextField = "CateName";
                this.GradeOptionType_Cate.DataValueField = "CateID";
                this.GradeOptionType_Cate.DataBind();
                this.GradeOptionType_Cate.Items.Insert(0, new ListItem("选择多级选项分类", "0"));
                this.GetShow(field);
                Call.SetBreadCrumb(Master, "<li>后台管理</li><li>企业黄页</li><li><a href='/manage/Page/PageModelManage.aspx'>模型管理</a></li><li>修改[" + model.ModelName + "模型]字段</li>");
            }
        }
        public string MID
        {
            get { return this.m_mdid; }
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PageField.aspx?ModelID=26");
        }
        private void GetShow(M_ModelField field)
        {
            this.Name.Text = field.FieldName;
            this.Name.Enabled = false;
            this.Alias.Text = field.FieldAlias;
            this.Description.Text = field.Description;
            this.Tips.Text = field.FieldTips;
            this.IsNotNull.SelectedValue = field.IsNotNull.ToString();
            this.IsSearchForm.SelectedValue = field.IsSearchForm.ToString();
            this.IsShow.SelectedValue = field.IsShow.ToString();
            this.rblCopy.SelectedValue = field.IsCopy.ToString();
            this.SetDownFiled.SelectedValue = field.IsDownField.ToString();
            if (this.SetDownFiled.SelectedValue == "1")
            {
                this.downserver.Visible = true;
                Serverlist();
                this.serverlist.SelectedValue = field.DownServerID.ToString();
            }
            else
            {
                this.downserver.Visible = false;
            }

            string type = field.FieldType;
            string content = field.Content;
            this.Type.SelectedValue = type;
            this.Type.Enabled = false;
            this.hdfOrder.Value = field.OrderID.ToString();
            switch (type)
            {
                //单行文本
                case "TextType":
                    this.TitleSize.Text = this.bll.GetFieldContent(content, 0, 1);
                    this.IsPassword.SelectedValue = this.bll.GetFieldContent(content, 1, 1);
                    this.TextType_DefaultValue.Text = this.bll.GetFieldContent(content, 2, 1);
                    //this.TextSearchType.SelectedValue = this.bll.GetFieldContent(content, 3, 1);
                    break;
                //多行文本(不支持Html)
                case "MultipleTextType":
                    this.MultipleTextType_Width.Text = this.bll.GetFieldContent(content, 0, 1);
                    this.MultipleTextType_Height.Text = this.bll.GetFieldContent(content, 1, 1);
                    this.DivMultipleTextType.Style["display"] = "";
                    this.DivTextType.Style["display"] = "none";
                    break;
                //多行文本(支持Html)
                case "MultipleHtmlType":
                    this.MultipleHtmlType_Width.Text = this.bll.GetFieldContent(content, 0, 1);
                    this.MultipleHtmlType_Height.Text = this.bll.GetFieldContent(content, 1, 1);
                    this.IsEditor.SelectedValue = this.bll.GetFieldContent(content, 2, 1);
                    this.DivMultipleHtmlType.Style["display"] = "";
                    this.DivTextType.Style["display"] = "none";
                    break;
                //单选项
                case "OptionType":
                    this.RadioType_Content.Text = SortStr(this.bll.GetFieldContent(content, 0, 1)).Replace("||", "\r\n");
                    this.RadioType_Type.SelectedValue = this.bll.GetFieldContent(content, 0, 0);
                    this.RadioType_Property.SelectedValue = this.bll.GetFieldContent(content, 1, 1).ToString();
                    this.RadioType_Default.Text = this.bll.GetFieldContent(content, 2, 1).ToString();
                    this.DivOptionType.Style["display"] = "";
                    this.DivTextType.Style["display"] = "none";
                    break;
                //多选项
                case "ListBoxType":
                    this.ListBoxType_Content.Text = SortStr(this.bll.GetFieldContent(content, 0, 1)).Replace("||", "\r\n");
                    this.ListBoxType_Type.SelectedValue = this.bll.GetFieldContent(content, 0, 0);
                    this.DivListBoxType.Style["display"] = "";
                    this.DivTextType.Style["display"] = "none";
                    break;
                //数字
                case "NumType":
                    this.NumberType_TitleSize.Text = this.bll.GetFieldContent(content, 0, 1);
                    this.NumberType_Style.SelectedValue = this.bll.GetFieldContent(content, 1, 1);
                    this.NumberType_Style.Enabled = false;
                    this.NumberType_DefaultValue.Text = this.bll.GetFieldContent(content, 2, 1);
                    this.NumSearchType.SelectedValue = this.bll.GetFieldContent(content, 3, 1);
                    this.NumRangType.SelectedValue = this.bll.GetFieldContent(content, 4, 1);
                    this.NumSearchRange.Text = this.bll.GetFieldContent(content, 5, 1).Replace("|", "\r\n");
                    function.Script(this,"window.onload=function(){document.getElementById('showdec').style.display = '';}");
                    try
                    {
                        if (this.bll.GetFieldContent(content, 6, 1) == "")
                        {
                            this.txtdecimal.Text = "0";
                        }
                        else
                        {
                            this.txtdecimal.Text = this.bll.GetFieldContent(content, 6, 1);
                        }
                    }
                    catch 
                    {
                        this.txtdecimal.Text = "0";
                    }
                    this.DivNumType.Style["display"] = "";
                    this.DivTextType.Style["display"] = "none";
                    break;
                //日期时间
                case "DateType":
                    this.DateSearchType.SelectedValue = this.bll.GetFieldContent(content, 0, 1);
                    this.DateSearchRang.Text = this.bll.GetFieldContent(content, 1, 1).Replace("$", "\r\n");
                    this.DateSearchUnit.SelectedValue = this.bll.GetFieldContent(content, 2, 1);
                    this.DivDateType.Style["display"] = "";
                    this.DivTextType.Style["display"] = "none";
                    break;
                //图片
                case "PicType":
                    this.RBLPicWaterMark.SelectedValue = this.bll.GetFieldContent(content, 0, 1);
                    this.TxtSPicSize.Text = this.bll.GetFieldContent(content, 1, 1);
                    this.TxtPicExt.Text = this.bll.GetFieldContent(content, 2, 1);
                    this.DivPicType.Style["display"] = "";
                    this.DivTextType.Style["display"] = "none";
                    this.tbSearch.Style["display"] = "none";
                    break;
                //多图片
                case "MultiPicType":
                    if (DataConverter.CBool(this.bll.GetFieldContent(content, 0, 1)))
                    {
                        this.ChkThumb.Checked = true;
                        this.TxtThumb.Text = this.bll.GetFieldContent(content, 1, 1);
                    }
                    else
                    {
                        this.ChkThumb.Checked = false;
                        this.TxtThumb.Text = "";
                    }
                    this.ChkThumb.Enabled = false;
                    this.TxtThumb.Enabled = false;
                    this.RBLWaterMark.SelectedValue = this.bll.GetFieldContent(content, 2, 1);
                    this.TxtPicSize.Text = this.bll.GetFieldContent(content, 3, 1);
                    this.TextImageType.Text = this.bll.GetFieldContent(content, 4, 1);
                    this.DivMultiPicType.Style["display"] = "";
                    this.DivTextType.Style["display"] = "none";
                    this.tbSearch.Style["display"] = "none";
                    break;
                //文件
                case "SmallFileType":
                    this.ChkFileSize.Enabled = false;
                    this.TxtFileSizeField.Enabled = false;
                    this.TxtMaxFileSizes.Text = this.bll.GetFieldContent(content, 0, 1);
                    this.TxtUploadFileTypes.Text = this.bll.GetFieldContent(content, 1, 1);
                    this.DivSmallFileType.Style["display"] = "";
                    this.tbSearch.Style["display"] = "none";
                    this.DivTextType.Style["display"] = "none";
                    break;
                //多文件
                case "FileType":
                    if (DataConverter.CBool(this.bll.GetFieldContent(content, 0, 1)))
                    {
                        this.ChkFileSize.Checked = true;
                        this.TxtFileSizeField.Text = this.bll.GetFieldContent(content, 1, 1);
                    }
                    else
                    {
                        this.ChkFileSize.Checked = false;
                        this.TxtFileSizeField.Text = "";
                    }
                    this.ChkFileSize.Enabled = false;
                    this.TxtFileSizeField.Enabled = false;
                    this.TxtMaxFileSize.Text = this.bll.GetFieldContent(content, 2, 1);
                    this.TxtUploadFileType.Text = this.bll.GetFieldContent(content, 3, 1);
                    this.DivFileType.Style["display"] = "";
                    this.tbSearch.Style["display"] = "none";
                    this.DivTextType.Style["display"] = "none";
                    break;

                //运行平台
                case "OperatingType":
                    this.TxtOperatingOption.Text = this.bll.GetFieldContent(content, 1, 1).Replace("|", "\r\n");
                    this.OperatingType_TitleSize.Text = this.bll.GetFieldContent(content, 0, 1);
                    this.OperatingType_DefaultValue.Text = this.bll.GetFieldContent(content, 2, 1);
                    this.DivOperatingType.Style["display"] = "";
                    this.DivTextType.Style["display"] = "none";

                    break;
                //超链接
                case "SuperLinkType":
                    this.SuperLinkType_TitleSize.Text = this.bll.GetFieldContent(content, 0, 1);
                    this.SuperLinkType_DefaultValue.Text = this.bll.GetFieldContent(content, 1, 1);
                    this.DivSuperLinkType.Style["display"] = "";
                    this.DivTextType.Style["display"] = "none";
                    this.tbSearch.Style["display"] = "none";
                    break;
                //多级选项
                case "GradeOptionType":
                    this.GradeOptionType_Cate.SelectedValue = this.bll.GetFieldContent(content, 0, 1);
                    this.GradeOptionType_Direction.SelectedValue = this.bll.GetFieldContent(content, 1, 1);
                    this.DivTextType.Style["display"] = "none";
                    this.DivGradeOptionType.Style["display"] = "";
                    break;
                //颜色字段
                case "ColorType":
                    this.ColorDefault.Text = this.bll.GetFieldContent(content, 1, 1);
                    this.DivColorType.Style["display"] = "";
                    this.DivTextType.Style["display"] = "none";
                    this.tbSearch.Style["display"] = "none";
                    break;
            }
        }

        private string SortStr(string str)
        {
            string context = "";
            string[] strx = str.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in strx)
            {
                if (s.IndexOf("|") > 0)
                {
                    string[] ss = s.Split(new char[] { '|' });
                    if (ss.Length > 2)
                        context += SortStr(s.Replace("|", "||"));
                    else
                        context += s;
                }
                else
                {
                    context += s + "|" + s;
                }
                context += "||";
            }
            if (context.EndsWith("||"))
            {
                context = context.Remove(context.Length - 2);
            }
            return context;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            this.GetIsOk();
            int ModelID = DataConverter.CLng(this.HdfModelID.Value);
            int FieldID = DataConverter.CLng(this.HdfFieldID.Value);
            string text = this.Name.Text;
            string str2 = this.Alias.Text;
            string str3 = this.Description.Text;
            bool flag = DataConverter.CBool(this.IsNotNull.SelectedValue);
            bool flag2 = DataConverter.CBool(this.IsSearchForm.SelectedValue);
            string selectedValue = this.Type.SelectedValue;
            string str7 = "";

            switch (selectedValue)
            {
                //单行文本
                case "TextType":
                    str7 = "TitleSize=" + this.TitleSize.Text + ",IsPassword=" + this.IsPassword.Text + ",DefaultValue=" + this.TextType_DefaultValue.Text + "";
                    //str7 = str7 + ",TextSearchType=" + this.TextSearchType.SelectedValue;
                    break;
                //多行文本(不支持Html)
                case "MultipleTextType":
                    str7 = "Width=" + this.MultipleTextType_Width.Text + ",Height=" + this.MultipleTextType_Height.Text + "";
                    break;
                //多行文本(支持Html)
                case "MultipleHtmlType":
                    str7 = "Width=" + this.MultipleHtmlType_Width.Text + ",Height=" + this.MultipleHtmlType_Height.Text + ",IsEditor=" + this.IsEditor.SelectedValue + "";
                    break;
                //单选项
                case "OptionType":
                    str7 = "" + this.RadioType_Type.SelectedValue + "=" + this.RadioType_Content.Text.Trim().Replace(" ", "").Replace("\r\n", "||").Replace("|||", "||") + ",Property=" + this.RadioType_Property.Text + ",Default=" + this.RadioType_Default.Text + "";
                    break;
                //多选项
                case "ListBoxType":
                    if (this.ListBoxType_Type.SelectedValue == "3")
                    {
                        string r = @"^*\|*\$0$";
                        str7 = Regex.Replace(this.ListBoxType_Content.Text, "(\\r\\n)+", "^");
                        string[] s = str7.Split(new string[] { "^" }, StringSplitOptions.RemoveEmptyEntries);
                        str7 = "" + this.ListBoxType_Type.SelectedValue + "=";
                        for (int i = 0; i < s.Length; i++)
                        {
                            if (!(Regex.IsMatch(s[i], r)))
                            {
                                str7 += s[i].Trim() + "|" + s[i].Trim() + "$0";
                                if (i < s.Length - 1)
                                    str7 += "||";
                            }
                            else
                            {
                                str7 += s[i];
                                if (i < s.Length - 1)
                                    str7 += "||";
                            }
                        }
                    }
                    else
                    {
                        str7 = "" + this.ListBoxType_Type.SelectedValue + "=" + this.ListBoxType_Content.Text.Trim().Replace(" ", "").Replace("\r\n", "||").Replace("|||", "||") + "";
                    }
                    break;
                //数字
                case "NumType":
                    str7 = "TitleSize=" + this.NumberType_TitleSize.Text + ",NumberType=" + this.NumberType_Style.SelectedValue + ",DefaultValue=" + this.NumberType_DefaultValue.Text + "";
                    str7 = str7 + ",NumSearchType=" + this.NumSearchType.SelectedValue + ",NumRangType=" + this.NumRangType.SelectedValue + ",NumSearchRang=" + this.NumSearchRange.Text.Trim().Replace(" ", "").Replace("\r\n", "$") + ",NumLenght=" + this.txtdecimal.Text;
                    break;
                //日期时间
                case "DateType":
                    str7 = "DateSearchType=" + this.DateSearchType.SelectedValue + ",DateSearchRang=" + this.DateSearchRang.Text.Trim().Replace(" ", "").Replace("\r\n", "$");
                    str7 = str7 + ",DateSearchUnit=" + this.DateSearchUnit.SelectedValue;
                    break;
                //图片
                case "PicType":
                    str7 = "Warter=" + this.RBLPicWaterMark.SelectedValue + ",MaxPicSize=" + this.TxtSPicSize.Text + ",PicFileExt=" + this.TxtPicExt.Text;
                    flag2 = false;
                    break;
                //多图片
                case "MultiPicType":
                    str7 = "ChkThumb=" + (this.ChkThumb.Checked ? "1" : "0") + ",ThumbField=" + this.TxtThumb.Text + ",Warter=" + this.RBLPicWaterMark.SelectedValue + ",MaxPicSize=" + this.TxtSPicSize.Text + ",PicFileExt=" + this.TextImageType.Text;
                    flag2 = false;
                    break;
                //文件
                case "SmallFileType":
                    str7 = "MaxFileSize=" + this.TxtMaxFileSizes.Text + ",UploadFileExt=" + this.TxtUploadFileTypes.Text;
                    flag2 = false;
                    break;
                //多文件
                case "FileType":
                    str7 = "ChkFileSize=" + (this.ChkFileSize.Checked ? "1" : "0") + ",FileSizeField=" + this.TxtFileSizeField.Text + ",MaxFileSize=" + this.TxtMaxFileSize.Text + ",UploadFileExt=" + this.TxtUploadFileType.Text;
                    flag2 = false;
                    break;
                //运行平台
                case "OperatingType":
                    str7 = "TitleSize=" + this.OperatingType_TitleSize.Text + ",OperatingList=" + this.TxtOperatingOption.Text.Trim().Replace(" ", "").Replace("\r\n", "|") + ",DefaultValue=" + this.OperatingType_DefaultValue.Text;
                    break;
                //超链接
                case "SuperLinkType":
                    str7 = "TitleSize=" + this.SuperLinkType_TitleSize.Text + ",DefaultValue=" + this.SuperLinkType_DefaultValue.Text;
                    flag2 = false;
                    break;
                case "GradeOptionType":
                    str7 = "GradeCate=" + this.GradeOptionType_Cate.SelectedValue + ",Direction=" + this.GradeOptionType_Direction.SelectedValue;
                    break;
                //颜色字段
                case "ColorType":
                    str7 = "TitleSize=" + this.SuperLinkType_TitleSize.Text + ",DefaultValue=" + this.ColorDefault.Text;
                    flag2 = false;
                    break;
                default:
                    str7 = "";
                    break;
            }
            M_ModelField modelfield = new M_ModelField();
            modelfield.ModelID = ModelID;
            modelfield.FieldID = FieldID;
            modelfield.FieldName = text;
            modelfield.FieldAlias = str2;
            modelfield.Description = str3;
            modelfield.FieldTips = this.Tips.Text.Trim();
            modelfield.FieldType = selectedValue;
            modelfield.Content = str7;
            modelfield.IsNotNull = flag;
            modelfield.IsSearchForm = flag2;
            modelfield.IsShow = DataConverter.CBool(Request.Form["IsShow"]);
            modelfield.ShowList = true;
            modelfield.ShowWidth = 0;
            modelfield.IsView = true;
            modelfield.IsDownField = DataConverter.CLng(this.SetDownFiled.SelectedValue);
            modelfield.DownServerID = DataConverter.CLng(this.serverlist.SelectedValue);

            //modelfield.OrderID = DataConverter.CLng(this.hdfOrder.Value);
            modelfield.IsCopy = DataConverter.CLng(this.rblCopy.SelectedValue);
            this.bll.Update(modelfield);
            //this.bll.ModelFieldHtml(ModelID);
            function.WriteSuccessMsg("操作成功!", "PageField.aspx?ModelID=" + ModelID.ToString());
            //Response.Redirect("ModelField.aspx?ModelID=" + ModelID.ToString());
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
            if (str2 == "")
            {
                function.WriteErrMsg(string.Concat(new object[] { "<li>字段别名不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
            }

            if (this.SetDownFiled.SelectedValue == "1")
            {
                if (this.serverlist.SelectedValue == "")
                {
                    function.WriteErrMsg(string.Concat(new object[] { "<li>关联下载服务器不能为空!</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                }
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
            }
            if ((selectedValue == "OptionType") && (this.RadioType_Content.Text == ""))
            {
                function.WriteErrMsg(string.Concat(new object[] { "<li>单选项的选项不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
            }
            if ((selectedValue == "ListBoxType") && (this.ListBoxType_Content.Text == ""))
            {
                function.WriteErrMsg(string.Concat(new object[] { "<li>多选项的选项不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
            }
            if (selectedValue == "NumType")
            {
                if (this.NumberType_TitleSize.Text == "")
                {
                    function.WriteErrMsg(string.Concat(new object[] { "<li>输入数字的文本框长度不能为空</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                }
                if (!DataValidator.IsNumber(this.NumberType_TitleSize.Text))
                {
                    function.WriteErrMsg(string.Concat(new object[] { "<li>输入数字的文本框长度必须是整形数字</li><li><a href='javascript:window.history.back(-1)'>返回</a></li><li><a href='../Content/AddModelField.aspx?ModelID=", this.HdfModelID.Value, "'>重新添加字段</a> <a href='../Content/ModelField.aspx?ModelID=", this.HdfModelID.Value, "'>返回字段列表</a> <a href='../Content/ModelManage.aspx'>模型管理</a></li>" }));
                }
            }
        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Serverlist();
            function.Script(Page, "SelectModelType();");
        }

        private void Serverlist()
        {
            if (this.SetDownFiled.SelectedValue == "1")
            {
                this.downserver.Visible = true;
                B_DownServer downdll = new B_DownServer();
                DataTable downlist = downdll.GetDownServerAll();
                downlist.DefaultView.Sort = "ServerID";
                this.serverlist.DataSource = downlist;
                this.serverlist.DataTextField = "ServerName";
                this.serverlist.DataValueField = "ServerID";
                this.serverlist.DataBind();
                this.serverlist.Items.Insert(0, new ListItem("请选择服务器", ""));
                for (int i = 0; i < this.Type.Items.Count; i++)
                {
                    if (this.Type.Items[i].Value != "SmallFileType")
                    {
                        this.Type.Items[i].Enabled = false;

                    }
                    else
                    {
                        int FieldID = DataConverter.CLng(base.Request.QueryString["FieldID"]);
                        int ModelID = DataConverter.CLng(base.Request.QueryString["ModelID"]);
                        M_ModelField field = this.bll.GetModelByID(ModelID.ToString(),FieldID);
                        string content = field.Content;
                        this.ChkFileSize.Enabled = false;
                        this.TxtFileSizeField.Enabled = false;
                        this.TxtMaxFileSizes.Text = this.bll.GetFieldContent(content, 0, 1);
                        this.TxtUploadFileTypes.Text = this.bll.GetFieldContent(content, 1, 1);
                        this.DivSmallFileType.Style["display"] = "";
                        this.tbSearch.Style["display"] = "none";
                        this.DivTextType.Style["display"] = "none";
                        this.Type.Items[i].Selected = true;
                    }
                }
            }
        }

    }
}