<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateSQL.aspx.cs" Inherits="ZoomLaCMS.Manage.Config.CreateSQL" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>插件管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table width="100%"  cellpadding="5" cellspacing="1" class="table table-bordered table-hover table-striped">
<tr class="tdbg">
    <td class="tdbgleft" align="right" style="width: 150px"><strong>插件名称：</strong></td>
    <td>
        <asp:TextBox ID="txtName" runat="server" class=" form-control" Width="156" MaxLength="20"></asp:TextBox>
    </td>
</tr>
    <tr class="tdbg">
    <td class="tdbgleft" align="right" style="width: 150px"><strong>创建的数据表名：</strong></td>
    <td>
        ZL_my_<asp:TextBox ID="txtTableName" runat="server" class=" form-control" Width="113"></asp:TextBox>
    </td>
</tr>
<tr class="tdbg">
    <td class="tdbgleft" align="right" ><strong>项目单位：</strong><br />例如：篇、个、张、件</td>
    <td>
          <asp:TextBox ID="txtUnit" runat="server" class=" form-control" Width="156" MaxLength="20" ></asp:TextBox>
    </td>
</tr>
      
<tr class="tdbg">
    <td class="tdbgleft" align="right" ><strong>项目图标：</strong></td>
    <td>
          <asp:TextBox ID="txtIcon" runat="server"  class=" form-control" Width="156" MaxLength="20"></asp:TextBox>
    </td>
</tr>
<tr class="tdbg">
    <td class="tdbgleft" align="right" ><strong>项目描述：</strong></td>
    <td>
          <asp:TextBox ID="txtExplain" runat="server" class=" form-control" Width="156" MaxLength="20"></asp:TextBox>
    </td>
</tr>
<tr class="tdbg">
    <td class="tdbgleft" align="right" ><strong>可执行次数：</strong></td>
    <td>
          <asp:TextBox ID="txtRunNum" runat="server" class=" form-control" Width="156" MaxLength="20"></asp:TextBox>
    </td>
</tr>
<tr class="tdbg">
    <td class="tdbgleft" align="right" ><strong>关联负责人：</strong></td>
    <td>
          <asp:TextBox ID="txtUserName" runat="server" class=" form-control" Width="156" MaxLength="20"></asp:TextBox>
        <asp:HiddenField ID="TxtUserID" runat="server" />
    </td>
</tr>
<tr class="tdbg">
    <td class="tdbgleft" align="right"><strong>按钮名称：</strong></td>
    <td>
          <asp:TextBox ID="txtBtnName" runat="server" class=" form-control" Width="156" MaxLength="20"></asp:TextBox>
    </td>
</tr>
<tr class="tdbg">
    <td class="tdbgleft" align="right" ><strong>附加文件：</strong></td>
    <td>
          <asp:TextBox ID="txtSqlUrl" runat="server" class=" form-control" Width="156" MaxLength="20"></asp:TextBox>
    </td>
</tr>      

<tr class="tdbg">
    <td class="tdbgleft" align="right" ><strong>自动计划：</strong></td>
    <td>
          <asp:TextBox ID="txtRunTime" runat="server" class=" form-control" Width="156" MaxLength="20"></asp:TextBox>
    </td> 
    </tr>
    <tr class="tdbg">
        <td ></td><td><asp:Button ID="CreateBtn" runat="server" text="添加" OnClick="CreateBtn_Click" class="btn btn-primary" /></td> 
    </tr>
</table> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>
