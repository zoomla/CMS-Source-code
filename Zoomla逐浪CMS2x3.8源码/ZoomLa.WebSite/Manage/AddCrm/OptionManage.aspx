<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OptionManage.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="Manage_AddCrm_OptionManage" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>选项管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td>序号
            </td>
            <td>选项名称
            </td>
            <td>展现形式
            </td>
            <td>是否启用
            </td>
            <td>操作
            </td>
        </tr>
        <ZL:ExRepeater ID="RPT" runat="server" OnItemCommand="RPT_ItemCommand" PageSize="20" PagePre="<tr class='text-center'><td colspan='5'>" PageEnd="</td></tr>">
            <ItemTemplate>
                <tr style="width: 500px" title="双击修改" ondblclick="location='AddOption.aspx?tagname=<%# Eval("tagName") %>';">
                    <td>
                        <%# Eval("ID")%>
                    </td>
                    <td>
                        <%#Eval("displayName") %>
                    </td>
                    <td>
                        <%#GetOptionStr() %>
                    </td>
                    <td>
                        <%#Eval("enable").Equals("True")?"是":"否" %>
                    </td>
                    <td>
                        <a href="AddOption.aspx?tagname=<%#Eval("tagname") %>" title="修改"><i class="fa fa-pencil"></i></a>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Del" CommandArgument='<%#Eval("tagName") %>' OnClientClick="return confirm('确定要删除?');" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>
    </table>
</asp:Content>
