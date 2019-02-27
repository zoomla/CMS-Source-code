<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddPage.aspx.cs" Inherits="Design_SPage_AddPage" MasterPageFile="~/Common/Master/Empty.master" ValidateRequest="false" %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>页面管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="min-height:550px;">
        <table class="table table-bordered table-striped">
        <tr><td class="td_m">页面名称</td><td><ZL:TextBox runat="server" ID="PageName_T" CssClass="form-control" AllowEmpty="false"  /></td></tr>
<%--        <tr><td>页面地址</td><td><asp:TextBox runat="server" ID="PageUrl_T" CssClass="form-control text_300" /></td></tr>--%>
<%--        <tr><td>关键词</td><td><asp:TextBox runat="server" ID="KeyWord_T" CssClass="form-control text_300" /></td></tr>--%>
        <tr><td>页面描述</td><td><asp:TextBox runat="server" ID="PageDesc_T" TextMode="MultiLine" CssClass="form-control" style="height:120px;" /></td></tr>
        <tr><td>引入资源</td><td>
            <asp:TextBox runat="server" ID="PageRes_T" CssClass="form-control" TextMode="MultiLine" style="height:200px;" placeholder="需要引入的JS或CSS资源,回车分隔"></asp:TextBox>
        </td></tr>
       <%-- <tr><td>背景图片</td><td><ZL:SFileUp runat="server" ID="SFile_UP" FType="Img" /></td></tr>
        <tr><td>背景设定</td><td>
           <label><input type="radio" name="bktype_rad" value="0" checked="checked"/>不重复</label>
           <label><input type="radio" name="bktype_rad" value="1"/>平铺</label>
           <label><input type="radio" name="bktype_rad" value="2"/>水平重复</label>
           <label><input type="radio" name="bktype_rad" value="3"/>垂直重复</label>
           <label><input type="radio" name="bktype_rad" value="4"/>水平居中垂直重复</label>
        </td></tr>--%>
        <tr><td></td><td>
            <asp:Button runat="server" ID="Save_Btn" class="btn btn-info" Text="保存" OnClick="Save_Btn_Click" />
            <button type="button" onclick="parent.closeDiag();" class="btn btn-info">关闭</button>
        </td></tr>
    </table>
    </div>
   
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script"></asp:Content>