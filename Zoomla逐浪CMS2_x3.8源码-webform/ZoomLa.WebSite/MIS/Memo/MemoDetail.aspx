<%@ Page  Language="C#" AutoEventWireup="true" CodeFile="MemoDetail.aspx.cs" MasterPageFile="~/Common/Master/Empty.master" Inherits="MIS_Memo_MemoDetail" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>备忘列表</title>
<link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script type="text/javascript">
        function loadPage(id, url) {
            $("#" + id).addClass("loader");
            $("#" + id).append("Loading......");
            $.ajax({
                type: "get",
                url: url,
                cache: false,
                error: function (){ alert('加载页面' + url + '时出错！'); },
                success: function (msg) {
                    $("#" + id).empty().append(msg);
                    $("#" + id).removeClass("loader");
                }
            });
        }
        $(function () {

        })
       // window.onload = function () { PopupDiv('div_warn', 'txtWarn', 'warn');   }

        //弹出div
        function PopupDiv(div_id,txt_sorce, type) {
            SetOptions(txt_sorce, type);
            var div_obj = $("#" + div_id);
            div_obj.animate({opacity : "show", left :300, top: 200, width: div_obj.width, height: div_obj.height } , 300);
            div_obj.focus();
        }

        //弹出提示内容div
        function PopupDiv(div_id) {
            var div_obj = $("#" + div_id);
            div_obj.animate({ opacity: "show", left: 300, top: 200, width: div_obj.width, height: div_obj.height }, 300);
        }

        //隐藏div
        function HideDiv(div_id) {
            $("#" + div_id).animate({ opacity: "hide" }, 300);
            var init = self.setInterval("AlertForm()",31000);
            window.clearInterval(init);
        }

        // 填充选择项
        function SetOptions(txt_id, type) {
            if ($("#" + txt_id).val().length == 0)
                return;
            switch (type) {
                case "warn":
                    SetWarn(txt_id);
                    break;
                case "share":
                    SetShare(txt_id);
                    break;
            }
        }
        //显示选择项
        function GetOptions(div_id, txt_id, type) {
            var options = "";
            switch (type) {
                case "warn":
                    options = GetWarn(div_id);
                    break;
                case "share":
                    options = GetShare(div_id);
                    break;
            }
            $("#" + txt_id).val(options);
            HideDiv(div_id);
        }
        //获取 提醒消息：
        function GetWarn(div_id) {
            var warn = "";
            warn = warn + $("#w_fqRepeat").find("option:selected").text() + "|";
            warn += $("#w_date").val() + "|";
            warn += $("#w_time").val() + "|";
            warn += $("#w_method").val();
            warn += "|";
            $(":checkbox[name=chkpWarn]:checked").each(function () {
                warn += $(this).val() + ",";
            });
            return warn;
        }

        //获取共享信息
        function GetShare(div_id) {
            var share = " ";
            $(":checkbox[name=chkUser]:checked").each(function () {
                share += $(this).val() + ",";
            });
            share=share.trim(",");
            return share;
        }

        //设置 提醒消息
        function SetWarn(txt) {
            var arr = $("#" + txt).val().split("|");
            $("#w_fqRepeat").val(arr[0]);
            $("#w_date").val(arr[1]);
            $("#w_time").val(arr[2]);
            $(":checkbox[name=chkpWarn]").each(function () {
                if (arr[3].indexOf($(this).val()) >= 0)
                    $(this).attr("checked", true);
                else
                    $(this).attr("checked", false);
            });
            $("w_method").val(arr[4]);
        }

        function AlertForm() {
            var DateTimes = new Date();
            var year = DateTimes.getFullYear();
            var Month = DateTimes.getMonth() + 1;
            if (Month < 10)
            {
                Month = "0" + Month;
            }
            var Day = DateTimes.getDate();
            if (Day < 10)
            {
                Day = "0" + Day;
            }
            var Hours = DateTimes.getHours();
            if (Hours < 10)
            {
                Hours = "0" + Hours;
            }
            var Minutes = DateTimes.getMinutes();
            if (Minutes < 10)
            {
                Minutes = "0" + Minutes;
            }
            var compair =year+"-"+Month+"-"+Day+"|"+Hours+":"+ Minutes;
            var TimeofWarn = document.getElementById("lblWarn").innerText;
            TimeofWarn = TimeofWarn.substring(TimeofWarn.indexOf("|")+1, TimeofWarn.indexOf("|") + 17);
            //alert(compair);
            //alert(TimeofWarn);
            if (TimeofWarn == compair){
                PopupDiv("TimeDiv");
            }
            else
            {
                HideDiv("TimeDiv");
            }
        }
        setInterval("AlertForm()",31000);
        //设置 共享信息
        function SetShare(txt) {
            
        }

        function LodePage(id,Cont) {
            document.getElementById("HidCommentInfo").value = id;
            document.getElementById("Txtco").innerText = Cont;
            var h = document.getElementById("Contents" + id).getBoundingClientRect().top;
            document.getElementById("hidenComment").style.display = "block";
            document.getElementById("hidenComment").style.top = (h - 104) + "px";
            document.getElementById("hidenComment").style.background = "#fdfce6";
        }

        //加载时间
            function show2() {
                var divs=document.getElementById("DateDiv");
                var Digital = new Date()
                var hours = Digital.getHours()
                var minutes = Digital.getMinutes()
                var seconds = Digital.getSeconds()
                var dn = "AM"
                if (hours > 12) {
                    dn = "PM";
                    hours = hours - 12;
                }
                if (hours == 0)
                    hours = 12;
                if (minutes <= 9)
                    minutes = "0" + minutes;
                if (seconds <= 9)
                    seconds = "0" + seconds;
                var ctime = hours + ":" + minutes + ":" + seconds + " " + dn;
                divs.innerHTML = ctime;
                setTimeout("show2()", 1000);
            }
            window.onload = show2;
    </script>
     <style type="text/css">
        #CommentAdds{ height: 44px;
            width: 716px;
        }
