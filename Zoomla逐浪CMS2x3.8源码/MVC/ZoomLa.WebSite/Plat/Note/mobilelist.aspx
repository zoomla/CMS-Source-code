<%@ Page Language="C#" MasterPageFile="~/Common/Master/Empty.master" AutoEventWireup="true" CodeBehind="mobilelist.aspx.cs" Inherits="ZoomLaCMS.Plat.Note.mobilelist" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>项目列表</title>
<link href="mobile_note.css" rel="stylesheet" type="text/css" />
<style>
    body{background-color:#f0f0f0;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="prolist_header">
            <div class="row">
                 <div class="col-xs-4 col-sm-4"></div>
                <div class="col-xs-4 col-sm-4 text-center pro_title"><strong>我的项目</strong></div>
                <div class="col-xs-4 col-sm-4 text-right">
                    <a href="mobilenote.aspx">创建</a>
                </div>
            </div>
        </div>
    <div class="container-fluid prolist_body">
        <div class="proitem newpro">
            <div class="newpro_div">
                <i class="fa fa-plus"></i><br />
                新建
            </div>
        </div>
        <asp:Repeater ID="RPT" runat="server" OnItemCommand="RPT_ItemCommand">
            <ItemTemplate>
                <div class="proitem">
                    <div class="pro_content" data-id="<%#Eval("ID") %>">
                        <img src="<%#Eval("topimg") %>" onerror="shownopic(this);" />
                        <div class="gray_div"></div>
                        <div class="pro_item_title">
                            <div><strong><%#Eval("Title") %></strong></div>
                            <div class="pro_item_date"><%#Eval("CDate","{0:yyyy年MM月dd日 HH:mm}") %></div>
                        </div>
                    </div>
                    <div class="pro_option">
                        <a href="mobilenote.aspx?id=<%#Eval("ID") %>" class="pro_option_edit"><i class="fa fa-pencil"></i></a>
                        <asp:LinkButton CommandName="del" CssClass="pro_option_del" CommandArgument='<%#Eval("ID") %>' runat="server"><i class="fa fa-trash"></i></asp:LinkButton>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script>
        var x;
        var touchflag = false;
        $(function () {
            $('.pro_content').on('touchstart', function (e) {
                x = e.originalEvent.targetTouches[0].pageX;
                $('.pro_content').css('left', '0');
                touchflag = true;
            })
            .on('touchmove', function (e) {
                var change = e.originalEvent.targetTouches[0].pageX - x;
                //调节滑动灵敏度(以防垂直滚动时误触发滑动)
                if (change > -30 && change <= 0 && touchflag) { return; }
                if (change < -30 && touchflag) { x = e.originalEvent.targetTouches[0].pageX; touchflag = false; return }
                change = Math.min(change, 0);
                e.currentTarget.style.left = change + 'px';
            })
            .on('touchend', function (e) {
                var left = parseInt(e.currentTarget.style.left)
                var new_left;
                if (left < -35) {
                    new_left = '-100px'
                } else {
                    new_left = '0px'
                }
                $(e.currentTarget).animate({ left: new_left }, 200)
            });
            //新建项目
            $(".newpro").click(function () {
                location = "mobilenote.aspx";
            });
            //预览项目
            $(".pro_content").click(function () {
                location = "mobileview.aspx?id=" + $(this).data('id');
            });
        })
    </script>
</asp:Content>