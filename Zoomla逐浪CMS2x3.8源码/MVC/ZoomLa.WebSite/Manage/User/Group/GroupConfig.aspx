<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupConfig.aspx.cs" Inherits="ZoomLaCMS.Manage.User.GroupConfig" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>会员组设置</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <asp:Literal ID="LTitle" runat="server" Text="会员组设置"></asp:Literal></td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 237px"><strong>会员组名称：</strong></td>
            <td>
                <asp:Label ID="GroupName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 237px"><strong>会员组描述：</strong></td>
            <td>
                <asp:Label ID="GroupInfo" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 237px"><strong>选择用户模型：</strong></td>
            <td>
                <asp:RadioButtonList ID="GroupModel" runat="server"></asp:RadioButtonList>
                <!--单选用这个-->
                <asp:CheckBoxList ID="CheckBoxList1" runat="server"></asp:CheckBoxList>
                <!--多选用这个-->
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;
        <asp:Button ID="EBtnSubmit" Text="保存" runat="server" OnClick="EBtnSubmit_Click" class="btn btn-primary" />
                <a href="GroupManage.aspx" class="btn btn-primary margin_l5">取消</a></td>
        </tr>
    </table>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>
