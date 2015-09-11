<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditSinglePage.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Content.EditSinglePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>修改单页节点</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
    <script src="../JS/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<a href="NodeManage.aspx">节点管理</a>&gt;&gt;<span>修改单页节点</span>
	</div>
    <div class="clearbox"></div>
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" style="margin: 0 auto;">
        <tr align="center">
            <td class="spacingtitle" colspan="2">
                <asp:Label ID="LblTitle" runat="server" Text="修改单页节点" />
            </td>
        </tr>        
        <tr class="tdbg">
            <td style="width: 288px" class="tdbgleft">
                <strong>所属节点：</strong></td>
            <td>
                &nbsp;<asp:Label ID="LblNodeName" runat="server" Text=""></asp:Label>
                <asp:HiddenField ID="HdnParentId" Value="0" runat="server" />
                <asp:HiddenField ID="HdnDepth" Value="0" runat="server" />
                <asp:HiddenField ID="HdnOrderID" Value="0" runat="server" />
                <asp:HiddenField ID="HdnNodeID" Value="0" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>单页名称：</strong></td>
            <td>
                <asp:TextBox ID="TxtNodeName" runat="server" />
                <asp:RequiredFieldValidator ID="ValrNodeName" runat="server" ErrorMessage="单页名称不能为空！"
                    ControlToValidate="TxtNodeName" Display="Dynamic" SetFocusOnError="True" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>单页标识符：</strong></td>
            <td>
                <asp:TextBox ID="TxtNodeDir" runat="server" />
                <asp:RequiredFieldValidator ID="ValrNodeIdentifier" runat="server" ErrorMessage="标识符不能为空！"
                    ControlToValidate="TxtNodeDir" Display="Dynamic" SetFocusOnError="True" /></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 288px" class="tdbgleft">
                <strong>单页扩展名：</strong></td>
            <td><asp:RadioButtonList ID="RBLListEx" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True" Value="0">.html</asp:ListItem>
                <asp:ListItem Value="1">.htm</asp:ListItem>
                <asp:ListItem Value="2">.shtml</asp:ListItem>
                <asp:ListItem Value="3">.aspx</asp:ListItem>
            </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>指定单页模板：</strong>
            </td>
            <td align="left">
                <asp:TextBox ID="TxtTemplate" MaxLength="255" runat="server" Columns="50"></asp:TextBox>
                <input type="button" value="选择模板" onclick="WinOpenDialog('../Template/TemplateList.aspx?OpenerText='+escape('TxtTemplate')+'&FilesDir=',650,480)" class="button"/>
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
                <strong>栏目图片地址：</strong>
                </td>
            <td>
                <asp:TextBox ID="TxtNodePicUrl" MaxLength="255" runat="server" Columns="50"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 288px" class="tdbgleft">
                <strong>栏目提示：</strong></td>
            <td>
                <asp:TextBox ID="TxtTips" runat="server" Columns="60" Height="30" Width="500" Rows="2"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>单页说明：</strong><br />
                用于在单页页详细介绍单页信息，支持HTML</td>
            <td>
                <asp:TextBox ID="TxtDescription" runat="server" Columns="60" Height="65px" Width="360px"
                    Rows="2" TextMode="MultiLine" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>单页META关键词：</strong><br />
                针对搜索引擎设置的关键词<br />
            </td>
            <td>
                <asp:TextBox ID="TxtMetaKeywords" runat="server" Height="65px" Width="360px" Columns="60"
                    Rows="4" TextMode="MultiLine" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>单页META网页描述：</strong><br />
                针对搜索引擎设置的网页描述<br />
            </td>
            <td>
                <asp:TextBox ID="TxtMetaDescription" runat="server" Height="65px" Width="360px" Columns="60"
                    Rows="4" TextMode="MultiLine" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                &nbsp; &nbsp;
                <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp; &nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="BtnCancel" value="取消" onclick="window.location.href='NodeManage.aspx'" />                
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>
