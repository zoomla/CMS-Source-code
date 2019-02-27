<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowJSCode.aspx.cs" Inherits="ZoomLa.WebSite.Manage.AddOn.ShowJSCode" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>获取广告代码</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr class="tdbg">
            <td align="left" style="height: 30px; padding-left: 60px; padding-bottom: 20px; padding-top: 30px;">
                <asp:TextBox ID="TxtZoneCode" runat="server" TextMode="MultiLine" class="form-control" Style="max-width: 578px; min-height: 60px; padding: 5px;"></asp:TextBox>
                &nbsp;
                <input id="Button1" type="button" value="获取广告代码" class=" btn btn-primary btn-xs" onclick="copy()" />
                <input id="Button2" type="button" value="返回列表" class="btn btn-primary btn-xs" onclick="javascript: history.back()" /></td>
        </tr>
        <tr class="tdbg">
            <td align="left" style="padding-left: 60px;">
                <span style="color: Grey">调用方法：点击按钮复制代码，粘贴到网页模板中的指定位置（飘、悬浮类广告放于模板最下方）即可。</span></td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        function copy() {
            var innerHTML = "复制广告JS失败您的浏览器不支持复制，请手动复制代码。";
            if (window.clipboardData) {
                innerHTML = "复制广告JS成功您现在可以将代码粘贴（ctrl+v）到网页中预定位置。";

                var str = $("#TxtZoneCode").html();
                while (str.indexOf("&lt;") >= 0) {
                    str = str.replace("&lt;", "<");
                }
                while (str.indexOf("&gt;") >= 0) {
                    str = str.replace("&gt;", ">");
                }

                window.clipboardData.setData("Text", str);
                alert(innerHTML);
            }
            else { alert(innerHTML); }
        }
    </script>
</asp:Content>
