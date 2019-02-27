<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageSend.aspx.cs" Inherits="ZoomLa.WebSite.User.MessageSend" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>会员中心 >> 发送短消息</title>
    <link href="/App_Themes/UserDefaultTheme/Default.css" type="text/css" rel="stylesheet" />
    <link href="/App_Themes/UserDefaultTheme/xtree.css" type="text/css" rel="stylesheet" />
    <link href="../css/user.css" rel="stylesheet" type="text/css" />
    <link href="../css/default1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">    
<div>
    <div class="r_navigation">
    <div class="r_n_pic"></div>
    您现在的位置：<span id="YourPosition"><span><a title="网站首页" href="/"><asp:Label ID="LblSiteName" runat="server" Text="Label"></asp:Label></a></span><span> &gt;&gt; </span><span><a title="会员中心" href="/User/Default.aspx">会员中心</a></span><span> &gt;&gt; </span><span><a title="短消息管理" href="/User/Message/MessageSend.aspx">发送短信</a></span></span></div>
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>发送网站短消息</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" style="height: 28px; width: 15%;">
                &nbsp;收件人：</td>
            <td align="left">
                <asp:TextBox ID="TxtInceptUser" runat="server" Width="426px"></asp:TextBox></td>
        </tr>        
        <tr class="tdbg">
            <td align="right" style="height: 28px; width: 15%;">
                短消息主题：</td>
            <td align="left">
                <asp:TextBox ID="TxtTitle" runat="server" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ValrTitle" runat="server" ControlToValidate="TxtTitle"
                    ErrorMessage="短消息主题不能为空" Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" style="height: 23px; width: 15%;">
                短消息内容：</td>
            <td align="left">
                <asp:TextBox ID="EditorContent" runat="server" Rows="10" TextMode="MultiLine" Width="99%"></asp:TextBox>                
                <asp:RequiredFieldValidator ID="ValrContent" runat="server" ControlToValidate="EditorContent"
                    ErrorMessage="短消息内容不能为空" Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr align="center" class="tdbg">
            <td colspan="2" style="height: 50px;" align="center">
                <asp:HiddenField ID="HdnMessageID" runat="server" />
                &nbsp;
                <asp:Button ID="BtnSend" runat="server" Text="发送" OnClick="BtnSend_Click" />&nbsp;                
                &nbsp;<asp:Button ID="BtnReset" runat="server" Text="清除" OnClick="BtnReset_Click" /></td>
        </tr>
     <tr align="center" class="tdbg">
         <td colspan="2" style="height: 50px">
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" />
         </td>
     </tr>
    </table>
    </div>
    </form>
</body>
</html>
