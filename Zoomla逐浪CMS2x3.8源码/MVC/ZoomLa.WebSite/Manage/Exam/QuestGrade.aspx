<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestGrade.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.QuestGrade" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title id="Title_Q" runat="server"></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
        <table class="table table-striped table-bordered table-hover" >
        <tr>
            <td>ID</td><td class="text_300"><asp:Label ID="Title_L" runat="server"></asp:Label></td><td>操作</td>
        </tr>
        <ZL:ExRepeater ID="RPT" runat="server" OnItemCommand="RPT_ItemCommand" PageSize="20" PagePre="<tr><td colspan='7'><div class='text-center'>" PageEnd="</div></td></tr>">
            <ItemTemplate>
                <tr>
                    <td><%#Eval("GradeID") %></td>
                    <td><%#Eval("GradeName") %></td>
                    <td>
                        <a href="javascript:;" onclick="ShowGrade(<%#Eval("GradeID") %>)" class="option_style"><i class="fa fa-pencil"></i>修改</a>
                        <a href="QuestionManage.aspx?<%#GetParamByCate()+"="+Eval("GradeID") %>" class="option_style"><i class="fa fa-newspaper-o"></i>试题管理</a>
                        <asp:LinkButton CommandName="Del" runat="server" OnClientClick="return confirm('是否确认删除!')" CssClass="option_style" CommandArgument='<%#Eval("GradeID") %>'><i class="fa fa-trash-o"></i>删除</asp:LinkButton>
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
        function ShowGrade(id) {
            $("#GradeID_Hid").val(id ? id : "");
            comdiag.width = "none";
            comdiag.content = "EditGrade_div";
            ShowComDiag("", "编辑<%=CateName %>");
            if (id) { $("#GradeName_T").val($("input[value='" + id + "']").data('name')) }
        }
    </script>
</asp:Content>
