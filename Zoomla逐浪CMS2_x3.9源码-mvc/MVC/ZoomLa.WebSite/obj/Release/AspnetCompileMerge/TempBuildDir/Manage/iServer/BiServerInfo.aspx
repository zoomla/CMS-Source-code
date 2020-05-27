<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BiServerInfo.aspx.cs" Inherits="ZoomLaCMS.Manage.iServer.BiServerInfo"  MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>问题详情</title>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Label ID="lblQuestionId" runat="server" Text="" Visible="false"></asp:Label>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td>会员登录帐号</td>
            <td>
                <span id="lblUserName" runat="server"></span>
                <asp:HiddenField ID="hfusername" runat="server" />
            </td>
        </tr>
        <tr>
            <td>状态<font color="red">*</font></td>
            <td>
                <asp:DropDownList ID="DropDownList1" CssClass="form-control text_md" runat="server" AutoPostBack="False">
                    <asp:ListItem Value="未解决">未解决</asp:ListItem>
                    <asp:ListItem Value="处理中">处理中</asp:ListItem>
                    <asp:ListItem Value="已解决">已解决</asp:ListItem>
                    <asp:ListItem Value="已锁定">已锁定</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>优先级<font color="red">*</font></td>
            <td>
                <asp:DropDownList ID="DropDownList2" CssClass="form-control text_md" runat="server" AutoPostBack="False">
                    <asp:ListItem Value="低">低</asp:ListItem>
                    <asp:ListItem Value="中">中</asp:ListItem>
                    <asp:ListItem Value="高">高</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>提交来源<font color="red">*</font></td>
            <td>
                <asp:DropDownList ID="DropDownList3" CssClass="form-control text_md" runat="server" AutoPostBack="False">
                    <asp:ListItem Value="电话">电话</asp:ListItem>
                    <asp:ListItem Value="E-mai">E-mail</asp:ListItem>
                    <asp:ListItem Value="网页表单">网页表单</asp:ListItem>
                    <asp:ListItem Value="直接面谈">直接面谈</asp:ListItem>
                    <asp:ListItem Value="其它">其它</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>问题类别<font color="red">*</font></td>
            <td>
                <asp:DropDownList ID="DropDownList4" CssClass="form-control text_md" runat="server" AutoPostBack="false">                    
                    <asp:ListItem Selected="True" Value="咨询">咨询</asp:ListItem>
                    <asp:ListItem Value="投诉">投诉</asp:ListItem>
                    <asp:ListItem Value="建议">建议</asp:ListItem>
                    <asp:ListItem Value="要求">要求</asp:ListItem>
                    <asp:ListItem Value="界面使用">界面使用</asp:ListItem>
                    <asp:ListItem Value="bug报告">bug报告</asp:ListItem>
                    <asp:ListItem Value="订单">订单</asp:ListItem>
                    <asp:ListItem Value="财务">财务</asp:ListItem>
                    <asp:ListItem Value="域名">域名</asp:ListItem>
                    <asp:ListItem Value="主机">主机</asp:ListItem>
                    <asp:ListItem Value="邮局">邮局</asp:ListItem>
                    <asp:ListItem Value="DNS">DNS</asp:ListItem>
                    <asp:ListItem Value="MSSQL">MSSQL</asp:ListItem>
                    <asp:ListItem Value="MySQL">MySQL</asp:ListItem>
                    <asp:ListItem Value="IDC">IDC</asp:ListItem>
                    <asp:ListItem Value="网站推广">网站推广</asp:ListItem>
                    <asp:ListItem Value="网站制作">网站制作</asp:ListItem>
                    <asp:ListItem Value="其它">其它</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>提交时间</td>
            <td>
                <asp:Label ID="lblSubTime" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>已读次数</td>
            <td>
                <asp:Label ID="lblReadCount" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>解决时间</td>
            <td>
                <asp:Label ID="lblSolveTime" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>附注（内部使用）</td>
            <td>
                <textarea class="form-control tarea_l" name="comments" rows="4" cols="40"></textarea></td>
        </tr>
        <tr id="Attch_Tr" visible="false" runat="server">
            <td>附件:</td>
            <td class="attch_td">
                <div style="margin-top: 10px;" id="Quest_Attch" class="uploader">
                    <ul class="filelist"></ul>
                </div>
                <asp:HiddenField ID="Quest_Attch_Hid" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="text-center">
                <asp:Button ID="Button2" runat="server" Text="更新" CssClass="btn btn-primary" OnClick="Button2_Click" />
            </td>
        </tr>
    </table>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2" class="title">最近的 5 条处理日志 </td>
        </tr>

        <tr>
            <td width="200px">
                <asp:Label ID="lblSubTime_V" runat="server" Text=""></asp:Label></td>
            <td>
                <asp:Label ID="lblUserName_V" runat="server" Text=""></asp:Label>提交了新问题</td>
        </tr>

    </table>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="title">对话详情</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSubTime_R" runat="server" Text=""></asp:Label>
                来自:<asp:Label ID="lblName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td><b>标题:<asp:Label ID="lblTitle_R" runat="server" Text=""></asp:Label></b> [<a href="#reply">回复</a>] [<a href="#top">返回页首</a>]</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblConent" runat="server" Text=""></asp:Label>
                <br />
            </td>
        </tr>
    </table>
    <asp:Repeater ID="resultsRepeater" runat="server" OnItemDataBound="resultsRepeater_ItemDataBound">
        <HeaderTemplate>
            <table class="table table-striped table-bordered table-hover">
                <tr>
                    <td class="title">回复记录</td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td class="tdbgmouseover">
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ReplyTime")%>'></asp:Label>
                    来自:<asp:Label ID="Label2" runat="server" Text='<%# GetUserName(Eval("UserId","{0}"))%>'></asp:Label></td>
            </tr>
            <tr>
                <td>[<a href="#reply">回复</a>] [<a href="javascript:;" onclick="EditReplay(<%#Eval("Id") %>,'<%#Eval("Content") %>')">编辑</a>] [<a href="#top">返回页首</a>]</td>
            </tr>
            <tr>
                <td style="border: #CCC 1px solid;">
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Content")%>'></asp:Label>
                    <br />
                    <div id="reply_attch" class="attch_td" runat="server" visible="false">
                        <div style="margin-top: 10px;" id="Reply_Attch_<%#Eval("ID") %>" class="uploader">
                        <ul class="filelist"></ul>
                        </div>
                        <input type="hidden" id="reply_attch_hid_<%#Eval("ID") %>" value="<%#Eval("Path").ToString().Trim('|') %>" />
                    </div>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2" class="text-center"><b>回复对话</b></td>
        </tr>
        <tr>
            <td style="min-width:150px;">标题</td>
            <td>
                <input type="text" name="title" runat="server" id="txtTitle" size="40" maxlength="100" class="form-control text_md" value="" /></td>
        </tr>
        <tr>
            <td>内容<font color="red">*</font></td>
            <td>
                <textarea runat="server" id="textarea1" style="width: 700px; height: 300px;"  cols="20" rows="2"></textarea>
                <%= Call.GetUEditor("textarea1",2)%>
            </td>
        </tr>
        <tr>
            <td>附件</td>
            <td>
                <input type="button" id="upfile_btn" class="btn btn-primary" value="选择文件" />
                 <div style="margin-top:10px;" id="uploader" class="uploader"><ul class="filelist"></ul></div>
                 <asp:HiddenField runat="server" ID="Attach_Hid" />
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>E-mail回复</td>
            <td>
                <asp:CheckBox ID="IsEmail" Checked="true" runat="server" />
                <span id="IsEmailcont">是否发送邮件回复给：<input type="text" runat="server" id="UserEmail" class="form-control text_md" name="reply_to" value="" /></span>
            </td>
        </tr>
    </table>
    <input type="hidden" name="greeting_name" value="" />
    <asp:Button ID="Button1" runat="server" Text="确认" CssClass="btn btn-primary" OnClick="Button1_Click" />

    <div style="position:absolute;top:-100000px;" id="editReply_div">
        <div class="container-fluid text-center">
            <textarea runat="server" id="EditReply_T" style="width:100%; height:300px;"></textarea>
            <%=Call.GetUEditor("EditReply_T",2) %>
            <asp:HiddenField ID="ReplyId_Hid" runat="server" />
            <asp:Button ID="EditReply_B" runat="server" OnClick="EditReply_B_Click" CssClass="btn btn-primary" OnClientClick="return SetReplyContent();" Text="保存" />
            <button type="button" class="btn btn-primary" onclick="CloseDiag()">关闭</button>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/zh-CN/attachment.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/JS/Controls/ZL_Webup.js"></script>
