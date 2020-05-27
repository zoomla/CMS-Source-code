<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendAffairList.aspx.cs" Inherits="ZoomLaCMS.Plat.OA.SendAffairList" MasterPageFile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>公文处理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol class="breadcrumb">
<li><a href="/Plat/Blog/">能力中心</a></li>
<li><a href="/Plat/OA/">OA办公</a></li>
<li><a href="SendAffairList.aspx">发文管理</a><span style="margin-left:10px;">提示：已开始审批的文件无法修改删除</span></li>
</ol>
<div id="site_main" class="container OspcList platcontainer" style="margin-top:43px;padding-top:5px;">
<div class="input-group nav_searchDiv">
  <asp:TextBox runat="server" ID="searchText" class="form-control" placeholder="请输入需要搜索的内容" />
  <span class="input-group-btn">
  <asp:LinkButton runat="server"  style="height:34px;" CssClass="btn btn-default" ID="searchBtn" OnClick="searchBtn_Click"><span style="margin-top:2px;" class="fa fa-search"></span></asp:LinkButton>
  </span> </div>
<div class="tab3" style="margin-top:5px;">
  <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"  EnableTheming="False" 
			CssClass="table table-striped table-bordered table-hover" EmptyDataText="你当前尚未发出任何公文!!" OnPageIndexChanging="EGV_PageIndexChanging" 
			OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound" >
	<Columns>
	<asp:BoundField HeaderText="ID" DataField="ID" />
	<asp:TemplateField HeaderText="发文类型">
	  <ItemTemplate> <%#GetType(Eval("DocType", "{0}")) %> </ItemTemplate>
	</asp:TemplateField>
	<asp:BoundField HeaderText="拟稿部门" DataField="Branch" />
	<asp:BoundField HeaderText="标题" DataField="Title" />
	<asp:TemplateField HeaderText="密级">
	  <ItemTemplate> <%#GetSecret( Eval("Secret","{0}")) %> </ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="紧急程度">
	  <ItemTemplate> <%# GetUrgency(Eval("Urgency","{0}")) %> </ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="重要程度">
	  <ItemTemplate> <%# GetImport(Eval("Importance","{0}")) %> </ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="状态">
	  <ItemTemplate> <%# GetStatus(Eval("Status","{0}")) %> </ItemTemplate>
	</asp:TemplateField>
	<asp:BoundField HeaderText="创建时间" DataField="CreateTime" />
	<asp:TemplateField HeaderText="操作">
	  <ItemTemplate>
		<asp:LinkButton ID="view" CommandArgument='<%#Eval("ID") %>' CommandName="View" runat="server">查看审批详情</asp:LinkButton>
		<asp:LinkButton ID="edit" runat="server" ToolTip="修改" CommandArgument='<%#Eval("ID") %>' CommandName="Edit1">修改</asp:LinkButton>
		<asp:LinkButton ID="del" CommandArgument='<%#Eval("ID") %>' CommandName="del2" runat="server" ToolTip="删除" OnClientClick="return delCon();">删除</asp:LinkButton>
	  </ItemTemplate>
	</asp:TemplateField>
	</Columns>
	<PagerStyle HorizontalAlign="Center"/>
	<RowStyle Height="24px" HorizontalAlign="Center" />
  </ZL:ExGridView>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript">
    $(function () {
        $("#top_nav_ul li[title='办公']").addClass("active");
    })
	function delCon() {
		return confirm('确定该公文移入回收站吗!!!');
	}
	setactive("办公");
</script>
</asp:Content>
