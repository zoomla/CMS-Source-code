<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSource.aspx.cs" Inherits="manage_Plus_AddSource" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>来源管理</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
          <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>其他管理</span> &gt;&gt;<span><a href="SourceManage.aspx">来源管理</a></span> &gt;&gt; <span>
                                    添加来源</span>
	</div>
    <div class="clearbox"></div>      
   
   <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="4" class="spacingtitle">
                <strong>
                    <asp:Label ID="LblPTitle" runat="server" Text="添加来源信息"></asp:Label></strong></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>名称：</strong></td>
            <td>
                <asp:TextBox ID="TxtName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="用户名不能为空" ControlToValidate="TxtName" Enabled="False">*</asp:RequiredFieldValidator>
            </td>
            <td rowspan="6" colspan="2">
                <table width="180" border="1">
                    <tr>
                        <td style="width: 100%;" align="center">
                            <img id="showphoto" runat="server" src="../../manage/Images/default.gif" width="150" height="172"
                                alt="来源图片" /></td>
                    </tr>
                </table>
                <asp:TextBox ID="txtpic" runat="server" Width="194px" Height="19px"></asp:TextBox>*<asp:Label
                                                        ID="LabPicPath" runat="server" Text="请选择上传路径！" Visible="False"></asp:Label><br />
                                                    <asp:FileUpload ID="FileUpload1" runat="server" Height="21px" />
                                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="上传" Width="58px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>地址：</strong></td>
            <td>
                <asp:TextBox ID="TxtAddress" runat="server" MaxLength="20"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>电话：</strong></td>
            <td>
                <asp:TextBox ID="TxtTel" runat="server" MaxLength="20"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>传真：</strong></td>
            <td>
                <asp:TextBox ID="TxtFax" runat="server" MaxLength="20"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>联系人：</strong></td>
            <td>
                <asp:TextBox ID="TxtContacterName" runat="server" MaxLength="20" Width="157px" /></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>主页：</strong></td>
            <td>
                <asp:TextBox ID="TxtHomePage" runat="server" /></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>邮编：</strong></td>
            <td>
                <asp:TextBox ID="TxtZipCode" runat="server" MaxLength="20" /></td>
            <td class="tdbgleft">
                <strong>邮件：</strong></td>
            <td>
                <asp:TextBox ID="TxtEmail" runat="server" MaxLength="20" /></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>通讯：</strong></td>
            <td>
                <asp:TextBox ID="TxtMail" runat="server" MaxLength="20" /></td>
            <td class="tdbgleft">
                <strong>IM：</strong></td>
            <td>
                <asp:TextBox ID="TxtIm" runat="server" MaxLength="20" /></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>分类：</strong></td>
            <td colspan="3">
                <asp:TextBox ID="TxtType" runat="server"></asp:TextBox>
                <asp:DropDownList ID="DropAuthorType" runat="server" DataTextField="name" DataValueField="name"
                    Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>属性：</strong></td>
            <td colspan="3">
                <asp:CheckBox ID="ChkElite" runat="server" />推荐
                <asp:CheckBox ID="ChkOnTop" runat="server" />置顶</td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>来源简介：</strong></td>
            <td colspan="3">
                <asp:TextBox ID="TxtIntro" runat="server" Height="300px" Width="583px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否启用</strong>
            </td>
            <td colspan="3">
                <asp:CheckBox ID="ChkPass" runat="server" Checked="true" /></td>
        </tr>
        <tr>
            <td colspan="4" align="center" class="tdbg">
            <asp:Button ID="EBtnModify" Text="修改" OnClick="EBtnModify_Click" runat="server" Visible="false"/>
                <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="Button2" value="取消" onclick="javascript:window.location.href='SourceManage.aspx'" />
                       </tr>
    </table>   
    </form>
</body>
</html>
