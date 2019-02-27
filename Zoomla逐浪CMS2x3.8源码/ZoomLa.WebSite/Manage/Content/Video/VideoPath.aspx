<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideoPath.aspx.cs" Inherits="Manage_Content_Video_VideoPath" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>路径配置</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered table-striped">
        <tr>
            <td class="td_l">启用云端服务器</td>
            <td>
                <label> <input type="radio" name="openftp_rad" value="1" checked="checked" />默认路径</label>
                <label> <input type="radio" name="openftp_rad" value="2" />启用云端服务器</label>
                <label> <input type="radio" name="openftp_rad" value="3" />云端存储模式(例如:百度云,Azure云)</label>
                <a href="<%=CustomerPageAction.customPath2+"File/FtpAll.aspx" %>">(添加云端服务器)</a>
            </td>
        </tr>
        <tr><td>操作</td>
            <td>
                <asp:Button runat="server" ID="Save_Btn" Text="保存" OnClick="Save_Btn_Click" CssClass="btn btn-primary" />
            </td>
            </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/SelectCheckBox.js"></script>
</asp:Content>