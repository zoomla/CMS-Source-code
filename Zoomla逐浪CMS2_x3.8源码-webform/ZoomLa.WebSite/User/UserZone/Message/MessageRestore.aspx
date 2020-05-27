<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageRestore.aspx.cs" Inherits="FreeHome.User.MessageRestore" MasterPageFile="~/User/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>信息存储</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
            <table class="table table-striped table-bordered table-hover">
                <tr>
                    <td align="center">内容:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TEXTAREA1"
                        ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
                    <td>
                        <textarea cols="30" rows="6" id="TEXTAREA1" class="form-control" runat="server"></textarea></td>
                </tr>
                <tr>
                    <td width="16%" align="center"></td>
                    <td width="84%">
                        <asp:Button ID="savebtn" CssClass="btn btn-primary" runat="server" OnClick="savebtn_Click" Text="回复" /></td>
                </tr>
            </table>
        </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>