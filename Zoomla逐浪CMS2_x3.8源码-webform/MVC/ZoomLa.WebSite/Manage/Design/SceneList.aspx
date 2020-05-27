<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SceneList.aspx.cs" Inherits="ZoomLaCMS.Manage.Design.SceneList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link href="/dist/css/star-rating.min.css" rel="stylesheet" />
    <script src="/dist/js/star-rating.min.js"></script>
    <style>
    .Template_box{height:21em;}
    .title{float:left;width:100%;overflow: hidden; text-overflow: ellipsis; white-space: nowrap;margin-top:3px;padding-left:0.5em;padding-right:34px; }
    .cdate{float:right;font-size:12px;}
    .cdate a{margin-left:5px;}
    .truename { padding-left: 5px; width: 120px;}
    .Template_box .rating-xs{font-size:0.7em;display:inline-block;}
    .Template_box .caption{display:none;}
    .Template_box .temp_pre{font-size:16px;margin:0 2px;}
    .popover{max-width:200px;width:181px;}
    .width34{width:34px;}
    .viscon{position:absolute;right:2px;}
    </style>
    <title>场景列表</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol id="BreadNav" class="breadcrumb navbar-fixed-top">
    <li><a href='<%=CustomerPageAction.customPath2 %>Main.aspx'><%=Resources.L.工作台 %></a></li>
    <li><a href='Default.aspx'>动力模块</a></li>
    <li><a href='SceneList.aspx'>场景列表</a><span id="reclink" runat="server"><a href="SceneList.aspx?status=-2" >[回收站]</a></span></li>
    <li id="recli" runat="server" visible="false" class="active">回收站</li>
    <div id="help" class="pull-right text-center"><a href="javascript::" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a></div>
    <div id="sel_box" runat="server" class="padding5">
        <div style="display: inline-block; width: 230px;">
            <div class="input-group" style="position: relative; margin-bottom: -12px;">
                <asp:TextBox ID="Skey_T" placeholder="场景名称或创建人" runat="server" CssClass="form-control text_md" />
                <span class="input-group-btn">
                    <asp:Button ID="Search_B" runat="server" Text="<%$Resources:L,搜索 %>" class="btn btn-primary" OnClick="Search_B_Click" />
                </span>
            </div>
        </div>
    </div>
</ol>
<div id="template" runat="server" class="template">
    <div class="panel panel-default" style="padding-top:8px;">
        <div class="panel-body padding0">
            <ul class="list-unstyled">
                <ZL:ExRepeater runat="server" ID="RPT" BoxType="dp" PageSize="20" PagePre="</div><div class='panel-footer text-center'>" PageEnd="</div>" OnItemCommand="RPT_ItemCommand">
                    <ItemTemplate>
                        <li class="padding5">
                            <div class="Template_box">
                                <div class="tempthumil">
                                    <a href="<%#Status ==-1 ? "javascript:;":"EditScene.aspx?id="+Eval("ID") %>" title="点击编辑">
                                        <img onmouseover="this.style.border='1px solid #9ac7f0';" onmouseout="this.style.border='1px solid #eeeeee';" alt="<%=Status ==-1 ? "":"点击进入模板管理" %>" onerror="shownopic(this);" style="width: 100%;" src="<%#Eval("PreviewImg") %>"></a>
                                </div>
                                <div>
                                    <input value="<%#Eval("Score") %>" type="number" disabled class="rating" min=0 max=5 step=0.5 data-size="xs">
                                    <span class="cdate" title="创建时间"><%#Eval("CDate","{0:yyyy/M/d HH:mm}") %><a class="temp_pre" href='/design/h5/preview.aspx?id=<%#Eval("Guid") %>' title="浏览场景" target="_blank"><i class="fa fa-globe"></i></a></span>
                                </div>
                                <span class="title"><strong  title="<%#Eval("Title") %>"><%--[<%#GetStatus() %>]--%><%#Eval("Title") %></strong><a href='Addon/VisitCount.aspx?infoid=<%#Eval("ID") %>' title="访问数量" class="viscon"><%#GetVisitCount() %></a></span>

                                <span <%=Status ==-1 ? "hidden":"" %>>
                                    <ul class="list_unstyled">
                                        <li class="width34"><%#Eval("ID") %> </li>
                                        <li class="truename" style="text-align:left;" title="<%#Eval("TrueName") %>" ><%#Eval("UName") %></li>
                                        <li class="temp_del">
                                            <a class="precode" href="javascript:;" data-guid='<%#Eval("Guid") %>' target="_blank"><i class="fa fa-qrcode"></i></a>
                                        </li>
                                        <li class="temp_del">
                                            <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del" OnClientClick="return confirm('你确定要删除吗');"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                        </li>
                                    </ul>
                                </span>
                                <span <%=Status ==-2 ? "":"hidden" %>>
                                    <ul class="list_unstyled">
                                        <li class="width34"><%#Eval("ID") %> </li>
                                        <li class="truename" style="text-align:left;"  title="<%#Eval("TrueName") %>" ><%#Eval("UName") %></li>
                                        <li class="temp_del">
                                            <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="rec" OnClientClick="return confirm('确定要恢复这条数据吗？');"><i class="fa fa-recycle"></i></asp:LinkButton>
                                        </li>
                                        <li class="temp_del">
                                            <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del2" OnClientClick="return confirm('数据删除后不可恢复，确定要删除吗？');"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                        </li>
                                    </ul>
                                </span>
                            </div>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        <div class="clearfix"></div>
                    </FooterTemplate>
                </ZL:ExRepeater>
            </ul>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<link href="/Plugins/JqueryUI/LightBox/css/lightbox.css" rel="stylesheet" media="screen" />
<script src="/Plugins/JqueryUI/LightBox/jquery.lightbox.js"></script>
    <script>
        $(".lightbox").lightbox({
            fitToScreen: true,
            imageClickClose: false
        });
        $(".rating").rating('refresh', {
            showClear: false
        });
        $("#sel_btn").click(function (e) {
            if ($("#sel_box").css("display") == "none") {
                $(this).addClass("active");
                $("#sel_box").slideDown(300);
                $(".template").css("margin-top","44px");
            }
            else {
                $(this).removeClass("active");
                $("#sel_box").slideUp(200);
                $(".template").css("margin-top", "0px");
            }
        });
        $(function () { pop.bindEvent($(".precode")); })
        var pop = { guid:'' };
        pop.bindEvent = function ($items) {
            $items.popover({
                animation: true, placement: 'top', title: "扫描二维码浏览", content: function () {
                    return '<div><img src="/common/common.ashx?url=<%=ZoomLa.Components.SiteConfig.SiteInfo.SiteUrl+"/design/h5/preview.aspx?id="%>' + pop.guid + '" style="width:150px;height:150px;"></div>';
                }, html: true, trigger: 'manual',
            }).hover(function () {
                var $uinfo = $(this);
                pop.guid = $uinfo.data("guid");
                $uinfo.popover('show');
            }, function () {
               $(this).popover('hide');
            })
        }
    </script>
</asp:Content>

