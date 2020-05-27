<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisitCount.aspx.cs" Inherits="ZoomLaCMS.Manage.Design.Addon.VisitCount" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head"><title>浏览统计</title>
    <style>
        .allchk_l {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="template" runat="server" class="template">
        <ol class="breadcrumb navbar-fixed-top">
            <li><a href='<%=CustomerPageAction.customPath2 %>Main.aspx'>工作台</a></li>
            <li class='active dropdown'><a href='../Default.aspx'>动力模块</a></li>
            <li><a href='../SceneList.aspx'>场景列表</a></li>
            <li class='active'><a href="VisitCount.aspx">访问统计</a></li>
            <div id="help" class="pull-right text-center" style="margin-right: 8px;"><a href="javascript:;" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a></div>
            <div id="sel_box" runat="server" class="padding5">
                <span class="pull-left" style="line-height: 40px;">场景名称：</span>
                <div class="input-group pull-left" style="width: 300px;">
                    <asp:TextBox runat="server" ID="souchkey" class="form-control" placeholder="请输入需要搜索的内容" />
                    <span class="input-group-btn">
                        <asp:LinkButton runat="server" CssClass="btn btn-default" ID="souchok" OnClick="souchok_Click"><span class="fa fa-search"></span></asp:LinkButton>
                    </span>
                </div>
            </div>
        </ol>
        <div class="template">
            <ul class="nav nav-tabs hidden-xs hidden-sm">
                <li><a href="#tab_detail" data-toggle="tab" onclick="showtabs('detail')">实时访客</a></li>
                <li><a href="#tab_overview" data-toggle="tab" onclick="showtabs('overview')">访问概览</a></li>
            </ul>
            <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="20" IsHoldState="false" BoxType="dp"
                OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound"
                CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="数据为空">
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="ID" />
                    <asp:TemplateField HeaderText="场景标题">
                        <ItemTemplate>
                            <a href="VisitCount.aspx?skey=<%#Eval("InfoTitle") %>" title="浏览"><%#Eval("InfoTitle") %></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="访问人">
                        <ItemTemplate>
                            <%#GetUser() %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="类型">
                        <ItemTemplate>
                            <%#GetType() %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IP地址">
                        <ItemTemplate>
                            <span title="<%#Eval("IP") %>" style="cursor: pointer"><%#GetIpLocation() %></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="访问时间" DataField="CDate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <a href="/design/h5/preview.aspx?id=<%#Eval("InfoID") %>" target="_blank" title="浏览"><i class="fa fa-globe"></i>浏览</a>
                            <asp:LinkButton ID="del_btn" runat="server" CssClass="option_style" CommandName="del" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('确定删除这条浏览记录吗?')"><i class="fa fa-trash-o"></i>删除</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </ZL:ExGridView>
            <div id="rpt_div" runat="server" visible="false">
                <table class="table table-striped table-bordered table-hover">
                    <tr id="Title_Tr" runat="server">
                        <th>ID</th>
                        <th>场景标题</th>
                        <th>访问量</th>
                        <th>最近一次访问时间</th>
                        <th>操作</th>
                    </tr>
                    <tr id="Empty_Tr" runat="server" visible="false">
                        <td colspan="7">数据为空</td>
                    </tr>
                    <ZL:ExRepeater runat="server" ID="RPT" PageSize="20" PagePre="<tr id='page_tr'><td colspan='11' id='page_td'>" PageEnd="</td></tr>">
                        <ItemTemplate>
                            <tr ondblclick="location='VisitDetail.aspx?title=<%#Eval("InfoTitle") %>&id=<%#Eval("InfoID") %>'">
                                <td><%#Eval("ID") %></td>
                                <td><a href="/design/h5/preview.aspx?id=<%#Eval("InfoID") %>" target="_blank"><%#Eval("InfoTitle") %></a></td>
                                <td><%#Eval("VisitCount") %></td>
                                <td><%#Eval("CDate","{0:yyyy-MM-dd HH:mm:ss}") %></td>
                                <td>
                                    <a href="VisitDetail.aspx?title=<%#Eval("InfoTitle") %>&id=<%#Eval("InfoID") %>" title="访问详情"><i class="fa fa-list-ul"></i>详情</a>
                                    <a href="/design/h5/preview.aspx?id=<%#Eval("InfoID") %>" target="_blank" title="浏览"><i class="fa fa-globe"></i>浏览</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div class="clearfix"></div>
                        </FooterTemplate>
                    </ZL:ExRepeater>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        $(function () {
            $("ul.nav li").removeClass("active");
            $("a[href='#tab_<%=view%>']").parent().addClass("active");
        })
        var userdiag = new ZL_Dialog();
        function seluser(id) {
            userdiag.reload = true;
            userdiag.title = "查看用户";
            userdiag.url = "../../User/Userinfo.aspx?id=" + id;
            userdiag.backdrop = true;
            userdiag.maxbtn = false;
            userdiag.ShowModal();
        }
        function CloseDiag() {
            userdiag.CloseModal();
            CloseComDiag();
        }
        $("#sel_btn").click(function () {
            if ($("#sel_box").css("display") == "none") {
                showsel();
            }
            else {
                $(this).removeClass("active");
                $("#sel_box").slideUp(200);
                $(".template").css("margin-top", "0");
            }
        });
        function showsel() {
            $(this).addClass("active");
            $("#sel_box").slideDown(300);
            $(".template").css("margin-top", "42px");
        }
        function showtabs(view) {
            location.href = "VisitCount.aspx?view=" + view;
        }
    </script>
</asp:Content>

