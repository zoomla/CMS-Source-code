<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRegular.aspx.cs" Inherits="ZoomLaCMS.Manage.User.Addon.AddRegular" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>添加规则</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped">
<tr><td class="td_l">充值金额</td><td>
    <ZL:TextBox runat="server" ID="Min_T" CssClass="form-control text_300" AllowEmpty="false" ValidType="FloatZeroPostive" />
                              </td></tr>
<tr><td>赠送金额</td><td><ZL:TextBox runat="server" ID="Purse_T" CssClass="form-control text_300" AllowEmpty="false" ValidType="FloatZeroPostive" /></td></tr>
<tr><td>赠送银币</td><td><ZL:TextBox runat="server" ID="SIcon_T" CssClass="form-control text_300" AllowEmpty="false" ValidType="FloatZeroPostive" /></td></tr>
<tr><td>赠送积分</td><td><ZL:TextBox runat="server" ID="Point_T" CssClass="form-control text_300" AllowEmpty="false" ValidType="FloatZeroPostive" /></td></tr>
<tr><td>备注</td><td><asp:TextBox runat="server" ID="UserRemind_T" CssClass="form-control text_500" MaxLength="50" /></td></tr>
<tr><td>备注(管理员)</td><td><asp:TextBox runat="server" ID="AdminRemind_T" CssClass="form-control text_500" MaxLength="50" /></td></tr>
<tr><td></td><td>
    <asp:Button runat="server" ID="Save_Btn" Text="保存信息" OnClick="Save_Btn_Click" CssClass="btn btn-primary" />
    <a href="RegularList.aspx" class="btn btn-default">返回列表</a>
</td></tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>