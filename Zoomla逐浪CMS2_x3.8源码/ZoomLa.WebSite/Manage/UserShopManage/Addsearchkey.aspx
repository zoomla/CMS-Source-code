<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Addsearchkey.aspx.cs" Inherits="manage_UserShopManage_Addsearchkey" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加关键字</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="text-center" colspan="2">
                <asp:Label ID="Label1" runat="server" Text="添加关键字"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdleft td_s"><b>关键字：</b></td>
            <td >
                <asp:TextBox ID="Searchkey" class="form-control" runat="server" Width="414px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Searchkey"
                    ErrorMessage="关键字不能为空!"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td class="tdleft"><b>分类：</b></td>
            <td >
                <asp:ListBox ID="Class" runat="server" CssClass="form-control" Height="201px" SelectionMode="Multiple" Width="273px"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td class="tdleft"><b>搜索次数：</b></td>
            <td >
                <asp:TextBox ID="SearchNum" class="form-control" runat="server" Width="160px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdleft"><b>搜索时间：</b></td>
            <td >
                <asp:TextBox ID="SearchTime" class="form-control" runat="server" Width="160px" onblur="setday(this);" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="SearchTime"
                    ErrorMessage="搜索时间不能为空!"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td class="tdleft"><b>首页显示：</b></td>
            <td >
                <asp:CheckBox ID="Showtop" runat="server" Text="是" /></td>
        </tr>
        <tr>
            <td class="tdleft"><b>推荐：</b></td>
            <td >
                <asp:CheckBox ID="Commend" runat="server" Text="推荐" /></td>
        </tr>
        <tr>
            <td width="100%" align="center" colspan="2">
                <asp:HiddenField ID="sid" runat="server" />
                <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="提交" OnClick="Button1_Click" />
                <asp:Button ID="Button2" class="btn btn-primary" runat="server" OnClientClick="location.href='ShopSearchKey.aspx';return false;" Text="返回" />
        </tr>
    </table>
</asp:Content>
