<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionTypeManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.QuestionTypeManage" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>题型管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table  class="table table-striped table-bordered table-hover">
    <tr class="title text-center">
        <td style="width:5%">
            <asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" />
        </td>
        <td style="width:15%">名称</td>
        <td style="width:10%">类型</td>
        <td style="width:40%">说明</td>
        <td style="width:20%">创建时间</td>
        <td style="width:15%" class="title">操作</td>
    </tr>
    <ZL:ExRepeater ID="ET_RPT" PageSize="10" runat="server" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='5' class='text-center'>" PageEnd="</td></tr>">
        <ItemTemplate>
            <tr class="text-center">
                <td><input name="idchk" type="checkbox" value='<%#Eval("t_id") %>' /></td>
                <td><%#Eval("t_name")%></td>
                <td><%#GetType(Eval("t_type","{0}"))%></td>
                <td><%#Eval("t_remark")%></td>
                <td><%#Eval("t_createtime")%></td>
                <td>
                    <a href="AddQuestionType.aspx?id=<%#Eval("t_id")%>" class="option_style"><i class="fa fa-pencil" title="修改"></i>修改</a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate></FooterTemplate>
    </ZL:ExRepeater>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Common.js"></script>
</asp:Content>
