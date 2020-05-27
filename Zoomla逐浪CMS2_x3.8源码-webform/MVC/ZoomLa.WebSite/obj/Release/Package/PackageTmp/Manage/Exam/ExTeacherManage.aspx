<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExTeacherManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.ExTeacherManage" ValidateRequest="false" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>教师管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover" id="EGV">
    <tr class="title">
        <td style="width:5%"></td>
        <td style="width:10%" class="title">教师分类</td>
        <td style="width:10%" class="title">教师名称</td>
        <td style="width:10%" class="title">职位</td>
        <td style="width:10%" class="title">授课</td>
        <td style="width:15%" class="title">创建时间</td>
        <td style="width:30%" class="title">老师信息</td>
        <td style="width:15%" class="title">操作</td>
    </tr>
    <ZL:ExRepeater ID="Repeater1" runat="server" PageSize="10" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='12' class='text-center'>" PageEnd="</td></tr>">
        <ItemTemplate>
            <tr class="text-center">
                <td>
                    <input name="idchk" type="checkbox" value='<%#Eval("ID") %>' />
                </td>
                <td>
                    <%#GetTeachClass(Eval("TClsss", "{0}"))%>
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
                    <%#GetRemark(Eval("Remark","{0}"))%>
                </td>
                <td>
                    <a href="AddExTeacher.aspx?id=<%#Eval("ID")%>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a> 
                    <a href="?menu=delete&id=<%#Eval("ID")%>" onclick="return confirm('确实要删除此教师?');" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</a>
                </td>
            </tr>
        </ItemTemplate>
    </ZL:ExRepeater>
</table>
<div>
    <asp:Button ID="Button3" class="btn btn-primary" runat="server" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" Text="批量删除" onclick="Button3_Click" /></div>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Common.js"></script>
    <script type="text/javascript">
        function CheckAll(spanChk)//CheckBox全选
        {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
        }
        HideColumn("5,6");
    </script>
</asp:Content>