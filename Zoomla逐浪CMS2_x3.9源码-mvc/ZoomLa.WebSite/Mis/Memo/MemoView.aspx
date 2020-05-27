<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemoView.aspx.cs" Inherits="ZoomLaCMS.MIS.Memo.MemoView"  MasterPageFile="~/Common/Master/Empty.master"%>

<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>备忘列表</title>
<link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />
<script>
    function loadPage(id, url) {
        $("#" + id).addClass("loader");
        $("#" + id).append("Loading......");
        $.ajax({
            type: "get",
            url: url,
            cache: false,
            error: function () { alert('加载页面' + url + '时出错！'); },
            success: function (msg) {
                $("#" + id).empty().append(msg);
                $("#" + id).removeClass("loader");
            }
        });
    }
    $(function () {

    })
    //隐藏DIV
    function HideDiv(div_id) {
        $("#" + div_id).animate({ opacity: "hide" }, 300);
    }

    function ShowDiv(div_id) {
        var div_obj = $("#" + div_id);
        div_obj.animate({ opacity: "show", left: 300, top: 660, width: div_obj.width, height: div_obj.height }, 300);
    }

    setTimeout("ShowDiv('TimeDiv')", 3000);

    //给隐藏域赋值
    function HidenFile() {

    }

    //加载时间
    function show2() {
        var divs = document.getElementById("DateDiv");
        var Digital = new Date();
        var year = Digital.getFullYear();
        var months = Digital.getMonth() + 1;
        var Days = Digital.getDate();
        var hours = Digital.getHours();
        var minutes = Digital.getMinutes();
        var seconds = Digital.getSeconds();
        var dn = "AM"
        if (hours >= 12) {
            dn = "PM";
            hours = hours - 12;
        }
        if (hours == 0)
            hours = 12;
        if (minutes <= 9)
            minutes = "0" + minutes;
        if (seconds <= 9)
            seconds = "0" + seconds;
        var ctime = year + "年" + months + "月" + Days + "日 " + hours + ":" + minutes + ":" + seconds + " " + dn;
        divs.innerHTML = ctime;
        setTimeout("show2()", 1000);
    }
    window.onload = show2;


