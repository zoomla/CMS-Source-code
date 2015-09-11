<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertLabel.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Template.InsertLabel" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>动态标签编辑器-<% =Request.QueryString["n"]%></title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form runat="server" id="form1">
        <table style="width:100%; height:100%" cellpadding="2" cellspacing="1" class="border">
            <tr style="height:30px">
                <td class="spacingtitle" valign="middle">
                    <img src="../Images/img_u.gif" alt="" />
                    <span style="color: #0000FF;font-weight: bold;font-size: 12px;">
                    <asp:Label ID="LabelName" runat="server"></asp:Label>
                    </span>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft">
                    <div style="height:100%; overflow: auto; border:1px double #ffffff">
                    <div style="width: 100%; text-align: center">
                            <asp:Label ID="labelintro" runat="server">无标签说明</asp:Label></div>
                    <div style="width: 95%; text-align: center">
                        <asp:Label ID="labelbody" runat="server"></asp:Label></div>
                    </div>
                </td>
            </tr>
            <tr style="height:30px">
               <td class="tdbgleft" align="center">
                   <input type="button" class="inputbutton" value="添加为标签" onclick="submitdate(<%= LabelType %>);" />&nbsp;&nbsp;
                   <input type="button" class="inputbutton" value="取消" onclick="window.close();" />
               </td>
            </tr>
        </table>
        <script language="JavaScript" type="text/javascript">
    <!--
    function submitdate(lbltype){
        var returnstr;
        if(lbltype=="1")
            returnstr = "{ZL.Source id=\"" + document.getElementById("<% =LabelName.ClientID %>").innerHTML + "\"";
        else
            returnstr = "{ZL.Label id=\"" + document.getElementById("<% =LabelName.ClientID %>").innerHTML + "\"";
        
        var oSpanArr = document.getElementsByTagName('SPAN');
        for ( var i=0; i<oSpanArr.length; i++ ) 
        {
            if(oSpanArr[i].stype=="0")
            {
                var pvalue = document.getElementById(oSpanArr[i].getAttribute("sid")).value;
                if(pvalue != "" && i > 0)
                    returnstr += " " + oSpanArr[i].getAttribute("sid") + "=\"" + pvalue + "\"";
            }
        }        
        if(window.showModalDialog != null)
        {
            window.returnValue = returnstr + " /}";
        }
        else
        {
            var pre = window.opener.document.getElementById('textContent').value.substr(0, window.opener.start);
            var post = window.opener.document.getElementById('textContent').value.substr(window.opener.end);
            window.opener.document.getElementById('textContent').value = pre + returnstr + " /}" + post;
        }        
        window.close();
    }
    -->
        </script>

    </form>
</body>
</html>