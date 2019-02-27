<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailSender.aspx.cs" Inherits="Common_EmailSender" EnableViewStateMac="false" ValidateRequest="false" MasterPageFile="~/Common/Common.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>邮件发送</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container">
<table class="table">
 <tr>
    <td colspan="2">
        <strong>邮件订阅</strong>
    </td>
</tr>
<tr><td class="text-right td_s"><span>From：</span> </td><td><input id="SeEmail" class="form-control" runat="server" /></td></tr>
<tr><td class="text-right"><span>To：</span></td><td><input id="toEmail" class="form-control" runat="server" /></td></tr>
<tr><td class="text-right"><span>标题：</span></td><td ><input id="Title" class="form-control"  runat="server" /> </td></tr>
<tr><td class="text-right"><span>内容：</span></td>
<td> 
<textarea id="con" runat="server" style="height:300px;width:100%;"> </textarea>
<%=Call.GetUEditor("con") %>
</td>
</tr>
<tr><td colspan="2" class="text-center"><asp:Button ID="send" CssClass="btn btn-primary"  runat="server" OnClick="send_Click" Text="发送邮件" /></td></tr>
</table>
</div>
<div id="showMsg" runat="server"></div>
<asp:HiddenField ID="shopids" runat="server" /> 

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style>
.table td span{line-height:30px;}
</style>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<script>
function ActionSec(obj) {
    //obj -1未放匿名发送时未登录,0发送失败,1为成功   
    if (obj == 0) {
        alert("发送失败!");
    }
    if (obj == 1) {
        alert("发送成功!");
    }
    if (obj == -1) {
        alert("邮件发送失败！系统未开放匿名发送邮件，请登录后再试！");
        window.location.href = "/User/";
    }
}
</script>
<script type="text/javascript">
    function getval(n, val) {
        if (document.getElementById("ckShop" + n).checked) {
            document.getElementById("shopids").value += val + "|";
        }
        else {
            var str = document.getElementById("shopids").value.split('|');
            var strs = "";
            if (str != "") {

                for (var i = 0; i < str.length; i++) {

                    if (str[i] != "") {
                        if (val == str[i]) {
                            // strs = strs+ "";
                        }
                        else {
                            strs = strs + str[i] + "|";
                        }
                    }
                }
            }
            document.getElementById("shopids").value = strs;
        }
    }
    //    function getval(n, val) {
    //        document.getElementById("shopids1").value += "id" + n + "=" + val + "|";
    //    }
    //    function call() {
    //     $.ajax({
    //      type: "POST",
    //    url: "/common/emailsender.aspx",
    //      data: "title=邮件主题&con=邮件内容",
    //          success: function (msg) {
    //              switch (msg) {
    //                   case "0": alert("邮件发送失败"); break;
    //                     case "1": alert("邮件发送成功"); break;
    //                   case "2": alert("邮件发送失败！系统未开放匿名发送邮件，请登录后再试"); break;
    //                    default: alert("邮件发送失败"); break;
    //             }
    //            },
    //            error: function (msg) {
    //              alert("邮件发送失败！系统未开放匿名发送邮件请登录后再试");
    //           }
    //       });
    //    }
</script>
</asp:Content>