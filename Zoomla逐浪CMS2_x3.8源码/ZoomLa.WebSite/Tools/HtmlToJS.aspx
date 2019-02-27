<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HtmlToJS.aspx.cs" Inherits="Tools_HtmlToJS" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>互转</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
   <div style="margin-bottom:5px;">
        <ZL:FileUpload runat="server" ID="Html_UP" AllowExt="html,txt,htm,shtml" style="display:inline-block;" />
    <asp:Button runat="server" ID="BeginUP_Btn" Text="上传" OnClick="BeginUP_Btn_Click" CssClass="btn btn-primary" />
   </div>
   <div class="panel panel-default">
       <div class="panel-heading">请输入Html代码
           <input type="button" value="转换" class="btn btn-primary" onclick="Convert();" />
       </div>
       <div class="panel-body">
           <asp:TextBox runat="server" ID="Html_T" TextMode="MultiLine" style="height:200px;width:100%;"></asp:TextBox>
       </div>
   </div>
   <div class="panel panel-default">
        <div class="panel-heading">
            JS代码处
            <input type="button" value="转换" class="btn btn-primary" onclick="ConvertToHtml();" />
        </div>
       <div class="panel-body">
           <asp:TextBox runat="server" ID="JS_T" TextMode="MultiLine" style="height:200px;width:100%;">
             
           </asp:TextBox>
       </div>
   </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script src="/JS/ZL_Regex.js"></script>
    <script>
        function Convert() {
            //*需要去除首尾空格,否则会带有&nbsp;
            var html = $("#Html_T").val();
            html = html.replace(/\"/g, "\\\"");
            var strArr = html.split(/\n/g);
            var result = "";
            for (var i = 0; i < strArr.length; i++) {
                if (ZL_Regex.isEmpty(strArr[i])) continue;
                if (result == "") { result = 'var html="' + strArr[i].trim() + '"'; }
                else { result += '\n+"' + strArr[i].trim() + '"'; }
            }
            result += ";";
            $("#JS_T").val(result);
        }
        function ConvertToHtml()
        {
            var html = $("#JS_T").val();
            html = html.replace(/\\\"/g, "\"").replace("var html =","");
            var strArr = html.split(/\n/g);
            var result = "";
            for (var i = 0; i < strArr.length; i++) {
                if (ZL_Regex.isEmpty(strArr[i])) continue;
                var start = strArr[i].indexOf("\"");
                var end = strArr[i].lastIndexOf("\"")
                result += strArr[i].substr((start + 1), (end - start - 1)) + "\r\n";//
            }
            $("#Html_T").val(result);
        }
        String.prototype.trim = function () {
            return this.replace(/(^\s*)|(\s*$)/g, '');
        }
    </script>
</asp:Content>