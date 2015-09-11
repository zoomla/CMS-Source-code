<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="User_Project_Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>项目详细内容</title>
     <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>服务要求</span>&gt;&gt;<span><a href="ProjectList.aspx">用户项目列表</a>  </span>&gt;&gt;<span>项目详细信息</span>	</div> 
                                        <div class="clearbox"></div>
  <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <asp:Label ID="LblTitle" runat="server" Text="客户项目详细信息" Font-Bold="True"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>项目名称：&nbsp;</strong></td>
            <td class="tdbg" align="left"> 
                <asp:Label ID="LblProName" runat="server" Text=""></asp:Label>               
                
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>项目简述：&nbsp;</strong></td>
            <td class="tdbg" align="left">               
                <asp:Label ID="LblProIntro" runat="server" Text=""></asp:Label>  
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>开始时间：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                         <asp:Label ID="LblStartDate" runat="server" Text=""></asp:Label>
                                     </td>
        </tr>
         <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>完成时间：&nbsp;</strong></td>
            <td class="tdbg" align="left">
             <asp:Label ID="LblEndDate" runat="server" Text=""></asp:Label>
                                    </td>
        </tr>    
             <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>工作内容：&nbsp;</strong></td>
            <td class="tdbg" align="left">
             <asp:Label ID="LblContent" runat="server" Text="暂无工作内容"></asp:Label>
                                    </td>
        </tr>          
    </table>
    <div class="clearbox"></div>    
    </form>
</body>
</html>
