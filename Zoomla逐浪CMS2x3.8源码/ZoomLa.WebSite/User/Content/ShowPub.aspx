<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="ShowPub.aspx.cs" Inherits="User_Content_ShowPub" ClientIDMode="Static" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>内容预览</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="content" data-ban="cnt"></div>
<div class="container margin_t5">
<ol class="breadcrumb">
<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
<li>我的互动</li>
<li class="active">信息预览</li>
</ol>
</div>
<div class="container">
<ul class="us_seta" style="margin-top: 10px;">
<asp:Label ID="Label1" runat="server" Text="信息预览"></asp:Label>
<asp:DetailsView ID="DetailsView1" runat="server" Width="100%" CellPadding="4" GridLines="None"
Font-Size="12px" Style="margin-bottom: 3px; margin-top: 2px;" CssClass="r_navigation">
<Fields></Fields>
<FooterStyle Font-Bold="True" BackColor="#FFFFFF" />
<CommandRowStyle Font-Bold="True" CssClass="tdbgleft" />
<RowStyle />
<FieldHeaderStyle Font-Bold="True" />
<PagerStyle HorizontalAlign="Center" />
<HeaderStyle Font-Bold="True" />
<EditRowStyle />
<AlternatingRowStyle />
</asp:DetailsView>
</ul>
<asp:HiddenField ID="hfUsername" runat="server" />
<ul class="us_seta" style="margin-top: 10px;">
<h1 style="text-align: center">
<asp:Label ID="Label2" runat="server" Text="互动信息"></asp:Label></h1>
<asp:DataList ID="Egv" runat="server" Width="100%" DataKeyField="ID">
<ItemTemplate>
<table class="table table-striped table-bordered table-hover">
<tr>
<td rowspan="3" valign="top" width="20%">
<%#GetImg(Eval("PubUserID","{0}"), Eval("PubUserName","{0}"))%>
</td>
<td colspan="2">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
<tr>
<td align="left">
<%#Eval("PubAddTime") %>
</td>
<td align="right">
<div id="info">
<%
if (GetisLoginUser())
{
%>
<a href="javascript:void(0)" onmouseover="showmenu(event,'<div class=menuitems><a href=\'javascript:void(0)\' onclick=\'gotourl(<%# Eval("Optimal") %>,<%=PubID %>,<%=GID %>,<%#Eval("ID") %>)\'><%# Eval("Optimal") == null || Eval("Optimal","{0}") != "1" ? "设置最佳" : "取消最佳"%></a></div><div class=menuitems><a href=\'javascript:void(0);\' onclick=\'setdb(<%# Eval("Optimal") %>,<%=PubID %>,<%=GID %>,<%#Eval("ID") %>)\' target=main_right><%# Eval("Optimal") == null || Eval("Optimal","{0}") != "2" ? "设置达标" : "取消达标"%></a></div><div class=menuitems><a href=\'javascript:void(0);\' onclick=\'setnodb(<%# Eval("Optimal") %>,<%=PubID %>,<%=GID %>,<%#Eval("ID") %>)\' target=main_right><%# Eval("Optimal") == null || Eval("Optimal","{0}") != "-1" ? "设为不达标" : "取消不达标"%></a></div>')">信息设置</a> <a href='javascript:void(0);' onclick="javascript:window.open('Reply.aspx?ID=<%#Eval("ID") %>&pubid=<%=PubID %>','', 'width=600,height=300,resizable=0,scrollbars=yes');">回复</a>

<asp:Label ID="Label3" runat="server" Text=""></asp:Label>
<%} %>
</div>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td colspan="2" valign="top">&nbsp;&nbsp;&nbsp;&nbsp;<strong>
<%#GetIco(Eval("Optimal","{0}")) %><br />
<%#Eval("PubTitle")%></strong>
<br />
<br />
&nbsp;&nbsp;<%#Eval("PubContent")%><br />
<br />
<%#GetTable(Eval("ID","{0}"), Eval("PubContentid","{0}"))%>
</td>
</tr>
<tr>
<td>
<%#GetTable(Eval("ID","{0}")) %>
</td>
</tr>
</table>
</ItemTemplate>
</asp:DataList>
<div style="text-align: center;">
共
<asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
条信息
<asp:Label ID="Toppage" runat="server" Text="" />
<asp:Label ID="Nextpage" runat="server" Text="" />
<asp:Label ID="Downpage" runat="server" Text="" />
<asp:Label ID="Endpage" runat="server" Text="" />
页次：<asp:Label ID="Nowpage" runat="server" Text="" />/<asp:Label ID="PageSize" runat="server"
Text="" />页
<asp:Label ID="pagess" runat="server" Text="" />条信息/页 转到第<asp:DropDownList ID="DropDownList1"
runat="server" AutoPostBack="True">
</asp:DropDownList>
页
</div>
</ul>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script language="javascript" type="text/javascript" src="/js/SelectCheckBox.js"></script>
    <script>
        function gotourl(Optimal, pubid, gid, id) {
            location.href = 'ShowPub.aspx?menu=setinfo&Optimal=' + Optimal + '&pid=' + pubid + '&ID=' + gid + '&GID=' + id;
        }
        function setdb(Optimal, pubid, gid, id) {
            location.href = 'ShowPub.aspx?menu=setdb&Optimal=' + Optimal + '&pid=' + pubid + '&ID=' + gid + '&GID=' + id;
        }
        function setnodb(Optimal, pubid, gid, id) {
            location.href = 'ShowPub.aspx?menu=setnodb&Optimal=' + Optimal + '&pid=' + pubid + '&ID=' + gid + '&GID=' + id;
        }

    </script>
</asp:Content>
