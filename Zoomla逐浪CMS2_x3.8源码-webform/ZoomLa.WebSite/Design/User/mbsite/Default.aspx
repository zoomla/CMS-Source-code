<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Design_User_mbsite_Default" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>我的微站</title>
<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0;" name="viewport" />
<link type="text/css" rel="stylesheet" href="/dist/css/weui.min.css" />
<link href="/Template/PowerZ/style/global.css?Version=20150910" rel="stylesheet" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="page-group" >
    <div id="page_list" class="page page-current">
        <header class="bar bar-nav">
            <a href="/design/mobile/welcome.aspx" class="icon icon-left pull-left"></a>
            <h1 class="title">我的微站</h1>
        </header>
        <div class="content native-scroll">
            <div class="list-block cards-list">
                <ul>
                    <asp:Repeater runat="server" ID="RPT" OnItemCommand="RPT_ItemCommand">
                        <ItemTemplate>
                            <li class="card list_item">
                                <div class="card-header">
                                    <span><i class="fa fa-mobile"></i><%#Eval("SiteName") %></span>
                                    <asp:LinkButton runat="server" CommandName="del" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除该站点吗?');" class="button button-fill button-danger">删除</asp:LinkButton>
                                </div>
                                <div class="card-content" onclick="location='/design/mobile/default.aspx?id=<%#Eval("ID") %>';">
                                    <div class="card-content-inner">
                                        <img src="<%#GetImg() %>" style="max-width: 100%;"/>
                                    </div>
                                </div>
                                <div class="card-footer text-cen">
                                    <a href="/design/mobile/default.aspx?id=<%#Eval("ID") %>" class="btn btn-info"><i class="fa fa-paint-brush"></i> 设计</a>
                                    <a href="/design/mobile/addsite.aspx?id=<%#Eval("ID") %>" class="btn btn-info"><i class="fa fa-cog"></i> 配置</a>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div class="row"><a href="/design/mobile/welcome.aspx" class="button button-big button-fill button-danger">新建微站</a></div>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<link href="/design/JS/sui/css/sm.css" rel="stylesheet" />
<script src="/design/h5/js/zepto.min.js"></script>
<script src="/design/JS/sui/js/sm.min.js"></script>
</asp:Content>