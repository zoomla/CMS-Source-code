<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hotel_OrderInfo.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.OtherOrder.Hotel_OrderInfo" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register TagPrefix="ZL" TagName="UserGuide" Src="~/Manage/I/ASCX/UserGuide.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>酒店订单详情</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<h3>酒店信息:</h3><table width="100%" cellpadding="2" cellspacing="1" class="table table-striped table-bordered table-hover">
    <tr class="tdbg">
        <td width="25%" align="center" class="title">酒店名称</td>
        <td width="25%" align="center" class="title">价格</td>
        <td width="25%" align="center" class="title">入住</td>
        <td width="25%" align="center" class="title">城市</td>
    </tr>
    <asp:Repeater ID="procart" runat="server" onitemcommand="procart_ItemCommand">
        <ItemTemplate>
            <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                <td width="25%" align="center"><%#Eval("proname")%></td>
                <td width="25%" align="center"><%#Eval("shijia") %></td>
                 <td width="25%" align="center"><%#Eval("AddTime") %></td>
                <td width="25%" align="center"><%#Eval("city") %></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    <tr class="tdbg">
        <td height="24" align="left" colspan="5">
        <br />
            合计： &nbsp;<asp:Label ID="Label2" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
<br />
<div align="center"><font style="font-size:large;"><strong>入住人:</strong></font><asp:Label ID="lblPasser" runat="server"></asp:Label></div><br />
<h3>联系人信息:</h3>
<table width="100%" border="0" class="table table-striped table-bordered table-hover" cellspacing="1" cellpadding="0">
    <tr>
        <td width="25%" height="24" align="center" class="tdbgleft"> 联系人</td>
        <td width="25%" height="24" align="center" class="tdbgleft">邮政编码</td>
        <td width="25%" height="24" align="center" class="tdbgleft">联系电话</td>
        <td width="25%" height="24" align="center" class="tdbgleft">联系人邮箱</td>
    </tr>
    <tr>
        <td width="25%" height="24" align="center" class="tdbgleft">
            <asp:Label ID="lblContact" runat="server" Text=""></asp:Label></td>
        <td width="25%" height="24" align="center" class="tdbgleft">
            <asp:Label ID="lblZipCode" runat="server" Text=""></asp:Label></td>
        <td width="25%" height="24" align="center" class="tdbgleft">
            <asp:Label ID="lblMobile" runat="server" Text=""></asp:Label></td>
        <td width="25%" height="24" align="center" class="tdbgleft">
            <asp:Label ID="Email" runat="server" Text=""></asp:Label></td>
    </tr>
     
</table>
<br />
<br />
<!--endprint-->
<table width="100%" cellpadding="2" cellspacing="1" class="table table-striped table-bordered table-hover" id="TABLE1">
    <tbody id="Tbody4">
        <asp:Repeater ID="procart2" runat="server">
            <ItemTemplate>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
</body>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
function preview() {
    window.open('Orderlistinfo.aspx?id=5&menu=print', '打印预览', '', '');
}

function pageload() {
<%
 if(Request.QueryString["menu"]=="print") 
 {
 %>
    bdhtml = window.document.body.innerHTML;
    sprnstr = "<!--startprint-->";
    eprnstr = "<!--endprint-->";
    prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
    prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
    window.document.body.innerHTML = prnhtml;
    window.print();
 <%
 } 
 %>
}
</script>
</asp:Content>