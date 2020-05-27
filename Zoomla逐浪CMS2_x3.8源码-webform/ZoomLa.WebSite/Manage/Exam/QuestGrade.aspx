<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuestGrade.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="User_Exam_QuestGrade" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title id="Title_Q" runat="server"></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
        <table class="table table-striped table-bordered table-hover" >
        <tr>
            <td class="td_s"></td><td class="td_s">ID</td><td class="text_300"><asp:Label ID="Title_L" runat="server"></asp:Label></td><td>操作</td>
        </tr>
        <ZL:ExRepeater ID="RPT" runat="server" OnItemCommand="RPT_ItemCommand" PageSize="20" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='6'><div class='text-center'>" PageEnd="</div></td></tr>">
            <ItemTemplate>
                <tr>
                    <td><input type="checkbox" value="<%#Eval("GradeID") %>" data-name="<%#Eval("GradeName") %>" name="idchk" /></td>
                    <td><%#Eval("GradeID") %></td>
                    <td><%#Eval("GradeName") %></td>
                    <td>
                        <a href="javascript:;" onclick="ShowGrade(<%#Eval("GradeID") %>)">修改</a>
                        <a href="QuestionManage.aspx?<%#GetParamByCate()+"="+Eval("GradeID") %>">试题管理</a>
                        <asp:LinkButton CommandName="Del" runat="server" OnClientClick="return confirm('是否确认删除!')" CommandArgument='<%#Eval("GradeID") %>'>删除</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>
    </table>
    <div id="EditGrade_div" style="display:none;">
        <div class="text-center">
            <asp:TextBox ID="GradeName_T" runat="server" CssClass="form-control text_300"></asp:TextBox>
            <asp:Button ID="Save_Btn" runat="server" CssClass="btn btn-primary" Text="确定" OnClick="Save_Btn_Click" />
            <asp:HiddenField ID="GradeID_Hid" runat="server" />
        </div>
    </div>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        $(function () {
            $("#chkAll").click(function () {
                var curobj = $(this)[0];
                $("input[name='idchk']").each(function () {
                    $(this)[0].checked = curobj.checked;
                });
            });
        })
        function ShowGrade(id) {
            $("#GradeID_Hid").val(id ? id : "");
            comdiag.width = "none";
            comdiag.content = "EditGrade_div";
            ShowComDiag("", "编辑<%=CateName %>");
            if (id) { $("#GradeName_T").val($("input[value='" + id + "']").data('name')) }
        }
    </script>
</asp:Content>


