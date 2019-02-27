<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="User_Profile_Profile" ClientIDMode="Static" ValidateRequest="false" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>返利详情</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="u_sign" id="u_store" data-nav="shop"></div>
<div class="container margin_t5">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="Default.aspx">我的返利</a></li>
        <li class="active">返利详情</li>
		<div class="clearfix"></div>
    </ol>
</div>
    <div class="container">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td colspan="5" class="text-center">返利详情
                </td>
            </tr>
            <tr>
                <td align="center" width="20%">订单编号</td>
                <td align="center" width="20%">订单金额</td>
                <td align="center" width="20%">购物商城</td>
                <td align="center" width="20%">返利金额</td>
                <td align="center" width="20%">确认返利</td>
            </tr>
            <ZL:ExRepeater ID="repf" runat="server" PagePre="<tr><td colspan='5' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>" OnItemDataBound="repf_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td align="center" width="20%">
                            <asp:HiddenField ID="hfId" runat="server" Value='<%#Eval("id") %>' />
                            <asp:Label ID="lblOrderNo" runat="server" Text='<%#Eval("orderNo") %>'></asp:Label></td>
                        <td align="center" width="20%">
                            <asp:Label ID="lblOrderMoney" runat="server"
                                Text='<%#DataBinder.Eval(Container, "DataItem.OrderMoney", "{0:N2}") %>'></asp:Label></td>
                        <td align="center" width="20%">
                            <asp:Label ID="lblShop" runat="server"> </asp:Label></td>
                        <td align="center" width="20%">
                            <asp:Label ID="lblFM" runat="server"
                                Text='<%#DataBinder.Eval(Container, "DataItem.ProfileMoney", "{0:N2}")%>'></asp:Label></td>
                        <td align="center" width="20%">
                            <asp:Label ID="lblProSate" runat="server"></asp:Label></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </ZL:ExRepeater>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/Common/Common.js" type="text/javascript"></script>
    <script>
        function CheckAll(spanChk)//CheckBox全选
        {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
        }
    </script>
</asp:Content>
