<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User/Default.master" CodeFile="TalkLog.aspx.cs" Inherits="User_UserTalk_TalkLog" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>聊天记录</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="group" data-ban="TalkLog"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
	<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
	<li class="active">聊天记录</li> 
    </ol>
</div>
    <div class="container btn_green">
        <asp:TextBox ID="ReUser_T" CssClass="form-control text_md" runat="server" placeholder="接收人"></asp:TextBox>
        <asp:TextBox ID="SDate_T" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm'});" CssClass="form-control text_md" runat="server" placeholder="开始时间"></asp:TextBox>
        <asp:TextBox ID="EDate_T" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm'});" CssClass="form-control text_md" runat="server" placeholder="结束时间"></asp:TextBox>
        <asp:Button ID="Find_B" runat="server" OnClick="Find_B_Click" Text="搜索" CssClass="btn btn-primary" />
        <asp:Button ID="DownFile_B" runat="server" OnClick="DownFile_B_Click" Text="下载" CssClass="btn btn-primary" />
    </div>
    <div class="container">
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="false" PageSize="10" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover margin_t10" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无相关数据！！" OnPageIndexChanging="EGV_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <%#Eval("ID") %>
                </ItemTemplate>
                <ItemStyle CssClass="td_s" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="发送者">
                <ItemTemplate>
                    <a href="javascript:;" onclick="ShowUInfo('<%#Eval("UserID") %>')"><%#GetSender() %></a> 
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="接收者">
                <ItemTemplate>
                    <%#GetReceUser() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="内容">
                <ItemTemplate>
                    <%#Eval("Content") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="发送时间" DataField="CDate" />
        </Columns>
    </ZL:ExGridView>
</div>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        var userDiag = new ZL_Dialog();
        function ShowUInfo(uid) {
            userDiag.title = "用户信息";
            userDiag.reload = true;
            userDiag.url = "Userinfo.aspx?id=" + uid;
            userDiag.ShowModal();
        }
    </script>
</asp:Content>
