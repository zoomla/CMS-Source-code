<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyProcess.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.Flow.ModifyProcess" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>修改流程</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
	<table  class="table table-striped table-bordered table-hover">
		<thead>
			<tr>
				<th colspan="2" align="center">
					<strong>修 改 流 程 步 骤</strong>
				</th>
			</tr>
		</thead>
		<tbody class="tdbg">
			<tr>
				<td class="style1">
					<strong>所属流程：</strong>
				</td>
				<td>
					<label id="lblFlow" runat="server"></label>
				</td>
			</tr>
			<tr>
				<td class="style1">
					<strong>步骤名称：</strong>
				</td>
				<td>
					<asp:TextBox ID="txtPName" class="l_input"  runat="server" Width="300px"></asp:TextBox><label style="color: Red">*</label>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPName"
						ErrorMessage="流程步骤名称不能为空"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
				<td class="style1">
					<strong>步骤描述：</strong>
				</td>
				<td>
					<textarea rows="8" style="width: 300px" id="FlowPrcess" runat="server" cols="6"></textarea>
				</td>
			</tr>
			<tr>
				<td class="style1">
					<strong>可以执行本步骤的角色：</strong>
				</td>
				<td>
					<asp:CheckBoxList ID="cblRole" runat="server" DataTextField="RoleName" DataValueField="RoleID"
						RepeatColumns="4" RepeatLayout="Flow" TabIndex="2">
					</asp:CheckBoxList>
				</td>
			</tr>
			<tr>
				<td>
					<strong>步骤序列：</strong>
				</td>
				<td>
					<input id="txtPCode" class="l_input"  runat="server" />
						<asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="只能输入数字" Type="Integer"
							ControlToValidate="txtPCode" MaximumValue="900000000" MinimumValue="0"></asp:RangeValidator>
				</td>
			</tr>
            <tr>
				<td class="style1">
					<strong>可执行本步骤的状态码：</strong>
				</td>
				<td>
					<asp:TextBox id="needCode_Text" class="l_input" runat="server"/>
                    <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="只能输入数字" Type="Integer"
							ControlToValidate="needCode_Text" MaximumValue="900000000" MinimumValue="0"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
				<td class="style1">
					<strong>审核通过的操作名：</strong>
				</td>
				<td>
					<asp:TextBox ID="txtPassCode" class="l_input"  runat="server" Width="200px"></asp:TextBox><label style="color: Red">*</label>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPName"
						ErrorMessage="审核通过的操作名不能为空 "></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
				<td class="style1">
					<strong>审核通过的状态码：</strong>
				</td>
				<td>
					<asp:DropDownList ID="ddlPassCode" runat="server" Width="208px" DataSourceID="odsPassState"
						DataTextField="StateName" DataValueField="StateCode">
					</asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td class="style2">
					<strong>未通过审核的操作名：</strong>
				</td>
				<td class="style3">
					<asp:TextBox ID="txtNoPassCode" class="l_input" runat="server" Width="200px"></asp:TextBox><label
						style="color: Red">*</label>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPName"
						ErrorMessage="未通过审核的操作名不能为空 "></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
				<td class="style1">
					<strong>未通过审核的状态码：</strong>
				</td>
				<td>
					<asp:DropDownList ID="ddlNoPassCode" runat="server" Width="208px" DataSourceID="odsNoPassState"
						DataTextField="StateName" DataValueField="StateCode">
					</asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td colspan="2" align="center">
					<asp:Button ID="btnModify" class="btn btn-primary"  runat="server" Text="修改流程步骤"  OnClick="btnModify_Click" />
				</td>
			</tr>
		</tbody>
	</table>
	<%--        <asp:ObjectDataSource ID="odsAuditingState" runat="server" SelectMethod="GetExecutableState"
		TypeName="ZoomLa.BLL.B_AuditingState"></asp:ObjectDataSource>--%>
	<asp:ObjectDataSource ID="odsPassState" runat="server" SelectMethod="GetPassState"
		TypeName="ZoomLa.BLL.B_AuditingState"></asp:ObjectDataSource>
	<asp:ObjectDataSource ID="odsNoPassState" runat="server" SelectMethod="GetNoPassState"
		TypeName="ZoomLa.BLL.B_AuditingState"></asp:ObjectDataSource>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>
