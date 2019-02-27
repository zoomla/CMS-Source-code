<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CartManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.CartManage"MasterPageFile="~/Manage/I/Default.master"  %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>购物车记录</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="top_opbar">
    <div class="input-group pull-left" style="width:300px;">
        <span class="input-group-addon">快速查找</span>
        <asp:DropDownList ID="quicksouch" runat="server" CssClass="form-control text_md" AutoPostBack="True">
            <asp:ListItem Value="1">所有购物车记录</asp:ListItem>
            <asp:ListItem Value="2">会员的购物车记录</asp:ListItem>
            <asp:ListItem Value="3">今天的购物车记录</asp:ListItem>
            <asp:ListItem Value="4">本周的购物车记录</asp:ListItem>
            <asp:ListItem Value="5">本月的购物车记录</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="input-group pull-left" style="width:300px;">
        <span class="input-group-addon">删除记录</span>
        <asp:DropDownList ID="souchtable" CssClass="form-control text_md" runat="server">
            <asp:ListItem Value="1">一天前</asp:ListItem>
            <asp:ListItem Value="7">一个星期前</asp:ListItem>
            <asp:ListItem Value="31">一个月前</asp:ListItem>
            <asp:ListItem Selected="True" Value="90">三个月前</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="clearfix"></div>
</div>
<ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="尚无购物车数据">
<Columns>
    <asp:TemplateField>
        <ItemTemplate>
            <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
        </ItemTemplate>
    </asp:TemplateField>
    <asp:BoundField HeaderText="ID" DataField="ID" HeaderStyle-CssClass="td_s" />
    <asp:BoundField HeaderText="商品名称" DataField="ProName" />
    <asp:BoundField HeaderText="客户名称" DataField="UserName" />
    <asp:BoundField HeaderText="时间" DataField="AddTime" DataFormatString="{0:yyyy年MM月dd日 HH:mm}" />
    <asp:BoundField HeaderText="数量" DataField="ProNum" />
    <asp:BoundField HeaderText="预计金额" DataField="AllMoney" DataFormatString="{0:f2}" />
    <asp:TemplateField HeaderText="操作">
        <ItemTemplate>
            <asp:LinkButton runat="server" CommandName="del2" OnClientClick="return confirm('不可恢复性删除数据,确定将该数据删除?')" CommandArgument='<%#Eval("ID") %>' class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
        </ItemTemplate>
    </asp:TemplateField>
</Columns>
    </ZL:ExGridView>
<asp:Button ID="BatDel" runat="server" class="btn btn-primary" Text="删除记录" OnClick="BatDel_Click" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>
