<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Message.aspx.cs" Inherits="ZoomLa.WebSite.User.Message" ClientIDMode="Static" ValidateRequest="false" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>收件箱</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="pub"></div>
<div class="container">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="Message.aspx">消息中心</a></li>
        <li class="active">收件箱</li>
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
<div class="container margin_t5 btn_green">
    <div class="input-group" style="width: 350px;margin-bottom:5px;">
        <asp:TextBox runat="server" ID="Skey_T" CssClass="form-control text_300" placeholder="请输入邮件名称" />
        <span class="input-group-btn">
            <asp:Button runat="server" ID="Skey_Btn" CssClass="btn btn-primary" Text="搜索" OnClick="Skey_Btn_Click" /></span>
    </div>
    <ZL:ExGridView runat="server" ID="EGV" DataKeyNames="MsgID" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False"
                CssClass="table table-striped table-bordered table-hover" EmptyDataText="无相关信息!!"
                OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="选择" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <input type="checkbox" name="idchk" value="<%#Eval("MsgID") %>" />
                        </ItemTemplate>
                        <HeaderStyle Width="5%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="标题">
                        <HeaderStyle Width="40%" />
                        <ItemTemplate><%# Eval("Title", "{0}")%><%#getStatus(Eval("ReadUser","{0}"))%></ItemTemplate>
                        <ItemStyle HorizontalAlign="left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="发件人">
                        <ItemTemplate>
                            <%# GetUserName(Eval("Sender","{0}")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PostDate" HeaderText="发送日期" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="center">
                        <HeaderStyle Width="20%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <a href="MessageRead.aspx?read=1&id=<%#Eval("MsgID") %>"><i class="fa fa-eye"></i>阅读</a>
                            <asp:LinkButton runat="server" CommandName="del2" OnClientClick="if(!this.disabled) return confirm('确实要删除此信息到垃圾箱吗？');" CommandArgument='<%# Eval("MsgID")%>'><i class="fa fa-trash"></i>删除</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle Height="24px" HorizontalAlign="Center" />
            </ZL:ExGridView>
        <div>
            <asp:Button ID="BatDel_Btn" class="btn btn-primary" runat="server" Text="批量删除" OnClick="BatDel_Btn_Click" />
        </div>
</div>
</asp:Content>
