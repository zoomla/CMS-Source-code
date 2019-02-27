<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAudit.aspx.cs" Inherits="ZoomLaCMS.Plat.Admin.UserAudit"  MasterPageFile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>用户审核</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container platcontainer">
<div class="child_head"><span class="child_head_span1"></span> <span class="child_head_span2">用户审核</span></div>
    <ul id="menuul" class="nav nav-tabs">
    <li data-s="0"><a href="UserAudit.aspx">未审核</a></li>
    <li data-s="99"><a href="UserAudit.aspx?s=99">已同意</a></li>
    <li data-s="-1"><a href="UserAudit.aspx?s=-1">已拒绝</a></li>
</ul>
<ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False" IsHoldState="false"
		class="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" OnRowDataBound="EGV_RowDataBound"
		OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
  <Columns>
  <asp:TemplateField ItemStyle-CssClass="td_s"><ItemTemplate><input type="checkbox" name="idchk" value='<%# Eval("ID") %>' /></ItemTemplate></asp:TemplateField>
  <asp:BoundField DataField="UserID" HeaderText="ID" SortExpression="UserID" ItemStyle-CssClass="td_s"></asp:BoundField>
    <asp:TemplateField HeaderText="头像">
        <ItemTemplate>
            <img class="img_s" src="<%#Eval("UserFace") %>" onerror="shownoface(this);" />
        </ItemTemplate>
    </asp:TemplateField>
  <asp:TemplateField HeaderText="会员名"><ItemTemplate> <%#Eval("UserName","{0}") %> </ItemTemplate></asp:TemplateField>
  <asp:BoundField HeaderText="手机" DataField="Mobile"/>
    <asp:TemplateField HeaderText="IP定位"><ItemTemplate><%#ZoomLa.BLL.Helper.IPScaner.IPLocation(Eval("IP","")) %></ItemTemplate></asp:TemplateField>
  <asp:TemplateField HeaderText="申请时间">
	<ItemTemplate> <%#Eval("CDate","{0:yyyy年MM月dd日 HH:mm}") %> </ItemTemplate>
  </asp:TemplateField>
  <asp:TemplateField HeaderText="操作">
	<ItemTemplate>
        <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="agree" OnClientClick="return confirm('确定要同意其加入公司吗?');">同意</asp:LinkButton>
        <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="reject" OnClientClick="return confirm('确定要拒绝申请吗?');">拒绝</asp:LinkButton>
    </ItemTemplate>
  </asp:TemplateField>
  </Columns>
</ZL:ExGridView>
<asp:Button runat="server" ID="BatAgree_Btn"  Text="批量同意" OnClick="BatAgree_Btn_Click"  CssClass="btn btn-info" OnClientClick="return subchk('确定要同意吗?');" />
<asp:Button runat="server" ID="BatReject_Btn" Text="批量拒绝" OnClick="BatReject_Btn_Click" CssClass="btn btn-info" OnClientClick="return subchk('确定要拒绝吗?');" />
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
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
</script>
</asp:Content>