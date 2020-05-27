<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.MIS.Memo.Default" MasterPageFile="~/Common/Master/Empty.master" %>

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
    //隐藏DIV
    function HideDiv(div_id) {
        $("#" + div_id).animate({ opacity: "hide" }, 300);
    }

    //隐藏两个DIV
    function HideDivs(div_id,div_ids) {
        $("#" + div_id).animate({ opacity: "hide" }, 300);
        $("#" + div_ids).animate({ opacity: "hide" }, 300);
    }

    function ShowDiv(div_id) {
        var div_obj = $("#" + div_id);
        div_obj.animate({ opacity: "show", left: 300, top: 660, width: div_obj.width, height: div_obj.height }, 300);
    }

    setTimeout("ShowDiv('TimeDiv')", 3000);

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

    function Punids(id,Cont)
    {
        document.getElementById("HidCommentInfo").value = id;
        document.getElementById("Txtco").innerText = Cont;
        document.getElementById("divEdit").style.display = "block";
        window.open('ComList.aspx?pid=' + id,'', 'width=400,height=220,resizable=0,scrollbars=yes');
    }

    function putid(id) {
        var url = '/Mis/Memo/ComList.aspx?pid=';
        document.getElementById("HidCommTxt").value = id;
        //loadPage("Comlist", url + id);
        document.getElementById("ComLists").style.display = "none";
        document.getElementById("CommentDiv").style.display = "block";
        var h = document.getElementById("divcon" + id).getBoundingClientRect().top;
        document.getElementById("CommentDiv").style.top = (h - 104) + "px";
        document.getElementById("CommentDiv").style.background = "#fdfce6";
    }

   //测试用
    function whatBrowser() {
        document.Browser.Name.value = navigator.appName;
        document.Browser.Version.value = navigator.appVersion;
        document.Browser.Code.value = navigator.appCodeName;
        document.Browser.Agent.value = navigator.userAgent;
        document.getElementById("hidappName").value = navigator.appName;
        document.getElementById("hidappVersion").value = navigator.appVersion;
        document.getElementById("hidappCodeName").value = navigator.appCodeName;
        document.getElementById("hiduserAgent").value = navigator.userAgent;
    } 
