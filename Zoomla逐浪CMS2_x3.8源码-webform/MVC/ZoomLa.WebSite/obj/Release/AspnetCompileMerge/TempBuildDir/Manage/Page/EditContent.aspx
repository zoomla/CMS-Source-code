<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditContent.aspx.cs" Inherits="ZoomLaCMS.Manage.Page.EditContent" ValidateRequest="false" MasterPageFile="~/Manage/I/Default.master"%>
<%@ Register Src="~/Manage/I/ASCX/TlpDP.ascx" TagPrefix="ZL" TagName="TlpDown" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加内容</title>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    
<table class="table table-striped table-bordered table-hover">
    <tbody id="Tabs0">
         <tr>
            <td class="td_m text-right">                    
               内容标题: </td>
            <td>
                <asp:TextBox ID="txtTitle" class= "form-control text_300" runat="server" Text=''></asp:TextBox>
                <span><font color="red">*</font></span>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtTitle">内容标题必填</asp:RequiredFieldValidator></td>
         </tr>
         <asp:Literal ID="ModelHtml" runat="server"></asp:Literal><tr>
            <td class="td_m text-right">                    
                推荐：</td>
            <td>
                <asp:CheckBox ID="ChkAudit" Text="推荐" runat="server" /></td>
         </tr>
         <tr>
            <td class="text-right">                    
                指定内容模板：</td>
            <td>
                <%=PageCommon.GetTlpDP("TxtTemplate") %>
                <asp:HiddenField ID="TxtTemplate_hid" runat="server" />
         </tr>
    </tbody>        
        <tr class="text-center">
            <td colspan="2">
                <asp:HiddenField ID="HdnItem" runat="server" />
                <asp:HiddenField ID="HdnNode" runat="server" />
                <asp:Button ID="EBtnSubmit" Text="保存项目" class="btn btn-primary"  OnClick="EBtnSubmit_Click" runat="server" />
                &nbsp;                
                <asp:Button ID="BtnBack" runat="server" class="btn btn-primary" Text="返　回" OnClick="BtnBack_Click" UseSubmitBehavior="False"
                    CausesValidation="False" />
            </td>
        </tr>
</table>
<ZL:TlpDown runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Common.js" type="text/javascript"></script>
<script src="/JS/DatePicker/WdatePicker.js" type="text/javascript"></script>
<script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
<script type="text/javascript" src="/JS/ZL_Content.js"></script>
<script>
    $().ready(function () {
        Tlp_initTemp();
    });
    var diag = new ZL_Dialog();
    function ShowTemplist(url) {
        diag.title = "选择模板";
        diag.url = url;
        diag.maxbtn = false;
        diag.ShowModal();
    }
    function CloseDialog() {
        diag.CloseModal();
    }
</script>
</asp:Content>