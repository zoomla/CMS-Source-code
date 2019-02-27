<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TieList.aspx.cs" Inherits="ZoomLaCMS.Manage.Guest.TieList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content ContentPlaceHolderID="head" Runat="Server">
    <title>留言列表</title>
</asp:Content>
<asp:Content  ContentPlaceHolderID="Content" Runat="Server">
    <ul class="nav nav-tabs hidden-xs hidden-sm">
	<li class="active"><a href="#tab-10" data-toggle="tab" onclick="ShowTabs(-10)"><%:lang.LF("所有贴子") %></a></li>
	<li><a href="#tab0" data-toggle="tab" onclick="ShowTabs(0)"><%:lang.LF("待审核") %></a></li>
	<li><a href="#tab99" data-toggle="tab" onclick="ShowTabs(99)"><%:lang.LF("已审核") %></a></li>
</ul>
    <div class="panel panel-default" style="padding:0px;">
        <div class="panel panel-body" style="padding:0px; margin:0px;">
            <ZL:ExGridView ID="Egv1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="20" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Lnk_Click" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无相关数据">
                <Columns>
                    <asp:TemplateField HeaderText="选择" HeaderStyle-CssClass="egv_chktd">
                        <ItemTemplate>
                            <input type="checkbox" name="idchk" title="" value='<%#Eval("ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="egv_chktd"></asp:BoundField>
                    <asp:TemplateField HeaderText="标题" HeaderStyle-CssClass="egv_title_mid">
                        <ItemTemplate>
                            [<a href="TieList.aspx?CateID=<%#Eval("CateID")%>"><%#Eval("CateName") %></a>]&nbsp <a style="<%#Eval("Style") %>" href="GuestTieShow.aspx?CateID=<%# Eval("CateID")%>&GID=<%# Eval("ID")%>"><%# Eval("Title")%></a>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态">
                        <ItemTemplate>
                            <%#GetStatus() %>
                        </ItemTemplate>
                        <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                        <HeaderStyle Width="6%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="会员名">
                        <ItemTemplate>
                            <a href="javascript:;" onclick="ShowUserInfo(<%# Eval("CUser") %>)" data-toggle="modal" data-target="#userinfo_div" title="点击查看该用户详情"><%#GetUserName(Eval("CUser","{0}")) %></a>
                        </ItemTemplate>
                        <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                        <HeaderStyle Width="6%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="创建时间" HeaderStyle-CssClass="egv_datetd">
                        <ItemTemplate>
                            <%#Eval("CDate") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="留言IP">
                        <ItemTemplate>
                            <%#Eval("IP").ToString()==""?"无IP地址":Eval("IP") %>
                        </ItemTemplate>
                        <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                        <HeaderStyle Width="12%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="帖子类型">
                        <ItemTemplate>
                            <%#GetMsgPosition() %>
                        </ItemTemplate>
                        <ItemStyle Width="8%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="点击数" HeaderStyle-CssClass="egv_chktd">
                        <ItemTemplate>
                            <%#GetHitCount() %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <a href="<%#"/PItem?id="+Eval("ID") %>" target="_blank" class="option_style"><%#lang.LF("<i class='fa fa-globe'></i>前台预览")%></a>
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="QList" CommandArgument='<%# Eval("ID")+"&CateID="+Eval("CateID") %>' CssClass="option_style"><i class="fa fa-navicon" title="内容"></i>贴子内容</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="13%" />
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="tdbg" HorizontalAlign="Center" />
                <RowStyle Height="24px" HorizontalAlign="Center" />
            </ZL:ExGridView>
        </div>
        <div class="clearbox"></div>
        <div class="panel panel-footer" style="padding:3px; margin:0px;">
            <asp:Button ID="btndelete" runat="server" CssClass="btn btn-primary" OnClientClick="return confirm('确定要删除选中的项目吗？')" Text="批量删除" OnClick="btndelete_Click" />
            <asp:Button ID="Rels" runat="server" CssClass="btn btn-primary" Text="批量还原" OnClick="Rels_Click" />
            <asp:Button ID="Check_B" runat="server" CssClass="btn btn-primary" Text="批量审核" OnClick="Check_B_Click" />
            <asp:Button ID="UnCheck_B" runat="server" CssClass="btn btn-primary" Text="取消审核" OnClick="UnCheck_B_Click" />
            <asp:Button ID="DelAll" runat="server" Visible="false" CssClass="btn btn-primary" OnClientClick="return delAll()" Text="清空回收站" OnClick="btndelete_Click" />
            <span id="Opiton_Span" runat="server" visible="false">
                <asp:Button ID="BtnAddMedalPosation" runat="server" Text="颁发勋章" CssClass="btn btn-primary" OnClick="BtnAddMedalPosation_Click" />
                <asp:Button ID="BtnSetAllTopPosation" runat="server" CssClass="btn btn-primary" Text="全局置顶" OnClick="BtnSetAllTopPosation_Click" />
                <asp:Button ID="BtnSetTopPosation" runat="server" CssClass="btn btn-primary" Text="版面置顶" OnClick="BtnSetTopPosation_Click" />
                <asp:Button ID="BtnClosTopPosation" runat="server" CssClass="btn btn-primary" Text="取消置顶" OnClick="BtnClosTopPosation_Click" />
                <asp:Button ID="BtnSetRecomPosation" runat="server" CssClass="btn btn-primary" Text="设为精华" OnClick="BtnSetRecomPosation_Click" />
                <asp:Button ID="BtnCloseRecomPosation" runat="server" CssClass="btn btn-primary" Text="取消精华" OnClick="BtnCloseRecomPosation_Click" />
                <asp:Button ID="BtnSetDown" runat="server" CssClass="btn btn-primary" Text="沉底贴子" OnClick="BtnSetDown_Click" />
                <asp:Button ID="BtnCloseDown" runat="server" CssClass="btn btn-primary" Text="取消沉底" OnClick="BtnCloseDown_Click" />
                <button type="button" onclick="ShowBarInfo()" class="btn btn-primary">移动版块</button>
            </span>
            <asp:HiddenField ID="HdnCateID" runat="server" />
        </div>
    </div>
    <div style="display:none;" id="barinfo">
        <div class="dropdown drop">
                <button class="btn btn-default dropdown-toggle text-left" type="button" id="dropdown1" runat="server" data-toggle="dropdown" aria-expanded="true">
                    <span id="dr_text">请选择版面</span>
                    <span class="caret pull-right" style="margin-top: 7px;"></span>
                    <asp:HiddenField ID="selected_Hid" runat="server" />
                </button>
                <ul id="PCate_ul" runat="server" class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1"></ul>
            </div>
        <asp:Button ID="MoveBar" CssClass="btn btn-primary drop" runat="server" OnClick="MoveBar_Click" Text="移动" />
    </div>
<style>
th{ text-align:center;}
.drop{display:inline;}
.drop .dropdown-toggle{width:200px;}
</style>
<script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
<script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
<script>
    var diag = new ZL_Dialog();
    var BarDiag = new ZL_Dialog();
    function ShowUserInfo(id) {
        diag.url = "../User/UserInfo.aspx?id=" + id;
        diag.title = "用户信息";
        diag.ShowModal();
    }
    function ShowBarInfo() {
        if (checkCH()) {
            BarDiag.width = 300;
            BarDiag.content = "barinfo";
            BarDiag.title = "请选择版面";
            BarDiag.ShowModal();
            $(".barli").click(function () {
                $("#selected_Hid").val($(this).data("barid"));
                $("#dr_text").text($(this).text());
            });
        } else {
            alert("请选择要转移的贴子！");
        }
        
    }
    function delAll() {
        var bl = false;
        if (confirm('确定要删除选中的项目吗？')) {
            $("[name=idchk]").click();
            return true;
        }
        return bl;
    }
    function checkCH() {
        var bl = false;
        $("[name=idchk]").each(function (i, d) {
            bl = d.checked;
            if (bl) {
                return false;
            }
        });
        return bl;
    }
    $().ready(function () {
        $(".nav-tabs li").removeClass("active");
        $("[href='#tab<%=BarStatus %>']").parent().addClass("active");
    });
    function ShowTabs(status) {
        location.href = 'TieList.aspx?CateID=<%=CateID %>&status=' + status;
    }
    
</script>
</asp:Content>