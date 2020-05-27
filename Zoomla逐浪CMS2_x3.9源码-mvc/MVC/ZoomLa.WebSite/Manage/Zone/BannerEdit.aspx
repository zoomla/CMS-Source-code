<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BannerEdit.aspx.cs" Inherits="ZoomLaCMS.Manage.Zone.BannerEdit" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>会员组模型编辑</title>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script src="/JS/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="clearbox"></div>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <asp:Literal ID="LTitle" runat="server" Text="修改栏目"></asp:Literal></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 35%">
                <strong>栏目名称：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtModelName" CssClass="form-control" runat="server" Width="156" MaxLength="200" /><font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="TxtModelName">模板名称不能为空</asp:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 35%">
                <strong>栏目模板：</strong>
            </td>
            <td>
                <asp:TextBox ID="ModeTemplate" CssClass="form-control text_300" runat="server" />
                <input type="button" value="选择模板" onclick="WinOpenDialog('../Template/TemplateList.aspx?OpenerText=' + escape('ModeTemplate') + '&FilesDir=', 650, 480)" class="btn" /></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 35%">
                <strong>栏目类型：</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">分栏目</asp:ListItem>
                    <asp:ListItem Value="1">首页栏目</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 35%">
                <strong>栏目状态：</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">不显示</asp:ListItem>
                    <asp:ListItem Value="1">显示</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnSubmit" Text="保存" CssClass="btn btn-primary" runat="server" OnClick="EBtnSubmit_Click" />
                &nbsp;&nbsp;
                <input name="Cancel" type="button" class="btn btn-primary" id="Cancel" value="取消" onclick="javescript: history.go(-1)" />
            </td>
        </tr>
    </table>
</asp:Content>

