<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NodeSearch.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.NodeSearch" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>节点搜索</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-striped table-bordered table-hover">
	<tr class="gridtitle" align="center" style="height: 25px;"> 
		<td style="width: 3%; text-align: center;"><strong>ID</strong></td>
		<td style="width: 15%;"><strong>节点名称</strong></td>
		<td style="width: 10%; height: 20px;"><strong>节点类型</strong></td>                
        <td style="width: 25%; height: 20px;"><strong>绑定模型</strong></td>
        <td style="width: 20%; height: 20px;"><strong>节点模板</strong></td>
        <td><strong>操作</strong></td>
	</tr>
    <ZL:Repeater runat="server" ID="RPT" PageSize="10" PagePre="<tr class='hidden'><td></td><td colspan='12'><div class='text-center'>" PageEnd="</div></td></tr>">
        <ItemTemplate>
            <tr>
                 <td><%#ZoomLa.Common.StringHelper.SkeyToRed(Eval("NodeID",""),Skey)  %></td>
                <td><a href="ContentManage.aspx?NodeID=<%#Eval("NodeID") %>"><%# GetIconPath(Convert.ToInt32(Eval("NodeID")))%></a>
                    <a href="EditNode.aspx?NodeID=<%#Eval("NodeID") %> "><%#ZoomLa.Common.StringHelper.SkeyToRed(Eval("NodeName",""),Skey) %></a></td>
                <td><%# GetNodeType(Eval("NodeType",""))%></td>
                <td><%# GetTemplate(Convert.ToInt32(Eval("NodeID")))%></td>
                <td><a href="/Admin/Template/TemplateEdit.aspx?filepath=/<%# GetTemplateurl(Convert.ToInt32(Eval("NodeID")))%>"><%# GetTempName(Convert.ToInt32(Eval("NodeID")))%></a></td> 
                <td class="optd"><%#GetOper()%></td>               
            </tr>
        </ItemTemplate>
        <FooterTemplate></FooterTemplate>
    </ZL:Repeater>
</table>  
</asp:Content>
