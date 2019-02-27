<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DownServerManage.aspx.cs" Inherits="manage_Plus_DownServerManage" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>下载服务器管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  
                CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" 
                OnPageIndexChanging="EGV_PageIndexChanging"  OnRowDataBound="Egv_RowDataBound" >
    
    <Columns>
    <asp:TemplateField HeaderText="选中" HeaderStyle-Height="24" ItemStyle-Height="22">
      <ItemTemplate>
        <asp:CheckBox ID="SelectCheckBox" runat="server" />
      </ItemTemplate>
      <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    <asp:BoundField DataField="ServerID" HeaderText="序号">
      <ItemStyle HorizontalAlign="Center" />
    </asp:BoundField>
    <asp:TemplateField HeaderText="服务器名">
      <ItemTemplate> <%# DataBinder.Eval(Container.DataItem, "ServerName")%> </ItemTemplate>
      <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="服务器LOGO">
      <HeaderStyle />
      <ItemTemplate> <%#Getico(Eval("ServerLogo","{0}"))%> </ItemTemplate>
      <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="显示方式">
      <ItemTemplate> <%#Convert.ToInt32(Eval("ShowType"))==0?"显示名称":"显示LOGo"%> </ItemTemplate>
      <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="服务器地址">
      <ItemTemplate> <%#Eval("ServerUrl")%> </ItemTemplate>
      <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="操作">
      <HeaderStyle Width="19%" />
      <ItemTemplate> <a href='DownServer.aspx?Action=Modify&DSId=<%# Eval("ServerID")%>' class="option_style"><i class="fa fa-pencil" title="修改"></i></a> 
          <a href="javascript:if(confirm('确定要删除?')) window.location.href='DownServerManage.aspx?SId=<%# Eval("ServerID")%>';" class="option_style"><i class="fa fa-trash-o" title="删除"></i> 删除</a> </ItemTemplate>
      <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    </Columns>
   <PagerStyle HorizontalAlign="Center" />
    <RowStyle Height="24px" HorizontalAlign="Center"  />
</ZL:ExGridView>
  <div class="clearbox"> </div>
  <table  class="table table-striped table-bordered table-hover" id="sleall">
    <tr>
      <td style="height: 24px"><asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged"  Text="全选" />
        &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btndelete" class="btn btn-primary" runat="server" OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('你确定要将所有选择项删除吗？')}" Text="批量删除" OnClick="btndelete_Click" />
        &nbsp; </td>
    </tr>
  </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>