<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegionEdit.aspx.cs" Inherits="test_RegionEdit" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>区域编辑</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
  <div class="margin_b2px top_opbar" runat="server" id="opdiv"> 
     <asp:HyperLink runat="server" ID="Add_A" CssClass="btn btn-primary"></asp:HyperLink>
  </div>
  <table id="EGV" class="table table-striped table-bordered table-hover content_list">
	<tr>
		<td>ID</td><td>标题</td>
		<td>录入者</td><td>点击数</td><td>推荐</td><td>排序</td><td>操作</td>
	</tr>
<ZL:ExRepeater ID="RPT" runat="server" PageSize="10" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='8'><div class='text-center'>" PageEnd="</div></td></tr>" OnItemDataBound="RPT_ItemDataBound" OnItemCommand="RPT_ItemCommand">
	<ItemTemplate>
		<tr ondblclick="location='ShowContent.aspx?GID=<%#Eval("GeneralID") %>&modeid=<%#Eval("ModelID") %>';">
			<td class="GID"><%#Eval("GeneralID") %></td>
            <td>
                <div class="Ctitle">
                    <span><%# GetPic(Eval("ModelID", "{0}"))%><%# GetTitle()%></span>
                </div>
            </td>
			<td><%#Eval("inputer") %></td>
			<td><%#Eval("Hits") %></td>
			<td><%#GetElite(Eval("EliteLevel", "{0}")) %></td>
            <td>
                <input type="hidden" value="<%#Eval("OrderId") %>" name="Order_hid" />
                <a href="javascript:;" onclick="MoveUp(this)">上移</a>
                <a href="javascript:;" onclick="MoveDown(this)">下移</a></td>
			<td>
				<asp:LinkButton runat="server" CommandName="Edit" CommandArgument='<%# Eval("GeneralID") %>' >修改</asp:LinkButton> |
				<asp:LinkButton ID="lbDelete" runat="server"  CommandName="Del" CommandArgument='<%# Eval("GeneralID") %>' OnClientClick="return confirm('你确定将该数据删除到回收站吗?');">删除</asp:LinkButton> | 
				<a href="/Item/<%#Eval("GeneralID") %>.aspx" target="_blank"><%#lang.LF("浏览")%></a>| 
				<asp:LinkButton ID="lbHtml" runat="server"></asp:LinkButton>
			</td>
		</tr>
	</ItemTemplate>
	<FooterTemplate></FooterTemplate>
</ZL:ExRepeater>
</table>
<div class="alert alert-info" runat="server" id="Empty_Div" visible="false"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>