<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStudio.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.News.AddStudio" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>编辑试题</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:HiddenField ID="sid" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Label ID="Label2" runat="server" Text="添加学员" Visible="false"></asp:Label>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2" style="text-align: center;">
                <asp:Label ID="Label1" runat="server"></asp:Label></td>
        </tr>
        <tbody id="Tabs0">
            <tr>
                <td style="width: 20%" align="right">
                    <asp:Label ID="ssjd_txt" runat="server" Text="学员姓名："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_Stuname" runat="server" class="l_input" Width="200px"></asp:TextBox><font color="red">*</font>
                </td>
            </tr>
            <tr>
                <td style="width: 20%" align="right">
                    <asp:Label ID="Label9" runat="server" Text="登录密码："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_Stupassword" runat="server" class="l_input" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 20%" align="right">
                    <asp:Label ID="Stucardnotxt" runat="server" Text="考号："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_Stucardno" runat="server" class="l_input" Width="200px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="width: 20%" align="right">
                    <asp:Label ID="Label11" runat="server" Text="所在组别："></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="txt_Stugroup" runat="server">
                        <asp:ListItem>学生</asp:ListItem>
                        <asp:ListItem>教师</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 20%" align="right">
                    <asp:Label ID="Label12" runat="server" Text="监管人："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_Regulation" runat="server" class="l_input" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 20%" align="right">
                    <asp:Label ID="Label14" runat="server" Text="学习课程："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_Course" runat="server" class="l_input" Width="200px">
                    </asp:TextBox>
                    参加培训课程
                </td>
            </tr>
            <tr>
                <td style="width: 20%" align="right">
                    <asp:Label ID="Label16" runat="server" Text="启用时间："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_Addtime" runat="server" class="l_input" Width="200px" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 20%" align="right">
                    <asp:Label ID="Label10" runat="server" Text="到期有效时间："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_Exptime" runat="server" class="l_input" Width="200px" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });"></asp:TextBox>
                </td>
            </tr>
        </tbody>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="添加学员" runat="server" OnClick="EBtnSubmit_Click" />
                <asp:Button ID="BtnBack" class="btn btn-primary" runat="server" Text="返回列表" UseSubmitBehavior="False"
                    CausesValidation="False" OnClick="BtnBack_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Common.js"></script>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>