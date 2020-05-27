<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Producer.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.Producer"  MasterPageFile="~/Manage/I/Default.master"%>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>添加厂商</title>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
  <table class="table table-striped table-bordered table-hover">
    <tr align="center">
    </tr>
    <tr>
      <td  style="width:100px">厂商名称：</td>
      <td valign="middle"><ZL:TextBox ID="Producername" runat="server" AllowEmpty="false" class="form-control text_300"  />
        <span style="color:red;margin-left:5px;">*</span>
        <asp:HiddenField ID="ID" runat="server" />
        <asp:HiddenField ID="uptype" runat="server" /></td>
    </tr>
    <tr>
      <td>厂商缩写：</td>
      <td valign="middle"><asp:TextBox ID="Smallname" runat="server" class="form-control text_300" ></asp:TextBox></td>
    </tr>
    <tr>
      <td>创建日期：</td>
      <td valign="middle"><asp:TextBox ID="CreateTime" runat="server" class="form-control text_300" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });"></asp:TextBox></td>
    </tr>
    <tr>
      <td>公司地址：</td>
      <td valign="middle"><ZL:TextBox ID="Coadd" runat="server" AllowEmpty="false" class="form-control text_300" ></ZL:TextBox>
        <span style="color:red;margin-left:5px;">*</span></td>
    </tr>
    <tr>
      <td>联系电话：</td>
      <td valign="middle"><ZL:TextBox ID="Telpho" runat="server" ValidType="PhoneNumber" AllowEmpty="false" class="form-control text_300"></ZL:TextBox>
        <span style="color:red;margin-left:5px;">*</span></td>
    </tr>
    <tr>
      <td>传真号码：</td>
      <td valign="middle"><ZL:TextBox ID="FaxCode" runat="server" class="form-control text_300"></ZL:TextBox></td>
    </tr>
    <tr>
      <td>邮政编码：</td>
      <td valign="middle"><asp:TextBox ID="PostCode" runat="server" class="form-control text_300"></asp:TextBox></td>
    </tr>
    <tr>
      <td>厂商主页：</td>
      <td valign="middle"><asp:TextBox ID="CoWebsite" runat="server" class="form-control text_300" ></asp:TextBox></td>
    </tr>
    <tr>
      <td>电子邮件：</td>
      <td valign="middle"><ZL:TextBox ID="Email" runat="server" ValidType="Mail" class="form-control text_300"></ZL:TextBox></td>
    </tr>
    <tr>
      <td>厂商分类：</td>
      <td valign="middle"><asp:RadioButtonList ID="CoClass" runat="server" RepeatDirection="Horizontal"  Width="420px">
          <asp:ListItem Selected="True">大陆厂商</asp:ListItem>
          <asp:ListItem>港台厂商</asp:ListItem>
          <asp:ListItem>日韩厂商</asp:ListItem>
          <asp:ListItem>欧美厂商</asp:ListItem>
          <asp:ListItem>其他厂商</asp:ListItem>
        </asp:RadioButtonList></td>
    </tr>
    <tr>
      <td>厂商照片：</td>
      <td valign="middle">
          <ZL:SFileUp ID="SFile_Up" runat="server" FType="Img" MaxWidth="500" MaxHeight="500" />
      </td>
    </tr>
    <tr class="WebPart">
      <td>厂商简介：</td>
      <td valign="middle">
        <textarea id="Content" name="Content" runat="server" style="width:80%;height:300px;"></textarea>
        <%=Call.GetUEditor("Content",2) %></td>
    </tr>
    <tr class="tdbg">
      <td colspan="2" align="" class="tdbg"><asp:Button ID="Button1" runat="server" class="btn btn-primary"  Text="保存设置" OnClick="Button1_Click"/></td>
    </tr>
  </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>