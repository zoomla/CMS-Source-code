<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailDetail.aspx.cs" Inherits="ZoomLaCMS.Plat.EMail.MailDetail" MasterPageFile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Head">
<title>阅读邮件</title>
<script src="/JS/Controls/ZL_Dialog.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container platcontainer">
<div class="child_head"><span class="child_head_span1"></span> <span class="child_head_span2">阅读邮件</span></div>
<table class="table table-bordered table-striped">
	    <tr>
	        <td style="width:120px" class="text-right">发件人：</td>
	        <td><asp:Label ID="LblSender" runat="server"></asp:Label></td>
	    </tr>
	    <tr>
	        <td class="text-right">收件人：</td>
	        <td><asp:Label ID="LblIncept" runat="server"></asp:Label></td>
	    </tr>
	    <tr>
	        <td class="text-right">邮件主题：</td>
	        <td><asp:Label ID="LblTitle" runat="server"></asp:Label></td>
	    </tr>
	    <tr>
	        <td class="text-right">创建时间：</td>
	        <td><asp:Label ID="LblSendTime" runat="server"></asp:Label></td>
	    </tr>
	    <tr>
	        <td class="text-right">邮件内容：</td>
	        <td><asp:Literal runat="server" ID="txt_Content"></asp:Literal></td>
	    </tr>
	   <tr>
            <td class="text-right">附件</td>
            <td>
                <div id="uploader" class="uploader">
                    <ul class="filelist"></ul>
                </div>
                <asp:HiddenField runat="server" ID="Attach_Hid" />
            </td>
        </tr>
	    <tr>
	        <td class="text-right">操作：</td>
	        <td><asp:Button ID="BtnReply" runat="server" Text="回复" CssClass="btn btn-info" OnClick="BtnReply_Click" /></td>
	    </tr>
        </table>
</div>
<link href="/JS/Controls/ZL_Webup.css" rel="stylesheet" />
<script src="/JS/Controls/ZL_Webup.js"></script>
<script>
    $(function () {
        ZL_Webup.AddReadOnlyLi($("#Attach_Hid").val()); setactive("办公");
    })
</script>
</asp:Content>


