<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetNodeOrder.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Content.SetNodeOrder" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>节点排序</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Literal ID="Literal1" runat="server" Visible="false"></asp:Literal>
<table class="table table-striped table-bordered table-hover" align="center">
	<tr class="gridtitle" align="center" style="height: 25px;">
		<td style="width: 10%; height: 20px;">
			<strong>节点ID</strong>
		</td>
		<td style="width: 20%; height: 20px;">
			<strong>节点名</strong>
		</td>
		<td style="width: 20%">
			<strong>节点目录</strong>
		</td>
		<td style="width: 20%">
			<strong>节点类型</strong>
		</td>
        <td style="width: 10%">
			<strong>手动排序</strong>
		</td>
		<td style="width: 20%">
			<strong>排序</strong>
		</td>
	</tr>
	<asp:Repeater ID="RepSystemModel" runat="server" OnItemCommand="Repeater1_ItemCommand">
		<ItemTemplate>
			<tr class="order_tr">
				<td class="text-center">
					<%#Eval("NodeID")%>
				</td>
				<td class="text-center">
					<%#Eval("NodeName")%>
				</td>
				<td class="text-center">
					<%# Eval("NodeDir")%>
				</td>
				<td class="text-center">
					<%# GetNodeType(Eval("NodeType", "{0}"))%>
				</td>
                <td class="text-center">
                    <input type="text" class="order_t" style="width:40px; text-align:center" name="OrderField<%#Eval("NodeID")%>" id="OrderField<%#Eval("NodeID")%>" value="<%#Eval("OrderID") %>" />
                    <input type="hidden" name="NodeIDValue" id="NodeIDValue" value="<%#Eval("NodeID")%>" />
				</td>
				<td class="text-center">
					<asp:LinkButton ID="LinkButton2" runat="server" CommandName="UpMove" CommandArgument='<%# Eval("NodeID") %>'>上移</asp:LinkButton>
					|
					<asp:LinkButton ID="LinkButton3" runat="server" CommandName="DownMove" CommandArgument='<%# Eval("NodeID") %>'>下移</asp:LinkButton>
				</td>
			</tr>
		</ItemTemplate>
	</asp:Repeater>
</table>
    <asp:Button ID="Button1" runat="server" Text="批量更新排序" CssClass="btn btn-info" onclick="Button1_Click" />
    <input type="button" value="整理序列号" class="btn btn-info" onclick="reorder();" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
    var reorder = function () {
        var $trs = $(".order_tr");
        for (var i = 0; i < $trs.length; i++) {
            $($trs[i]).find(".order_t").val((i + 1));
        }
    }
</script>
</asp:Content>