<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RedPacket.aspx.cs" Inherits="ZoomLaCMS.Manage.WeiXin.RedPacket" MasterPageFile="~/Manage/I/Default.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>红包列表</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
 <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="数据为空">
        <Columns>
            <asp:TemplateField HeaderText="ID"><ItemTemplate><%#Eval("ID") %></ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="红包名称">
                <ItemTemplate>
                    <a href="RedPacketFlow.aspx?MainID=<%#Eval("ID") %>"><%#Eval("Name") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="红包金额(合计)"><ItemTemplate><%#Eval("RedAMount","{0:f2}") %></ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="红包数量"><ItemTemplate><%#Eval("RedCount") %></ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="红包剩余"><ItemTemplate><%#Eval("LeftRedCount") %></ItemTemplate></asp:TemplateField>
            <asp:BoundField HeaderText="生效时间" DataField="SDate" />
            <asp:BoundField HeaderText="到期时间" DataField="EDate" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a class="option_style" href="RedPacketAdd.aspx?id=<%#Eval("ID") %>&appid=<%#Eval("APPID") %>"><i class="fa fa-pencil"></i></a>
                    <a href="RedPacketFlow.aspx?MainID=<%#Eval("ID") %>" class="option_style"><i class="fa fa-list"></i>查看详情</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">
.allchk_l{display:none;}
</style>
</asp:Content>