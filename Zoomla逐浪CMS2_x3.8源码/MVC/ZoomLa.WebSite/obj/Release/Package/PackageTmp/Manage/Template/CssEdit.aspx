<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CssEdit.aspx.cs" Inherits="ZoomLaCMS.Manage.Template.CssEdit" MasterPageFile="~/Manage/I/Default.master"  ValidateRequest="false"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" href="/Plugins/CodeMirror/lib/codemirror.css">
    <link rel="stylesheet" href="/Plugins/CodeMirror/theme/eclipse.css">
    <script src="/Plugins/CodeMirror/lib/codemirror.js"></script>
    <script src="/Plugins/CodeMirror/mode/css.js"></script>
    <script src="/Plugins/CodeMirror/addon/selection/active-line.js"></script>
    <script src="/Plugins/CodeMirror/mode/htmlmixed.js"></script>
    <title>样式编辑</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-striped table-bordered table-hover">
    <tr class="title" style="font-weight:normal;">
        <td> 
         <%--   <%=ShowPath.Trim()%> --%>
             <span style="float:left; margin-left:10px; margin-right:10px; font-weight:bold; margin-top:0.3em;">请输入风格名称:</span>
            <div class="input-group" style="width: 300px; float:left;">
                   <input runat="server" id="TxtFilename" style="text-align: right" class="form-control" />
                   <asp:Label ID="name_L" runat="server" class="input-group-addon">
                       .css
                   </asp:Label>
               </div>
    </td>
    </tr>
    <tr>
        <td>
          <asp:TextBox ID="textContent" runat="server" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
</table>
<div style="text-align:center; width:100%">
    <asp:Button ID="Button1" runat="server" Text="保存风格" OnClick="Button1_Click" class="btn btn-primary"/>
    <asp:Button ID="Button2" runat="server" Text="取消操作" OnClick="Button2_Click" class="btn btn-primary"/>
  
</div>
    <asp:HiddenField ID="HdnShowPath" runat="server" />
    <asp:HiddenField ID="Hdnmethod" runat="server" />
    <asp:HiddenField ID="HdnFilePath" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        var editor = CodeMirror.fromTextArea(document.getElementById("textContent"), {
            lineNumbers: true,
            styleActiveLine: true
        });
    </script>
</asp:Content>