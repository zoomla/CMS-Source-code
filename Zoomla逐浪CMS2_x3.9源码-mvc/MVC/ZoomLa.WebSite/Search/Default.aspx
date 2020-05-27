<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Search.Default" MasterPageFile="~/Common/Master/Empty.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>全站搜索_<%=GetName()%></title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="Search_top"><div class="Search_top_overlay"></div></div>
<div class="Search_header">
<div class="container ">
<p class="pull-right">
<a href="">注册会员</a>
<a href="/user/">用户登录</a>
<a href="/help.html">使用帮助</a>
</p>
<h1 class="search_logo"><a href="/"><img src="<%= Call.LogoUrl %>" alt="<%= Call.SiteName %>" class="img-responsive" /></a></h1>
</div>
</div>

<div id="Div1" runat="server" class="Search_headerIndex">
<div class="container">
<div class="row">
<div class="col-lg-12 col-md-12 col-sm-6 col-xs-12 col-lg-offset-4  col-md-offset-2  ">
    <div class="pull-left margin_b1em">
        <select name="ddlnode" class="btn btn-default dropdown-toggle ">
            <option value="-1">全部栏目</option>
            <asp:Literal ID="NodeHtml_Li" runat="server" EnableViewState="false"></asp:Literal>
        </select>
    </div>
        <div class="input-group">
            <asp:TextBox ID="TxtKeyword" runat="server" onclick="setEmpty(this)" onblur="settxt(this)" Text="请输入关键字" CssClass="form-control input-sm"></asp:TextBox>
            <span class="input-group-btn">
            <asp:Button ID="btnSearch" runat="server" Width="50" Text="搜索" OnClick="btnSearch_Click" CssClass="btn btn-default input-sm" />
            </span>
        </div><!-- /input-group -->
        
</div>
</div>
</div>
</div>
<div class="container Search_elit">
<ul>
<li class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
<div class="Search_elitprj">
<a href="/baike" target="_blank"><i class="fa fa-university"></i></a>
<a href="/baike" target="_blank">百科中心</a>
</div>
</li>
<li class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
<div class="Search_elitprj">
<a href="/guest/" target="_blank"><i class="fa fa-volume-up"></i></a>
<a href="/guest/" target="_blank">留言中心</a>
</div>
</li>
<li class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
<div class="Search_elitprj">
<a href="/ask" target="_blank"><i class="fa fa-puzzle-piece"></i></a>
<a href="/ask" target="_blank">问答中心</a>
</div>
</li>
<li class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
<div class="Search_elitprj">
<a href="/index" target="_blank"><i class="fa fa-ship"></i></a>
<a href="/index" target="_blank">论坛</a>
</div>
</li>
<li class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
<div class="Search_elitprj">
<a href="/dai/" target="_blank"><i class="fa fa-thumbs-up"></i></a>
<a href="/dai/" target="_blank">在线试戴</a>
</div>
</li>
<li class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
<div class="Search_elitprj">
<a href="/office" target="_blank"><i class="fa  fa-beer"></i></a>
<a href="/office" target="_blank">OA办公</a>
</div>
</li>
<li class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
<div class="Search_elitprj">
<a href="/plat" target="_blank"><i class="fa  fa-modx"></i></a>
<a href="/plat" target="_blank">能力中心</a>
</div>
</li>
<li class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
<div class="Search_elitprj">
<a href="/Class_2/Default.aspx" target="_blank"><i class="fa fa-shopping-bag"></i></a>
<a href="/Class_2/Default.aspx" target="_blank">线上商城</a>
</div>
</li>
</ul>
</div>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script type="text/javascript">
    function setEmpty(obj) {
        if (obj.value == "请输入关键字") {
            obj.value = "";
        }
    }
    function settxt(obj) {
        if (obj.value == "") {
            obj.value = "请输入关键字";
        }
    }
</script>
</asp:Content>