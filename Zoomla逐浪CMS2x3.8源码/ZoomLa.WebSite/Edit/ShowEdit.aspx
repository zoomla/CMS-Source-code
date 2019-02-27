<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowEdit.aspx.cs" Inherits="Edit_ShowEdit" EnableViewStateMac="false" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
<title>选择打开文件</title>
<% m_UserInput = Server.HtmlEncode(Request.QueryString["OpenWords"]);%> 
<link href="../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
<link href="../App_Themes/UserThem/edit.css" rel="stylesheet" type="text/css" />
<base target="_self" /> 
</head>
<body>
    <form id="form1" runat="server">
    <script src="/JS/Popup.js" language="javascript" type="text/javascript"></script>
    <table width="100%" border="0" cellpadding="2" cellspacing="1">
        <tr class="tdbg">
            <td align="right">
                搜索当前目录文件：
                <asp:TextBox ID="TxtSearchKeyword" runat="server"></asp:TextBox>
                <asp:Button ID="BtnSearch" runat="server" Text="搜索" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%">
        <tr>
            <td>
                当前目录：<asp:Label ID="LblCurrentDir" runat="server"></asp:Label>
            </td>
            <td><a href="javascript:backToMainDir()" style="color:blue;">返回主目录</a></td>
            <td><a href="javascript:backToPervious()" style="color:blue;">返回上级目录</a></td>
         </tr>
    </table>
   <div class="editdocLi"> <ul>
    <asp:Repeater ID="RptFiles" runat="server" OnItemCommand="RptFiles_ItemCommand"> 
        <ItemTemplate>
            <li>   
                <%# System.Convert.ToInt32(Eval("type")) == 1 ? "<a href='javascript:openDir(\""+ Eval("Name")+"\");' style='background:url(/App_Themes/AdminDefaultTheme/images/System13.png) no-repeat top center;'>" + Eval("Name") + "</a>" : "<span>" + "<a href='javascript:add(\"" + Request.QueryString["Dir"] + "/" + Eval("Name") + "\")' title='" + Eval("Name") + "'>" + Eval("Name") + "</a></span> "%>
            </li>
        </ItemTemplate> 
    </asp:Repeater>
    </ul>
    </div>
    <div id="dHTMLADPreview" style="z-index: 1000; left: 0px; visibility: hidden; width: 10px; position: absolute; top: 0px; height: 10px">
    </div>
    <script  type="text/javascript">
        function add(obj) {
            opener.document.getElementById('<%= m_UserInput %>').value = "";
            if (obj == "") { return false; }
            opener.document.getElementById('<%= m_UserInput %>').value = obj;
            window.close();
            opener.GetWord();
        }
        //打开文件夹
        function openDir(dirName)
        {
            location.href = "ShowEdit.aspx?Dir="+dirName+"&OpenWords=" + '<%= m_UserInput %>';
        }
        //返回主目录
        function backToMainDir() { location.href = "ShowEdit.aspx?OpenWords=" + '<%= m_UserInput %>'; }
        function backToPervious() { history.go(-1);}
    </script>
    </form>
</body>
</html>
