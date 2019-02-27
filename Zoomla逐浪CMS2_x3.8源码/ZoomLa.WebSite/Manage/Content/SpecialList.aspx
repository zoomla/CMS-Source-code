<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SpecialList.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="ZoomLa.Web.Site.Content.SpecialList" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>选择专题</title>
    <script type="text/javascript">
        function category() { alert("你选择的是专题类别，请选择专题！"); }
        function SetSpec(specname, specid) {
            parent.UpdateSpe(specname, specid);
        }
    </script>
    <style type="text/css">
        * {
            font-family: 'Microsoft YaHei';
        }
        
        #node_div ul li {
            list-style-type: none;
        }
    </style>
    <script type="text/javascript">
        function ShowChild() {
            $obj = $(event.srcElement);
            $obj.parent().parent().find("ul:eq(0)").toggle();
        }
        function ChkChild(obj) {
            $(obj).parent().find(".nodechk").each(function () { this.checked = obj.checked; });
        }
        function GetResult() {
            var vs = [];
            $(".nodechk:checked").each(function () {
                vs.push({ id: $(this).val(), name: $(this).data("name") })
            });
            return JSON.stringify(vs);
        }
        function ReturnResult(r) {//支持window.open与frame引用
            if (opener) {
                opener.DealResult(r);
            }
            else {
                parent.DealResult(r);
            }
        }
        function checkAll() {
            $(".nodechk").each(function (i, j) {
                j.checked = $("#AllCheck")[0].checked;
            });
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="node_div">
            <asp:Literal runat="server" ID="NodeHtml_Lit" EnableViewState="false"></asp:Literal>
        </div>
        <div>
            <asp:TreeView ID="tvNav" runat="server" ExpandDepth="0" ShowLines="True" EnableViewState="false"
                NodeIndent="10" ImageSet="Simple2" Width="294px">
                <NodeStyle BorderStyle="None" />
            </asp:TreeView>
        </div>
</asp:Content>
