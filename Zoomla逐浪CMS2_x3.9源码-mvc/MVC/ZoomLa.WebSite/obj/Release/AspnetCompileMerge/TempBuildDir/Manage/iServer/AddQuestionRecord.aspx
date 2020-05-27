<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddQuestionRecord.aspx.cs" Inherits="ZoomLaCMS.Manage.iServer.AddQuestionRecord"  MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加问题</title>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:HiddenField runat="server" ID="OrderID" />
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2" class="title" align="center">
                <input type="hidden" name="module" value="manage_support_supportticketadd" />
                <input type="hidden" name="dosupportticketadd" value="" />
                <input type="hidden" name="dologinsearch" value="" />
                <input type="hidden" name="user_id" value="" />添加问题
            </td>
        </tr>
        <tr>
            <td  style="width:150px;">会员登录帐号<font color="red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control text_md" Text=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td >状态<font color="red">*</font>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList1" CssClass="form-control text_md" runat="server" AutoPostBack="true">
                    <asp:ListItem Selected="True" Value="未解决">未解决</asp:ListItem>
                    <asp:ListItem Value="处理中">处理中</asp:ListItem>
                    <asp:ListItem Value="已解决">已解决</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td >优先级<font color="red">*</font>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control text_md" AutoPostBack="true">
                    <asp:ListItem Value="低">低</asp:ListItem>
                    <asp:ListItem Selected="True" Value="中">中</asp:ListItem>
                    <asp:ListItem Value="高">高</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" cssclass="form-control text_md"
            onmouseout="this.className='tdbg'">
            <td >提交来源<font color="red">*</font>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList3" CssClass="form-control text_md" runat="server" AutoPostBack="true">
                    <asp:ListItem Selected="True" Value="电话">电话</asp:ListItem>
                    <asp:ListItem Value="E-mai">E-mail</asp:ListItem>
                    <asp:ListItem Value="网页表单">网页表单</asp:ListItem>
                    <asp:ListItem Value="直接面谈">直接面谈</asp:ListItem>
                    <asp:ListItem Value="其它">其它</asp:ListItem>
                </asp:DropDownList>
                输入电话号码、E-mail等：<asp:TextBox ID="txtRootInfo" CssClass="form-control text_md" runat="server" Text=""
                    Width="252px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td >问题类别<font color="red">*</font>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control text_md" AutoPostBack="true">                    
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
            <td >问题标题<font color="red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control text_md" Text="" Width="348px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td >问题内容<font color="red">*</font>
            </td>
            <td>
                <textarea runat="server" id="textarea1" style="width:700px; height:300px;"></textarea>
            </td>
        </tr>
        <tr>
            <td >附注（内部使用）
            </td>
            <td>
                <textarea class="form-control text_md" name="comments" rows="6" cols="80"></textarea>
            </td>
        </tr>
        <tr>
            <td >附件
            </td>
            <td>
               <input type="button" id="upfile_btn" class="btn btn-primary" value="选择文件" />
                 <div style="margin-top:10px;" id="uploader" class="uploader"><ul class="filelist"></ul></div>
                 <asp:HiddenField runat="server" ID="Attach_Hid" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary" runat="server" OnClick="LinkButton1_Click">添加</asp:LinkButton>
                <asp:LinkButton ID="LinkButton2" CssClass="btn btn-primary" runat="server">取消</asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Common.js"></script>
    <script type="text/javascript" src="/js/zh-CN/attachment.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/JS/Controls/ZL_Webup.js"></script>
<link href="/JS/Controls/ZL_Webup.css" rel="stylesheet" />
    <script type="text/javascript">
        <%= Call.GetEditor("textarea1")%>
    </script>
    <script>
        $(function () {
            $("#upfile_btn").click(ZL_Webup.ShowFileUP);
        })
        function AddAttach(file, ret, pval) { return ZL_Webup.AddAttach(file, ret, pval); }
        function fn_CheckLoginSearch(theForm) {
            if (!fn_CheckRequired(theForm.login, "登录帐号"))
                return false;
            return true;
        }
        function fn_CheckSupportTicket(theForm) {
            if (theForm.source_type.options[theForm.source_type.selectedIndex].value == 'phone'
			  || theForm.source_type.options[theForm.source_type.selectedIndex].value == 'email') {
                if (!fn_CheckRequired(theForm.source, "电话或者E-mail"))
                    return false;
            }
            if (!fn_CheckRequired(theForm.title, "问题标题"))
                return false;
            if (!fn_CheckRequired(theForm.content, "问题内容"))
                return false;
            return true;
        }
    </script>
</asp:Content>
