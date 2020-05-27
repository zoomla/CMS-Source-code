<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddWorkFlowType.aspx.cs" Inherits="ZoomLaCMS.Manage.WorkFlow.AddWorkFlowType"  MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加类型</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="AddType" runat="server">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td style="width: 10%;">类型名称:</td>
                <td>
                    <asp:TextBox ID="TypeName" CssClass="form-control" Width="300" runat="server"></asp:TextBox>
                    <span style="color: #f00;">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TypeName" runat="server" ErrorMessage="类型名称不能为空" ForeColor="red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="line-height: 200px;">类型描述:</td>
                <td>
                    <asp:TextBox ID="TypeDesc" CssClass="form-control" Width="300" Height="200" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg" style="height: 34px;">
                <td>操作:</td>
                <td>
                    <asp:Button ID="SavBtn" CssClass="btn btn-primary" OnClick="SavBtn_Click" runat="server" Text="添加" />
                    <button type="button" class="btn btn-primary" onclick="window.location.href='FlowTypeList.aspx'">返回列表</button>
                </td>
            </tr>
        </table>
    </div>
    <div id="TypeList" runat="server"></div>
</asp:Content>
