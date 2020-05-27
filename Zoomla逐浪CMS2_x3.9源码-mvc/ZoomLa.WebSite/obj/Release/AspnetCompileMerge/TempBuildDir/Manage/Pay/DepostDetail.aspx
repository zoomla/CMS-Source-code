<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepostDetail.aspx.cs" Inherits="ZoomLaCMS.Manage.Pay.DepostDetail" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>充值详情</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="condiv">
    <table class="table table-bordered table-striped">
        <tr><td class="td_m">充 值 号 ：</td>  <td><asp:Label runat="server" ID="OrderNo_L"></asp:Label></td></tr>
        <tr><td>付款状态：</td><td><asp:Label runat="server" ID="PayStatus_L"></asp:Label></td></tr>
        <tr><td>用 户 名 ：</td><td><asp:Label runat="server" ID="UserName_L"></asp:Label></td></tr>
        <tr><td>充值金额：</td><td><asp:Label runat="server" ID="MoneyPay_L"></asp:Label></td></tr>
        <tr><td>优惠金额：</td><td>0.00</td></tr>
        <tr><td>充值日期：</td><td><asp:Label runat="server" ID="AddTime_L"></asp:Label></td></tr>
        <tr><td>支付平台：</td><td><asp:Label runat="server" ID="PayPlat_L"></asp:Label></td></tr>
        <tr><td>实收金额：</td><td><asp:Label runat="server" ID="MoneyTrue_L"></asp:Label></td></tr>
        <tr><td>付款日期：</td><td><asp:Label runat="server" ID="PayedTime_L"></asp:Label></td></tr>
        <tr><td>备注：</td><td><asp:Label runat="server" ID="Remark_L"></asp:Label></td></tr>
        <tr><td></td>
        <td> 
            <input type="button" class="btn btn-primary" value="导出Excel" onclick="OutToExcel();"  />
            <asp:Literal ID="Return_L" runat="server" />
        </td></tr>
    </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Label/ZLHelper.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        function OutToExcel() {
            var $html = $(document.getElementById("condiv").outerHTML);
            $html.find("td").css("border", "1px solid #666");
            $html.find("tr:last").remove();
            ZLHelper.OutToExcel($html.html(), "充值信息");
        }
        var userdiag = new ZL_Dialog();
        function showuser(uid) {
            userdiag.reload = true;
            userdiag.title = "查看用户";
            userdiag.url = "../User/Userinfo.aspx?id="+uid;
            userdiag.backdrop = true;
            userdiag.maxbtn = false;
            userdiag.ShowModal();
        }
    </script>
</asp:Content>