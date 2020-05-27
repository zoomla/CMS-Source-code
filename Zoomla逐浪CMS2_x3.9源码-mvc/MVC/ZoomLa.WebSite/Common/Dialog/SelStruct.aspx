<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelStruct.aspx.cs" Inherits="ZoomLaCMS.Common.Dialog.SelStruct" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>选择部门</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="node_div" style="min-height: 300px;">
        <asp:Literal runat="server" ID="NodeHtml_Lit" EnableViewState="false"></asp:Literal>
    </div>
    <div style="position: fixed; border-radius: 3px; padding: 5px; top: 2px; left: 200px;">
        <input type="button" id="sure_Btn" value="确定" class="btn btn-primary" onclick="ReturnResult(GetResult());" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script>
        function ShowChild() {
            $obj = $(event.srcElement);
            $obj.parent().parent().find("ul:eq(0)").toggle();
        }
        function ChkChild(obj) {
            $(obj).parent().parent().find("input[name=nodeChk]").each(function () { this.checked = obj.checked; });
        }
        function GetResult() {
            var nodeArr = [];
            $("[name=nodeChk]:checked").each(function () {
                var node = { nodeid: $(this).val(), nodename: $(this).data("name") };
                nodeArr.push(node);
            });
            return nodeArr;
        }
        function ReturnResult(nodeArr) {
            parent.DealResult(nodeArr, "struct", "<%=CName%>");
        }
        function checkAll() {
            $("[name=nodeChk]").each(function (i, j) {
                j.checked = $("#AllCheck")[0].checked;
            });
        }
    </script>
</asp:Content>
