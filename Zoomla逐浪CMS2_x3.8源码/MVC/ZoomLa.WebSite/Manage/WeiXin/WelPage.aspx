<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WelPage.aspx.cs" Inherits="ZoomLaCMS.Manage.WeiXin.WelPage"  MasterPageFile="~/Manage/I/Default.master" ValidateRequest="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>欢迎语</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered table-striped">
        <tr><td>标题</td><td><asp:TextBox runat="server" ID="Title_T" CssClass="form-control m715-50" />
            <asp:RequiredFieldValidator ID="p1" runat="server" ControlToValidate="Title_T" Display="Dynamic" ForeColor="Red" SetFocusOnError="True" ErrorMessage="标题不能为空!" />
                       </td></tr>
        <tr><td>内容</td><td>
            <asp:TextBox runat="server" TextMode="MultiLine" ID="Content_T" CssClass="form-control m715-50" style="height:120px;" placeholder="请输入内容" />
            <asp:RequiredFieldValidator ID="p2" runat="server" ControlToValidate="Content_T" Display="Dynamic" ForeColor="Red" SetFocusOnError="True" ErrorMessage="内容不能为空!" />
                       </td></tr>
        <tr>
            <td>图片</td>
            <td>
                <asp:TextBox runat="server" ID="PicUrl_T" CssClass="form-control m715-50" placeholder="http://demo.z01.com/pic.jpg" />
                <input type="button" class="btn btn-info" value="上传图片" onclick="pic.sel('PicUrl_T');" />
                <input type="file" id="pic_up" class="hidden" onchange="pic.upload();" />
                <div class="rd_green margin_t5">图片链接,支持JPG、PNG格式，较好的效果为大图360*200，小图</div>
                <div class="rd_green margin_t5">如未上传图片,则以文本格式返回信息(文本格式可支持超链接)</div>
            </td>
        </tr>
        <tr><td>链接</td><td><asp:TextBox runat="server" ID="Url_T" CssClass="form-control m715-50"  placeholder="请输入链接"/>
            <div class="rd_green margin_t5">点击图文消息跳转链接</div></td></tr>
        <tr><td></td><td><asp:Button runat="server" ID="Save_Btn"  CssClass="btn btn-primary" Text="确定" OnClick="Save_Btn_Click"/></td></tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Controls/ZL_Webup.js"></script>
    <script>
        var pic = { id: "pic_up", txtid: null };
        pic.sel = function (id) { $("#" + pic.id).val(""); $("#" + pic.id).click(); pic.txtid = id; }
        pic.upload = function () {
            var fname = $("#" + pic.id).val();
            if (!SFileUP.isWebImg(fname)) { alert("请选择图片文件"); return false; }
            SFileUP.AjaxUpFile(pic.id, function (data) {
                var url = "<%:ZoomLa.Components.SiteConfig.SiteInfo.SiteUrl%>";
                $("#" + pic.txtid).val(url + data);
            });
        }
    </script>
</asp:Content>