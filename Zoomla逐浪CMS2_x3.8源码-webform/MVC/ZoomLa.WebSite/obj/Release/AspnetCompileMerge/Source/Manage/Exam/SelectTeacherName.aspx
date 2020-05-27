<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectTeacherName.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.SelectTeacherName" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>选择课程</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
    <tr class="title text-center">    
        <td style="width:10%" class="title">教师名称</td>
        <td style="width:10%" class="title">职位</td>
        <td style="width:10%" class="title">授课</td>
        <td style="width:15%" class="title">创建时间</td>
        <td style="width:40%" class="title">老师信息</td>
        <td style="width:15%" class="title">操作</td>
    </tr>
    <ZL:ExRepeater ID="Teacher_RPT" PageSize="10" runat="server">
        <ItemTemplate>
            <tr class="text-center">
                <td><%#Eval("TName")%></td>
                <td><%#Eval("Post")%></td>
                <td><%#Eval("Teach")%></td>
                <td><%#Eval("CreatTime")%></td>
                <td><%#GetRemark(Eval("Remark","{0}"))%></td>
                <td>
                     <a href="SelectTeacherName.aspx?menu=select&id=<%#Eval("ID") %>&name=<%#Eval("TName") %>">选择</a>                   
                </td>
            </tr>
        </ItemTemplate>
    </ZL:ExRepeater>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Dialog.js"></script>
    <script type="text/javascript">
        function onstr() {
            window.opener = null;
            parent.Dialog.close();
        }

        function setvalue(objname, valuetxt) {
            var mainright = window.top.main_right;
            parent.document.getElementById(objname).value = valuetxt;
        }
    </script>
</asp:Content>