<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="AddStructure.aspx.cs" Inherits="User_UserZone_AddStructure" %>
<asp:Content ContentPlaceHolderID="head" runat="Server"><title>创建企业结构</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="index" data-ban="zone"></div>
    <ol class="container breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="Structure.aspx">企业结构</a></li>
        <li class="active">创建企业结构</li>
    </ol>
    <div class="container padding0" style="margin-top: 5px;">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td class="td_l"><strong>结构名：</strong></td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtProjectName" class=" form-control text_300" runat="server" />
                    <asp:Label ID="LblMessage" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr><td></td>
                <td>
                    <asp:Button ID="BtnCommit" runat="server" Text="提交" class="btn btn-primary" OnClick="Button1_Click" />
                    <asp:Button ID="Btn" runat="server" Text="返回" class="btn btn-primary" OnClick="Button2_Click" />
                </td>
            </tr>

        </table>
    </div>
</asp:Content>
