<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="MyAnswer.aspx.cs" Inherits="User_Guest_MyAnswer" ClientIDMode="Static" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>我的回答</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="index" data-ban="ask"></div>
<div class="container margin_t10">
<ol class="breadcrumb">
    <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
    <li class="active">我的回答</li>
	<div class="clearfix"></div>
</ol>
</div>
<div  class="container">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td style="width: 70%;">我的回答</td>
            <td style="width: 30%;">
                <select id="switch" onchange="switchChange(this)" class="form-control">
                    <option value="0">全部回答</option>
                    <option value="1">被采纳</option>
                    <option value="2">未采纳</option>
                    <option value="3">待解决</option>
                </select>
            </td>
        </tr>
        <ZL:ExRepeater ID="Repeater_ask" runat="server" PagePre="<tr><td colspan='2' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>" OnItemDataBound="Repeater_ask_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td style="width: 70%; padding-left: 10px;">
                        <a href='/Guest/Ask/SearchDetails.aspx?ID=<%# Eval("ID")%>' target="_blank"><%# Eval("Qcontent")%></a><br />
                        <label runat="server" id="count"></label>
                        个回答 | <%#Convert.ToDateTime(Eval("AddTime")).ToString("yyyy-MM-dd")%>
                    </td>
                    <td style="width: 30%; padding-left: 10px;">
                        <label runat="server" id="lbstatus"></label>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>
    </table>
</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        window.onload = function () {
            var type = location.href.split("type=")[1];
            if (type == 0 || type == 1 || type == 2 || type == 3) {
                document.getElementById("switch").value = type;
            }
        }
        function switchChange(obj) {
            location.href = "?type=" + obj.value;
        }
    </script>
</asp:Content>
