<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportData.aspx.cs" Inherits="ZoomLaCMS.Tools.ImportData" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>数据导入</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container margin_t10">
  
    <table class="table table-bordered table-striped">
        <tr><td class="td_m">模型ID</td><td>
            <asp:DropDownList runat="server" ID="Model_DP" DataTextField="ModelName" DataValueField="ModelID"></asp:DropDownList>
                                      </td></tr>
        <tr><td></td><td>
            <asp:Button runat="server" ID="Article_Btn" Text="导入文章" OnClick="Article_Btn_Click" />
            <asp:Button runat="server" ID="Photo_Btn"  Text="导入图库" OnClick="Photo_Btn_Click"/>
            
        </td></tr>
    </table>
      <div>
          <asp:Button runat="server" ID="Node_Btn" Text="导入节点" OnClick="Node_Btn_Click" />
      </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">

</asp:Content>