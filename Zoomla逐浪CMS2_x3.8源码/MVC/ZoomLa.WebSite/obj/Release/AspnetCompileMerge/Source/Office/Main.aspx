<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.Main" MasterPageFile="~/Office/OAMain.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title></title>
<link href="/dist/css/font-awesome.min.css" rel="stylesheet" />
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script src="/JS/ICMS/ZL_Common.js"></script>
<script src="/JS/jquery-ui.min.js"></script>
    <script>
        $(function () {
            $("#sortable").sortable(
                {
                    stop: sortCallBack//拖动结束后回调方法
                });
            $("#sortable").disableSelection();
        });
        function sortCallBack() {
            var s = "";
            $("#sortable li").each(function () { s += $(this).attr("id") + "," });
            setCookie("sortli", s);
        }
        $().ready(function () {
            var s2 = getCookie("sortli");
            if (s2) {
                liArr = s2.split(',');
                for (var i = 0; i < liArr.length; i++) {
                    $li = $("#" + liArr[i]).remove();
                    $("#sortable").append($li);
                }
            }
            $().ready(function () {
                $(".table_mouse tr").addClass("tdbg");
                $(".table_mouse tr").mouseover(function () { $(this).removeClass("tdbg").addClass("tdbgmouseover") }).mouseout(function () { $(this).removeClass("tdbgmouseover").addClass("tdbg") });
                $(".table_mouse tr").dblclick(function () { v = $(this).find("#chkID").val(); location = "ViewContent.aspx?Gid=" + v; });
            });

            //获取事务等的信息数
            setTimeout(function () { GetInfo(); }, 1000);
        });
        function GetInfo() {
            //a = "GetAffir";
            //$.ajax({
            //    type: "Post",
            //    url: "/Office/OAajax.ashx",
            //    dataType: "json",
            //    data: { action: a },
            //    success: function (data) {
            //        $("#affairSpan").text("（" + data.affair + "）");
            //        $("#affairSpan2").text("（" + data.affair2 + "）");
            //    },
            //    error: function (data) {

            //    }
            //});
        }
    </script>
    <script>
        $().ready(function () {
            $("#user_left").hide();
            $("#user_right").css({ width: '100%' });
        })
