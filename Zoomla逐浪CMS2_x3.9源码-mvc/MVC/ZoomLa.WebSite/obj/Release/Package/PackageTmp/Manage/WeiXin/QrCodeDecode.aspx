<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QrCodeDecode.aspx.cs" Inherits="ZoomLaCMS.Manage.WeiXin.QrCodeDecode" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>二维解码</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr class="tdbg">
            <td class="tdbgleft" width="150">选择图片</td>
            <td width="350">
                <ZL:FileUpload ID="FupFile" runat="server" />
                <%--<asp:FileUpload ID="FupFile" runat="server" />--%>
                <br />
                <asp:Label ID="ReturnManage" runat="server"></asp:Label></td>
            <td rowspan="3">
                <asp:Image ID="ImgCode" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" width="150">解码内容为：</td>
            <td>
                <asp:Label ID="txtEncodeData" runat="server" TextMode="MultiLine" Width="300" Height="100"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td></td>
            <td></td></tr></table>
     </asp:Content>