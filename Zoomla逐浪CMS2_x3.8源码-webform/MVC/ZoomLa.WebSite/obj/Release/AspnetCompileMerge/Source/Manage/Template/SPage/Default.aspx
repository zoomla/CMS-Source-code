<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Manage.Design.SPage.Default" MasterPageFile="~/Manage/I/Default.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>页面列表</title>
    <style>
        .SPage_list { margin-top: 10px; border: 1px solid transparent; border-radius: 3px; -webkit-box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.125); box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.125); background: #fff; }
        .SPage_list .SPage_list_t { position: relative; padding-top: 70%; }
        .SPage_list .SPage_list_t a { display: block; position: absolute; top: 0; left: 0; width: 100%; height: 100%; background: #cf4646; color: #fff; font-weight: 100; }
        .SPage_list .SPage_list_t a.bgcolor1 { background: #cf4646; }
        .SPage_list .SPage_list_t a.bgcolor2 { background: #6f5499; }
        .SPage_list .SPage_list_t a.bgcolor3 { background: #0769ad; }
        .SPage_list .SPage_list_t a.bgcolor4 { background: #4caf50; }
        .SPage_list .SPage_list_t a.bgcolor5 { background: #3498db; }
        .SPage_list .SPage_list_t a.bgcolor6 { background: #f0db4f; }
        .SPage_list .SPage_list_t a.bgcolor7 { background: #1fa67a; }
        .SPage_list .SPage_list_t a.bgcolor8 { background: #d30d15; }
        .SPage_list .SPage_list_t a.bgcolor9 { background: #f43c12; }
        .SPage_list .SPage_list_t a.bgcolor10 { background: #c3522f; }
        .SPage_list .SPage_list_t a.bgcolor11 { background: #1abc9c; }
        .SPage_list .SPage_list_t a.bgcolor12 { background: #6d3353; }
        .SPage_list .SPage_list_t a.bgcolor13 { background: #2d89ef; }
        .SPage_list .SPage_list_t a.bgcolor14 { background: #2489c5; }
        .SPage_list .SPage_list_t a.bgcolor15 { background: #d35400; }
        .SPage_list .SPage_list_t a.bgcolor16 { background: #46bfbd; }
        .SPage_list .SPage_list_t a.bgcolor17 { background: #04a5e6; }
        .SPage_list .SPage_list_t a.bgcolor18 { background: #2b9646; }
        .SPage_list .SPage_list_t a.bgcolor19 { background: #8e44ad; }
        .SPage_list .SPage_list_t a.bgcolor20 { background: #e48632; }

        .SPage_list .SPage_list_tc { position: absolute; padding-left: 10%; padding-right: 10%; top: 50%; width: 100%; text-align: center; -webkit-transform: translate(0,-50%); transform: translate(0,-50%); }
        .SPage_list .SPage_list_tc h3 { margin-top: 0; font-size: 1.5em; }
        .SPage_list .SPage_list_tc p { margin-top: 5px; margin-bottom: 0; color: #fafafa; font-size: 0.9em; }
        .SPage_list .SPage_list_b { padding-top: 15px; padding-bottom: 15px; padding-left: 15px; padding-right: 15px; font-size: 0.9em; text-align: center; }
        .SPage_list .SPage_list_b a { margin-left: 2px; margin-right: 2px; color: #333; }
        .SPage_list .SPage_list_b a:hover { color: #ff7000; }

        .SPage_list1 { padding: 15px; border-bottom: 1px solid #ddd; background: #fff; }
        .SPage_list1 .SPage_list_t { float: left; width: 75%; }
        .SPage_list1 .SPage_list_t a { display: block; width: 100%; }
        .SPage_list1 .SPage_list_t h3 { float: left; margin-top: 0; margin-bottom: 0; width: 33.3333%; font-size: 1em; }
        .SPage_list1 .SPage_list_t p { float: left; margin-bottom: 0; width: 33.3333%; }
        .SPage_list1 .SPage_list_b { float: left; width: 25%; text-align: center; }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExRepeater runat="server" ID="RPT" PageSize="16" PagePre="<div class='clearfix'></div><div class='text-center'>" PageEnd="</div>" OnItemCommand="RPT_ItemCommand">
        <ItemTemplate>
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12 padding5">
                <div class="SPage_list">
                    <div class="SPage_list_t">
                        <a href="/design/spage/Default.aspx?id=<%#Eval("ID") %>" target="_blank" title="<%#Eval("PageName")%>">
                            <div class="SPage_list_tc">
                                <h3><%#Eval("PageName") %></h3>
                                <p><i class="fa fa-clock-o"></i><%#Eval("CDate","{0:yyyy年MM月dd日 HH:mm}") %></p>
                                <p>备注：<%#Eval("PageDESC") %></p>
                            </div>
                        </a>
                    </div>
                    <div class="SPage_list_b">
                        <a href="/design/spage/Default.aspx?id=<%#Eval("ID") %>" target="_blank"><i class="fa fa-paint-brush"></i> 设计</a>
                        <a href="<%#string.IsNullOrEmpty(Eval("ViewUrl",""))?"/design/spage/preview.aspx?id="+Eval("ID"):Eval("ViewUrl","") %>" target="_blank"><i class="fa fa-eye"></i> 预览</a>
                        <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="copy" OnClientClick="return confirm('确定要复制吗');" class="option_style"><i class="fa fa-copy"></i> 复制</asp:LinkButton>
                        <a href="AddPage.aspx?id=<%#Eval("ID") %>"><i class="fa fa-edit"></i> 修改</a>
                        <asp:LinkButton runat="server" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗');"><i class="fa fa-trash"></i> 删除</asp:LinkButton>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </ItemTemplate>
        <FooterTemplate></FooterTemplate>
    </ZL:ExRepeater>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        $().ready(function (e) {
            $(".SPage_list_t a").each(function (index, element) {
                $(this).addClass("bgcolor" + (parseInt(Math.random() * 20 + 1)));
            });
        });
        function ShowColor(obj) {
            if ($(obj).find(".fa-list-ul").length > 0) {
                $(obj).find(".fa-list-ul").removeClass("fa-list-ul").addClass("fa-th-large");
                $(".SPage_list").addClass("SPage_list1").removeClass("SPage_list").parent().removeClass("col-lg-3").addClass("col-lg-12");
            }
            else if ($(obj).find(".fa-th-large").length > 0) {
                $(obj).find(".fa-th-large").removeClass("fa-th-large").addClass("fa-list-ul");
                $(".SPage_list1").addClass("SPage_list").removeClass("SPage_list1").parent().removeClass("col-lg-12").addClass("col-lg-3");
            }
        }
    </script>
</asp:Content>
