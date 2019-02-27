<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FillOrder.aspx.cs" Inherits="Manage_Shop_FillOrder" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        #EGV1 tr th,#EGV tr td{text-align:center;}
        #AllID_Chk{display:none;}
    </style>
    <title><%=Resources.L.补订单 %></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">

<div class="userlist" id="userlist" runat="server">
    <ZL:ExGridView ID="EGV"  runat="server" AutoGenerateColumns="False" PageSize="20"  OnRowCommand="EGV_RowCommand" OnPageIndexChanging="EGV_PageIndexChanging" 
        IsHoldState="false" Height="20px"   AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" 
        GridLines="None" EnableModelValidation="True">
        <PagerStyle HorizontalAlign="Center" />
        <RowStyle HorizontalAlign="Center" />
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="UserID" ReadOnly="true" />
            <asp:BoundField DataField="UserName" HeaderText="<%$Resources:L,会员名 %>" />
            <asp:TemplateField HeaderText="<%$Resources:L,会员组 %>">
                <ItemTemplate>
                    <%# GetGroupName(Eval("GroupID","{0}")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Purse" HeaderText="<%$Resources:L,资金余额 %>" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="RegTime" HeaderText="<%$Resources:L,注册时间 %>" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="UserExp" HeaderText="<%$Resources:L,积分 %>" />
            <asp:BoundField DataField="LoginTimes" HeaderText="<%$Resources:L,登录次数 %>" />
            <asp:BoundField DataField="LastLockTime" HeaderText="<%$Resources:L,最后登录时间 %>" />
            <asp:TemplateField HeaderText="<%$Resources:L,状态 %>">
                <ItemTemplate>
                    <%# GetStatus(Eval("Status","{0}")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:L,操作 %>">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" CommandName="select" CommandArgument='<%#Eval("UserID") %>' runat="server" CssClass="option_style"><i class="fa fa-users" title="<%=Resources.L.选择会员 %>"></i><%=Resources.L.选择会员 %></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</div>

      <%--  <ZL:ExGridView ID="ExGridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" 
            PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Egv_RowCommand" 
            AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" 
            EnableTheming="False" EnableModelValidation="True" EmptyDataText="无相关数据">--%>

<div class="shoplist" id="shoplist" runat="server" visible="false">
    <ZL:ExGridView ID="EGV1"  runat="server" AutoGenerateColumns="False" PageSize="20"  OnRowCommand="EGV1_RowCommand" OnPageIndexChanging="EGV1_PageIndexChanging" 
        IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无相关信息！！">
        <PagerStyle HorizontalAlign="Center" />
        <RowStyle HorizontalAlign="Center" />
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="true"/>
            <asp:TemplateField HeaderText="<%$Resources:L,商品图片 %>">
                <ItemTemplate>
                    <%# getproimg(Eval("Thumbnails","{0}")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="proname" HeaderText="<%$Resources:L,商品名称 %>" />
            <asp:TemplateField HeaderText="<%$Resources:L,商品类型 %>">
                <ItemTemplate>
                    <%#getshoptype(Eval("ID" ,"{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="AddUser" HeaderText="<%$Resources:L,录入者 %>" />
            <asp:TemplateField HeaderText="<%$Resources:L,价格 %>">
                <ItemTemplate>
                    <%#formatcs(Eval("LinPrice","{0}"),Eval("ProClass","{0}"),Eval("PointVal","{0}")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:L,操作 %>">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton2" CommandArgument='<%# Eval("ID") %>' CommandName="select" runat="server"><%=Resources.L.选择商品 %></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
         <PagerStyle HorizontalAlign="Center" />
    </ZL:ExGridView>
</div>
<div id="orderinfo" class="orderinfo border" runat="server" visible="false">
    <table class="ordertable" style="width:100%;" CellSpacing="1">
        <tr class="tdbg">
            <td colspan="2" class="spacingtitle" align="center"> <%=Resources.L.填写订单信息 %> </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align:right;width:50%;"><%=Resources.L.数量 %>：</td>
            <td>
                <asp:TextBox CssClass="l_input" ID="TextBox1" Text="1" runat="server"></asp:TextBox>
                <asp:Button ID="Button2" runat="server" CssClass="C_input" Text="<%$Resources:L,加数量 %>" OnClick="Button2_Click" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="必须为正整数" ForeColor="Red" ControlToValidate="TextBox1" ValidationExpression="^[0-9]*[1-9][0-9]*$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align:right;"><%=Resources.L.是否已付款 %></td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Value="0" Text="<%$Resources:L,否 %>"></asp:ListItem>
                    <asp:ListItem Value="1" Text="<%$Resources:L,是 %>"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="2" align="center" ><asp:Button CssClass="C_input" ID="Button1" runat="server" Text="<%$Resources:L,添加订单 %>" OnClick="Button1_Click" /></td>
        </tr>
    </table>
</div>
<asp:HiddenField ID="HiddenField1" runat="server" />
<asp:HiddenField ID="HiddenField2" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>