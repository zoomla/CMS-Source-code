<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommentView.aspx.cs" Inherits="ZoomLaCMS.Manage.I.ASCX.CommentView" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>查看评论</title>
    <script src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <div>
        <table class="table table-bordered">
            <tr>
                <td>评论ID</td>
                <td><asp:Label ID="CommentID" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>标题</td>
                <td>
                    <asp:TextBox runat="server" ID="Title_T" CssClass="form-control text_300"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>评论内容</td>
                <td>
                    <asp:TextBox runat="server" ID="Contents" TextMode="MultiLine" CssClass="form-control text_300" style="height:150px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>评论时间</td>
                <td>
                    <asp:TextBox runat="server" ID="CommentTime" CssClass="form-control text_300" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' })"  ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>评论分数</td>
                <td><asp:Label ID="commScore" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>审核状态</td>
                <td><asp:Label ID="commStatus" runat="server"></asp:Label></td>
            </tr>
            <tr><td></td><td>
                <asp:Button runat="server" ID="Sure_Btn" Text="修改" OnClick="Sure_Btn_Click" CssClass="btn btn-primary" />
                <a href="ShowContent.aspx?Gid=<%:Gid %>&modeid=<%:modeid %>" class="btn btn-default">返回</a>
                         </td></tr>
        </table>
    </div>
</asp:Content>

