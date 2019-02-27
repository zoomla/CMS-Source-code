<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideoInfo.aspx.cs" Inherits="Manage_Content_Video_VideoInfo" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>视频信息</title>
    <style>
        img{width:100px;height:80px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="td_l text-right">ID:</td>
            <td>
                <asp:Label ID="ID_L" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="text-right">视频名称:</td>
            <td>
                <asp:TextBox ID="VName_T" CssClass="form-control text_300" runat="server" autofocus="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-right">视频路径:</td>
            <td>
                <asp:TextBox ID="VPath_T" CssClass="form-control text_300" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-right">视频大小:</td>
            <td>
                <asp:TextBox ID="VSize_T" CssClass="form-control text_300" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-right">视频长度:</td>
            <td>
                <asp:TextBox ID="VTime_T" CssClass="form-control text_300" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-right">缩略图:</td>
            <td>
                <img src="#" onerror="shownopic(this);" runat="server" id="Thum_Img" />
                <button type="button" onclick="SelFile()" class="btn btn-primary">选择缩略图</button>
                <asp:HiddenField ID="Thum_Hid" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="text-right">上传者:</td>
            <td>
                <asp:Label ID="UserName_L" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="text-right">备注:</td>
            <td>
                <asp:TextBox ID="Desc_T" CssClass="form-control text_300" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-center" colspan="2">
                <asp:Button ID="SaveInfo_B" runat="server" Text="保存" CssClass="btn btn-primary" OnClick="SaveInfo_B_Click" />
                <a href="VideoList.aspx" class="btn btn-primary">返回</a>
            </td>
        </tr>
    </table>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        var diag = new ZL_Dialog();
        function SelFile() {
            diag.title = "选择在线图片";
            diag.url = "/Common/SelFiles.aspx";
            diag.maxbtn = false;
            diag.ShowModal();
        }
        function PageCallBack(action, url, pval) {
            $("#Thum_Img").attr('src', url.split('|')[0]);
            $("#Thum_Hid").val(url.split('|')[0]);
            diag.CloseModal();
        }
    </script>
</asp:Content>

