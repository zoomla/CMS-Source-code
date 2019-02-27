<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetReport.aspx.cs" Inherits="ZoomLaCMS.Manage.Sentiment.GetReport" MasterPageFile="~/Common/Master/Empty.master"%>
<%@ Import Namespace="ZoomLaCMS.Manage.Sentiment" %>
<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
  <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <div>
        <asp:TextBox runat="server" ID="T1" Visible="false"></asp:TextBox><asp:Button runat="server" ID="Skey_Btn" OnClick="Skey_Btn_Click" Text="搜索" Visible="false"/>
        <ZL:ExGridView ID="EGV" Visible="false" runat="server" AutoGenerateColumns="true" PageSize="10"
            OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True"
            CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有内容">
        </ZL:ExGridView>
<%--        <table class="table table-bordered table-striped"><tr><td>关键词</td><td>采集数</td><td>新闻</td><td>微博</td><td>来源</td><td>文章数</td><td>扩散速度</td></tr></table>--%>
        <div class="alert alert-info">
            数据来源为新闻,微博,微信,按关键词抓取最近一个月内的一百条信息
        </div>
        <asp:Repeater runat="server" ID="RPT" OnItemDataBound="RPT_ItemDataBound">
            <ItemTemplate>
             <ul class="tul col-lg-12 col-md-12">
                 <li style="line-height:40px;font-size:14px;">
                    <span id="<%#((AnalyModel)Container.DataItem).Skey %>_sp">
                     关键词:<span class="rd_red"><%#((AnalyModel)Container.DataItem).Skey %></span>总计:<%#(Container.DataItem as AnalyModel).CollCount %>新闻:<%#(Container.DataItem as AnalyModel).FromNews %>微博:<%#(Container.DataItem as AnalyModel).FromBlog %>
                        微信:<%#(Container.DataItem as AnalyModel).FromWx %>
                    </span>
                     <a href="DataList.aspx?skey=<%#((AnalyModel)Container.DataItem).Skey %>" class="btn btn-sm btn-info">查看详情</a> 
                     <a href="javascript:;" onclick="DownToWord('<%#((AnalyModel)Container.DataItem).Skey %>');" class="btn btn-sm btn-info" title="将当前报告存档为Word下载至本地">
                         存为Word到本地<i class="fa fa-download margin_l5"></i></a>
                 </li>
                 <li>
                     <textarea id="sumpie_<%#((AnalyModel)Container.DataItem).Skey %>" style="display:none;"><%#((AnalyModel)Container.DataItem).SumPie %></textarea>
                     <iframe id="sumpie_<%#((AnalyModel)Container.DataItem).Skey%>_ifr" src="/Plugins/ECharts/ZLEcharts.aspx?CodeID=sumpie_<%#((AnalyModel)Container.DataItem).Skey %>" class="picifr"></iframe>
                 </li>
                 <li>
                      <textarea id="timeline_<%#((AnalyModel)Container.DataItem).Skey %>" style="display:none;"><%#((AnalyModel)Container.DataItem).TimeLine %></textarea>
                      <iframe id="timeline_<%#((AnalyModel)Container.DataItem).Skey%>_ifr"  src="/Plugins/ECharts/ZLEcharts.aspx?CodeID=timeline_<%#((AnalyModel)Container.DataItem).Skey  %>" class="picifr"></iframe>
                 </li>
                 <li>
                     <textarea id="timepie_<%#((AnalyModel)Container.DataItem).Skey %>" style="display:none;"><%#((AnalyModel)Container.DataItem).TimePie %></textarea>
                     <iframe id="timepie_<%#((AnalyModel)Container.DataItem).Skey%>_ifr"  src="/Plugins/ECharts/ZLEcharts.aspx?CodeID=timepie_<%#((AnalyModel)Container.DataItem).Skey  %>" class="picifr"></iframe>
                 </li>
                 <li>
                    <asp:UpdatePanel runat="server"><ContentTemplate>
                     <table class="table table-bordered table-hover table-striped">
                         <tr><td>标题</td><td>来源</td><td>链接</td></tr>
                         <ZL:ExRepeater runat="server" ID="LinkRPT" PageSize="10" PagePre="<tr id='page_tr'><td colspan='3' style='vertical-align:middle;' id='page_td'>" PageEnd="</td></tr>">
                             <ItemTemplate>
                               <tr><td class="text-left"><%#Eval("Title") %></td><td><%#Eval("Source") %></td>
                                   <td><a href="<%#Eval("Link") %>" target="_blank">浏览</a></td></tr>
                             </ItemTemplate>
                             <FooterTemplate></FooterTemplate>
                         </ZL:ExRepeater>
                     </table></ContentTemplate></asp:UpdatePanel>
                 </li>
             </ul>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <asp:HiddenField runat="server" ID="skey_hid" />
    <asp:HiddenField runat="server" ID="sumstr_hid" />
    <asp:HiddenField runat="server" ID="sumpie_hid" />
    <asp:HiddenField runat="server" ID="timeline_hid" />
    <asp:HiddenField runat="server" ID="timepie_hid" />
    <asp:Button runat="server" ID="WordReport_Btn" OnClick="WordReport_Btn_Click" style="display:none;" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
   <style type="text/css">
       .picifr{width:100%;max-width:800px; height:410px;border:none;}
       .tul {border-bottom:1px solid #ddd;}
       .tul li {text-align:center;border:1px solid #ddd;border-bottom:none;}
   </style>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        var diag = new ZL_Dialog();
        function ShowDiag(skey) {
            diag.title = skey;
            diag.backdrop = false;
            diag.maxbtn = false;
            diag.url = "DataList.aspx?skey=" + escape(skey);
            diag.ShowModal();
        }
        function DownToWord(skey)
        {
            diag.ShowMask("正在生成Word中...");
            setTimeout(function(){
                $("#skey_hid").val(skey);
                $("#sumstr_hid").val($("#"+skey+"_sp").text());
                $("#sumpie_hid").val($("#sumpie_"+skey+"_ifr")[0].contentWindow.GetBase64());
                $("#timeline_hid").val($("#timeline_"+skey+"_ifr")[0].contentWindow.GetBase64());
                $("#timepie_hid").val($("#timepie_"+skey+"_ifr")[0].contentWindow.GetBase64());
                $("#WordReport_Btn").trigger("click");
                diag.CloseModal();
            },500);

        }
    </script>
</asp:Content>
