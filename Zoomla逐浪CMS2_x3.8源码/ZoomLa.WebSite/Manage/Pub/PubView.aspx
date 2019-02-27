<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PubView.aspx.cs" Inherits="manage_Pub_PubView" EnableViewStateMac="false"  MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
     <title>查看信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div>
 <table width="100%" border="0" cellpadding="0" cellspacing="1" id="bindset">
<tr align="center">
  <td colspan="4" class="spacingtitle"><strong>查看<asp:Label ID="ptit" runat="server" Font-Bold="True"></asp:Label>信息</strong>
  </td>
</tr>
 </table> 
<div class="clearbox" > </div>
	<asp:DetailsView ID="DetailsView1"  runat="server" Width="100%" 
					CellPadding="4" GridLines="None" 
		Font-Size="12px" style="margin-bottom: 3px; margin-top:2px;" CssClass="table table-bordered table-hover table-striped">
					<Fields></Fields>
					<FooterStyle Font-Bold="True"  BackColor="#FFFFFF" />
					<CommandRowStyle Font-Bold="True" CssClass="tdlefttxt" />
					<RowStyle/>
					<FieldHeaderStyle Font-Bold="True"  CssClass="tdbg" />
					<PagerStyle HorizontalAlign="Center" />
					<HeaderStyle Font-Bold="True" />
					<EditRowStyle />
					<AlternatingRowStyle />
		  </asp:DetailsView> 
<asp:HiddenField ID="HiddenSmall" runat="server" />
			 <asp:HiddenField ID="HdnModelID" runat="server" />
			  <asp:HiddenField ID="HiddenID" runat="server" />
				<asp:HiddenField ID="HiddenPubid" runat="server" />


<div style="text-align:center;"> 
	<asp:Button ID="Button2" runat="server" class="btn btn-primary" onclick="Button2_Click" Text="回复" />
&nbsp;<asp:Button ID="Button1" runat="server" Text="返回"  class="btn btn-primary" onclick="Button1_Click" /></div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>
