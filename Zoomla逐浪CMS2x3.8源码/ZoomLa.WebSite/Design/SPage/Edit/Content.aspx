<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Content.aspx.cs" Inherits="Design_SPage_Edit_Content" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>内容管理</title>
<script src="/Plugins/Ueditor/ueditor.config.js"></script>
<script src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
   <textarea id="content" style="height:500px;"></textarea>
   <div class="text-center margin_t5">
       <input type="button" class="btn btn-info" value="保存内容" onclick="save();" />
   </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script>
    var model = parent.scope.comp;
    var ue = null;
    $(function () {
        $("#content").val(model.data.value);
        ue = UE.getEditor("content");
    })
    function save() {
        model.data.value = parent.Base64.encode(ue.getContent());
        parent.scope.update();
    }
</script>
</asp:Content>