</script>
<style>
#user_left { display:none;}
#user_right{ width:100%;}
.oa_item{ position:relative; }
.oa_item .item_count{ position:absolute; display:block; top:-5px; right:0; width:18px; height:18px; line-height:18px; border-radius:50%; background:#f00; color:#fff; font-size:12px; }
.prcess{font-size:45px;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
        <div>
            <ul id="sortable" style="width: 100%;">
                <li class="ui-state-default" runat="server" id="mainChk1">
                    <div class="OAborder">
                        <div class="duty_t" style="background: url('Images/Document.gif') no-repeat;">
                            <div class="duty_tp">
                                <span class="fork" onclick="closeDiv(this);"></span>
                                <span class="oaarrow" onclick="coll(this);"></span>
                                <span class="oaarrowDown" style="display: none;" onclick="coll2(this);"></span>
                                <strong>公文流转</strong>
                            </div>
                        </div>
                        <div class="content templatelist">
                            <div class="inContent executive">
                                <table class="myTable threeTr">
                                    <tr class="hover_hander">
                                        <td><a href="javascript:;" onclick="ShowMain('#draftdoc_ul','/Office/Drafting.aspx')">
                                            <img src="Images/Main/official.gif" /><div>公文拟稿</div>
                                        </a></td>
                                        <td>
                                            <a href="javascript:;" onclick="ShowMain('#agencydoc_ul','/Office/AffairsList.aspx?view=1')">
                                                <img src="Images/Main/wait.gif" /><div>待办公文<span id="affairSpan" style="color: red;"></span></div>
                                            </a></td>
                                        <td><a href="javascript:;" onclick="ShowMain('#office_ul','/Office/AffairsList.aspx?view=2')">
                                            <img src="Images/Main/done.gif" /><div>已办事务</div>
                                        </a></td>
                                    </tr>
                                    <tr class="hover_hander">
                                        <td><a href="javascript:;" onclick="ShowMain('#senddoc_ul','/Office/Office/Default.aspx')">
                                            <img src="Images/Main/post.gif" /><div>发文管理</div>
                                        </a></td>
                                        <td><a href="javascript:;" onclick="ShowMain('#userinfo_ul','/Office/AffairsList.aspx?view=3')">
                                            <img src="Images/Main/Receipt.gif" /><div>收文管理</div>
                                        </a></td>
                                        <td><a href="javascript:;" onclick="ShowMain('#userinfo_ul','/Office/Office/DocManage.aspx')">
                                            <img src="Images/Main/Circulated.gif" /><div>传阅管理</div>
                                        </a></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: right;"><a href="#">More...</a></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </li>
                <li class="ui-state-default" runat="server" id="mainChk2">
                    <div class="OAborder">
                        <div class="duty_t" style="background: url('Images/Branch.gif') no-repeat;">
                            <div class="duty_tp">
                                <span class="fork" onclick="closeDiv(this);"></span>
                                <span class="oaarrow" onclick="coll(this);"></span>
                                <span class="oaarrowDown" style="display: none;" onclick="coll2(this);"></span>
                                <strong>日常管理</strong>
                            </div>
                        </div>
                        <div class="content templatelist">
                            <div class="inContent office">
                                <table class="myTable threeTr">
                                    <tr class="hover_hander">
                                        <td><a href="javascript:;" onclick="ShowMain('#newdoc_ul','/Office/Flow/FlowList.aspx')">
                                            <img src="Images/Main/draft.gif" /><div>新建发文</div>
                                        </a></td>
                                        <td><a href="/Office/Flow/ApplyList.aspx?view=3">
                                            <img src="Images/Main/ziyuan.gif" /><div>我的公文</div>
                                        </a></td>
                                        <td><a href="/Office/Flow/ApplyList.aspx?view=1">
                                            <img src="Images/Main/Awaiting.gif" /><div class="oa_item">待审公文
                                            <span class="item_count" runat="server" id="wait_sp">0</span></div>
                                        </a></td>
                                        <td><a href="/Office/Flow/ApplyList.aspx?view=2">
                                            <img src="Images/Main/down.gif" /><div>已办公文</div>
                                        </a></td>
                                    </tr>
                                    <tr class="hover_hander"> 
                                        <td><a href="javascript:;" onclick="ShowMain('#newdoc_ul','/Office/Flow/FlowList.aspx')" >
                                            <img src="Images/Main/form.gif" /><div>新建发文</div>
                                        </a></td>
                                        <td><a href="javascript:;" onclick="ShowMain('#newdoc_ul','/Office/Flow/ApplyList.aspx?view=4')">
                                            <img src="Images/Main/schedule.gif" /><div>我的借阅</div>
                                        </a></td>
                                        <td><a href="javascript:;" onclick="ShowMain('#doclist_ul','/Office/Doc/FiledList.aspx')">
                                            <img src="Images/Main/schedule.gif" /><div>档案管理</div>
                                        </a></td>
                                        <td><a href="javascript:;" onclick="ShowMain('#doclist_ul','/Office/Doc/BorrowList.aspx')">
                                            <img src="Images/Main/schedule.gif" /><div>借阅记录</div>
                                        </a></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: right;"><a href="#">More...</a></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </li>
                <li class="ui-state-default">
                    <div class="OAborder">
                        <div class="duty_t" style="background: url('Images/Myemail.gif') no-repeat;">
                            <div class="duty_tp">

                                <strong>系统管理</strong>
                            </div>
                        </div>
                        <div class="content templatelist">
                            <div class="inContent email">
                                <table class="myTable threeTr">
                                    <tr class="hover_hander">
                                        <td><a href="Flow/FlowApply.aspx?ProID=1">
                                            <span class="fa fa-sign-in prcess"></span><div>新建收文</div>
                                        </a></td>
                                        <td><a href="Flow/FlowApply.aspx?ProID=2">
                                            <span class="fa fa-sign-out prcess"></span><div>新建发文</div>
                                        </a></td>
                                        <td><a href="Flow/FlowApply.aspx?ProID=3">
                                            <span class="fa fa-clock-o prcess"></span><div>新建请假</div>
                                        </a></td>
                                        <td><a href="Flow/FlowApply.aspx?ProID=4">
                                            <span class="fa fa-group prcess"></span><div>新建会议</div>
                                        </a></td>
                                    </tr>
                                    <tr class="hover_hander">
                                        <td>
                                            <a href="Flow/ApplyList.aspx?view=3&proid=1">
                                                <span class="fa fa-briefcase prcess"></span><div>我的收文</div>
                                            </a>
                                        </td>
                                        <td>
                                            <a href="Flow/ApplyList.aspx?view=3&proid=2">
                                                <span class="fa  fa-newspaper-o prcess"></span><div>我的发文</div>
                                            </a>
                                        </td>
                                        <td>
                                            <a href="Flow/ApplyList.aspx?view=3&proid=3">
                                                <span class="fa fa-pencil-square-o prcess"></span><div>我的请假</div>
                                            </a>
                                        </td>
                                        <td>
                                            <a href="Flow/ApplyList.aspx?view=3&proid=4">
                                                <span class="fa fa-comments-o prcess"></span><div>我的会议</div>
                                            </a>
                                        </td>
                                    </tr>
                                    
                                    
                                </table>
                            </div>
                        </div>
                    </div>
                </li>
                <li class="ui-state-default" runat="server" id="mainChk4">
                    <div class="OAborder">
                        <div class="duty_t" style="background: url('Images/Myemail.gif') no-repeat;">
                            <div class="duty_tp">
                                <span class="fork" onclick="closeDiv(this);"></span>
                                <span class="oaarrow" onclick="coll(this);"></span>
                                <span class="oaarrowDown" style="display: none;" onclick="coll2(this);"></span>
                                <strong>我的邮箱管理</strong>
                            </div>
                        </div>
                        <div class="content templatelist">
                            <div class="inContent email">
                                <table class="myTable threeTr">
                                    <tr class="hover_hander">
                                        <td><a href="javascript:;" onclick="ShowMain('Mail/MailManage.aspx','/Office/Mail/MessageSend.aspx')">
                                            <img src="Images/Main/writemail.gif" /><div>写邮件</div>
                                        </a></td>
                                        <td><a href="javascript:;" onclick="ShowMain('Mail/MailManage.aspx','/Office/Mail/Message.aspx')">
                                            <img src="Images/Main/recivemail.gif" />
                                            <div class="oa_item">
                                                收邮件<span class="item_count" runat="server" id="Span2">0</span>
                                            </div>
                                        </a></td>
                                        <td><a href="javascript:;" onclick="ShowMain('Mail/MailManage.aspx','/Office/Mail/MessageOutbox.aspx')">
                                            <img src="Images/Main/sendmail.gif" /><div>发件箱</div>
                                        </a></td>
                                    </tr>
                                    <tr class="hover_hander">
                                        <td><a href="javascript:;" onclick="ShowMain('Mail/MailManage.aspx','/Office/Mail/MessageDraftbox.aspx')">
                                            <img src="Images/Main/draftmail.gif" /><div>草稿箱</div>
                                        </a></td>
                                        <td><a href="javascript:;" onclick="ShowMain('Mail/MailManage.aspx','/Office/Mail/MessageGarbagebox.aspx')">
                                            <img src="Images/Main/rubbishmali.gif" /><div>垃圾箱</div>
                                        </a></td>
                                        <td><a href="javascript:;" onclick="ShowMain('Mail/MailManage.aspx','/Office/Other/StructList.aspx')">
                                            <img src="Images/Main/contact.gif" /><div>通迅录</div>
                                        </a></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: right;"><a href="#">More...</a></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </li>
                <li class="ui-state-default" runat="server" id="mainChk5">
                    <div class="OAborder">
                        <div class="duty_t" style="background: url('Images/Newest.gif') no-repeat;">
                            <div class="duty_tp">
                                <span class="fork" onclick="closeDiv(this);"></span>
                                <span class="oaarrow" onclick="coll(this);"></span>
                                <span class="oaarrowDown" style="display: none;" onclick="coll2(this);"></span>
                                <strong>最新通知</strong>
                            </div>
                        </div>
                        <div class="content templatelist">
                            <div class="inContent">
                                <table class="table_li table_mouse">
                                   <%Call.Label("{ZL.Label id=\"输出指定节点标题_无时间属性\" NodeID=\"1\" TitleNum=\"30\" ShowNum=\"10\" /}"); %> 
                                   <%-- <asp:Repeater runat="server" ID="noticeR1">
                                        <ItemTemplate>
                                            <tr title="双击阅读">
                                                <td><a class="bbstitle" href="/Item/<%#Eval("GeneralID") %>.aspx" target="_blank" title="<%#Eval("Title") %>"><%#Eval("Title") %></a>
                                                    <input type="checkbox" id="chkID" value="<%#Eval("GeneralID") %>" style="display: none;" /></td>
                                                <td><%#Eval("Inputer") %></td>
                                                <td class="text-right wid180"><%# DataBinder.Eval(Container.DataItem, "CreateTime", "{0:yyyy年M月d日 HH:mm}")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>--%>
                                </table>
                                <div class="more"><a href="/Class_1/Default.aspx" target="_blank">More...</a></div>
                            </div>
                        </div>
                    </div>
                </li>
                <li class="ui-state-default" runat="server" id="mainChk6">
                    <div class="OAborder">
                        <div class="duty_t" style="background: url('Images/Bbs.gif') no-repeat;">
                            <div class="duty_tp">
                                <span class="fork" onclick="closeDiv(this);"></span>
                                <span class="oaarrow" onclick="coll(this);"></span>
                                <span class="oaarrowDown" style="display: none;" onclick="coll2(this);"></span>
                                <strong>论坛交流</strong>
                            </div>
                        </div>
                        <div class="content templatelist">
                            <div class="inContent">
                                <table class="table_li table_mouse"> 
                                    <ZL:Repeater runat="server" ID="RPT">
                                        <ItemTemplate>
                                            <li><a href="/PItem?id=<%#Eval("ID") %>" target="_blank"><%#Eval("Title") %></a></li>
                                        </ItemTemplate>
                                    </ZL:Repeater>
                                    <%//Call.Label("{ZL.Label id=\"贴吧输出热点新闻标题\" NodeID=\"1\" TitleNum=\"30\" ShowNum=\"10\"  SysNum=\"80\" /}"); %>
                                </table>
                                <div class="more"><a href="/index" target="_blank">More...</a></div>
                            </div>
                        </div>
                    </div>
                </li>
            </ul>
        </div>
        <script type="text/javascript">
            function coll(obj) {
                $p = $(obj).closest('li');
                $p.find(".content").hide("fast");
                $(obj).hide().siblings().show();
            }
            function coll2(obj) {
                $p = $(obj).closest('li');
                $p.find(".content").show("fast");
                $(obj).hide().siblings().show();
            }
            function closeDiv(obj) {
                $p = $(obj).closest('li');
                //$p.hide("fast");
                $li = $p.remove();
            }
        </script>
</asp:Content>
