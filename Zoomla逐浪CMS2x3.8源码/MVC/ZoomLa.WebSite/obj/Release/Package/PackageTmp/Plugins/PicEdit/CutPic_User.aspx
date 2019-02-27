<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CutPic_User.aspx.cs" Inherits="ZoomLaCMS.Plugins.PicEdit.CutPic_User" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>图片截取</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="cutbox">
            <asp:HiddenField runat="server" ID="SourceImg_Hid" />
            <asp:HiddenField runat="server" ID="NowImg_Hid" />
            <div id="imgFunc">
                <div class="col-xs-2" id="imgSave">
                    <asp:LinkButton runat="server" ID="Save_Btn" CssClass="btn btn-primary margin_t5" OnClientClick="return PreSave();" OnClick="Save_Btn_Click"><i class="fa fa-save"></i>保存</asp:LinkButton>
                </div>
                <div class="col-xs-10" id="imgEdit">
                    <ul class="nav nav-tabs pull-right cut-option" role="tablist">
                        <li class="active"><a href="#imgCut" role="tab" data-toggle="tab"><i class="fa fa-crop"></i>裁剪</a></li>
                        <li><a href="#imgSmall" role="tab" data-toggle="tab"><i class="fa fa-table"></i>缩放</a></li>
                        <li><a href="#imgMark" role="tab" data-toggle="tab"><i class="fa fa-unlink"></i>水印</a></li>
                        <li><a href="#imgWord" role="tab" data-toggle="tab"><i class="fa fa-text-width"></i>文字</a></li>
                        <li><a href="javascript:;" onclick="RotateFunc(90);" role="tab" data-toggle="tab"><i class="fa fa-undo"></i>左旋转</a></li>
                        <li><a href="javascript:;" onclick="RotateFunc(-90);" role="tab" data-toggle="tab"><i class="fa fa-repeat"></i>右旋转</a></li>
                    <%--<li><a href="#imgQua" role="tab" data-toggle="tab"><i class="fa fa-strikethrough"></i>品质</a></li>
                        <li><a href="#imgWord" role="tab" data-toggle="tab"><i class="fa fa-reply"></i></a></li>
                        <li><a href="#imgWord" role="tab" data-toggle="tab"><i class="fa fa-share"></i></a></li>--%>
                    </ul>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="container-fluid">
                <div id="imgBox" class="row">
                <div class="colo-lg-3 col-md-3 col-sm-4 col-xs-5 padding5" id="imgLeftFunc">
                    <div class="tab-content">
                        <div class="tab-pane active" id="imgCut">
                            <div class="imgTextItem">
                                <ul class="list-unstyled" role="menu">
                                    <li><h4>裁剪区域</h4></li>
                                    <li>
                                        <div class="input-group" style="width: 260px;">
                                            <span class="input-group-addon">宽:高</span>
                                            <input id="width_t" type="text" class="form-control text_x num" style="border-right:none;">
                                            <input id="height_t" type="text" class="form-control text_x num">
                                            <span class="input-group-btn"></span>
                                        </div>
                                        <input type="button" class="btn btn-default margin_t5" onclick="ApplyCut();" value="应用裁剪"  />
                                        <div class="clearfix"></div>
                                    </li>
                                    <li></li>
                                    <li class="divider"></li>
                                    <li><h4>预览</h4></li>
                                    <%-- 
                                 <li class="prev_size" id="imgCut_size">
                                        <input type="button" class="btn" value="1:1" onclick="ChangeAspect('1:1');" />
                                        <input type="button" class="btn" value="4:3" onclick="ChangeAspect('4:3');" />
                                        <input type="button" class="btn" value="16:9" onclick="ChangeAspect('16:9');" /></li>--%>
                                </ul>
                                <div class="preview_photo" style="overflow:hidden">
                                	<img src="" id="preview_img"/>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane" id="imgSmall">
                            <div class="imgTextItem">
                                <ul class="list-unstyled" role="menu">
                                    <li>
                                        <h4>缩放尺寸</h4>
                                    </li>
                                    <li>
                                         <div class="input-group" style="width: 260px;">
                                            <span class="input-group-addon">宽:高</span>
                                            <input id="zoom_width_t" type="text" class="form-control text_x num" style="border-right:none;">
                                            <input id="zoom_height_t" type="text" class="form-control text_x num">
                                            <span class="input-group-btn"></span>
                                        </div>
                                    <div class="clearfix"></div>
                                    </li>
                                    <li class="margin_t5"><span class="pull-left" title="保持初始宽高比,避免图片变形" >
                                          <input type="button" class="btn btn-default" onclick="ZoomFunc();" value="缩放图片"  />
                                          <label><input type="checkbox" id="scaled_chk" checked="checked" />约束比例</label></span>
                                    </li>
                                    <div class="clearfix"></div>
                                    <li class="divider"></li>
                                    <li><h4>预设尺寸</h4></li>
                                </ul>
                            </div>
                        </div>
                        <div class="tab-pane" id="imgMark">
                            <div class="imgFuncItem">
                                <h4>水印方案</h4>
                                <ul class="list-unstyled" style="height:180px;overflow-y:auto;">
                                    <ZL:ExRepeater runat="server" ID="RPT" PageSize="10" PagePre="<li>" PageEnd="</li>">
                                        <ItemTemplate>
                                            <li class="col-lg-4 col-md-4 col-sm-4 col-xs-4 padding5" style="height: 100px;">
                                                <label title="<%#Eval("Name") %>">
                                                    <img src="<%#Eval("Path") %>" class="img_50" alt="水印方案">
                                                    <input name="waterimg_rad" type="radio" value="<%#Eval("Path") %>" /></label>
                                            </li>
                                        </ItemTemplate>
                                  <%--      <FooterTemplate></FooterTemplate>--%>
                                    </ZL:ExRepeater>
                                </ul>
                                <div class="clearfix"></div>
                                <div class="divider margin_t10"></div>
                            </div>
                            <div class="imgFuncItem">
                                <h5>水印位置</h5>
                                <asp:DropDownList ID="ImgPos_DP" runat="server" CssClass="form-control text_md margin_t10">
                                    <asp:ListItem Value="0">中间</asp:ListItem>
                                    <asp:ListItem Value="1">中上</asp:ListItem>
                                    <asp:ListItem Value="2">中下</asp:ListItem>
                                    <asp:ListItem Value="3">左上</asp:ListItem>
                                    <asp:ListItem Value="4">左下</asp:ListItem>
                                    <asp:ListItem Value="5">右上</asp:ListItem>
                                    <asp:ListItem Value="6">右下</asp:ListItem>
                                    <asp:ListItem Value="7">随机</asp:ListItem>
                                </asp:DropDownList>
                                <div class="clearfix"></div>
                                <div class="divider margin_t10"></div>
                            </div>
                            <div class="imgFuncItem">
                                <ul class="list-unstyled">
                                    <li><h5>透明度设置</h5></li>
                                    <li>
                                        <div id="pdiv">
                                            <input id="slidenum" type="number" onkeydown="return IsEnter(this);" max="100" min="1" value="100" />
                                            <span id="slidename" style="position: absolute; top: -6px; right: -85px;">%</span>
                                            <div id="slideProgress"></div>
                                        </div>
                                    </li>
                                </ul>
                                <div class="clearfix"></div>
                                 <div class="divider margin_t10"></div>
                                <span class="pull-right margin_t10">
                                    <input type="button" class="btn btn-default" value="添加图片水印" onclick="AddImgWater();"></span>
                            </div>                                                  
                        </div>
                        <div class="tab-pane padding5" id="imgWord">
                            <h4>文字水印</h4><input type="button" value="样式选择" onclick="ShowFontStyle();" class="btn btn-default" />
                            <div class="divider" style="margin-top:5px;margin-bottom:5px;"></div>
                            <asp:HiddenField ID="ThreadStyle" runat="server" />
                            <textarea id="txtTitle"  class="form-control text_md" style="display:inline;">文字水印</textarea> 
                        <div class="divider" style="margin-top:5px;margin-bottom:5px;"></div>
                            <h5>水印位置</h5>
                        <asp:DropDownList ID="WaterPos_DP" runat="server" CssClass="form-control text_md margin_t10" >
                            <asp:ListItem Value="0">中间</asp:ListItem>
                            <asp:ListItem Value="1">中上</asp:ListItem>
                            <asp:ListItem Value="2">中下</asp:ListItem>
                            <asp:ListItem Value="3">左上</asp:ListItem>
                            <asp:ListItem Value="4">左下</asp:ListItem>
                            <asp:ListItem Value="5">右上</asp:ListItem>
                            <asp:ListItem Value="6">右下</asp:ListItem>
                            <asp:ListItem Value="7">随机</asp:ListItem>
                        </asp:DropDownList>
                            <div class="clearfix"></div>
                            <div class="divider" style="margin-top:5px;margin-bottom:5px;"></div>
                            <span class="pull-right"><input type="button" class="btn btn-default" onclick="AddFontWater();" value="添加文字水印"></span>
                    </div>
                </div>
            </div>
                <div class="colo-lg-9 col-md-9 col-sm-12 col-xs-7 padding5">
                    <div id="imgRightFunc" style="border:1px solid #fff;">
                          <img src="<%=NowImg_Hid.Value+"?"+ZoomLa.Common.function.GetRandomString(6) %>" id="photo" class="img-responsive" style="cursor: move;" />
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="ImgWidth_Hid" />
        <asp:HiddenField runat="server" ID="ImgHeight_Hid" />
    </div>
