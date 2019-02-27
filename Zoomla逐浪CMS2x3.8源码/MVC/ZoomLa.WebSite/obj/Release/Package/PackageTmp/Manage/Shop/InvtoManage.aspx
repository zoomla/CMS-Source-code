<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvtoManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.InvtoManage"MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>发票类型</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
    <tr class="tdbg">
        <td style="width:10%">ID</td>
        <td style="width:20%">发票类型名称</td>
        <td style="width:20%">发票类型说明</td>
        <td style="width:20%">税率</td>
        <td style="width:20%">操作</td>
    </tr>
    <ZL:ExRepeater ID="IType_RPT" PageSize="10" runat="server" PagePre="<tr><td colspan='5' class='text-center'>" PageEnd="</td></tr>">
        <ItemTemplate>
            <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'" id="<%#Eval("id") %>" ondblclick="getinfo(this.id )">
                <td><%#Eval("id") %></td>
                <td><%#Eval("InvtoType")%></td>
                <td><%#Eval("Remark")%></td>
                <td><%#Eval("Invto")%> %</td>
                <td>
                    <a href="AddInvoType.aspx?id=<%#Eval("id") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                     <a href="?menu=del&id=<%#Eval("id") %>" onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</a>
                </td>
            </tr>
        </ItemTemplate>
    </ZL:ExRepeater>
    <tr>
        <td colspan="7">说明：“禁用”某送货方式后，前台订购时将不再显示此送货方式，但已有订单中仍然显示。
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