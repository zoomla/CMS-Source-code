<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AskAdd.aspx.cs" Inherits="ZoomLaCMS.Manage.Design.Ask.AskAdd" MasterPageFile="~/Manage/I/Default.Master" %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<script src="/JS/DatePicker/WdatePicker.js"></script>
<title>问题管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol id="BreadNav" class="breadcrumb navbar-fixed-top">
    <li><a href='<%=CustomerPageAction.customPath2 %>Main.aspx'><%=Resources.L.工作台 %></a></li>
    <li><a href='Default.aspx'>动力模块</a></li>
    <li><a href='AskList.aspx'>问卷调查</a></li>
    <li><a href='<%=Request.RawUrl %>'>问卷管理</a></li>
</ol>
 <table class="table table-bordered table-striped">
     <tr><td class="td_m">问卷名称</td><td><ZL:TextBox runat="server" ID="Title_T" class="form-control text_300" AllowEmpty="false" /></td></tr>
     <tr><td>问卷封面</td><td>
         <ZL:SFileUp runat="server" ID="PreViewImg_UP" />
     </td></tr>
     <tr><td>到期时间</td><td><asp:TextBox runat="server" ID="EndDate_T" class="form-control text_300" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd' })"></asp:TextBox></td></tr>
     <tr><td>问卷说明</td><td><asp:TextBox runat="server" ID="Remind_T" class="form-control m715-50" TextMode="MultiLine" style="height:120px;"></asp:TextBox></td></tr>
     <tr><td></td><td>
         <asp:Button runat="server" ID="Save_Btn" Text="保存信息" class="btn btn-info" OnClick="Save_Btn_Click" />
         <a href="AskList.aspx" class="btn btn-default">返回列表</a>
                  </td></tr>
 </table> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>