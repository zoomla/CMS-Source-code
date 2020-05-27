<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update_TravelMoney.aspx.cs" Inherits="manage_Shop_OtherOrder_Update_TravelMoney" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register TagPrefix="ZL" TagName="UserGuide" Src="~/Manage/I/ASCX/UserGuide.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>修改旅游费用</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
  <div class="border" align="center">
    <table class="table table-striped table-bordered table-hover">
      <tr class="tdbg">
        <td colspan="2" style="width: 13%; height: 23px" align="center"> 修改费用 </td>
      </tr>
      <tr class="tdbg">
        <td  align="center"> 旅游线: </td>
        <td align="center" style="width: 13%; height: 23px"><asp:Label ID="hlNo" runat="server"></asp:Label></td>
      </tr>
      <tr>
        <td style="width: 13%; height: 23px" align="center"> 成人价格: </td>
        <td align="center" style="width: 13%; height: 23px"><asp:Label ID="lblPrice" runat="server"></asp:Label></td>
      </tr>
      <tr>
        <td style="width: 13%; height: 23px" align="center"> 儿童数量: </td>
        <td width="20%" align="center" style="height: 23px"><asp:Label ID="lblInfo" runat="server"></asp:Label></td>
      </tr>
      <tr>
        <td style="width: 13%; height: 23px" align="center"> 成人数量: </td>
        <td width="20%" align="center" style="height: 23px"><asp:Label ID="lblStock" runat="server"></asp:Label></td>
      </tr>
      <tr>
        <td style="width: 13%; height: 23px" align="center"> 总价: </td>
        <td width="20%"  style="height: 23px"><asp:TextBox ID="txtPrice" class="form-control" style="width:auto;" runat="server" ></asp:TextBox></td>
      </tr>
      <tr>
        <td colspan="2" style="width: 13%; height: 23px;text-align:center;"><asp:Button ID="update" runat="server" class="btn btn-primary"   Text="修改" 
				onclick="update_Click" /></td>
      </tr>
    </table>
  </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>