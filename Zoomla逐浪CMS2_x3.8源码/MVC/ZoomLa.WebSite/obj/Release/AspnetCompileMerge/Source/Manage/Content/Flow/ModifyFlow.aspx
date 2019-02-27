<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyFlow.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.Flow.ModifyFlow" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>修改审核状态</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <thead class="spacingtitle">
            <tr>
                <td colspan="2" align="center">修改流程</td>
            </tr>
        </thead>
        <tr>
            <td class="tdleft td_l">
                <strong>流程名称：</strong></td>
            <td>
                <input id="txtName" class="form-control text_300" runat="server" size="50" /><label style="color: Red">*</label>
            </td>
        </tr>
        <tr>
            <td class="tdleft">
                <strong>流程描述：</strong>
            </td>
            <td>
                <textarea id="txtFlowDepict" runat="server" rows="10" class="form-control" cols="8" style="width: 360px"></textarea>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="保存" OnClick="btnSave_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>
