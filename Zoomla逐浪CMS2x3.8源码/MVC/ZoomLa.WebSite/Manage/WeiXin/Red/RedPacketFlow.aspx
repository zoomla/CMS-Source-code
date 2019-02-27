<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RedPacketFlow.aspx.cs" Inherits="ZoomLaCMS.Manage.WeiXin.RedPacketFlow" MasterPageFile="~/Manage/I/Default.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>红包详情列表</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="top_opbar">
    <asp:Label runat="server" ID="RedPacket_L"></asp:Label>
</div>
<ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="数据为空">
        <Columns>
            <asp:TemplateField HeaderStyle-CssClass="td_s">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="td_s"><ItemTemplate><%#Eval("ID") %></ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="领取码"><ItemTemplate><a href="RedDetail.aspx?id=<%#Eval("ID") %>"><%#Eval("RedCode") %></a></ItemTemplate></asp:TemplateField>
            <asp:BoundField HeaderText="金额" DataField="AMount" DataFormatString="{0:f2}"  HeaderStyle-CssClass="td_m"/>
            <asp:TemplateField HeaderText="状态"  HeaderStyle-CssClass="td_m">
                <ItemTemplate><%#GetStatus() %></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="领取人" DataField="UserName" HeaderStyle-CssClass="td_l"/>
            <asp:BoundField HeaderText="领取时间" DataField="UseTime" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" HeaderStyle-CssClass="td_l"/>
            <asp:TemplateField HeaderText="操作"  HeaderStyle-CssClass="td_m">
                <ItemTemplate>
                    <a class="option_style" href="RedDetail.aspx?id=<%#Eval("ID") %>"><i class="fa fa-pencil"></i></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
<div class="margin_t5">
    <asp:Button runat="server" ID="BatDel_Btn" class="btn btn-info" Text="批量删除" OnClick="BatDel_Btn_Click" OnClientClick="return confirm('确定要删除吗?');" />
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>