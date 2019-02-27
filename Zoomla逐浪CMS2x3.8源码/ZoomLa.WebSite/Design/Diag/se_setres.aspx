<%@ Page Language="C#" AutoEventWireup="true" CodeFile="se_setres.aspx.cs" Inherits="Design_Diag_se_setres" MasterPageFile="~/Common/Master/Empty.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <link href="/Design/res/css/se_design.css" rel="stylesheet" />
    <style>
    body{background-color:#fff;}
    .respane .mpane #style_ul li{width:auto;margin-right:10px;}
    #op_ul li{cursor:pointer;}
    </style>
    <title>资源选择</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="respane">
    <div class="lpane">
        <ul class="list-unstyled" id="op_ul">
            <li class="active">形状库</li>
            <li class="type_shape" onclick="location.href = 'se_setres.aspx?type=shape&pageid=<%:Request["pageid"]%>';">图形</li>
            <li class="type_text" onclick="location.href = 'se_setres.aspx?type=text&pageid=<%:Request["pageid"]%>';">文字</li>
            <li class="type_icon" onclick="location.href = 'se_setres.aspx?type=icon&pageid=<%:Request["pageid"]%>';">图标</li>
            <li class="type_myimg" onclick="location.href = 'se_setres.aspx?type=myimg&pageid=<%:Request["pageid"]%>';" >我的图片</li>
        </ul>
    </div>
    <div id="res_div" runat="server" class="mpane">
        <div class="header">
            <ul runat="server" class="list-unstyled" id="style_ul"></ul>
        </div>
        <div class="main">
            <div runat="server" id="empty_div" class="alert alert-info margin_t10" visible="false">没有匹配的资源数据</div>
            <ul class="list-unstyled img_ul">
                <asp:Repeater runat="server" ID="RPT" EnableViewState="false">
                    <ItemTemplate>
                        <li class="img_li" title="<%#Eval("name") %>" onclick="add('<%#Eval("VPath") %>');">
                            <img src="<%#Eval("VPath") %>" />
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div class="clearfix"></div>
            <div>
                <asp:Literal runat="server" ID="Page_Lit" EnableViewState="false"></asp:Literal>
            </div>
        </div>
    </div>
    <div id="myimg_div" runat="server" visible="false" class="mpane">
        <div class="main">
             <div runat="server" id="emptyimg" class="alert alert-info margin_t10" visible="false">您还没有上传过图片</div>
             <ul class="list-unstyled img_ul">
                <asp:Repeater runat="server" ID="Myimg_RPT">
                    <ItemTemplate>
                        <li class="img_li" title="<%#Eval("name") %>" onclick="add('<%#Eval("Path") %>')">
                            <img src="<%#Eval("Path") %>" />
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div class="clearfix"></div>
            <div>
                <asp:Literal runat="server" ID="ImgPage_Lit" EnableViewState="false"></asp:Literal>
            </div>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script>
        $(function () {
            var type = '<%:Request.QueryString["type"] %>';
            if(type==""){type="shape";}
            $(".type_"+type).addClass("active");
        })
        function add(vpath) {
            var comp_img = { "dataMod": { "src": vpath }, "config": { "type": "image", "compid": "image", "css": "candrag", "style": "", "imgstyle": "width:300px;height:300px;" } };
            top.AddComponent(comp_img);

        }
        function getto(style) {
            location.href = "se_setres.aspx?style=" + style + "&pageid=<%:Request["pageid"]%>&type=<%:Request.QueryString["type"] %>";
        }
    </script>
</asp:Content>
