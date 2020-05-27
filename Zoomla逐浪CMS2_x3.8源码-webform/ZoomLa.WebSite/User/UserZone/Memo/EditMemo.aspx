<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="EditMemo.aspx.cs" Inherits="EditMemo" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>编辑备忘录</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/Userzone/Default.aspx">我的空间</a></li>
        <li><a href="MemoList.aspx">备忘录列表</a></li>
        <li><a href="/User/UserZone/Memo/MemoContext.aspx?ID=<%= Request.QueryString["ID"] %>">我的备忘录</a></li>
        <li class="active">编辑备忘录</li>
    </ol>
    <div style="margin-bottom:10px;">
        <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
    </div>
    <div>
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td colspan="2" class="text-center"><strong>备忘录详细</strong></td>
            </tr>
            <tr>
                <td>标题：</td>
                <td align="left">
                    <asp:TextBox ID="txtTitle" CssClass="form-control" runat="server" style="max-width:300px;"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle" ErrorMessage="请输入备忘录的标题"></asp:RequiredFieldValidator><br />
                </td>
            </tr>
            <tr>
                <td>时间：</td>
                <td align="left">
                    <asp:TextBox ID="txtTime" runat="server" CssClass="form-control" onfocus="setday(this)" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });" style="max-width:300px;"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTime" ErrorMessage="请输入备忘录的时间"></asp:RequiredFieldValidator><br />
                </td>
            </tr>
            <tr>
                <td valign="top">内容：</td>
                <td align="left">
                    <asp:TextBox ID="txtContext" CssClass="form-control" runat="server" TextMode="MultiLine" Height="120px" style="max-width:300px;"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtContext" ErrorMessage="请输入备忘录的内容"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="提交" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/DatePicker/WdatePicker.js" type="text/javascript"></script>
</asp:Content>
