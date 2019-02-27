<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditApply.aspx.cs" Inherits="Manage_Plat_AuditApply" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>申请管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div>
    <ul id="menuul" class="nav nav-tabs">
    <li data-s="0"><a href="AuditApply.aspx">未审核</a></li>
    <li data-s="99"><a href="AuditApply.aspx?s=99">已同意</a></li>
    <li data-s="-1"><a href="AuditApply.aspx?s=-1">已拒绝</a></li>
</ul>
<ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False" IsHoldState="false"
		class="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!"
		OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
  <Columns>
  <asp:TemplateField ItemStyle-CssClass="td_s"><ItemTemplate><input type="checkbox" name="idchk" value='<%# Eval("ID") %>' /></ItemTemplate></asp:TemplateField>
  <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="UserID" ItemStyle-CssClass="td_s"></asp:BoundField>
   <asp:BoundField HeaderText="公司名称" DataField="CompName" />
   <asp:TemplateField HeaderText="会员名"><ItemTemplate><a href="javascript:;" onclick="showuinfo('<%#Eval("UserID") %>');"><%#Eval("UserName","{0}") %></a> </ItemTemplate></asp:TemplateField>
   <asp:BoundField HeaderText="手机" DataField="Mobile"/>
   <asp:BoundField HeaderText="邮箱" DataField="Email" />
   <asp:TemplateField HeaderText="IP定位"><ItemTemplate><%#ZoomLa.BLL.Helper.IPScaner.IPLocation(Eval("IP","")) %></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="申请时间">
	<ItemTemplate> <%#Eval("CDate","{0:yyyy年MM月dd日 HH:mm}") %> </ItemTemplate>
  </asp:TemplateField>
   <asp:BoundField HeaderText="备注" DataField="UserRemind" />
  </Columns>
</ZL:ExGridView>
<asp:Button runat="server" ID="BatAgree_Btn"  Text="批量同意" OnClick="BatAgree_Btn_Click"  CssClass="btn btn-info" OnClientClick="return subchk('确定要同意吗?');" />
<asp:Button runat="server" ID="BatReject_Btn" Text="批量拒绝" OnClick="BatReject_Btn_Click" CssClass="btn btn-info" OnClientClick="return subchk('确定要拒绝吗?');" />
<div class="alert alert-info margin_t10">审核通过后,会自动为用户创建公司与部门,并以Email的方式通知对方</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
    $(function () {
        var s = "<%=ZStatus%>";
        var $li = $("#menuul li[data-s='" + s + "']");
        if ($li.length > 0) { $li.addClass("active"); }
        else { $("#menuul li:first").addClass("active"); }
    })
    function subchk(msg) {
        if ($("input[name='idchk']:checked").length < 1) { alert("请先选定要操作的数据"); return false; }
        if (!confirm(msg)) { return false; }
        return true;
    }
    function showuinfo(uid) { ShowComDiag("../User/UserInfo.aspx?id="+uid,"用户信息"); }
</script>
</asp:Content>