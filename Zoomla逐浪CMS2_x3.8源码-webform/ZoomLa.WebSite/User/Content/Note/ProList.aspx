<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProList.aspx.cs" MasterPageFile="~/Plat/Main.master" Inherits="User_Content_Note_ProjectPre" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
<title>项目列表</title>
<link rel="stylesheet" type="text/css" href="/user/content/note/note.css" />
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="server">
<div class="container platcontainer">
<div class="child_head"><span class="child_head_span1"></span> <span class="child_head_span2">项目管理</span></div>
<div class="margin-top note_pro_list">
<ul class="list-unstyled prolist_ul">
<ZL:ExRepeater ID="ProList_RPT" runat="server" OnItemCommand="ProList_RPT_ItemCommand" PageSize="7" PagePre="<div class='clearfix'></div><div class='text-center margin_t5'>" PageEnd="</div>">
	<ItemTemplate>
		<li class="pre_li col-lg-3 col-md-3 col-sm-6 col-xs-12">
			<div class="pro_item" onclick="window.open('<%#GetViewUrl() %>')" title="查看项目" data-id="<%#Eval("ID") %>">
				<img src="<%#Eval("topimg") %>" onerror="shownopic(this);" />
				<div class="pro_tips">名称：<%#Eval("Title") %></div>
				<div class="pro_tips">时间：<%#Eval("CDate","{0:yyyy年MM月dd日 HH:mm}") %></div>
				<div class="pro_tips">用户：<%#Eval("CUName") %></div>
			</div>
			<div class="pro_tools">
				<a href="<%#GetViewUrl() %>" target="_blank"><i class="fa fa-eye"></i> 查看</a>
				<a href="<%#GetNoteUrl(Convert.ToInt32(Eval("ID"))) %>" target="_blank" title="编辑项目"><i class="fa fa-magic"></i> 编辑</a>
				<asp:LinkButton ID="Del_Link" CommandName="Del" OnClientClick="return confirm('是否确定删除!')" CommandArgument='<%#Eval("ID") %>' runat="server" ToolTip="删除"><i class="fa fa-trash"></i> 删除</asp:LinkButton>
			</div>
		</li>
	</ItemTemplate>
	<FooterTemplate></FooterTemplate>
</ZL:ExRepeater>
</ul>
</div>
<div class="alert alert-info" id="empty_div" runat="server" visible="false">
无项目数据
</div>
</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">
<script>
$(function () {
	var listflag = 0;
	$(".note_pro_list .pro_item").click(function () {
		//window.open("view.aspx?id=" + $(this).data('id'), "_blank");
	});
	$(".note_pro_list .pre_li").hover(function () {
		var obj = this;
		$(obj).find(".pro_tools").css('top', '0');
		$(obj).popover({ content: $(obj).find('.pro_detail').html(), placement: "bottom", html: true });
		$(obj).popover('show');
	}, function () {
		$(this).popover('hide');
		$(this).find(".pro_tools").css('top', '-30px');
		clearTimeout(listflag);
	});
	var addli = '<li class="pre_li col-lg-3 col-md-3 col-sm-6 col-xs-12"><div class="pro_item text-center" onclick="window.open(\'note.aspx\');" style="height:310px;" title="添加项目"><i style="font-size:254px;line-height:310px;color:#64bbfa;" class="fa fa-plus-square-o"></i></div></li>';
	$li= $(".prolist_ul li:last");
	if ($li.length > 0) { $li.after(addli); }
	else { $(".prolist_ul").append(addli); }
});
setactive("项目");
</script>
</asp:Content>