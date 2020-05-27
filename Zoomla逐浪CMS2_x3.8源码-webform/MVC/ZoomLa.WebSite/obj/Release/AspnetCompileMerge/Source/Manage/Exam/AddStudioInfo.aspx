<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStudioInfo.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.AddStudioInfo" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加学员信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Literal ID="liCoures" runat="server" Visible="false"></asp:Literal>
    <asp:HiddenField runat="server" ID="sid_Hid" />
    <table  class="table table-striped table-bordered table-hover">
         <tr><td colspan="2" style="text-align: center;"><asp:Label ID="Label1" runat="server"></asp:Label></td></tr>
        <tbody id="Tabs0">
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">学员姓名： &nbsp;</td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Studioname" runat="server" CssClass="l_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="招生人员不能为空!" ControlToValidate="txt_Studioname"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">预设用户名： &nbsp;</td>
                <td class="bqright">

                 <asp:TextBox ID="txt_PriorUserName" runat="server" CssClass="l_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="招生人员不能为空!" ControlToValidate="txt_PriorUserName"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">预设登录密码： &nbsp;</td>
                <td class="bqright">
                <asp:TextBox ID="txt_LogPassWord" runat="server" CssClass="l_input"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">联系电话： &nbsp;</td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Tel" runat="server" CssClass="l_input" Width="184px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ErrorMessage="联系电话不能为空!" ControlToValidate="txt_Tel"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">联系地址： &nbsp;</td>
                <td class="bqright">
                   <asp:TextBox ID="txt_Addinfo" runat="server" TextMode="MultiLine" 
                        CssClass="X_input" Height="57px" Width="267px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ErrorMessage="联系地址不能为空!" ControlToValidate="txt_Addinfo"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">身份证号码： &nbsp;</td>
                <td class="bqright">
                   <asp:TextBox ID="txt_CradNo" runat="server" CssClass="l_input" Width="264px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ErrorMessage="身份证号码不能为空!" ControlToValidate="txt_CradNo"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">录入时间： &nbsp;</td>
                <td class="bqright">
                   <asp:TextBox ID="txt_WriteTime" runat="server" CssClass="l_input"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">备注说明：</td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Remark" runat="server" TextMode="MultiLine" Height="81px" 
                        Width="296px"></asp:TextBox>
                </td>
            </tr>
        </tbody>
        <tr class="tdbgbottom">
            <td colspan="2">
            <asp:HiddenField ID="hftid" runat="server" />
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="添加信息" runat="server" onclick="EBtnSubmit_Click"/> &nbsp;
                <asp:Button ID="BtnBack" class="btn btn-primary" runat="server" Text="返回列表" OnClientClick="location.href='StudioInfoListByTech.aspx?id=16';return false;" UseSubmitBehavior="False"  CausesValidation="False" />
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