<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserRole.aspx.cs" Inherits="Plat_Admin_UserRole" MasterPageFile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>用户权限</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<%--<ol class="breadcrumb">
<li><a href="/Plat/Blog/">能力中心</a></li>
<li><a href="/Plat/Admin/">管理中心</a></li>
<li>角色管理 [<a href="AddUserRole.aspx" style="color:red;">添加角色</a>]</li>
</ol>--%>
<div class="container platcontainer" style="padding-bottom:5px;">
    
    <div class="child_head"> <span class="child_head_span1" style="margin-bottom:-3px;"></span> <span class="child_head_span2">角色管理</span><a href="AddUserRole.aspx" style="float:right;"><i class="fa fa-plus"></i>添加角色</a></div>
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False"
        CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" OnRowCommand="EGV_RowCommand" OnRowDataBoundDataRow="EGV_RowDataBoundDataRow">
    <Columns>
    <asp:TemplateField>
      <ItemTemplate>
        <input type="checkbox" name="idChk" title="选择" value='<%#Eval("ID") %>' />
      </ItemTemplate>
      <ItemStyle  CssClass="tdbg" HorizontalAlign="Center" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="ID">
      <ItemTemplate> <%# Eval("ID")%> </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="角色名称">
      <ItemTemplate> <a href="RoleAuth.aspx?ID=<%#Eval("ID") %>" target="_blank"> <%# Eval("RoleName")%></a> </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="创建时间">
      <ItemTemplate> <%# Eval("CreateTime")%> </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="备注">
      <ItemTemplate> <%# Eval("RoleDesc")%> </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="操作">
      <ItemTemplate> <a href="AddUserRole.aspx?ID=<%#Eval("ID") %>"><span class="fa fa-pencil"  style="color:#7D98A1;"></span></a>
        <asp:LinkButton ID="DelBtn" runat="server" CommandName="Del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('你确定要删除吗!');" ToolTip="删除"><i class="fa fa-trash-o" style="font-size:1.3em;color:#7D98A1;"></i></asp:LinkButton>
      </ItemTemplate>
    </asp:TemplateField>
    </Columns>
  </ZL:ExGridView>
  <asp:Button runat="server" Text="批量删除" OnClientClick="return confirm('你确定要删除选中项吗!');" ToolTip="批量删除" CssClass="btn btn-primary" ID="BatDel_Btn" OnClick="BatDel_Btn_Click"/>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style>
#EGV { margin-top: 10px; }
</style>
<script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
<script type="text/javascript">
    $(function () {
        $("#top_nav_ul li[title='管理']").addClass("active");
        $("#EGV tr>th:eq(0)").html("<input type=checkbox id='chkAll'/>");
        $("#chkAll").click(function () {
            selectAllByName(this, "idChk");
        });
    });
</script>
</asp:Content>
