<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPage.aspx.cs" Inherits="ZoomLaCMS.Design.SPage.AddPage" MasterPageFile="~/Common/Master/Empty.master" ValidateRequest="false" %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>页面管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="min-height:580px;">
        <table class="table table-bordered table-striped">
        <tr><td class="td_m">页面名称</td><td><ZL:TextBox runat="server" ID="PageName_T" CssClass="form-control text_300" AllowEmpty="false" ValidType="String"  /></td></tr>
        <tr><td>预览链接</td><td><asp:TextBox runat="server" ID="ViewUrl_T" CssClass="form-control" placeholder="为空则使用默认预览链接"/></td></tr>
        <tr><td>数据源标签</td><td><asp:TextBox runat="server" ID="PageDSLabel_T" class="form-control" placeholder="请放置数据源标签"></asp:TextBox></td></tr>
        <tr><td>页面头部</td><td>
            <asp:TextBox runat="server" ID="PageRes_T" CssClass="form-control" TextMode="MultiLine" style="height:200px;" placeholder="请输入头部Meta,标题和需要引入的JS或CSS资源,支持标签">
            </asp:TextBox>
        </td></tr>
        <tr><td>页面底部</td><td>
             <asp:TextBox runat="server" ID="PageBottom_T" CssClass="form-control" TextMode="MultiLine" style="height:200px;" placeholder="推荐将JS资源置于底部,以加快页面解析,支持标签"></asp:TextBox>
        </td></tr>
        <tr><td>备注</td><td><asp:TextBox runat="server" ID="PageDesc_T" CssClass="form-control" /></td></tr>
        <tr><td></td><td>
            <asp:Button runat="server" ID="Save_Btn" class="btn btn-info" Text="保存信息" OnClick="Save_Btn_Click" />
        </td></tr>
    </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script"></asp:Content>