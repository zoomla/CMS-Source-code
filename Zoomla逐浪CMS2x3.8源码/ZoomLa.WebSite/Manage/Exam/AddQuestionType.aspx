<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddQuestionType.aspx.cs" Inherits="manage_Question_AddQuestionType" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>题型管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Literal ID="liQuestionType" runat="server" Visible="false"></asp:Literal>
        <table class="table table-striped table-bordered table-hover">
        <tr><td colspan="2" style="text-align: center;"><asp:Label ID="Label1" runat="server"></asp:Label></td></tr>
        <tbody id="Tabs0">
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="ssjd_txt" runat="server" Text="名称："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_name" runat="server" class="form-control text_md"   ></asp:TextBox>
                    &nbsp;<font color="red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="名称不能为空!" ControlToValidate="txt_name"></asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label4" runat="server" Text="题型："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                 <asp:DropDownList ID="ddType" runat=server  CssClass="form-control text_md">
             <%--     //单选,多选,判断，填空,问答,组合--%>
                    <asp:ListItem Value="1">单选题</asp:ListItem>
                    <asp:ListItem Value="2">多选题</asp:ListItem>
                    <asp:ListItem Value="3">判断题</asp:ListItem>
                    <asp:ListItem Value="4">填空题</asp:ListItem>
                    <asp:ListItem Value="5">问答题</asp:ListItem>
                    <asp:ListItem Value="6">组合题</asp:ListItem>
                 </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label2" runat="server" Text="说明："></asp:Label>&nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txtRemark" runat="server" class="form-control text_md" TextMode="MultiLine" Height="123px" ></asp:TextBox>
                </td>
            </tr>
        </tbody>
        <tr class="tdbgbottom">
            <td colspan="2">
            <asp:HiddenField ID="hftid" runat="server" />
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="保存" runat="server" onclick="EBtnSubmit_Click"/>&nbsp;
                <asp:Button ID="BtnBack" class="btn btn-primary" runat="server" Text="返回列表" OnClientClick="location.href='QuestionTypeManage.aspx';return false;" UseSubmitBehavior="False"  CausesValidation="False" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>