<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceCode.aspx.cs" Inherits="ZoomLaCMS.Manage.User.Service.ServiceCode" ValidateRequest="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>生成客服代码</title>
    <script src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="text-right td_m">企业名称:</td>
            <td>
                <asp:TextBox runat="server" ID="FName_T" CssClass="form-control text_300"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="R1" ControlToValidate="FName_T" ForeColor="Red" Display="Dynamic" ErrorMessage="名称不能为空"/>
            </td>
        </tr>
        <tr>
            <td class="text-right">欢迎语:</td>
            <td>
                <asp:TextBox runat="server" ID="Content_T" TextMode="MultiLine" style="height:150px;width:100%;"></asp:TextBox>
                <%=Call.GetUEditor("Content_T",2) %>
<%--                <asp:RequiredFieldValidator runat="server" ID="R2" ControlToValidate="Content_T" ForeColor="Red" Display="Dynamic" ErrorMessage="内容不能为空"/>--%>
            </td>
        </tr>
        <tr><td class="text-right">主题：</td><td><label><input type="radio" name="theme_rad" value="def" checked="checked" />默认主题</label></td></tr>
        <tr>
            <td class="text-right">操作:</td>
            <td>
                <asp:Button runat="server" ID="Save_Btn" OnClick="Save_Btn_Click" Text="保存" CssClass="btn btn-primary" />
                <a href="CodeList.aspx" class="btn btn-primary">返回</a>
   <%--             <input type="button" class="btn btn-primary" id="getCode" value="生成代码" onclick="return getCode_onclick()" />--%>
            </td>
        </tr>
        <tr>
            <td class="text-right">引用代码:</td>
            <td><asp:TextBox ID="txtCode" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
    </table>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>
