<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMemo.aspx.cs" MasterPageFile="~/Common/Master/Empty.master" Inherits="MIS_Memo_New" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>编辑页</title>
    <script src="/JS/DatePicker/WdatePicker.js"></script>
    <script src="/JS/Common.js"></script>
    <link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript"> 
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


        window.onload = function () {
        };

       $(function () {
           $("#txtMTitle").focus();
           $("#txtMTitle").blur(function () {
               if ($("#txtMTitle").val().length > 0)
                   $("#errMsgTitle").css("display", "none");
           })
           $("#txtContent").blur(function () {
               if ($("#txtContent").val().length > 0)
                   $("#errMsgContent").css("display", "none");
           })
       });
       function CheckNull()
       {
           if ($("#txtMTitle").val().length == 0)
           {
               $("#errMsgTitle").css("display", "block");
               return false;
           }
           //if ($("#txtContent").val().length == 0)
           //{
           //    $("#errMsgContent").css("display", "block");
           //    return false;
           //}
           return true;
       }
       //弹出div
       function PopupDiv(div_id,txt_sorce, type) {
         SetOptions(txt_sorce,type);
           var div_obj = $("#" + div_id);       
           div_obj.animate({opacity: "show", left:300, top: 200, width: div_obj.width, height: div_obj.height }, 300);
           div_obj.focus();
       }
       //隐藏div
       function HideDiv(div_id) {
           $("#" + div_id).animate({ opacity: "hide" }, 300);
       }

        // 填充选择项
       function SetOptions(txt_id, type)
       {
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
       function GetOptions(div_id, txt_id, type)
       {
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
       function GetWarn(div_id)
       {
           var warn = "";
           warn = warn + $("#w_fqRepeat").find("option:selected").text() +"|";
           warn += $("#w_date").val() + "|";
           warn += $("#w_time").val() + "|";
           $(":checkbox[name=chkpWarn]:checked").each(function () {
               warn += $(this).val() + ",";
           });
           warn = warn.Trim(",");
           warn += "|";
           warn += $("#w_method").val();
           return warn;
       }
        //获取共享信息
       function GetShare(div_id)
       {
           var share = "";
           $(":checkbox[name=chkUser]:checked").each(function () {
               share += $(this).val() + ",";
           });
           return share.Trim(",");
       }

        //设置 提醒消息
       function SetWarn(txt)
       {
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

        //设置 共享信息
       function SetShare(txt)
       {

       }

       function SelectUser() {
           window.open('/Mis/SelUser.aspx?TypeSelect=UserList&OpenerText=<%=txtShare.ClientID %>','', 'width=600,height=450,resizable=0,scrollbars=yes');
            }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="Meno">
            <div id="Meno_left">
<%--                <div class="Meno_new"><a href="AddMemo.aspx" target="ifMemo">新建备忘</a></div>--%>
                <div class="Meno_li">
                    <ul>
                        <li class="Meno_new"><a href="AddMemo.aspx">新建备忘</a></li>
                        <li class="Meno_all"><a href="Default.aspx">全部备忘 </a></li>
                        <li class="Meno_my"><a href="Default.aspx?UName=<%= buser.GetLogin().UserName %>">我的备忘  </a></li>
                        <li class="Meno_share"><a href="Default.aspx?Sname=<%= buser.GetLogin().UserName %>">共享给我  </a></li>
                        <li class="Meno_pin"><a href="Default.aspx?Mname=<%= buser.GetLogin().UserName %>">我的评论</a></li>
                    </ul>
                </div>
            </div>
            <div id="Meno_right"  style="width:715px;">
            <div>
            <div class="Mis_Title">
                <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
            </div>
            <table width="100%" border="0" cellpadding="10" cellspacing="5" style="margin: 0 auto;">
                <tr>
                    <td style="width: 150px; text-align: center">
                        <span style="color: red">*</span> 提醒标题:</td>
                    <td>
                        <asp:TextBox ID="txtMTitle" runat="server" Columns="80"></asp:TextBox>
                          <span id="errMsgTitle" style="display:none; color: red;" runat="server">请填写备忘信息的标题</span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px; vertical-align: top; text-align: center"><span style="color: red">*</span> 备忘信息:</td>
                    <td>
                        <asp:TextBox ID="txtContent" Width="620px" Height="150px" TextMode="MultiLine"  OnTextChanged="txtContent_TextChanged" runat="server"></asp:TextBox>
                        <span id="errMsgContent" style="display : none; color: red;" runat="server">请填写你的备忘信息</span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px; text-align: center">设置共享:</td>
                    <td>
                        <asp:TextBox ID="txtShare" runat="server" Columns="60"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px; text-align: center">设置提醒:</td>
                    <td>
                        <asp:TextBox ID="txtWarn" runat="server" Columns="60"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button Text="提交" runat="server" ID="btnSubmit" CssClass="i_bottom" OnClick="Button_Click" OnClientClick="return CheckNull();" />&nbsp; &nbsp;     
                        <input id="btnReturn" type="button" value="取消" class="i_bottom" onclick="location.href = 'Default.aspx'" />
                    </td>
                </tr>
            </table>
            <div id="div_warn" class="pop_box">
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
                    <input type="button" class="i_bottom" value="确定" onclick="GetOptions('div_warn', 'txtWarn', 'warn');" />
                    &nbsp;
                    <input type="button" class="i_bottom" value="取消" onclick="HideDiv('div_warn')" />
                </div>
            </div>
            <div id="div_share" class="pop_box">
                <div class="p_head">
                    <div class="p_h_title">设置共享</div>
                    <div class="p_h_close" onclick="HideDiv('div_share')">关闭</div>
                </div>
                <div class="p_body">
                                <div>
                                    用户列表： </div> 
                    <div id="userlst">
                        <ul>
                        <asp:Repeater ID="rptUserLists" runat="server">
                            <ItemTemplate>
                                <li><input type="checkbox" name="chkUser" value='<%# Container.DataItem%>' /><%# Container.DataItem%></li>
                            </ItemTemplate>
                        </asp:Repeater>
                            </ul>
                        </div> 
                    <div style="margin-left:270px; clear:both;"> 
                        <asp:Button ID="btnNext" runat="server" Text="换一组" OnClick="btnNext_Click" UseSubmitBehavior="false"/>
                    </div>
                </div>
                <div class="p_bottom">
                    <input type="button" value="确定" class="i_bottom" onclick="GetOptions('div_share', 'txtShare', 'share')" />
                    &nbsp;
                    <input type="button" value="取消" class="i_bottom" onclick="HideDiv('div_share')" />
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
                <div id="Div1">
                    <ul>
                        <li>jiefja</li>
                        <li>jiefja</li>
                        <li>jiefja</li>
                    </ul>
                </div>
            </div>
        </div>
</asp:Content>
  