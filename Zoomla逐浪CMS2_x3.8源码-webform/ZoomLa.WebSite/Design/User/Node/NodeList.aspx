<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NodeList.aspx.cs" Inherits="Design_User_Node_NodeList" MasterPageFile="~/Design/Master/User.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link href="/Design/res/css/user.css" rel="stylesheet" />
    <title>栏目列表</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container siteinfo">
   <ol class="breadcrumb">
        <li><a href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/Design/User/">动力模块</a></li>
        <li class="active">栏目列表 [<a href="AddNode.aspx">添加栏目</a>]</li>
    </ol>
   <table class="table table-striped table-bordered table-hover nodelist_div">
	<tr class="gridtitle text-center">
		<td class="td _s text-center"><strong>ID</strong></td>
		<td ><strong><%=Resources.L.节点名称 %></strong></td>
        <td class="td_m"><strong>文章数(总计)</strong></td>
        <td><strong><%=Resources.L.创建时间 %></strong></td>
        <td><strong>操作</strong></td>
	</tr>
	<asp:Repeater ID="RPT" runat="server" EnableViewState="false">
		<ItemTemplate>
			<tr id="tr_<%#Eval("NodeID") %>" class="text-center">
				<td><strong><%# Eval("NodeID") %></strong></td>
				<td class="text-left"><%# ShowIcon()%></td>
                <td><%#Eval("ItemCount") %></td>
                <td><%#Eval("CDate") %></td>
                <td>
                    <%#GetOP() %>
                </td>
			</tr>
		</ItemTemplate> 
	</asp:Repeater>
</table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/JS/Modal/APIResult.js"></script>
<script>
var node = {};
node.del = function (nid) {
    if (!confirm("确定要删除吗")) { return false; }
    $("#tr_" + nid).remove();
    $.post("/design/design.ashx?action=node_del", { "nid": nid }, function (data) {
        var model = APIResult.getModel(data);
        if (!APIResult.isok(model)) { console.log(model.retmsg); }
    });
}
</script>
</asp:Content>