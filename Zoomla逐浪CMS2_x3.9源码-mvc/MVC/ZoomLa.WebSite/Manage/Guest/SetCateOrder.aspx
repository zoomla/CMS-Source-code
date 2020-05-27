<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetCateOrder.aspx.cs" Inherits="ZoomLaCMS.Manage.Guest.SetCateOrder" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>栏目排序</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover text-center">
        <tr>
		<td style="width:10%;">
			<strong>栏目ID</strong>
		</td>
		<td>
			<strong>分类名称</strong>
		</td>
		<td style="width:20%;">
			<strong>栏目类型</strong>
		</td>
        <td style="width:10%;">
            <strong>手动排序</strong>
        </td>
		<td>
			<strong>排序</strong>
		</td>
	   </tr>
        <asp:Repeater ID="RepCate_rp" runat="server">
		<ItemTemplate>
        <tr>
            <td id="CateID"><%#Eval("CateID") %></td>
            <td><%#Eval("CateName")%></td>
            <td><%#(Eval("GType").ToString()=="0")?"留言栏目":"贴吧栏目"%></td>
            <td>
                <input id="orderId_hid" name="SetOrder" type="text" class="text-center" style="max-width:30px;" value="<%#Eval("OrderID") %>" />
            </td>
            <td><a onclick="uptd(this)">上移</a>|
            <a onclick="downtd(this)">下移</a>
            <input id="oldOrder_hid" type="hidden" value="<%#Eval("OrderID") %>" />
            </td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="5"><asp:Button ID="SaveOrder_B" OnClientClick="return SaveOrder()" CssClass="btn btn-primary" OnClick="SaveOrder_B_Click" runat="server" Text="保存排序" /></td>
        </tr>
    </table>
    <input type="hidden" runat="server" id="changeids" value="" />
    <input type="hidden" runat="server" id="changeorders" value="" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function PostToCS(a, v, callBack) {
            $.ajax({
                type: "Post",
                data: { action: a, value: v },
                success: function (data) { callBack(data); },
                error: function () { callBack(data); }
            });
        }
        function uptd(obj) {
            var onthis = $(obj).parent().parent();
            var getup = $(obj).parent().parent().prev();
            if ($(getup).find("#CateID").text()=="") {
                alert("已经是首行了！");
                return;
            }
            switchID(onthis, getup);
            $(getup).before(onthis);
        }
        function downtd(obj) {
            var onthis = $(obj).parent().parent();
            var getdown = $(obj).parent().parent().next();
            if ($(getdown).find("#CateID").text() == "") {
                alert("已经是行尾了！");
                return;
            }
            switchID(onthis,getdown);
            $(getdown).after(onthis);
        }
        function switchID(tr1, tr2)//传入要交换Tr
        {
            $hid1 = $(tr1).find("#orderId_hid");
            $hid2 = $(tr2).find("#orderId_hid");
            var oid1 = $hid1.val();
            var oid2 = $hid2.val();
            var mid1 = $(tr1).find("#CateID").text();
            var mid2 = $(tr2).find("#CateID").text();
            $hid1.val(oid2);
            $hid2.val(oid1);
            //PostToCS("UpdateOrder", mid1 + ":" + oid2 + "," + mid2 + ":" + oid1, function (data){ });
        }
        function SaveOrder() {
            var staut = true;
            $("[name=SetOrder]").each(function (i, d) {
                var onthis = $(d).parent().parent();
                if (!isNaN($(d).val())) {
                    if ($(onthis).find("#oldOrder_hid").val()!=$(d).val()) {
                        $("#changeids").val($("#changeids").val() + $(onthis).find("#CateID").text() + ",");
                        $("#changeorders").val($("#changeorders").val() + $(d).val() + ",");
                    }
                } else {
                    alert("排序只能数字!");
                    $("#changeids").val("");
                    $("#changeorders").val("");
                    staut = false;
                    return false;
                }
            });
            return staut;
        }
        function Refresh() {
            parent.location = parent.location;
        }
    </script>
</asp:Content>