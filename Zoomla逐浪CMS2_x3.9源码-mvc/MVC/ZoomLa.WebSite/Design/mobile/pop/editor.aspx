<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editor.aspx.cs" Inherits="ZoomLaCMS.Design.mobile.pop.editor"  MasterPageFile="~/Common/Master/Empty.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>内容编辑</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <header class="bar bar-nav" style="position:fixed;">
    <a class="button button-link button-nav pull-left back" href="javascript:;" onclick="closeSelf();">
      <span class="icon icon-left"></span>
      返回
    </a>
    <a href="javascript:;" class="button button-fill button-nav pull-right" onclick="save();">保存修改</a>
    <h1 class="title">内容编辑</h1>
</header>
   <textarea id="editor_t" style="width:100%;"></textarea>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<link href="/design/JS/sui/css/sm.min.css" rel="stylesheet" />
<link href="/Plugins/mbeditor/mbeditor.css" rel="stylesheet" />
<style type="text/css">
.wangEditor-mobile-txt {padding-top:70px;}
</style>
<script src="/Design/h5/js/zepto.js"></script>
<script src="/Plugins/mbeditor/mbeditor.js"></script>
<script>
    var editor = new ___E('editor_t');
    editor.init();
    editor.$txt.focus();
    function settxt(content) {
        editor.$txt.html(content);
        editor.$txt.focus();
    }
    function closeSelf() {
        parent.closeEditor();
    }
    function save() {
        parent.saveEditor(editor.$txt.html());
    }
</script>
</asp:Content>
