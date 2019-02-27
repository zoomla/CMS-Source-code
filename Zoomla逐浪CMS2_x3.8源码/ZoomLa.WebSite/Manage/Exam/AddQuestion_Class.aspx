<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddQuestion_Class.aspx.cs" Inherits="manage_Question_AddQuestion_Class" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>编辑分类</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="td_m text-right">
                <asp:Label ID="ssjd_txt" runat="server" Text="类别名称:"></asp:Label>
            </td>
            <td class="bqright">
                <asp:TextBox ID="txtClassName" runat="server" class="form-control text_md" OnTextChanged="txtClassName_TextChanged"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="类别不能为空!"
                    ControlToValidate="txtClassName"></asp:RequiredFieldValidator>
                <asp:HiddenField ID="hfC_Id" runat="server" Value="" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft"  align="right">
                <asp:Label ID="zt_txt" runat="server" Text="所属类别:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlC_ClassId" CssClass="form-control text_300" runat="server">
                </asp:DropDownList>
                <asp:TextBox ID="txtClassId" runat="server" CssClass="form-control text_md"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <asp:Label ID="Label3" runat="server" Text="类别类型:"></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="C_ClassType" runat="server"
                    RepeatDirection="Horizontal" CssClass="text_md">
                    <asp:ListItem Selected="True" Value="1">答题类型</asp:ListItem>
                    <asp:ListItem Value="2">视频操作类型</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="tdbgleft" align="right">
                <asp:Label ID="hits_txt" runat="server" Text="排序:"></asp:Label>
            </td>
            <td class="bqright">
                <asp:TextBox ID="txtC_OrderBy"  class="form-control text_md" runat="server">0</asp:TextBox>
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="保存分类" OnClick="EBtnSubmit_Click"
                    runat="server" />
                &nbsp;
                <asp:Button ID="BtnBack" class="btn btn-primary" runat="server" Text="返　回" OnClick="BtnBack_Click"
                    UseSubmitBehavior="False" CausesValidation="False" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>