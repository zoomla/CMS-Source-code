<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="ZoomLaCMS.Manage.User.Message"  MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>信息管理</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol class="breadcrumb navbar-fixed-top">
    <li><a href='<%=CustomerPageAction.customPath2 %>Main.aspx'><%=Resources.L.工作台 %></a></li>
    <li><a href='UserManage.aspx'>用户管理</a></li>
    <li><a href='MessageSend.aspx'>信息发送</a></li>
    <li class="active">短消息列表</li>
    <div id="help" class="pull-right text-center"><a href="javascript::" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a></div>
    <div id="sel_box" runat="server" class="padding5">
        <div style="display: inline-block; width: 230px;">
            <div class="input-group" style="position: relative; margin-bottom: -12px;">
                <asp:TextBox ID="Skey_T" placeholder="输入短消息主题" runat="server" CssClass="form-control text_md" />
                <span class="input-group-btn">
                    <asp:Button ID="Search_B" runat="server" Text="<%$Resources:L,搜索 %>" class="btn btn-primary" OnClick="Search_B_Click" />
                </span>
            </div>
        </div>
    </div>
</ol>
<div class="template" id="template" runat="server">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td style="text-align:right;width:25%;"><strong>批量删除会员（发件人）短消息：<br />
            </strong>可以用英文状态下的逗号将用户名隔开实现多会员同时删除 </td>
            <td>
                <div class="input-group" style="width: 380px;padding-top:3px;">
                    <asp:TextBox class="form-control" ID="TxtSender" runat="server" style="float:left;margin-right:5px;"></asp:TextBox>
                    <span class="input-group-btn">
                        <asp:Button ID="BtnDelSender" CssClass="btn btn-primary" runat="server" Text="删除" OnClientClick="return confirm('确定要删除吗？');" OnClick="BtnDelSender_Click" class="btn btn-primary"/>
                    </span>
                </div>  
            </td>
            <td><strong>批量删除指定日期范围内的短消息：<br />
            </strong>默认为删除已读信息 </td>
            <td style="height: 28px; width: 511px;">
                <asp:DropDownList ID="DropDelDate" runat="server" class="btn btn-default dropdown-toggle">
                    <asp:ListItem Value="1">一天前</asp:ListItem>
                    <asp:ListItem Value="3">三天前</asp:ListItem>
                    <asp:ListItem Value="7">一星期前</asp:ListItem>
                    <asp:ListItem Value="30">一个月前</asp:ListItem>
                    <asp:ListItem Value="60">两个月前</asp:ListItem>
                    <asp:ListItem Value="180">半年前</asp:ListItem>
                    <asp:ListItem Value="0">所有短消息</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="BtnDelDate" runat="server" Text="删除" OnClientClick="return confirm('确定要删除吗？');" OnClick="BtnDelDate_Click"  class="btn btn-primary"/>
            </td>
        </tr>
    </table>
    <ul class="nav nav-tabs hidden-xs hidden-sm">
        <li><a href="#tab_all" data--toggle="tab" onclick="showtabs('all')">全部</a></li>
        <li><a href="#tab_sys" data-toggle="tab" onclick="showtabs('sys')">系统消息</a></li>
        <li><a href="#tab_mb" data-toggle="tab" onclick="showtabs('mb')">手机短信</a></li>
        <li><a href="#tab_code" data-toggle="tab" onclick="showtabs('code')">验证码</a></li>
    </ul>
    <ZL:ExGridView ID="EGV" runat="server" Width="100%"  AutoGenerateColumns="False"  OnRowCommand="Row_Command"  AllowPaging="true" EmptyDataText="没有数据" OnPageIndexChanging="EGV_PageIndexChanging" CssClass="table table-striped table-bordered table-hover" onrowdatabound="EGV_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="idChk" value="<%#Eval("MsgID") %>"/>
                </ItemTemplate>
                <ItemStyle Width="5%" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="短消息主题">
                <ItemTemplate> <a href="MessageRead.aspx?id=<%#Eval("MsgID")%>" title='<%# Eval("Title")%>'> <%# Eval("Title")%></a> </ItemTemplate>
                <ItemStyle Width="20%" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="短消息类型">
                <ItemTemplate><%#GetMsgType(Convert.ToInt32(Eval("MsgType")))%></ItemTemplate>
                <ItemStyle Width="8%" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="收件人">
                <ItemTemplate>
                    <%#Eval("Incept","{0}") %>
                    <headerstyle Width="13%" />
                    <itemstyle HorizontalAlign="Center" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="收件人状态">
                <HeaderStyle Width="9%" />
                <ItemTemplate> <%#GetStatus(Convert.ToInt32(Eval("MsgID")),"incept")%> </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="发件人">
                <ItemTemplate>
                    <%#Eval("Sender","{0}") %>
                </ItemTemplate>
                <ItemStyle Width="13%" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="发件人状态">
                <ItemTemplate> <%#GetStatus(Convert.ToInt32(Eval("MsgID")),"sender")%> </ItemTemplate>
                <ItemStyle Width="9%" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="发送日期">
                <ItemTemplate> <%# Eval("PostDate","{0:yyyy-MM-dd}")%> </ItemTemplate>
                <ItemStyle Width="13%" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDel" runat="server" CommandName="DeleteMsg" OnClientClick="if(!this.disabled) return confirm('确定删除此信息?');"  CommandArgument='<%# Eval("MsgID")%>' CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                    <asp:LinkButton ID="lbRead" runat="server" CommandName="ReadMsg" CommandArgument='<%# Eval("MsgID")%>' CssClass="option_style"><i class="fa fa-eye" title="阅读"></i>阅读信息</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" Width="9%" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView> 
    <asp:Button ID="Button2" runat="server" Font-Size="9pt" Text="批量删除" OnClientClick="return confirm('确定要删除这些记录吗？');" OnClick="Button2_Click" class="btn btn-primary" /><br />
</div>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        $().ready(function () {
            $("#EGV tr>th:eq(0)").html("<input type=checkbox id='chkAll' />");
            $("#chkAll").click(function () {
                SelecteAllByName(this, "idChk");
            });
            $("ul.nav li").removeClass("active");
            $("a[href='#tab_<%=view%>']").parent().addClass("active");
        });
        function SelecteAllByName(obj,name) {
            var input = document.getElementsByName("idChk");
            var len = input.length;
            for (var i = 0; i < len; i++) {
                if(input[i].type=="checkbox"){
                    input[i].checked = obj.checked;
                }
            }
        }
        $("#sel_btn").click(function (e) {
            if ($("#sel_box").css("display") == "none") {
                $(this).addClass("active");
                $("#sel_box").slideDown(300);
                $(".template").css("margin-top", "44px");
            }
            else {
                $(this).removeClass("active");
                $("#sel_box").slideUp(200);
                $(".template").css("margin-top", "0px");
            }
        });
        function showtabs(view) {
            location.href = "Message.aspx?view=" + view;
        }
    </script>
</asp:Content>
