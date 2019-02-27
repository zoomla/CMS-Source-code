<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print.aspx.cs" Inherits="MIS_OA_Flow_Print" MasterPageFile="~/Common/Common.master" %>
<%@ Register Src="~/MIS/OA/Tlp/receprint.ascx" TagPrefix="oa" TagName="receprint" %>
<%@ Register Src="~/MIS/OA/Tlp/sendprint.ascx" TagPrefix="oa" TagName="sendprint" %>
<%@ Register Src="~/MIS/OA/Tlp/defTlp.ascx" TagPrefix="oa" TagName="defTlp" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>表单打印</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="print_top" class="printtop">
    <span class="print-title">打印页面</span>
    <label class="text" for="">大小：</label>
    <select id="paper" onchange="changeSize(this)">
        <option selected="selected" value="A4">A4</option>
        <option value="A3">A3</option></select>
    <label class="text">字号：</label>
    <select id="fontSize" onchange="changeFont(this)">
        <option selected="selected" value="14px">14px</option>
        <option value="16px">16px</option>
        <option value="18px">18px</option>
        <option value="20px">20px</option>
    </select>
    <label class="text" for="">行高：</label>
    <select id="lineHeight" onchange="changeLh(this)">
        <option selected="selected" value="24">24px</option>
        <option value="26">26px</option>
        <option value="28">28px</option>
        <option value="30">30px</option>
    </select>
    <span><img class="startprint" onclick="doprint()" alt="" src="/Template/V3/style/images/print-btn.png" /></span>
</div>
 <asp:Panel runat="server" ID="OAForm_Div" class="content">
     <!--startprint-->
     <div class="cont-wrap" id="contentTr" style="line-height: 24px;">
         <oa:sendprint runat="server" ID="ascx_sendprint" Visible="false" />
         <oa:receprint runat="server" ID="ascx_receprint" Visible="false" />
         <oa:defTlp runat="server" ID="ascx_def" Visible="false" />
     </div>
     <!--endprint-->
 </asp:Panel>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <style type="text/css" media="screen">
        *{ margin:0; padding:0;}
        p{ margin:0;padding:0;}
        body{ padding:0;margin:0;font-size:16px;}
        .content{ margin: 30px auto; padding: 16px; border: 1px solid #ccc; text-align: left;width:794px; }
        #print_top a{ text-decoration: none;}
        #print_top { width:100%;height: 46px; padding-top: 6px; display: block; text-align: center;}
        .print-title{font-size: 24px;color: #555;font-family: Microsoft YaHei;padding-right: 100px;}
        .text{font-size: 12px;}
        .printtop{ width: 100%; height: 46px; padding-top: 6px; display: block; text-align: center; background:#F9F9F9; border-bottom:1px solid #D6D6D6;}
        .printtop span{ margin: 0 14px;}
        .printtop select{margin-right: 50px;}
        .startprint:hover{ cursor:pointer;}
        .content{word-wrap: break-word}
        hr{height:0px; border-top:1px solid #000;}
</style>
    <style type="text/css"  media="print">
        body{ font-size: 14pt; font-family: '宋体';}
        .printtop{ display: none; visibility: hidden;}
        .content{ width: 100%; }
        .content h1{ font-size: 22px; font-family: Microsoft YaHei,SimHei; font-weight: normal; text-align:center; line-height:40px; padding-bottom:5px;}
        p{ margin:0;}
        .content .center{ text-align: center;}
        .content a{ text-decoration: underline; }
        hr{height:0px; border-top:1px solid #000;}
    </style>
    <script>
        function changeFont(t) {
            $('.cont-wrap').css('font-size', $(t).val());
        }
        function changeLh(t) {
            $('.cont-wrap').css('line-height', $(t).val() + 'px');
        }
        function changeSize(t) {
            var wid = $(t).val() == 'A3' ? '998px' : '794px';
            $('#OAForm_Div').css('width', wid);
        }
        var isprintHtml = false;
        function doprint() {
            var bdhtml = window.document.body.innerHTML;
            if (!isprintHtml) {
                var sprnstr = "<!--startprint-->";
                var eprnstr = "<!--endprint-->";
                var prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
                prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
                window.document.body.innerHTML = prnhtml;
            }
            isprintHtml = true;
            window.print();
        }
    </script>
</asp:Content>
