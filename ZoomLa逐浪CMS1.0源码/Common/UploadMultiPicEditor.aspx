<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadMultiPicEditor.aspx.cs" Inherits="ZoomLa.WebSite.Common.UploadMultiPicEditor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>多图片上传</title>
    <script type="text/javascript" src="Common.js"></script>    
    <script type="text/javascript">    
   function setImg(url)
   { 
      //var url=document.form1.FilePicPath.value;
      var oEditor = window.parent.InnerDialogLoaded() ;
      var oImg = oEditor.FCK.CreateElement( 'IMG' ); 
      oImg.src = url;
	  oImg.setAttribute( '_fcksavedurl', url) ;
	  window.parent.Cancel() ;
   }
   </script>
   <style type="text/css">
   body
    {
	    background: #f1f1e3;
	    padding: 0px;
	    margin: 0px;
	    font-family: "宋体";
	    font-size: 12px;
    }
   .btn
    {
	    border: solid 1px #7b9ebd;
	    padding: 1px 2px 1px 2px;
	    font-size: 12px;
	    height: 22px;
	    filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr=#ffffff, EndColorStr=#cecfde);
	    color: black;
    }
   </style>
</head>
<body>
<form id="form1" runat="server">
    <table width="100%" align="center" cellpadding="2" cellspacing="0">
            <tr>
                <td>
                    <table width="100%" align="center" cellpadding="2" cellspacing="0">
                        <tr>
                            <td align="right" height="25" width="16%">
                                地址一:
                            </td>
                            <td>
                                <input id="File1" type="file" size="40" name="File1" runat="server" class="btn" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" height="25">
                                地址二:
                            </td>
                            <td>
                                <input id="File2" type="file" size="40" name="File1" runat="server" class="btn" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" height="25">
                                地址三:
                            </td>
                            <td>
                                <input id="File3" type="file" size="40" name="File1" runat="server" class="btn" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" height="25">
                                地址四:
                            </td>
                            <td>
                                <input id="File4" type="file" size="40" name="File1" runat="server" class="btn" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" height="25">
                                地址五:
                            </td>
                            <td>
                                <input id="File5" type="file" size="40" name="File1" runat="server" class="btn" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" height="25">
                                地址六:
                            </td>
                            <td>
                                <input id="File6" type="file" size="40" name="File1" runat="server" class="btn" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="Button1" runat="server" Text=" 上 传 " CssClass="btn" OnClick="Button1_Click" />&nbsp; &nbsp;<input id="Reset1" type="reset" value=" 重 来 " class="btn" /><span style="color:Red;">图片大小不能超过100K</span></td>
                        </tr>
                    </table>
                    <asp:Literal ID="Literal1" runat="server" Visible="false"></asp:Literal></td>
            </tr>
        </table>
</form>
</body>
</html>
