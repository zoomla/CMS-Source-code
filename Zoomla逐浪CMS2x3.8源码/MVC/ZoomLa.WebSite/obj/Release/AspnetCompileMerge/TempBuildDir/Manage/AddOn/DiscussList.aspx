<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeBehind="DiscussList.aspx.cs" Inherits="ZoomLaCMS.Manage.AddOn.DiscussList" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>讨论管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
 <table class="table table-bordered table-responsive table-hover">
    <tr align="center">
        <td colspan="2" class="spacingtitle">
            <asp:Label ID="Label1" runat="server" Text="项目内容讨论列表" Font-Bold="True"></asp:Label></td>
    </tr>        
    </table>                                                    
<ZL:ExGridView ID="Egv" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
       DataKeyNames="DiscussID" ShowHeader="false" PageSize="5" OnPageIndexChanging="Egv_PageIndexChanging" Width="100%" OnRowCommand="GridView1_RowCommand" EmptyDataText="无相关讨论内容">
        <Columns>
            <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                      <asp:CheckBox ID="chkSel" runat="server" />
                  </ItemTemplate>               
                  <ItemStyle CssClass="tdbg" HorizontalAlign="Center" Width="5%"/>
            </asp:TemplateField> 
            <asp:BoundField DataField="DiscussID" HeaderText="ID" ItemStyle-HorizontalAlign="Center">
                     <ItemStyle CssClass="tdbg" HorizontalAlign="Center" Width="5%" />
            </asp:BoundField>
            
           <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>  
                    <%#DataBinder.Eval(Container.DataItem, "DiscussDate").ToString()%> 
                    <br />
                   <%#DataBinder.Eval(Container.DataItem, "Content").ToString()%> 
                </ItemTemplate>
                
                <ItemStyle CssClass="tdbg" HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作"  ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>                        
                    <asp:LinkButton ID="LnkDelete"  runat="server" CommandName="DeleteDiscuss" OnClientClick="if(!this.disabled) return confirm('确实要删除吗？');"
                        CommandArgument='<%# Eval("DiscussID")%>'>删除</asp:LinkButton>  
                  </ItemTemplate>
                  
                  <ItemStyle CssClass="tdbg" HorizontalAlign="Center"  Width="10%"/>                    
            </asp:TemplateField>
        　</Columns>
         <RowStyle ForeColor="Black" BackColor="#DEDFDE" Height="25px" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <PagerStyle CssClass="tdbg" ForeColor="Black" HorizontalAlign="Center" />
        <HeaderStyle CssClass="tdbg" Font-Bold="True" ForeColor="#E7E7FF" BorderStyle="None" Height="30px" Font-Overline="False" />
        <PagerSettings FirstPageText="第一页" LastPageText="最后页" Mode="NextPreviousFirstLast" NextPageText="下一页" PreviousPageText="上一页" />
    </ZL:ExGridView>
      <div class="clearbox"></div>                    
    <asp:CheckBox ID="cbAll" runat="server" AutoPostBack="True" Font-Size="9pt" OnCheckedChanged="cbAll_CheckedChanged"  Text="全选" />      
    <asp:Button ID="btnDel" runat="server" Text="批量删除" OnClick="btnDel_Click" class="btn btn-primary" OnClientClick="return confirm('确实要删除吗？');"/>
   <div class="clearbox"></div>
   <table class="table table-bordered table-responsive table-hover">
    <tr align="center">
        <td colspan="2" class="spacingtitle">
            <asp:Label ID="Label2" runat="server" Text="发布讨论" Font-Bold="True"></asp:Label></td>
    </tr>
      <tr>
        <td class="tdbgleft" align="right" style="width: 105px">
            <strong>用户名：&nbsp;</strong></td>
        <td class="tdbg" align="left">
            <asp:TextBox ID="TxtUserName" runat="server" ReadOnly="true" class="form-control text_300"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="tdbgleft" align="right" style="width: 105px">
            <strong>讨论内容：&nbsp;</strong></td>
        <td class="tdbg" align="left">
            <asp:TextBox ID="TxtDisContent" runat="server" TextMode="MultiLine" Rows="8" 
                Columns="50" class="form-control text_300" Height="105px" Width="334px"></asp:TextBox>
            </td>
    </tr>      
    <tr class="tdbgbottom">
        <td colspan="2">              
            <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" class="btn btn-primary"/>&nbsp;&nbsp;
            <input name="Cancel" type="button" id="Cancel" value="取消" onclick="javascript:window.location.href='ProjectManage.aspx'" class="btn btn-primary"/>
        </td>
    </tr>
</table>
</asp:Content>



