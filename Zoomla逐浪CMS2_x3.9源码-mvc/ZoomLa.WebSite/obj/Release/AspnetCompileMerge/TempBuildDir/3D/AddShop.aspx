<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddShop.aspx.cs" Inherits="ZoomLaCMS._3D.AddShop"  MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>店铺管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td class="tdleft td_l"><strong>用户名：</strong></td>
                <td>
                    <asp:DropDownList runat="server" ID="user_DP" CssClass="text_300 form-control"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="tdleft"><strong>店铺名：</strong></td>
                <td>
                    <asp:TextBox runat="server" ID="shopName_T" CssClass="form-control text_300"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdleft"><strong>店铺图片：</strong></td>
                <td>
                    <asp:TextBox runat="server" ID="shopImg_T" Style="width: 300px" CssClass="form-control text_300"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdleft"><strong>位置X轴：</strong></td>
                <td>
                    <asp:TextBox runat="server" ID="posX_T" MaxLength="5" Style="width: 50px" CssClass="form-control"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdleft"><strong>位置Y轴：</strong></td>
                <td>
                    <asp:TextBox runat="server" ID="posY_T" MaxLength="5" Style="width: 50px" CssClass="form-control"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center">
                    <asp:Button runat="server" ID="sure_Btn" Text="确定" OnClick="sure_Btn_Click" class="btn btn-primary" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>
