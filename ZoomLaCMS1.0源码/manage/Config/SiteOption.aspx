<%@ Page Language="C#" AutoEventWireup="true" validateRequest="false" CodeFile="SiteOption.aspx.cs" Inherits="manage_Config_SiteOption" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>网站参数</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
    <script src="../JS/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
     <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<span>网站参数配置</span>
	</div>
	<div class="clearbox"></div>
    <table width="99%" cellspacing="1" cellpadding="0" class="border" align="center">
    <tr class="wzlist">
        <td align="center" id="Nav">
            <a href="SiteInfo.aspx">网站信息</a></td>
        <td align="center" id="Td1">
            <a href="SiteOption.aspx">网站参数</a></td>
        <td align="center" id="Td3">
            <a href="UserConfig.aspx">用户参数</a></td>
        <td align="center" id="Td5">
            <a href="MailConfig.aspx">邮件参数</a></td>
        <td align="center" id="Td4">
            <a href="ThumbConfig.aspx">缩略图参数</a></td>
        <td align="center" id="Td2">
            <a href="IPLockConfig.aspx">IP访问限定</a></td>
    </tr>
    </table> <br />
                <table cellspacing="1" cellpadding="0" class="border" width="99%">
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 200px">
                            <strong>广告目录：</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 200px; ">
                            <strong>是否启用后台管理认证码：</strong></td>
                        <td>
                            <asp:RadioButton ID="RadioButton1" runat="server" GroupName="EnableSiteManageCod" TabIndex="1"/>是
                            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="EnableSiteManageCod" TabIndex="2"/>否</td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 200px; height: 23px;">
                            <strong>是否使用软键盘输入密码：</strong></td>
                        <td>
                            <asp:RadioButton ID="RadioButton3" runat="server" GroupName="EnableSoftKey" TabIndex="1"/>是
                            <asp:RadioButton ID="RadioButton4" runat="server" GroupName="EnableSoftKey" TabIndex="2"/>否</td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 200px">
                            <strong>是否允许上传文件：</strong></td>
                        <td>
                            <asp:RadioButton ID="RadioButton5" runat="server" GroupName="EnableUploadFiles" TabIndex="1"/>是
                            <asp:RadioButton ID="RadioButton6" runat="server" GroupName="EnableUploadFiles" TabIndex="2"/>否</td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 200px; ">
                            <strong>链接地址方式：</strong></td>
                        <td>
                            <asp:RadioButton ID="RadioButton7" runat="server" GroupName="IsAbsoluatePath" TabIndex="1"/>相对路径
                            <asp:RadioButton ID="RadioButton8" runat="server" GroupName="IsAbsoluatePath" TabIndex="2"/>绝对路径</td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 200px">
                            <strong>后台管理目录：</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 200px; ">
                            <strong>后台管理认证码：</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 200px; height: 23px;">
                            <strong>网站模板根目录：</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 200px; height: 23px;">
                            <strong>网站首页模板：</strong></td>
                        <td style="height: 23px">
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><input type="button" value="选择模板" onclick="WinOpenDialog('../Template/TemplateList.aspx?OpenerText='+escape('TextBox2')+'&FilesDir=',650,480)" class="button"/>
                            </td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 200px; height: 23px;">
                            <strong>网站风格目录：</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 200px">
                            <strong>网站上传目录：</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 200px; ">
                            <strong>上传文件的类型：</strong></td><%--？？？--%>
                        <td>
                            <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 200px; height: 23px;">
                            <strong>允许上传的最大文件大小：</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox>KB    提示：1 KB = 1024 Byte，1 MB = 1024 KB</td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 300px; height: 20px;">
                            <strong>上传文件的保存目录规则：</strong></td>
                        <td style="height: 20px">
                            <asp:DropDownList ID="DropDownList1" runat="server">                                
                                <asp:ListItem>{$FileType}/{$Year}{$Month}</asp:ListItem>
                                <asp:ListItem>{$FileType}/{$Year}/{$Month}</asp:ListItem>
                                <asp:ListItem>{$FileType}/{$NodeDir}/{$Year}/{$Month}</asp:ListItem>
                                <asp:ListItem>{$RootDir}/{$Year}/{$Month}</asp:ListItem>
                                <asp:ListItem>{$UserGroupID}/{$Year-$Month}</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                </table>
        <br />
            
        <asp:Button ID="Button1" runat="server" Text="保存设置" OnClick="Button1_Click" /><br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        &nbsp;<br />
        <br />
        &nbsp;
    </form>
</body>
</html>

