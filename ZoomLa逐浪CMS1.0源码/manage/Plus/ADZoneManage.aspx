<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ADZoneManage.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Plus.ADZoneManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>广告版位管理</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/SelectCheckBox.js"></script>
</head>
<body>
    <form id="form1" runat="server">
      <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>附件管理</span> &gt;&gt;广告版位管理
	  </div> 
      <div class="clearbox"></div>
      <div class="line">根据广告版位名搜索：<asp:TextBox ID="TxtADName" runat="server">关键字</asp:TextBox><asp:Button ID="BntSearch" runat="server" Text="查询" OnClick="BntSearch_Click" /></div>
      <div class="clearbox"></div>
      <div class="divbox" id="nocontent" runat="server">暂无广告版位信息</div> 
      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ZoneID" Width="100%" AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="Lnk_Click" CssClass="border">
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSel" runat="server" />
                    </ItemTemplate>
                    <ItemStyle CssClass="tdbg" Width="5%" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="序号" DataField="zoneid">
                <ItemStyle CssClass="tdbg" Width="5%" HorizontalAlign="Center" />
                </asp:BoundField> 
                <asp:TemplateField HeaderText="版位名称">
                <ItemTemplate>
                    <asp:HyperLink ID="LnkZoneName" NavigateUrl='<%# Eval("ZoneId", "ADManage.aspx?ZoneId={0}") %>'
                        runat="server"><%# Eval("ZoneName")%></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" Width="20%" HorizontalAlign="Center" />
                
                </asp:TemplateField>
                <asp:TemplateField HeaderText="类型">
                 <HeaderStyle Width="10%" />
                <ItemTemplate>
                   <%#getzonetypename(DataBinder.Eval(Container.DataItem, "ZoneType").ToString())%>
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="显示类型">
                 <HeaderStyle Width="10%" />
                <ItemTemplate>
                <%#getzoneshowtypename(DataBinder.Eval(Container.DataItem, "ShowType").ToString())%>   
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="尺寸">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%#Eval("ZoneWidth")%>
                    x
                    <%#Eval("ZoneHeight")%>
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="活动">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <%#((bool)DataBinder.Eval(Container.DataItem, "Active")) ? "活动" : "暂停"%>
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                <HeaderStyle />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="AddAdv" CommandArgument='<%# Eval("ZoneID") %>'>添加</asp:LinkButton> | 
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ZoneID") %>'>修改</asp:LinkButton> | 
                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Copy" CommandArgument='<%# Eval("ZoneID") %>'>复制</asp:LinkButton><br />
                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Del" OnClientClick="return confirm('确定要删除此版位吗？');" CommandArgument='<%# Eval("ZoneID") %>'>删除</asp:LinkButton> | 
                    <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Clear" OnClientClick="return confirm('确定要清空此版位吗？');" CommandArgument='<%# Eval("ZoneID") %>'>清空</asp:LinkButton> | 
                    <asp:LinkButton ID="LinkButton6" runat="server" CommandName="SetAct" CommandArgument='<%# Eval("ZoneID") %>'><%# (bool)Eval("Active") == false ? "活动" : "暂停"%></asp:LinkButton>                             
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="版位JS">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton7" runat="server" CommandName="PreView" CommandArgument='<%# Eval("ZoneID") %>'>预览</asp:LinkButton> | 
                    <asp:LinkButton ID="LinkButton9" runat="server" CommandName="Refresh" CommandArgument='<%# Eval("ZoneID") %>'>刷新</asp:LinkButton><br />
                    <asp:LinkButton ID="LinkButton8" runat="server" CommandName="JS" CommandArgument='<%# Eval("ZoneID") %>'>JS调用代码</asp:LinkButton>                    
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
        <div class="clearbox"></div>        
        <asp:CheckBox ID="CheckSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="CheckSelectAll_CheckedChanged" Text="全选" />
        <asp:Button ID="BtnDelete" runat="server" Text="批量删除选定版位" Width="134px" OnClientClick="if(!IsSelectedId()){alert('请选择版位');return false;}else{return confirm('你确定要删除选中的版位吗？')}" OnClick="BtnDelete_Click" />
        <asp:Button ID="BtnActive" runat="server" Text="激活版位" OnClientClick="if(!IsSelectedId()){alert('请选择版位');return false;}else{return confirm('你确定要激活选中的版位吗？')}" OnClick="BtnActive_Click" />
        <asp:Button ID="BtnPause" runat="server" Text="暂停版位" OnClientClick="if(!IsSelectedId()){alert('请选择版位');return false;}else{return confirm('你确定要暂停选中版位吗？')}" OnClick="BtnPause_Click" />
        <asp:Button ID="BtnRefurbish" runat="server" Text="刷新版位JS" OnClick="BtnRefurbish_Click" />
    </form>
</body>
</html>
