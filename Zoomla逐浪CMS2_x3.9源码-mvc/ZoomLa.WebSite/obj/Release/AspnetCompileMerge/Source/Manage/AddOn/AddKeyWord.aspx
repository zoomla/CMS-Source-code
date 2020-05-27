<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeBehind="AddKeyWord.aspx.cs" Inherits="ZoomLaCMS.Manage.AddOn.AddKeyWord" %>

<asp:Content ContentPlaceHolderID="head" Runat="Server"><title>添加关键字</title></asp:Content>
<asp:Content ContentPlaceHolderID="Content" Runat="Server">
    <table class="table table-striped table-bordered table-hover">
    <tr align="center">
        <td colspan="2">
            <asp:Label ID="LblTitle" runat="server" Text="添加关键字" Font-Bold="True"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="right" >
            <strong>关键字名称：&nbsp;</strong></td>
        <td align="left">
            <asp:TextBox ID="TxtKeywordText" runat="server" class=" form-control pull-left" Width="150"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ValrKeywordText" ControlToValidate="TxtKeywordText" runat="server" ErrorMessage="关键字名称不能为空！" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right" >
            <strong>关键字类别：&nbsp;</strong></td>
        <td  align="left">
            <asp:RadioButtonList ID="RadlKeywordType" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True" Value="0">常规关键字</asp:ListItem>
                <asp:ListItem Value="1">搜索关键字</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td align="right" >
            <strong>关键字权重：&nbsp;</strong>
        </td>
        <td  align="left">
            <asp:TextBox ID="TxtPriority" runat="server" Columns="5" class=" form-control pull-left" Width="100"></asp:TextBox>
            <span style="color: blue">数字越大权重越高越被优先</span>
            <asp:RequiredFieldValidator ID="ValrPriority" ControlToValidate="TxtPriority" runat="server" ErrorMessage="关键字权重不能为空！" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="text-center">
            <asp:Button ID="EBtnModify" class="btn btn-primary"  Text="修改" OnClick="EBtnModify_Click" runat="server" Visible="false"/>
            <asp:Button ID="EBtnSubmit"  class="btn btn-primary" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
            <input name="Cancel" type="button" class="btn btn-primary" id="Cancel" value="取消" onclick="javascript: window.location.href = 'KeyWordManage.aspx'" />
        </td>
    </tr>
</table>
</asp:Content>