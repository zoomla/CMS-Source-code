<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserUpload.aspx.cs" Inherits="Common_UserUpload" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>用户文件上传</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <div>
         <ZL:FileUpload ID="FileUp_File" runat="server" style="display:inline-block;" />
         <asp:Button ID="FileUp_Btn" runat="server" Text="上传" OnClick="FileUp_Btn_Click"/>
         <asp:RequiredFieldValidator ID="VF" runat="server" ErrorMessage="请选择上传路径" ControlToValidate="FileUp_File" ForeColor="Red" Display="Dynamic" />
         <asp:Label runat="server" ID="FilePath_L"></asp:Label>
         <asp:Label runat="server" ID="LblMsg_L" ForeColor="Red"></asp:Label>
     </div>
     <asp:HiddenField runat="server" ID="FilePath_Hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script>
        //ifr字段js
        var ZLIfrField = {};
        ZLIfrField.UploadPic = function (path, id) {
            if (parent.document.getElementById(id)) {
                parent.document.getElementById(id).value = path;
            }
        }
    </script>
</asp:Content>