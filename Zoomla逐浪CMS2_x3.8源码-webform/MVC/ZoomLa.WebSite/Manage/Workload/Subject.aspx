<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Subject.aspx.cs" Inherits="ZoomLaCMS.Manage.Workload.Subject" MasterPageFile="~/Manage/I/Default.master" ClientIDMode="Static" ValidateRequest="false" %>
<%@ Register TagPrefix="uc"TagName="NodeList"Src="~/Manage/I/ASCX/NodeTreeJs.ascx"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>工作统计</title>
    <style>
        .depart { min-height: 600px; border: 1px solid #ddd; background: #eee; border-radius: 4px; }
        .depart h4 { padding-left: 0.5em; height: 2em; line-height: 2em; border: 1px solid #ddd; background: #f5f5f5; border-radius: 4px; color: #428BCA; font-size: 1.2em; }
        .depart i { margin-right: 0.5em; color: #0094ff; }
        .panel-body { padding: 3px 0; }
        .depart .panel-body li { margin-bottom: -1px; height: 2.4em; line-height: 2.4em; border: 1px solid #eee; }
        .depart .panel-body li a { display: block; padding-left: 2.2em; background: rgba(247, 247, 247, 1); }
        .depart .panel-body li a:hover { background: #61B0E9; color: #fff; }
        .depart_rtitle { padding: 0.3em; min-height: 2.6em; background: #f5f5f5; border: 1px solid #ddd; border-radius: 4px; }
        .container-fluid{padding-left:0px;padding-right:0px;}
        .depart_list { margin-top: 1em; }
        .padding_td5 {padding-top: 5px;padding-bottom: 5px;}
        .dashed { border-bottom: 1px dashed #ccc;}
        #EGV th{text-align:center;}
        .allchk_l{display:none;}
        #nodeNav .input-group{display:none;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
        <div class="panel panel-default">
            <div class="panel-heading">栏目列表</div>
            <div class="panel-body">
                <uc:NodeList ID="Lists" runat="server" />
            </div>
        </div>
    </div>
    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
        <div id="filter">
            <div id="years_body" class="padding_td5 dashed">
                <strong>年份：</strong>
                <div class="btn-group" id="years_div" data-toggle="buttons">
                    <asp:Literal ID="Years_Li" EnableViewState="false" runat="server"></asp:Literal>
                </div>
            </div>
            <div id="months_body" class="padding_td5 dashed">
                <strong>月份：</strong>
                <div class="btn-group" data-toggle="buttons">
                    <asp:Literal ID="Months_Li" EnableViewState="false" runat="server"></asp:Literal>
                </div>
            </div>
            <div id="model_body" class="padding_td5">
                <strong>模型：</strong>
                <div class="btn-group" data-toggle="buttons">
                    <asp:Literal ID="Models_Li" runat="server" EnableViewState="false"></asp:Literal>
                </div>
            </div>
        </div>
        <div class="depart_list">
            <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" OnPageIndexChanging="EGV_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="Inputer" HeaderText="编辑" />
                    <asp:BoundField DataField="PCount" HeaderText="发稿量" />
                    <asp:BoundField DataField="Hits" HeaderText="访问量" />
                    <asp:BoundField DataField="ComCount" HeaderText="评论量" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <a href="../Content/ContentManage.aspx?KeyType=1&KeyWord=<%#HttpUtility.UrlEncode(Eval("Inputer","")) %>" title="查看">查看</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" />
                <RowStyle Height="24px" HorizontalAlign="Center" />
            </ZL:ExGridView>
        </div>
    </div>
    <asp:Button ID="Export" runat="server" OnClick="Export_Click" CssClass="hidden" Text="导出" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/DatePicker/WdatePicker.js"></script>
    <script>
        $("#filter .sealink").click(function () {
            location.href = $(this).find("span").data("href");
        });
        function downtable() {
            $("#Export").click();
        }
        function ExNode(obj, nodid) {
            $(obj).parent().parent().find("ul").hide(500);
            $(obj).next("ul").show(500);
        }
        function ShowData(obj, nodeid) {
            location.href = "Subject.aspx?nodeid=" + nodeid;
        }
    </script>
</asp:Content>
