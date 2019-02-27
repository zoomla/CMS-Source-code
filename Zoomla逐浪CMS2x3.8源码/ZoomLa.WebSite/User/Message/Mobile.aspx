<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Mobile.aspx.cs" Inherits="User_Message_Mobile" ClientIDMode="Static" ValidateRequest="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server"><title>手机短信</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="pub"></div>
<div class="container margin_t5">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="Message.aspx">消息中心</a></li>
        <li class="active">手机短信</li> 
    </ol>
</div>
<div class="container">
    <div class="btn-group btn_green">
        <a href="MessageSend.aspx" class="btn btn-primary">新消息</a>
        <a href="Message.aspx" class="btn btn-primary">收件箱</a>
        <a href="MessageOutbox.aspx" class="btn btn-primary">发件箱</a>
        <a href="MessageDraftbox.aspx" class="btn btn-primary">草稿箱</a>
        <a href="MessageGarbagebox.aspx" class="btn btn-primary">垃圾箱</a>
        <a href="Mobile.aspx" class="btn btn-primary">手机短信</a>
    </div>
</div>
<%ZoomLa.Common.function.WriteErrMsg("功能关闭,请联系管理员开启"); %>
<div class="container margin_t10 btn_green">
    <table class="table table-bordered table-striped">
        <tr>
            <td class="td_l text-right">接收方手机号码：</td>
            <td>
                <table id="TblAddMessage" width="100%" visible="true" runat="server">
                    <tr>
                        <td>
                            <asp:TextBox ID="TxtInceptUser" runat="server" Width="326px" class="form-control"></asp:TextBox>
                            一次只能发给一个号码一条短信
            <asp:RegularExpressionValidator ControlToValidate="TxtInceptUser" ID="rev1" runat="server" ValidationExpression="^1\d{10}$">手机号码有误，请重新输入！</asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtInceptUser" runat="server">手机号码不能为空 </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="text-right">短信内容：<br />
                (字数70个字以内)</td>
            <td>
                <asp:TextBox ID="EditorContent" runat="server" Rows="10" TextMode="MultiLine" style="max-width:500px;" class="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ValrContent" runat="server" ControlToValidate="EditorContent" ErrorMessage="短消息内容不能为空" Display="Dynamic">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr><td class="text-right">发送结果</td><td><asp:Label runat="server" ID="Result_L" ForeColor="Red"></asp:Label></td></tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="BtnSend" runat="server" Text="发送" OnClick="BtnSend_Click" CssClass="btn btn-primary" />
                <asp:Button ID="BtnReset" runat="server" Text="清除" OnClientClick="return CheckIsMobile(document.getElementById('TxtInceptUser').value)" OnClick="BtnReset_Click" class="btn btn-primary" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
            </td>
        </tr>
    </table>
    <div class="alert alert-success">
        <asp:Label ID="LblMobile" runat="server" Text="Label" Style="color: Red;"></asp:Label>
        <i class="fa fa-lightbulb-o"></i> 提示：系统支持以Mobile.aspx?MB=[手机号码]&txt=[发送内容]方式GET将手机号码传值发送，如：Mobile.aspx?MB=13177777714&txt=默认短信内容。
    </div>
</div>
</asp:Content>
