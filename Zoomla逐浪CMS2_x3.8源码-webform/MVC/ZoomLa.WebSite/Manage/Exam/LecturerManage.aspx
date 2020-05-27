<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LecturerManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.LecturerManage" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>讲师管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
    <tr class="title text-center">
        <td style="width:5%"></td>
        <td style="width:7%">类别</td>
        <td style="width:10%">名称</td>
        <td style="width:6%">性别</td>
        <td style="width:7%">职位</td>
        <td style="width:10%">电话</td>
        <td style="width:15%">创建时间</td>
        <td style="width:15%">特长</td>
        <td style="width:15%">介绍</td>
        <td style="width:15%">操作</td>
    </tr>
    <ZL:ExRepeater ID="ExLec_RPT" PageSize="10" runat="server" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='12' class='text-center'>" PageEnd="</td></tr>">
        <ItemTemplate>
            <tr class="text-center">
                <td><input name="idchk" type="checkbox" value='<%#Eval("ID") %>' /></td>
                <td><%#Eval("TechType")%></td>
                <td><%#Eval("TechName")%></td>
                <td><%#GetSex(Eval("TechSex","{0}"))%></td>
                <td><%#GetClass(Eval("TechDepart", "{0}"))%></td>
                <td><%#Eval("TechPhone")%></td>
                <td><%#Eval("CreateTime")%></td>
                <td><%#GetContent(Eval("TechSpecialty", "{0}"))%></td>
                <td><%#GetContent(Eval("TechIntrodu", "{0}"))%></td>
                <td>
                    <a href="AddLecturer.aspx?id=<%#Eval("ID")%>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a> 
                    <a href="?menu=delete&id=<%#Eval("ID")%>" OnClick="return confirm('确实要删除此教师?');" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</a>
                </td>
            </tr>
        </ItemTemplate>
    </ZL:ExRepeater>
</table>
<div>
    <asp:Button ID="Button3" class="btn btn-primary" Style="width: 110px;" runat="server" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');"
        Text="批量删除" onclick="Button3_Click" /></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
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
</script>
</asp:Content>
