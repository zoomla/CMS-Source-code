<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDevelop.aspx.cs" Inherits="ZoomLaCMS.Manage.Config.AddDevelop" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加计划</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">添加计划</td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 200px">
                <strong>计划名称：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtPlanName" MaxLength="200" Style="width: 156px;" runat="server"></asp:TextBox><font color="red">*</font>
                <span id="RequiredFieldValidator1" style="color: Red; visibility: hidden;">计划名称不能为空</span>
            </td>
        </tr>

        <tr>
            <td class="tdbgleft">
                <strong>设置执行规则：</strong>
            </td>
            <td>
                <asp:DropDownList ID="TxtPlanRule" runat="server" AutoPostBack="true">
                </asp:DropDownList><font color="red">*</font>
            </td>
        </tr>
        <tr style="display: none">
            <td class="tdbgleft"><strong>选择操作的数据库：</strong></td>
            <td>
                <asp:DropDownList ID="DR_Data" runat="server" AutoPostBack="true">
                </asp:DropDownList><font color="red">*</font></td>
        </tr>
        <tr>
            <td class="tdbgleft"><strong>启用时间：</strong></td>
            <td>
                <%-- 使用 UpdatePanel 局部刷新， 实现点击 Textbox,弹出 Calendar 控件来选取时间 、、 text 客户端控件， 直接用js 定义 --%>

                <asp:TextBox type="text" ID="TxtExecutionTime" runat="server" onfocus="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss', minDate: new Date() })" autocomplete="off" title="双击日期控件获取"></asp:TextBox>
                <font color="red">*格式：yyyy/mm/dd hh:mm:ss</font>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft">
                <strong>功能描述：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtDescription" runat="server" TextMode="MultiLine" Rows="2" Columns="20" class="l_input" Style="height: 77px; width: 365px;"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:HiddenField ID="HdnDevId" runat="server" />
                <asp:Button ID="EBtnSubmit" Text="保 存" class="btn btn-primary" OnClick="EBtnSubmit_Click" runat="server" />
                &nbsp;&nbsp;
		<input name="Cancel" type="button" id="Cancel" class="btn btn-primary" value="取 消" onclick="window.location.href = 'DevelopmentCenter.aspx';" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/chinese.js"></script>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>