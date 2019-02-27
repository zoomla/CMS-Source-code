<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NodeList.aspx.cs" Inherits="Common_NodeList" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>节点选取</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <div id="node_div" style="min-height:300px;">
            <asp:Literal runat="server" ID="NodeHtml_Lit" EnableViewState="false"></asp:Literal>
        </div>
        <div runat="server" id="opdiv" style="position: fixed; border-radius: 3px; padding: 5px;top:2px;left:200px;display:none;"><!-- box-shadow: 0 4px 20px 1px rgba(0,0,0,0.2);-->
            <input type="button" id="sure_Btn" value="确定" class="btn btn-primary" onclick="ReturnResult(GetResult());" />
        </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <style type="text/css">
        *{font-family:'Microsoft YaHei';} 
    </style>
    <script type="text/javascript">
        function ShowChild(obj) {
            $obj = $(obj);//event.srcElement || 
            $obj.siblings("ul").toggle();
            //$obj.parent().parent().find("ul:eq(0)").toggle();
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
            switch ("<%=Source%>") {
                case "content":
                    parent.PageCallBack("pushcon", nodeArr);//文章推送
                    break;
                default:
                    parent.DealResult(nodeArr);
                    break;
            }
        }
        function checkAll() {
            $("[name=nodeChk]").each(function (i, j) {
                j.checked = $("#AllCheck")[0].checked;
            });
        }
        //给父页面调用
        function SureFunc() {
            ReturnResult(GetResult());
        }
    </script>
</asp:Content>
