<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Config.aspx.cs" Inherits="Manage_Design_Config" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>动力配置</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered">
        <tr><td class="td_m">动力域名</td><td><asp:TextBox runat="server" ID="Domain_T" CssClass="form-control text_300" /><span>格式:site.z01.com</span></td></tr>
       <%-- <tr><td>微站点数</td><td><ZL:TextBox runat="server" ID="MBSiteCount_T" CssClass="form-control text_300" ValidType="Int" AllowEmpty="false" /><span>每个用户所能拥有的微站点数量</span></td></tr>--%>
        <tr><td></td><td><asp:Button runat="server" ID="Save_Btn" CssClass="btn btn-primary" OnClick="Save_Btn_Click" Text="保存配置" /></td></tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>