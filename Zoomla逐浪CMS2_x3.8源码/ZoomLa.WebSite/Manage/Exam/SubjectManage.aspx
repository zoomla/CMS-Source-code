<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubjectManage.aspx.cs" Inherits="Manage_Exam_SubjectManage" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>学科管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td></td>
            <td>ID</td>
            <td>学科名称</td>
            <td>学科组别</td>
            <td>同时段最大安排数</td>
            <td>备注</td>
            <td>操作</td>
        </tr>
        <ZL:ExRepeater ID="Sub_RPT" runat="server" PageSize="10" OnItemCommand="Sub_RPT_ItemCommand" PagePre="<tr id='page_tr'><td><input type='checkbox' id='chkAll'/></td><td colspan='10' id='page_td'>" PageEnd="</td></tr>">
        <ItemTemplate>
            <tr>
                <td><input type="checkbox" name="idchk" value="<%#Eval("ID") %>" /></td>
                <td><%#Eval("ID") %></td>
                <td><%#Eval("Name") %></td>
                <td><%#Eval("Subject") %></td>
                <td><%#Eval("MaxCount") %></td>
                <td><%#Eval("Flag") %></td>
                <td>
                    <asp:LinkButton ID="Edit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("ID") %>' CssClass="option_style"><i class="fa fa-pencil" title="修改"></i></asp:LinkButton>
                    <asp:LinkButton ID="Del" runat="server" OnClientClick="return confirm('确认删除该学科?')" CommandName="Del" CommandArgument='<%#Eval("ID") %>' CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
           <FooterTemplate></FooterTemplate>
    </ZL:ExRepeater>
    </table>
    <div id="addsub_div" style="display:none;">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td style="width:15%;" class="text-right">学科名称：</td>
                <td><asp:TextBox ID="SubName_T" CssClass="form-control text_md" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">学科组别：</td>
                <td>
                    <asp:TextBox ID="GroName_T" CssClass="form-control text_md" runat="server"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="text-right">最大安排数：</td>
                <td>
                  <asp:TextBox runat="server" ID="MaxCount_T" CssClass="form-control text_md" Text="1" />
                </td>
            </tr>
            <tr>
                <td class="text-right">备注：</td>
                <td>
                    <asp:TextBox TextMode="MultiLine" CssClass="form-control tarea_l" runat="server" ID="Flag_T" Rows="3"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="text-center">
                    <asp:HiddenField ID="SubID_Hid" runat="server" />
                    <asp:Button ID="Save_B" runat="server" Text="添加" OnClick="Save_B_Click" CssClass="btn btn-primary" />
                    <button class="btn btn-primary" type="button" onclick="addDiag.CloseModal()">取消</button>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        var addDiag = new ZL_Dialog();
        $().ready(function () {
            if ($("#SubID_Hid").val()) {
                ShowEidtDiag("修改学科");
            }
        });
        function ShowEidtDiag(title) {
            ShowDiag(title);
            $("#Save_B").val("修改");
        }
        function ShowAddDiag(title) {
            ShowDiag(title);
            $("#SubName_T").focus();
            $("#SubName_T").val("");
            $("#GroName_T").val("");
            $("#Flag_T").val("");
            $("#Save_B").val("添加");
        }
        function ShowDiag(title) {
            addDiag.title = title;
            addDiag.maxbtn = false;
            addDiag.content = "addsub_div";
            addDiag.ShowModal();
        }
    </script>
</asp:Content>
