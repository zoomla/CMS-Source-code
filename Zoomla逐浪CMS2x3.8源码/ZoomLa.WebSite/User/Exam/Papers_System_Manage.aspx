<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Papers_System_Manage.aspx.cs" Inherits="manage_Question_Papers_System_Manage" EnableViewStateMac="false" MasterPageFile="~/User/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>系统试卷管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="edu" data-ban="ke"></div>
<div class="container margin_t5">
	<ol class="breadcrumb">
		<li><a href="/user">用户中心</a></li>
		<li class="active">组卷管理 [<a href='Add_Papers_System.aspx?menu=Add'>添加试卷</a>]</li>
	</ol>
</div>
<div class="container">
	<ZL:ExGridView ID="EGV" runat="server" AllowPaging="true" AutoGenerateColumns="False" AllowSorting="true"
		CssClass="table table-striped table-bordered table-hover" EnableTheming="False"
		DataKeyNames="id" OnPageIndexChanging="EGV_PageIndexChanging">
		<Columns>
			<asp:TemplateField ItemStyle-Width="50">
				<ItemTemplate>
					<input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="试卷标题">
				<ItemTemplate>
					<i class="fa fa-paragraph"></i> <%#Eval("p_name") %>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="试卷分类" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
				<ItemTemplate>
					<%#GetClass() %>
				</ItemTemplate>
				<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="试卷类型" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
				<ItemTemplate>
					<%#GetStyle(Eval("p_style", "{0}"))%>
				</ItemTemplate>
				<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
				<ItemTemplate>
					<a class="option_style" href="/User/Questions/ExamDetail.aspx?ID=<%#Eval("id") %>" target="_blank"><i class="fa fa-clipboard"></i>参加考试</a>
					<a class="option_style" href="Paper_QuestionManage.aspx?pid=<%#Eval("id") %>"><i class="fa fa-magic">题目管理</i></a>
					<a class="option_style" href="Add_Papers_System.aspx?menu=Edit&id=<%#Eval("id") %>" title="修改"><i class="fa fa-pencil"></i></a>
					<a class="option_style" href="DownPaper.aspx?ID=<%#Eval("ID") %>" target="_blank" title="下载试卷"><i class="fa fa-download"></i></a>
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</ZL:ExGridView>
	<div class="clearbox">
	</div>
	<asp:Button ID="BtnDelete" runat="server" class="btn btn-primary" OnClientClick="return confirm('确定删除?')" Text="批量删除" OnClick="BtnDelete_Click" />
	<asp:Button runat="server" ID="Combine_Btn" CssClass="btn btn-primary" Text="合并试卷" OnClick="Combine_Btn_Click" />
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
</asp:Content>