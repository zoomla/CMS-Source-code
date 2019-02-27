<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpLoadFile.aspx.cs" Inherits="Common_UpLoadFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>上传文件</title>
    <link href="../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />    
    <base target="_self"></base>
    <script type="text/javascript" src="../manage/js/Common.js"></script>
    <script language="javascript">
    function isok()
    {
        if(document.form1.File1.value=="")
        {
            alert("请选择需要上传的文件")
            document.form1.File1.focus();
            return false;
        }
        return true;
    }
    </script>
</head>
<body onload="setxx()" style="margin:0px">
    <form id="form1" runat="server">
    <div>
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle">
                <asp:TextBox ID="File_PicPath" runat="server" style="display:none"></asp:TextBox>
                <table width="450" cellpadding="4" cellspacing="0" border="0" align="center">
                    <tr>
                        <td>
                            选择文件：<input id="File1" type="file" size="29" name="File1" runat="server" class="btn">
                            <asp:Button ID="Button1" runat="server" Text=" 上传 " Height="22px" onmousedown="isok()" OnClick="Button1_Click" CssClass="btn" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>    
    </div>
    </form>
</body>
</html>
<script>
function setxx()
{
  $('File_PicPath').value = dialogArguments.document.form1.FilePicPath.value
}
</script>
