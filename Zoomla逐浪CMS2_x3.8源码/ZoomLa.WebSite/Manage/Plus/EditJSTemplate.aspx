<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditJSTemplate.aspx.cs" Inherits="ZoomLa.WebSite.Manage.AddOn.EditJSTemplate" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>编辑广告JS模板</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr align="center">
            <td><b>修改模板内容</b></td>
        </tr>
        <tr>
            <td style="height: 350px" align="center">
                <asp:TextBox ID="TxtADTemplate" runat="server" CssClass="form-control" Style="max-width: 600px;" Height="326px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="EBtnSaverTemplate" class="btn btn-primary" runat="server" Text="保存修改结果" OnClick="EBtnSaverTemplate_Click" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnZoneType" runat="server" />
</asp:Content>
