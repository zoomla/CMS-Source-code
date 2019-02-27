<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Manage/I/Default.master" CodeFile="AddKnowledge.aspx.cs" Inherits="Manage_Exam_AddKnowledge" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>编辑知识点</title>
<link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">

        <tr>
            <td class="text-right">所属科目:</td>
            <td>
                <asp:Label ID="QuessClass_L" runat="server"></asp:Label>
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td class="text-right">所属知识点:</td>
            <td><asp:Label ID="PKnow_L" Text="无" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="td_m text-right">名称:</td>
            <td>
                <asp:TextBox ID="Name_T" runat="server" CssClass="form-control text_300"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td class="text-right">所属年级:</td>
            <td>
                <asp:DropDownList ID="GradeList_Drop" DataTextField="GradeName" DataValueField="GradeID" runat="server" CssClass="form-control text_300"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="text-right">是否启用</td>
            <td>
                <input type="checkbox" checked="checked" class="switchChk" id="Status_Check" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="text-right">系统知识点:</td>
            <td>
                <input type="checkbox" class="switchChk" id="IsSyst_Check" runat="server" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="Save_Btn" runat="server" CssClass="btn btn-primary" OnClientClick="return CheckData()" Text="保存" OnClick="Save_Btn_Click" />
                <a href="KnowledgeManage.aspx?nid=<%=NodeID %>" class="btn btn-primary">返回</a>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="OldName_Hid" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/dist/js/bootstrap-switch.js"></script>
    <script>
        $(function () {
            InitQuestEvent();
            if ($("#NodeID_Hid").val() || $("#NodeID_Hid").val() > 0)
            { SetQuestType($("#NodeID_Hid").val()); }
        });
        function InitQuestEvent() {
            $(".Quesst_Dr button").click(function () {
                $(this).next().toggle();
            });
        }
        //选择试题类别
        function SelQuestType(obj, id) {
            $(".Quesst_Dr button .curquest").html($(obj).text());
            $("#NodeID_Hid").val(id);
            $(obj).closest('ul').hide();
        }
        function SetQuestType(id) {
            $(".Quesst_Dr button .curquest").html($(".Quesst_Dr ul [data-id='" + id + "']").text());
        }
        function CheckData() {
            if ($("#Name_T").val().trim() == "") { alert('知识点名称不能为空!'); return false; }
            return true;
        }
    </script>
</asp:Content>

