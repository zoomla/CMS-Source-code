<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="ExChangeRecord.aspx.cs" Inherits="User_Profile_ExChangeRecord" ClientIDMode="Static" ValidateRequest="false" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>兑换记录</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="shop" data-ban="shop"></div>
<div class="container margin_t5">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">兑换记录</li> 
    </ol>
</div>
    <div class="container">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td colspan="6" class="text-center">
                    兑换记录
                </td>
            </tr>
            <tr>
                <th align="center" width="20%">申请时间</th>
                <th align="center" width="10%">卡类别</th>
                <th align="center" width="10%">卡信息</th>
                <th align="center" width="30%">备注</th>
                <th align="center" width="10%">确认日期</th>
                <th align="center" width="10%">兑换状态</th>
            </tr>
            <ZL:ExRepeater ID="repf" runat="server" PagePre="<tr><td colspan='6' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>">
                <ItemTemplate>
                    <tr>
                        <td align="center" width="20%">
                            <asp:HiddenField ID="hfId" runat="server" Value='<%#Eval("id") %>' />
                            <asp:Label ID="lblOrderData" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OrderData","{0:yyyy-MM-dd}") %>'></asp:Label></td>
                        <td align="center" width="10%">
                            <asp:Label ID="lblCardType" runat="server" Text='<%# GetCardType(Eval("CardType","{0}")) %>'></asp:Label></td>
                        <td align="center" width="10%">
                            <asp:Label ID="lblCardinfo" runat="server" Text='<%#GetCardinfo(Eval("ShopCardId","{0}")) %>'> </asp:Label></td>
                        <td align="center" width="30%">
                            <asp:Label ID="lblRemark" runat="server" Text='<%#GetRemark(Eval("remark","{0}"))%>'></asp:Label></td>
                        <td align="center" width="10%">
                            <asp:Label ID="lblconfirmData" runat="server" Text='<%#GetConfirmData(Eval("confirmData","{0}"),Eval("confirmState","{0}")) %>'></asp:Label></td>
                        <td align="center" width="10%">
                            <asp:Label ID="lblDstate" runat="server" Text='<%#GetState(Eval("State","{0}")) %>'></asp:Label></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </ZL:ExRepeater>
            
        </table>
    </div>
</asp:Content>
