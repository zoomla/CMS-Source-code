<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEnroll.aspx.cs" Inherits="manage_Exam_AddEnroll" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>招生信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr><td colspan="2"><asp:Label ID="Label1" runat="server" Text="添加招生信息"></asp:Label></td></tr>
        <tbody id="Tabs0">
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">招生人员： &nbsp;</td>
                <td class="bqright">
                    <asp:DropDownList ID="EnrollUser" runat="server"  CssClass="form-control text_md">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="招生人员不能为空!" ControlToValidate="EnrollUser"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">备注说明：</td>
                <td class="bqright">
                    <asp:TextBox ID="txt_infos" runat="server" TextMode="MultiLine" Height="81px"  CssClass="form-control text_md"></asp:TextBox>
                </td>
            </tr>
            
        </tbody>
        <tr class="tdbgbottom">
            <td colspan="2">
            <asp:HiddenField ID="hftid" runat="server" />
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="添加信息" runat="server" onclick="EBtnSubmit_Click"/> &nbsp;
                <asp:Button ID="BtnBack" class="btn btn-primary" runat="server" Text="返回列表" OnClientClick="location.href='ApplicationManage.aspx';return false;" UseSubmitBehavior="False"  CausesValidation="False" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Dialog.js"></script>
    <script type="text/javascript">
        function Openwin() {
            var diag = new Dialog();
            diag.Modal = false;
            diag.Width = 400;
            diag.Height = 450;
            diag.Title = "选择分类";
            diag.URL = "SelecQuestionClass.aspx";
            diag.show();
        }
    </script>
</asp:Content>