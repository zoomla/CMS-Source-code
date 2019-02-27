<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectIndex.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="manage_AddOn_ProjectIndex" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>项目管理</title>
<script type="text/javascript" src="/js/Dialog.js"></script>
<script>
var diag = new Dialog();
function open_title(quid) {
diag.Width = 900;
diag.Height = 1000;
diag.Title = "添加内容<span style='font-weight:normal'>[ESC键退出当前操作]</span>";
diag.URL = "../iServer/BiServerInfo.aspx?QuestionId=" + quid;
diag.show();
}
</script>
<script type="text/javascript" language="javascript">
function getinfo(id) {
location.href = "ProjectsDetail.aspx?ProjectID=" + id + "";
}
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div style="margin-top:10px">
<asp:Repeater ID="resultsRepeater_w" runat="server">
	<HeaderTemplate>
		<table class="table table-bordered table-responsive table-hover">
			<thead>
			<tr><td colspan="7" class="text-center">最新问题</td></tr>
				<tr>
					<td>编号</td>
					<td>标题</td>
					<td>优先级</td>
					<td>问题类型</td>
					<td>已读次数</td>
					<td>提交时间</td>
					<td>状态</td>
				</tr>
			</thead>
			<tbody>
	</HeaderTemplate>
	<ItemTemplate>
		<tr>
			<td><%# Eval("QuestionId")%></td>
			<td>
			<a href="../iServer/BiServerInfo.aspx?QuestionId=<%#Eval("QuestionId")%>" ><%# ZoomLa.Common.BaseClass.CheckInjection(Eval("Title", "{0}"))%></a></td>
			<td><%# Eval("Priority")%></td>
			<td><%# Eval("Type")%></td>
			<td><%# Eval("ReadCount")%></td>
			<td><%# Eval("SubTime")%></td>
			<td><asp:Label ID="lblState" runat="server" ForeColor="Red" Text='<%# Eval("State")%>'></asp:Label></td>
		</tr>
	</ItemTemplate>
	<FooterTemplate>
	</tbody>
	</table>
	</FooterTemplate>
</asp:Repeater>
</div>
<div class="clearbox">
</div>
<table class="table table-bordered table-responsive table-hover">
	<tr>
		<td colspan="7" class="text-center">
			最新项目
		</td>
	</tr>
	<tbody>
		<tr>
			<td width="3%">
				ID
			</td>
			<td width="10%">
				项目名称
			</td>
			<td width="7%">
				项目类型
			</td>
			<td width="5%">
				项目价格
			</td>
			<td width="7%">
				项目经理
			</td>
			<td width="6%">
				当前进度
			</td>
			<td width="12%">
				申请时间
			</td>
		</tr>
		<asp:Repeater ID="Repeater1" runat="server">
			<ItemTemplate>
				<tr id='<%#Eval("ID") %>' title="双击查看详情" ondblclick="getinfo(this.id)">
					<td>
						<asp:Label ID="Label1" runat="server" Text='<%# Eval("ID") %>' Visible="false" /><%# Eval("ID","{0}")%>
					</td>
					<td>
						<a href="ProjectsDetail.aspx?ProjectID=<%# Eval("ID","{0}")%>">
							<%# Eval("Name")%></a>
					</td>
					<td>
						<%# GetProType(Eval("TypeID","{0}")) %>
					</td>
					<td>
						<%# GetManageGroup(Eval("Leader","{0}")) == 1 ? Eval("Price", "￥{0}.00") : "******"%>
					</td>
					<td>
						<a href="../AddCRM/ViewCustomer.aspx?FieldName=Person_Add&id=3">
							<%#GetLeader(Eval("Leader","{0}"))%></a>
					</td>
					<td>
						<a href='<%#Eval("ID","ProjectsProcesses.aspx?ID={0}") %>'>
							<div style="width: 90%; border: solid 1px red; height: 5px">
								<div id="line" runat="server" style="background-color: green; height: 5px; float: left">
								</div>
							</div>
						</a>
					</td>
					<td>
						<%# Eval("ApplicationTime")%>
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</tbody>
</table>
<table class="table table-bordered table-responsive table-hover">
	<tr>
		<td colspan="5" class="text-center">
			最新客户
		</td>
	</tr>
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
			客户来源
		</td>
	</tr>
	<asp:Repeater ID="Repeater2" runat="server">
		<ItemTemplate>
			<tr>
				<td>
					<%#Eval("Flow")%>
				</td>
				<td>
				   <a href="../AddCRM/ViewCustomer.aspx?FieldName=Person_Add&id=<%#Eval("Flow") %>"><%#Eval("P_name")%></a>
				</td>
				<td>
					<%#Eval("Client_Type","{0}")=="1"?"企业":"个人"%>
				</td>
				<td>
					<%#Eval("Code")%>
				</td>
				<td>
					<%#Eval("Client_Source")%>
				</td>
			</tr>
		</ItemTemplate>
	</asp:Repeater>
</table>
</asp:Content>

