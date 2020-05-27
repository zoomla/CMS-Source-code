<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelContent.aspx.cs" Inherits="ZoomLaCMS.Common.Dialog.SelContent"  ValidateRequest="false" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>选择内容</title>
<style>
    .selected { background-color: #ccc !important; }
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="text_300">
    <div class="input-group">
        <asp:TextBox ID="Search_T" runat="server" CssClass="form-control" placeholder="请输入关键字"></asp:TextBox>
        <span class="input-group-btn">
            <asp:LinkButton ID="Search_L" runat="server" CssClass="btn btn-default" OnClick="Search_L_Click"><i class="fa fa-search"></i></asp:LinkButton>
        </span>
    </div>
</div>
<table id="EGV" class="table table-striped table-bordered table-hover content_list">
    <tr>
        <td></td>
        <td>ID</td>
        <td>标题</td>
        <td>录入者</td>
        <td>节点名称</td>
        <td>点击数</td>
    </tr>
    <ZL:Repeater ID="RPT" runat="server" PageSize="10" PagePre="<tr><td colspan='6'><div class='text-center'>"
        PageEnd="</div></td></tr>">
        <ItemTemplate>
            <tr ondblclick="location='ShowContent.aspx?GID=<%#Eval("GeneralID") %>';">
                <td class="td_s">
                    <input type="radio" name="GeneralID" value="<%#Eval("GeneralID") %>" /></td>
                <td><%#Eval("GeneralID") %></td>
                <td><%#Eval("Title") %></td>
                <td><%#Eval("Inputer") %></td>
                <td><%#Eval("NodeName") %></td>
                <td><%#Eval("Hits") %></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate></FooterTemplate>
    </ZL:Repeater>
</table>
<asp:HiddenField ID="HtmlContent_Hid" runat="server" />
<div class="text-center">
    <asp:Button ID="SelContent_B" runat="server" CssClass="btn btn-primary" Text="选择" OnClick="SelContent_B_Click" />
    <button type="button" class="btn btn-primary" onclick="parent.CloseDiag();">取消</button>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script type="text/javascript" src="/JS/ICMS/ZL_Common.js"></script>
<script>
    $(function () {
        $("#EGV tr").click(function () {
            var radio = $(this).find("input[name='GeneralID']")[0];
            if (radio) {
                radio.checked = true;
                $("#EGV tr").removeClass('selected');
                $(this).addClass('selected');
            }
        });
    });
    function SetContent(title, gid) {
        parent.GetContent($("#HtmlContent_Hid").val(), title, gid);
    }
</script>
</asp:Content>