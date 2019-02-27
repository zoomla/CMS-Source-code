<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResList.aspx.cs" Inherits="ZoomLaCMS.Design.User.ResList" MasterPageFile="~/Design/Master/User.master" ValidateRequest="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<link href="/design/res/css/user.css" rel="stylesheet" />
<title>资源管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="siteinfo container">
<ol class="breadcrumb">
    <li><a href="/User/Default.aspx">会员中心</a></li>
    <li><a href="/Design/User/">动力模块</a></li>
    <li class="active">资源管理</li>
</ol>
<div class="padding10" style="font-size:12px;padding-top:0px;position:relative;top:-10px;">
<div id="bread_div" style="display:none;"><i class="fa fa-list"></i> <asp:Label runat="server" ID="Bread_L" EnableViewState="false"></asp:Label></div>
<div class="colhead"><span class="col40">文件名</span><span class="col20">大小</span><span class="col20">修改日期</span><span class="col20">操作</span></div>
<div class="mainlist" style="height:350px;overflow-y:auto;">
    <asp:Repeater runat="server" ID="RPT" OnItemCommand="RPT_ItemCommand">
        <ItemTemplate>
            <div class="coltr">
                <span class="col40"><%#GetLink()%></span>
                <span class="col20"><%#Eval("ExSize") %></span>
                <span class="col20"><%#Eval("LastWriteTime","{0:yyyy年MM月dd日 HH:mm}") %></span>
                <span class="col20">
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("Name") %>' CommandName="del" OnClientClick="return confirm('确定要删除吗?');"><i class="fa fa-trash-o"></i> 删除</asp:LinkButton>
                    <a href="/Plat/Doc/DownFile.aspx?DType=design&fname=<%#Eval("Name") %>&siteID=<%:SiteID %>" target="_blank"><i class="fa fa-download"></i> 下载</a>
                </span>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
</div>
<a href="javascript:;" id="upfile_btn" class="btn btn-info"><i class="fa fa-upload"></i> 上传文件</a>
<%--<a href="javascript:;" id="newdir_btn" class="btn btn-info"><i class="fa fa-folder-o"></i> 新文件夹</a>--%>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
.breadcrumb {padding-left:0px;}
</style>
<link href="/JS/Controls/ZL_Webup.css" rel="stylesheet" />
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/ICMS/ZL_Common.js"></script>
<script src="/JS/Controls/ZL_Webup.js"></script>
<script>
    $(function () {
        ZL_Webup.config.json.ashx = "action=design%26SiteID=<%:SiteID%>";
$("#upfile_btn").click(ZL_Webup.ShowFileUP);
})
//如何判断是否全部上传完成
function AddAttach(file, ret, pval) {
location = location;
}
</script>
</asp:Content>