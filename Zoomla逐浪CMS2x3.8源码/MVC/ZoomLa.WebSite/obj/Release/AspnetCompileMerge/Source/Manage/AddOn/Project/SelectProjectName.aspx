<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectProjectName.aspx.cs" Inherits="ZoomLaCMS.Manage.AddOn.Project.SelectProjectName" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>查询用户</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered table-striped">
	<tr>
		<td>
			ID
		</td>
		<td>
			客户名称
		</td>
		<td>
			客户类别
		</td>
		<td>
			客户编号
		</td>
		<td>
			客户组别
		</td>
		<td>
			客户来源
		</td>
		<td>
			操作
		</td>
	</tr>
	<ZL:ExRepeater ID="RPT" runat="server" PagePre="<tr><td colspan='7' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>">
		<ItemTemplate>
			<tr>
				<td>
					<%#Eval("Flow")%>
				</td>
				<td>
					<%#Eval("P_name")%>
				</td>
				<td>
					<%#Eval("Client_Type","{0}")=="1"?"企业":"个人"%>
				</td>
				<td>
					<%#Eval("Code")%>
				</td>
				<td>
					<%#Eval("Client_Group")%>
				</td>
				<td>
					<%#Eval("Client_Source")%>
				</td>
				<td>
					<a href="SelectUserName.aspx?menu=select&id=<%#Eval("Flow") %>">选择</a>
				</td>
			</tr>
		</ItemTemplate>
        <FooterTemplate></FooterTemplate>
	</ZL:ExRepeater>
      </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>