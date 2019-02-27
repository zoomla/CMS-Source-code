<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetNodeOrder.aspx.cs" Inherits="Manage_Page_SetNodeOrder" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
      <title>节点排序</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Literal ID="Literal1" runat="server" Visible="false"></asp:Literal>
    <table class="table table-striped table-bordered table-hover">
            <tr class="gridtitle" align="center" style="height: 25px;">
                <td style="width: 10%; height: 20px;"><strong>栏目ID</strong></td>
                <td style="width: 20%; height: 20px;"><strong>栏目名称</strong></td>
                <td style="width: 20%"><strong>节点目录</strong></td>
                <td style="width: 20%"><strong>栏目类型</strong></td>
                <td style="width: 10%"><strong>手动排序</strong></td>
                <td style="width: 20%"><strong>排序</strong></td>
            </tr>
            <asp:Repeater ID="RepSystemModel" runat="server" OnItemCommand="Repeater1_ItemCommand">
                <ItemTemplate>
                    <tr class="tdbg">
                        <td align="center">
                            <%#Eval("TemplateID")%>
                            <asp:HiddenField ID="Hid_TemplateID" runat="server" Value='<%#Eval("TemplateID")%>' Visible="false" />
                        </td>
                        <td align="center">
                            <%#Eval("TemplateName")%>
                        </td>
                        <td align="center">
                            <%# Eval("Identifiers")%>
                        </td>
                        <td align="center">
                            <%# GetNodeType(Eval("TemplateType", "{0}"))%>
                        </td>
                        <td align="center">
                            <input type="text" class="l_input" style="width: 40px; text-align: center" name="order_T"  value="<%#Eval("OrderID") %>" style="width:50px;text-align:center;" tabindex="1"/>
                            <input type="hidden" name="order_Hid" value="<%#Eval("templateID")+":"+Eval("OrderID") %>" />
                        </td>
                        <td align="center">
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="UpMove" CommandArgument='<%# Eval("TemplateID") %>'>上移</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="DownMove" CommandArgument='<%# Eval("TemplateID") %>'>下移</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <asp:Button ID="Button1" runat="server" Text="批量更新排序" CssClass="btn btn-primary" OnClick="Button1_Click" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>
