<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BatchAddFile.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Common.BatchAddFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>批量设置上传文件</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>    
    文件名格式：<asp:TextBox ID="TxtFileName" runat="server" Columns="30"></asp:TextBox>数字序号通配符为#<br/>    
    开始序号：<asp:TextBox ID="TxtStartNum" runat="server" Columns="5">1</asp:TextBox>
    结束序号：<asp:TextBox ID="TxtEndNum" runat="server" Columns="5">99</asp:TextBox><br/>
        <font color=red>上传的文件请预先上传至<font color=blue>文件地址格式</font>中相应目录</font><br/>
        <input type=button ID="Button1" value="确定设置" OnClick="ReturnUrl();" /></div>
    <script language="javascript" type="text/javascript">
<!--
function ReturnUrl()
{
    var url="";
    var fileobj = document.getElementById('TxtFileName');
    var numobj=document.getElementById('TxtStartNum');
    var num1obj=document.getElementById('TxtEndNum');
    var filename=fileobj.value;
    var num=Number(numobj.value);
    num=num*1;    
    var num1=Number(num1obj.value);
    num1=num1*1;
    for(i=num;i<=num1;i++)
    {
        if(url=="")
            url=filename.replace("#",i);
        else
            url=url+"|"+filename.replace("#",i);
    }
    var isMSIE= (navigator.appName == "Microsoft Internet Explorer");
    if (isMSIE)
    {
        window.returnValue = url;
        window.close();
    }
    else
    {
        
        var obj = opener.document.getElementById('<%= Request.QueryString["ClientId"] %>');
        var arr=url.split("|");
        for(i=0;i<arr.length;i++)
        {
            var newurl='图片地址'+(obj.length+1)+'|'+arr[i];
            obj.options[obj.length]=new Option(newurl,newurl);
        }
        ChangeHiddenFieldValue();
             
        window.close();
    }
}

function ChangeHiddenFieldValue()
{
    var obj = opener.document.getElementById('<%= Request.QueryString["HiddenFieldId"] %>');
    var softUrls = opener.document.getElementById('<%= Request.QueryString["ClientId"] %>');
    var value = "";
    for(i=0;i<softUrls.length;i++)
    {
        if(value!="")
        {
            value = value+ "$";
        }
        value = value + softUrls.options[i].value;
    }
    obj.value = value;
}
//-->
        </script>
    </form>
</body>
</html>
