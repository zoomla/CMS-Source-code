<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddQuestion_Knowledge.aspx.cs" ValidateRequest="false" Inherits="manage_Question_AddQuestion_Knowledge" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>添加知识点</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <asp:Label ID="Label2" runat="server" Visible="false"></asp:Label>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2" style="text-align: center;">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 20%" align="right">
                <asp:Label ID="ssjd_txt" runat="server" Text="知识点："></asp:Label>
                &nbsp;
            </td>
            <td class="bqright">
                <asp:TextBox ID="txtK_Name" runat="server" class="l_input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="知识点不能为空!"
                    ControlToValidate="txtK_Name"></asp:RequiredFieldValidator>
                <asp:HiddenField ID="hfC_Id" runat="server" Value="" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 20%" align="right">
                <asp:Label ID="zt_txt" runat="server" Text="所属类别："></asp:Label>
                &nbsp;
            </td>
            <td>
                <asp:DropDownList ID="ddlC_ClassId" runat="server">
                </asp:DropDownList>
                <asp:TextBox ID="txtClassId" runat="server" CssClass="l_input"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 20%" align="right">
                <asp:Label ID="hits_txt" runat="server" Text="排　　序："></asp:Label>
                &nbsp;
            </td>
            <td class="bqright">
                <asp:TextBox ID="txtK_OrderBy" class="l_input" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="保存知识点" OnClick="EBtnSubmit_Click"
                    runat="server" />
                &nbsp;
                <asp:Button ID="BtnBack" class="btn btn-primary" runat="server" Text="返　回" OnClick="BtnBack_Click"
                    UseSubmitBehavior="False" CausesValidation="False" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>