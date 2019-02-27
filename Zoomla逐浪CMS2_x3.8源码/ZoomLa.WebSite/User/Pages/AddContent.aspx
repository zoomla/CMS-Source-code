<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="AddContent.aspx.cs" Inherits="ZoomLa.WebSite.User.Content.AddContent" EnableViewStateMac="false" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加黄页内容</title>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
        <li>
            <asp:Label ID="lblNodeName" runat="server"></asp:Label>
        </li>
        <li>添加内容：<asp:Label ID="lblAddContent" runat="server"></asp:Label>
        </li>
    </ol>
    <div>
        <table class="table table-bordered table-hover table-striped">
            <tr>
                <td colspan="2" class=" text-center">
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>所属节点：</td>
                <td>
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>内容标题：</td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control m715-50" MaxLength="30"></asp:TextBox>
                    <span><font color="red">*</font></span>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtTitle">内容标题必填</asp:RequiredFieldValidator>
                </td>
            </tr>
            <asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
            <tr class="btn_green">
                <td colspan="2" class="text-center">
                    <asp:HiddenField ID="HdnNode" runat="server" />
                    <asp:HiddenField ID="HdnModel" runat="server" />
                    <asp:TextBox ID="FilePicPath" runat="server" Text="fbangd" Style="display: none"></asp:TextBox>
                    <asp:Button ID="EBtnSubmit" Text="添加项目" CssClass="btn btn-primary" OnClick="EBtnSubmit_Click" runat="server" />
                    <a href="MyContent.aspx?ModelID=<%=this.NodeID %>" class="btn btn-primary">返回列表</a>
                </td>
            </tr>
        </table>
    </div>
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