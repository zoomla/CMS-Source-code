<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Content.aspx.cs" Inherits="ZoomLaCMS.Design.SPage.Edit.Content" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>内容管理</title>
<script src="/Plugins/Ueditor/ueditor.config.js"></script>
<script src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="height:600px;overflow:hidden;">
        <textarea id="content" style="height:450px;width:98%;"></textarea>
        <div class="text-center margin_t5">
            <input type="button" class="btn btn-info" value="保存内容" onclick="save();" />
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script>
    var model = parent.scope.comp;
    var ue = null;
    $(function () {
        $("#content").val(parent.Base64.decode(model.data.value));
        ue = UE.getEditor("content", {height:400});
    })
    function save() {
        model.data.value = parent.Base64.encode(ue.getContent());
        parent.scope.label(model);
        parent.scope.update();
    }
</script>
</asp:Content>

