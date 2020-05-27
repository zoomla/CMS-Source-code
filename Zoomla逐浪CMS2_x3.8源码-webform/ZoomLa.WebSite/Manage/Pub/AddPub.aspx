<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddPub.aspx.cs" Inherits="manage_Page_AddPub" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<script src="../JS/DatePicker/WdatePicker.js" type="text/javascript"></script>
<script src="../JS/Common.js" type="text/javascript"></script>
<title>互动信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="us_seta" id="manageinfo" runat="server">
	<asp:DetailsView ID="DetailsView1" runat="server" Width="100%" CellPadding="4" GridLines="None" Font-Size="12px" Style="margin-bottom: 3px; margin-top: 2px;" CssClass="table table-bordered table-hover table-striped">
		<Fields>
		</Fields>
		<FooterStyle Font-Bold="True" BackColor="#FFFFFF" />
		<CommandRowStyle Font-Bold="True" CssClass="tdbgleft" />
		<RowStyle />
		<FieldHeaderStyle Font-Bold="True" />
		<PagerStyle HorizontalAlign="Center" />
		<HeaderStyle Font-Bold="True" />
		<EditRowStyle />
		<AlternatingRowStyle />
	</asp:DetailsView>
	<asp:HiddenField ID="HiddenSmall" runat="server" />
	<asp:HiddenField ID="HdnModelID" runat="server" />
	<asp:HiddenField ID="HiddenID" runat="server" />
	<asp:HiddenField ID="HiddenPubid" runat="server" />
	<br />
	<table style="width: 100%; margin: 0 auto; margin-top: 5px;" cellpadding="0" cellspacing="0"
		class="border">
		<tr class="tdbg">
			<td colspan="2" class="title" style="text-align: center">
				<asp:Label ID="LblModelName" runat="server" Text=""></asp:Label>
			</td>
		</tr>
		<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
			<td align="right" class="tdbgleft">
				标题
			</td>
			<td>
				<asp:TextBox ID="TextBox1" class=" form-control" runat="server" Width="365px"></asp:TextBox>
			</td>
		</tr>
		<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
			<td align="right" class="tdbgleft">
				内容
			</td>
			<td>
				<asp:TextBox ID="tx_PubContent" runat="server" class=" form-control " Height="107px" TextMode="MultiLine"	Width="456px"></asp:TextBox>
			</td>
		</tr>
		<asp:Literal ID="ModelHtml" runat="server"></asp:Literal><tr class="tdbgbottom border">
			<td colspan="2">
				<asp:HiddenField ID="HdnModel" runat="server" />
				<asp:HiddenField ID="HiddenParentid" runat="server" />
				<asp:HiddenField ID="HdnPubid" runat="server" />
				<asp:HiddenField ID="HdnID" runat="server" />
				<asp:HiddenField ID="HdnType" runat="server" />
				<asp:TextBox ID="FilePicPath" runat="server" Text="fbangd" Style="display: none"></asp:TextBox>
				<asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" CssClass="btn btn-primary" />
				<asp:Button ID="Button1" Text="返回" runat="server" OnClientClick="clickurl();return false;" CssClass="btn btn-primary" />
				<br />
			</td>
		</tr>
	</table>
</div>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
	function clickurl() {
		var refer = document.referrer;
		if (refer != "") {
			location.href = refer;
		}
		else {
			window.close();
		}
	}
</script>
 </asp:Content>