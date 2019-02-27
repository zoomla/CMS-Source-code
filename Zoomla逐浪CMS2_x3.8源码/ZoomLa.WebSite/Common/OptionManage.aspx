<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OptionManage.aspx.cs" MasterPageFile="~/Common/Common.master" Inherits="Common_OptionManage" enableviewstatemac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>选项管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="添加选项" onclick="Button1_Click" />
    <table class="table table-border table-condensed table-responsive">
    <tr>
      <td>序号</td>
      <td>选项标题</td>
      <td>选项值</td>
      <td>操作</td>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
      <ItemTemplate>
	    <tr>
	      <td><%#Eval("id") %></td>
	      <td><%#Eval("classname")%></td>
	      <td><%#Eval("classvalue")%></td>
	      <td><a href="AddOption.aspx?menu=edit&fid=<%#Eval("id") %>&id=<%=Request.QueryString["id"] %>&ModelID=<%=Request.QueryString["ModelID"] %>">修改</a> <a href="OptionManage.aspx?menu=delete&fid=<%#Eval("id") %>&id=<%=Request.QueryString["id"] %>&ModelID=<%=Request.QueryString["ModelID"] %>"  target="_self" >删除</a></td>
	    </tr>
      </ItemTemplate>
    </asp:Repeater>
    </table>
</asp:Content>


