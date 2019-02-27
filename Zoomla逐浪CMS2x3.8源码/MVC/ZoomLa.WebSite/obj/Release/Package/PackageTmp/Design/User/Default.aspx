<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Design.User.Default" MasterPageFile="~/Design/Master/User.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>控制中心</title>
<link href="/Plugins/Third/alert/sweetalert.css" rel="stylesheet" />
<script src="/Plugins/Third/alert/sweetalert.min.js?v=1"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="siteinfo container">
<ol class="breadcrumb">
<li><a href="/Design/">站点创作</a></li>
<li><a href="<%=Request.RawUrl %>">控制中心</a></li>
<li class="active">列表</li>
</ol>
<div class="info_head">
<div class="info_head_img">
<a runat="server" id="domain_a2" href="#" target="_blank">
<img src="#" runat="server" id="TlpView_img" onerror="javascript:this.src='/UploadFiles/timg.jpg';" /></a>
</div>
<div class="pull-left margin_l20">
<div>
<asp:Label runat="server" ID="SiteName_L"></asp:Label>
<a runat="server" id="domain_a" href="#" class="btn btn-default" style="color: #555; text-decoration: none;" title="点击访问" target="_blank">你尚未申请域名</a>
<a runat="server" id="sitecfg_a" href="#" class="btn btn-info"><i class="fa fa-cogs"></i>站点配置</a>
<div class="btn-group">
<button type="button" class="btn btn-info" onClick="ChangeSite();"><i class="fa fa-list"></i>切换站点</button>
<ul class="dropdown-menu" role="menu" hidden>
<asp:Repeater runat="server" ID="Site_RPT">
<ItemTemplate>
<li><a href="Default.aspx?ID=<%#Eval("ID") %>"><%#Eval("SiteName") %></a></li>
</ItemTemplate>
</asp:Repeater>
</ul>
</div>
<a href="Content/List.aspx?SiteID=<%:SiteID %>" class="btn btn-primary"><i class="fa fa-pencil"></i>内容管理</a>
<a href="#" class="btn btn-primary"><i class="fa fa-user-plus"></i>用户升级</a>
<asp:LinkButton runat="server" ID="DownSite_Btn" class="btn btn-primary" OnClick="DownSite_Btn_Click" OnClientClick="return downSite();"><i class="fa fa-download"></i> 全站下载</asp:LinkButton>
</div>
</div>
<div class="clearfix"></div>
</div>
<div class="row">
<div class="col-lg-8 col-md-8 col-sm-8 col-xs-12">
<!-- Nav tabs -->
<ul class="nav nav-tabs" role="tablist">
<li role="presentation" class="active"><a href="#home" aria-controls="home" role="tab" data-toggle="tab">站点管理</a></li>
<li role="presentation"><a href="#profile" aria-controls="profile" role="tab" data-toggle="tab">组件管理</a></li>
<li class="pull-right"><a href="/design/"><i class="fa fa-eye"></i> 浏览</a></li>
<li class="pull-right"><a href="/design/"><i class="fa fa-pencil"></i> 设计</a></li>
</ul>
<!-- Tab panes -->
<div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="home">
<ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="5" IsHoldState="false" OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="你还没有创建页面">
<Columns>
<asp:BoundField HeaderText="标题" DataField="Title" />
<asp:BoundField HeaderText="路径" DataField="Path" />
<asp:BoundField HeaderText="创建时间" DataField="CDate" DataFormatString="{0:yyyy年MM月dd日 hh:mm}" />
<asp:TemplateField HeaderText="操作">
<ItemTemplate>
<a class="option_style" href="/Design/PreView.aspx?ID=<%#Eval("Guid") %>" target="_blank"><i class="fa fa-eye"></i>预览</a>
<a class="option_style" href="PageInfo.aspx?ID=<%#Eval("Guid") %>"><i class="fa fa-cog"></i>配置</a>
<a class="option_style" href="/Design/?ID=<%#Eval("Guid") %>" target="_blank"><i class="fa fa-paint-brush"></i>设计</a>
<asp:LinkButton runat="server" CssClass="option_style" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗');"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
</Columns>
</ZL:ExGridView>
</div>
<div role="tabpanel" class="tab-pane" id="profile">
<table class="table table-bordered table-striped">
<tr>
<td>别名</td>
<td>创建时间</td>
<td>操作</td>
</tr>
<asp:Repeater runat="server" ID="Global_RPT" OnItemCommand="Global_RPT_ItemCommand">
<ItemTemplate>
<tr>
<td><%#Eval("Title","").TrimStart('/') %></td>
<td><%#Eval("CDate","{0:yyyy年MM月dd日 HH:mm}") %></td>
<td>
<a class="option_style" href="/Design/PreView.aspx?ID=<%#Eval("Guid") %>" target="_blank" ><i class="fa fa-eye"></i>预览</a>
<a class="option_style" href="PageInfo.aspx?ID=<%#Eval("Guid") %>"><i class="fa fa-cog"></i>配置</a>
<a class="option_style" href="/Design/?ID=<%#Eval("Guid") %>" target="_blank"><i class="fa fa-paint-brush"></i>设计</a>
<asp:LinkButton runat="server" CssClass="option_style" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗');"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
</td>
</tr>
</ItemTemplate>
</asp:Repeater>
</table>
</div>
</div>
<div class="panel panel-default">
<div class="panel-heading">
    <i class="fa fa-tasks"></i> H5场景 
    <a href="/design/h5/" target="_blank" class="btn btn-sm btn-info"><i class="fa fa-pencil"></i> 前往设计</a>
    <a href="PubList.aspx" class="btn btn-sm btn-info"><i class="fa fa-clone" ></i> 互动信息</a>
