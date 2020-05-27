<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductMove.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.ProductMove" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>内容批量移动</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
	    <tr align="center">
            <td colspan="2" class="spacingtitle">
                批量移动商品到其他节点</td>
        </tr>
        <tr>
            <td class="text-right">                    
                商品ID：</td>
            <td>
                <asp:TextBox ID="TxtContentID" class="form-control m715-50" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtContentID"
                    ErrorMessage="商品ID不能为空" /></td>
         </tr>
         <tr>
            <td style="width: 20%" align="right">                    
                目标节点：</td>
            <td>
              <select id="node_dp" name="node_dp" class="form-control text_md">
                    <asp:Literal runat="server" ID="Node_Lit"></asp:Literal>
              </select>
            </td>
         </tr>
         <tr>
            <td class="text-center" colspan="2">                    
                <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="批量处理" OnClick="Button1_Click" />
                <input name="Cancel" type="button" class="btn btn-primary" id="BtnCancel" value="取消" onclick="location.href = 'ProductManage.aspx'" />
            </td>            
         </tr>
	</table>
</asp:Content>