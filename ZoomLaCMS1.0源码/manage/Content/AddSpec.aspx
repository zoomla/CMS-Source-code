<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSpec.aspx.cs" Inherits="manage_Content_AddSpec" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>添加专题</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
    <script src="../JS/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<a href="SpecialManage.aspx">专题类别管理</a>&gt;&gt;<span>添加专题</span>
	</div>
    <div class="clearbox"></div>
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" style="margin: 0 auto;">
        <tr align="center">
            <td class="spacingtitle" colspan="2">
                <asp:Label ID="LblTitle" runat="server" Text="添加专题" />
            </td>
        </tr> 
        <tr class="tdbg">
            <td style="width: 288px" class="tdbgleft">
                <strong>所属专题类别：</strong></td>
            <td>
                <asp:Label ID="lblCate" runat="server" Text="Label"></asp:Label>
                <asp:HiddenField ID="HdnCateID" Value="0" runat="server" /> 
            </td>
        </tr>       
        <tr class="tdbg">
            <td style="width: 288px" class="tdbgleft">
                <strong>专题名：</strong></td>
            <td>
                <asp:TextBox ID="TxtSpecName" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="专题不能为空!"
                    ControlToValidate="TxtSpecName" Display="Dynamic" SetFocusOnError="True" />
                <asp:HiddenField ID="HdnSpecID" Value="0" runat="server" />                
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>专题类别目录：</strong></td>
            <td>
                <asp:TextBox ID="TxtSpecDir" runat="server" />
                <asp:RequiredFieldValidator ID="ValrCateDir" runat="server" ErrorMessage="专题目录不能为空!"
                    ControlToValidate="TxtSpecDir" Display="Dynamic" SetFocusOnError="True" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>专题类别描述：</strong></td>
            <td>
                <asp:TextBox ID="TxtSpecDesc" runat="server" Columns="50" Height="30" Width="500" Rows="5" TextMode="MultiLine" />
            </td>
        </tr>        
        <tr class="tdbg">
            <td style="width: 288px" class="tdbgleft">
                <strong>打开方式：</strong></td>
            <td>
                <asp:RadioButtonList ID="RBLOpenType" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">原窗口打开</asp:ListItem>
                    <asp:ListItem Value="1">新窗口打开</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 288px" class="tdbgleft">
                <strong>专题列表页扩展名：</strong>
                </td>
            <td>
                <asp:RadioButtonList ID="RBLFileExt" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">.html</asp:ListItem>
                    <asp:ListItem Value="1">.htm</asp:ListItem>
                    <asp:ListItem Value="2">.shtml</asp:ListItem>
                    <asp:ListItem Value="3">.aspx</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 288px" class="tdbgleft">
                <strong>专题列表页文件名规则：</strong>
                </td>
            <td>
                <asp:RadioButtonList ID="RBLListFileRule" runat="server">
                    <asp:ListItem Selected="True" Value="1">专题类别目录/专题目录/List_专题ID_PageIndex</asp:ListItem>
                    <asp:ListItem Value="2">专题类别目录/List/List_专题ID_PageIndex</asp:ListItem>
                    <asp:ListItem Value="3">Special/List_专题ID_PageIndex</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 288px" class="tdbgleft">
                <strong>专题列表页模板：</strong></td>
            <td>
                <asp:TextBox ID="TxtListTemplate" runat="server" Columns="50"></asp:TextBox>
                <input type="button" value="选择模板" onclick="WinOpenDialog('../Template/TemplateList.aspx?OpenerText='+escape('TxtListTemplate')+'&FilesDir=',650,480)" class="button"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="专题列表页模板不能为空!"
                    ControlToValidate="TxtListTemplate" Display="Dynamic" SetFocusOnError="True" /></td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                &nbsp; &nbsp;
                <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp; &nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="BtnCancel" value="取消" onclick="window.location.href='SpecialManage.aspx'" />                
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>
