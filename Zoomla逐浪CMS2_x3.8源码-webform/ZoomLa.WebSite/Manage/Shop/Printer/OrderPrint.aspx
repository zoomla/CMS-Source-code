<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderPrint.aspx.cs" Inherits="Manage_Shop_Printer_OrderPrint" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>订单输出</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered">
        <tr>
            <td><strong>选择设备</strong></td>
            <td>
                <ZL:Repeater ID="RPT" runat="server">
                    <ItemTemplate>
                        <label style="margin-right:15px;"><input type="radio" name="Dev_R" value="<%#Eval("ID") %>" <%#Eval("IsDefault").ToString().Equals("1") ? "checked='true'":"" %> /><%#Eval("Alias") %></label>
                    </ItemTemplate>
                </ZL:Repeater>
            </td>
        </tr>
        <tr>
            <td class="td_m"><strong>选择模板:</strong></td>
            <td>
                <asp:DropDownList CssClass="form-control text_md" runat="server" ID="Tlp_DP"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td><strong>选择订单</strong></td>
            <td>
                <asp:DropDownList CssClass="form-control text_md" runat="server" ID="Order_DP"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td><strong>打印数量</strong></td>
            <td>
                <ZL:TextBox ID="Num_T" runat="server" CssClass="form-control text_md" AllowEmpty="false" Text="1" ValidType="Int" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button runat="server" ID="Print_Btn" OnClick="Print_Btn_Click" Text="发送打印" CssClass="btn btn-primary" />
                <a href="MessageList.aspx" class="btn btn-primary">查看流水</a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>
