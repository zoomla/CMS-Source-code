<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditRegionPrice.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.EditRegionPrice"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>区域价格管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped">
    <tr><td class="td_m">地区</td><td><asp:Label runat="server" ID="Region_L"></asp:Label></td></tr>
    <tr><td>价格详情</td><td>
        <table class="table table-bordered table-striped margin_t5">
            <tr>
                <td>会员组</td>
                <td>价格</td>
            </tr>
            <asp:Repeater runat="server" ID="Group_RPT">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("GroupName") %></td>
                        <td>
                            <input type="text" value="<%#GetPrice() %>" class="form-control text_md price_t float" data-gid="<%#Eval("GroupID") %>" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </td>
    </tr>
<tr><td></td><td>
    <asp:Button runat="server" ID="Save_Btn" CssClass="btn btn-primary" OnClick="Save_Btn_Click" Text="保存信息" OnClientClick="return presave();" />
    <asp:HiddenField runat="server" ID="Price_Hid" />
 </td></tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
    function presave() {
        var price = [];
        $(".price_t").each(function () {
            var $input = $(this);
            price.push({ gid: $input.data("gid"), price: $input.val() });
        });
        $("#Price_Hid").val(JSON.stringify(price));
    }
</script>
</asp:Content>