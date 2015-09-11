<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SourceList.aspx.cs" Inherits="manage_AddOn_SourceList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择来源</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="width: 100%; height: 100%" border="0" cellpadding="2" cellspacing="0"
                class="border">
                <tr style="height: 100%; width: 100%">                    
                    <td class="tdbg" align="left" valign="top" colspan="3" style="width: 70%;">
                        <div id="f1" visible="false" runat="server"><iframe id="main_right" name="main_right" scrolling="yes" style="width: 100%; height: 100%"
                            src="SelectSource.aspx?flag=source" frameborder="0" marginheight="0" marginwidth="1" runat="server"></iframe></div>
                      <div id="f2" visible="false" runat="server"><iframe id="Iframe1" name="main_right" scrolling="yes" style="width: 100%; height: 100%"
                            src="SelectSource.aspx?flag=author" frameborder="0" marginheight="0" marginwidth="1" runat="server"></iframe></div>
                              <div id="f3" visible="false" runat="server"><iframe id="Iframe2" name="main_right" scrolling="yes" style="width: 100%; height: 100%"
                            src="SelectSource.aspx?flag=keyword" frameborder="0" marginheight="0" marginwidth="1" runat="server"></iframe></div>
                    </td>
                </tr>
                <tr class="title" style="height: 22; width: 177px">
                    <td style="width: 103px" align="right">
                        来源名称：</td>
                    <td align="left">
                        <input type="text" id="FileName" size="60" style="height:22px;" readonly="readOnly" class="inputtext" />
                        <input type="hidden" id="ParentDirText" />
                        <?xml namespace="" prefix="ASP" ?>
                        <asp:HiddenField ID="HdnParentDir" runat="server" />
                    </td>
                    <td align="center" style="width: 177px">
                        <input type="button" class="inputbutton" id="BtnSubmit" value="　确定　" onclick="javascript:window.close();add()" />
                        <input type="button" class="inputbutton" id="BtnCancel" value="　取消　" onclick="javascript:window.close();" />
                    </td>
                </tr>
            </table>
            </div>
    </form>
      <script language="javascript" type="text/javascript">
    function add()
    {
        window.dialogArguments.document.getElementById('TxtTemplate').value = document.getElementById('FileName').value;
    }
    </script>
</body>
</html>
