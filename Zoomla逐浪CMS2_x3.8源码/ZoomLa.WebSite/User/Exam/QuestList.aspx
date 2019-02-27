<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuestList.aspx.cs" Inherits="User_Exam_QuestList" MasterPageFile="~/User/Default.master" %>
<%@ Register Src="~/Manage/I/ASCX/TreeView.ascx" TagPrefix="ZL" TagName="TreeView" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>试题管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="edu" data-ban="ke"></div>
<div class="container margin_t5">
<ol class="breadcrumb">
	<li><a href="/user">用户中心</a></li>
	<li>试题管理<asp:Label ID="AddQuest_L" runat="server"></asp:Label></li>
</ol>
</div>
<div class="container">
 <div id="nodeNav" class="col-lg-2 col-md-2 col-sm-2 col-xs-12 divBorder" style="padding:0;height:500px;overflow-y:auto;">
	<div>
		<div>
			<ZL:TreeView ID="MyTree" IsShrink="true" NodeName="C_ClassName" NodePid="C_Classid" NodeID="C_id" runat="server" />
			<span style="color:green;" runat="server" id="remind" visible="false" />
		</div>
	</div>
</div>
<div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
	<div class="input-group text_300">
	<asp:TextBox ID="Skey_T" runat="server" CssClass="form-control" placeholder="试题名"></asp:TextBox>
	<span class="input-group-btn">
		<asp:LinkButton ID="Search_B" runat="server" OnClick="Search_B_Click" CssClass="btn btn-default"><i class="fa fa-search"></i></asp:LinkButton>
	</span>
</div>
<ul class="nav nav-tabs margin_t5">
	<li id="tab_99"><a href="javascript:;" onclick="ShowTabs(99)">所有试题</a></li>
	<li id="tab_0"><a href="javascript:;" onclick="ShowTabs(0)">单选题</a></li>
	<li id="tab_1"><a href="javascript:;" onclick="ShowTabs(1)">多选题</a></li>
	<li id="tab_2"><a href="javascript:;" onclick="ShowTabs(2)">填空题</a></li>
	<li id="tab_4"><a href="javascript:;" onclick="ShowTabs(4)">完型填空题</a></li>
	<li id="tab_3"><a href="javascript:;" onclick="ShowTabs(3)">解析题</a></li>
	<li id="tab_10"><a href="javascript:;" onclick="ShowTabs(10)">大题</a></li>
</ul>
<div>
	<ZL:ExGridView ID="EGV" runat="server" DataKeyNames="Tagkey" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False" IsHoldState="false"
		OnRowDataBound="EGV_RowDataBound" OnRowCommand="EGV_RowCommand" class="table table-striped table-bordered table-hover margin_t5" EmptyDataText="暂无试题信息" OnPageIndexChanging="EGV_PageIndexChanging">
		<Columns>
			<asp:TemplateField ItemStyle-Width="50">
				<ItemTemplate>
					<input type="checkbox" name="idchk" value="<%#Eval("p_id") %>" />
				</ItemTemplate>
			</asp:TemplateField>
			<asp:BoundField HeaderText="ID" DataField="p_id" />
			<asp:TemplateField HeaderText="试题标题">
				<ItemTemplate>
					<a href="AddEngLishQuestion.aspx?id=<%#Eval("p_id") %>"><%# GetTitle()%></a>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:BoundField DataField="p_Inputer" HeaderText="作者" ItemStyle-VerticalAlign="Middle" />
			<asp:TemplateField HeaderText="难度" ItemStyle-VerticalAlign="Middle">
				<ItemTemplate>
					<%#questBll.GetDiffStr(Convert.ToDouble(Eval("p_Difficulty"))) %>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="类别" ItemStyle-VerticalAlign="Middle">
				<ItemTemplate>
					<%#GetClass(Eval("p_Class","{0}"))%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="题型" ItemStyle-VerticalAlign="Middle">
				<ItemTemplate>
					<%#GetType(Eval("p_Type", "{0}"))%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="知识点">
				<ItemTemplate>
					<%#GetTagKeys() %>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:BoundField DataField="p_CreateTime" HeaderText="创建时间"/>
			<asp:TemplateField HeaderText="相关操作" ItemStyle-VerticalAlign="Middle">
				<ItemTemplate>
					<asp:Label ID="prebtn" runat="server"></asp:Label>
					<asp:LinkButton runat="server" ID="editbtn" CommandName="edit2" CommandArgument='<%#Eval("p_id") %>' Visible="false" ToolTip="修改"><span class="fa fa-pencil"></span></asp:LinkButton>
					<asp:LinkButton runat="server" ID="delbtn" CommandName="del2" CommandArgument='<%#Eval("p_id") %>' OnClientClick="return confirm('确定要删除吗?');" Visible="false" ToolTip="删除"><span class="fa fa-trash-o"></span></asp:LinkButton>
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</ZL:ExGridView>
</div>
</div>

</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
function ShowTabs(qtype) {
	var url = "QuestList.aspx?qtype=" + qtype + "&skey=<%:Skey%>";
	location = url;
}
$(function () {
	$("#tab_<%:QType%>").addClass("active");
})
</script>
</asp:Content>