<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TlpList.aspx.cs" Inherits="ZoomLaCMS.Manage.Design.TlpList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link href="/dist/css/star-rating.min.css" rel="stylesheet" />
    <script src="/dist/js/star-rating.min.js"></script>
    <style>
    .rating-xs{font-size:1.4em;}
    .margintop-5{margin-top:-5px;}
    .margintop-8{margin-top:-8px;}
    .Template_box{height:21em;}
    .title{text-overflow: ellipsis;white-space: nowrap;padding-left: 0.5em;width:90%;overflow:hidden;display:inline-block;margin-top:1px;}
    </style>
    <title>模板列表</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol id="BreadNav" class="breadcrumb navbar-fixed-top">
    <li><a href='<%=CustomerPageAction.customPath2 %>Main.aspx'><%=Resources.L.工作台 %></a></li>
    <li><a href='Default.aspx'>动力模块</a></li>
    <li id="tlpli" class='active'>模板列表 <span id="reclink" runat="server"><a href='<%=Request.QueryString["type"]=="1"? "AddScenceTlp.aspx":"AddTlp.aspx" %>'>[新建模板]</a> <a href="TlpList.aspx?Status=-2&type=<%=Request.QueryString["type"] %>">[回收站]</a></span></li>
    <li class="active" id="recli" runat="server" visible="false">回收站</li>
    <div id="help" class="pull-right text-center"><a href="javascript::" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a></div>
    <div id="sel_box" runat="server" class="padding5">
        <div style="display: inline-block; width: 230px;">
            <div class="input-group" style="position: relative; margin-bottom: -12px;">
                <asp:TextBox ID="Skey_T" placeholder="模板名称" runat="server" CssClass="form-control text_md" />
                <span class="input-group-btn">
                    <asp:Button ID="Search_B" runat="server" Text="<%$Resources:L,搜索 %>" class="btn btn-primary" OnClick="Search_B_Click" />
                </span>
            </div>
        </div>
    </div>
</ol>
<div id="template" runat="server" class="template">
    <div class="panel panel-default" style="padding-top:8px;">
        <div id="navtabs_div" class="v3_navtabs v3_all_label margin_b2px"></div>
        <div class="panel-body padding0">
            <div runat="server" id="empty_div" class="alert alert-info margin_t10" visible="false">没有匹配的数据</div>
            <ul class="list-unstyled">
                <ZL:ExRepeater runat="server" BoxType="dp" ID="RPT" PageSize="20" PagePre="</div> <div class='panel-footer text-center'>" PageEnd="</div>" OnItemCommand="RPT_ItemCommand">
                    <ItemTemplate>
                        <li class="padding5">
                            <div class="Template_box">
                                <div class="tempthumil"><a href="<%#GetEditLink() %>" title="点击编辑">
                                    <img onmouseover="this.style.border='1px solid #9ac7f0';" onmouseout="this.style.border='1px solid #eeeeee';" alt="点击进入模板管理" onerror="shownopic(this);" style="width: 100%;" src="<%#Eval("PreviewImg") %>"></a></div>
                                <div class="margintop-5"><input value="<%#Eval("Score") %>" type="number" disabled class="rating" min=0 max=5 step=0.5 data-size="xs"></div>
                                <span class="margintop-8 title"><strong>[<%#GetClassName() %>]<%#GetStatus() %></strong> <%#Eval("TlpName") %></span>
                                <span class="pull-right margintop-8"><%#GetThumbImg() %></span>
                                <span <%=Status ==-1 ? "hidden":"" %>>
                                    <ul class="list_unstyled">
                                        <li class="temp_del"><%#Eval("ID") %> </li>
                                        <li style="text-align:left;padding-left:5px;width:102px;"><i class="fa fa-rmb"></i> <%#GetPrice() %></li>
                                        <li class="temp_del" title="设为默认">
                                             <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="setdef" OnClientClick="return confirm('你确定要将其设为默认模板吗?');"><%#GetIsDef() %></asp:LinkButton></li>
                                        </li>
                                        <li class="temp_del">
                                            <a href="<%#GetViewLink() %>" target="_blank" title="浏览模板"><i class="fa fa-eye"></i></a>
                                        <li class="temp_del">
                                            <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del2" OnClientClick="return confirm('你确定删除吗');"><i class="fa fa-trash-o"></i></asp:LinkButton></li>
                                    </ul>
                                </span>
                                <span <%=Status ==-1 ? "":"hidden" %>>
                                    <ul class="list_unstyled">
                                        <li class="temp_del"><%#Eval("ID") %> </li>
                                        <li style="text-align:left;padding-left:5px;width:102px;"><i class="fa fa-rmb"></i> <%#GetPrice() %></li>
                                        <li class="temp_del">
                                            <a href="<%#GetViewLink() %>" target="_blank" title="浏览模板"><i class="fa fa-eye"></i></a>
                                        </li>
                                        <li class="temp_del">
                                            <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="rec" OnClientClick="return confirm('确定要恢复这条数据吗？');"><i class="fa fa-recycle"></i></asp:LinkButton>
                                        </li>
                                        <li class="temp_del">
                                            <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del" OnClientClick="return confirm('数据删除后不可恢复，确定要删除吗？');"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                        </li>
                                    </ul>
                                </span>
                                <div class="clearfix"></div>
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
<asp:HiddenField ID="TlpClass_Hid" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<link href="/Plugins/JqueryUI/LightBox/css/lightbox.css" rel="stylesheet" media="screen" />
<script src="/Plugins/JqueryUI/LightBox/jquery.lightbox.js"></script>
<script src="/JS/Plugs/ZL_NavTab.js"></script>
<script>
    $(function () {
        if ("<%=Status==-2%>"=="True") {
             $("#tlpli").removeClass("active").html("<a href='TlpList.aspx?type=<%=Request.QueryString["type"]%>'>模板列表</a>");
        }

        base_url = document.location.href.substring(0, document.location.href.indexOf('index.html'), 0);
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
                $(".template").css("margin-top", "44px");
            }
            else {
                $(this).removeClass("active");
                $("#sel_box").slideUp(200);
                $(".template").css("margin-top", "0px");
            }
        });
       $("#navtabs_div").ZL_NavTab({
            feildid: "ID",
            feildname: "Name",
            curid: "<%=ClassID %>",
            hid: "TlpClass_Hid",
            tabclick: function (id) {
                location.href = "TlpList.aspx?type=<%=ZType %>&ClassID=" + id;
            }
        });
    });
</script>
</asp:Content>