<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="CashInfo.aspx.cs" Inherits="User_producter_CashInfo" ClientIDMode="Static" ValidateRequest="false" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>注册信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/Info/UserInfo.aspx">账户管理</a></li>
        <li class="active">注册信息</li>
    </ol>
    <div class="btn-group">
        <a href="ProducterManage.aspx" class="btn btn-primary">产品管理</a>
        <a href="cash.aspx" class="btn btn-primary">申请现金</a>
        <a href="CashInfo.aspx" class="btn btn-primary">帐户查看</a>
    </div>
    <div class="us_seta" style="margin-top: 10px;" id="manageinfos" runat="server">
        
    </div>
    <div class="us_seta" style="margin-top: 10px;" id="manageinfo" runat="server">
        <h1 style="text-align: center"></h1>
        <div>
            <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" RowStyle-VerticalAlign="Middle"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True">
        <Columns>
            <asp:TemplateField >
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("Y_ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                   <%# Eval("Y_ID")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="银行">
                <ItemTemplate>
                    <%#Eval("Bank") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="帐号" HeaderStyle-Width="45%">
                <ItemTemplate>
                    <%#Eval("Account") %>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="申请金额">
                <ItemTemplate>
                    <%#Eval("Money") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <%#Eval("yState") %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
            <%--<table class="table table-striped table-bordered table-hover">
                <tr>
                    <td colspan="6" class="text-center">申请信息</td>
                </tr>
                <tr align="center">
                    <td width="10%" class="title">
                        <asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" /></td>
                    <td width="10%" class="title">ID</td>
                    <td width="10%" class="title">银行</td>
                    <td width="30%" class="title">帐号</td>
                    <td width="10%" class="title">申请金额</td>
                    <td width="10%" class="title">状态</td>
                </tr>
                <asp:Repeater ID="gvCard" runat="server">
                    <ItemTemplate>
                        <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                            <td height="22" align="center">
                                <input name="Item" type="checkbox" value='<%# Eval("Y_ID")%>' /></td>
                            <td height="22" align="center"><%# Eval("Y_ID")%></td>
                            <td height="22" align="center"><%# Eval("Bank")%></td>
                            <td height="22" align="center"><%# Eval("Account")%></td>
                            <td height="22" align="center"><%#showMoney(Eval("money").ToString())%></td>
                            <td height="22" align="center"><%#shoyState(Eval("yState").ToString())%></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr class="tdbg">
                    <td height="22" colspan="7" align="center" class="tdbgleft">共
	<asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
                        条信息
	<asp:Label ID="Toppage" runat="server" Text="" />
                        <asp:Label ID="Nextpage" runat="server" Text="" />
                        <asp:Label ID="Downpage" runat="server" Text="" />
                        <asp:Label ID="Endpage" runat="server" Text="" />
                        页次：<asp:Label ID="Nowpage" runat="server" Text="" />/<asp:Label ID="PageSize" runat="server" Text="" />页
	<asp:Label ID="pagess" runat="server" Text="" />条信息/页 转到第<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
    </asp:DropDownList>
                        页</td>
                </tr>
            </table>--%>
        </div>
    </div>
</asp:Content>
