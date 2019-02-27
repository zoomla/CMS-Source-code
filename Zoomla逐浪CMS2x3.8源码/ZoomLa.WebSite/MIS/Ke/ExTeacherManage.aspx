<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExTeacherManage.aspx.cs" Inherits="manage_Question_ExTeacherManage" ValidateRequest="false" EnableViewStateMac="false" MasterPageFile="~/User/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>教师管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="edu" data-ban="ke"></div>
    <div class="container margin_t5">
        <ol class="breadcrumb">
        <li><a href="/User/">用户中心</a></li>
        <li class="active">教师管理 [<a href="AddExTeacher.aspx">添加教师</a>]</li>
        </ol>
        </div>
    <div class="container">
    <table class="table table-striped table-bordered table-hover" id="EGV">
    <tr class="title">
        <td>
            
        </td>
        <td class="title">教师分类</td>
        <td class="title">教师名称</td>
        <td class="title">职位</td>
        <td class="title">授课</td>
        <td class="title">创建时间</td>
        <td class="title">老师信息</td>
        <td class="title">操作</td>
    </tr>
    <ZL:ExRepeater ID="RPT" PageSize="20" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='7'><div class='text-center'>" PageEnd="</div></td></tr>" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <input name="idchk" type="checkbox" value='<%#Eval("ID") %>' />
                </td>
                <td>
                    <%#Eval("TypeName")%>
                </td>
                <td>
                    <%#Eval("TName")%>
                </td>
                <td>
                    <%#Eval("Post")%>
                </td>
                <td>
                    <%#Eval("Teach")%>
                </td>
                <td>
                  <%#Eval("CreatTime")%>
                </td>
                <td>
                    <div><%#GetRemark(Eval("Remark").ToString()) %></div>
                </td>
                <td>
                    <a href="AddExTeacher.aspx?id=<%#Eval("ID")%>" class="option_style" title="修改"><i class="fa fa-pencil"></i></a> <a href="?menu=delete&id=<%#Eval("ID")%>" onclick="return confirm('确实要删除此教师吗？');" class="option_style" title="删除"><i class="fa fa-trash"></i></a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate></FooterTemplate>
    </ZL:ExRepeater>
</table>
</div>
<div>
    </div>
<div class="container btn_green"><asp:Button ID="Button3" class="btn btn-primary" runat="server" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" Text="批量删除" onclick="Button3_Click" /></div>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Common.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#chkAll").click(function () {
                var allcheck = $(this)[0];
                $("input[name='idchk']").each(function () {
                    $(this)[0].checked = allcheck.checked;
                });
            });
        });
    </script>
</asp:Content>