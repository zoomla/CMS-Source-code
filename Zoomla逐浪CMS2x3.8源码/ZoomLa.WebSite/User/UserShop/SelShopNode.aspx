<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelShopNode.aspx.cs" MasterPageFile="~/User/Default.master" Inherits="User_UserShop_SelProType" %>
<%@ Register Src="WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc2" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>选择商品类型</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="shop" data-ban="store"></div>
    <div class="container margin_t5">
        <ol class="breadcrumb" style="margin-bottom: 0px;">
            <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
            <li class="active">选择商品类别</li>
        </ol>
    </div>
    <div class="container">
        <div class="btn_green">
            <uc2:WebUserControlTop ID="ShopNav" runat="server" />
        </div>
        <table class="table btn_green btn_green_lg">
            <tr>
                <td>
                    <asp:ListBox ID="class0" DataTextField="NodeName" DataValueField="NodeID" CssClass="form-control" runat="server" Height="280px" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="class0_SelectedIndexChanged"></asp:ListBox>
                </td>
                <td>
                    <asp:ListBox ID="class1" DataTextField="NodeName" DataValueField="NodeID" CssClass="form-control" runat="server" Height="280px" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="class1_SelectedIndexChanged"></asp:ListBox>
                </td>
                <td>
                    <asp:ListBox ID="class2" DataTextField="NodeName" DataValueField="NodeID" CssClass="form-control" runat="server" Height="280px" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="class2_SelectedIndexChanged"></asp:ListBox>
                </td>
                <td>
                    <asp:ListBox ID="class3" DataTextField="NodeName" DataValueField="NodeID" CssClass="form-control" runat="server" Height="280px" Width="180px" AutoPostBack="True"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="text-center">
                    <a runat="server" id="Add_Href" class="btn btn-primary">添加商品</a>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>