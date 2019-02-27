<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="MyAsk.aspx.cs" Inherits="User_Guest_MyAsk" ClientIDMode="Static"  %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>提问列表</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="index" data-ban="ask"></div>
    <div class="container margin_t10">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">提问列表</li>
		<div class="clearfix"></div>
    </ol>
    </div>
    <div class="container">
    <div class="us_seta">
        <table class="table table-bordered table-hover table-striped">
            <tr>
                <td style="width: 70%;">我的提问</td>
                <td style="width: 30%;">
                    <select id="switch" onchange="switchChange(this)" class="form-control">
                        <option value="0">全部提问</option>
                        <option value="2">已解决提问</option>
                        <option value="1">待解决提问</option>
                    </select></td>
            </tr>
            <ZL:ExRepeater ID="Repeater_ask" runat="server" PagePre="<tr><td colspan='2' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>" OnItemDataBound="Repeater_ask_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td style="width: 50%; padding-left: 10px;">
                            <a href='/Guest/Ask/Interactive.aspx?ID=<%# Eval("ID")%>' target="_blank"><%# Eval("Qcontent")%></a><br />
                            <label runat="server" id="count"></label>
                            个回答 | <%#Convert.ToDateTime(Eval("AddTime")).ToString("yyyy-MM-dd")%>
                        </td>
                        <td style="width: 30%; padding-left: 10px;">
                            <label runat="server" id="lbstatus"></label>
                        </td>
                        <%--<td style="width:30%; padding-left:10px;"><label id="txtView" runat="server"><asp:LinkButton ID="lbUpdate" CommandName="Update" Text="处理" runat="server"></asp:LinkButton></label>  </td>--%>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </ZL:ExRepeater>
        </table>
    </div>
        </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        window.onload = function () {
            var type = location.href.split("type=")[1];
            if (type == 0 || type == 1 || type == 2) {
                document.getElementById("switch").value = type;
            }
        }
        function switchChange(obj) {
            location.href = "?type=" + obj.value;
        }
    </script>
</asp:Content>
