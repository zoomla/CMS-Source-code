<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangAnswer.aspx.cs" Inherits="ZoomLa.WebSite.User.ChangAnswer" MasterPageFile="~/User/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>修改密码提示问题</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="container" style=" align-content:center;">
        <ol class="breadcrumb">
            <li><a href="/User/">会员中心</a></li>
            <li class="active">修改密码提示问题</li>
        </ol>
    </div>
  <div class="container btn_green " style="margin-top: 10px; list-style-type:none;"> 
	<table class="table table-bordered table-striped">
        <tr>
			<td colspan="2" class="title" style="text-align: center"><strong>修改密码提示问题</strong></td>
		</tr>
        <tr>
			<td class="text-right"><strong>密码提示问题：</strong></td>
			<td class="text-left">
				 <asp:TextBox ID="TxtQuestion" runat="server" class="form-control text_300"/>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtQuestion" runat="server" ErrorMessage="密码提示问题！" Display="Dynamic" class="color_red"/>
			</td>
		</tr>
          <tr>
			<td class="text-right"><strong>新密码提示答案：</strong></td>
			<td class="text-left">
				  <asp:TextBox ID="TxtNewAnswer" runat="server" class="form-control text_300" />
			</td>
		</tr>
         <tr>
			<td class="text-right"><strong>确认密码提示答案：</strong></td>
			<td class="text-left">
				  <asp:TextBox ID="TxtNewAnswer2" runat="server" class="form-control text_300"/>
            <asp:CompareValidator ID="CompareValidator1" ControlToValidate="TxtNewAnswer2" ControlToCompare="TxtNewAnswer" ErrorMessage="两次输入的密码提示答案不一致！" runat="server" class="color_red" />
			</td>
		</tr>
        <tr>
            <td></td>
            <td class="text-left">
                <asp:Button ID="BtnSubmit" runat="server" Text="修改" OnClick="BtnSubmit_Click" class="btn btn-primary" />
                <asp:Button ID="BtnCancle" runat="server" Text="取消" OnClick="BtnCancle_Click" ValidationGroup="BtnCancel" class="btn btn-primary" />
            </td>
           </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>