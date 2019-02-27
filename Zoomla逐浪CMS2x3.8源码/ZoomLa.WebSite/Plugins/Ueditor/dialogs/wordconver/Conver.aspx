<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Conver.aspx.cs" Inherits="Plugins_Ueditor_dialogs_wordconver_Conver" ClientIDMode="Static" EnableViewState="false" %><!DOCTYPE html>
<html>
<head runat="server">
<meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<meta name="viewport" content="width=device-width, initial-scale=1.0" />
<script type="text/javascript" src="../internal.js"></script>
<title>Word转换</title>
</head>
<body>
<form id="form1" runat="server">
<div style="padding: 5px;">
    <div>
        <ZL:FileUpload runat="server" ID="File_UP"/>
        <%--<asp:FileUpload runat="server" ID="File_UP" />--%>
        <asp:Button runat="server" ID="Add_Btn" Text="开始解析" OnClick="Add_Btn_Click" OnClientClick="ShowDiv();" Style="padding-left: 5px; border-radius:3px;" /><br/>
        *支持DOC、DOCX,RTF,TXT等格式上传。
        <div style="display: none;" runat="server" id="Content_Div"></div>
    </div>
    <div style="display: none;" id="waitdiv">正在努力解析中,请等待...</div>
</div>
<script type="text/javascript">
    function SetContent() {
        editor.setContent(document.getElementById("Content_Div").innerHTML);
        dialog.close();
    }
    function ShowDiv() {
        document.getElementById("waitdiv").style.display = "";
    }
</script>
</form>
</body>
</html>
