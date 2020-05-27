<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupSort.aspx.cs" Inherits="ZoomLaCMS.Plat.Admin.GroupSort" MasterPageFile="~/Plat/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Head"><title>部门排序</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped">
    <tr><td style="width:40px;"></td><td>ID</td><td>名称</td><td>手动排序</td><td>排序</td></tr>
    <asp:Repeater runat="server" ID="RPT" EnableViewState="false">
        <ItemTemplate>
            <tr class="order_tr" id="tr_<%#Eval("ID") %>">
                <td><input type="checkbox" name="idchk" value="<%#Eval("ID") %>" checked="checked" /></td>
                <td><%#Eval("ID") %></td>
                <td><%#Eval("GroupName") %></td>
                <td>
                    <input type="text"  style="width:60px; text-align:center" class="order_t" name="idtxt_<%#Eval("ID")%>" value="<%#Eval("OrderID") %>" />
                </td>
                <td>
                    <a href="javascript:;" onclick="sort.up('<%#Eval("ID") %>');"><i class="fa fa-long-arrow-up"></i> 上移</a>
                    <a href="javascript:;" onclick="sort.down('<%#Eval("ID") %>');"><i class="fa fa-long-arrow-down"></i> 下移</a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
<asp:Button runat="server" ID="BatOrder_Btn" Text="保存排序" OnClick="BatOrder_Btn_Click" CssClass="btn btn-info" />
<input type="button" value="整理序列号" class="btn btn-info" onclick="sort.reorder();" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
//用于table布局下排序
var sort = {};
sort.up = function (id) {
    var $tr = $("#tr_" + id);
    var $pre = $tr.prev(".order_tr");
    if ($pre.length > 0) {
        //交换orderID和位置
        var temp = sort.getorder($tr);
        sort.setorder($tr, sort.getorder($pre));
        sort.setorder($pre, temp);
        $pre.before($tr);
    }
}
sort.down = function (id) {
    var $tr = $("#tr_" + id);
    var $next = $tr.next(".order_tr");
    if ($next.length > 0) {
        //交换orderID和位置
        var temp = sort.getorder($tr);
        sort.setorder($tr, sort.getorder($next));
        sort.setorder($next, temp);
        $next.after($tr);
    }
}
//重新从1开始生成序列号(根据tr顺序)
sort.reorder = function () {
    var $trs = $(".order_tr");
    for (var i = 0; i < $trs.length; i++) {
        $($trs[i]).find(".order_t").val((i + 1));
    }
}
sort.getorder = function ($tr) {
    return $tr.find(".order_t").val();
}
sort.setorder = function ($tr, order) {
    $tr.find(".order_t").val(order);
}
</script>
</asp:Content>