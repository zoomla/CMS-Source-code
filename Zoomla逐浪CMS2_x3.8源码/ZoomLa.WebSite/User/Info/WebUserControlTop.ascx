<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebUserControlTop.ascx.cs" Inherits="User_Info_WebUserControlTop" %>
 <ul style="width:100%;border:1px solid #ddd; padding:10px 10px;" class="list-unstyled current">
        <li style="width:60px; text-align:center " ><a href="UserInfo.aspx">注册信息</a></li>
        <li style="width:60px; text-align:center "><a href="UserBase.aspx">基本信息</a></li>
        <li style="width:60px; text-align:center "><a href="DredgeVip.aspx">VIP信息</a></li>
          <li style="width:100px; text-align:center "><a href="listprofit.aspx">我的收益信息</a></li>
          <li style="width:80px; text-align:center; display:none"><a href="UserBank.aspx">银行信息</a></li>
          <%--<li style="width:60px; text-align:center "><a href="User_regaction.aspx">密保问题</a></li>--%>
        <li style=" text-align:center ;width:120px;"><asp:Label ID="Label4" runat="server" Text=""></asp:Label></li><div class="clearfix"></div>
 </ul>
<div class="clearfix"></div>
<style>
    .current li{ float:left; margin-right:5px;}
</style>