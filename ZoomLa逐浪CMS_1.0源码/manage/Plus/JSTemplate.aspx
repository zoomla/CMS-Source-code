<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JSTemplate.aspx.cs" Inherits="ZoomLa.WebSite.Manage.AddOn.JSTemplate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>广告管理</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/Main.css" type="text/css" rel="stylesheet" />
        
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="../JS/RiQi.js" type="text/javascript"></script>    
</head>

<body>
    <form id="form1" runat="server">
     <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>附件管理</span> &gt;&gt;<span><a href="ADZoneManage.aspx" title="广告版位管理">广告版位管理</a></span>&gt;&gt;<span>广告模板管理</span>
	</div>
    <div class="clearbox"></div>      
       
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" Width="100%" OnRowCommand="GridView1_RowCommand" CssClass="border">
        <Columns>
            <asp:BoundField DataField="JSTemplateID" HeaderText="类型ID">
            <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                 </asp:BoundField>    
            <asp:BoundField DataField="JSTemplateName" HeaderText="版块类型名称" >
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:BoundField>    
            <asp:BoundField DataField="JSTemplatePath" HeaderText="版块文件所在路径">
                 <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:BoundField> 
            <asp:BoundField DataField="JSTemplateSize" HeaderText="版块大小">
            <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
             </asp:BoundField>             
            <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>                        
                <asp:LinkButton ID="LinkButton1"  runat="server" CommandName="Modify" CommandArgument='<%# Eval("JSTemplateID")%>'>修改模板</asp:LinkButton> 
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    
    </asp:GridView>
             
       
    
    </form>
</body>
</html>

