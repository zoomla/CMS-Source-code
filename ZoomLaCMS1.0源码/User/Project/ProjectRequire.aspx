<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectRequire.aspx.cs" Inherits="User_Project_ProjectRequire" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>需求提交</title>
     <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>服务要求</span>&gt;&gt;<span><a href="ProjectList.aspx">项目列表</a></span> &gt;&gt<span> 提交服务</span>	</div> 
                                        <div class="clearbox"></div> 
                                         
      <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <asp:Label ID="LblTitle" runat="server" Text="提交客户要求" Font-Bold="True"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>用户名：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtUserName" runat="server" ReadOnly="true"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ValrKeywordText" ControlToValidate="TxtUserName"
                    runat="server" ErrorMessage="用户名不能为空！" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>需求描述：&nbsp;</strong></td>
            <td class="tdbg" align="left">
               <asp:TextBox ID="TxtRequireContent" runat="server" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtUserName"
                    runat="server" ErrorMessage="需求描述不能为空！" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
          <tr class="tdbgbottom">
            <td colspan="2">
                             <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="取消" onclick="javascript:window.location.href='ProjectList.aspx'" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
