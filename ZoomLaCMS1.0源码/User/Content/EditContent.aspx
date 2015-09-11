<%@ Page Language="C#" AutoEventWireup="true" validateRequest="false" CodeFile="EditContent.aspx.cs" Inherits="ZoomLa.WebSite.User.Content.EditContent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>更新内容</title>
    <link href="/App_Themes/UserDefaultTheme/Default.css" type="text/css" rel="stylesheet" />
    <link href="/App_Themes/UserDefaultTheme/xtree.css" type="text/css" rel="stylesheet" />
    <link href="../css/user.css" rel="stylesheet" type="text/css" />
    <link href="../css/default1.css" rel="stylesheet" type="text/css" />
    <script src="../../Common/Common.js" type="text/javascript"></script>
    <script src="../../Common/RiQi.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
        <div class="r_n_pic"></div>
        <span><asp:Label ID="lblNodeName" runat="server" Text="Label"></asp:Label></span><span> &gt;&gt; </span><span>修改内容：<asp:Label ID="lblAddContent" runat="server" Text="Label"></asp:Label></span>
    </div>
    <div class="clearbox"></div>
    
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr align="center">
            <td colspan="3" class="spacingtitle">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td>
        </tr>        
    </table>
    <div class="clearbox"></div>
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tbody id="Tabs0">
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 20%" align="right">                    
                    所属节点：</td>
                <td class="bqright">
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></td>
             </tr>             
             <tr class="tdbg">
                <td class="tdbgleft" style="width: 20%" align="right">                    
                    内容标题：</td>
                <td class="bqright">
                    <asp:TextBox ID="txtTitle" runat="server" Text='' Width="35%" MaxLength="30"></asp:TextBox>
                    <span><font color="red">*</font></span>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtTitle">内容标题必填</asp:RequiredFieldValidator></td>
             </tr>
             <asp:Literal ID="ModelHtml" runat="server"></asp:Literal>             
        </tbody>        
        <tr class="tdbgbottom" align="center">
            <td colspan="2">
                <asp:HiddenField ID="HdnItem" runat="server" />
                <asp:TextBox ID="FilePicPath" runat="server" Text="fbangd" Style="display: none"></asp:TextBox>
                <asp:Button ID="EBtnSubmit" Text="更新项目" OnClick="EBtnSubmit_Click" runat="server" />
                &nbsp;                
                <asp:Button ID="BtnBack" runat="server" Text="返　回" OnClick="BtnBack_Click" UseSubmitBehavior="False"
                    CausesValidation="False" />
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>
