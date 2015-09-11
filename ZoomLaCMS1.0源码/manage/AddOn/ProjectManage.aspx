<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectManage.aspx.cs" Inherits="manage_AddOn_ProjectManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>项目管理</title>
     <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span> 项目管理</span>	</div> 
                                        <div class="clearbox"></div>  
  <asp:GridView ID="Egv" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="border"
           DataKeyNames="ProjectID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" Width="100%" OnRowCommand="GridView1_RowCommand" EmptyDataText="无任何相关数据">
            <Columns>
                <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                          <asp:CheckBox ID="chkSel" runat="server" />
                      </ItemTemplate>
                      <HeaderStyle Width="4%" />
                      <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:TemplateField> 
                <asp:BoundField DataField="ProjectID" HeaderText="ID" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="10%" />
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="项目名称" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>  
                        <a href="WorkManage.aspx?Pid=<%#Eval("ProjectID")%>"><%#DataBinder.Eval(Container.DataItem, "ProjectName").ToString()%></a>      
                    </ItemTemplate>
                    <HeaderStyle Width="15%" />
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:TemplateField>                                  
               <asp:TemplateField HeaderText="立项数" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="8%" />
                    <ItemTemplate> 
                        <%# CountWork(Convert.ToInt32(Eval("ProjectID")))%>
                    </ItemTemplate>
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
               </asp:TemplateField>
                <asp:TemplateField HeaderText="已完成" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="8%" />
                    <ItemTemplate>  
                     <%# (int)Eval("Status") == 0 ? "<span style=\"color: #ff0033\">×</span>" : "√"%>                
                             
                    </ItemTemplate>
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
               </asp:TemplateField> 
                     <asp:TemplateField HeaderText="进度" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="5%" />
                    <ItemTemplate>  
                            <%# CountRate(Convert.ToInt32(Eval("ProjectID"))) %>
                    </ItemTemplate>
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
               </asp:TemplateField>                   
                  <asp:BoundField DataField="StartDate"  DataFormatString="{0:d}" HtmlEncode="false" HeaderText="开始时间" ItemStyle-HorizontalAlign="Center" >
                    <HeaderStyle Width="10%" />
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:BoundField>     
                  <asp:TemplateField HeaderText="完成时间" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="15%" />
                    <ItemTemplate> 
                         <%# GetProjectEndDate(Convert.ToInt32(Eval("ProjectID")))%>
                    </ItemTemplate>
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
               </asp:TemplateField>     
                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>                        
                        <asp:LinkButton ID="LnkDelete"  runat="server" CommandName="DelProject" OnClientClick="return confirm('确实要删除吗？');"
                            CommandArgument='<%# Eval("ProjectID")%>'>删除</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2"  runat="server" CommandName="ShowWork" 
                            CommandArgument='<%# Eval("ProjectID")%>'>查看进程</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton1"  runat="server" CommandName="AddWork"
                            CommandArgument='<%# Eval("ProjectID")%>'>添加节点</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3"  runat="server" CommandName="ModifyProject"
                            CommandArgument='<%# Eval("ProjectID")%>'>修改</asp:LinkButton>
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
        <asp:CheckBox ID="cbAll" runat="server" AutoPostBack="True" Font-Size="9pt" OnCheckedChanged="cbAll_CheckedChanged"
            Text="全选" />      
        
        <asp:Button ID="btnDel" runat="server" Text="批量删除" OnClick="btnDel_Click" OnClientClick=" return confirm('确实要删除这些选择吗？');"  />
     
            
 
    </form>
</body>
</html>
