<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MsgList.aspx.cs" Inherits="Manage_User_MsgList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>聊天详情</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
        <li><a href="ServiceSeat.aspx">客服通</a></li>
        <li><a href="/Admin/User/MsgEx.aspx">聊天记录</a></li>
        <li><asp:Label runat="server" ID="SToR_L"></asp:Label></li>
    </ol>
    <div style="height:40px;"></div>
    <ul class="nav nav-tabs hidden-xs hidden-sm">
        <li class="active"><a href="#tab0" data-toggle="tab" data-index="0">聊天框</a></li>
        <li><a href="#tab5" data-toggle="tab" data-index="1">列表</a></li>
    </ul>
    <div class="tab" id="tab0">
        <iframe src="/Common/Chat/ChatHistory.aspx?suid=<%:suid %>&ruid=<%:ruid %>" class="chatifr"></iframe>
    </div>
    <div class="tab" id="tab1" style="display:none;">
        <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="false" PageSize="10" IsHoldState="false" 
            AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" 
            EnableTheming="False" EnableModelValidation="True" EmptyDataText="无聊天数据!" OnPageIndexChanging="EGV_PageIndexChanging">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_s">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="发送人" ItemStyle-CssClass="td_m">
               <ItemTemplate>
                   <%#Eval("UserName") %>
               </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="内容">
                <ItemTemplate>
                    <%#Eval("Content")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="发送时间" DataField="CDate" ItemStyle-CssClass="td_l" />
        </Columns>
    </ZL:ExGridView>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        .chatifr {border:1px solid #ddd;width:1024px;height:750px;padding:5px;}
    </style>
    <script>
        $(function () {
            $("[data-toggle=tab]").click(function () {
                ShowTab($(this).data("index"));
            });
        })
        function ShowTab(index) {
            $(".tab").hide();
            $("#tab" + index).show();
        }
    </script>
</asp:Content>