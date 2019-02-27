<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectManage.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="manage_AddOn_ProjectManage" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>项目管理</title>
<script type="text/javascript">
 function CheckKeyword()
 {
   if(document.form1.SearchValue.value=="")
    {
           alert("请输入要搜索的关键字！");
           form1.SearchValue.focus();
           return false
     }
 }    
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="clearbox"></div> 
<div class="divbox" id="nocontent" runat="server" visible="false">无相关数据</div> 
<ZL:ExGridView ID="Egv" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="border" DataKeyNames="ProjectID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" Width="100%" OnRowCommand="GridView1_RowCommand" EmptyDataText="无任何相关数据" OnRowDataBound="Egv_RowDataBound">
    <Columns>
        <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:CheckBox ID="chkSel" runat="server" />
              </ItemTemplate>
              <HeaderStyle Width="4%" />
              <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:BoundField DataField="ProjectID" HeaderText="ID" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle Width="5%" />
            <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="项目名称" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>  
                <%#DataBinder.Eval(Container.DataItem, "ProjectName").ToString()%>      
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
       <asp:TemplateField HeaderText="已审核" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle Width="8%" />
            <ItemTemplate>  
             <%# (bool)Eval("Passed") ==false ? "<span style=\"color: #ff0033\">×</span>" : "√"%>                
                     
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
            <HeaderStyle Width="8%" />
            <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
        </asp:BoundField>     
          <asp:TemplateField HeaderText="完成时间" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle Width="8%" />
            <ItemTemplate> 
                 <%#GetProjectEndDate2(Eval("EndDate").ToString())%>
            </ItemTemplate>
            <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
       </asp:TemplateField> 
       <asp:TemplateField HeaderText="剩余天数" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle Width="8%" />
            <ItemTemplate> 
                 <%# GetSurplusDays(Convert.ToInt32(Eval("ProjectID")))%>
            </ItemTemplate>
            <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
       </asp:TemplateField>    
        <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>                        
                <asp:LinkButton ID="LnkDelete"  runat="server" CommandName="DelProject" OnClientClick="return confirm('确实要删除吗？');"
                    CommandArgument='<%# Eval("ProjectID")%>'>删除</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2"  runat="server" CommandName="ShowWork" 
                    CommandArgument='<%# Eval("ProjectID")%>' Enabled='<%# (bool)Eval("Passed")%>'>查看进程</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton1"  runat="server" CommandName="AddWork"
                    CommandArgument='<%# Eval("ProjectID")%>' Enabled='<%# (bool)Eval("Passed")%>'>添加节点</asp:LinkButton>
                    
                    <asp:LinkButton ID="LinkButton3"  runat="server" CommandName="ModifyProject"
                    CommandArgument='<%# Eval("ProjectID")%>'>修改</asp:LinkButton><br />
                     <a href="ProjectManage.aspx?Action=<%# (bool)Eval("Passed") == false ? "Passed" : "CancelPassed"%>&PId=<%#Eval("ProjectID")%>">
                                <%# (bool)Eval("Passed") == false ? "通过审核" : "取消审核"%>
                                </a>
                       <asp:LinkButton ID="LinkButton4"  runat="server" CommandName="AddColumn" 
                    CommandArgument='<%# Eval("ProjectID")%>' Enabled='<%# (bool)Eval("Passed")%>'>添加字段</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton5"  runat="server" CommandName="ModifyPermissions" 
                    CommandArgument='<%# Eval("ProjectID")%>' Enabled='<%# (bool)Eval("Passed")%>'>修改权限</asp:LinkButton>
                </ItemTemplate>
              <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
        </asp:TemplateField>
    　</Columns>
     <RowStyle ForeColor="Black" BackColor="#DEDFDE" Height="25px" />
    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
    <PagerStyle CssClass="tdbg" ForeColor="Black" HorizontalAlign="Center" />
    <HeaderStyle CssClass="tdbg" Font-Bold="True" ForeColor="#E7E7FF" BorderStyle="None" Height="30px" Font-Overline="False" />
</ZL:ExGridView>
<div class="clearbox"></div>                      
<asp:Button ID="btnDel" runat="server" Text="批量删除" OnClick="btnDel_Click"  OnClientClick="return confirm('确实要删除选中的信息吗？');" class="btn btn-primary"/>
<asp:DropDownList ID="DLType" runat="server">
<asp:ListItem Value="0" Text="按名称" Selected="True"></asp:ListItem>
<asp:ListItem Value="1" Text="按时间"></asp:ListItem>
<asp:ListItem Value="2" Text="按ID"></asp:ListItem>        
<asp:ListItem Value="3" Text="按描述"></asp:ListItem>
<asp:ListItem Value="4" Text="按客户名称"></asp:ListItem>        
</asp:DropDownList> <asp:TextBox runat="server" class="form-control" ID="SearchValue" Text="关键字"></asp:TextBox><asp:Button ID="Search" runat="server" Text="搜索"  OnClientClick="return CheckKeyword();" OnClick="Search_Click" class="btn btn-primary"/>
</asp:Content>
