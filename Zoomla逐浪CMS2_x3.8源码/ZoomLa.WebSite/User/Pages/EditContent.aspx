<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="EditContent.aspx.cs" Inherits="ZoomLa.WebSite.User.Content.EditContent" EnableViewStateMac="false" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
    <title>修改黄页内容</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
        <li>
            <asp:Label ID="lblNodeName" runat="server" Text="Label"></asp:Label>
        </li>
        <li>修改内容：<asp:Label ID="lblAddContent" runat="server" Text="Label"></asp:Label>
        </li>
    </ol>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2" class="text-center">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>所属节点：</td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Label" />
            </td>
        </tr>
        <tr>
            <td>内容标题：</td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" Text='' CssClass="form-control text_md" />
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtTitle">内容标题必填</asp:RequiredFieldValidator>
            </td>
        </tr>
        <asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
        <tr>
            <td colspan="2" class="text-center">
                <asp:HiddenField ID="HdnItem" runat="server" />
                <asp:TextBox ID="FilePicPath" runat="server" CssClass="form-control text_md" Text="fbangd" Style="display: none"></asp:TextBox>
                <asp:Button ID="EBtnSubmit" CssClass="btn btn-primary" Text="更新项目" OnClick="EBtnSubmit_Click" runat="server" />
                <asp:Button ID="BtnBack" runat="server" CssClass="btn btn-primary" Text="返回" OnClick="BtnBack_Click" UseSubmitBehavior="False" CausesValidation="False" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script>
    var zlconfig = {
        updir: "<%=ZoomLa.Components.SiteConfig.SiteOption.UploadDir.ToLower()%>",
        duptitlenum: "<%=ZoomLa.Components.SiteConfig.SiteOption.DupTitleNum%>",
        modelid: "<%=ModelID%>"
    };
</script>
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="/JS/Common.js"></script>
<script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
<script type="text/javascript" src="/JS/ICMS/tags.json"></script>
<script type="text/javascript" src="/JS/ZL_Content.js"></script>
</asp:Content>