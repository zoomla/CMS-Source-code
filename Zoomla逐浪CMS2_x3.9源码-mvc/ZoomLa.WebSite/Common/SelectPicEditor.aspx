<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectPicEditor.aspx.cs" Inherits="ZoomLaCMS.Common.SelectPicEditor" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>上传图片</title>
<script type="text/javascript" src="Common.js"></script>
<script type="text/javascript">
    function setImg() {
        if ($('FilePicPath').value.trim().length == 0) {
            alert("没有选择文件");
            $("FilePicPath").focus();
            return;
        }
        var url = document.form1.FilePicPath.value;
        var wid = document.form1.TxtWidth.value;
        var hei = document.form1.TxtHeight.value;
        var oEditor = window.parent.InnerDialogLoaded();
        var oImg = oEditor.FCK.CreateElement('IMG');
        oImg.src = url;
        oImg.setAttribute('_fcksavedurl', url);
        if (wid != "")
            oImg.setAttribute('width', wid);
        if (hei != "")
            oImg.setAttribute('height', hei);
        window.parent.Cancel();
    }

    function isok() {
        if ($("FupFile").value == "") {
            alert("请选择图片路径!");
            $("FupFile").focus();
            return false;
        }
        return true;
    }
</script>
<style type="text/css">
body { background: #f1f1e3; padding: 0px; margin: 0px; font-family: "宋体"; font-size: 12px; }
.btn { border: solid 1px #7b9ebd; padding: 1px 2px 1px 2px; font-size: 12px; height: 22px;  filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr=#ffffff, EndColorStr=#cecfde);
color: black; }
</style>
</head>
<body style="margin:0px">
<form id="form1" runat="server">
<table width="100%" align="center" cellpadding="0" cellspacing="0">
<tr>
  <td><table width="440" cellpadding="4" cellspacing="0" border="0" align="center">
	  <tr>
		<td colspan="2"> 图片路径：
		  <asp:TextBox ID="FilePicPath" runat="server" Columns="35" CssClass="btn" Enabled="False"></asp:TextBox>
		  <input id="Button2" type="button" value="插 入" runat="server" class="btn" /></td>
	  </tr>
	  <tr>
		<td> 图片宽度：
		  <asp:TextBox ID="TxtWidth" runat="server" Columns="5" CssClass="btn">100</asp:TextBox>
		  &nbsp;
		  图片高度：
		  <asp:TextBox ID="TxtHeight" runat="server" Columns="5" CssClass="btn">100</asp:TextBox></td>
	  </tr>
	  <tr>
		<td colspan="2"> 选择文件：
		  <ZL:FileUpload ID="FupFile" runat="server"/>
		  <asp:Button ID="Button1" runat="server" Text=" 上传 " Height="22px" OnClick="Button1_Click" CssClass="btn" OnClientClick="return isok()" />
		  <br />
		  <span id="span1" runat="server" style="color:Red;">图片大小不能超过100K</span></td>
	  </tr>
	</table></td>
</tr>
</table>
</form>
</body>
</html>