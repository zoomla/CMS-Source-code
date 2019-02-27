<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectCourse.aspx.cs" Inherits="manage_Question_SelectCourse" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>选择课程</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table  class="table table-striped table-bordered table-hover">
    <tr class="tdbg">
        <td align="center" colspan="7" class="title" width="100%">课 程 列 表</td>
    </tr>
    <tr class="tdbg">
        <td align="center" class="tdbgleft" style="height: 24px;  font-weight: bold">ID</td>
        <td align="center" class="tdbgleft" style="height: 24px; font-weight: bold">课程名称</td>
        <td align="center" class="tdbgleft" style="height: 24px; font-weight: bold">课程缩写</td>
        <td align="center" class="tdbgleft" style="height: 24px; font-weight: bold">课程代码</td>
        <td align="center" class="tdbgleft" style="height: 24px; font-weight: bold">热门课程</td>
        <td align="center" class="tdbgleft" style="height: 24px; font-weight: bold">负责人</td>
        <td align="center" class="tdbgleft" style="height: 24px;  font-weight: bold">操作</td>
    </tr>
    <ZL:ExRepeater ID="RPT" runat="server" PagePre="<tr><td colspan='7' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>">
        <ItemTemplate>
            <tr>
                <td align="center" style="height: 24px;"><%#Eval("ID")%></td>
                <td align="center" style="height: 24px;"><%#Eval("CourseName")%></td>
                <td align="center" style="height: 24px;"><%#Eval("CoureseThrun")%></td>
                <td align="center" style="height: 24px;"><%#Eval("CoureseCode")%></td>
                <td align="center" style="height: 24px;"><%#GetHot(Eval("Hot","{0}"))%></td>
                <td align="center" style="height: 24px;"><%#GetAdminName(Eval("AddUser", "{0}"))%></td>                 
                <td align="center" style="height: 24px;">
                    <a href="SelectCourse.aspx?menu=select&id=<%#Eval("ID") %>&name=<%#Eval("CourseName") %>">选择</a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate></FooterTemplate>
    </ZL:ExRepeater>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Dialog.js"></script>
    <script>
        function onstr() {
            window.opener = null;
            parent.Dialog.close();
        }

        function setvalue(objname, valuetxt) {
            var mainright = window.top.main_right;
            mainright.document.getElementById(objname).value = valuetxt;
        }
    </script>
</asp:Content>