<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditField.aspx.cs" Inherits="ZoomLa.WebSite.Manage.User.EditField" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>修改字段</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/Common.js"></script>
    <script type="text/javascript" src="../js/ModelField.js"></script>   
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span><a href="UserManage.aspx">会员管理</a></span> &gt;&gt;<a href="UserModelManage.aspx">会员模型管理</a>
    &gt;&gt; <a href="UserModelField.aspx?ModelId=<%=Request.QueryString["ModelID"] %>">字段列表</a>
    &gt;&gt; 修改[<asp:Label ID="lblModel" runat="server" Text="Label"></asp:Label>模型]字段
	</div>
    <div class="clearbox"></div>
    <table class="border" cellspacing="1" cellpadding="0" width="100%" border="0" align="center">
        <tr class="tdbg">
            <td class="tdbgleft">
                字段别名：
            </td>
            <td>
                <asp:TextBox ID="Alias" runat="server" MaxLength="20"></asp:TextBox><font
                    color="#ff0066">*</font><span class="tips"> 如：文章标题</span></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                字段名称：
            </td>
            <td>
                <asp:TextBox ID="Name" MaxLength="50" runat="server"></asp:TextBox><font
                    color="#ff0066">*</font><span class="tips">字段名由字母、数字、下划线组成</span>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                字段提示：
            </td>
            <td>
                <asp:TextBox ID="Tips" MaxLength="50" runat="server"></asp:TextBox><span class="tips">显示在字段别名下方作为重要提示的文字</span>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                字段描述：
            </td>
            <td>
                <asp:TextBox ID="Description" runat="server" Columns="40" Rows="6" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                是否必填：
            </td>
            <td>
                <asp:RadioButtonList ID="IsNotNull" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="True">是</asp:ListItem>
                    <asp:ListItem Selected="True" Value="False">否</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                是否在搜索表单显示：
            </td>
            <td>
                <asp:RadioButtonList ID="IsSearchForm" runat="server" RepeatDirection="Horizontal"
                    CssClass="input1">
                    <asp:ListItem Value="True">是</asp:ListItem>
                    <asp:ListItem Selected="True" Value="False">否</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                字段类型：
            </td>
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
                    <asp:ListItem Value="FileType">文件</asp:ListItem>
                    <asp:ListItem Value="NumType">数字</asp:ListItem>
                    <asp:ListItem Value="OperatingType">运行平台</asp:ListItem>
                    <asp:ListItem Value="SuperLinkType">超链接</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <!--  单行文本   -->
        <tbody id="DivTextType" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">文本框长度：</td>
                <td><asp:TextBox ID="TitleSize" runat="server" Columns="10" MaxLength="4">35</asp:TextBox></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">是否为密码：</td>
                <td><asp:RadioButtonList ID="IsPassword" runat="server" RepeatDirection="Horizontal"
                    CssClass="input1">
                    <asp:ListItem Value="password">是</asp:ListItem>
                    <asp:ListItem Selected="True" Value="text">否</asp:ListItem>
                </asp:RadioButtonList></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">默认值：</td>
                <td><asp:TextBox ID="TextType_DefaultValue" runat="server" Columns="10"></asp:TextBox></td>
            </tr>
        </tbody>
        <!--  多行文本(不支持Html)    -->
        <tbody id="DivMultipleTextType" style="display:none" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">显示的宽度：</td>
                <td><asp:TextBox ID="MultipleTextType_Width" runat="server" Columns="10" MaxLength="4">500</asp:TextBox>px</td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">显示的高度：</td>
                <td><asp:TextBox ID="MultipleTextType_Height" runat="server" Columns="10" MaxLength="4">200</asp:TextBox>px</td>
            </tr>
        </tbody>
        <!--  多行文本(支持Html)    -->
        <tbody id="DivMultipleHtmlType" style="display:none" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">编辑器类型：</td>
                <td><asp:DropDownList ID="IsEditor" runat="server">
                    <asp:ListItem Value="1">简洁型编辑器</asp:ListItem>
                    <asp:ListItem Value="2">标准型编辑器</asp:ListItem>
                    <asp:ListItem Value="3">增强型编辑器</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">显示的宽度：</td>
                <td><asp:TextBox ID="MultipleHtmlType_Width" runat="server" Columns="10" MaxLength="4">715</asp:TextBox>px</td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">显示的高度：</td>
                <td><asp:TextBox ID="MultipleHtmlType_Height" runat="server" Columns="10" MaxLength="4">400</asp:TextBox>px</td>
            </tr>
        </tbody>
        <!--  单选项    -->
        <tbody id="DivOptionType" style="display:none" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">分行键入每个选项：</td>
                <td><input id="Button3" type="button" value="从数据字典中选择选项" class="btn" onclick="SelectDictionary('RadioType_Content')" /><br />
                    <asp:TextBox ID="RadioType_Content" runat="server" TextMode="MultiLine" Columns="40" Rows="6"></asp:TextBox> <span style="color: red">*</span></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">默认值：</td>
                <td><asp:TextBox ID="RadioType_Default" runat="server"></asp:TextBox> 注：没有数据录入的默认值，与前台显示无关.</td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">显示选项：</td>
                <td><asp:RadioButtonList ID="RadioType_Type" runat="server">
                    <asp:ListItem Selected="True" Value="1">单选下拉列表框</asp:ListItem>
                    <asp:ListItem Value="2">单选按钮</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    是否设置属性值：</td>
                <td>
                    <asp:RadioButtonList ID="RadioType_Property" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="True" Selected="True">是</asp:ListItem>
                    <asp:ListItem Value="False">否</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
        </tbody>
        <!--  多选项    -->
        <tbody id="DivListBoxType" style="display:none" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">分行键入每个选项：</td>
                <td><input id="Button4" type="button" value="从数据字典中选择选项" class="btn" onclick="SelectDictionary('ListBoxType_Content')" /><br />
                    <asp:TextBox ID="ListBoxType_Content" runat="server" TextMode="MultiLine" Columns="40" Rows="6"></asp:TextBox> <span style="color: red">*</span></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">显示选项：</td>
                <td><asp:RadioButtonList ID="ListBoxType_Type" runat="server">
                    <asp:ListItem Selected="True" Value="1">复选框</asp:ListItem>
                    <asp:ListItem Value="2">多选列表框</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
        </tbody>
        <!--  日期型    -->
        <tbody id="DivDateType" style="display:none" runat="server">
            
        </tbody>
        <!--  图片类型    -->
        <tbody id="DivPicType" style="display:none" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">图片是否加水印：</td>
                <td>
                    <asp:RadioButtonList ID="RBLPicWaterMark" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    允许上传的图片大小：</td>
                <td>
                    <asp:TextBox ID="TxtSPicSize" runat="server" Columns=5>1024</asp:TextBox>KB <span style="color: blue">
                        提示：1 KB = 1024 Byte，1 MB = 1024 KB</span> <span style="color: red">*</span></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    允许上传的图片类型：</td>
                <td>
                    <asp:TextBox ID="TxtPicExt" runat="server" Columns="30"></asp:TextBox><span style="color: red">*</span>
                <span style="color: Blue">注：允许多个类型请用“|”号分割，如：jpg|gif|bmp等等</span></td>
            </tr>
        </tbody>
        <!--  多图片    -->
        <tbody id="DivMultiPicType" style="display:none" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">缩略图选项：</td>
                <td>
                    <asp:CheckBox ID="ChkThumb" runat="server" Text="是否保存缩略图地址" />&nbsp;&nbsp;保存缩略图地址字段：
                    <asp:TextBox ID="TxtThumb" runat="server" Columns="20"></asp:TextBox><span style="color:Red;display:none;">保存缩略图地址字段名不能与主字段名重复！</span></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">图片是否加水印：</td>
                <td>
                    <asp:RadioButtonList ID="RBLWaterMark" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    允许上传的图片大小：</td>
                <td>
                    <asp:TextBox ID="TxtPicSize" runat="server" Columns=5>1024</asp:TextBox>KB <span style="color: blue">
                        提示：1 KB = 1024 Byte，1 MB = 1024 KB</span> <span style="color: red">*</span></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    允许上传的图片类型：</td>
                <td>
                    <asp:TextBox ID="TextImageType" runat="server" Columns="30"></asp:TextBox> <span style="color: red">*</span>
                <span style="color: Blue">注：允许多个类型请用“|”号分割，如：jpg|gif|bmp等等</span></td>
            </tr>
        </tbody>            
        <!--  文件类型    -->
        <tbody id="DivFileType" style="display:none" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">保存文件大小设置：</td>
                <td>
                    <asp:CheckBox ID="ChkFileSize" runat="server" Text="是否保存文件大小" Checked="True" /><br/>保存文件大小字段名：
                    <asp:TextBox ID="TxtFileSizeField" runat="server" Columns="20"></asp:TextBox><br/>
                    <span style="color:Red;display:none;">注：字段名由字母、数字、下划线组成，并且仅能字母开头，不以下划线结尾。不能与已有字段名重复</span></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    允许上传的文件大小：</td>
                <td>
                    <asp:TextBox ID="TxtMaxFileSize" runat="server" Columns=5>1024</asp:TextBox>KB <span style="color: blue">
                        提示：1 KB = 1024 Byte，1 MB = 1024 KB</span> <span style="color: red">*</span></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    允许上传的文件类型：</td>
                <td>
                    <asp:TextBox ID="TxtUploadFileType" runat="server" Columns="30"></asp:TextBox> <span style="color: red">*</span>
                <span style="color: Blue">注：允许多个类型请用“|”号分割，如：jpg|gif|bmp等等</span></td>
            </tr>
        </tbody>
        <!--  运行平台    -->
        <tbody id="DivOperatingType" style="display:none" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">分行键入&nbsp;&nbsp;<br />
                    每个平台选项：</td>
                <td>
                    <asp:TextBox ID="TxtOperatingOption" runat="server" TextMode="MultiLine" Columns="40" Rows="6"></asp:TextBox> <span style="color: red">*</span> <span style="color: blue">注：一行一个默认项</span></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">文本框长度：</td>
                <td><asp:TextBox ID="OperatingType_TitleSize" runat="server" Columns="10" MaxLength="4">35</asp:TextBox><span style="color: red">*</span></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">默认值：</td>
                <td><asp:TextBox ID="OperatingType_DefaultValue" runat="server" Columns="30"></asp:TextBox><span style="color: green">例：Win2000/XP/Win2003</span> </td>
            </tr>
        </tbody>
        <!--  超链接    -->
        <tbody id="DivSuperLinkType" style="display:none" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">文本框长度：</td>
                <td><asp:TextBox ID="SuperLinkType_TitleSize" runat="server" Columns="10" MaxLength="4">50</asp:TextBox></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">默认值：</td>
                <td><asp:TextBox ID="SuperLinkType_DefaultValue" runat="server" Columns="30"></asp:TextBox><span style="color: green">例：http://127.0.0.1/</span></td>
            </tr>
        </tbody>            
        <!--  数字类型    -->
        <tbody id="DivNumType" style="display:none" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">文本框长度：</td>
                <td><asp:TextBox ID="NumberType_TitleSize" runat="server" Columns="10" MaxLength="4">35</asp:TextBox></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">数字类型：</td>
                <td>
                    <asp:RadioButtonList ID="NumberType_Style" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">整数</asp:ListItem>
                        <asp:ListItem Value="2">小数</asp:ListItem>
                        <asp:ListItem Value="3">货币</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">默认值：</td>
                <td><asp:TextBox ID="NumberType_DefaultValue" runat="server" Columns="10"></asp:TextBox></td>
            </tr>
        </tbody>
        <tr class="tdbg">
            <td class="tdbgleft">
            </td>
            <td height="50">
                <asp:Button ID="Button1" runat="server" Text=" 修改字段 " CssClass="btn" OnClientClick="return isok()" OnClick="Button1_Click" />
                &nbsp;&nbsp;
                <input id="Button2" type="button" value=" 返回字段列表 " class="btn" onclick="javascript:window.location.href='UserModelField.aspx?ModelID=<%=Request.QueryString["ModelID"] %>'" />
                <asp:HiddenField ID="HdfModelID" runat="server" /><asp:HiddenField ID="HdfFieldID" runat="server" /><asp:HiddenField ID="hdfOrder" runat="server" />
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>
