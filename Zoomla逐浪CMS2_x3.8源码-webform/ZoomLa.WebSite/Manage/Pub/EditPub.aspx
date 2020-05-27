<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditPub.aspx.cs" Inherits="manage_Pub_EditPub" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Head">
    <script type="text/javascript" src="../../JS/DatePicker/WdatePicker.js"></script>
    <script src="/JS/Common.js" type="text/javascript"></script>
    <title>互动信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="us_seta" id="manageinfo" runat="server">

<table cellpadding="2" cellspacing="1" class=" table table-bordered table-hover table-striped" style="background-color: white;width:100%;">
	<tr class="tdbg" >
		<td align="center" class="title" colspan="2" height="24" width="100%">
			<asp:Label ID="LblModelName" runat="server" Text=""></asp:Label>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td align="right" class="tdbgleft">
			标题
		</td>
		<td>
			<asp:TextBox ID="TextBox1" class=" form-control" runat="server" Width="426px"></asp:TextBox>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td align="right" class="tdbgleft">
			内容
		</td>
		<td>
			<asp:TextBox ID="tx_PubContent" class="form-control" runat="server" Height="107px" TextMode="MultiLine" Width="456px"></asp:TextBox>
		</td>
	</tr>
	<asp:Literal ID="ModelHtml" runat="server"></asp:Literal><tr class="tdbgbottom border">
		<td colspan="2" style="height: 84px">
			<asp:HiddenField ID="HdnModel" runat="server" />
			<asp:HiddenField ID="HdnPubid" runat="server" />
			<asp:HiddenField ID="HdnID" runat="server" />
			<asp:HiddenField ID="HdnType" runat="server" />
			<asp:TextBox ID="FilePicPath" runat="server" Text="fbangd" Style="display: none"></asp:TextBox>
			<asp:Button ID="EBtnSubmit" Text=" 保 存 " class="btn btn-primary"  OnClick="EBtnSubmit_Click" runat="server" />
			<asp:Button ID="Button1" Text=" 返 回 " class="btn btn-primary"  runat="server" OnClick="Button1_Click" />
			<br />
		</td>
	</tr>
</table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">


</asp:Content>
