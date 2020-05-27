<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MastDefault.aspx.cs" Inherits="ZoomLaCMS.Edit.MastDefault" MasterPageFile="~/Common/Master/Empty.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>写作助理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="wtfltx_nav">
<div id="wtfltx_navH">
<img id="wt_close" onClick="nav_close('wtfltx_navC','wt_close','wt_show')" src="../App_Themes/User/w_close.gif" alt=""/> <span id="wt_show"  onClick="nav_close('wtfltx_navC','wt_show','wt_close')">展开</span><b>文体分类体系导航</b>  <i>[提示:框架内点击/SourceList.aspx?NodeID=1方法即能传入内容，"1“为ID变量，读取该节点栏目列表页。]</i>
</div>
<div id="w_navGD">
<img onClick="nav_close('w_navGD','w_navGD','w_navGD')" src="../App_Themes/User/w_close.gif" alt=""/>
<br/>
<span id="hidden1" onClick="nav_close('zhank01','hidden1','zh1')">关闭</span><a  href="javascript:;" id="zh1" onclick="nav_show('zhank01','zh1','hidden1')"><i class="fa fa-chevron-circle-down"></i>展开</a><b>知照类：</b><br/>
<div id="zhank01" style="display:none"><a href="#">公告</a>    <a href="#">通报</a>    <a href="#">公报</a>   <a href="#"> 通知</a>   <a href="#"> 函</a><br/></div>
<a  href="javascript:;" id="zh2" onclick="nav_show('zhank02','zh2','hidden2')"><i class="fa fa-chevron-circle-down"></i>展开</a><span id="hidden2" onClick="nav_close('zhank02','hidden2','zh2')">关闭</span><b>批答类：</b><br/>
<div id="zhank02" style="display:none"><a href="#">批复</a>    <a href="#">答复函</a><br/></div>
<a  href="javascript:;" id="zh3" onclick="nav_show('zhank03','zh3','hidden3')"><i class="fa fa-chevron-circle-down"></i>展开</a><span id="hidden3" onClick="nav_close('zhank03','hidden3','zh3')">关闭</span><b>执办类：</b><br/>
<div id="zhank03" style="display:none"><a href="#">决议</a>    <a href="#">命令</a>   <a href="#"> 条例 </a>  <a href="#"> 决定 </a>  <a href="#"> 通告</a><br/></div>
<a  href="javascript:;" id="zh4" onclick="nav_show('zhank04','zh4','hidden4')"><i class="fa fa-chevron-circle-down"></i>展开</a><span id="hidden4" onClick="nav_close('zhank04','hidden4','zh4')" >关闭</span><b>报请类：</b><br/>
<div id="zhank04" style="display:none"><a href="#">报告</a>   <a href="#"> 请复函</a> <a href="#"> 请示</a>    <a href="#">方案</a><br/></div>
......
</div> 
<div id="wtfltx_navC">
<ul>
<li><span class="pull-right"><a href="javascript:;" onclick="nav_show('w_navGD','w_navGD','w_navGD')"><i class="fa fa-plus"></i>更多</a></span><b>法定公文：</b><a href="SourceList.aspx?NodeID=78" target="contents">请示</a> <a href="#">公告</a><a href="#"> 通告</a><a href="#"> 通知</a><a href="#"> 意见函</a><a href="#"> 批复</a> </li>                                    
<li><span class="pull-right"><a href="javascript:;"><i class="fa fa-plus"></i>更多</a></span><b>军事文书：</b><a href="#">动员令</a><a href="#"> 政工信息</a><a href="#"> 考核材料 </a><a href="#">政课教案</a></li>  
<li><span class="pull-right"><a href="javascript:;"><i class="fa fa-plus"></i>更多</a></span><b>事务公文：</b><a href="#" id="fltx11" class="zy_s02">讲话稿</a><a href="#" id="fltx12" class="zy_s02"> 工作总结</a><a href="#"> 竞聘演讲稿 </a><a href="#">述职报告</a></li>                                
<li><span class="pull-right"><a href="javascript:;"><i class="fa fa-plus"></i>更多</a></span><b>教育文书：</b><a href="#">教学大纲</a><a href="#"> 教案 </a><a href="#">毕业论文</a><a href="#"> 网络课件</a></li>
<li><span class="pull-right"><a href="javascript:;"><i class="fa fa-plus"></i>更多</a></span><b>商务文书：</b><a href="#">商品询价函</a><a href="#"> 承包合同 </a><a href="#">网上交易协议书</a></li> 
<li><span class="pull-right"><a href="javascript:;"><i class="fa fa-plus"></i>更多</a></span><b>涉外文书:</b><a href="#">进口合同</a><a href="#"> 反倾销应诉书</a><a href="#"> 出口商品推广函</a></li>
<li><span class="pull-right"><a href="javascript:;"><i class="fa fa-plus"></i>更多</a></span><b>经济文书：</b><a href="#">经济活动分析报告</a><a href="#"> 财经决算报告</a>         </li>                             
<li><span class="pull-right"><a href="javascript:;"><i class="fa fa-plus"></i>更多</a></span><b>其他行业文书:</b><a href="#"> 房屋租用协议书</a><a href="#"> 住院记录</a><a href="#"> 信贷计划书</a></li>
<li><span class="pull-right"><a href="javascript:;"><i class="fa fa-plus"></i>更多</a></span><b>法律文书：</b><a href="#">辩护词</a><a href="#"> 刑事上诉状</a><a href="#"> 民事调解书 </a>        </li>                              
<li><span class="pull-right"><a href="javascript:;"><i class="fa fa-plus"></i>更多</a></span><b>日常实用文书：</b><a href="#">求职信 </a><a href="#">招聘启事</a><a href="#"> 招工启事</a><a href="#"> 推荐信</a></li>
<li><span class="pull-right"><a href="javascript:;"><i class="fa fa-plus"></i>更多</a></span><b>科技文书：</b><a href="#">科技论文 </a><a href="#">科研开题报告词</a><a href="#"> 发明申报书</a>   </li>                             
<li><span class="pull-right"><a href="javascript:;"><i class="fa fa-plus"></i>更多</a></span><b> 社交礼仪文书：</b><a href="#">祝酒辞 </a><a href="#">欢送词</a><a href="#"> 颁奖词</a><a href="#"> 贺电 </a><a href="#">悼词</a>  </li>
<li><span class="pull-right"><a href="javascript:;"><i class="fa fa-plus"></i>更多</a></span><b>新闻出版文书：</b><a href="#">人物消息 </a><a href="#">综合报道</a><a href="#"> 深度报道</a><a href="#"> 发刊词</a></li>                            
<li><span class="pull-right"><a href="javascript:;"><i class="fa fa-plus"></i>更多</a></span><b>日常电子文书：</b><a href="#">手机短信 </a><a href="#">博客</a><a href="#"> 电子明信片</a> </li>
</ul>
</div>
<div class="clear"></div>
</div>

    <div id="w_content" style="width:100%; padding:1px;">
<div id="w_conL" style="width:48%">
            <div id="w_conL_t">
            <a href="javascript:void(0)" id="fl" onclick="alertWin('提示与资源区','Source.aspx' ,500,550)" >浮动窗口</a><a href="#" id="ret">还原窗口</a><a href="Default.aspx" class="in_gaoJ" style="cursor:pointer">进入中级>></a><span class="dqtit">当前为高级助写中心 </span> <b>提示与资源区</b>
            </div>

<iframe id="contents" src="MastSource.aspx" name="contents" style=" width:99%; height:98%; overflow: auto;"></iframe>

</div>

<div id="WCON_m">
<div id="WCON_m_pic"><img src="../App_Themes/User/toleft.jpg" alt=""  onclick="nav_close('w_conL','','WCON_r')"/><img src="../App_Themes/User/middel.jpg" alt=""   onclick="nav_close('w_conL','m','WCON_r')"/><img src="../App_Themes/User/toright.jpg" alt=""   onclick="nav_close('WCON_r','','w_conL')"/></div>
</div>

    <div id="WCON_r" style="width:48%; float:right;height:100%;">
    <div id="WCON_r_t">
    <b>写作区</b>
    <img onclick="nav_close('wtfltx_navC','zdh','wt_show')" id="Zdh" src="../App_Themes/UserThem/images/edit/w_suof.jpg" alt="" />
    
    </div>
    <%--<div id="WCON_r_m">
    <%-- <a href="#">查找</a><a href="#"> 表格</a><a href="#">插入图片 </a><a href="#"> 统计字数</a><a href="#"> 插入格式模板</a><br/>
   <span><a href="#">剪切 </a><a href="#"> 复制</a><a href="#">粘贴 </a></span><img src="../Template/style/images/w_word1.gif" alt="" /><img src="../Template/style/images/w_word2.gif" alt=""/>--%>
    <%--</div>--%>
    <div id="WCON_r_c01" style=" height:95%; margin-right:10px; background:none;">
    <%
        string id = "";
        if (Request.QueryString["ID"] == null || Request.QueryString["ID"] == "")
        {
            id = "0";
        }
        else
        {
            id = Request.QueryString["ID"];
        }
         %>
        <iframe id="Edit" name="Edit" src="Edit.aspx?ID=<%=id%>&DocType=<%=Request.QueryString["DocType"] %>&DocTitle=<%=Request.QueryString["DocTitle"] %>&uptp=<%=Request.QueryString["uptp"] %>" style="width:100%; height:97%; overflow:auto"></iframe></div>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <link href="../App_Themes/V3.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript">
        function nav_close(id1, id2, id3) {
            document.getElementById(id1).style.display = "none";
            document.getElementById(id3).style.display = "block";
            if (id2 == "") {
                document.getElementById(id3).style.width = "97%";
            }
            else {
                if (id2 == "wt_show") {
                    //document.getElementById(id3).style.width = "48%";
                    document.getElementById(id1).style.display = "block";
                    //document.getElementById(id1).style.width = "48%";
                    document.getElementById(id3).style.display = "block";
                    document.getElementById(id2).style.display = "none";
                } else if (id2 == "m") {
                    document.getElementById(id3).style.width = "48%";
                    document.getElementById(id1).style.display = "block";
                    document.getElementById(id1).style.width = "48%";
                    document.getElementById(id3).style.display = "block";
                } else if (id2 == "zdh") {
                    document.getElementById("w_conL").style.display = "none";
                    document.getElementById("WCON_r").style.width = "95%";
                    document.getElementById("nav_close").style.display = "none";
                    document.getElementById("wt_close").style.display = "none";
                }
                document.getElementById(id2).style.display = "none";
            }
        }

        function alertWin(title, msg, w, h) {
            var titleheight = "22px"; // 提示窗口标题高度 
            var bordercolor = "rgb(154,199,240)"; // 提示窗口的边框颜色 
            var titlecolor = "#FFFFFF"; // 提示窗口的标题颜色 
            var titlebgcolor = "rgb(154,199,240)"; // 提示窗口的标题背景色 
            var bgcolor = "#FFFFFF"; // 提示内容的背景色 

            var iWidth = document.documentElement.clientWidth;
            var iHeight = document.documentElement.clientHeight;
            var bgObj = document.createElement("div");
            bgObj.style.cssText = "position:absolute;left:0px;top:0px;width:" + iWidth + "px;height:" + Math.max(document.body.clientHeight, iHeight) + "px;filter:Alpha(Opacity=30);opacity:0.3;background-color:#000000;z-index:1000;";
            document.body.appendChild(bgObj);

            var msgObj = document.createElement("div");
            msgObj.style.cssText = "position:absolute;font:11px '宋体';top:" + (iHeight - h) / 2 + "px;left:" + (iWidth - w) / 2 + "px;width:" + w + "px;height:" + h + "px;text-align:center;border:1px solid " + bordercolor + ";background-color:" + bgcolor + ";padding:1px;line-height:22px;z-index:102;";
            document.body.appendChild(msgObj);

            var table = document.createElement("table"); //www.divcss5.com divcss5
            msgObj.appendChild(table);
            table.style.cssText = "margin:0px;border:0px;padding:0px; width:100% ;";
            table.cellSpacing = 0;
            var tr = table.insertRow(-1);
            var titleBar = tr.insertCell(-1);
            titleBar.style.cssText = "width:100%;height:" + titleheight + "px;text-align:left;padding:3px;margin:0px;font:bold 13px '宋体';color:" + titlecolor + ";border:1px solid " + bordercolor + ";cursor:move;background-color:" + titlebgcolor;
            titleBar.style.paddingLeft = "10px";
            titleBar.innerHTML = title;
            var moveX = 0;
            var moveY = 0;
            var moveTop = 0;
            var moveLeft = 0;
            var moveable = false;
            var docMouseMoveEvent = document.onmousemove;
            var docMouseUpEvent = document.onmouseup;
            titleBar.onmousedown = function () {
                var evt = getEvent();
                moveable = true;
                moveX = evt.clientX;
                moveY = evt.clientY;
                moveTop = parseInt(msgObj.style.top);
                moveLeft = parseInt(msgObj.style.left);

                document.onmousemove = function () {
                    if (moveable) {
                        var evt = getEvent();
                        var x = moveLeft + evt.clientX - moveX; //www.divcss5.com divcss5
                        var y = moveTop + evt.clientY - moveY;
                        if (x > 0 && (x + w < iWidth) && y > 0 && (y + h < iHeight)) {
                            msgObj.style.left = x + "px";
                            msgObj.style.top = y + "px";
                        }
                    }
                };
                document.onmouseup = function () {
                    if (moveable) {
                        document.onmousemove = docMouseMoveEvent; //www.divcss5.com divcss5
                        document.onmouseup = docMouseUpEvent;
                        moveable = false;
                        moveX = 0;
                        moveY = 0;
                        moveTop = 0;
                        moveLeft = 0;
                    }
                };
            }

            var closeBtn = tr.insertCell(-1);
            closeBtn.style.cssText = "cursor:pointer; padding:2px;background-color:" + titlebgcolor;
            closeBtn.innerHTML = "<span style='font-size:15pt; color:" + titlecolor + ";'>×</span>";
            closeBtn.onclick = function () {
                document.body.removeChild(bgObj);
                document.body.removeChild(msgObj);
            }
            var msgBox = table.insertRow(-1).insertCell(-1);
            msgBox.style.width = "90%";
            msgBox.style.height = h - 25 + "px";
            msgBox.colSpan = 2;
            msgBox.innerHTML = "<iframe  src=" + msg + " ' style='width:98%; height:97%; overflow:auto'></iframe>"

            // 获得事件Event对象，用于兼容IE和FireFox 
            function getEvent() {
                return window.event || arguments.callee.caller.arguments[0];
            }
        }
</script>
    <script type="text/javascript">
        //  var Edit = $(window.parent.document).contents().find("#Edit")[0].contentWindow;
        function copytext(content) {

            Edit.window.copytext(content);
        }
        function copyimg(src) {
            Edit.window.copyimg(src);
        }
        function SaveOnline() {
            Edit.SaveOnline()
        }
        function New() {
            Edit.window.New();
        }
        function Save(add, cont, id) {
            //alert(cont +id);
            //alert(add);
            $.ajax({
                type: "Get",
                url: "/Edit/Default.aspx",
                data: "type=SaveOnline&addNew=" + add + "&ID=" + id + "&content=" + escape(cont),
                success: function (msg) {
                    alert(msg);
                },
                error: function (msg) {
                    alert(msg)
                }
            });
        }
</script>
</asp:Content>


