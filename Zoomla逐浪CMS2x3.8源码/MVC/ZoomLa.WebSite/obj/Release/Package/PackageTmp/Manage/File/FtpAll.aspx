<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FtpAll.aspx.cs" Inherits="ZoomLaCMS.Manage.FtpFile.FtpAll" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>所有配置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="EGV" CssClass="table table-striped table-bordered table-hover" DataKeyNames="ID" runat="server"
       AutoGenerateColumns="False" AllowPaging="True" PageSize="6" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="Egv_RowDataBound">
    <Columns>
    <asp:TemplateField HeaderText="选中" HeaderStyle-Height="24" ItemStyle-Height="22">
      <ItemTemplate>
        <asp:CheckBox ID="SelectCheckBox" runat="server" />
      </ItemTemplate>
      <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    <asp:BoundField DataField="ID" HeaderText="序号">
      <ItemStyle HorizontalAlign="Center" />
    </asp:BoundField>
    <asp:TemplateField HeaderText="服务器名">
      <ItemTemplate> <%#Eval("Alias")%> </ItemTemplate>
      <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="IP">
      <ItemTemplate> <%#Eval("FtpServer")%> </ItemTemplate>
      <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="端口">
      <ItemTemplate> <%#Eval("FtpPort")%> </ItemTemplate>
      <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="用户名">
      <ItemTemplate> <%#Eval("FtpUsername")%> </ItemTemplate>
      <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="密码">
      <ItemTemplate> <%#Eval("FtpPassword")%> </ItemTemplate>
      <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="超时时间" Visible="false">
      <ItemTemplate> <%#Eval("OutTime")%> </ItemTemplate>
      <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="访问URL">
      <ItemTemplate> <%#Eval("Url")%> </ItemTemplate>
      <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="目录">
      <ItemTemplate>
        <%#Eval("SavePath")%> </ItemTemplate>
      <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="操作">
      <HeaderStyle Width="19%" />
      <ItemTemplate> 
          <a href='FtpConfig.aspx?DSId=<%# Eval("ID")%>' class="option_style"><i class="fa fa-pencil" title="修改"></i></a> 
          <a href="javascript:if(confirm('确定要删除?')) window.location.href='FtpAll.aspx?SId=<%# Eval("ID")%>';" class="option_style"><i class="fa fa-trash-o" title="删除"></i></a> 
          <a href='ShowFtpFile.aspx?FId=<%# Eval("ID")%>' class="option_style"><i class="fa fa-magic" title="管理"></i>文件管理</a>
      </ItemTemplate>
      <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    </Columns>
  </ZL:ExGridView>
  <div class="clearbox"> </div>
  <table  class="table table-striped table-bordered table-hover" id="sleall">
    <tr>
      <td style="height: 24px"><asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" Text="全选" />
        &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btndelete" class="btn btn-primary" runat="server" OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('你确定要将所有选择项删除吗？')}" Text="批量删除" OnClick="btndelete_Click" />
        &nbsp;
        </tr>
  </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>