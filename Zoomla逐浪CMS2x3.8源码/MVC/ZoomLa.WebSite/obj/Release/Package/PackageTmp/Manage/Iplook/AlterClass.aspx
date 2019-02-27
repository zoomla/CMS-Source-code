<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlterClass.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="ZoomLaCMS.Manage.Iplook.AlterClass" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>IP分类管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
      <div class="r_navigation">
        <span>后台管理</span> &gt;&gt; <span>扩展功能</span> &gt;&gt; <span>其他功能</span> &gt;&gt; <span><a href="DownServerManage.aspx"> IP分类管理</a></span>
        &gt;&gt; <span> 
            <asp:Label ID="Label4" runat="server" Text="修改分类信息"></asp:Label></span>
    </div>
    <div class="clearbox">
    </div>
    <table class="table table-striped table-bordered table-hover">
        <tr class="text-center">
            <td colspan="2" class="spacingtitle">
                <strong>
                    <asp:Label ID="LblTitle" runat="server" Text="修改分类信息" Font-Bold="True"></asp:Label>
                </strong>
            </td>
        </tr>
        
        <tr>
            <td align="left" class="style1">
                <strong>分类ID：</strong>
            </td>
            <td class="tdbg" style="text-align: left; width: 70%;">
                <asp:TextBox ID="class_ID"  class="form-control" runat="server" Width="290px" Enabled="False"></asp:TextBox>
               
            </td>
        </tr>
        <tr>
            <td  align="left" class="style1">
                <strong>分类名：</strong><br />
            </td>
            <td class="tdbg" style="text-align: left; width: 70%;">
                <asp:TextBox ID="class_name" class="form-control" runat="server" Width="290px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <strong>所属分类：</strong>
            </td>
            <td class="tdbg" style="text-align: left; ">
                <asp:DropDownList ID="leadto_ID" runat="server" Width="156px" DataTextField="class_name" DataValueField="class_ID">
        </asp:DropDownList>
            </td>
        </tr>
       
        <tr class="tdbg">
            <td style="text-align: center" colspan="2">
                <asp:Button ID="EBtnModify" class="btn btn-primary"  Text="修改" OnClick="EBtnModify_Click" runat="server" /><input name="BtnCancel" type="button" class="btn btn-primary"  onclick="javascript:window.location.href='IPManage.aspx'"
                    value=" 取消 " /></td>
        </tr>
    </table>
</asp:Content>

