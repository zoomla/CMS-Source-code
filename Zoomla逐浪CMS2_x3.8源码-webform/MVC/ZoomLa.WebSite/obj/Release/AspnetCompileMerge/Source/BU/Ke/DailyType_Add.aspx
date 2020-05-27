<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DailyType_Add.aspx.cs" Inherits="ZoomLaCMS.MIS.Ke.DailyType_Add"  MasterPageFile="~/Plat/Empty.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head"><title></title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered table-striped">
        <tr>
            <td class="td_m">标题</td>
            <td><ZL:TextBox runat="server" ID="Title_T" CssClass="form-control text_300" AllowEmpty="false" /></td>
        </tr>
        <tr>
            <td>类型</td>
            <td>
                <asp:DropDownList CssClass="form-control text_md" runat="server" ID="Type_DP" >
                    <asp:ListItem Text="记录" Value="0"></asp:ListItem>
                    <asp:ListItem Text="小结" Value="1"></asp:ListItem>
                    <asp:ListItem Text="今日计划" Value="2"></asp:ListItem>
                    <asp:ListItem Text="备忘" Value="3"></asp:ListItem>
                    <asp:ListItem Text="待办" Value="4"></asp:ListItem>
                    <asp:ListItem Text="协商" Value="5"></asp:ListItem>
                    <asp:ListItem Text="求助" Value="6"></asp:ListItem>
                    <asp:ListItem Text="汇报" Value="7"></asp:ListItem>
                    <asp:ListItem Text="日志" Value="8"></asp:ListItem>
                    <asp:ListItem Text="消息" Value="9"></asp:ListItem>
                    <asp:ListItem Text="其它" Value="10"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>分享</td>
            <td>
                <asp:RadioButtonList runat="server" ID="IsShare_RB" RepeatDirection="Horizontal">
                    <asp:ListItem Text="是" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="否" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>推荐</td>
            <td>
                <asp:RadioButtonList runat="server" ID="IsElit_RB" RepeatDirection="Horizontal">
                    <asp:ListItem Text="是" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="否" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>提醒</td>
            <td>
                <asp:RadioButtonList runat="server" ID="IsWarn_RB" RepeatDirection="Horizontal">
                    <asp:ListItem Text="提醒我" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="不提醒" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>紧要程度</td>
            <td>
                <asp:DropDownList CssClass="form-control text_md" runat="server" ID="Level_DP" >
                    <asp:ListItem Text="不重要不紧急" Value="0"></asp:ListItem>
                    <asp:ListItem Text="重要" Value="1"></asp:ListItem>
                    <asp:ListItem Text="紧急" Value="2"></asp:ListItem>
                    <asp:ListItem Text="重要且紧急" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>描述</td>
            <td>
                <asp:TextBox runat="server" ID="Content_T" TextMode="MultiLine" Width="500" Height="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td></td>
            <td>
                <asp:LinkButton runat="server" ID="Save_Btn" CssClass="btn btn-primary" OnClick="Save_Btn_Click">保存</asp:LinkButton>
                <a href="javascript:;" class="btn btn-default" onclick="HideMe();">取消</a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
function HideMe() {
    parent.HideTypeDialog();
}
</script>
</asp:Content>
