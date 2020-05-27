<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderStateManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.OrderStateManage" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>资金状态处理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
    <tr >
    <td width="35%"> 请选择状态：<asp:HiddenField ID="hforderid" runat='server' /></td>
    <td> <asp:DropDownList ID="ddstate" runat='server' AutoPostBack="True" 
            onselectedindexchanged="ddstate_SelectedIndexChanged">
            <asp:ListItem Value="0">等待付款</asp:ListItem>
            <asp:ListItem Value="1">全部付款</asp:ListItem>
            <asp:ListItem Value="2">部分付款</asp:ListItem>
            <asp:ListItem Value="3">货到付款</asp:ListItem>
            <asp:ListItem Value="5">订单取消，等待退款</asp:ListItem>
            <asp:ListItem Value="4">订单取消，退款成功</asp:ListItem>
        </asp:DropDownList><span id="tips" style="color:Red" runat="server"></span>
</td></tr>
     <tr >
        <td><asp:Label ID="lblbank" runat="server">银行：</asp:Label> ：</td>
        <td><asp:TextBox ID="txtBank" runat="server" class="form-control" ></asp:TextBox></td>
     </tr>
    <tr >
        <td><asp:Label ID="lblmon" runat="server">金额：</asp:Label></td>
        <td><asp:TextBox ID="txtMoney" runat="server" class="form-control" ></asp:TextBox></td>
     </tr>
    <tr >
        <td><asp:Label ID="lblC" runat="server">到帐日期：</asp:Label>：</td>
        <td><asp:TextBox ID="txtDate" runat="server" class="form-control" ></asp:TextBox></td>
     </tr>
     <tbody id="bodybank" runat="server" >
     <tr >
        <td>银行流水号：</td>
        <td><asp:TextBox ID="txtNumber" runat="server" class="form-control" ></asp:TextBox></td>
     </tr>
     </tbody>
    <tr >
        <td style="line-height:119px">备注：</td><td><asp:TextBox ID="txtRemark" 
            runat="server" TextMode="MultiLine" Height="119px" Width="407px" ></asp:TextBox></td></tr>
    <tbody id="duanxin" runat="server">
    <tr >
        <td>短信发送：</td><td><input id="nock" runat="server" type="checkbox" onclick="onchan()" />手机短信和E_Mail通知</td>
    </tr></tbody>
   <tbody id="con" style="display:none">
    <tr >
        <td>通知内容：</td><td>
       <asp:TextBox ID="txtContent" runat="server" class="form-control" TextMode="MultiLine" Height="106px" Width="404px"></asp:TextBox>
       <font color="red">{$UserName}订货人姓名;{$SiteName}网站名称;{$Remark}为上面的备注信息; {$OrderCode}为订单号</font>
      </td></tr>
    </tbody>
    <tr >
       <td align="center" colspan="2">
        <asp:Button ID="btn" runat="server" Text="保存" onclick="btn_Click" class="btn btn-primary" /><span id="sptip" runat="server" style="color:Red"></span></td></tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript">
    function onchan() {
        if (document.getElementById("nock").checked) {
            document.getElementById("con").style.display = "";
        } else {
            document.getElementById("con").style.display = "none";
        }
    }
</script>
</asp:Content>