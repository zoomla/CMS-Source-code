<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContentList.aspx.cs" Inherits="ZoomLaCMS.Common.ContentList" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>节点选取</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="node_div" class="flolt_L">
        <asp:Literal runat="server" ID="NodeHtml_Lit" EnableViewState="false"></asp:Literal>
    </div>
    <iframe runat="server" id="User_IFrame" class="flolt_L contentstyle" src="" frameborder="0"></iframe>
    <ZL:ExGridView CssClass="table table-bordered" runat="server" ID="Egv" AutoGenerateColumns="false" EmptyDataText="无内容">
        <Columns>
            <asp:TemplateField HeaderText="内容标题">
                <ItemTemplate>
                    <a href='javascript:ReturnResult(<%#Eval("GeneralID") %>)'><%#Eval("Title") %></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
     <style type="text/css">
        a{text-decoration:none;}
        li a{color:green;font-size:14px;}
        td a{font-size:14px;color:black;}
        a:hover{text-decoration:underline;}
        *{font-family:'Microsoft YaHei';}
        #node_div ul li{list-style-type:none;}
        .flolt_L{float:left;}
        .contentstyle{margin-left:60px;width:800px;height:350px;display:none;}
        .contentTable{width:100%;color:brown;border:1px solid #ddd;}
    </style>
    <script type="text/javascript">
        function ShowChild() {
            $obj = $(event.srcElement);
            $obj.parent().parent().find("ul:eq(0)").toggle();
        }
        function ChkChild(obj) {
            $(obj).parent().find("input[name=nodeChk]").each(function () { this.checked = obj.checked; });
        }
        function GetResult() {
            var vs = "";
            $("#node_div input[name=nodeChk]:checked").each(function () { vs += $(this).val() + ","; });
            return vs;
        }
        function ReturnResult(r) {//支持window.open与frame引用
            if (opener) {
                opener.opener.DealResult(r);
            }
            else {
                window.parent.parent.DealResult(r);
            }
        }
        function showContent(data) {
            $("#User_IFrame").show();
            $("#User_IFrame").attr("src", data);
        }

    </script>
</asp:Content>