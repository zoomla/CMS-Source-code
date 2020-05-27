<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowChartCode.aspx.cs" Inherits="ZoomLaCMS.Manage.Plus.ShowChartCode" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>获取图表代码</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td align="left" style="height: 30px; padding-left: 60px; padding-bottom: 20px; padding-top: 30px;">
                <asp:TextBox ID="TxtChartCode" runat="server" TextMode="MultiLine" CssClass="form-control" Style="max-width: 578px; height: 90px;"></asp:TextBox>
                
            </td>
        </tr>
        <tr>
            <td class="text-center">
                <input id="clipBtn" type="button" value="获取图表代码" class="btn btn-primary" onclick="copy()" />
                <input type="button" value="返回列表" class="btn btn-primary" onclick="javascript: history.back()" />
            </td>
        </tr>
        <tr>
            <td align="left" style="padding-left: 60px;">
                <span style="color: Grey">提示：点击按钮复制代码，支持以GET方式定义图表大小，如：http://url/Plugins/Chart.aspx?Did=129&width=600&height=480<br />
                </span>
            </td>
        </tr>
    </table>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        function copy() {
            var innerHTML = "复制失败您的浏览器不支持复制，请手动复制代码。";
            if (window.clipboardData) {
                innerHTML = "复制成功您现在可以将代码粘贴（ctrl+v）到网页中预定的模板代码位置。";

                var str = $("#TxtChartCode").html();
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
