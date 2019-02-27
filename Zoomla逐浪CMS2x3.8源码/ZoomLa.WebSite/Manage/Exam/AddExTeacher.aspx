<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddExTeacher.aspx.cs" Inherits="manage_Question_AddExTeacher" ValidateRequest="false" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
    <title>添加教师</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Literal ID="liCoures" runat="server" Visible="false"></asp:Literal>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2">
                <asp:Label ID="Label1" runat="server" Text="添加教师" /></td>
        </tr>
        <tbody id="Tabs0">
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">教师名称： &nbsp;</td>
                <td class="bqright">
                    <asp:TextBox ID="txt_name" runat="server" class="form-control text_md" ></asp:TextBox>
                    &nbsp;<font color="red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ErrorMessage="教师名称不能为空!" ControlToValidate="txt_name"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="display:none;">
                <td class="tdbgleft" style="width: 20%" align="right">教师分类：</td>
                <td class="bqright">
                    <asp:HiddenField ID="hfid" runat="server" />
                    <asp:TextBox ID="txtClassname" runat='server' class="form-control text_md"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label3" runat="server" Text="教师职位："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Post" runat="server" class="form-control text_md" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ErrorMessage="教师职位不能为空!" ControlToValidate="txt_Post"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label4" runat="server" Text="教师授课："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Teach" runat="server" class="form-control text_md" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ErrorMessage="教师授课不能为空!" ControlToValidate="txt_Teach"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="Label2" runat="server" Text="教师信息："></asp:Label></td>
                <td>
                    <asp:TextBox runat="server" ID="textarea1" TextMode="MultiLine" style="width:700px;height:200px;"></asp:TextBox>
                </td>
            </tr>
        </tbody>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:HiddenField ID="hftid" runat="server" />
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="保存" runat="server" OnClick="EBtnSubmit_Click" />
                &nbsp;
                <asp:Button ID="BtnBack" class="btn btn-primary" runat="server" Text="返回列表" OnClientClick="location.href='ExTeacherManage.aspx';return false;" UseSubmitBehavior="False" CausesValidation="False" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Dialog.js"></script>
    <%=Call.GetUEditor("textarea1",2) %>
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