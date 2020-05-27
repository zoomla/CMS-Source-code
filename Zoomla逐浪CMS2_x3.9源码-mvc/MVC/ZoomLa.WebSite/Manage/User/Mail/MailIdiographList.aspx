<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailIdiographList.aspx.cs" Inherits="ZoomLaCMS.Manage.User.Mail.MailIdiographList" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>签名列表</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
  <div>
    <ZL:ExGridView ID="EGV" DataKeyNames="ID" Width="100%" runat="server" OnRowCommand="EGV_RowCommand"  class="table table-striped table-bordered table-hover" AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting">
            <Columns>
            <asp:TemplateField>
              <ItemTemplate>
                <input  name="idchk" type="checkbox" value='<%#Eval("ID")  %>' />
              </ItemTemplate>
                <ItemStyle CssClass="td_s" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ID">
              <ItemStyle />
              <ItemTemplate>
                <%# Eval("ID") %>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="签名名称">
              <ItemTemplate> <a href='addMailIdiograph.aspx?ID=<%#Eval("ID") %>'><%#Eval( "Name")%></a> </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="签名内容">
              <ItemTemplate>
                <%# Eval("Context") %>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="添加时间">
              <ItemTemplate>
               <%# Eval("AddTime") %>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="签名状态">
              <ItemTemplate> <%#GetState(Eval("State").ToString())%> </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton CommandName="Del" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('是否确定删除!')" runat="server"><i class="fa fa-trash"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
          </ZL:ExGridView>
  </div>
    <asp:Button ID="Dels_Btn" runat="server" CssClass="btn btn-primary" Text="批量删除" OnClientClick="return confirm('是否确定删除!')" OnClick="Dels_Btn_Click" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript">
    //全选
    $().ready(function () {
       
    });
</script>
</asp:Content>
