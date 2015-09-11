<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ADManage.aspx.cs" Inherits="ZoomLa.WebSite.Manage.AddOn.ADManage" %>
<%@ Import Namespace="ZoomLa.BLL" %>
<%@ Import Namespace="System.Data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>广告管理</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />        
    <script language="javascript" type="text/javascript" src="../js/SelectCheckBox.js"></script>  
</head>

<body>
    <form id="form1" runat="server">
     <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>附件管理</span> &gt;&gt;<span>广告管理</span>
		</div> 
        <div class="clearbox"></div>
        <div class="divbox" id="nocontent" runat="server">暂无广告版位信息</div>     
        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="ADID" AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="Lnk_Click" CssClass="border" >
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSel" runat="server" />
                    </ItemTemplate>
                    <ItemStyle CssClass="tdbg" Width="5%" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="ADID" HeaderText="序号" >
                    <ItemStyle CssClass="tdbg" Width="5%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="预览">                   
                    <ItemTemplate>
                       <a href="PreviewAD.aspx?ADId=<%#Eval("ADId")%>">预览</a>
                    </ItemTemplate>
                     <ItemStyle  CssClass="tdbg" Width="5%" HorizontalAlign="Center" />
                </asp:TemplateField>                          
                <asp:TemplateField HeaderText="广告名称">
                   <ItemTemplate>
                        <a href="Advertisement.aspx?ADId=<%#Eval("ADId")%>"><%# Eval("ADName")%></a>
                    </ItemTemplate>                    
                      <ItemStyle  CssClass="tdbg" Width="20%" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="类型">
                    <HeaderStyle Width="5%" />
                    <ItemTemplate>
                        <%# GetADType(Eval("ADType","{0}")) %>
                    </ItemTemplate>
                    <ItemStyle  CssClass="tdbg" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="priority" HeaderText="权重" >
                    <ItemStyle  CssClass="tdbg" Width="5%" HorizontalAlign="Center" />
                </asp:BoundField> 
                <asp:TemplateField HeaderText="点击数">
                    <HeaderStyle Width="7%" />
                    <ItemTemplate>
                        <%#(bool)(Eval("countclick"))?Eval("clicks"):"不统计"%>
                    </ItemTemplate>
                     <ItemStyle  CssClass="tdbg" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="浏览数">
                    <ItemTemplate>
                        <%#(bool)(Eval("countview"))?Eval("views"):"不统计"%> 
                    </ItemTemplate>
                    <ItemStyle  CssClass="tdbg" Width="7%" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="剩余天数">
                    <ItemTemplate>
                      <%#( (DateTime)(Eval("OverdueDate")) - DateTime.Now).Days%>
                    </ItemTemplate>
                    <ItemStyle  CssClass="tdbg" Width="10%" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="已审核">
                    <HeaderStyle Width="7%" />
                    <ItemTemplate>
                       <%# (bool)Eval("Passed") == false ? "<span style=\"color: #ff0033\">×</span>" : "√"%>                
                    </ItemTemplate>   
                    <ItemStyle  CssClass="tdbg" HorizontalAlign="Center" />                                  
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">                    
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ADID") %>'>修改</asp:LinkButton> | 
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Copy" CommandArgument='<%# Eval("ADID") %>'>复制</asp:LinkButton> | 
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Del" OnClientClick="return confirm('确定要删除此广告吗？');" CommandArgument='<%# Eval("ADID") %>'>删除</asp:LinkButton> | 
                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Pass" CommandArgument='<%# Eval("ADID") %>'><%# (bool)Eval("Passed") == false ? "通过审核" : "取消审核"%></asp:LinkButton> 
                    </ItemTemplate>
                     <ItemStyle  CssClass="tdbg" HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <RowStyle ForeColor="Black" BackColor="#DEDFDE" Height="25px" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <PagerStyle CssClass="tdbg" ForeColor="Black" HorizontalAlign="Center" />
            <HeaderStyle CssClass="tdbg" Font-Bold="True" ForeColor="#E7E7FF" BorderStyle="None" Height="30px" Font-Overline="False" />
            <PagerSettings FirstPageText="第一页" LastPageText="最后页" Mode="NextPreviousFirstLast" NextPageText="下一页" PreviousPageText="上一页" />
        </asp:GridView>
        <div class="clearbox"></div>
        <asp:CheckBox ID="CheckSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="CheckSelectAll_CheckedChanged" Text="全选" />&nbsp;
        <asp:Button ID="btndelete" runat="server" OnClientClick="if(!IsSelectedId()){alert('请选择广告');return false;}else{return confirm('你确定要删除选中的广告吗？')}" Text="删除选定广告" OnClick="btndelete_Click" />&nbsp;
        <asp:Button ID="btnsetpassed" runat="server" Text="审核通过选定广告" OnClick="btnsetpassed_Click" />&nbsp;
        <asp:Button ID="btncancelpassed" runat="server" Text="取消审核选定广告" OnClick="btncancelpassed_Click" />
    </form>
</body>
</html>