</div>
<div class="panel-body" style="min-height: 150px;">
<table class="table table-bordered table-striped">
<tr>
<td></td>
<td>名称</td>
<td>创建时间</td>
<td>操作</td>
</tr>
<ZL:ExRepeater ID="Scence_RPT" runat="server" BoxType="dp" PageSize="10"  PagePre="<tr><td colspan='12'><div class='text-center'>" PageEnd="</div></td></tr>" OnItemCommand="Scence_RPT_ItemCommand">
<ItemTemplate>
<tr ondblclick="location.href='/design/h5/?id=<%#Eval("Guid") %>';">
<td><input type="checkbox" name="h5_idchk" value="<%#Eval("id") %>" /></td>
<td><a href="/h5/<%#Eval("ID") %>" target="_blank"><%#Eval("Title","").TrimStart('/') %></a></td>
<td><%#Eval("CDate","{0:yyyy年MM月dd日 HH:mm}") %></td>
<td>
<a class="option_style" href="/Design/h5/preview.aspx?id=<%#Eval("Guid") %>" target="_blank" ><i class="fa fa-eye"></i>预览</a>
<a class="option_style" href="/design/h5/?ID=<%#Eval("Guid") %>" target="_blank"><i class="fa fa-paint-brush"></i>设计</a>
<a class="option_style" href="PubList.aspx?H5ID=<%#Eval("Guid") %>" ><i class="fa fa-clone" ></i>互动</a>
<asp:LinkButton runat="server" CssClass="option_style" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗');"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
</td>
</tr>
</ItemTemplate>
<FooterTemplate>
    <div class="clearfix"></div>
