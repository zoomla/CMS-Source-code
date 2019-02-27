<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetPageOrder.aspx.cs" Inherits="Manage_Page_SetPageOrder" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>栏目排序</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="td_s"><strong>ID</strong></td>
            <td><strong>栏目名</strong></td>
            <td><strong>节点类型</strong></td>
            <td class="td_m"><strong>手动排序</strong></td>
            <td><strong>排序</strong></td>
        </tr>
        <asp:Repeater ID="RPT" runat="server" OnItemCommand="RPT_ItemCommand">
            <ItemTemplate>
                <tr>
                    <td><%#Eval("TemplateID") %></td>
                    <td><%#Eval("TemplateName") %></td>
                    <td><%#gettemptype() %></td>
                    <td>
                        <input type="text" class="l_input" style="width:40px; text-align:center" name="OrderField<%#Eval("TemplateID")%>" id="OrderField<%#Eval("TemplateID")%>" value="<%#Eval("OrderID") %>" />
                        <input type="hidden" name="PageValue" id="SpecIDValue" value="<%#Eval("TemplateID")%>" />
                    </td>
                    <td>
                        <asp:LinkButton runat="server" CommandName="UpMove" CommandArgument='<%# Eval("TemplateID") %>'>上移</asp:LinkButton>
                        |
					    <asp:LinkButton runat="server" CommandName="DownMove" CommandArgument='<%# Eval("TemplateID") %>'>下移</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <asp:Button ID="Save_Btn" runat="server" Text="批量更新排序" CssClass="btn btn-primary" OnClick="Save_Btn_Click" />
</asp:Content>


