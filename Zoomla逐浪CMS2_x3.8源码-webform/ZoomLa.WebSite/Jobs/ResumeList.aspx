<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResumeList.aspx.cs" Inherits="ZoomLa.WebSite.Jobs.ResumeList" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>求职信息</title>
<link href="../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<div class="us_seta"><a title="网站首页" href="/" target="_parent"><asp:Label ID="LblSiteName" runat="server" Text="Label"></asp:Label></a>&gt;&gt;
<asp:Label ID="lblUserName" runat="server" Text="Label"></asp:Label>&gt;&gt; 求职信息</div>
  <div class="us_seta" style="margin-top:10px;" id="manageinfo" runat ="server">
    <h1 style="text-align:center">求职信息列表</h1>
    <ZL:ExGridView ID="Egv" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="CID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" OnRowCommand="Lnk_Click" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataText="暂时没有用户向贵单位投递简历">
      <Columns>
      <asp:BoundField DataField="CID" HeaderText="ID">
        <HeaderStyle Width="5%" />
        <ItemStyle HorizontalAlign="Center" />
      </asp:BoundField>
      <asp:TemplateField HeaderText="求职用户" >
        <ItemTemplate> <%# GetUser(Eval("UserID", "{0}"))%> </ItemTemplate>
        <HeaderStyle Width="10%" />
        <ItemStyle HorizontalAlign="Center" />
      </asp:TemplateField>
      <asp:TemplateField HeaderText="申请职位" >
        <ItemTemplate> <%# GetJob(Eval("JobID", "{0}"))%> </ItemTemplate>
        <HeaderStyle Width="30%" />
        <ItemStyle HorizontalAlign="Center" />
      </asp:TemplateField>
      <asp:BoundField DataField="SendTime" HeaderText="发出时间">
        <HeaderStyle Width="15%" />
        <ItemStyle HorizontalAlign="Center" />
      </asp:BoundField>
      <asp:TemplateField HeaderText="操作" >
        <ItemTemplate>
          <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Show" CommandArgument='<%# Eval("ResumeID") %>'>查看简历</asp:LinkButton>
          | <a href="SendExaminee.aspx?ID=<%# Eval("ResumeID") %>" target="_blank">面试通知</a> |
          <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Del" CommandArgument='<%# Eval("CID") %>' OnClientClick="return confirm('你确定将该数据删除吗？')">删除</asp:LinkButton>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
      </asp:TemplateField>
      </Columns>
      <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
      <RowStyle BackColor="#EFF3FB" Height="25px" />
      <EditRowStyle BackColor="#2461BF" />
      <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
      <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
      <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
      <AlternatingRowStyle BackColor="White" />
      <PagerSettings FirstPageText="第一页" LastPageText="最后页" Mode="NextPreviousFirstLast" NextPageText="下一页" PreviousPageText="上一页" />
    </ZL:ExGridView>
  </div>
</form>
</body>
</html>