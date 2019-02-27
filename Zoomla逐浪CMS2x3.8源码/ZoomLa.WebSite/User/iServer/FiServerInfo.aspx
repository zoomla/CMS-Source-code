<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="FiServerInfo.aspx.cs" ValidateRequest="false" Inherits="manage_iServer_FiServerInfo" ClientIDMode="Static" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>问题详情</title>
    <script charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="cnt"></div> 
<div class="container">
    <ol class="breadcrumb">
    <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
    <li><a href="FiServer.aspx">有问必答</a></li>
    <li class="active">问题详情</li>
</ol>
</div>
<div class="container margin_t5 btn_green">
                  <asp:Label ID="lblQuestionId" runat="server" Text="" Visible="false"></asp:Label>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td width="100">标题</td>
                        <td>
                            <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td width="100">状态</td>
                        <td>
                            <asp:Label ID="lblState" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>优先级</td>
                        <td>
                            <asp:Label ID="lblPriority" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>提交来源</td>
                        <td>
                            <asp:Label ID="lblRoot" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td>问题类别</td>
                        <td>
                            <asp:Label ID="lblType" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td>提交时间</td>
                        <td>
                            <asp:Label ID="lblSubTime" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td>已读次数</td>
                        <td>
                            <asp:Label ID="lblReadCount" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td>解决时间</td>
                        <td>
                            <asp:Label ID="lblSolveTime" runat="server" Text=""></asp:Label></td>
                    </tr>
                </table>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td class="text-center">对话详情</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSubTime_R" runat="server" Text=""></asp:Label>
                            来自:<asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>标题:<asp:Label ID="lblTitle_R" runat="server" Text=""></asp:Label></b> [<a href="#reply">回复</a>]
                        </td>
                    </tr>
                    <tr>
                        <td style="border: #CCC 1px solid;">
                            <asp:Label ID="lblConent" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><span id="spDiv" runat="server"></span>
                            <br />
                        </td>
                    </tr>
                </table>
                <asp:Repeater ID="resultsRepeater" runat="server">
                    <HeaderTemplate>
                        <table class="table table-bordered table-striped table-hover">
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="回复记录" ID="rep"></asp:Label>
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="lblSubTime_R" runat="server" Text='<%# Eval("ReplyTime")%>'></asp:Label>
                                来自:<asp:Label ID="lblName" runat="server" Text='<%# GetUserName(Eval("UserId","{0}"))%>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>[<a href="#reply">回复</a>]</td>
                        </tr>
                        <tr>
                            <td style="border: #CCC 1px solid;">
                                <asp:Label ID="lblConent" runat="server" Text='<%# Eval("Content")%>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><%#GetFile(Eval("path","{0}")) %>  </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <div id="replyDiv" runat="server">
                    <table class="table table-bordered table-striped table-hover">
                        <tr>
                            <td colspan="2" class="text-center">回复对话</td>
                        </tr>
                        <tr>
                            <td width="120">标题</td>
                            <td>
                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Text="" Width="387px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>内容<font color="red">*</font></td>
                            <td>
                                <textarea runat="server" id="textarea1" style="width:700px;min-height:500px;" name="content" rows="4" cols="40"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td>附件
                            </td>
                            <td>
                                <input type="button" class="btn btn-primary" id="upfile_btn" value="选择文件" />
                                <div style="margin-top:10px;" id="uploader" class="uploader"><ul class="filelist"></ul></div>
                                <asp:HiddenField runat="server" ID="Attach_Hid" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="提交回复" OnClick="Button1_Click" />
                                <a href="FiServer.aspx" class="btn btn-primary">返回列表</a>
                            </td>
                        </tr>
                    </table>
                </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<%--<script src="/js/zh-CN/attachment.js"></script>--%>
<link href="/JS/Controls/ZL_Webup.css" rel="stylesheet" />
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Controls/ZL_Webup.js"></script>
<script>
    <%= Call.GetEditor("textarea1")%>
    function fn_CheckSupportTicket(theForm) {
        if (!fn_CheckRequired(theForm.content, "回复内容"))
            return false;
        return true;
    }
    $(function () {
        $("#upfile_btn").click(ZL_Webup.ShowFileUP);
    })
    function AddAttach(file, ret, pval) { return ZL_Webup.AddAttach(file, ret, pval); }
</script>
</asp:Content>
