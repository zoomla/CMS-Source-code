using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.FTP;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Manage_I_Content_AddModelField : CustomerPageAction
{
    B_ModelField bll = new B_ModelField();
    B_Model bModel = new B_Model();
    B_User buser = new B_User();
    B_UserBaseField ubfieldBll = new B_UserBaseField();
    public int FieldID { get { return DataConverter.CLng(Request.QueryString["FieldID"]); } }
    public int ModelID { get { return DataConverter.CLng(Request.QueryString["ModelID"]); } }
    public int ModelType { get { return DataConverter.CLng(Request.QueryString["ModelType"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.model, "ModelEdit");
            if (ModelID == 0) { function.WriteErrMsg("没有指定要添加字段的模型ID!"); }
            MyBind();
            Call.HideBread(Master);
        }
    }
    private void MyBind() 
    {
        FieldList_A.HRef = customPath2 + "Content/ModelField.aspx?ModelId=" + ModelID + "&ModelType=" + ModelType;
        GradeOptionType_Cate.DataSource = B_GradeOption.GetCateList();
        GradeOptionType_Cate.DataTextField = "CateName";
        GradeOptionType_Cate.DataValueField = "CateID";
        GradeOptionType_Cate.DataBind();
        GradeOptionType_Cate.Items.Insert(0, new ListItem("选择多级选项分类", "0"));
        GradeOptionType_Cate.Items.Insert(1, new ListItem("省市县三级联动", "a"));
        GradeOptionType_Cate.Items.Insert(1, new ListItem("省市县镇级四级联动", "b"));
        BindTypeRad();
        switch (ModelType)
        {
            case 9:
                if (FieldID > 0)
                {
                    lblModel.Text = "修改[用户注册]模型字段";
                    Button1.Text = "修改字段";
                    M_UserBaseField modelfield = ubfieldBll.GetSelect(FieldID);
                    M_ModelField fieldMod = new M_ModelField();
                    GetField(CopyModelField(modelfield, fieldMod));
                }
                else
                {
                    lblModel.Text = "添加[用户注册]模型字段";
                }
                FieldList_A.HRef = customPath2 + "User/SystemUserModel.aspx";
                CurTableName_L.Text = "ZL_UserBaseField";
                break;
            default:
                M_ModelInfo model = bModel.GetModelById(ModelID);
                if (!ZoomLa.SQLDAL.DBHelper.Table_IsExist(model.TableName)) { function.WriteErrMsg("模型表[" + model.TableName + "]不存在,无法进行字段操作"); }
                CurTableName_L.Text = model.TableName;
                if (FieldID > 0)
                {
                    lblModel.Text = "修改[" + model.ModelName + "]模型字段";
                    Button1.Text = "修改字段";
                    M_ModelField fieldMod = bll.GetModelByID(ModelID.ToString(), FieldID);
                    GetField(fieldMod);
                }
                else
                {
                    lblModel.Text = "添加[" + model.ModelName + "]模型字段";
                }
                break;
        }
        ModelType_L.Text = bll.GetModelType(ModelType);
    }
    //显示字段类型单选列表
    private void BindTypeRad()
    {
        ListItem li1 = new ListItem("单行文本", "TextType");
        ListItem li2 = new ListItem("多行文本(不支持Html)", "MultipleTextType");
        ListItem li3 = new ListItem("多行文本(支持Html)", "MultipleHtmlType");
        ListItem li4 = new ListItem("图片", "PicType");
        ListItem li5 = new ListItem("图片入库", "SqlType");
        ListItem li7 = new ListItem("单选项", "OptionType");
        ListItem li8 = new ListItem("多选项", "ListBoxType");
        ListItem li9 = new ListItem("多级选项", "GradeOptionType");
        ListItem li10 = new ListItem("文件", "SmallFileType");
        ListItem li11 = new ListItem("下拉文件", "PullFileType");
        ListItem li12 = new ListItem("多文件", "FileType");
        ListItem li13 = new ListItem("日期时间", "DateType");
        ListItem li14 = new ListItem("数字", "NumType");
        ListItem li15 = new ListItem("运行平台", "OperatingType");
        ListItem li16 = new ListItem("超链接", "SuperLinkType");
        ListItem li17 = new ListItem("颜色代码", "ColorType");
        ListItem li19 = new ListItem("递增双时间", "DoubleDateType");
        ListItem li20 = new ListItem("在线浏览", "Upload");
        ListItem li21 = new ListItem("手机短信", "MobileSMS");
        ListItem li22 = new ListItem("地图", "MapType");
        ListItem li23 = new ListItem("远程文件", "RemoteFile");
        ListItem li24 = new ListItem("图表", "Charts");
        ListItem li25 = new ListItem("文件入库", "SqlFile");
        ListItem li26 = new ListItem("库选定段", "TableField");
        ListItem li27 = new ListItem("随机数", "Random");
        ListItem li28 = new ListItem("智能图组", "Images");
        ListItem li29 = new ListItem("拍照字段", "CameraType");
        ListItem li30 = new ListItem("压图传入", "autothumb");
        ListItem li31 = new ListItem("微视频", "api_qq_mvs");
        Type_Rad.Items.Add(li1);
        Type_Rad.Items.Add(li2);
        Type_Rad.Items.Add(li3);
        Type_Rad.Items.Add(li4);
        Type_Rad.Items.Add(li28);

        Type_Rad.Items.Add(li5);
        Type_Rad.Items.Add(li7);
        Type_Rad.Items.Add(li8);
        Type_Rad.Items.Add(li9);
        Type_Rad.Items.Add(li25);

        Type_Rad.Items.Add(li29);
        Type_Rad.Items.Add(li10);
        Type_Rad.Items.Add(li11);
        Type_Rad.Items.Add(li12);
        Type_Rad.Items.Add(li13);

        Type_Rad.Items.Add(li14);
        Type_Rad.Items.Add(li15);
        Type_Rad.Items.Add(li16);
        Type_Rad.Items.Add(li17);
        Type_Rad.Items.Add(li20);

        Type_Rad.Items.Add(li21);
        Type_Rad.Items.Add(li22);
        Type_Rad.Items.Add(li26);
        Type_Rad.Items.Add(li27);
        Type_Rad.Items.Add(li30);

        Type_Rad.Items.Add(li31);
        li1.Selected = true;
    }
    //添加
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FieldID == 0)
        {
            GetIsOk_Add();
        }
        else
        {
            GetIsOk_Updata();
        }
        bool flag2 = IsSearchForm.Checked;
        string defaultValue = "", strContent = "", tempFieldName = "", tempFieldType = "", fieldType = "nvarchar";
        switch (Type_Rad.SelectedValue)
        {
            case "TextType": //单行文本
                strContent = "TitleSize=" + TitleSize.Text + ",IsPassword=" + (IsPassword.Checked ? "password" : "text") +
                    ",DefaultValue=" + TextType_DefaultValue.Text + ",SelVideo=" + Text_SelVideo_Chk.Checked + ",SelIcon=" + Text_SelIcon_Chk.Checked;
                fieldType = "nvarchar";
                break;
            case "MultipleTextType": //多行文本(不支持Html)
                strContent = "Width=" + MultipleTextType_Width.Text + ",Height=" + MultipleTextType_Height.Text + ",SelUser=" + MText_SelUser_Chk.Checked + ",Down=" + MText_Down_Chk.Checked;
                fieldType = "ntext";
                break;
            case "MultipleHtmlType": //多行文本(支持Html)
                strContent = "Width=" + MultipleHtmlType_Width.Text + ",Height=" + MultipleHtmlType_Height.Text + ",IsEditor=" + IsEditor.SelectedValue + ",AllowWord_Chk=" + AllowWord_Chk.Checked + ",Topimg=" + Topimg_Chk.Checked;
                fieldType = "ntext";
                break;
            case "OptionType"://单选项
                strContent = "" + RadioType_Type.SelectedValue + "=" + RadioType_Content.Text.Trim().Replace(" ", "").Replace("\r\n", "||") + ",Property=" + RadioType_Property.Checked + ",Default=" + RadioType_Default.Text + "";
                fieldType = "nvarchar";
                defaultValue = RadioType_Default.Text;
                break;
            case "ListBoxType": //多选项
                if (ListBoxType_Type.SelectedValue == "3")
                {
                    string r = @"^*\|*\$0$";
                    strContent = Regex.Replace(ListBoxType_Content.Text, "(\\r\\n)+", "^");
                    string[] s = strContent.Split(new string[] { "^" }, StringSplitOptions.RemoveEmptyEntries);
                    strContent = "" + ListBoxType_Type.SelectedValue + "=";
                    for (int i = 0; i < s.Length; i++)
                    {
                        if (!(Regex.IsMatch(s[i], r)))
                        {
                            //throw new Exception(s.Length.ToString());
                            strContent += s[i].Trim() + "|" + s[i].Trim() + "$0";
                            if (i < s.Length - 1)
                                strContent += "||";
                        }
                        else
                        {
                            strContent += s[i];
                            if (i < s.Length - 1)
                                strContent += "||";
                        }
                    }

                }
                else
                {
                    strContent = ListBoxType_Type.SelectedValue + "=" + ListBoxType_Content.Text.Trim().Replace("\r\n", "||") + "";
                }
                fieldType = "ntext";
                break;
            case "NumType"://数字
                strContent = "TitleSize=" + NumberType_TitleSize.Text + ",NumberType=" + NumberType_Style.SelectedValue + ",DefaultValue=" + NumberType_DefaultValue.Text;
                strContent = strContent + ",NumSearchType=,NumRangType=,NumSearchRang=,NumLenght=" + txtdecimal.Text;
                int numstyle = DataConverter.CLng(NumberType_Style.SelectedValue);
                if (numstyle == 1)
                    fieldType = "int";
                if (numstyle == 2)
                    fieldType = "float";
                if (numstyle == 3)
                    fieldType = "money";
                break;
            case "DateType": //日期时间
                strContent = "Type=" + DateSearchType.SelectedValue;
                fieldType = "datetime";
                break;
            case "PicType"://图片
                strContent = "Water=" + Pic_Water_Chk.Checked + ",MaxPicSize=" + MaxPicSize_T.Text + ",PicFileExt=" + PicFileExt_T.Text + ",SelUpfile=" + Pic_SelUpFile_Chk.Checked + ",Compress=" + Pic_Compress_Chk.Checked;
                fieldType = "nvarchar";
                flag2 = false;
                break;
            case "SqlType"://入库图片
                strContent = "MaxPicSize=" + TxtMaxPicSize.Text + ",FileExtArr=" + TxtPicSqlType.Text;
                fieldType = "ntext";
                tempFieldName = "FIT_" + Name_T.Text;
                tempFieldType = "nvarchar";
                flag2 = false;
                break;
            case "SqlFile"://入库文件
                strContent = "MaxPicSize=" + TxtMSqlFileSize.Text + ",FileExtArr=" + TxtSqlFiletext.Text;
                fieldType = "ntext";
                tempFieldName = "FIT_" + Name_T.Text;
                tempFieldType = "nvarchar";
                flag2 = false;
                break;
            case "MultiPicType": //多图片
                strContent = "ChkThumb=" + (ChkThumb.Checked ? "1" : "0") + ",ThumbField=" + TxtThumb.Text + ",Warter=" + Pic_Water_Chk.Checked + ",MaxPicSize=" + TxtPicSize.Text + ",PicFileExt=" + TextImageType.Text;
                if (ChkThumb.Checked)
                {
                    tempFieldName = TxtThumb.Text;
                    tempFieldType = "nvarchar";
                }
                fieldType = "ntext";
                flag2 = false;
                break;
            case "SmallFileType"://文件
                strContent = "MaxFileSize=" + TxtMaxFileSizes.Text + ",UploadFileExt=" + TxtUploadFileTypes.Text + ",SelUpfile=" + (rblSelUploadFile.Checked ? "1" : "0") + ",isbigfile=" + (isBigFile.Checked ? "1" : "0");
                fieldType = "nvarchar";
                flag2 = false;
                break;
            case "PullFileType"://下拉文件
                strContent = PullFileText.Text;
                fieldType = "nvarchar";
                flag2 = false;
                break;
            case "FileType"://多文件
                //if (IsSwfFileUpload.Checked)
                //{
                //    strContent = "MaxFileSize=" + TxtMaxFileSize.Text + ",UploadFileExt=" + TxtUploadFileType.Text;
                //}
                //else
                //{
                //    
                //}
                //strContent = "MaxFileSize=10240000,UploadFileExt=zip";
                strContent = "ChkFileSize=" + (ChkFileSize.Checked ? "1" : "0") + ",FileSizeField=" + TxtFileSizeField.Text + ",MaxFileSize=,UploadFileExt=zip";
                if (ChkFileSize.Checked)
                {
                    tempFieldName = TxtFileSizeField.Text;
                    tempFieldType = "nvarchar";
                }
                fieldType = "ntext";
                flag2 = false;
                break;
            case "OperatingType": //运行平台
                strContent = "TitleSize=" + OperatingType_TitleSize.Text + ",OperatingList=" + TxtOperatingOption.Text.Trim().Replace(" ", "").Replace("\r\n", "|") + ",DefaultValue=" + OperatingType_DefaultValue.Text;
                fieldType = "nvarchar";
                break;
            case "SuperLinkType"://超链接
                strContent = "TitleSize=" + SuperLinkType_TitleSize.Text + ",DefaultValue=" + SuperLinkType_DefaultValue.Text;
                fieldType = "nvarchar";
                flag2 = false;
                break;
            case "GradeOptionType":
                strContent = "GradeCate=" + GradeOptionType_Cate.SelectedValue + ",Direction=" + GradeOptionType_Direction.SelectedValue;
                fieldType = "nvarchar";
                break;
            case "ColorType"://颜色字段
                strContent = "TitleSize=" + SuperLinkType_TitleSize.Text + ",DefaultValue=" + ColorDefault.Text;
                fieldType = "nvarchar";
                flag2 = false;
                break;
            case "DoubleDateType"://双时间字段
                strContent = "DateSearchType=" + DateSearchType.SelectedValue + ",DateSearchRang=" + DateSearchRang.Text.Trim().Replace(" ", "").Replace("\r\n", "$");
                strContent = strContent + ",DateSearchUnit=" + DateSearchUnit.SelectedValue;
                fieldType = "ntext";
                break;
            case "Upload":// 在线浏览
                strContent = "Warter=" + Pic_Water_Chk.Checked + ",MaxPicSize=" + TextBox2.Text + ",PicFileExt=" + TextBox3.Text;
                fieldType = "nvarchar";
                break;
            case "MapType"://地图类型
                strContent = "source=" + MapSource_DP.SelectedValue + ",type=" + MapType_Rad.SelectedValue;
                fieldType = "ntext";
                break;
            case "SwfFileUpload"://智能多文件上传
                strContent = "MaxFileSize=" + TxtMaxFileSize1.Text + ",UploadFileExt=" + TxtUploadFileType1.Text;
                fieldType = "ntext";
                break;
            case "RemoteFile": //远程文件
                strContent = "FtpID=" + DropDownList1.SelectedItem.Value + ",MaxFileSize=" + TxtMaxFileSize2.Text + ",UploadFileExt=" + TxtUploadFileType2.Text;
                fieldType = "ntext";
                break;
            case "MobileSMS": //手机短信
                strContent = "Width=" + MobileSMSType_Width.Text + ",Height=" + MobileSMSType_Height.Text + "";
                fieldType = "ntext";
                break;
            case "Charts":
                fieldType = "int";
                break;
            case "TableField"://库选字段
                strContent = "TextField=" + TableField_Text.Text.Trim() + "," + "ValueField=" + TableField_Value.Text.Trim() + ","
                            + "WhereStr=" + Where_Text.Text.Trim() + "," + "FieldType=" + TableFieldType_Drop.SelectedValue;
                //strContent = TableField_Text.Text.Trim()+","+TableField_Value.Text.Trim()+","+Where_Text.Text.Trim();
                fieldType = "nvarchar";
                break;
            case "Random"://随机数
                {
                    string len = DataConverter.CLng(Random_Len_T.Text) == 0 ? "6" : Random_Len_T.Text;
                    strContent = "Type=" + Random_Type_Rad.SelectedValue + ",Len=" + len;
                    fieldType = "nvarchar";
                }
                break;
            case "Images"://组图
                {
                    strContent = "IsWater=" + (IsWater_Images.Checked ? "1" : "0");
                    fieldType = "ntext";
                }
                break;
            case "CameraType"://拍照
                {
                    strContent = "cameraWidth=" + DataConverter.CLng(CameraWidth_T.Text) + "," + "cameraHeight=" + DataConverter.CLng(CameraHeight_T.Text)
                                + ",imgWidth=" + DataConverter.CLng(CameraImgWidth_T.Text) + "," + "imgHeight=" + DataConverter.CLng(CameraImgHeight_T.Text);
                    fieldType = "nvarchar";
                }
                break;
            case "autothumb"://压图传入
                {
                    strContent = "width=" + DataConverter.CLng(autothumb_width_t.Text.Trim()) + ",height=" + DataConverter.CLng(autothumb_height_t.Text.Trim());
                    fieldType = "nvarchar";
                }
                break;
            case "api_qq_mvs":
                {
                    strContent = "";
                    fieldType = "nvarchar";
                }
                break;
            default:
                function.WriteErrMsg("保存异常，选定字段类型不匹配!!!");
                break;
        }
        M_ModelField modelfield = new M_ModelField();
        M_ModelInfo modelMod = bModel.SelReturnModel(ModelID);
        if (FieldID != 0)
        {
            modelfield = bll.GetModelByID(ModelID.ToString(), FieldID);
        }
        else
        {
            modelfield.ModelID = ModelID;
            modelfield.FieldID = 0;
        }
        modelfield.FieldName = Name_T.Text.Trim();
        modelfield.FieldAlias = Alias_T.Text.Trim();
        modelfield.FieldTips = Tips.Text.Trim();
        modelfield.Description = Description.Text;
        modelfield.IsNotNull = IsNotNull.Checked;
        modelfield.IsSearchForm = flag2;
        modelfield.FieldType = Type_Rad.SelectedValue;
        modelfield.Content = strContent;
        //modelfield.OrderID = bll.GetMaxOrder(ModelID)+1;
        modelfield.ShowList = true;
        modelfield.ShowWidth = 0;
        modelfield.IsShow = IsShow.Checked;
        modelfield.IsView = true;
        modelfield.IsDownField = SetDownFiled.Checked ? 1 : 0;
        modelfield.DownServerID = DataConverter.CLng(serverlist.SelectedValue);
        modelfield.IsCopy = rblCopy.Checked ? 0 : -1;
        modelfield.Islotsize = Islotsize.Checked;
        modelfield.IsChain = IsChain.Checked;
        switch (ModelType)
        {
            case 9:
                UBFieldAddORUpdate(fieldType, strContent, defaultValue, tempFieldName, tempFieldType);
                break;
            default:
                if (FieldID == 0)
                {
                    bll.AddModelField(modelMod, modelfield);
                    if (!string.IsNullOrEmpty(tempFieldName))
                    {
                        bll.AddField(modelMod.TableName, tempFieldName, tempFieldType, "");
                    }
                }
                else
                {
                    bll.Update(modelfield);
                }
                function.WriteSuccessMsg("操作成功", "ModelField.aspx?ModelID=" + ModelID + "&ModelType=" + ModelType);
                break;
        }
    }
    protected void Return_Btn_Click(object sender, EventArgs e)
    {
        switch (ModelType)
        {
            case 9:
                Response.Redirect("../User/SystemUserModel.aspx");
                break;
            default:
                Response.Redirect("ModelField.aspx?ModelID=" + ModelID + "&ModelType=" + ModelType);
                break;
        }
    }
    //-----------------------Tools
    private void GetIsOk_Add()
    {
        string text = Name_T.Text;
        string str2 = Alias_T.Text;
        string selectedValue = Type_Rad.SelectedValue;
        if (text == "")
        {
            function.WriteErrMsg("字段名不能为空", "javascript:history.back(-1);");
        }
        if (!DataValidator.IsValidUserName(text))
        {
            function.WriteErrMsg("字段名必须由字母、数字、下划线组成", "javascript:history.back(-1);");
        }
        if (bll.IsExists(ModelID, text))
        {
            function.WriteErrMsg("该模型已有相同字段名的字段", "javascript:history.back(-1);");
        }
        if (str2 == "")
        {
            function.WriteErrMsg("字段别名不能为空", "javascript:history.back(-1);");
        }

        if (SetDownFiled.Checked)
        {
            if (serverlist.SelectedValue == "")
            {
                function.WriteErrMsg("关联下载服务器不能为空", "javascript:history.back(-1);");
            }
        }
        switch (selectedValue)
        {
            case "TextType":
                if (TitleSize.Text == "")
                {
                    function.WriteErrMsg("文本框长度不能为空", "javascript:history.back(-1);");
                }
                if (!DataValidator.IsNumber(TitleSize.Text))
                {
                    function.WriteErrMsg("文本框长度必须是整形数字", "javascript:history.back(-1);");
                }
                break;
            case "MultipleTextType":
                if (MultipleTextType_Width.Text == "")
                {
                    function.WriteErrMsg("文本框显示的宽度不能为空", "javascript:history.back(-1);");
                }
                if (!DataValidator.IsNumber(MultipleTextType_Width.Text))
                {
                    function.WriteErrMsg("文本框显示的宽度必须是整形数字", "javascript:history.back(-1);");
                }
                if (MultipleTextType_Height.Text == "")
                {
                    function.WriteErrMsg("文本框显示的高度不能为空", "javascript:history.back(-1);");
                }
                if (!DataValidator.IsNumber(MultipleTextType_Height.Text))
                {
                    function.WriteErrMsg("文本框显示的高度必须是整形数字", "javascript:history.back(-1);");
                }
                break;
            case "MultipleHtmlType":
                if (MultipleHtmlType_Width.Text == "")
                {
                    function.WriteErrMsg("文本框显示的宽度不能为空", "javascript:history.back(-1);");
                }
                if (!DataValidator.IsNumber(MultipleHtmlType_Width.Text))
                {
                    function.WriteErrMsg("文本框显示的宽度必须是整形数字", "javascript:history.back(-1);");
                }
                if (MultipleHtmlType_Height.Text == "")
                {
                    function.WriteErrMsg("文本框显示的高度不能为空", "javascript:history.back(-1);");
                }
                if (!DataValidator.IsNumber(MultipleHtmlType_Height.Text))
                {
                    function.WriteErrMsg("文本框显示的高度必须是整形数字", "javascript:history.back(-1);");
                }
                break;
            case "OptionType":
                if (RadioType_Content.Text == "")
                {
                    function.WriteErrMsg("单选项的选项不能为空", "javascript:history.back(-1);");
                }
                break;
            case "ListBoxType":
                if (ListBoxType_Content.Text == "")
                {
                    function.WriteErrMsg("多选项的选项不能为空", "javascript:history.back(-1);");
                }
                break;
            case "NumType":
                if (NumberType_TitleSize.Text == "")
                {
                    function.WriteErrMsg("输入数字的文本框长度不能为空", "javascript:history.back(-1);");
                }
                if (!DataValidator.IsNumber(NumberType_TitleSize.Text))
                {
                    function.WriteErrMsg("输入数字的文本框长度必须是整形数字", "javascript:history.back(-1);");
                }
                break;
            case "PicType":
                if (string.IsNullOrEmpty(MaxPicSize_T.Text))
                {
                    function.WriteErrMsg("图片文件大小限制不能为空", "javascript:history.back(-1);");
                }
                if (string.IsNullOrEmpty(PicFileExt_T.Text))
                {
                    function.WriteErrMsg("允许上传的图片类型不能为空", "javascript:history.back(-1);");
                }
                break;

            case "SqlType":
                if (TxtMaxPicSize.Text == "")
                {
                    function.WriteErrMsg("图片文件大小限制不能为空", "javascript:history.back(-1);");
                }
                if (TxtPicSqlType.Text == "")
                {
                    function.WriteErrMsg("允许上传的图片类型不能为空", "javascript:history.back(-1);");
                }
                break;

            case "SqlFile":
                if (TxtMSqlFileSize.Text == "")
                {
                    function.WriteErrMsg("文件文件大小限制不能为空", "javascript:history.back(-1);");
                }
                if (TxtSqlFiletext.Text == "")
                {
                    function.WriteErrMsg("允许上传的文件类型不能为空", "javascript:history.back(-1);");
                }
                break;

            case "MultiPicType":
                if (ChkThumb.Checked && TxtThumb.Text.Trim() == "")
                {
                    function.WriteErrMsg("选择保存缩略图时，图片缩略图保存字段名不能为空", "javascript:history.back(-1);");
                }
                if (TxtPicSize.Text == "")
                {
                    function.WriteErrMsg("图片文件大小限制不能为空", "javascript:history.back(-1);");
                }
                if (TextImageType.Text == "")
                {
                    function.WriteErrMsg("允许上传的图片类型不能为空", "javascript:history.back(-1);");
                }
                break;
            case "SmallFileType":
                if (TxtMaxFileSizes.Text == "")
                {
                    function.WriteErrMsg("允许上传的文件大小限制不能为空", "javascript:history.back(-1);");
                }
                if (TxtUploadFileTypes.Text == "")
                {
                    function.WriteErrMsg("允许上传的文件类型不能为空", "javascript:history.back(-1);");
                }
                break;
            case "FileType":
                if (ChkFileSize.Checked && TxtFileSizeField.Text == "")
                {
                    function.WriteErrMsg("选择保存文件大小时，文件大小保存字段名不能为空", "javascript:history.back(-1);");
                }
                break;
            case "OperatingType":
                if (OperatingType_TitleSize.Text == "")
                {
                    function.WriteErrMsg("输入运行平台的文本框长度不能为空", "javascript:history.back(-1);");
                }
                if (TxtOperatingOption.Text == "")
                {
                    function.WriteErrMsg("运行平台选项不能为空", "javascript:history.back(-1);");
                }
                break;
            case "SuperLinkType":
                if (SuperLinkType_TitleSize.Text == "")
                {
                    function.WriteErrMsg("输入超链接的文本框长度不能为空", "javascript:history.back(-1);");
                }
                break;
            case "MobileSMS":
                if (MobileSMSType_Width.Text == "")
                {
                    function.WriteErrMsg("文本框显示的宽度不能为空", "javascript:history.back(-1);");
                }
                if (!DataValidator.IsNumber(MobileSMSType_Width.Text))
                {
                    function.WriteErrMsg("文本框显示的宽度必须是整形数字", "javascript:history.back(-1);");
                }
                if (MobileSMSType_Height.Text == "")
                {
                    function.WriteErrMsg("文本框显示的高度不能为空", "javascript:history.back(-1);");
                }
                if (!DataValidator.IsNumber(MobileSMSType_Height.Text))
                {
                    function.WriteErrMsg("文本框显示的高度必须是整形数字", "javascript:history.back(-1);");
                }
                break;
            case "Upload":
                if (TextBox2.Text == "")
                {
                    function.WriteErrMsg("文件大小限制不能为空", "javascript:history.back(-1);");
                }
                if (TextBox3.Text == "")
                {
                    function.WriteErrMsg("文件类型不能为空", "javascript:history.back(-1);");
                }
                break;
            case "SwfFileUpload":
                if (TxtMaxFileSize1.Text == "")
                {
                    function.WriteErrMsg("允许上传的文件大小限制不能为空", "javascript:history.back(-1);");
                }
                if (TxtUploadFileType1.Text == "")
                {
                    function.WriteErrMsg("允许上传的文件类型不能为空", "javascript:history.back(-1);");
                }
                break;
            case "RemoteFile":
                if (TxtMaxFileSize2.Text == "")
                {
                    function.WriteErrMsg("允许上传的文件大小限制不能为空", "javascript:history.back(-1);");
                }
                if (TxtUploadFileType2.Text == "")
                {
                    function.WriteErrMsg("允许上传的文件类型不能为空", "javascript:history.back(-1);");
                }
                break;
        }
    }
    private void GetIsOk_Updata()
    {
        string text = Name_T.Text;
        string str2 = Alias_T.Text;
        string selectedValue = Type_Rad.SelectedValue;

        if (text == "")
        {
            function.WriteErrMsg("字段名不能为空", "javascript:history.back(-1);");
        }
        if (!DataValidator.IsValidUserName(text))
        {
            function.WriteErrMsg("字段名必须由字母、数字、下划线组成", "javascript:history.back(-1);");
        }
        if (str2 == "")
        {
            function.WriteErrMsg("字段别名不能为空", "javascript:history.back(-1);");
        }

        if (SetDownFiled.Checked)
        {
            if (serverlist.SelectedValue == "")
            {
                function.WriteErrMsg("关联下载服务器不能为空", "javascript:history.back(-1);");
            }
        }

        switch (selectedValue)
        {
            case "TextType":
                if (TitleSize.Text == "")
                {
                    function.WriteErrMsg("文本框长度不能为空", "javascript:history.back(-1);");
                }
                if (!DataValidator.IsNumber(TitleSize.Text))
                {
                    function.WriteErrMsg("文本框长度必须是整形数字", "javascript:history.back(-1);");
                }
                break;

            case "MultipleTextType":
                if (MultipleTextType_Width.Text == "")
                {
                    function.WriteErrMsg("文本框显示的宽度不能为空", "javascript:history.back(-1);");
                }
                if (!DataValidator.IsNumber(MultipleTextType_Width.Text))
                {
                    function.WriteErrMsg("文本框显示的宽度必须是整形数字", "javascript:history.back(-1);");
                }
                if (MultipleTextType_Height.Text == "")
                {
                    function.WriteErrMsg("文本框显示的高度不能为空", "javascript:history.back(-1);");
                }
                if (!DataValidator.IsNumber(MultipleTextType_Height.Text))
                {
                    function.WriteErrMsg("文本框显示的高度必须是整形数字", "javascript:history.back(-1);");
                }
                break;

            case "MultipleHtmlType":
                if (MultipleHtmlType_Width.Text == "")
                {
                    function.WriteErrMsg("文本框显示的宽度不能为空", "javascript:history.back(-1);");
                }
                if (!DataValidator.IsNumber(MultipleHtmlType_Width.Text))
                {
                    function.WriteErrMsg("文本框显示的宽度必须是整形数字", "javascript:history.back(-1);");
                }
                if (MultipleHtmlType_Height.Text == "")
                {
                    function.WriteErrMsg("文本框显示的高度不能为空", "javascript:history.back(-1);");
                }
                if (!DataValidator.IsNumber(MultipleHtmlType_Height.Text))
                {
                    function.WriteErrMsg("文本框显示的高度必须是整形数字", "javascript:history.back(-1);");
                }
                break;
        }
        if ((selectedValue == "OptionType") && (RadioType_Content.Text == ""))
        {
            function.WriteErrMsg("单选项的选项不能为空", "javascript:history.back(-1);");
        }
        if ((selectedValue == "ListBoxType") && (ListBoxType_Content.Text == ""))
        {
            function.WriteErrMsg("多选项的选项不能为空", "javascript:history.back(-1);");
        }
        if (selectedValue == "NumType")
        {
            if (NumberType_TitleSize.Text == "")
            {
                function.WriteErrMsg("输入数字的文本框长度不能为空", "javascript:history.back(-1);");
            }
            if (!DataValidator.IsNumber(NumberType_TitleSize.Text))
            {
                function.WriteErrMsg("输入数字的文本框长度必须是整形数字", "javascript:history.back(-1);");
            }
        }
        if (selectedValue == "SwfFileUpload")
        {

        }
        if (selectedValue == "RemoteFile")
        {
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
                {
                    context += SortStr(s.Replace("|", "||"));
                }
                else
                {
                    if (s.IndexOf('$') == -1)
                    {
                        context += s + "$0";
                    }
                    else
                    {
                        context += s;
                    }
                }
            }
            else
            {
                if (s.IndexOf('$') == -1)
                {
                    context += s + "|" + s + "$0";
                }
                else
                {
                    context += s + "|" + s;
                }
            }
            context += "||";
        }
        if (context.EndsWith("||"))
        {
            context = context.Remove(context.Length - 2);
        }
        return context;
    }
    private void Serverlist()
    {
        if (SetDownFiled.Checked)
        {
            downserver.Visible = true;
            B_DownServer downdll = new B_DownServer();
            DataTable downlist = downdll.GetDownServerAll();
            downlist.DefaultView.Sort = "ServerID";
            serverlist.DataSource = downlist;
            serverlist.DataTextField = "ServerName";
            serverlist.DataValueField = "ServerID";
            serverlist.DataBind();
            serverlist.Items.Insert(0, new ListItem("请选择服务器", ""));
            for (int i = 0; i < Type_Rad.Items.Count; i++)
            {
                if (Type_Rad.Items[i].Value != "SmallFileType")
                {
                    Type_Rad.Items[i].Enabled = false;
                }
                else
                {
                    M_ModelField field = bll.GetModelByID(ModelID.ToString(), FieldID);
                    string content = field.Content;
                    ChkFileSize.Enabled = false;
                    TxtFileSizeField.Enabled = false;
                    TxtMaxFileSizes.Text = bll.GetFieldContent(content, 0, 1);
                    TxtUploadFileTypes.Text = bll.GetFieldContent(content, 1, 1);
                    DivSmallFileType.Style["display"] = "";
                    DivTextType.Style["display"] = "none";
                    Type_Rad.Items[i].Selected = true;
                }
            }
        }
    }
    private void Ftplist()
    {
        B_FTP bf = new B_FTP();
        DataTable dt = bf.SelectFtp_All();
        //throw new Exception(dt.Rows.Count.ToString());
        dt.DefaultView.Sort = "ID";
        DropDownList1.DataSource = dt;
        DropDownList1.DataTextField = "Alias";
        DropDownList1.DataValueField = "ID";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new ListItem("请选择服务器", "0"));
    }
    //使用字段模型的值,填充页面服务端控件
    private void GetField(M_ModelField m_MF)
    {
        Name_T.Text = m_MF.FieldName;
        Alias_T.Text = m_MF.FieldAlias;
        Description.Text = m_MF.Description;
        Tips.Text = m_MF.FieldTips;
        IsNotNull.Checked = m_MF.IsNotNull;
        IsSearchForm.Checked = m_MF.IsSearchForm;
        IsShow.Checked = m_MF.IsShow;
        rblCopy.Checked = m_MF.IsCopy != -1 ? true : false;
        SetDownFiled.Checked = m_MF.IsDownField == 1 ? true : false;
        Islotsize.Checked = m_MF.Islotsize;
        IsChain.Checked = m_MF.IsChain;
        FieldModel fieldMod = new FieldModel(m_MF.Content);
        //IsSwfFileUpload.Enabled = false;
        Name_T.Enabled = false;
        string content = m_MF.Content;
        string type = m_MF.FieldType;
        //EditAuth_Hid.Value = fieldMod.GetValue("EditAuth");
        //EditAuth_T.Text = buser.GetUserNameByIDS(EditAuth_Hid.Value);
        if (type == "SwfFileUpload")
        {
            Type_Rad.SelectedValue = "FileType";
            type = "FileType";
            //IsSwfFileUpload.Checked = true;
        }
        else
        {
            //IsSwfFileUpload.Checked = false;
            Type_Rad.SelectedValue = type.Trim();
        }
        Type_Rad.Enabled = false;
        #region
        switch (type)
        {
            //单行文本
            case "TextType":
                TitleSize.Text = fieldMod.GetValue("TitleSize");
                IsPassword.Checked = fieldMod.GetValue("IsPassword").Equals("password");
                TextType_DefaultValue.Text = fieldMod.GetValue("DefaultValue");
                Text_SelVideo_Chk.Checked = DataConverter.CBool(fieldMod.GetValue("SelVideo"));
                Text_SelIcon_Chk.Checked = fieldMod.GetValBool("SelIcon");
                DivTextType.Style["display"] = "";
                break;
            //多行文本(不支持Html)
            case "MultipleTextType":
                MultipleTextType_Width.Text = fieldMod.GetValue("width");
                MultipleTextType_Height.Text = fieldMod.GetValue("height");
                MText_SelUser_Chk.Checked = DataConverter.CBool(fieldMod.GetValue("SelUser"));
                MText_Down_Chk.Checked = fieldMod.GetValBool("Down");
                DivTextType.Style["display"] = "none";
                DivMultipleTextType.Style["display"] = "";
                break;
            //多行文本(支持Html)
            case "MultipleHtmlType":
                //Width=715,Height=400,IsEditor=3,AllowWord_Chk=true
                MultipleHtmlType_Width.Text = fieldMod.GetValue("Width");
                MultipleHtmlType_Height.Text = fieldMod.GetValue("Height");
                IsEditor.SelectedValue = fieldMod.GetValue("IsEditor");
                Topimg_Chk.Checked = fieldMod.GetValBool("Topimg");
                AllowWord_Chk.Checked = fieldMod.GetValBool("AllowWord_Chk");
                DivTextType.Style["display"] = "none";
                DivMultipleHtmlType.Style["display"] = "";
                break;
            //单选项
            case "OptionType":
                RadioType_Content.Text = SortStr(bll.GetFieldContent(content, 0, 1)).Replace("||", "\r\n");
                RadioType_Type.SelectedValue = bll.GetFieldContent(content, 0, 0);
                RadioType_Property.Checked = bll.GetFieldContent(content, 1, 1).Equals("True") ? true : false;
                RadioType_Default.Text = bll.GetFieldContent(content, 2, 1).ToString();
                DivTextType.Style["display"] = "none";
                DivOptionType.Style["display"] = "";
                break;
            //多选项
            case "ListBoxType":
                ListBoxType_Content.Text = SortStr(bll.GetFieldContent(content, 0, 1)).Replace("||", "\r\n");
                ListBoxType_Type.SelectedValue = bll.GetFieldContent(content, 0, 0);
                DivTextType.Style["display"] = "none";
                DivListBoxType.Style["display"] = "";
                break;
            //数字
            case "NumType":
                NumberType_TitleSize.Text = bll.GetFieldContent(content, 0, 1);
                NumberType_Style.SelectedValue = bll.GetFieldContent(content, 1, 1);
                NumberType_Style.Enabled = false;
                NumberType_DefaultValue.Text = bll.GetFieldContent(content, 2, 1);
                ClientScript.RegisterStartupScript(GetType(), "", "<script>window.onload=function(){document.getElementById('showdec').style.display = '';}</script>");
                try
                {
                    if (bll.GetFieldContent(content, 6, 1) != "")
                    {
                        txtdecimal.Text = bll.GetFieldContent(content, 6, 1);
                    }
                    else
                    {
                        txtdecimal.Text = "0";
                    }
                }
                catch (Exception)
                {
                    txtdecimal.Text = "0";
                }
                DivTextType.Style["display"] = "none";
                DivNumType.Style["display"] = "";
                break;
            //日期时间
            case "DateType":
                DateSearchType.SelectedValue = fieldMod.GetValue("Type");
                DivTextType.Style["display"] = "none";
                DivDateType.Style["display"] = "";
                break;
            //图片
            case "PicType":
                Pic_Water_Chk.Checked = fieldMod.GetValBool("Water");
                MaxPicSize_T.Text = fieldMod.GetValue("MaxPicSize");
                PicFileExt_T.Text = fieldMod.GetValue("PicFileExt");
                Pic_SelUpFile_Chk.Checked = fieldMod.GetValBool("SelUpFile");
                Pic_Compress_Chk.Checked = fieldMod.GetValBool("Compress");
                DivPicType.Style["display"] = "";
                DivTextType.Style["display"] = "none";
                break;
            //入库图片
            case "SqlType":
                TxtMaxPicSize.Text = bll.GetFieldContent(content, 0, 1);
                DivSqlType.Style["display"] = "";
                DivTextType.Style["display"] = "none";
                DivTextType.Visible = false;
                TxtPicSqlType.Text = bll.GetFieldContent(content, 1, 1);
                break;
            //入库数据
            case "SqlFile":
                TxtMSqlFileSize.Text = bll.GetFieldContent(content, 0, 1);
                DivSqlFile.Style["display"] = "";
                DivTextType.Style["display"] = "none";
                TxtSqlFiletext.Text = bll.GetFieldContent(content, 1, 1);
                DivTextType.Visible = false;
                break;
            //多图片
            case "MultiPicType":
                if (DataConverter.CBool(bll.GetFieldContent(content, 0, 1)))
                {
                    ChkThumb.Checked = true;
                    TxtThumb.Text = bll.GetFieldContent(content, 1, 1);
                }
                else
                {
                    ChkThumb.Checked = false;
                    TxtThumb.Text = "";
                }
                ChkThumb.Enabled = false;
                TxtThumb.Enabled = false;
                RBLWaterMark.Checked = bll.GetFieldContent(content, 2, 1).Equals("True") ? true : false;
                TxtPicSize.Text = bll.GetFieldContent(content, 3, 1);
                TextImageType.Text = bll.GetFieldContent(content, 4, 1);
                DivMultiPicType.Style["display"] = "";
                DivTextType.Style["display"] = "none";

                break;
            //文件
            case "SmallFileType":
                ChkFileSize.Enabled = false;
                TxtFileSizeField.Enabled = false;
                TxtMaxFileSizes.Text = bll.GetFieldContent(content, 0, 1);
                TxtUploadFileTypes.Text = bll.GetFieldContent(content, 1, 1);
                rblSelUploadFile.Checked = bll.GetFieldContent(content, 2, 1).Equals("1");
                DivSmallFileType.Style["display"] = "";
                DivTextType.Style["display"] = "none";
                break;
            //下拉文件
            case "PullFileType":
                PullFileText.Text = bll.GetFieldContent(content, 0, 0);
                DivPullFileType.Style["display"] = "";
                DivTextType.Style["display"] = "none";
                break;
            //多文件
            case "FileType":
                if (DataConverter.CBool(bll.GetFieldContent(content, 0, 1)))
                {
                    ChkFileSize.Checked = true;
                }
                else
                {
                    ChkFileSize.Checked = false;
                }
                TxtFileSizeField.Text = bll.GetFieldContent(content, 1, 1);
                DivFileType.Style["display"] = "";
                DivTextType.Style["display"] = "none";
                break;
            //运行平台
            case "OperatingType":
                TxtOperatingOption.Text = bll.GetFieldContent(content, 1, 1).Replace("|", "\r\n");
                OperatingType_TitleSize.Text = bll.GetFieldContent(content, 0, 1);
                OperatingType_DefaultValue.Text = bll.GetFieldContent(content, 2, 1);
                DivOperatingType.Style["display"] = "";
                DivTextType.Style["display"] = "none";
                break;
            //超链接
            case "SuperLinkType":
                SuperLinkType_TitleSize.Text = bll.GetFieldContent(content, 0, 1);
                SuperLinkType_DefaultValue.Text = bll.GetFieldContent(content, 1, 1);
                DivSuperLinkType.Style["display"] = "";
                DivTextType.Style["display"] = "none";
                break;
            //多级选项
            case "GradeOptionType":
                GradeOptionType_Cate.SelectedValue = bll.GetFieldContent(content, 0, 1);
                GradeOptionType_Direction.SelectedValue = bll.GetFieldContent(content, 1, 1);
                DivTextType.Style["display"] = "none";
                DivGradeOptionType.Style["display"] = "";
                break;
            //颜色字段
            case "ColorType":
                ColorDefault.Text = bll.GetFieldContent(content, 1, 1);
                DivColorType.Style["display"] = "";
                DivTextType.Style["display"] = "none";
                break;
            //offece转换为flash
            case "Upload":
                TextBox2.Text = bll.GetFieldContent(content, 1, 1);
                TextBox3.Text = bll.GetFieldContent(content, 2, 1);
                DivUpload.Style["display"] = "";
                DivTextType.Style["display"] = "none";
                break;
            case "MapType":
                FieldModel fieldmod = new FieldModel(content);
                DivTextType.Style["display"] = "none";
                DivMapType.Style["display"] = "";
                MapSource_DP.SelectedValue = fieldmod.GetValue("source");
                MapType_Rad.SelectedValue = fieldmod.GetValue("type");
                break;
            case "SwfFileUpload":
                TxtMaxFileSize1.Text = bll.GetFieldContent(content, 0, 1);
                TxtUploadFileType1.Text = bll.GetFieldContent(content, 1, 1);
                DivTextType.Style["display"] = "none";
                DivSwfFileUpload.Style["display"] = "";
                break;
            case "RemoteFile":
                Ftplist();
                DropDownList1.SelectedValue = m_MF.Content.Split(',')[0].Split('=')[1].ToString();
                TxtMaxFileSize2.Text = bll.GetFieldContent(content, 1, 1);
                TxtUploadFileType2.Text = bll.GetFieldContent(content, 2, 1);
                DivTextType.Style["display"] = "none";
                DivRemoteFile.Style["display"] = "";
                break;
            case "TableField":
                fieldmod = new FieldModel(m_MF.Content);
                TableField_Text.Text = fieldmod.GetValue("TextField");// m_MF.Content.Split(',')[0];
                TableField_Value.Text = fieldmod.GetValue("ValueField");
                Where_Text.Text = fieldmod.GetValue("WhereStr");
                TableFieldType_Drop.SelectedValue = fieldmod.GetValue("FieldType");
                break;
            case "Random":
                Random_Len_T.Text = fieldMod.GetValue("len");
                Random_Type_Rad.SelectedValue = fieldMod.GetValue("type");
                break;
            case "Images"://组图
                IsWater_Images.Checked = fieldMod.GetValue("IsWater").Equals("1");
                break;
            case "CameraType":
                CameraWidth_T.Text = fieldMod.GetValue("cameraWidth");
                CameraHeight_T.Text = fieldMod.GetValue("cameraHeight");
                CameraImgWidth_T.Text = fieldMod.GetValue("imgWidth");
                CameraImgHeight_T.Text = fieldMod.GetValue("imgHeight");
                break;
            case "autothumb":
                autothumb_width_t.Text = fieldMod.GetValue("width");
                autothumb_height_t.Text = fieldMod.GetValue("height");
                break;
        #endregion
        }
        function.Script(this, "SelectModelType();");
    }
    //-----------------------ZL_UserBaseField
    private void UBFieldAddORUpdate(string fieldType, string content, string defaultValue, string tempFieldName, string tempFieldType)
    {
        M_UserBaseField modelfield = new M_UserBaseField();
        if (FieldID > 0) { modelfield = ubfieldBll.GetSelect(FieldID); }
        modelfield.FieldName = Name_T.Text;
        modelfield.FieldAlias = Alias_T.Text;
        modelfield.Description = Description.Text;
        modelfield.FieldTips = Tips.Text.Trim();
        modelfield.FieldType = Type_Rad.SelectedValue;
        modelfield.Content = content;
        modelfield.IsNotNull = IsNotNull.Checked ? 1 : 0;
        modelfield.OrderId = ubfieldBll.GetMaxID();
        modelfield.ShowList = 1;//是否列表显示
        modelfield.ShowWidth = 10;//百分比
        modelfield.NoEdit = 0;
        if (FieldID > 0)
        {
            ubfieldBll.GetUpdate(modelfield);
        }
        else
        {
            if (!string.IsNullOrEmpty(tempFieldName) && !string.IsNullOrEmpty(tempFieldType))
            {
                bll.AddField("ZL_UserBase", tempFieldName, tempFieldType, "");
            }
            else
            {
                bll.AddField("ZL_UserBase", modelfield.FieldName, fieldType, defaultValue);
            }
            ubfieldBll.GetInsert(modelfield);
        }
        Response.Redirect("../User/SystemUserModel.aspx");
    }
    private M_ModelField CopyModelField(M_UserBaseField ubmodel, M_ModelField m_MF)
    {
        m_MF.FieldName = ubmodel.FieldName;
        m_MF.FieldAlias = ubmodel.FieldAlias;
        m_MF.Content = ubmodel.Content;
        m_MF.FieldType = ubmodel.FieldType;
        m_MF.Description = ubmodel.Description;
        m_MF.FieldTips = ubmodel.FieldTips;
        m_MF.IsNotNull = ubmodel.IsNotNull == 1;
        m_MF.IsSearchForm = false;
        m_MF.IsShow = true;
        m_MF.IsCopy = 1;
        m_MF.IsDownField = 0;
        m_MF.Islotsize = false;
        m_MF.IsChain = false;
        return m_MF;
    }
}