<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelTearcher.aspx.cs" MasterPageFile="~/Common/Common.master" Inherits="User_Exam_SelTearcher" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>选择教师</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="input-group text_300">
	<asp:TextBox ID="Search_T" runat="server" CssClass="form-control" placeholder="用户名"></asp:TextBox>
  <span class="input-group-btn">
	  <asp:LinkButton ID="Search_Btn" runat="server" CssClass="btn btn-default" OnClick="Search_Btn_Click"><span class="fa fa-search"></span></asp:LinkButton>
  </span>
</div>
<ZL:ExGridView ID="Egv" runat="server" CssClass="table table-bordered table-striped table-hover margin_t10" AllowPaging="True" AutoGenerateColumns="False" 
	 PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" EmptyDataText="没有相关用户!">
	<Columns>
		<asp:TemplateField>
			<ItemTemplate>
				<input type="checkbox" data-name="<%#Eval("UserName") %>" name="idchk" value="<%#Eval("UserID") %>" />
			</ItemTemplate>
		</asp:TemplateField>
		<asp:BoundField DataField="GroupName" HeaderText="用户组" />
		<asp:BoundField DataField="UserName" HeaderText="用户名" />
		<asp:BoundField DataField="HoneyName" HeaderText="真实姓名" />
	</Columns>
</ZL:ExGridView>
<div class="text-center margin_t10">
	<button type="button" class="btn btn-primary" onclick="CheckTearcher()">确定</button>
	<button type="button" onclick="parent.CloseDiag()" class="btn btn-primary">取消</button>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style>.allchk_l{display:none;}</style>
<script>
	$(function () {
		$("#Egv tr").click(function () {
			$("input[name=idchk]").each(function () {
				$(this)[0].checked = false;
			})
			$(this).find('input[name=idchk]')[0].checked=true;
		});
	});
	function CheckTearcher() {
		if ($("input[name=idchk]:checked")[0]) {
			var chkteach = $("input[name=idchk]:checked");
			parent.GetTearcher(chkteach.val(), chkteach.data('name'));
		} else {
			alert("请选择用户!");
		}
		
	}
</script>
</asp:Content>