<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddQuestionForType.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.AddQuestionForType" ValidateRequest="false" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>添加试题</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <asp:Label ID="Label2" runat="server" Text="添加试题" Visible="false"></asp:Label>
    <table class="table table-striped table-bordered table-hover">
        <tr align="center">
            <td class="tdbg">选择分类：
                                        <asp:DropDownList ID="classtype" runat="server" CssClass="x_input">
                                        </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="classtype"
                    Display="Dynamic" ErrorMessage="试题类型不能为空！"></asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Button ID="Button1" runat="server" Text="下一步" CssClass="btn btn-primary" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>