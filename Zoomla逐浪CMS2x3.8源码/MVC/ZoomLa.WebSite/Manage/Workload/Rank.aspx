<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Rank.aspx.cs" Inherits="ZoomLaCMS.Manage.Workload.Rank" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register TagPrefix="uc"TagName="NodeList"Src="~/Manage/I/ASCX/NodeTreeJs.ascx"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>分类排行</title>
    <style>
        .padding0 { padding-left: 0; padding-right: 0; }
        .padding5 { padding-left: 5px; padding-right: 5px; }
        .padding10 { padding-left: 10px; padding-right: 10px; }
        .ranksort { margin-top: 0.5em; }
        .ranksort .nav-tabs li a { padding: 0; width: 100px; height: 36px; line-height: 36px; color: #666; text-align: center; }
        .ranksort .nav-tabs li a:hover, .ranksort .nav-tabs li a:focus { outline: none; background: none; border-color: rgba(209, 222, 241, 1); }
        .ranksort .nav-tabs .active a, .ranksort .nav-tabs .active a:hover, .ranksort .nav-tabs .active a:focus { background: #eee; border-color: rgba(209, 222, 241, 1); color: #000; }
        .rankbody { margin-top: 0.5em; }
        .rankbody_title { padding-left: 0.5em; height: 2.6em; line-height: 2.6em; font-size: 1.2em; box-shadow: 0 2px 5px #ddd; }
        .rankbody_title i { margin-right: 0.5em; color: #428bca; }
        .rankbody_l { height: 500px; background: #f5f5f5; border: 1px solid #ddd; box-shadow: 1px 1px 5px #eee; border-radius: 4px; }
        .rankbody_l #filter li { padding-left: 1em; height: 2em; line-height: 2em; }
        .depart_rtitle { padding: 0.3em; min-height: 2.6em; background: #f5f5f5; border: 1px solid #ddd; border-radius: 4px; }
        .depart_list { margin-top: 0.5em; }
        .padding_td5 {padding-top: 5px;padding-bottom: 5px;}
        .dashed { border-bottom: 1px dashed #ccc;}
        .panel-body { padding: 3px 0; }
        #nodeNav .input-group{display:none;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="ranksort">
        <ul class="nav nav-tabs" role="tablist">
            <li><a href="ContentRank.aspx">综合排行</a></li>
            <li class="click"><a href="Rank.aspx?Type=click">点击排行</a></li>
            <li class="comment"><a href="Rank.aspx?Type=comment">评论排行</a></li>
        </ul>
    </div>
    <div class="rankbody">
        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 padding0 rankbody_l">
            <div class="panel panel-default">
                <div class="panel-heading">栏目列表</div>
                <div class="panel-body">
                    <uc:NodeList ID="Lists" runat="server" />
                </div>
            </div>
        </div>
        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12 padding10 rankbody_r">
            <div id="filter">
                <div id="years_body" class="container-fluid padding_td5 dashed">
                    <strong>年份：</strong>
                    <div class="btn-group" id="years_div" data-toggle="buttons">
                        <asp:Literal ID="Years_Li" EnableViewState="false" runat="server"></asp:Literal>
                    </div>
                </div>
                <div id="months_body" class="container-fluid padding_td5 dashed">
                    <strong>月份：</strong>
                    <div class="btn-group" data-toggle="buttons">
                        <asp:Literal ID="Months_Li" EnableViewState="false" runat="server"></asp:Literal>
                    </div>
                </div>
                <div id="model_body" class="container-fluid padding_td5">
                    <strong>模型：</strong>
                    <div class="btn-group" data-toggle="buttons">
                        <asp:Literal ID="Models_Li" runat="server" EnableViewState="false"></asp:Literal>
                    </div>
                </div>
            </div>
            <div class="depart_list">
                <table class="table table-striped table-bordered">
                    <tr>
                        <th style="width:30%">标题</th>
                        <th>点击数</th>
                        <th>评论数</th>
                        <th>录入</th>
                        <th>录入时间</th>
                    </tr>
                    <tbody>
                        <ZL:ExRepeater ID="RPT" runat="server" PageSize="10" PagePre="<tr><td class='text-center' colspan='5'>" PageEnd="</td></tr>">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("Title") %></td>
                                    <td><%#Eval("Hits") %></td>
                                    <td><%#Eval("ComCount") %></td>
                                    <td><%#Eval("Inputer") %></td>
                                    <td><%#Eval("CreateTime","{0:yyyy-MM-dd}") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate></FooterTemplate>
                        </ZL:ExRepeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <asp:Button ID="DownToExcel_B" runat="server" CssClass="hidden" OnClick="DownToExcel_B_Click" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script>
        $(function () {
            var ztype = '<%=ZType %>';
            if (ztype == 'comment') { $(".comment").addClass("active"); }
            else { $(".click").addClass("active"); }
        });
        $("#filter .sealink").click(function () {
            location.href = $(this).find("span").data("href");
        });
        function downtable() {
            $("#DownToExcel_B").click();
        }
        function ExNode(obj, nodid) {
            $(obj).parent().parent().find("ul").hide(500);
            $(obj).next("ul").show(500);
        }
        function ShowData(obj, nodeid) {
            location.href = "Rank.aspx?nodeid=" + nodeid;
        }
    </script>
</asp:Content>