.spanU{float:left; width:35px;padding-top:8px;}
.uface{width:35px;height:35px;}
.divuinfo{ margin-top:30px; padding-right:10px;}
.divuinfo p{float:left; padding:0; margin:0; padding-left:10px;  }
.divuinfo li{border-bottom:dotted 1px #ccc;}
.divuinfo .spedt{ display:block; text-align:right; float:right; margin-top:-24px;}

#Contents{background-color:#808080; width:680px; height:228px; padding-top:-100px;}
#Close { text-align:right;}
#head{ padding-top:-50px;}
.TextDiv{background-color:#fff; width:660px; margin-left:10px;}
#TxtComment{}
#BtnSubmit{ text-align:right; padding-right:10px;}
#hidenComment{display:none;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <div id="Meno">
            <div id="Meno_left">
<%--                <div class="Meno_new"><a href="AddMemo.aspx"  target="ifMemo">新建备忘</a></div>--%>
                <div class="Meno_li">
                    <ul>
                        <li class="Meno_new"><a href="AddMemo.aspx">新建备忘</a></li>
                        <li class="Meno_all"><a href="Default.aspx">全部备忘</a></li>
                        <li class="Meno_my"><a href="Default.aspx?UName=<%= buser.GetLogin().UserName%>">我的备忘  </a></li>
                        <li class="Meno_share"><a href="Default.aspx?Sname=<%= buser.GetLogin().UserName %>">共享给我  </a></li>
                        <li class="Meno_pin"><a href="Default.aspx?Mname=<%= buser.GetLogin().UserName %>">我的评论</a></li>
                    </ul>
                </div>
            </div>
             <div>
            <div id="Meno_right">
           <div id="Meno_Title">
            <a href="Default.aspx">全部备忘</a> &gt; &gt; 备忘详情 
        </div>
        <div>
                  <div style="padding-left:5px;">
                        <asp:LinkButton ID="LinkEdit" runat="server" Text="编辑" OnClick="LinkEdit_Click"></asp:LinkButton>&nbsp;
                        <asp:LinkButton ID="LinkDelete" OnClientClick="return confirm('确定要删除该记录么？')" runat="server" Text="删除" OnClick="LinkDelete_Click"></asp:LinkButton>&nbsp;
                        <asp:LinkButton ID="LinkBtn" runat="server" OnClick="LinkBtn_Click" Text="取消共享"></asp:LinkButton>
                        <asp:Label ID="lblShares" runat="server" Text="设置共享"><a href="#" title="">设置共享</a></asp:Label>&nbsp;
                        <asp:Label ID="lblWarns" runat="server" Text="提醒"><a href="#" title="">提醒</a></asp:Label>&nbsp;
                        <asp:Label ID="labBack" runat="server" Text=""><a href="Default.aspx" title="">返回</a></asp:Label>

                    </div>
                    <div class="ctop" style="padding: 5px;">
                        <span class="author" style="color: #278139;">创建者：<asp:Label ID="lblInputer" runat="server"></asp:Label>  </span>&nbsp; &nbsp; 
                        <span>创建于：<asp:Label ID="lblCreateTime" runat="server"></asp:Label>  </span></div>
                    <div style="margin-left: 4px;">共享人：<asp:Label ID="lblIsShare" runat="server"></asp:Label>   </div>
                    <div style="margin-left: 4px;">备忘内容：<asp:Label ID="lblContent" runat="server" ForeColor="#FF3300"></asp:Label></div>
                    <div style="margin-left: 4px;">提醒：<asp:Label ID="lblWarn" runat="server"></asp:Label></div>
              </div>
                <div id="CommentCount">评论<span><asp:Label ID="lblCount" runat="server" ForeColor="Red"></asp:Label></span></div>
                <div id="CommentAdds">
                    <div><asp:TextBox ID="TxtComment" runat="server" Height="46px" TextMode="MultiLine" Width="708px"></asp:TextBox></div>
                    <asp:CheckBox  ID="ChkAllUser" runat="server" Text="提醒所有人共享"/><span style="float:right;"><asp:Button ID="SubBtn" runat="server" Text="提交" OnClick="SubBtn_Click" CssClass="i_bottom" /></span>
                </div>
                    <div class="divuinfo">
                        <ul>
                    <asp:Repeater id="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommmand">
                        <ItemTemplate>
                           <li> <span class="spanU"><%#getUserface(Eval("UserID","{0}")) %></span><p><span><%#getUserInfo(Eval("UserID","{0}")) %>&nbsp;&nbsp;</span><%#Eval("CommentTime") %><br /><%#Eval("Contents") %></p>
         <div class="clear"></div>
       <span class="spedt"><asp:LinkButton ID="del" runat="server" CommandArgument='<%#Eval("CommentID") %>' CommandName="Del" OnClientClick="return confirm('确定要删除该记录么？')" Text="删除"></asp:LinkButton>
       <a href="#" title="" onclick="LodePage('<%#Eval("CommentID")%>','<%#Eval("Contents")%>')">编辑</a>
           <div id='Contents<%#Eval("CommentID")%>'></div>
           </span>
           </li>
                        </ItemTemplate>
                    </asp:Repeater>
                       </ul>
                </div>
        <div id="hidenComment">
        <div id="Contents">
       <div id="heads">
          <div id="Close"><a href="#" title="" onclick="HideDiv('hidenComment')">关闭</a></div>
      </div>
          <div class="TextDiv">
              <asp:TextBox ID="Txtco" runat="server" TextMode="MultiLine" Height="139px" Width="658px"></asp:TextBox>
              <br />
              <asp:CheckBox ID="CbkEdit" runat="server" Text="提醒所有人共享"/>
              <input ID="HidCommentInfo" type="hidden" value="11" runat="server" />
              <input id="HidContents" type="hidden"  runat="server" />
          </div>
          <div id="BtnSubmit">
              <asp:Button ID="BtnSub" runat="server"  Text="提交" OnClick="BtnSub_Click"/>&nbsp;<input type="button"  value="取消" onclick="HideDiv('Contents')"/>
          </div>
    </div>
       </div>
            </div>
          </div>
 

        <div id="div_share" class="pop_box">
                <div class="p_head">
                    <div class="p_h_title">设置共享</div>
                    <div class="p_h_close" onclick="HideDiv('div_share')">关闭</div>
                </div>
                <div class="p_body">
                                <div>用户列表：</div> 
                    <div id="Div1">
                        <ul>
                        <asp:Repeater ID="rptUserLists" runat="server">
                            <ItemTemplate>
                        <li><input type="checkbox" name="chkUser" value='<%# Container.DataItem%>'/><%# Container.DataItem%></li>
                            </ItemTemplate>
                        </asp:Repeater>
                            </ul>
                        </div> 
                    <div style="margin-left:270px; clear:both;"> 
                        <asp:Button ID="btnNext" runat="server" Text="换一组" OnClick="btnNext_Click" UseSubmitBehavior="false"/>
                    </div>
                </div>
                <div class="p_bottom">
                    <div style="display:none;"><input type="text" ID="TxtUser" runat="server" Text=""/></div>
                    <asp:Button ID="BtnYess" runat="server" Text="确定"  OnClientClick="GetOptions('div_share', 'TxtUser', 'share')" OnClick="BtnYes_Click" />
                    &nbsp;
                   <asp:Button ID="Button1" runat="server" Text="取消"  OnClientClick="HideDiv('div_warn')" />
                </div>
            </div>

        <div id="div_warn"  class="pop_box">
                <div class="p_head">
                    <div class="p_h_title">设置提醒</div>
                    <div class="p_h_close" onclick="HideDiv('div_warn')">关闭</div>
                </div>
                <div class="p_body">
                    <table style="width: 90%; margin: 0 auto;">
                        <tr>
                            <td>重复频率:</td>
                            <td>
                                <select id="w_fqRepeat">
                                    <option value="不重复" selected>不重复</option>
                                    <option value="每天">每天</option>
                                    <option value="每周">每周</option>
                                    <option value="每月">每月</option>
                                    <option value="每年">每年</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>提醒日期:</td>
                            <td>
                                <input id="w_date" type="text" value='<%= DateTime.Now.ToString("yyyy-MM-dd") %>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'});" /></td>
                        </tr>
                        <tr>
                            <td>提醒时间:</td>
                            <td>
                                <input id="w_time" type="text" value='<%= DateTime.Now.AddHours(1).ToString("HH:mm") %>' onfocus="WdatePicker({dateFmt:'HH:mm'});" /></td>
                        </tr>
                        <tr>
                            <td>提醒人员:</td>
                            <td>
                                <input type="checkbox" name="chkpWarn" value="本人" checked />本人
                                <input type="checkbox" name="chkpWarn" value="所有共享人" />所有共享人
                            </td>
                        </tr>
                        <tr>
                            <td>提醒方式:</td>
                            <td>
                                <select id="w_method">
                                    <option value="即时通讯" selected>即时通讯</option>
                                    <option value="Email">Email</option>
                                    <option value="手机短信">手机短信</option>
                                </select>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="p_bottom">
                    <div style="display:none"><input type="text" ID="TxtCall" runat="server" Text=""/></div>
                    <asp:Button ID="BtnWarn" runat="server"  Text="确定" OnClientClick="GetOptions('div_warn', 'TxtCall', 'warn');" OnClick="BtnWarns_Click" />
                    &nbsp;
                    <asp:Button ID="BtnReset" runat="server" Text="取消" OnClientClick="HideDiv('div_warn')" />
                </div>
            </div>

           <div id="TimeDiv" class="pop_box" style="margin-left:298px;margin-top:450px;">
                 <div id="TimeHead" style="text-align:right; font-size:14px;" class="p_head">
                    <div class="p_h_title"></div>
                    <div class="p_h_close" onclick="HideDiv('TimeDiv')">关闭</div>
                 </div>
                 <div id="TimeInfo">
                     <div id="DateDiv" style="font-size:20px; color:#278139;font-weight:bold;text-align:right;"></div>
                     <div id="warnContent" style="height:100px;">
                         提醒内容：<asp:Label ID="lblWarnContent" runat="server" Text=""></asp:Label><br />
                         提示人员：<asp:Label ID="lblPeson" runat="server" Text=""></asp:Label><br /><br />
                         提示时间：<asp:Label ID="lblWarnTime" runat="server" Text="Label"></asp:Label>
                     </div>
                 </div>
           </div>
        </div>
    <div id="LoadPages"></div>

</asp:Content>
