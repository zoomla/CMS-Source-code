<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvtoManage.aspx.cs" Inherits="manage_Shop_InvtoManage" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>发票类型</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
    <tr class="tdbg">
        <td width="10%" height="24" align="center" class="title">ID</td>
        <td width="20%" align="center" class="title">发票类型名称</td>
        <td width="20%" align="center" class="title">发票类型说明</td>
        <td width="20%" align="center" class="title">税率</td>
        <td width="20%" align="center" class="title">操作</td>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'"
                id="<%#Eval("id") %>" ondblclick="getinfo(this.id )">
                <td height="24" align="center">
                    <%#Eval("id") %>
                </td>
                <td height="24" align="center">
                    <%#Eval("InvtoType")%>
                </td>
                <td height="24" align="center">
                    <%#Eval("Remark")%>
                </td>
                <td height="24" align="center">
                   <%#Eval("Invto")%> %
                </td>
                <td height="24" align="center">
                    <a href="AddInvoType.aspx?id=<%#Eval("id") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                     <a href="?menu=del&id=<%#Eval("id") %>" onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    <tr class="tdbg">
        <td height="24" colspan="7" align="center" class="tdbgleft">
            <span style="text-align: center">
            共<asp:Label ID="Allnum" runat="server" Text=""></asp:Label>条数据
                <asp:Label ID="Toppage" runat="server" Text="" />
                <asp:Label ID="Nextpage" runat="server" Text="" />
                <asp:Label ID="Downpage" runat="server" Text="" />
                <asp:Label ID="Endpage" runat="server" Text="" />页次：
                <asp:Label ID="Nowpage" runat="server" Text="" />/
                <asp:Label ID="PageSize" runat="server" Text="" />页
                <asp:Label ID="pagess" runat="server" Text="" />
                <asp:TextBox ID="txtPage" runat="server" AutoPostBack="true" OnTextChanged="txtPage_TextChanged"></asp:TextBox> 条数据/页 转到第
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"  onselectedindexchanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
                页<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtPage" ErrorMessage="只能输入数字" Type="Integer" MaximumValue="100000" MinimumValue="0"></asp:RangeValidator>
                </span>
        </td>
    </tr>
    <tr>
        <td height="24" colspan="7">说明：“禁用”某送货方式后，前台订购时将不再显示此送货方式，但已有订单中仍然显示。
        </td>
    </tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function getinfo(id) {
            location.href = "AddInvoType.aspx?menu=edit&id=" + id + "";
        }
</script>
</asp:Content>