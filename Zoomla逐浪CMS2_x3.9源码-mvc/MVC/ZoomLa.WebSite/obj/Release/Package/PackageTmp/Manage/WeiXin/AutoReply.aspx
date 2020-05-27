<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoReply.aspx.cs" Inherits="ZoomLaCMS.Manage.WeiXin.AutoReply"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>自动回复</title>
<style>.td_md{width:100px;line-height:30px !important;}</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="alert alert-info" role="alert">
     <span class="fa fa-info-circle"></span> 图片与语音需在素材管理中添加
    </div>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="text-right td_md">客户回复:</td>
            <td>
                <asp:TextBox ID="GuestMsg_T" runat="server" CssClass="form-control text_300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-right td_md">自动回复:</td>
            <td>
                <asp:TextBox ID="Content_T" runat="server" TextMode="MultiLine" CssClass="form-control text_300" Height="200" MaxLength="1000" placeholder="最多1000个字符"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td></td><td ><asp:Button ID="Save_B" runat="server" CssClass="btn btn-primary" OnClick="Save_B_Click" Text="添加配置" /></td>
        </tr>
    </table>
</asp:Content>

