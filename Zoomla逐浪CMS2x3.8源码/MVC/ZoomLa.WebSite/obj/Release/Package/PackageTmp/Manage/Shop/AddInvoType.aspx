<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddInvoType.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.AddInvoType" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加发票类型</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-striped table-bordered table-hover">
    <tr class="tdbg">
      <td colspan="2" style="text-align:center;">
      <asp:Label id="invType" runat="server">添加发票类型</asp:Label></td>
    </tr>
    <tr>
      <td width="24%" height="24" class="tdbg"><strong>发票类型名称</strong></td>
      <td width="76%" height="24" ><asp:TextBox ID="deliname" runat="server" class="form-control text_300" />
          <asp:HiddenField ID="ID_H" runat="server" />
      </td>
    </tr>        
    <tr >
      <td height="24" class="tdbg"><strong>发票类型简介</strong></td>
      <td height="24" ><asp:TextBox ID="deliinfo" class="form-control text_300" runat="server" Height="177px" TextMode="MultiLine" Width="428px" /></td>
    </tr>

       <tr>
      <td class="tdbg"><strong>税率</strong></td>
      <td ><asp:TextBox ID="shuilv" class="form-control text_300" runat="server" Text='0' Width="50px" /> %
      <asp:RegularExpressionValidator ID="RegularExpressionValidator13"  runat="server" ControlToValidate="shuilv" ErrorMessage="税率格式不对!" ValidationExpression="\d+[.]?\d*"  Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator></td>
    </tr>
    <tr>
      <td colspan="2"><asp:Button ID="Button1" class="btn btn-primary"  Text="保存发票类型" runat="server" OnClick="Button1_Click" />          </td>
    </tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>
