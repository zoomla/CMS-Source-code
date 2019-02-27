<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditOrderSql.aspx.cs" Inherits="manage_Shop_EditOrderSql" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
      <title>查看账单申请</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
      <table class="table table-striped table-bordered table-hover">
          <tr><td align="right" class="tdbgleft" height="24" > 帐单类型:</td><td> 
           <asp:DropDownList ID="DrpOrderType" runat="server"><asp:ListItem Value="1">新开返利</asp:ListItem><asp:ListItem Value="2">消费上报</asp:ListItem></asp:DropDownList></td></tr>
            <tr>
			 <td align="right" class="tdbgleft" height="24" >报单商家用户ID:</td><td><asp:TextBox ID="TxtUserName" runat="server"  class="l_input" Width="453px"></asp:TextBox></td></tr>
        <tr>
            <td align="right" class="tdbgleft">申请时间:</td><td><asp:TextBox ID="TxtAddTime" runat="server" class="l_input"  Width="453px"></asp:TextBox></td></tr>
          <tr>
            <td align="right" class="tdbgleft">返分会员的ID:</td><td><asp:TextBox ID="TxtPuserID" runat="server"  class="l_input" Width="453px"></asp:TextBox></td></tr>
        <tr>
            <td align="right" class="tdbgleft">返分会员用户名:</td><td><asp:TextBox ID="TxtPuserName" runat="server"  class="l_input" Width="453px"></asp:TextBox></td></tr>
        <tr>
            <td align="right" class="tdbgleft">订单金额:</td><td><asp:TextBox ID="Txtmoney" runat="server" class="l_input" Width="453px"></asp:TextBox></td></tr>
      <tr>
          <td align="right" class="tdbgleft">脚本:</td><td><asp:TextBox ID="TxtSqlstr" class="l_input" runat="server" Height="107px" TextMode="MultiLine" Width="456px"></asp:TextBox></td></tr> 
           <tr>
            <td align="right" class="tdbgleft">执行次数:</td><td><asp:TextBox ID="TxtRunNum" runat="server" class="l_input" Width="453px"></asp:TextBox></td></tr>
           <tr>
           <td align="right" class="tdbgleft">备注:</td><td><asp:TextBox ID="Txtremark" runat="server" Width="195px"></asp:TextBox></td></tr>
       <tr><td >&nbsp;</td><td  ><asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="修改" Class="C_input" /> 
           <asp:Button ID="Button4" runat="server" OnClick="Run_Click" Text="执行批处理" Class="C_input" /> 
            <asp:Button ID="Button3" runat="server" OnClick="Save_Click" Text="生成脚本文件" Class="C_input" />  
            <asp:Button ID="Button5" runat="server" OnClick="Save1_Click" Text="下载数据库脚本" Class="C_input" />   
           <input type="button" ID="Button2"  Value="返回" Class="C_input" onclick="javascript: history.back();"  /> </td></tr>
    </table>  
        <div id="RunOK" runat="server"></div>
</asp:Content>