<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DatalistProfile.aspx.cs" Inherits="Manage_Config_DatalistProfile" MasterPageFile="~/Manage/I/Default.master"%>
<%@ Register Src="~/Manage/I/ASCX/SPwd.ascx" TagPrefix="uc1" TagName="SPwd" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>系统全库概况</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <uc1:SPwd runat="server" ID="SPwd" Visible="false" />
    <div id="BreadDiv" class="container-fluid mysite" style="margin-bottom:10px;">
        <div class="row">
            <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
                <li><a href="<%=CustomerPageAction.customPath2 %>Main.aspx">工作台</a></li>
                <li><a href="DatalistProfile.aspx">扩展功能</a></li>
                <li><a href="RunSql.aspx">开发中心</a></li>
                <li><a href='DatalistProfile.aspx'>系统全库概况</a>
                    [<asp:LinkButton ID="Link2" OnClientClick='return confirm("本操作将从云台获取默认数据库标注信息且会覆盖您的个性定义，是否确定？")' OnClick="Link2_Click1" runat="server" Style="color: #1e860b;">更新ZL_DataList表</asp:LinkButton>]
                    [<asp:LinkButton ID="LinkButton5" OnClick="Link2_Click" runat="server">运行库概况</asp:LinkButton>]
        <%--            [<span class="line"><a href="ViewList.aspx" style="color: red;">视图概况</a></span>]--%>
                </li>
				<div class="pull-right" style="padding-top:5px;">
                    <div id="help"><a href="javascript:;" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a><a onclick="help_show('68')" title="帮助" class="help_btn"><i class="fa fa-question-circle"></i></a></div>
                    <div id="sel_box">
                        <div class="input-group sel_box">
                            <asp:TextBox runat="server" ID="Search_T" class="form-control" placeholder="请输入表名/说明" />
                            <span class="input-group-btn">
                                <asp:LinkButton runat="server" CssClass="btn btn-default" ID="Search_Btn" OnClick="Search_Btn_Click">搜索</asp:LinkButton>
                                <asp:Button runat="server" ID="Search_Btn_Hid" OnClick="Search_Btn_Click" Style="display: none;" />
                            </span>
                        </div>
                    </div>
				</div>
            </ol>
        </div>
    </div>
    <div runat="server" visible="false" id="maindiv">
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"  EnableTheming="False"  
                CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" 
                OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" >
                <Columns>
                <asp:BoundField HeaderText="序号" DataField="ID">
                <ItemStyle  CssClass="td_s" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="数据表名">
                <ItemTemplate>
                <%# Eval("TableName")%>
                </ItemTemplate>
                <ItemStyle Width="15%" HorizontalAlign="left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="类型">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                <a href="DatalistProfile.aspx?type=<%#Eval("Type")%>"><%#GetType(Eval("Type","{0}"))%> </a>
                <%--
                <%#getzonetypename(DataBinder.Eval(Container.DataItem, "ZoneType").ToString())%> --%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="说明">
                <HeaderStyle />
                <ItemTemplate>
                <%# Eval("Explain")%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                <HeaderStyle Width="20%" />
                <ItemTemplate>
                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' CssClass="option_style"><i class="fa fa-pencil" title="修改"></i></asp:LinkButton>
                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="View" CommandArgument='<%# Eval("ID") %>' CssClass="option_style"><i class="fa fa-table" title="表结构"></i>表结构</asp:LinkButton>
                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="ViewData" CommandArgument='<%# Eval("ID") %>' CssClass="option_style"><i class="fa fa-strikethrough" title="表数据"></i>表数据</asp:LinkButton>
                <%--<asp:LinkButton ID="LinkButton4" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ID") %>'>表操作</asp:LinkButton> --%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" />
                <RowStyle Height="24px" HorizontalAlign="Center"  />
        </ZL:ExGridView>    
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
     <style type="text/css">
         #GridView2 td,#GridView2 th{ border:#FFF 1px solid; padding-left:5px;}
         h1{margin:0;padding:20px 0;font-size:16px;}
         ol{padding-left:20px;line-height:130%;}
         #box{width:600px;text-align:left;margin:0 auto;padding-top:80px;}
         #suggest,#suggest2{width:200px;}
         .gray{color:gray;}
         .ac_results {background:#fff;border:1px solid #7f9db9;position: absolute;z-index: 10000;display: none;}
         .ac_results ul{margin:0;padding:0;list-style:none;}
         .ac_results li a{white-space: nowrap;text-decoration:none;display:block;color:#05a;padding:1px 3px;}
        .ac_results li{border:1px solid #fff;}
        .ac_over,.ac_results li a:hover {background:#c8e3fc;}
        .ac_results li a span{float:right;}
        .ac_result_tip{border-bottom:1px dashed #666;padding:3px;}
     </style>
<%--    <script type="text/javascript" src="/js/j.dimensions.js"></script>
    <script type="text/javascript" src="/js/datalist.js"></script>
    <script type="text/javascript" src="/js/j.suggest.js"></script>--%>
    <script type="text/javascript">
        //$(function () {
        //    $("#TxtName").suggest(citys, { hot_list: commoncitys, dataContainer: '#arrcity_3word', onSelect: function () { $("#BntSearch").click(); }, attachObject: '#suggest' });
        //});
        <%--  var commoncitys = new Array();
        commoncitys[0] = new Array('NOD', '节点表', 'ZL_Node', 'NO');
        commoncitys[1] = new Array('USR', '会员表', 'ZL_User', 'US');
        commoncitys[2] = new Array('CMD', '内容主表', 'ZL_CommonModel', 'CM');
        commoncitys[3] = new Array('CDT', '商城主表', 'ZL_Commodities', 'CD');
        commoncitys[4] = new Array('UBS', '会员详情表', 'ZL_UserBase', 'UB');
        commoncitys[5] = new Array('UCT', '会员购物车', 'ZL_UserCart', 'UC');
        commoncitys[6] = new Array('ORD', '会员订单', 'ZL_OrderInfo', 'OI');
        commoncitys[7] = new Array('PGR', '黄页申请表', 'ZL_PageReg', 'PR');
        commoncitys[8] = new Array('QUE', '问答表', 'ZL_Question', 'QT');
        commoncitys[9] = new Array('ART', '文章内容表', 'ZL_C_Article', 'CA');
        <%=ViewState["dataArr"]%>--%>
        $(function () {
            $("#Search_T").keypress(function () {
                if (event.keyCode == 13)
                    $("#Search_Btn_Hid").click();
            });
            $("#sel_btn").click(function (e) {
                if ($("#sel_box").css("display") == "none") {
                    $(this).addClass("active");
                    $("#sel_box").slideDown(300);
                }
                else {
                    $(this).removeClass("active");
                    $("#sel_box").slideUp(200);
                }
            });
        })
    </script>
</asp:Content>

