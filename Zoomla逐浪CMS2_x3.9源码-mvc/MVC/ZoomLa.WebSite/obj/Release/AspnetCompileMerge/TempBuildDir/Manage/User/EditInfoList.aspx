<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditInfoList.aspx.cs" Inherits="ZoomLaCMS.Manage.User.EditInfoList" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>会员组模型</title>
</asp:Content>

<asp:content runat="server" contentplaceholderid="Content">
   <div class="us_seta" style="margin-top:10px;" id="manageinfo" runat ="server"></div>
<table class="table table-striped table-bordered table-hover">
  <tr>
    <td colspan="2" style="line-height:30px"><h1 style="text-align:center; font-size:14px;">
        <asp:Label ID="LblModelName" runat="server" Text=""></asp:Label>
      </h1></td>
  </tr>
  <asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
  <tr>
    <td colspan="2" style="height: 84px"><asp:HiddenField ID="HdnModel" runat="server" />
      <asp:HiddenField ID="HdnModelName" runat="server" />
      <asp:HiddenField ID="HdnID" runat="server" />
      <asp:HiddenField ID="HdnType" runat="server" />
      <asp:TextBox ID="FilePicPath" runat="server" Text="fbangd" Style="display: none"></asp:TextBox>
      <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" class="btn btn-primary" />
      <asp:Button ID="Button1" Text="返回"  runat="server" onclick="Button1_Click"  class="btn btn-primary"/>
      <br />
      
  </tr>
</table>
</asp:content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script src="/JS/Common.js" type="text/javascript"></script>
</asp:Content>