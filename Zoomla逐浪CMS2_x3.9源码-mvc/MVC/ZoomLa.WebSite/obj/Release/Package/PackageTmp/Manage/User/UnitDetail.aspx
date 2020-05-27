<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitDetail.aspx.cs" Inherits="ZoomLaCMS.Manage.User.UnitDetail" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head"><title>会员提成_流水</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False"
        CssClass="table table-striped table-bordered table-hover" EmptyDataText="无流水记录!!"
        OnPageIndexChanging="EGV_PageIndexChanging">
        <Columns>
            <%--            <asp:TemplateField HeaderText="消费日期">
                <ItemTemplate><%#Convert.ToDateTime(Eval("CDate")).ToString("yyyy年MM月dd日 HH:mm") %></ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="用户名">
                <ItemTemplate><a href="UserInfo.aspx?id=<%#Eval("UserID") %>" target="_blank"><%#Eval("UserName") %></a></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="推荐人">
                <ItemTemplate><a href="UserInfo.aspx?id=<%#Eval("PUserID") %>" target="_blank"><%#Eval("PUserName") %></a></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="消费金额">
                <ItemTemplate><%#Eval("AMount","{0:f2}") %></ItemTemplate>
            </asp:TemplateField>
            <%--            <asp:BoundField HeaderText="提成比率" DataField="UnitPercent"/>--%>
            <asp:TemplateField HeaderText="提成金额">
                <ItemTemplate><%#Eval("RealUnit","{0:f2}") %></ItemTemplate>
            </asp:TemplateField>
            <%--  <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                </ItemTemplate>
            </asp:TemplateField>--%>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <RowStyle Height="24px" HorizontalAlign="Center" />
    </ZL:ExGridView>
    <div>总业绩额<span class="rd_red_l" runat="server" id="TotalAmount_sp">0.00</span>,提成<span class="rd_red_l" runat="server" id="TotalUnit_sp">0.00</span></div>
    <style type="text/css">
        .rd_red_l {
            color: red;
            font-size: 1.5em;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>
