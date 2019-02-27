<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="FiServer.aspx.cs" Inherits="manage_iServer_FiServer" ClientIDMode="Static" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>有问必答</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="cnt"></div>
    <div class="container margin_t5">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">有问必答</li>
        <li style="width:240px; height:22px;">
            <div class="input-group" style="width: 200px; top:-26px; left:30px;">
                <asp:TextBox ID="TextBox1" runat="server" Text="" class="form-control input-control" placeholder="请输入标题"></asp:TextBox>
                <span class="input-group-btn">
                    <button class="btn btn-default" type="button" onserverclick="Button1_Click" onclick="SearchPage();" runat="server"><span class="fa fa-search"></span></button>
                </span>
           </div>            
        </li>
        <div class="clearfix"></div>
    </ol>
</div>
    <div class="container">
    <div runat="server" id="Login" visible="false">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td colspan="2" class="text-center"><font color="red">本页需支付密码才能登录请输入支付密码</font></td>
            </tr>
            <tr>
                <td style="width: 50%;" class="text-right">
                    <asp:TextBox ID="Second" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="sure" CssClass="btn btn-primary" runat="server" Text="确定" OnClick="sure_Click" />
                </td>
            </tr>
        </table>
    </div>
</div>
    <div class="container btn_green">
    <div runat="server" id="DV_show">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <div id="viewPanel">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <table class="table table-striped table-bordered table-hover">
                                        <tr>
                                            <td colspan="2" class="title" style="text-align: center">有问必答
                                            </td>
                                        </tr>
                                        <tr align="left" class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                                            <td width="100">
                                                <a href="SelectiServer.aspx"><b>总数:</b></a>
                                            </td>
                                            <td>&nbsp;<asp:Label ID="lblAllNum" runat="server" Text="0" onmouseover="onUP(this)" onmouseout="onDown(this)"  onclick="jump(this,-1)"></asp:Label>
                                                <div id="typeList" class="btn-group" style="margin-left:100px;">
                                                    <asp:Repeater ID="repSeachBtn" runat="server">
                                                        <ItemTemplate>
                                                            <a name="type" href='SelectiServer.aspx?type=<%# returnType(Eval("type")) %>' target="_self"><%# Eval("type") %></a>&nbsp;&nbsp;
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <a href="SelectiServer.aspx" target="_self">All&gt;&gt;</a>&nbsp;&nbsp;
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </td>
                                        </tr>
                                        <asp:Panel ID="panel_w" runat="server" Visible="false">
                                            <tr align="left" class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                                                <td>
                                                    <a href="SelectiServer.aspx?num=1"><font color="red">未解决:</font></a>
                                                </td>
                                                <td>&nbsp;<asp:Label ID="lblnum_w" runat="server" Text=""  onmouseover="onUP(this)" onmouseout="onDown(this)"  onclick="jump(this,1)"></asp:Label>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="panel_ch" runat="server" Visible="false">
                                            <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                                                <td>
                                                    <a href="SelectiServer.aspx?num=2"><font color="brown">处理中:</font></a>
                                                </td>
                                                <td>&nbsp;<asp:Label ID="lblNum_ch" runat="server" Text="" onmouseover="onUP(this)" onmouseout="onDown(this)"  onclick="jump(this,2)"></asp:Label>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="panel_y" runat="server" Visible="false">
                                            <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                                                <td>
                                                    <a href="SelectiServer.aspx?num=3"><font color="green">已解决:</font></a>
                                                </td>
                                                <td>&nbsp;<asp:Label ID="lblnum_y" runat="server" Text="" onmouseover="onUP(this)" onmouseout="onDown(this)"  onclick="jump(this,3)"></asp:Label>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="panel_sock" runat="server" Visible="false">
                                            <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                                                <td>
                                                    <a href="SelectiServer.aspx?num=4"><font color="gray">已锁定:</font></a>
                                                </td>
                                                <td>&nbsp;<asp:Label ID="lblSock" runat="server" Text="" onmouseover="onUP(this)" onmouseout="onDown(this)" onclick="jump(this,4)"></asp:Label>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                    </table>
                           
                                    
                                    <!-- Nav tabs -->
                                    <table class="table table-striped table-bordered table-hover">
                                        <tr class="tdbg">
                                            <td>
                                                <ul class="nav nav-tabs" role="tablist" id="myTab">
                                                  <li class="active"><a href="#tab1" id="d" onclick="javascript:location.href='FiServer.aspx?num=1'" role="tab" data-toggle="tab">待解决问题</a></li>
                                                  <li><a href="#tab2" onclick="javascript:location.href='FiServer.aspx?num=2'" role="tab" data-toggle="tab">处理中问题</a></li>
                                                  <li><a href="#tab3" onclick="javascript:location.href='FiServer.aspx?num=3'" role="tab" data-toggle="tab">已经解决问题</a></li>
                                                </ul>
                                                <div style="position:relative; top:-36px; margin-left:350px;">
                                                    <a href="SelectiServer.aspx">所有问题</a><a style="margin-right:40px;">&nbsp;</a>
                                                  <button type="button" style="position:relative; top:-6px;" class="btn btn-primary" onclick="javascript:location.href='AddQuestion.aspx'">
                                                     <span class="fa fa-plus"></span> 提交新问题
                                                  </button>
                                                </div> 
                                                <table class="table table-striped table-bordered table-hover">
                                                    <asp:Repeater ID="resultsRepeater_w" runat="server">
                                                        <HeaderTemplate>
                                                            <tr class="title" style="height: 24px; text-align: center">
                                                                <td>编号</td>
                                                                <td>标题</td>
                                                                <td>优先级</td>
                                                                <td>问题类型</td>
                                                                <td>已读次数</td>
                                                                <td>提交时间</td>
                                                                <td>状态</td>
                                                            </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr align="center" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'" class="tdbg">
                                                                <td><%# Eval("QuestionId")%></td>
                                                                <td>
                                                                    <a href="FiServerInfo.aspx?QuestionId=<%#Eval("QuestionId")%>"><%# ZoomLa.Common.BaseClass.Htmlcode(Eval("Title", "{0}"))%><a>
                                                                </td>
                                                                <td><%# Eval("Priority")%></td>
                                                                <td><a href='SelectiServer.aspx?type=<%# returnType(Eval("type")) %>' target="_self"><%# Eval("type") %></a></td>
                                                                <td><%# Eval("ReadCount")%></td>
                                                                <td><%# Eval("SubTime")%></td>
                                                                <td>
                                                                    <asp:Label ID="lblState" runat="server" ForeColor="Red" Text='<%# Eval("State")%>'></asp:Label></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater> 
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <%
            if (Request.QueryString["OrderID"] != null && Request.QueryString["OrderID"] != "")
            {
        %>
        <div style="width: 100%; float: right">
            <input id="btnreturn" class="btn btn-primary" type="button" style="float: right;" value="返回订单" onclick="javascript:location.href='/user/PreViewOrder.aspx?menu=ViewOrderlist&id=<%=Request.QueryString["OrderID"] %>'" />
        </div>
        <%} %>
    </div> 
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        if (getParam("num"))
        {
            $("li a[href='#tab" + getParam("num") + "']").parent().addClass("active").siblings("li").removeClass("active");;
        };
        $(function () {
            var num = $("#typeList").find("a[name='type']").length;
            if (parseInt(num) == 0)
                $("#typeList").hide();
        });
        function jump(obj,num)
        {
            var name=$(obj).text();
            if(parseInt(name)>0)
            {
                if(num>0)
                    window.location.href="SelectiServer.aspx?num="+num;
                else
                    window.location.href="SelectiServer.aspx";
            }                
        }
        function onUP(obj)
        {
            var name=$(obj).text();
            if(parseInt(name)>0)
                $(obj).css({"color":"#428bca","cursor":"pointer","text-decoration":"underline"});
            else
                $(obj).css({"cursor":"default"});
        }
        function onDown(obj)
        {
            $(obj).css({"color":"#000","cursor":"default","text-decoration":"none"});
        }

    </script>
</asp:Content>
