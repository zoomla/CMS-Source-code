<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContentRank.aspx.cs" Inherits="ZoomLaCMS.Manage.Workload.ContentRank" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>综合排行</title>
    <style>
        .padding5{ padding-left:5px; padding-right:5px;}
        .padding10{ padding-left:10px; padding-right:10px;}
        .ranksort{ margin-top:0.5em;}
        .ranksort .nav-tabs li a{ padding:0; width:100px; height:36px; line-height:36px; color:#666; text-align:center;}
        .ranksort .nav-tabs li a:hover,.ranksort .nav-tabs li a:focus{ outline:none; background:none; border-color:rgba(209, 222, 241, 1);}
        .ranksort .nav-tabs .active a,.ranksort .nav-tabs .active a:hover,.ranksort .nav-tabs .active a:focus{ background:#eee; border-color:rgba(209, 222, 241, 1); color:#000;}
        .nodelist{ margin-top:0.5em; margin-left:10px;}
        .nodelist strong{ float:left; display:block; width:60px; height:36px; line-height:36px;}        
        .rankbox_title{ margin:10px; padding-left:1em; height:2.4em; line-height:2.4em; border:1px solid #ddd; background:#eee; border-radius:4px;}         
        .ranktitle{ height:2.2em; line-height:2.2em; border-bottom:1px solid #ddd; background:#f5f5f5; }
        .ranktitle i{ margin-right:5px; font-size:1.6em; color:#999;}
        .ranktitle p{ padding-left:1em; width:120px; border-bottom:2px solid #aaa;}
        .rankitem{ min-height:200px; border:1px solid #ddd;}
        .rankitem ul{ padding:0.5em 0.8em;}
        .rankitem li{ padding-left:0.5em; min-height:2em; line-height:2em; border-bottom:1px dotted #eee;}
        .rankitem li::before{ content:"1"; float:left; font-family:"宋体"; font-style:oblique; font-size:16px; color:#333; display:block; width:20px; margin-right:0.5em; }
        .rankitem li:nth-child(1)::before{ content:"1"; color:#f00; font-size:20px;}
        .rankitem li:nth-child(2)::before{ content:"2"; color:#f00; font-size:20px;}
        .rankitem li:nth-child(3)::before{ content:"3"; color:#f00; font-size:20px;}
        .rankitem li:nth-child(4)::before{ content:"4";}
        .rankitem li:nth-child(5)::before{ content:"5";}
        .rankitem li:nth-child(6)::before{ content:"6";}
        .rankitem li:nth-child(7)::before{ content:"7";}
        .rankitem li:nth-child(8)::before{ content:"8";}
        .rankitem li:nth-child(9)::before{ content:"9";}
        .rankitem li:nth-child(10)::before{ content:"10";}
        .rankitem li em{ float:left; display:block; width:1.2em; font-size:1.2em; color:#666; text-align:center; margin-right:0.5em; font-family:"宋体";}
        .rankitem li:nth-child(1) em,.rankitem li:nth-child(2) em,.rankitem li:nth-child(3) em{ color:#f00; font-size:1.4em;}
        .rankitem li:nth-child(2n){ background:#f8f8f8;}
        .rankitem li:last-child{ border-bottom:none;}
        .rankitem .badge{ margin-right:0.5em; padding:0; height:2em; line-height:2em; border-radius:50%; background:none; color:#666; font-family:"宋体"; font-size:14px;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="ranksort">
        <ul class="nav nav-tabs" role="tablist">
        <li class="active"><a href="ContentRank.aspx">综合排行</a></li>
        <li><a href="Rank.aspx?Type=click">点击排行</a></li>
        <li><a href="Rank.aspx?Type=comment">评论排行</a></li>
        <li><a href="#Export">导出</a></li>
        </ul>
    </div>
    <div class="nodelist">
        <strong>频道：</strong>
        <asp:DropDownList ID="NodeList" runat="server" CssClass="form-control" Width="200" DataTextField="NodeName" DataValueField="NodeId" SelectionMode="Multiple"></asp:DropDownList> 
    <div class="clearfix"></div>       
    </div>
    <div class="rankbox">
        <div class="rankbox_title">所有栏目</div>
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 padding10 rankbox_l">
            <div class="rankitem">
            <div class="ranktitle"><p><i class="fa fa-signal"></i>点击排行</p></div>
            <ul class="list-unstyled">
            <asp:Repeater ID="Hits_RPT" runat="server">
                <ItemTemplate>
                    <li><%#Eval("Title") %><span class="badge pull-right"><%#Eval("Hits") %></span></li>
                </ItemTemplate>
            </asp:Repeater>
            </ul>
            </div>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 padding10 rankbox_m">
            <div class="rankitem">
            <div class="ranktitle"><p><i class="fa fa-signal"></i>评论排行</p></div>
            <ul class="list-unstyled">
            <asp:Repeater ID="Com_RPT" runat="server">
                <ItemTemplate>
                    <li><%#Eval("Title") %><span class="badge pull-right"><%#Eval("ComCount") %></span></li>
                </ItemTemplate>
            </asp:Repeater>
            </ul>
            </div>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 padding10 rankbox_r">
            <div class="rankitem">
            <div class="ranktitle"><p><i class="fa fa-signal"></i>DIGG排行</p></div>
            <ul class="list-unstyled">
            <asp:Repeater ID="Di_RPT" runat="server">
                <ItemTemplate>
                    <li><%#Eval("Title") %><span class="badge pull-right"><%#Eval("GCount") %></span></li>
                </ItemTemplate>
            </asp:Repeater>
            </ul>
            </div>
        </div> 
        <div class="clearfix"></div>
        <asp:Repeater ID="ItemList_RPT" OnItemDataBound="ItemList_RPT_ItemDataBound" runat="server">
        <ItemTemplate>
        <div class="rankbox">
        <div class="rankbox_title"><%#Eval("NodeName") %></div>
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 padding10 rankbox_l">
            <div class="rankitem">
            <div class="ranktitle"><p><i class="fa fa-signal"></i>点击排行</p></div>
            <ul class="list-unstyled">
                <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                <li><%#Eval("Title") %><span class="badge pull-right"><%#Eval("Hits") %></span></li>
                </ItemTemplate>
                </asp:Repeater>
            </ul>
            </div>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 padding10 rankbox_m">
            <div class="rankitem">
            <div class="ranktitle"><p><i class="fa fa-signal"></i>评论排行</p></div>
            <ul class="list-unstyled">
                <asp:Repeater ID="Repeater2" runat="server">
                <ItemTemplate>
                <li><%#Eval("Title") %><span class="badge pull-right"><%#Eval("ComCount") %></span></li>
                </ItemTemplate>
                </asp:Repeater>
            </ul>
            </div>
            <div class="clearfix"></div>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 padding10 rankbox_r">
        <div class="rankitem">
        <div class="ranktitle"><p><i class="fa fa-signal"></i>DIGG排行</p></div>
        <ul class="list-unstyled">
        <asp:Repeater ID="Gi_RPT" runat="server">
            <ItemTemplate>
                <li><%#Eval("Title") %><span class="badge pull-right"><%#Eval("GCount") %></span></li>
            </ItemTemplate>
        </asp:Repeater>
        </ul>
        </div>
        </div>
            <div class="clearfix"></div>
        </ItemTemplate>
    </asp:Repeater>
    </div>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>