</FooterTemplate>
</ZL:ExRepeater>
</table>
<asp:Button runat="server" ID="H5_BatDel_Btn" Text="批量移除" OnClick="H5_BatDel_Btn_Click" OnClientClick="return confirm('确定要移除吗?');" CssClass="btn btn-info" />
</div>
</div>
<div class="panel panel-default">
<div class="panel-heading"><i class="fa fa-question-circle-o"></i> 问卷之星 <a href="/design/ask/default.aspx#/tab/ask_add" target="_blank" class="btn btn-sm btn-info"><i class="fa fa-plus"></i> 新建问卷</a></div>
<div class="panel-body">
<table class="table table-bordered table-striped">
<tr>
<td>问卷名称</td>
<td>创建时间</td>
<td>到期时间</td>
<td>操作</td>
</tr>
<ZL:ExRepeater ID="Ask_RPT" runat="server" BoxType="dp" PageSize="10"  PagePre="<tr><td colspan='12'><div class='text-center'>" PageEnd="</div></td></tr>" OnItemCommand="Scence_RPT_ItemCommand">
<ItemTemplate>
<tr ondblclick="location.href='/design/ask/AskResult.aspx?id=<%#Eval("ID") %>';" title="双击查看问卷结果">
<td><a href="/design/ask/default.aspx#/tab/ask_qlist/21" title="<%#Eval("Title") %>"><%#Eval("Title") %></a></td>
<td><%#Eval("CDate","{0:yyyy-MM-dd HH:mm}") %></td>
<td><%#Eval("EndDate","{0:yyyy-MM-dd HH:mm}") %></td>
<td>
<a href="/design/ask/default.aspx#/tab/ask_view/<%#Eval("ID") %> " target="_blank"><i class="fa fa-eye"></i>预览</a>
<a href="/design/ask/default.aspx#/tab/ask_qlist/<%#Eval("ID") %>"><i class="fa fa-edit"></i> 编辑</a>
<a href="javascript:;" onclick="DelAskFun('<%#Eval("ID") %>');"><i class="fa fa-trash"></i> 删除</a>
</td>
<td></td>
</tr>
</ItemTemplate>
<FooterTemplate>
<div class="clearfix"></div>
</FooterTemplate>
</ZL:ExRepeater>
</table>
</div>
</div>
</div>
<div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
<div class="panel panel-default" style="width: 321px;">
<div class="panel-heading"><i class="fa fa-th-large"></i>快捷菜单</div>
<div class="panel-body padding0">
<ul class="list-unstyled" id="shortcut_ul">
<li onclick="location='/Design/?SiteID=<%:SiteID %>';"><i class="fa fa-paint-brush"></i>修改站点</li>
<li onclick="location='Content/List.aspx?SiteID=<%:SiteID %>'"><i class="fa fa-gift"></i>内容管理</li>
<li style="border-right: none;" onclick="location='ResList.aspx?SiteID=<%:SiteID %>';"><i class="fa fa-desktop"></i>资源管理</li>
<li onclick="location='Node/NodeList.aspx?SiteID=<%:SiteID %>';"><i class="fa fa-list-ol"></i>节点管理</li>
<li><i class="fa fa-eye"></i>站点信息</li>
<li style="border-right: none;"><i class="fa fa-lightbulb-o"></i>查看帮助</li>
</ul>
</div>
<div class="panel-footer text-right" style="border-top: none;"><a href="#">显示更多</a></div>
</div>
</div>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/ICMS/ZL_Common.js"></script>
<script>
function downSite() {
	if (!confirm('确定要下载全站,以进行私有化布署吗?')) { return false; }
	comdiag.ShowMask("正在打包数据,请等待");
	return true;
}
function ChangeSite()
{
	swal({ title: "站点切换", "text": "免费版只能创建一个站点，点击购买权限。", type: "info", showCancelButton: true, confirmButtonColor: "#DD6B55", confirmButtonText: "购买权限", closeOnConfirm: false }, function () { window.location.href="/Class_13/Default.aspx" });
}
function DelAskFun(qid) {
    var api = "/design/ask/server/ask.ashx?action="
    var r = confirm("数据不可恢复，确定要删除吗？");
    if (r == true) {
        $.post(api + "del", { "id": qid }, function (data) {
            data = APIResult.getModel(data);
            if (APIResult.isok(data)) {
                alert("删除成功！");
                window.location.href = window.location.href;
            }
        })
    }
}
</script>
</asp:Content>