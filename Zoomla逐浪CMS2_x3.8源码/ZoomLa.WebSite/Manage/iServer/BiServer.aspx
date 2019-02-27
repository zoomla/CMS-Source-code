<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BiServer.aspx.cs" Inherits="manage_iServer_BiServer" MasterPageFile="~/Manage/I/Default.master" ClientIDMode="Static" EnableViewStateMac="false" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title><%=lang.LF("有问必答")%></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="input-group" style="width: 200px; position:absolute; top:0px; left:300px;">
        <asp:TextBox ID="TextBox1" runat="server" Text="" class="form-control input-control" placeholder="请输入标题"></asp:TextBox>
        <span class="input-group-btn">
            <button class="btn btn-default" type="submit" onserverclick="Button1_Click" runat="server"><span class="fa fa-search"></span></button>
        </span>
    </div>            
    <div>
        <!-- v3.0.2 -->
        <table class="table">
            <tr>
                <td colspan="2" style="text-align: center">
                    <table class="table table-striped table-bordered table-hover">
                        <tr>
                            <td colspan="2" class="title" align="center"><%=lang.LF("有问必答")%></td>
                        </tr>
                        <tr align="left" ondblclick="javascript:window.location.href='BselectiServer.aspx'">
                            <td width="100" align="right"><a href="BselectiServer.aspx"><%=lang.LF("总数")%></a>：</td>
                            <td>
                                <asp:Label ID="lblAllNum" runat="server" Text="0" onmouseover="onUP(this)" onmouseout="onDown(this)"  onclick="jump(this,-1)"></asp:Label>
                                <div id="typeList" class="btn-group" style="margin-left:100px;">
                                    <asp:Repeater ID="repSeachBtn" runat="server">
                                        <ItemTemplate>
                                            <a name="type" href='BselectiServer.aspx?type=<%# returnType(Eval("type")) %>&num=1' target="_self"><%# Eval("type") %></a>&nbsp;&nbsp;
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <a href="BselectiServer.aspx" target="_self">All&gt;&gt;</a>&nbsp;&nbsp;
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </td>
                        </tr>
                        <asp:Panel ID="panel_w" runat="server" Visible="false">
                            <tr align="left" ondblclick="javascript:window.location.href='BselectiServer.aspx?num=1'">
                                <td class="TitleTD" align="right">
                                    <a href="BselectiServer.aspx?num=1"><font color="red"><%=lang.LF("未解决")%></font></a>：
                                </td>
                                <td>
                                    <asp:Label ID="lblnum_w" runat="server" Text="" onmouseover="onUP(this)" onmouseout="onDown(this)"  onclick="jump(this,1)"></asp:Label>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="panel_ch" runat="server" Visible="false">
                            <tr align="left" ondblclick="javascript:window.location.href='BselectiServer.aspx?num=2'">
                                <td class="TitleTD" align="right">
                                    <a href="BselectiServer.aspx?num=2"><font color="brown"><%=lang.LF("处理中")%></font></a>：
                                </td>
                                <td>
                                    <asp:Label ID="lblNum_ch" runat="server" Text="" onmouseover="onUP(this)" onmouseout="onDown(this)"  onclick="jump(this,2)"></asp:Label>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="panel_y" runat="server" Visible="false">
                            <tr align="left" ondblclick="javascript:window.location.href='BselectiServer.aspx?num=3'">
                                <td class="TitleTD" align="right">
                                    <a href="BselectiServer.aspx?num=3"><font color="green"><%=lang.LF("已解决")%></font></a>：
                                </td>
                                <td>
                                    <asp:Label ID="lblnum_y" runat="server" Text="" onmouseover="onUP(this)" onmouseout="onDown(this)"  onclick="jump(this,3)"></asp:Label>
                                </td>
                            </tr>
                        </asp:Panel>
                    </table>
                    <table class="table table-striped table-bordered">
                        <tr>
                            <td>
                                <ul class="nav nav-tabs" role="tablist" id="myTab">
                                    <li class="active"><a href="#tab1" id="d" onclick="javascript:location.href='BiServer.aspx?num=1'" role="tab" data-toggle="tab">待解决问题</a></li>
                                    <li><a href="#tab2" onclick="javascript:location.href='BiServer.aspx?num=2'" role="tab" data-toggle="tab">处理中问题</a></li>
                                    <li><a href="#tab3" onclick="javascript:location.href='BiServer.aspx?num=3'" role="tab" data-toggle="tab">已经解决问题</a></li>
                                </ul>
                                <br />
                                <asp:Repeater OnItemCommand="resultsRepeater_w_ItemCommand" ID="resultsRepeater_w" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-striped table-bordered table-hover" id="EGV">
                                            <tr align="center">
                                                <th class="title"><%=lang.LF("编号")%></th>
                                                <th class="title"><%=lang.LF("标题")%></th>
                                                <th class="title">提交者</th>
                                                <th class="title"><%=lang.LF("优先级")%></th>
                                                <th class="title"><%=lang.LF("问题类型")%></th>
                                                <th class="title"><%=lang.LF("已读次数")%></th>
                                                <th class="title"><%=lang.LF("提交时间")%></th>
                                                <th class="title"><%=lang.LF("状态")%></th>
                                                <th class="title"><%=lang.LF("操作")%></th>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr align="center" ondblclick="javascript:window.location.href='BiServerInfo.aspx?QuestionId=<%#Eval("QuestionId")%>'">
                                            <td>
                                                <%# Eval("QuestionId")%>
                                            </td>
                                            <td>
                                                <a href="BiServerInfo.aspx?QuestionId=<%#Eval("QuestionId")%>">
                                                    <%# ZoomLa.Common.BaseClass.CheckInjection(Eval("Title", "{0}"))%></a>
                                            </td>
                                            <td>
                                                <a data-toggle="modal" data-target="#userinfo_div" onclick="opentitle('../User/Userinfo.aspx?id=<%#Eval("UserId") %>','查看会员')" href="###" title="查看会员"><%#GetUserName(Eval("UserId","{0}"))%></a>
                                            </td>
                                            <td><%# Eval("Priority")%></td>
                                            <td><a href='BselectiServer.aspx?type=<%# returnType(Eval("type")) %>' target="_self"><%# Eval("type") %></a></td>
                                            <td><%# Eval("ReadCount")%></td>
                                            <td><%# Eval("SubTime")%></td>
                                            <td>
                                                <asp:Label ID="lblState" runat="server" ForeColor="Red" Text='<%# Eval("State")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <a href="BiServerInfo.aspx?QuestionId=<%#Eval("QuestionId")%>"><i class="fa fa-pencil" title="编辑"></i></a> &nbsp;&nbsp;
                                                <asp:LinkButton runat="server" CommandName="Del" CommandArgument='<%#Eval("QuestionId") %>' OnClientClick="return confirm('确认要删除此项?')" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="modal" id="userinfo_div">
        <div class="modal-dialog" style="width: 800px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <span class="modal-title"><strong id="title">用户信息</strong></span>
                </div>
                <div class="modal-body">
                      <iframe id="user_ifr" style="width:100%;height:500px;border:none;" src=""></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Common.js"></script>
    <script type="text/javascript" src="/JS/Dialog.js"></script>
    <script  type="text/javascript">
        if (getParam("num")) {
            $("li a[href='#tab" + getParam("num") + "']").parent().addClass("active").siblings("li").removeClass("active");;
        };
        $(function () {
            var num = $("#typeList").find("a[name='type']").length;
            if (parseInt(num) == 0)
                $("#typeList").hide();
        });
        function opentitle(url, title) {
            $("#title").text(title);
            $("#user_ifr").attr("src", url);
        };
        function checkAll() {
            xstatus = document.getElementById("cbAll").checked;
            var checkBoxs = document.getElementsByName("Btchk");
            for (i = 0; i < checkBoxs.length; i++) {
                checkBoxs[i].checked = xstatus;
            }

        };
        function isAnyOneChecked() {
            var checkBoxs = document.getElementsByName("Btchk");
            for (i = 0; i < checkBoxs.length; i++) {
                if (checkBoxs[i].checked) return true;
            }
        };
        function jump(obj, num) {
            var name = $(obj).text();
            if (parseInt(name) > 0) {
                if (num > 0)
                    window.location.href = "BselectiServer.aspx?num=" + num;
                else
                    window.location.href = "BselectiServer.aspx";
            }
        };
        function onUP(obj) {
            var name = $(obj).text();
            if (parseInt(name) > 0)
                $(obj).css({ "color": "#428bca", "cursor": "pointer", "text-decoration": "underline" });
            else
                $(obj).css({ "cursor": "default" });
        }
        function onDown(obj) {
            $(obj).css({ "color": "#000", "cursor": "default", "text-decoration": "none" });
        }
        HideColumn("3,5,6,7");
    </script>
    <style type="text/css">
        th { text-align: center;}
    </style>
</asp:Content>
