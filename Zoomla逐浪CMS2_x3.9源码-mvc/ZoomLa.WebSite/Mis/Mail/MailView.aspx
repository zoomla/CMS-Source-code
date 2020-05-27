<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailView.aspx.cs" Inherits="ZoomLaCMS.MIS.Mail.MailView" MasterPageFile="~/Common/Master/Empty.master" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>阅读邮件</title>
<link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />
<script type="text/javascript" src="/js/ajaxrequest.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div>
<strong><asp:Label ID="labSubject"  runat="server"></asp:Label></strong><br />
发件人 :   <asp:Label ID="labFrom"  runat="server"></asp:Label>   <br />
时间 :   <asp:Label ID="labTime"  runat="server"></asp:Label><br />
收件人 :  <asp:Label ID="labUserMail"  runat="server"></asp:Label>   
<br />
<asp:TextBox ID="richtbxMailContentReview" runat="server" TextMode="MultiLine" Rows="50" Columns="80"></asp:TextBox>
<div><ul>
<asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
    <ItemTemplate>
<li>附件：<asp:LinkButton ID="lkbDrw" runat="server" CommandName="lkbDrw" CommandArgument='<%#Eval("name") %>' Text='<%#Eval("name") %>'></asp:LinkButton> </li>
</ItemTemplate></asp:Repeater>
</ul></div> 
<%-- <asp:TextBox ID="richtbxMailContentReview" TextMode="MultiLine" runat="server" Columns="100" Rows="50"></asp:TextBox>--%> 
    <asp:HiddenField ID="txbSendTo" runat="server"></asp:HiddenField> 
<asp:HiddenField ID="txbSubject" runat="server"></asp:HiddenField> 
<asp:HiddenField ID="richtbxBody" runat="server"></asp:HiddenField> 
    
  <asp:Button ID="btnReplyCurrentMail" runat="server" Text="回复" OnClick="btnReplyCurrentMail_Click" CssClass="i_bottom" ></asp:Button>
&nbsp;<asp:Button ID="Del" OnClick="Del_Click" CssClass="i_bottom" runat="server" OnClientClick="return confirm('确认要删除此邮件吗？')" Text="删除" />
</div> 
</asp:Content>

