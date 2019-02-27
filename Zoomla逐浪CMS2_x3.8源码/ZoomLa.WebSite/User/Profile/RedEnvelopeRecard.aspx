<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="RedEnvelopeRecard.aspx.cs" Inherits="User_Profile_RedEnvelopeRecard" ClientIDMode="Static" ValidateRequest="false" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>红包申请记录</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="Default.aspx">我的返利</a></li>
        <li class="active">红包申请记录</li>
    </ol>
    <div>
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td colspan="5" class="text-center">红包申请记录
                </td>
            </tr>
            <tr>
                <td align="center" width="15%">申请时间</td>
                <td align="center" width="15%">红包</td>
                <td align="center" width="20%">扣除手续费</td>
                <td align="center" width="10%">申请状态</td>
                <td align="center" width="40%">备注</td>
            </tr>
            <ZL:ExRepeater ID="repf" runat="server" PagePre="<tr><td colspan='5' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>" OnItemDataBound="repf_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td align="center" width="15%">
                            <asp:HiddenField ID="hfId" runat="server" Value='<%#Eval("id") %>' />
                            <asp:Label ID="lblOrderData" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OrderData","{0:yyyy-MM-dd}") %>'></asp:Label></td>
                        <td align="center" width="15%">
                            <asp:Label ID="lblRedE" runat="server"></asp:Label></td>
                        <td align="center" width="20%">
                            <asp:Label ID="lblDeducFee" runat="server" Text='<%#Eval("DeducFee") %>'> </asp:Label></td>
                        <td align="center" width="10%">
                            <asp:Label ID="lblState" runat="server"></asp:Label></td>
                        <td align="center" width="40%">
                            <asp:Label ID="lblRemark" runat="server"></asp:Label></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </ZL:ExRepeater>
        </table>
    </div>
</asp:Content>