function putid(id) {
var url = '/Mis/Memo/ComList.aspx?pid=';
document.getElementById("HidCommTxt").value = id;
loadPage("Comlist", url + id)
document.getElementById("CommentDiv").style.display = "block";
var h = document.getElementById("divcon" + id).getBoundingClientRect().top;
document.getElementById("CommentDiv").style.top = (h - 100) + "px";
document.getElementById("CommentDiv").style.background = "#fdfce6";
}
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
 <div id="Meno">
    <div id="Meno_left">
        <div class="Meno_new"><a href="AddMemo.aspx">新建备忘</a></div>
        <div class="Meno_li">
            <ul>
                <li class="Meno_all"><a href="MemoView.aspx">全部备忘</a></li>
                <li class="Meno_my"><a href="Default.aspx?UName=<%= buser.GetLogin().UserName%>">我的备忘</a></li>
                <li class="Meno_share"><a href="Default.aspx?Sname=<%=buser.GetLogin().UserName%>">共享给我  </a></li>
                <li class="Meno_pin"><a href="#">我的评论</a></li>
            </ul>
        </div>
    </div>
    <div id="Meno_right">
                        <div>
        <div id="Meno_Title">
            <asp:Literal ID="ltlTitle" runat="server"></asp:Literal></div>
        <div class="Meno_search">
            <a href="#">
                <img src="../../App_Themes/UserThem/images/Mis/icon_att_gray.gif" alt="" /></a>
            <a href="#">
                <img src="../../App_Themes/UserThem/images/Mis/icon_remind_gray.gif" alt="" /></a>
            <a href="#">
                <img src="../../App_Themes/UserThem/images/Mis/icon_share.gif" alt="" /></a>
        </div>
        <div class="Meno_list"  >
            <div id="divEmpty" style="text-align:center; height:40px; line-height:40px;" runat="server">暂时没有备忘记录， 快去<a href="AddMemo.aspx">新建</a>一个吧、、、</div>
                
            <div id="CommentDiv" style=" position:absolute; padding:8px;border:1px solid #ccc; display:none;">
                    <span class="closex"> <a href="javascript:void(0)" onclick="HideDiv('CommentDiv')" >×</a> </span>
                <asp:TextBox ID="TxtComment" runat="server" Height="47px" TextMode="MultiLine" Width="661px"></asp:TextBox>
                <br />
                <input ID="HidCommTxt" type="hidden" value="11" runat="server" />
                <asp:Button ID="BtnComment" CssClass="i_bottom" Text="提交" runat="server" OnClick="BtnComment_Click"/>
                <input type="button" value="取消" class="i_bottom" id="conbtn" onclick="HideDiv('CommentDiv')" />
                <div id="Comlist"> </div><%-- --%>

            </div>
            <asp:Repeater ID="rptMemos" runat="server" DataMember="ID" OnItemCommand="rptMemos_ItemCommand">
                <HeaderTemplate>
                    <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li style="border-bottom: 1px dotted #cbc6c6;">
                        <div class="ctop" style="padding:5px;">
                        <span class="author" style="color:#278139;"><%#Eval("Inputer") %></span> &nbsp; &nbsp; 
            <span>创建于 <%# Eval("CreateTime", "{0:yyyy-MM-dd  HH:mm}") %></span>
                        </div>
                        <div style="margin-left:20px;"><%# CutTitle(Eval("Title", "{0}")) %></div>
                        <div style="float: right;">
                            <asp:LinkButton ID="lbtnDetail" CommandName="Detail" CommandArgument='<%# Eval("ID") %>' runat="server">详细信息</asp:LinkButton> &nbsp; | &nbsp;
                            <asp:LinkButton ID="lbtnEdit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' runat="server">编辑</asp:LinkButton>&nbsp; | &nbsp;
                                <asp:LinkButton ID="lbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("ID")%>' runat="server" OnClientClick="return confirm('确定要删除该条记录么？')">删除</asp:LinkButton>&nbsp; | &nbsp;
                                <a href="#" title="" onclick="putid('<%#Eval("ID")%>')">评论<%#getcout(Convert.ToInt32(Eval("ID")))%></a></div>
                        <div id='divcon<%#Eval("ID")%>'></div>
                        <div class="clear"></div>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <div id="divPager" runat="server" style="float: right;padding:5px;">
            共 <asp:Label ID="lblTotalCnts" runat="server"></asp:Label> 条数据&nbsp;&nbsp;
                            
            <asp:LinkButton ID="lbtnFirstPage" CommandName="First" OnClick="LbtnAlterPage_Click" runat="server">首页</asp:LinkButton> &nbsp;
                <asp:LinkButton ID="lbtnPrePage" CommandName="Previous" OnClick="LbtnAlterPage_Click" runat="server">上一页</asp:LinkButton>&nbsp;
                <asp:LinkButton ID="lbtnNextPage" CommandName="Next" OnClick="LbtnAlterPage_Click" runat="server">下一页</asp:LinkButton>&nbsp;
                <asp:LinkButton ID="lbtnLastPage" CommandName="Last" OnClick="LbtnAlterPage_Click" runat="server">尾页</asp:LinkButton>&nbsp; &nbsp;                  
                转到：
            <asp:DropDownList ID="ddlPages" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPages_SelectedIndexChanged"> </asp:DropDownList>
        </div>
              
            <div id="TimeDiv" class="pop_box" style=" height:200px; margin-left:298px; margin-top:-30px;">
            <div id="TimeHead" style="text-align:right; font-size:14px;" class="p_head">
                <div class="p_h_title" style=" text-align:left;">今日提醒</div>
                <div class="p_h_close" onclick="HideDiv('TimeDiv')">关闭</div>
                </div>
                <div id="TimeInfo">
                    <div id="DateDiv" style="font-size:20px; color:#278139;font-weight:bold;text-align:left;"></div>
                    <div id="warnContent" style="height:100px;">
                        <asp:Repeater ID="RepToday" runat="server" OnItemCommand="RepToday_ItemCommand">
                            <HeaderTemplate>
                                <ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                            <li style="border-bottom:1px dotted #808080">
                                标题:<asp:LinkButton ID="linkBtn" runat="server" CommandName="Detail" CommandArgument='<%# Eval("ID") %>'><%#Eval("Title")%></asp:LinkButton>&nbsp;&nbsp;分享人:<%#Eval("IsShare")%>&nbsp;&nbsp;创建时间:<%#Eval("CreateTime") %></li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
        </div>
    </div>

    </div>
</div>

<div id="div_memo" class="pop_box">
    <div class="p_head">
        <div class="p_h_title">备忘提醒</div>
        <div class="p_h_close" onclick="HideDiv('div_share')">关闭</div>
    </div>
    <div class="p_body">
        <div>
            kefeffff 标题：
        </div>
            <div id="userlst">
                <ul>
                    <li>jiefja</li>
                    <li>jiefja</li>
                    <li>jiefja</li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>

