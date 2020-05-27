<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowProcess.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.Flow.FlowProcess" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加步骤</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <th colspan="2" class="text-center">
                <strong>添加流程步骤</strong>
            </th>
        </tr>
        <tr>
            <td>
                <strong>所属流程：</strong>
            </td>
            <td>
                <label id="lblFlow" runat="server"></label>
            </td>
        </tr>
        <tr>
            <td>
                <strong>步骤名称：</strong>
            </td>
            <td>
                <asp:TextBox ID="txtPName" class="form-control" runat="server"></asp:TextBox><label style="color: Red">*</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="txtPName" ErrorMessage="流程步骤名称不能为空"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <strong>步骤描述：</strong>
            </td>
            <td>
                <textarea style="width: 300px; height: 60px;" id="FlowPrcess" runat="server" class="form-control" cols="6"></textarea>
            </td>
        </tr>
        <tr>
            <td>
                <strong>可以执行本步骤的角色：</strong>
            </td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" DataTextField="RoleName" DataValueField="RoleID" RepeatColumns="4" RepeatLayout="Flow" TabIndex="2"></asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td><strong>步骤序列：</strong></td>
            <td>
                <input id="txtPCode" class="form-control" runat="server" /><label style="color: Red">*<asp:RequiredFieldValidator
                    ID="RequiredFieldValidator4" runat="server"
                    ControlToValidate="txtPName" ErrorMessage="审核通过的操作名不能为空 "></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="只能输入数字" Type="Integer"
                        ControlToValidate="txtPCode" MaximumValue="900000000" MinimumValue="0"></asp:RangeValidator></label></td>
        </tr>
        <tr>
            <td>
                <strong>可执行本步骤的状态码：</strong>
            </td>
            <td>
                <asp:TextBox ID="needCode_Text" class="form-control" runat="server" />
                <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="只能输入数字" Type="Integer"
                    ControlToValidate="needCode_Text" MaximumValue="900000000" MinimumValue="0"></asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td><strong>审核通过操作名：</strong></td>
            <td>
                <asp:TextBox ID="txtPassCode" class="form-control" runat="server" Width="200px"></asp:TextBox><label style="color: Red">*</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPName" ErrorMessage="审核通过的操作名不能为空 "></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <strong>审核通过的状态码：</strong>
            </td>
            <td>
                <asp:DropDownList ID="ddlPassCode" runat="server" Width="208px" DataSourceID="odsPassState" CssClass="form-control" DataTextField="StateName" DataValueField="StateCode"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <strong>未通过审核的操作名：</strong>
            </td>
            <td class="style3">
                <asp:TextBox ID="txtNoPassCode" class="form-control" runat="server" CssClass="form-control" Width="200px"></asp:TextBox><label style="color: Red">*</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                    ControlToValidate="txtPName" ErrorMessage="未通过审核的操作名不能为空 "></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <strong>未通过审核的状态码：</strong>
            </td>
            <td>
                <asp:DropDownList ID="ddlNoPassCode" CssClass="form-control" runat="server" Width="208px" DataSourceID="odsNoPassState"
                    DataTextField="StateName" DataValueField="StateCode">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnSave" class="btn btn btn-primary" runat="server" Text="保存流程步骤" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="odsAuditingState" runat="server" SelectMethod="GetExecutableState" TypeName="ZoomLa.BLL.B_AuditingState"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsPassState" runat="server" SelectMethod="GetPassState" TypeName="ZoomLa.BLL.B_AuditingState"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsNoPassState" runat="server" SelectMethod="GetNoPassState" TypeName="ZoomLa.BLL.B_AuditingState"></asp:ObjectDataSource>
</asp:Content>
