<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectPicEditor.aspx.cs" Inherits="ZoomLa.WebSite.Common.SelectPicEditor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>上传图片</title>
    <script type="text/javascript" src="Common.js"></script>    
    <script type="text/javascript">
    
   function setImg()
   {      
      if($('FilePicPath').value.trim().length==0)
      {
        alert("没有选择文件");
        $("FilePicPath").focus();
        return;  
      }  
      var url=document.form1.FilePicPath.value;
      var oEditor = window.parent.InnerDialogLoaded() ;
      var oImg = oEditor.FCK.CreateElement( 'IMG' ); 
      oImg.src = url;
	  oImg.setAttribute( '_fcksavedurl', url) ;
	  window.parent.Cancel() ;
   }
   
   function isok()
   {        
        if($("File1").value=="")
        {
            alert("请选择图片路径!");
            $("File1").focus();
            return false;
        }
        return true;
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
<body style="margin:0px">
    <form id="form1" runat="server">
        <table width="100%" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table width="440" cellpadding="4" cellspacing="0" border="0" align="center">                        
                        <tr>
                            <td colspan="2">
                                文件路径：<asp:TextBox ID="FilePicPath" runat="server" Columns="35"></asp:TextBox>
                                <input id="Button2" type="button" value="插 入" class="btn" onclick="setImg()" />
                                </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                选择文件：<input id="File1" type="file" size="35" name="File1" runat="server" class="btn" />
                                <asp:Button ID="Button1" runat="server" Text=" 上传 " Height="22px" OnClick="Button1_Click"
                                    CssClass="btn" OnClientClick="return isok()" />
                                <br /><span style="color:Red;">图片大小不能超过100K</span>
                            </td>
                        </tr>                        
                    </table>
                   </td>
            </tr>
        </table>
    </form>
</body>
</html>
