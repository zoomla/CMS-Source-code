<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifySysOrderField.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.ModifySysOrderField" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>订单模型字段修改</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="text-center" colspan="2">修改系统会员模型字段        
            </td>
        </tr>
        <tr>
            <td>字段别名：</td>
            <td>
                <asp:TextBox class="form-control" ID="Alias" runat="server" MaxLength="20"></asp:TextBox>
                <font color="#ff0066">*</font><span class="tips"> 如：文章标题</span>
                <asp:HiddenField ID="hfType" runat="server" />
            </td>
        </tr>
        <tr>
            <td>字段名称：</td>
            <td>
                <asp:TextBox CssClass="form-control" ID="Name" MaxLength="50" runat="server"></asp:TextBox><font color="#ff0066">*</font><span class="tips">字段名由字母、数字、下划线组成，不接受空格</span>
            </td>
        </tr>

        <tr>
            <td>是否允许修改：</td>
            <td>
                <asp:RadioButtonList ID="NoEdit" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">是</asp:ListItem>
                    <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>

        <tr>
            <td>字段提示：</td>
            <td>
                <asp:TextBox class="form-control" ID="Tips" MaxLength="50" runat="server"></asp:TextBox><span class="tips">显示在字段别名下方作为重要提示的文字</span>
            </td>
        </tr>
        <tr>
            <td>字段描述：</td>
            <td>
                <asp:TextBox class="form-control" ID="Description" runat="server" Columns="40" Rows="6" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr style="display: none">
            <td>是否必填：</td>
            <td>
                <asp:RadioButtonList ID="IsNotNull" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="True">是</asp:ListItem>
                    <asp:ListItem Selected="True" Value="False">否</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr style="display: none">
            <td>是否列表显示：</td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="True">是</asp:ListItem>
                    <asp:ListItem Selected="True" Value="False">否</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>列表显示列宽度：</td>
            <td>
                <asp:TextBox class="form-control" ID="TextBox1" runat="server" Columns="10" MaxLength="4">10</asp:TextBox>%
            </td>
        </tr>
        <tbody id="tbSearch" runat="server">
            <tr>
                <td>是否在搜索表单显示：</td>
                <td>
                    <asp:RadioButtonList ID="IsSearchForm" runat="server" RepeatDirection="Horizontal"
                        CssClass="input1">
                        <asp:ListItem Value="True">是</asp:ListItem>
                        <asp:ListItem Selected="True" Value="False">否</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
        </tbody>
        <tr>
            <td>字段类型：</td>
            <td>
                <asp:RadioButtonList ID="Type" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" onclick="SelectModelType()">
                    <asp:ListItem Value="TextType" Selected="True">单行文本</asp:ListItem>
                    <asp:ListItem Value="MultipleTextType">多行文本(不支持Html)</asp:ListItem>
                    <asp:ListItem Value="MultipleHtmlType">多行文本(支持Html)</asp:ListItem>
                    <asp:ListItem Value="OptionType">单选项</asp:ListItem>
                    <asp:ListItem Value="ListBoxType">多选项</asp:ListItem>
                    <asp:ListItem Value="DateType">日期时间</asp:ListItem>
                    <asp:ListItem Value="PicType">图片</asp:ListItem>
                    <asp:ListItem Value="MultiPicType">多图片</asp:ListItem>
                    <asp:ListItem Value="SmallFileType">文件</asp:ListItem>
                    <asp:ListItem Value="FileType">多文件</asp:ListItem>
                    <asp:ListItem Value="NumType">数字</asp:ListItem>
                    <asp:ListItem Value="OperatingType">运行平台</asp:ListItem>
                    <asp:ListItem Value="SuperLinkType">超链接</asp:ListItem>
                    <asp:ListItem Value="GradeOptionType">多级选项</asp:ListItem>
                    <asp:ListItem Value="ColorType">颜色代码</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <!--  单行文本   -->
        <tbody id="DivTextType" runat="server">
            <tr>
                <td>文本框长度：</td>
                <td>
                    <asp:TextBox class="form-control" ID="TitleSize" runat="server" Columns="10" MaxLength="4">35</asp:TextBox></td>
            </tr>
            <tr>
                <td>是否为密码：</td>
                <td>
                    <asp:RadioButtonList ID="IsPassword" runat="server" RepeatDirection="Horizontal"
                        CssClass="input1">
                        <asp:ListItem Value="password">是</asp:ListItem>
                        <asp:ListItem Selected="True" Value="text">否</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td>默认值：</td>
                <td>
                    <asp:TextBox class="form-control" ID="TextType_DefaultValue" runat="server" Columns="10"></asp:TextBox></td>
            </tr>
        </tbody>
        <!--  多行文本(不支持Html)    -->
        <tbody id="DivMultipleTextType" style="display: none" runat="server">
            <tr>
                <td>显示的宽度：</td>
                <td>
                    <asp:TextBox class="form-control" ID="MultipleTextType_Width" runat="server" Columns="10" MaxLength="4">500</asp:TextBox>px</td>
            </tr>
            <tr>
                <td>显示的高度：</td>
                <td>
                    <asp:TextBox class="form-control" ID="MultipleTextType_Height" runat="server" Columns="10" MaxLength="4">200</asp:TextBox>px</td>
            </tr>
        </tbody>
        <!--  多行文本(支持Html)    -->
        <tbody id="DivMultipleHtmlType" style="display: none" runat="server">
            <tr>
                <td>编辑器类型：</td>
                <td>
                    <asp:DropDownList ID="IsEditor" runat="server">
                        <asp:ListItem Value="1">简洁型编辑器</asp:ListItem>
                        <asp:ListItem Value="2">标准型编辑器</asp:ListItem>
                        <asp:ListItem Value="3">增强型编辑器</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>显示的宽度：</td>
                <td>
                    <asp:TextBox class="form-control" ID="MultipleHtmlType_Width" runat="server" Columns="10" MaxLength="4">715</asp:TextBox>px</td>
            </tr>
            <tr>
                <td>显示的高度：</td>
                <td>
                    <asp:TextBox class="form-control" ID="MultipleHtmlType_Height" runat="server" Columns="10" MaxLength="4">400</asp:TextBox>px</td>
            </tr>
        </tbody>
        <!--  单选项    -->
        <tbody id="DivOptionType" style="display: none" runat="server">
            <tr>
                <td>分行键入每个选项：</td>
                <td>
                    <input id="Button3" type="button" value="从数据字典中选择选项" class="btn btn-primary" onclick="SelectDictionary('RadioType_Content')" /><input
                        id="Button5" type="button" value="添加数据字段" onclick='SubPage("RadioType_Content")' class="btn btn-primary" /><br />
                    <asp:TextBox class="form-control" ID="RadioType_Content" runat="server" TextMode="MultiLine" Columns="40" Rows="6"></asp:TextBox>
                    <span style="color: red">*</span></td>
            </tr>
            <tr>
                <td>默认值：</td>
                <td>
                    <asp:TextBox class="form-control" ID="RadioType_Default" runat="server"></asp:TextBox>
                    注：没有数据录入的默认值，与前台显示无关.</td>
            </tr>
            <tr>
                <td>显示选项：</td>
                <td>
                    <asp:RadioButtonList ID="RadioType_Type" runat="server">
                        <asp:ListItem Selected="True" Value="1">单选下拉列表框</asp:ListItem>
                        <asp:ListItem Value="2">单选按钮</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td>是否设置属性值：</td>
                <td>
                    <asp:RadioButtonList ID="RadioType_Property" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="True" Selected="True">是</asp:ListItem>
                        <asp:ListItem Value="False">否</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
        </tbody>
        <!--  多选项    -->
        <tbody id="DivListBoxType" style="display: none" runat="server">
            <tr>
                <td>分行键入每个选项：</td>
                <td>
                    <input id="Button4" type="button" value="从数据字典中选择选项" class="btn btn-primary" onclick="SelectDictionary('ListBoxType_Content')" /><input
                        id="Button6" type="button" value="添加数据字段" onclick='SubPage("ListBoxType_Content")' class="btn btn-primary" /><br />
                    <asp:TextBox class="form-control" ID="ListBoxType_Content" runat="server" TextMode="MultiLine" Columns="40" Rows="6"></asp:TextBox>
                    <span style="color: red">*</span></td>
            </tr>
            <tr>
                <td>显示选项：</td>
                <td>
                    <asp:RadioButtonList ID="ListBoxType_Type" runat="server">
                        <asp:ListItem Selected="True" Value="1">复选框</asp:ListItem>
                        <asp:ListItem Value="2">多选列表框</asp:ListItem>
                        <asp:ListItem Value="3">多填选项框</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
        </tbody>
        <!--  日期型    -->
        <tbody id="DivDateType" style="display: none" runat="server">
            <tr>
                <td>搜索范围：</td>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="text-center">
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="DateSearchType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">开始-结束日期</asp:ListItem>
                                    <asp:ListItem Value="0">自定范围</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox class="form-control" ID="DateSearchRang" runat="server" TextMode="MultiLine" Columns="40" Rows="6"></asp:TextBox><br />
                                <asp:RadioButtonList ID="DateSearchUnit" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0" Selected="True">小时</asp:ListItem>
                                    <asp:ListItem Value="1">天</asp:ListItem>
                                    <asp:ListItem Value="2">月</asp:ListItem>
                                    <asp:ListItem Value="3">年</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <font color="red">选择了自定范围时请输入此项内容,格式：范围名|范围<br /> 分行输入范围，单位选择 天 如：<br />
                                        3天内|0-3<br />7天内|0-7<br />7天-15天|7-15<br />
                                        1个月内|0-30</font>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
        <!--  图片类型    -->
        <tbody id="DivPicType" style="display: none" runat="server">
            <tr>
                <td>图片是否加水印：</td>
                <td>
                    <asp:RadioButtonList ID="RBLPicWaterMark" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td>允许上传的图片大小：</td>
                <td>
                    <asp:TextBox class="form-control" ID="TxtSPicSize" runat="server" Columns="5">1024</asp:TextBox>KB <span style="color: blue">提示：1 KB = 1024 Byte，1 MB = 1024 KB</span> <span style="color: red">*</span></td>
            </tr>
            <tr>
                <td>允许上传的图片类型：</td>
                <td>
                    <asp:TextBox class="form-control" ID="TxtPicExt" runat="server" Columns="30"></asp:TextBox><span style="color: red">*</span>
                    <span style="color: Blue">注：允许多个类型请用“|”号分割，如：jpg|gif|bmp等等</span></td>
            </tr>
        </tbody>
        <!--  多图片    -->
        <tbody id="DivMultiPicType" style="display: none" runat="server">
            <tr>
                <td>缩略图选项：</td>
                <td>
                    <asp:CheckBox ID="ChkThumb" runat="server" Text="是否保存缩略图地址" />&nbsp;&nbsp;保存缩略图地址字段：
                    <asp:TextBox class="form-control" ID="TxtThumb" runat="server" Columns="20"></asp:TextBox><span style="color: Red; display: none;">保存缩略图地址字段名不能与主字段名重复！</span></td>
            </tr>
            <tr>
                <td>图片是否加水印：</td>
                <td>
                    <asp:RadioButtonList ID="RBLWaterMark" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td>允许上传的图片大小：</td>
                <td>
                    <asp:TextBox class="form-control" ID="TxtPicSize" runat="server" Columns="5">1024</asp:TextBox>KB <span style="color: blue">提示：1 KB = 1024 Byte，1 MB = 1024 KB</span> <span style="color: red">*</span></td>
            </tr>
            <tr>
                <td>允许上传的图片类型：</td>
                <td>
                    <asp:TextBox class="form-control" ID="TextImageType" runat="server" Columns="30"></asp:TextBox>
                    <span style="color: red">*</span>
                    <span style="color: Blue">注：允许多个类型请用“|”号分割，如：jpg|gif|bmp等等</span></td>
            </tr>
        </tbody>
        <!--  文件类型    -->
        <tbody id="DivSmallFileType" style="display: none" runat="server">
            <tr>
                <td>允许上传的文件大小：
                </td>
                <td>
                    <asp:TextBox class="form-control" ID="TxtMaxFileSizes" runat="server" Columns="5">1024</asp:TextBox>KB <span style="color: blue">提示：1 KB = 1024 Byte，1 MB = 1024 KB</span> <span style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>允许上传的文件类型：    
                </td>
                <td>
                    <asp:TextBox class="form-control" ID="TxtUploadFileTypes" runat="server" Columns="30"></asp:TextBox>
                    <span style="color: red">*</span> <span style="color: Blue">注：允许多个类型请用“|”号分割，如：rar|jpg|gif|bmp等等</span>
                </td>
            </tr>
        </tbody>
        <!--  多文件类型    -->
        <tbody id="DivFileType" style="display: none" runat="server">
            <tr>
                <td>保存文件大小设置：
                </td>
                <td>
                    <asp:CheckBox ID="ChkFileSize" runat="server" Text="是否保存文件大小" Checked="True" /><br />
                    保存文件大小字段名：
                    <asp:TextBox class="form-control" ID="TxtFileSizeField" runat="server" Columns="20"></asp:TextBox><br />
                    <span style="color: Red; display: none;">注：字段名由字母、数字、下划线组成，并且仅能字母开头，不以下划线结尾。不能与已有字段名重复</span>
                </td>
            </tr>
            <tr>
                <td>允许上传的文件大小：
                </td>
                <td>
                    <asp:TextBox class="form-control" ID="TxtMaxFileSize" runat="server" Columns="5">1024</asp:TextBox>KB <span style="color: blue">提示：1 KB = 1024 Byte，1 MB = 1024 KB</span> <span style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>允许上传的文件类型：
                </td>
                <td>
                    <asp:TextBox class="form-control" ID="TxtUploadFileType" runat="server" Columns="30"></asp:TextBox>
                    <span style="color: red">*</span> <span style="color: Blue">注：允许多个类型请用“|”号分割，如：jpg|gif|bmp等等</span>
                </td>
            </tr>
        </tbody>
        <!--  运行平台    -->
        <tbody id="DivOperatingType" style="display: none" runat="server">
            <tr>
                <td>分行键入&nbsp;&nbsp;<br />
                    每个平台选项：</td>
                <td>
                    <asp:TextBox class="form-control" ID="TxtOperatingOption" runat="server" TextMode="MultiLine" Columns="40" Rows="6"></asp:TextBox>
                    <span style="color: red">*</span> <span style="color: blue">注：一行一个默认项</span></td>
            </tr>
            <tr>
                <td>文本框长度：</td>
                <td>
                    <asp:TextBox class="form-control" ID="OperatingType_TitleSize" runat="server" Columns="10" MaxLength="4">35</asp:TextBox><span style="color: red">*</span></td>
            </tr>
            <tr>
                <td>默认值：</td>
                <td>
                    <asp:TextBox class="form-control" ID="OperatingType_DefaultValue" runat="server" Columns="30"></asp:TextBox><span style="color: green">例：Win2000/XP/Win2003</span> </td>
            </tr>
        </tbody>
        <!--  超链接    -->
        <tbody id="DivSuperLinkType" style="display: none" runat="server">
            <tr>
                <td>文本框长度：</td>
                <td>
                    <asp:TextBox class="form-control" ID="SuperLinkType_TitleSize" runat="server" Columns="10" MaxLength="4">50</asp:TextBox></td>
            </tr>
            <tr>
                <td>默认值：</td>
                <td>
                    <asp:TextBox class="form-control" ID="SuperLinkType_DefaultValue" runat="server" Columns="30"></asp:TextBox><span style="color: green">例：http://127.0.0.1/</span></td>
            </tr>
        </tbody>
        <!--  多级选项    -->
        <tbody id="DivGradeOptionType" style="display: none" runat="server">
            <tr>
                <td>选择分级数据分类：</td>
                <td>
                    <asp:DropDownList ID="GradeOptionType_Cate" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>选项下拉框排列格式化：</td>
                <td>
                    <asp:RadioButtonList ID="GradeOptionType_Direction" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Selected="True">一行横排</asp:ListItem>
                        <asp:ListItem Value="1">多行竖排</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </tbody>
        <!--  数字类型    -->
        <tbody id="DivNumType" style="display: none" runat="server">
            <tr>
                <td>文本框长度：</td>
                <td>
                    <asp:TextBox class="form-control" ID="NumberType_TitleSize" runat="server" Columns="10" MaxLength="4">35</asp:TextBox></td>
            </tr>
            <tr>
                <td>数字类型：</td>
                <td>
                    <asp:RadioButtonList ID="NumberType_Style" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1" onclick="show(this.value);">整数</asp:ListItem>
                        <asp:ListItem Value="2" onclick="show(this.value);">小数</asp:ListItem>
                        <asp:ListItem Value="3" onclick="show(this.value);">货币</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>

            <tr id="showdec" style="display: none">
                <td>
                    <strong>小数位：</strong>
                </td>
                <td>
                    <asp:TextBox class="form-control" ID="txtdecimal" runat="server" Text="0"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>默认值：</td>
                <td>
                    <asp:TextBox class="form-control" ID="NumberType_DefaultValue" runat="server" Columns="10"></asp:TextBox></td>
            </tr>
            <tr>
                <td>搜索设置：
                </td>
                <td>
                    <asp:RadioButtonList ID="NumSearchType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Selected="True">精确搜索</asp:ListItem>
                        <asp:ListItem Value="0">范围搜索</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td>搜索范围：                    
                </td>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="text-center">
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="NumRangType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">下限-上限</asp:ListItem>
                                    <asp:ListItem Value="0">自定范围</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <font color="red">选择了范围搜索时，此设置才有效</font>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox class="form-control" ID="NumSearchRange" runat="server" TextMode="MultiLine" Columns="40" Rows="6"></asp:TextBox><br />
                            </td>
                            <td>
                                <font color="red">选择了自定范围时请输入此项内容,格式：范围名|范围<br /> 分行输入范围如：<br />
                                        500元以下|0-500<br />500元-1000元|500-1000<br />1000元以上|1000</font>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
        <!----颜色代码---->
        <tbody id="DivColorType" style="display: none" runat="server">
            <tr>
                <td>默认颜色值：
                </td>
                <td>
                    <asp:TextBox class="form-control" ID="ColorDefault" runat="server" MaxLength="7" Columns="7" Style="font-size: 9pt; height: 15px;"></asp:TextBox>
                    <img onclick="SelectColor(this,'ColorDefault');" src="/App_Themes/AdminDefaultTheme/Images/selectclolor.gif" align="absmiddle" style="border-width: 0px; cursor: pointer" />
                </td>
            </tr>
        </tbody>
        <tr>
            <td></td>
            <td height="50">
                <asp:Button ID="Button1" runat="server" Text="修改字段" OnClientClick="return isok()" OnClick="Button1_Click" class="btn btn-primary" />
                &nbsp;&nbsp;
                <input id="Button2" type="button" runat="server" value="返回字段列表" onclick="javascript: window.location.href = 'SystemOrderModel.aspx?Type=shop'" class="btn btn-primary" />
                <asp:HiddenField ID="HdfFieldID" runat="server" />
                <asp:HiddenField ID="hdfOrder" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Common.js"></script>
    <script type="text/javascript" src="/JS/ModelField.js"></script>
    <script>
        function show(chvalue) {
            if (chvalue == '1') {
                document.getElementById('showdec').style.display = 'none';
            }
            else {
                document.getElementById('showdec').style.display = '';
            }
        }
    </script>
</asp:Content>