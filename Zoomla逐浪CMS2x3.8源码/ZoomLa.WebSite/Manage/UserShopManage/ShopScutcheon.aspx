<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopScutcheon.aspx.cs" Inherits="manage_UserShopManage_ShopScutcheon" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>品牌管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td width="105" class="text-center" class="title"><span class="tdbgleft">
                <asp:CheckBox ID="chkAll" runat="server" />
            </span></td>
            <td width="253" class="text-center">品牌名称</td>
            <td width="81" class="text-center">品牌分类</td>
            <td width="51" class="text-center">已启用</td>
            <td width="75" class="text-center"><span>属性</span></td>
            <td width="304" class="text-center"><span></span><span class="tdbgleft">操作</span></td>
        </tr>
        <asp:Repeater ID="Trademarklist" runat="server">
            <ItemTemplate>
                <tr ondblclick="editScutcheon('<%# Eval("id") %>')">
                    <td class="text-center">
                        <input type="checkbox" name="Item" value="<%#Eval("id") %>" /></td>
                    <td class="text-center"><%#Eval("Trname") %></td>
                    <td class="text-center"><%#Eval("TrClass")%></td>
                    <td class="text-center"><%#showstop2(DataBinder.Eval(Container, "DataItem.id", "{0}"))%></td>
                    <td class="text-center"><%#showtop2(DataBinder.Eval(Container, "DataItem.id", "{0}"))%> <%#showjian2(DataBinder.Eval(Container, "DataItem.id", "{0}"))%></td>
                    <td class="text-center">&nbsp; <a href="AddShopBrand.aspx?menu=edit&id=<%#Eval("id") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                        <a href="?menu=delete&id=<%#Eval("id") %>" onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" class="option_style"><i class="fa fa-trash-o" title="删除"></i></a>
                         <%#showstop(DataBinder.Eval(Container, "DataItem.id", "{0}"))%> 
                        <%#showtop(DataBinder.Eval(Container, "DataItem.id", "{0}"))%> 
                        <%#showjian(DataBinder.Eval(Container, "DataItem.id", "{0}"))%> 
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="6" class="text-center" class="tdbgleft">共
                    <asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
                条数据 
                    <asp:Label ID="Toppage" runat="server" Text="" />
                <asp:Label ID="Nextpage" runat="server" Text="" />
                <asp:Label ID="Downpage" runat="server" Text="" />
                <asp:Label ID="Endpage" runat="server" Text="" />
                页次：<asp:Label ID="Nowpage" runat="server" Text="" />/<asp:Label ID="PageSize" runat="server" Text="" />页 
                    <asp:Label ID="pagess" runat="server" Text="" />条数据/页  转到第<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
                    </asp:DropDownList>页</td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Button ID="Button1" class="btn btn-primary" Text="删除选中品牌" runat="server" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        $().ready(function () {
            $("#chkAll").click(function () {//EGV 全选
                selectAllByName(this, "Item");
            });
        })
        function editScutcheon(id) {
            location.href = "AddShopBrand.aspx?menu=edit&id=" + id;
        }
    </script>
</asp:Content>