<link href="/JS/Controls/ZL_Webup.css" rel="stylesheet" />
    <script type="text/javascript">
        function fn_CheckSupportTicket(theForm) {
            if (!fn_CheckRequired(theForm.content, "回复内容"))
                return false;
            return true;
        }
        function opentitle(url, title) {
            var diag = new Dialog();
            diag.Width = 600;
            diag.Height = 400;
            diag.Title = title;
            diag.URL = url;
            diag.show();
        }
        $(function () {
            $("#upfile_btn").click(ZL_Webup.ShowFileUP);
            $(".attch_td").each(function () {
                var attchpre = new MFileUP({ id: $(this).find(".uploader").attr('id'), hid: $(this).find("input[type='hidden']").attr('id') });
                attchpre.AddReadOnlyLi($(this).find("input[type='hidden']").val());
            });
        });

        function AddAttach(file, ret, pval) { return ZL_Webup.AddAttach(file, ret, pval); }
        var editDiag = new ZL_Dialog();
        //编辑回复内容
        function EditReplay(rid, oldcontent) {
            UE.getEditor("EditReply_T").setContent('');
            $("#ReplyId_Hid").val(rid);
            editDiag.title = "编辑回复内容";
            editDiag.content = "editReply_div";
            editDiag.ShowModal();
            setTimeout(function () { UE.getEditor("EditReply_T").setContent(oldcontent); }, 500);
        }
        function SetReplyContent() {
            var ue = UE.getEditor("EditReply_T");
            $("#EditReply_T").val(ue.getContent());
            return true;
        }
        function CloseDiag() {
            editDiag.CloseModal();
        }

    </script>
</asp:Content>
