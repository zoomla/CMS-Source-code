<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlterIP.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="ZoomLaCMS.Manage.Iplook.AlterIP" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>添加IP信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="r_navigation">
		
		<span>后台管理</span>&gt;&gt;<span>扩展功能</span> &gt;&gt;<span><a href="InputIp.aspx"></a>修改IP信息</span> </div> 
		<div class="clearbox"></div>  

    <table class="table table-striped table-bordered table-hover">
        <tr class="text-center">
            <td colspan="2" class="spacingtitle">
                <asp:Label ID="LblTitle" runat="server" Text="修改IP信息" Font-Bold="True"></asp:Label></td></tr>
        <tr>
            <td class="style1">
                <strong> <asp:Label ID="Label5" runat="server" Text="Label" Width="60%">分类名称：</asp:Label></strong></td>
            <td>
            
        <asp:DropDownList ID="class_ID" runat="server"  Width="155px" DataTextField="class_name" DataValueField="class_ID">
        </asp:DropDownList>
            </td>
        </tr>   
            <tr>
                <td>
                <strong><asp:Label ID="Label1" runat="server" Text="起始ＩＰ："></asp:Label></strong> 
                </td>
                <td> 
       <asp:TextBox ID="startIP" class="l_input"  runat="server" Width="150px"></asp:TextBox>
                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator1" runat="server" ControlToValidate="startIP" 
                        ErrorMessage="请输入正确格式的IP" 
                        ValidationExpression="\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="startIP" ErrorMessage="请输入起始IP"></asp:RequiredFieldValidator>
                </td>
            </tr>
         <tr>
            <td class="style1" align="center">
                <strong> <asp:Label ID="Label2" runat="server" Text="终止ＩＰ："></asp:Label></strong></td>
            <td class="tdbg" align="left">
        <asp:TextBox ID="endIp" class="form-control" runat="server" Width="150px"></asp:TextBox>
                <asp:RegularExpressionValidator 
                    ID="RegularExpressionValidator2" runat="server" ControlToValidate="endIp" 
                    ErrorMessage="请输入正确格式的IP" 
                    ValidationExpression="\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="endIp" ErrorMessage="请输入终止IP"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style1" align="center">
                <strong> <asp:Label ID="Label3" runat="server">省级名称：</asp:Label></strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="pro_name" class="form-control" runat="server" Width="150px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1" align="center">
                <strong> <asp:Label ID="Label4" runat="server" Text="Label">市级名称：</asp:Label></strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="city_name" class="form-control" runat="server" Width="150px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="5">
        <asp:Button ID="submit" runat="server" Text="修改"   class="btn btn-primary"
            style="height: 22px" Height="16px" onclick="submit_Click" />
                &nbsp;&nbsp;
            </td>
        </tr>
    </table> 
</asp:Content>