<asp:HiddenField runat="server" ID="SImgHeight_Hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <link href="/App_Themes/V3.css" rel="stylesheet" />
    <link href="css/imgareaselect-animated.css" rel="stylesheet" />
    <link href="/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style> 
.opitnButton { float: left; width: 50%; text-align: center; margin-top: 5px; color: white;}
.opitnButton * { margin: 3px;}
.modal { min-height: 600px;}
.cutbox { background: none;}
#pdiv{ width:80%;height:6px;background:#666;margin-top:50px;position:relative;text-align:center;line-height:20px; border-radius:5px;}
#slidenum{ position:absolute; top:-12px; right:-70px; width:60px; height:30px; line-height:30px; border:1px solid #ccc; text-align:center;}
#slideProgress{ width:30px; height:30px; background:url(css/arrow-right.png); background-size:100% 100%; cursor:pointer; position:absolute;right:0;top:-13px; overflow:hidden;} 
/*#preview_img{max-width:200px;max-height:200px;}*/
.preview_photo{ width:220px; height:150px;} 
.cut-option li a{padding-left:0px; padding-right:0px;}
.imgTextItem li{ height:30px; line-height:30px;}
.imgTextItem .form-control{width:70px;}
.imgTextItem .prev_size button{ float:left; margin-right:10px; width:20%;}
</style>
    <script src="/dist/js/bootstrap.min.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/JS/ZL_Regex.js"></script>
    <script src="JS/jquery.imgareaselect.js"></script>
    <script src="JS/jquery.imgareaselect.pack.js"></script>
    <script type="text/javascript">
        var cursele = {}, selectObj;
        var stylediag = new ZL_Dialog();
        var preWidth = $(".preview_photo").innerWidth();
        var preHeight = $(".preview_photo").innerHeight();
        var phoWidth = $("#photo").innerWidth();
        var phoHeight = $("#photo").innerHeight();
        var pval = <%=string.IsNullOrEmpty(Request["pval"]) ? "{}" : Request["pval"]%>//父页面传递的Json
			$(function () {
			    selectObj = $('#photo').imgAreaSelect({
			        //aspectRatio: "1:1",//裁剪框的宽高比
			        fadeSpeed: 500,
			        autoHide: false,
			        handles: true,
			        instance: true,
			        autoHide: false,
			        persistent: false,
			        //x1: 10, y1: 10, x2: 200, y2: 160,
			        onInit: function () {
			            //mageHeight 的真实高度 （if scaled with the CSSwidthandheightproperties） 
			            //imageWidth 真实宽度 （if scaled with the CSSwidthandheightproperties）
			        },
			        onSelectEnd: function (img, selection) {
			            cursele = selection;
			            var scaleX = preWidth / selection.width;
			            var scaleY = preHeight / selection.height;
			            $("#width_t").val(selection.width);
			            $("#height_t").val(selection.height);
			            $("#preview_img").attr("src", $('#photo').attr("src"));
			            $("#preview_img").css({
			                width: $("#photo").innerWidth() * scaleX,
			                height: $("#photo").innerHeight() * scaleY,
			                marginLeft: '-' + (Math.round(selection.x1 * scaleX)) + 'px',
			                marginTop: '-' + (Math.round(selection.y1 * scaleY)) + 'px'
			            });
			        }
			    });
			    if ($("[name=waterimg_rad]").length > 0)
			    { $("[name=waterimg_rad]")[0].checked = true; }
			    $("#width_t").val($("#ImgWidth_Hid").val()); $("#zoom_width_t").val($("#ImgWidth_Hid").val());
			    $("#height_t").val($("#ImgHeight_Hid").val()); $("#zoom_height_t").val($("#ImgHeight_Hid").val());
			    $("#zoom_width_t").change(ScaledFunc);
			    $("#imgRightFunc").css("min-height", $("#imgLeftFunc").height());
			    //ZL_Regex.B_Num(".num");
			});
            //应用裁剪,如选中则裁选中区域,否则从0,0开始
            function ApplyCut() {
                var x1, y1, xwidth, yheight, spath;
                if (!cursele.x1)//如未选中,根据其中所填裁剪
                {
                    cursele.x1 = 0;
                    cursele.y1 = 0;
                    cursele.width = parseInt($("#width_t").val());
                    cursele.height = parseInt($("#height_t").val());
                    if (isNaN(cursele.width) || isNaN(cursele.height)) {
                        alert("请先选定裁剪区域,或输入正确的宽高"); return false;
                    }
                }
                $.post("CutPic_User.aspx", { action: "crop", "x1": cursele.x1, "y1": cursele.y1, width: cursele.width, height: cursele.height, vpath: $("#photo").attr("src") }, function (url) {
                    selectObj.cancelSelection();
                    $("#photo").attr("src", url);//+ "?" + Math.random()
                    $("#NowImg_Hid").val(url);
                })
            }
            //缩放
            function ZoomFunc() {
                var json = { action: "zoom", vpath: $("#NowImg_Hid").val(), width: $("#zoom_width_t").val(), height: $("#zoom_height_t").val() };
                $.post("CutPic.aspx", json, function (url) {
                    $("#photo").attr("src", url);//+ "?" + Math.random()
                    $("#NowImg_Hid").val(url);
                });
            }
            //绑定图片宽高尺寸,以宽得高
            function ScaledFunc() {
                if (document.getElementById("scaled_chk").checked) {
                    var rate = $("#ImgHeight_Hid").val() / $("#ImgWidth_Hid").val();
                    var width = parseInt($("#zoom_width_t").val());
                    var height = parseInt(width * rate);
                    $("#zoom_height_t").val(height);
                }
            }
            //旋转(左或右90度)
            function RotateFunc(ang) {
                var json = { action: "rotate", vpath: $("#NowImg_Hid").val(), angle: ang };
                $.post("CutPic.aspx", json, function (url) {
                    $("#photo").attr("src", url);
                    $("#NowImg_Hid").val(url);
                });
            }
            //水印等都是修改最初的图片
            //添加文字水印
            function AddFontWater() {
                var font = { text: "水印文字", family: "Arial", size: "18pt", weight: "bold", style: "normal", decoration: "none", color: "rgb(0, 0, 0)", background: "rgb(255,255,255)" };
                font.text = $("#txtTitle").val();
                font.family = GetCSS(font.family, "font-family");
                font.size = GetCSS(font.size, "font-size");
                font.weight = GetCSS(font.weight, "font-weight");
                font.style = GetCSS(font.style, "font-style");
                font.decoration = GetCSS(font.decoration, "text-decoration");
                font.color = GetCSS(font.color, "color");
                font.background = GetCSS(font.background, "background");
                if (font.text == "") { alert("水印文字不能为空"); return; }
                var json = { action: "fontwater", vpath: $("#NowImg_Hid").val(), pos: $("#WaterPos_DP").val(), fontmodel: JSON.stringify(font) };
                $.post("CutPic.aspx", json, function (data) {//返回生成的新图的URL
                    $("#photo").attr("src", data);
                })
            }
            //添加图片水印
            function AddImgWater() {
                //操作,图片路径,水印图片路径
                var waterimg = $("[name=waterimg_rad]:checked")[0].value;
                var json = { action: "imgwater", vpath: $("#NowImg_Hid").val(), pos: $("#ImgPos_DP").val(), trans: $("#slidenum").val(), watervpath: $("[name=waterimg_rad]:checked")[0].value };
                $.post("CutPic.aspx", json, function (data) {
                    $("#photo").attr("src", data);
                })
            }
            //function SaveImg() {//将其返回给父窗口
            //    var imgurl=$("#photo").attr("src");
            //    parent.AddImgFunc(imgurl,pval);
            //}
            function PreSave() {
                var vpath = $("#photo").attr("src");
                if (vpath.indexOf("?") > -1) {
                    vpath = vpath.substring(0, vpath.indexOf("?"));
                }
                $("#NowImg_Hid").val(vpath);
            }
            function AfterSave() {
                var imgurl = $("#SourceImg_Hid").val();
                parent.PageCallBack("cutpic", imgurl, pval);
            }
            //------Tools
            //有值则返回,否则使用默认值
            function GetCSS(def, css) {
                var $obj = $("#txtTitle");
                def = $obj.css(css) == "" ? def : $obj.css(css);
                return def;
            }
            function ShowFontStyle() {
                ShowDiag("设置字体", "/Common/SelectStyle.htm");
            }
            function ShowDiag(title, url) {
                stylediag.title = title;
                stylediag.url = url;
                stylediag.maxbtn = false;
                stylediag.backdrop = true;
                stylediag.ShowModal();
            }
        </script>
    <script type="text/javascript">
        //透明度拖动
        window.onload = function () {
            var oDiv = document.getElementById("slideProgress");
            var oPDiv = document.getElementById("pdiv");
            var startY = startoDivTop = 0;
            oDiv.onmousedown = function (e) {
                var e = e || window.event;
                startX = e.clientX;
                startoDivLeft = oDiv.offsetLeft;
                if (oDiv.setCapture) {
                    oDiv.onmousemove = doDarg;
                    oDiv.onmouseup = stopDarg;
                    oDiv.setCapture();
                }
                else {
                    document.addEventListener("mousemove", doDarg, true);
                    document.addEventListener("mouseup", stopDarg, true);
                }
                function doDarg(e) {
                    var e = e || window.event;
                    var t = e.clientX - startX + startoDivLeft;
                    if (t < 0) {
                        t = 0;
                    }
                    else if (t > oPDiv.offsetWidth - oDiv.offsetWidth) {
                        t = oPDiv.offsetHeight - oDiv.offsetHeight;
                    }
                    if (t < 0) {
                        t = oPDiv.offsetWidth - oDiv.offsetWidth;
                    }
                    $("#slidenum").val(Math.ceil(t / (oPDiv.offsetWidth - oDiv.offsetWidth) * 100));
                    oDiv.style.left = t + "px";
                }
                function stopDarg() {
                    if (oDiv.releaseCapture) {
                        oDiv.onmousemove = doDarg;
                        oDiv.onmouseup = stopDarg;
                        oDiv.releaseCapture();
                    }
                    else {
                        document.removeEventListener("mousemove", doDarg, true);
                        document.removeEventListener("mouseup", stopDarg, true);
                    }
                    oDiv.onmousemove = null;
                    oDiv.onmouseup = null;
                }
            }
        }
        function changenum() {
            document.getElementById("slideProgress").style.left = ($("#slidenum").val() / 100) * (document.getElementById("pdiv").offsetWidth - document.getElementById("slideProgress").offsetWidth) + "px";
        }
        function IsEnter(obj) {
            if (event.keyCode == 13) {
                changenum();
                return false;
            }
        }
        </script>
</asp:Content>