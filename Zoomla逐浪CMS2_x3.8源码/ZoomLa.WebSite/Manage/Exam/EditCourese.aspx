<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditCourese.aspx.cs" Inherits="manage_Exam_EditCourese" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>学员管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2" style="text-align: center;">
                <asp:Label ID="Label1" runat="server"></asp:Label></td>
        </tr>
        <tbody id="Tabs0">
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="ssjd_txt" runat="server" Text="学员姓名："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Stuname" runat="server" class="l_input" Width="200px">
                    </asp:TextBox>
                    &nbsp;<font color="red">*</font>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label9" runat="server" Text="登录密码："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Stupassword" runat="server" TextMode="Password" class="l_input" Width="200px">
                    </asp:TextBox>&nbsp;为空则不修改密码
                </td>
            </tr>

            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Stucardnotxt" runat="server" Text="班级："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:DropDownList ID="ClassID" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label11" runat="server" Text="状态："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:DropDownList ID="txt_State" runat="server">
                        <asp:ListItem>审核</asp:ListItem>
                        <asp:ListItem>未审核</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label12" runat="server" Text="是否付款："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:RadioButtonList ID="PayMent" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">未付款</asp:ListItem>
                        <asp:ListItem Value="1">已付款</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label16" runat="server" Text="申请时间："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Addtime" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });" class="l_input" Width="200px">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label10" runat="server" Text="备注："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Remark" runat="server" class="l_input" Width="388px"
                        Height="155px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </tbody>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="添加学员" runat="server" OnClick="EBtnSubmit_Click" />
                &nbsp;
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