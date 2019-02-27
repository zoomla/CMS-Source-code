<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApprovalQuote.aspx.cs" Inherits="MIS_Target_ApprovalQuote" %>
<!DOCTYPE html>
<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>审批</title>
    <link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />
    <script>
        function ProTypes(obj) {
            document.getElementById("ProID").value = obj.value;
        }
        function Cquote() {
            parent.document.getElementById("QuoteContent").style.display = "none";
        }
    </script>
</head>
<body onload="ProTypes('sas')">
<form id="qform1" runat="server">
<div class="Qutote_Tit">
    <span class="rights"><a href="javascript:void(0)" onclick="Cquote()">关闭</a></span>
<asp:Label ID="QTitle" runat="server" Text="Label"></asp:Label>
</div>
<div class="Qutote_li">
<%--<div class="search" style="border-bottom:1px dashed #ccc; padding-bottom:10px;"> 
<asp:TextBox ID="TxtKey" CssClass="b_input" runat="server" Text="请输入关键字" Width="260" onclick="setEmpty(this)" onblur="settxt(this)"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="" CssClass="bottom_bg" OnClick="Button1_Click" />
</div>--%>
<ul> <asp:Repeater ID="Repeater1" runat="server">
<ItemTemplate>
<li><input type="radio" name="types" onclick="ProTypes(this)"  value="<%#Eval("ID") %>" id='<%#Eval("ID") %>'/>
    <a href="ProjectView.aspx?ID=<%#Eval("ID") %>"><%#Eval("content") %></a> </li>
    </ItemTemplate>
    </asp:Repeater>  
</ul>
    <table>
</table>
<div class="bot_bor"><asp:Button Text="引用"  runat="server" ID="BtnCommit" OnClientClick="Cquote()" CssClass="i_bottom"  OnClick="Button1_Click"/></div>
</div>
<asp:HiddenField ID="ProID" runat="server" />
<asp:HiddenField ID="HiddenField1" runat="server" />
</form>
<div id="WirteJs"></div>
</body>
</html>
