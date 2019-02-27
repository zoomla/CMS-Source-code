<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NodeList.aspx.cs" Inherits="ZoomLaCMS.Manage.Common.NodeList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>节点选择</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="node_div">
        <asp:Literal runat="server" ID="NodeHtml_Lit" EnableViewState="false"></asp:Literal>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        $(function () {
            $("[name='nodes']").click(function () {
                var nodename = $(this).attr('data-name');
                //var parentnode = $(this).closest("ul").parent();
                //if (parentnode.find('.list_span').length > 0) {//有父节点
                //    nodename = parentnode.find('.list_span').text() + ">>" + nodename;
                //}
                if (parent.ShowNode) { parent.ShowNode(nodename) }
                parent.SelNode($(this).val());
            });
        })
        function ShowChild(obj) {
            $obj = $(obj);
            if ($obj.prev().find('span').hasClass('fa-folder')) {
                $obj.next().show();
                $obj.prev().find('span').removeClass('fa-folder').addClass('fa-folder-open');
            } else {
                $obj.next().hide();
                $obj.prev().find('span').removeClass('fa-folder-open').addClass('fa-folder');
            }
        }
    </script>
</asp:Content>

