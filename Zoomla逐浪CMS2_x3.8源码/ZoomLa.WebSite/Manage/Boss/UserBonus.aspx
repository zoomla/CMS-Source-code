<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserBonus.aspx.cs" Inherits="Manage_Boss_UserBonus" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register Src="~/Manage/Boss/StructDP.ascx" TagPrefix="ZL" TagName="StructDP" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>会员分红</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="top_opbar">
        <div class="input-group" style="width: 840px; margin-left: 5px; float: left;">
            <ZL:StructDP runat="server" id="StructDP" />
            <asp:TextBox ID="UName_T" runat="server" placeholder="用户名" CssClass="form-control text_md" Style="border-right: none;"></asp:TextBox>
            <asp:TextBox ID="STime_T" runat="server" placeholder="起始时间" CssClass="form-control text_md" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm' })" Style="border-right: none;"></asp:TextBox>
            <asp:TextBox ID="ETime_T" runat="server" placeholder="结束时间" CssClass="form-control text_md" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm' })"></asp:TextBox>
            <span class="input-group-btn">
                <asp:LinkButton runat="server" ID="Skey_Btn" OnClick="Skey_Btn_Click" CssClass="btn btn-default"><span class="fa fa-search"></span></asp:LinkButton>
            </span>
        </div>
        <div class="clearfix"></div>
    </div>
    <div class="top_opbar">
        <span class="rd_green">按当前查询值统计：</span>
        分红合计：<span id="bonus_query_sp" class="rd_red_l" runat="server">0</span>,
        波动金额合计：<span id="bdmoney_sp" class="rd_red_l" runat="server">0</span>,
        平均波动盈亏率：<span id="percent_sp" class="rd_red_l" runat="server">0</span>
    </div>
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false"
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="尚无分红记录">
        <Columns>
            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="td_s"><ItemTemplate>
                <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" /></ItemTemplate></asp:TemplateField>
            <asp:BoundField HeaderText="用户名" DataField="UserName" />
            <asp:TemplateField HeaderText="入账状态">
                <ItemTemplate><%#GetStatus() %></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="计算公式" DataField="formula" />
            <asp:BoundField HeaderText="波动金额" DataField="bdmoney" DataFormatString="{0:f2}"/>
            <asp:BoundField HeaderText="分红金额" DataField="unit" DataFormatString="{0:f2}" />
            <asp:BoundField HeaderText="日期" DataField="cdate" />
            <asp:BoundField HeaderText="备注" DataField="Remark" />
        </Columns>
    </ZL:ExGridView>
    <div>
<%--        <asp:Button runat="server" ID="BeginBonus_Btn" OnClick="BeginBonus_Btn_Click" CssClass="btn btn-primary" Text="计算分红" />
        <a href="BonusToUser.aspx" class="btn btn-primary">分红入账</a>--%>
    </div>
    <div class="alert alert-info margin_t5">波动的幅度为正负10%,计算公式:(余额+(余额*波动))*分红<br />
        波动金额将影响高频账户内金额,分红则进入手工账户
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>
