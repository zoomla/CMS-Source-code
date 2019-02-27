<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SpecContent.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Content.SpecContent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>专题内容管理</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>内容管理</span> &gt;&gt;<span>按专题管理</span>&gt;&gt;
		<span><asp:Label ID="Label1" runat="server" Text="专题名"></asp:Label></span>
	</div>
	<div class="clearbox"></div>
	<div class="divbox" id="nocontent" runat="server">暂无内容</div>
	<asp:GridView ID="Egv" runat="server" AllowPaging="True" AutoGenerateColumns="False"
       DataKeyNames="SpecInfoID" PageSize="20" OnPageIndexChanging="Egv_PageIndexChanging" OnRowCommand="Lnk_Click" Width="100%">
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
                    <a href="ContentView.aspx?GeneralID=<%# Eval("GeneralID")%>"><%# Eval("Title")%></a>       
                </ItemTemplate>
                <HeaderStyle Width="30%" />
                <ItemStyle CssClass="tdbg" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="录入人"　DataField="Inputer" >
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                <HeaderStyle Width="10%" />
            </asp:BoundField>
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
                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Del" CommandArgument='<%# Eval("SpecInfoID") %>' OnClientClick="return confirm('你确定将该数据从专题中删除吗？')">删除</asp:LinkButton>
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
    <asp:Button ID="Button1" runat="server" Text="从所属专题删除" CssClass="button" OnClick="btnDelete_Click" OnClientClick="if(!IsSelectedId()){alert('请选择内容项');return false;}else{return confirm('你确定要从专题中删除选中内容吗？')}"/>&nbsp;               
    <asp:Button ID="Button2" runat="server" Text="添加到其他专题" OnClick="btnAddTo_Click" CssClass="button" />&nbsp;
    <asp:Button ID="Button3" runat="server" Text="移动到其他专题" OnClick="btnMoveTo_Click" CssClass="button" />
    </form>
</body>
</html>
