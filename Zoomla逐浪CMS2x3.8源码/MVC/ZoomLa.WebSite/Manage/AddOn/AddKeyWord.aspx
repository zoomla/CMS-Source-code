<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeBehind="AddKeyWord.aspx.cs" Inherits="ZoomLaCMS.Manage.AddOn.AddKeyWord" %>
<asp:Content ContentPlaceHolderID="head" Runat="Server"><title>添加关键字</title></asp:Content>
<asp:Content ContentPlaceHolderID="Content" Runat="Server">
    <table class="table table-striped table-bordered">
    <tr>
        <td class="td_l"><strong>关键字名称：</strong></td><td>
            <ZL:TextBox ID="KeyWord_T" runat="server" class=" form-control text_300" AllowEmpty="false" />
        </td>
    </tr>
    <tr>
        <td><strong>关键字类别：</strong></td>
        <td>
            <label><input type="radio" name="keyttype_rad" value="1" checked="checked" />搜索关键字</label>
        </td>
    </tr>
    <tr>
        <td><strong>关键字权重：</strong></td>
        <td>
            <ZL:TextBox ID="TxtPriority" runat="server" class=" form-control text_300" AllowEmpty="false" ValidType="IntZeroPostive" Text="3" />
            <span class="rd_green">数字越大权重越高越被优先</span>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="保存信息" OnClick="EBtnSubmit_Click" runat="server" />
            <a href="KeyWordManage.aspx" class="btn btn-primary">返回列表</a>
        </td>
    </tr>
</table>
</asp:Content>