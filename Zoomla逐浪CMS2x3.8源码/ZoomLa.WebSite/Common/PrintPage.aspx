<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPage.aspx.cs" Inherits="Common_PrintPage" ClientIDMode="Static" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>打印页面</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <!--startprint-->
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
</div><!--top end-->
<div id="content" class="content con" style="margin-top:10px;">
    <div class="cont-wrap" id="contentTr" style="line-height:24px;">
        <asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
        <div id="prog_Div" class="curDrag" style="width:100%;cursor:move;"></div>
    </div>
</div><!--endprint-->
<div style="color:green;text-align:center;">(提示:签名,签章等信息可自由拖动)</div>
<asp:HiddenField runat="server" ID="curPosD" Value="0|0" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <style type="text/css" media="screen">
*{ margin:0; padding:0;}
body{ padding:0;margin:0;font-size:16px;}
.content{ margin: 30px auto; padding: 16px; border: 1px solid #ccc; text-align: left; }
.con { width:794px;}
#print_top a{ text-decoration: none;}
#print_top { width:100%;height: 46px; padding-top: 6px; display: block; text-align: center;}
p{ margin:0;padding:0;}
.print-title{font-size: 24px;color: #555;font-family: Microsoft YaHei;padding-right: 100px;}
.text{font-size: 12px;}
.printtop{ width: 100%; height: 46px; padding-top: 6px; display: block; text-align: center; background:#F9F9F9; border-bottom:1px solid #D6D6D6;}
.printtop span{ margin: 0 14px;}
.printtop select{margin-right: 50px;}
.startprint:hover{ cursor:pointer;}
.content{word-wrap: break-word}
hr{height:0px; border-top:1px solid #000;}
</style>
    <style type="text/css" media="print">
body{ font-size: 14pt; font-family: '宋体';}
.printtop{ display: none; visibility: hidden;}
.content{ width: 100%; }
.content h1{ font-size: 22px; font-family: Microsoft YaHei,SimHei; font-weight: normal; text-align:center; line-height:40px; padding-bottom:5px;}
p{ margin:0;}
.content .center{ text-align: center;}
.content a{ text-decoration: underline; }
hr{height:0px; border-top:1px solid #000;}
</style>
    <script type="text/javascript" src="/JS/jquery-ui.min.js"></script>
    <script type="text/javascript">
        $(function () {
            var positions = $('.position span');
            positions.each(function (k) {
                if (k == positions.length - 1) return;
                $(this).after('&nbsp;>');
            })
        });
        function changeFont(t) {
            $('.cont-wrap').css('font-size', $(t).val());
        }
        function changeLh(t) {
            $('.cont-wrap').css('line-height', $(t).val() + 'px');
        }
        var content = '';//全文缓存
        //function havepic(t) {
        //    if ($(t).attr('checked')) {
        //        $('.cont-wrap').html(content);
        //    }
        //    else {
        //        content = content == '' ? $('.cont-wrap').html() : content;
        //        $('.cont-wrap')[0].innerHTML = $('.cont-wrap').html().replace(/<img\s*[^>]*[\/]?>/img, '');
        //    }
        //}
        function changeSize(t) {
            var wid = $(t).val() == 'A3' ? '998px' : '794px';
            $('#content').css('width', wid);
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
        $().ready(function () {
            $(".curDrag").draggable
                ({
                    addClasses: false,
                    axis: false,
                    cursor: 'crosshair',
                    //start: function () { alert(12);},
                    //drag: function (){},
                    stop: function () { GetPos(); },
                    containment: 'parent'
                });//dragable end;
        });
        function GetPos() {
            //无时为auto:auto
            obj = $($(".curDrag")[0]);
            x = obj.css("top");
            y = obj.css("left");
            $("#curPosD").val(x + "|" + y);
        }
        //ID:x|y
        function InitPos(v) {
            if (v == "") return;
            var imgArr = v.split(',');
            for (var i = 0; i < imgArr.length; i++) {
                var signDiv = "<div class='curDrag' style='bottom:0;left:0;width:{width/}px;'><img src='{img/}'/></div>";
                var x = imgArr[i].split(':')[1].split('|')[0];
                var y = imgArr[i].split(':')[1].split('|')[1];
                var img = imgArr[i].split(':')[2];
                signDiv = signDiv = signDiv.replace("{x/}", x);
                signDiv = signDiv.replace("{y/}", y);
                signDiv = signDiv.replace("{img/}", img);
                $("#contentTr").append(signDiv);
            }
        }
        $().ready(function () {
            $(".curDrag").each(function () {
                var img = $(this).find("img"); //获取img元素
                $(this).css("width", img.width());//设置宽度
            })
        })
        function creatimg(img) {
            if (img != "" && img != "#") {
                var signDiv = "<div class='curDrag' style='bottom:0;left:0;'><img src='{img/}'/></div>";
                signDiv = signDiv = signDiv.replace("{img/}", img);
                $("#contentTr").append(signDiv);
            }
        }
        //-----------
        //显示处理意见，可拖动
        function DisProg(s) {
            $("#prog_Div").append(s);
        }
</script>
</asp:Content>
