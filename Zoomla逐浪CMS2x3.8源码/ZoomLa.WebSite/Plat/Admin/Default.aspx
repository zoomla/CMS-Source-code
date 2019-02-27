<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Plat_Admin_Default" MasterPageFile="~/Plat/Main.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<style type="text/css">
.oaul { margin-top: 5px; }
.oaul li { float: left; display: inline-block; cursor: pointer; list-style-type: none; margin-right: 10px; }
.app{padding:5px;}
.cdiv { height:100%; text-align:center; padding-top:35%; }
.cdiv span { color: white; font-size: 1.5em; }
</style>
<title>管理中心</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container platcontainer">
    <div class="child_head"><span class="child_head_span1"></span> <span class="child_head_span2">平台管理</span></div>
    <div class="col-md-2 col-sm-4 col-xs-6 app"> 
        <div style="background: #FE7906;" class="cdiv" onclick="location='UserManage.aspx';">
            <span class="fa fa-folder-open" style="margin-right: 10px;"></span><br /> <span>用户管理</span>
        </div>
    </div>
    <div class="col-md-2 col-sm-4 col-xs-6 app">
         <div style="background: #b572dd;" class="cdiv" onclick="location='UserAudit.aspx';">
            <span class="fa fa-user" style="margin-right: 10px;"></span><br /> <span>用户审核</span>
        </div>
    </div>
    <div class="col-md-2 col-sm-4 col-xs-6 app"> 
        <div style="background: #004B9B;" title="角色权限" class="cdiv" onclick="location='UserRole.aspx';">
            <span class="fa fa-file" style="margin-right: 10px;"></span><br /> <span>角色权限</span>
        </div>
    </div>
    <div class="col-md-2 col-sm-4 col-xs-6 app"> 
        <div style="background: #74B512;" class="cdiv" title="公司信息" onclick="location='/Plat/Group/CompDetail.aspx';">
            <span class="fa fa-folder" style="margin-right: 10px;"></span><br /> <span>公司信息</span>
        </div>
    </div>
    <div class="col-md-2 col-sm-4 col-xs-6 app"> 
        <div style="background: #A43AE3;" class="cdiv" title="部门管理" onclick="location='/Plat/Admin/GroupAdmin.aspx';">
            <span class="fa fa-book" style="margin-right: 10px;"></span><br /> <span>部门管理</span>
        </div>
    </div>
    <div class="col-md-2 col-sm-4 col-xs-6 app"> 
        <div style="background: #22AFC2;" class="cdiv" title="文库管理" onclick="location='/Plat/Doc/Default.aspx'">
            <span class="fa fa-hdd-o" style="margin-right: 10px;"></span><br /> <span>文库管理</span>
        </div>
    </div>
</div>
<script>
    $(function () {
        $("#top_nav_ul li[title='管理']").addClass("active");
        $(".app").height($(".app").width());
        window.onresize = function () {
            $(".app").height($(".app").width());
        }
    });
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    
</asp:Content>