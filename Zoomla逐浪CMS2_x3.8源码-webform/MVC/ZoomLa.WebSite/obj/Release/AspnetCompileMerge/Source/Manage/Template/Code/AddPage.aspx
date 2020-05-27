<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPage.aspx.cs" Inherits="ZoomLaCMS.Manage.Template.Code.AddPage"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>添加页面</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered table-striped table-hover">
        <tr><td class="td_m">链  接:</td><td>
            <asp:TextBox runat="server" ID="Url_T" CssClass="form-control text_300"/>
            <asp:Button runat="server" ID="LoadPage_Btn" CssClass="btn btn-primary" Text="加载页面" OnClick="LoadPage_Btn_Click" OnClientClick="return LoadCheck();" />
                                       </td></tr>
        <tr><td>别  名:</td><td>
            <asp:TextBox runat="server" ID="PageAlias_T" CssClass="form-control text_300" />
            <asp:RequiredFieldValidator runat="server" ID="RV1" ControlToValidate="PageAlias_T" ForeColor="Red" ErrorMessage="页面名不能为空" />
        </td></tr>
        <tr><td>页面名:</td><td>
            <asp:TextBox runat="server" ID="PageName_T" CssClass="form-control text_300" />
            <asp:RequiredFieldValidator runat="server" ID="RV2" ControlToValidate="PageName_T" ForeColor="Red" ErrorMessage="页面名不能为空" />
            <span class="rd_green">英文或数字,不可包含特殊符号</span></td></tr>
        <tr><td>Dll模块:</td><td>
            <label><input type="checkbox" name="models_chk" value="bll" checked="checked" />BLL(逻辑)</label>
            <label><input type="checkbox" name="models_chk" value="model" checked="checked"/>Model(模型)</label>
            <label><input type="checkbox" name="models_chk" value="common" />Common(公共)</label>
            <label><input type="checkbox" name="models_chk" value="component" />Component(组件)</label>
            <label><input type="checkbox" name="models_chk" value="sqldal" />SQLDAL(数据库)</label>
            <label><input type="checkbox" name="models_chk" value="safe" />safe(安全)</label> </td></tr>
        <tr><td>类型:</td><td>
            <label><input type="radio" name="pagetype_rad" value="aspx" checked="checked"/>aspx</label>
         <%--   <label><input type="radio" name="pagetype_rad" value="ashx"/>ashx</label>
            <label><input type="radio" name="pagetype_rad" value="ascx"/>ascx</label>--%>
                        </td></tr>
        <tr><td>功能描述:</td><td>
            <asp:TextBox runat="server" ID="Remind_T" TextMode="MultiLine" CssClass="form-control text_300" style="height:120px;" />
        </td></tr>
        <tr><td></td><td>
            <asp:Button runat="server" CssClass="btn btn-primary" ID="Save_Btn" Text="保存" OnClick="Save_Btn_Click" />
            <a class="btn btn-primary" href="PageList.aspx">返回</a>
        </td></tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/SelectCheckBox.js"></script>
    <script src="/JS/ZL_Regex.js"></script>
    <script src="/JS/chinese.js"></script>
    <script>
        $(function () {
            $("#PageAlias_T").keyup(function () { Getpy('PageAlias_T', 'PageName_T'); });
        })
        function LoadCheck() {
            var url = $("#Url_T").val();
            if (ZL_Regex.isEmpty(url)) { alert("页面不能为空"); return false; }
        }
    </script>
</asp:Content>