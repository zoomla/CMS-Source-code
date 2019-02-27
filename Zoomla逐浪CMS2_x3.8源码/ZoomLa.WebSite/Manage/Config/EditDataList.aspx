<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditDataList.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="manage_Config_EditDataList" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>修改表说明</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
  <table class="table table-striped table-bordered table-hover">
    <tr>
      <td><strong>
        <span id="Label1">修改表说明</span>
        </strong></td>
    </tr>
    <tr>
      <td class="text-left"><strong>数据表名：</strong></td>
      <td class="text-left"> <asp:Label ID="TxtTableName" runat="server" ></asp:Label> </td>
    </tr>
       <tr>
      <td class="text-left"><strong>类型：</strong></td>
      <td class="text-left"> <asp:Label ID="TxtType" runat="server" ></asp:Label><asp:HiddenField ID="TxtTypeID" runat="server" /> </td>
    </tr>
    
      <tr>
      <td class="text-left"><strong>说明：</strong></td>
      <td class="text-left"> <asp:TextBox ID="TxtExplain"  runat="server"  class="form-control" style="width:220px;"></asp:TextBox> &nbsp;*
         </td>
    </tr>
    <tr  class="tdbg"><td colspan="2"><asp:Button ID="EBtnTable" runat="server" OnClick="EBtnTable_Click" Text="保存" class="btn btn-primary"/>
  <input id="Cancel"  name="Cancel" onclick="GoBack();" type="button" value="取消" class="btn btn-primary" />
     </td></tr>
</table>
</asp:Content>