</script>
<style>
.spanU{float:left;  width:35px; padding-top:8px;}
.uface{width:35px;height:35px;}
.spedt{ float:right;}
.divuinfo{position:relative;background-color:#fdfce6;}
.divuinfo p{float:left; padding:0; margin:0; padding-left:10px;  }
.divuinfo li{border-bottom:dotted 1px #ccc;}
.divuinfo .spedt{ display:block; text-align:right;}

#Contents{background-color:#808080;  width:680px; height:228px; padding-top:-100px;}
#Close { text-align:right;}
.Close{ float:right; padding-right:10px;}
#head{ padding-top:-50px;}
.TextDiv{background-color:#fff; width:660px; margin-left:10px; border:solid 1px #808080;}
#TxtComment{}
#BtnSubmit{ text-align:right; padding-right:10px;}
#hidenComment{display:none;}
.BtnDiv{ float:right; padding-right:10px;}
#divEdits{display:none;}

#BtnSubmit{ text-align:right; padding-right:10px;}
#hidenComment{display:block;}
.BtnDiv{ float:right; padding-right:10px;}
#divEdit{display:none;}
#ComLists{ display:block;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<input type="hidden" id="hidappName" runat="server" />
<input type="hidden" id="hidappVersion" runat="server" />
<input type="hidden" id="hidappCodeName" runat="server" />
<input type="hidden" id="hiduserAgent" runat="server" />
       <div id="Meno">
        <div id="Meno_left">
<%--            <div class="Meno_new"><a href="AddMemo.aspx" target="ifMemo">新建备忘</a></div>  target="ifMemo"--%>
            <div class="Meno_li">
                <ul>
                    <li class="Meno_new"><a href="AddMemo.aspx" >新建备忘</a></li>
                    <li class="Meno_all"><a href="Default.aspx" >全部备忘</a></li>
                    <li class="Meno_my"><a href="Default.aspx?UName=<%= buser.GetLogin().UserName%>"  >我的备忘</a></li>
                    <li class="Meno_share"><a href="Default.aspx?Sname=<%=buser.GetLogin().UserName%>" >共享给我  </a></li>
                    <li class="Meno_pin"><a href="Default.aspx?Mname=<%=buser.GetLogin().UserName%>" >我的评论</a></li>
                </ul>
            </div>
        </div>
        <div id="Meno_right" style="width:715px;">
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
                <asp:Repeater ID="rptMemos" runat="server" DataMember="ID" OnItemCommand="rptMemos_ItemCommand">
                    <HeaderTemplate>
                        <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li style="border-bottom: 1px dotted #cbc6c6;">
                            <div class="ctop" style="padding:5px;">
                            <span class="author" style="color:#278139;"><%#Eval("Inputer") %></span> &nbsp; &nbsp; 
                <span>创建于<%# Eval("CreateTime", "{0:yyyy-MM-dd  HH:mm}") %></span>
                           </div>
                            <div style="margin-left:20px;"><%# CutTitle(Eval("Title", "{0}")) %></div>
                            <div style="float:right; padding-right:10px;">
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
                        
                <asp:Repeater ID="rptComm" runat="server">
                    <HeaderTemplate>
                        <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li style="border-bottom:1px dotted #808080;">
                           <div>
                               <span><%#Eval("Contents")%></span>&nbsp;&nbsp;<span>(<%#Eval("CommentTime")%>)</span>
                           </div>
                            <div>
                                <span>我评论</span>&nbsp;<span><%#Eval("GeneralID")%>的备忘</span>&nbsp;&nbsp;<span><a href="MemoDetail.aspx?MID=<%#Eval("GeneralID")%>" title=""><%#Eval("Title")%>"</a></span>
                            </div>
                            <div style="text-align:right;"><a href="#" title="" onclick="Punids('<%#Eval("CommentID")%>','<%#Eval("Contents")%>')">编辑</a>&nbsp;|&nbsp;<a href="#" title="">删除</a>
                                <div id='Cous<%#Eval("GeneralID")%>'></div>
                            </div>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>

                <input type="hidden" id="HidCommentInfo" runat="server"/>
                <div id="divEdit">
                 <div class="heads">
                 <div class="Close"><a href="#" title="" onclick="HideDiv('divEdit')">关闭</a></div>
                </div>
              <div class="TextDiv">
               <asp:TextBox ID="Txtco" runat="server" TextMode="MultiLine" Height="139px" Width="658px"></asp:TextBox>
               <br />
               <asp:CheckBox ID="Chk" runat="server" Text="提醒所有共享人"/>
            </div>
          <div class="BtnDiv">
           <asp:Button ID="BtnSub" runat="server" Text="提交" OnClick="BtnSub_Click" />&nbsp;&nbsp;<asp:Button ID="BtnColse" runat="server" Text="取消"/>
          </div>
</div>

            </div>
            <div id="divPager" runat="server" style="float: right;padding:5px;">
                共 <asp:Label ID="lblTotalCnts" runat="server"></asp:Label>条数据&nbsp;&nbsp;
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
                         <asp:Label ID="lblShows" Font-Size="X-Large" Visible="true" Text="今天没有提醒" ForeColor="#ff0000" runat="server"></asp:Label>
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
                       
             <div id="CommentDiv" style=" position:absolute; padding:8px;border:1px solid #ccc;display:none;">
                 <div style=" text-align:right;">
                <span class="closex"><a href="javascript:void(0)" onclick="HideDivs('CommentDiv','Comlist')" >×</a></span>
                 </div>
                <asp:TextBox ID="TxtComment" runat="server" Height="47px" TextMode="MultiLine" Width="661px"></asp:TextBox>
                <br />
                <input ID="HidCommTxt" type="hidden" value="11" runat="server" />
                <asp:Button ID="BtnComment" Text="提交评论" runat="server" OnClick="BtnComment_Click"/>
                <input type="button" value="取消" id="conbtn" onclick="HideDiv('CommentDiv')"/> 
                 <span style=" float:right;">
                     <asp:LinkButton ID="BtnSearch" runat="server" Text="查看所有评论" OnClick="BtnSearch_Click"></asp:LinkButton>
                 </span>
                 <div id="ComLists" runat="server" style="display:none;">
                     <ul>
                      <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommmand">
                  <ItemTemplate>
                        <li><span class="spanU"><%#getUserface(Eval("UserID","{0}")) %></span>
                        <p><span><%#getUserInfo(Eval("UserID","{0}")) %></span><%#Eval("CommentTime") %>
                        <br /><%#Eval("Contents")%>
                        </p>
                        <div class="clear"></div>
                        <span class="spedt">
                        <a href="#" title="" onclick="GetID('<%#Eval("Contents")%>','<%#Eval("CommentID")%>')">编辑</a>
                        <asp:LinkButton ID="del" runat="server" CommandArgument='<%#Eval("CommentID")%>' CommandName="Del" Text="删除"></asp:LinkButton></span></li>
                </ItemTemplate>
       </asp:Repeater>
     <li>
        <input id="HidComID" type="hidden" />
        <input type="hidden" id="HidID" runat="server" />
     </li> 
  </ul>
       </div>
            </div>
<%--            <div id="ComLists">
                <iframe width="680px" src="ComList.aspx?pid="></iframe>
            </div>--%>
        </div>
      </div>
</asp:Content>

 