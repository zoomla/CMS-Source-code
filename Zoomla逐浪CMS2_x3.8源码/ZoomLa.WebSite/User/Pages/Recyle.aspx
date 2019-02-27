<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Recyle.aspx.cs" Inherits="User_Content_Recyle" MasterPageFile="~/User/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>黄页回收站</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="page" data-ban="page"></div>
<div class="container">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/Page/Default.aspx">黄页</a></li>
        <li class="active">回收站</li>
    </ol>
</div>
 <div class="container">
        <ZL:ExGridView runat="server" ID="EGV" DataKeyNames="GeneralID" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False"
        CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!"
        OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="选择">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("GeneralID") %>" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="GeneralID" HeaderText="ID">
                <HeaderStyle Width="6%" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="标题">
                <HeaderStyle Width="50%" />
                <ItemTemplate>
                    <a href="<%# GetUrl(Eval("GeneralID", "{0}"))%>" target="_blank"><%# GetModel(Eval("GeneralID", "{0}"))%><%# Eval("Title")%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <%# GetStatus(Eval("Status", "{0}")) %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="已生成">
                <ItemTemplate>
                    <%# GetCteate(Eval("IsCreate", "{0}"))%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton ID="Btn_Rec" runat="server" CommandName="Rec" CommandArgument='<%# Eval("GeneralID") %>'>还原</asp:LinkButton>
                    <asp:LinkButton ID="Btn_Del" runat="server" CommandName="Del" CommandArgument='<%# Eval("GeneralID") %>' OnClientClick="return confirm('你确定将该数据彻底删除吗？')">彻底删除选中</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <RowStyle Height="24px" HorizontalAlign="Center" />
    </ZL:ExGridView>
    <div class="btn_green">
        <asp:Button ID="Button1" runat="server" Text="批量还原" OnClick="btnRecAll_Click" OnClientClick="if(!IsSelectedId()){alert('请选择还原项');return false;}else{return confirm('你确定要将所选中的项还原吗？')}" CssClass="btn btn-primary" UseSubmitBehavior="true" />
        <asp:Button ID="Bat_Del" Text="批量删除" runat="server" OnClick="Bat_Del_Click" OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('你确定要将所选中的项删除吗？')}" CssClass="btn btn-primary" />
        <input type="text" runat="server" id="TxtSearchTitle" class="form-control text_md" style="color: #666;" value="请输入标题" onclick="if (this.value == '请输入标题') { this.value = ''; this.style.color = 'black'; }" onblur="if(this.value==''){this.value='请输入标题';this.style.color='#666';}">
        <asp:Button ID="Btn_Search" runat="server" Text="搜索" CssClass="btn btn-primary" OnClick="Btn_Search_Click" />
        <script>
            document.getElementById("TxtSearchTitle").value.trim();
        </script>
    </div>
 </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/SelectCheckBox.js"></script>
</asp:Content>
