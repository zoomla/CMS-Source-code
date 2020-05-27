<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPaperClass.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.AddPaperClass" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>添加试题分类</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover    ">
        <tr>
            <td class="td_m text-right">类别名称:</td>
            <td><asp:TextBox ID="TypeName_T" runat="server" CssClass="form-control text_300"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="text-right">所属类别:</td>
            <td>
                <asp:DropDownList ID="Type_Drop" DataValueField="ID" DataTextField="TypeName" runat="server" CssClass="form-control text_300"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="text-right">备注:</td>
            <td>
                <asp:TextBox ID="Remind_T" runat="server" CssClass="form-control text_300" Height="150" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-right">排序:</td>
            <td>
                <asp:TextBox ID="Order_T" Text="0" runat="server" CssClass="form-control text_300 num"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="Save_Btn" CssClass="btn btn-primary" runat="server" Text="保存分类" OnClick="Save_Btn_Click" />
                <a href="Paper_Class_Manage.aspx" class="btn btn-primary">返回分类</a>
            </td>
        </tr>
    </table>
    <script type="text/javascript" src="/JS/ZL_Regex.js"></script>
    <script>
        $(function () {
            ZL_Regex.B_Num(".num");
        });
    </script>
</asp:Content>


