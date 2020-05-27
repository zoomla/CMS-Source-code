<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailWrite.aspx.cs" Inherits="ZoomLaCMS.Plat.EMail.MailWrite"  MasterPageFile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Head">
<title>发送邮件</title>
<link type="text/css" rel="stylesheet" href="/Plugins/Uploadify/style.css" />
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="mainDiv container platcontainer">
  <div class="child_head"><span class="child_head_span1"></span> <span class="child_head_span2">发送邮件</span></div>
  <table class="table table-border table-hover">
    <tr>
        <td class="text-right">发件邮箱：</td>
        <td><asp:DropDownList ID="Emails_D" CssClass="form-control text_300" DataValueField="ID" DataTextField="Acount" runat="server"></asp:DropDownList></td>
    </tr>
	<tr>
	  <td class="text-right">收件人：</td>
	  <td class="tdRight"><asp:TextBox CssClass="form-control text_300" ID="Receiver_T" runat="server" /></td>
	</tr>
	<tr>
	  <td class="text-right">邮件标题：</td>
	  <td class="tdRight"><asp:TextBox ID="TxtTitle" CssClass="form-control text_300" runat="server" /></td>
	</tr>
	<tr><td colspan="2"><asp:TextBox ID="EditorContent" runat="server" TextMode="MultiLine" Width="98%" Height="300px" ClientIDMode="Static"> </asp:TextBox></tr>
      <tr>
          <td class="tdleft"><strong>已上传文件：</strong><asp:HiddenField runat="server" ID="hasFileData" />
          </td>
          <td id="hasFileTD" runat="server"></td>
      </tr>
      <tr>
          <td class="tdleft"><strong>附件：</strong></td>
          <td>
              <input type="button" id="upfile_btn" class="btn btn-primary" value="选择文件" />
              <div style="margin-top: 10px;" id="uploader" class="uploader">
                  <ul class="filelist"></ul>
              </div>
              <asp:HiddenField runat="server" ID="Attach_Hid" />
          </td>
      </tr>
	<tr>
	  <td colspan="2" class="text-center">
		<asp:Button ID="Send_Btn" runat="server" Text="发送邮件" OnClientClick="return SendConfirm();" OnClick="Send_Btn_Click" class="btn btn-info"/>
		<a href="/Plat/Email/" class="btn btn-default">返回列表</a>
	  </td>
	</tr>
	<tr>
	  <td colspan="2" align="center"><%-- <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" />--%></td>
	</tr>
  </table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<%=Call.GetUEditor("EditorContent",2)%> 
<link href="/JS/Controls/ZL_Webup.css" rel="stylesheet" />
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Controls/ZL_Webup.js"></script>
<script>
    $(function () {
        $("#upfile_btn").click(ZL_Webup.ShowFileUP); setactive("办公");
    })
    function AddAttach(file, ret, pval) { return ZL_Webup.AddAttach(file, ret, pval); }
</script>
</asp:Content>