<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoreUpdate.aspx.cs" Inherits="ZoomLaCMS.Manage.UserShopMannger.StoreUpdate" ValidateRequest="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>商家店铺修改</title>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td class="text-right">店铺名称</td>
                <td>
                    <asp:TextBox ID="UserShopName_T" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
        </table>
    </div>
    <div style="margin-left: 400px">
        <asp:Button ID="Esubmit" CssClass="btn btn-primary" runat="server" Text="保存信息" OnClick="Esubmit_Click" />
        <asp:HiddenField ID="HiddenField1" runat="server" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
    var zlconfig = {
        updir: "<%=ZoomLa.Components.SiteConfig.SiteOption.UploadDir.ToLower()%>",
        duptitlenum: "<%=ZoomLa.Components.SiteConfig.SiteOption.DupTitleNum%>",
        modelid: "<%=0%>"
        };
</script>
<script type="text/javascript" src="/JS/OAKeyWord.js"></script>
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="/JS/chinese.js"></script>
<script type="text/javascript" src="/JS/Common.js"></script>
<script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
<script type="text/javascript" src="/JS/ICMS/tags.json"></script>
<script type="text/javascript" src="/JS/ZL_Content.js"></script>
</asp:Content>

