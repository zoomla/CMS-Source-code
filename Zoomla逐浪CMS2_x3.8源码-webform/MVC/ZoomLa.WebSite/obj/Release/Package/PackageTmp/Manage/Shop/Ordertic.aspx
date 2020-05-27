<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ordertic.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.Ordertic" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>发票状态管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
    <tr>
    <td width="35%"> 请选择状态：<asp:HiddenField ID="hforderid" runat='server' /></td>
    <td> <asp:DropDownList ID="ddstate" runat='server' AutoPostBack="True" 
            onselectedindexchanged="ddstate_SelectedIndexChanged">
            <asp:ListItem Value="0">未开发票</asp:ListItem>
            <asp:ListItem Value="1">已开发票</asp:ListItem>
        </asp:DropDownList><span id="tips" style="color:Red" runat="server"></span>
</td></tr>
    <tbody id="fahuo" runat="server">
    <tr>
        <td>邮递方式：</td>
        <td><asp:TextBox ID="txtMS" runat="server" class="form-control"></asp:TextBox></td>
    </tr>
     <tr>
        <td>邮递日期：</td>
        <td><asp:TextBox ID="txttime" runat="server" class="form-control"></asp:TextBox></td>
    </tr>
    </tbody>
    <tr>
        <td style="line-height:119px">备注：</td>
        <td><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control" Height="200"></asp:TextBox></td></tr>
    <tr>
        <td>短信发送：</td><td><input id="nock" runat="server" type="checkbox" onclick="onchan()" />手机短信和E_Mail通知</td>
    </tr>
   <tbody id="con">
    <tr>
        <td>通知内容：</td><td>
       <asp:TextBox ID="txtContent" runat="server" class="form-control" TextMode="MultiLine" CssClass="form-control" Height="200"></asp:TextBox><br />
       <font color="red">{$UserName}订货人姓名;{$SiteName}网站名称;{$Remark}为上面的备注信息; {$OrderCode}为订单号;{$DeliveryUser}送货员;{$DeliUserMobile}送货手机</font>
      </td></tr>
    </tbody>
    <tr>
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