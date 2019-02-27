<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebUserControlSearchLeft.ascx.cs"
    Inherits="FreeHome.FriendSearch.WebUserControlSearchLeft" %>
<form action="FriendSearch_result.aspx" id="formme" method="post" name="formme">
<div class="s1">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td colspan="2" align="center">
                快速搜索
            </td>
        </tr>
        <tr>
            <td width="31%" align="center">
                性别
            </td>
            <td width="69%">
                <asp:RadioButtonList ID="RadioButtonList1" name="RadioButtonList1" runat="server"
                    RepeatDirection="Horizontal">
                    <asp:ListItem>男生</asp:ListItem>
                    <asp:ListItem Selected="True">女生</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td align="center">
                年龄
            </td>
            <td>
                &nbsp;<asp:TextBox ID="TextBox1" runat="server" Width="48px"></asp:TextBox>~
                <asp:TextBox ID="TextBox2" runat="server" Width="48px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center">
                地区
            </td>
            <td>
                &nbsp;<asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;
            </td>
            <td>
                &nbsp;<asp:DropDownList ID="DropDownList4" runat="server" Visible="false">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;
            </td>
            <td>
                &nbsp;<input id="Button1" runat="server" type="submit" value="快速搜索" />
            </td>
        </tr>
    </table>
</div>
</form>
