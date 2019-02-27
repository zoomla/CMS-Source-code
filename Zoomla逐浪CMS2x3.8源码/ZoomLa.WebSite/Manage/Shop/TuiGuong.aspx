<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TuiGuong.aspx.cs" Inherits="manage_Shop_TuiGuong" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>商品推广</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table width="98%">
    <tr align="center" class="tdbg">
        <td>商品ID:<asp:Label ID="ShopID" runat="server" Text=''/></td>
        <td>推广人:<asp:Label ID="ExWorker" runat="server" Text=''/></td>                    
    </tr>
        <tr align="center" class="tdbg">
        <td>商品价格:<asp:Label ID="ShopPrice" runat="server" Text=""/></td>              
        <td >商品名称:<asp:Label ID="ShopName" runat="server" Text=""/></td>            
    </tr>
    <tr align="center" class="tdbg">
        <td >购买数量:<asp:Label ID="lblNum" runat="server" Text=""/></td>              
        <td>仓库库存:<asp:Label ID="lblgu" runat="server" Text=""/></td>                    
    </tr>
</table>
<br />
<div>    
<asp:TextBox ID="txtJiexi" runat="server" TextMode="MultiLine"  CssClass="l_input" Height="61px" Width="98%"></asp:TextBox><br />     
<input type="button"  name="Submit" onClick='copyToClipBoard()' value="复制推广代码"  class="C_input"/>    
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
        <script type="text/javascript">
            function copyToClipBoard() {
                var clipBoardContent = "";
                clipBoardContent += document.title;
                clipBoardContent += "";
                clipBoardContent += this.location.href;
                window.clipboardData.setData("Text", clipBoardContent);
                alert("复制成功，您可以将其粘贴到QQ/MSN上啦！");
            }
    </script>
</asp:Content>