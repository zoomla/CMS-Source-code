<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContentRecycle.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Content.ContentRecycle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>回收站内容</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/SelectCheckBox.js"></script>    
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<span><a href="ContentManage.aspx">内容管理</a></span>&gt;&gt;<span>回收站</span>
	</div>
    <div class="clearbox"></div>
    <asp:GridView ID="Egv" runat="server" AllowPaging="True" AutoGenerateColumns="False"
       DataKeyNames="GeneralID" PageSize="20" OnRowDataBound="Egv_RowDataBound" OnPageIndexChanging="Egv_PageIndexChanging" OnRowCommand="Lnk_Click" Width="100%">
        <Columns>
        　　<asp:TemplateField HeaderText="选择">
                  <ItemTemplate>
                      <asp:CheckBox ID="chkSel" runat="server" />
                  </ItemTemplate>
              <ItemStyle CssClass="tdbg" />
            </asp:TemplateField> 
            <asp:BoundField DataField="GeneralID" HeaderText="ID">
                <HeaderStyle Width="5%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Title" HeaderText="标题">
                <HeaderStyle Width="50%" />
                <ItemStyle CssClass="tdbg" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="所属栏目">
                <HeaderStyle Width="10%" />
                <ItemTemplate>  
                    <%# GetNodeName(Eval("NodeID", "{0}")) %>       
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="内容模型">
                <HeaderStyle Width="10%" />
                <ItemTemplate>  
                    <%# GetModelName(Eval("ModelID", "{0}")) %>       
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="Inputer" HeaderText="输入人">
                <HeaderStyle Width="10%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:BoundField>                       
            <asp:TemplateField HeaderText="操作" >
                <ItemTemplate>                     
                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Del" CommandArgument='<%# Eval("GeneralID") %>' OnClientClick="return confirm('你确定将该数据彻底删除吗？')">删除</asp:LinkButton> | 
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Reset" CommandArgument='<%# Eval("GeneralID") %>' OnClientClick="return confirm('你确定将该数据还原吗？')">还原</asp:LinkButton>
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
    <asp:Button ID="Button1" runat="server" Text="还原选中内容" CssClass="button" OnClick="btnAudit_Click" OnClientClick="if(!IsSelectedId()){alert('请选择内容');return false;}else{return confirm('你确定要还原选中内容吗？')}"/>&nbsp;               
    <asp:Button ID="Button2" runat="server" Text="清除选中内容" OnClick="btnDelete_Click" OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('你确定要将所有选择项彻底删除吗？')}" CssClass="button" UseSubmitBehavior="true" />&nbsp;
    <asp:Button ID="Button3" runat="server" Text="清空回收站" CssClass="button" OnClick="btnDeleteAll_Click" />&nbsp;
    <asp:Button ID="Button4" runat="server" Text="还原所有内容" CssClass="button" OnClick="btnReset_Click" />
    
    </form>
</body>
</html>
