<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectTemplate.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Template.SelectTemplate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>选择模板</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%">
        <tr>
			<td align="left">
				当前目录：<asp:Label ID="lblDir" runat="server" Text="Label"></asp:Label>
			</td>
			<td align="right">
                <asp:Literal ID="LitParentDirLink" runat="server"></asp:Literal>
            </td>
		</tr>
    </table>
    <div style="width: 100%; height: 100%;">
            <asp:Repeater ID="RepFiles" runat="server" OnItemDataBound="RepFiles_ItemDataBound">
                <ItemTemplate>
                    <%#  System.Convert.ToInt32(Eval("type")) == 1 ? "<img src=\"../Images/Node/closefolder.gif\" style=border-right: 0px; border-top: 0px;border-left: 0px; border-bottom: 0px />" : "<img src=\"../Images/Node/singlepage.gif\" style=border-right: 0px; border-top: 0px;border-left: 0px; border-bottom: 0px />"%>
                    <a href="#" onclick="<%#  System.Convert.ToInt32(Eval("type")) == 1 ?"openfilesdir('" + Eval("Name") + "')":"add('" + Eval("Name") + "')"%>">
                        <%# Eval("Name")%>
                    </a>
                    <br />
                </ItemTemplate>
            </asp:Repeater>
            <asp:HiddenField ID="HdnFileText" runat="server" />
     </div>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
    function add(obj)
    {
        if(obj==""){return false;}
        parent.document.getElementById('FileName').value = document.getElementById('HdnFileText').value + "/" + obj;
    }
    function openfilesdir(obj)
    {
        if(obj==""){return false;}
        var pathtext="<%= Server.UrlEncode(Request.QueryString["FilesDir"]) %>";

        parent.document.getElementById('ParentDirText').value = pathtext;
        var path="SelectTemplate.aspx?FilesDir="+pathtext+"/"+escape(obj);
        parent.document.getElementById('main_right').src=path;
    }
        </script>
</body>
</html>
