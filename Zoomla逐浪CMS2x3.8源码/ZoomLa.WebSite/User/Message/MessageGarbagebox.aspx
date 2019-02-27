<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="MessageGarbagebox.aspx.cs" Inherits="User_Message_MessageGarbagebox" ClientIDMode="Static" ValidateRequest="false" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>垃圾箱</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="index" data-ban="pub"></div>
    <div class="container margin_t5">
        <ol class="breadcrumb">
            <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
            <li><a href="Message.aspx">消息中心</a></li>
            <li class="active">垃圾箱</li>
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
    <div class="container btn_green margin_t10">
        <ZL:ExGridView runat="server" ID="EGV" DataKeyNames="MsgID" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False"
            CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!"
            OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="选择" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:CheckBox ID="cheCk" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Width="5%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="主题">
                    <HeaderStyle Width="40%" />
                    <ItemTemplate>
                        <%# Eval("Title", "{0}")%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="收件人" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <%#GetUserName(Eval("Incept","{0}")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="发件人" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <%#GetUserName(Eval("Sender","{0}")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PostDate" HeaderText="发送日期" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="center">
                    <HeaderStyle Width="20%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="操作" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDel" runat="server" CommandName="DeleteMsg" OnClientClick="if(!this.disabled) return confirm('确实要删除此信息到垃圾箱吗？');" CommandArgument='<%# Eval("MsgID")%>' Text="删除"></asp:LinkButton>
                        <asp:LinkButton ID="lbRead" runat="server" CommandName="ReadMsg" CommandArgument='<%# Eval("MsgID")%>'>阅读信息</asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="20%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <RowStyle Height="24px" HorizontalAlign="Center" />
        </ZL:ExGridView>
        <asp:Button ID="BatDel_Btn" runat="server" Text="批量删除" OnClick="BatDel_Btn_Click" class="btn btn-primary" />
    </div>
</asp:Content>
