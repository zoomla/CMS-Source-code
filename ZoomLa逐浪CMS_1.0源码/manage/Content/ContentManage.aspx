<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContentManage.aspx.cs" Inherits="ZoomLa.WebSite.Manage.ContentManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>内容管理</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/SelectCheckBox.js"></script>    
</head>
<body>
    <form id="form1" runat="server">    
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>内容管理</span> &gt;&gt;<span>按栏目管理</span>
	</div>
	<div class="clearbox"></div>
	<div class="divline">	    
		<ul style="cursor:hand;">
            <li><a href="ContentManage.aspx?NodeID=<%=Request.QueryString["NodeID"] %>">内容列表</a></li>
            <li><a href="ContentManage.aspx?NodeID=<%=Request.QueryString["NodeID"] %>&flag=Audit">已审核内容</a></li>
            <li><a href="ContentManage.aspx?NodeID=<%=Request.QueryString["NodeID"] %>&flag=UnAudit">未审核内容</a></li>
            <li><a href="ContentManage.aspx?NodeID=<%=Request.QueryString["NodeID"] %>&flag=Elite">推荐内容</a></li>
        </ul>
	</div>
    <div class="clearbox"></div>
    <div class="divline">
        <b>可添加内容：</b><asp:Label ID="lblAddContent" runat="server" Text="Label"></asp:Label>
    </div>
    <div class="clearbox"></div>
    <asp:GridView ID="Egv" runat="server" AllowPaging="True" AutoGenerateColumns="False"
       DataKeyNames="GeneralID" PageSize="20" OnRowDataBound="Egv_RowDataBound" OnPageIndexChanging="Egv_PageIndexChanging" OnRowCommand="Lnk_Click" Width="100%">
        <Columns>
        　　<asp:TemplateField HeaderText="选择">
                  <ItemTemplate>
                      <asp:CheckBox ID="chkSel" runat="server" />
                  </ItemTemplate>
                  <HeaderStyle Width="5%" />
              <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID"　DataField="GeneralID" >
                <HeaderStyle Width="5%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:BoundField>            
            <asp:TemplateField HeaderText="标题">
                <ItemTemplate>  
                    <%# GetTitle(Eval("GeneralID","{0}"),Eval("NodeID","{0}"),Eval("Title","{0}"))%>       
                </ItemTemplate>
                <HeaderStyle Width="30%" />
                <ItemStyle CssClass="tdbg" />
            </asp:TemplateField>            
            <asp:BoundField HeaderText="点击数"　DataField="Hits" >
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="推荐">
                <ItemTemplate>  
                    <%# GetElite(Eval("EliteLevel", "{0}")) %>       
                </ItemTemplate>
                <HeaderStyle Width="10%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>  
                    <%# GetStatus(Eval("Status", "{0}")) %>       
                </ItemTemplate>
                <HeaderStyle Width="10%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="已生成">
                <ItemTemplate>  
                    <%# GetCteate(Eval("IsCreate", "{0}"))%>     
                </ItemTemplate>
                <HeaderStyle Width="10%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="操作" >
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Edit" CommandArgument='<%# Eval("GeneralID") %>'>修改</asp:LinkButton> | 
                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Del" CommandArgument='<%# Eval("GeneralID") %>' OnClientClick="return confirm('你确定将该数据删除到回收站吗？')">删除</asp:LinkButton>
                  </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
         <RowStyle ForeColor="Black" BackColor="#DEDFDE" Height="25px" />
         <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
         <PagerStyle CssClass="tdbg" ForeColor="Black" HorizontalAlign="Center" />
         <HeaderStyle CssClass="tdbg" Font-Bold="True" ForeColor="#E7E7FF" BorderStyle="None" Height="30px" Font-Overline="False" />
         <PagerSettings FirstPageText="第一页" LastPageText="最后页" Mode="NextPreviousFirstLast" NextPageText="下一页" PreviousPageText="上一页" />
    </asp:GridView>
    <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" Font-Size="9pt" OnCheckedChanged="CheckBox2_CheckedChanged" Text="全选" />
    <asp:Button ID="Button1" runat="server" Text="审核通过" CssClass="button" OnClick="btnAudit_Click" OnClientClick="if(!IsSelectedId()){alert('请选择审核项');return false;}else{return confirm('你确定要审核选中内容吗？')}"/>&nbsp;               
    <asp:Button ID="Button2" runat="server" Text="批量删除" OnClick="btnDeleteAll_Click"
        OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('你确定要将所有选择项放入回收站吗？')}" CssClass="button" UseSubmitBehavior="true" />&nbsp;
    <asp:Button ID="Button3" runat="server" Text="批量移动" OnClick="btnMove_Click" OnClientClick="if(!IsSelectedId()){alert('请选择移动项');return false;}else{return true}" CssClass="button" UseSubmitBehavior="true" />    
    </form>
</body>
</html>
