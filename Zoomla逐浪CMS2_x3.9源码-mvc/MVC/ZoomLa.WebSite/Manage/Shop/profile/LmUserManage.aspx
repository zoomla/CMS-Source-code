<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LmUserManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.profile.LmUserManage"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>联盟会员</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"  DataKeyNames="UserID" AllowSorting="True" GridLines="None" RowStyle-CssClass="tdbg"
   EnableModelValidation="True" OnPageIndexChanging="EGV_PageIndexChanging" OnRowDataBound="Egv_RowDataBound" >
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="chkSel" runat="server" />
            </ItemTemplate>
            <HeaderStyle Width="3%" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="会员名">
            <ItemTemplate>
                <a href="/User/Userinfo.aspx?id=<%# Eval("UserID") %>">
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                </a>
            </ItemTemplate>
            <HeaderStyle Width="10%" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:BoundField DataField="RegTime" HeaderText="注册时间" SortExpression="RegTime" DataFormatString="{0:yyyy-MM-dd}">
            <HeaderStyle Width="20%" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <%--<asp:BoundField HeaderText="已付款" />
    <asp:BoundField HeaderText="待批" />
    <asp:BoundField HeaderText="拒批" />
    <asp:BoundField HeaderText="状态" />--%>
        <asp:TemplateField HeaderText="点击">
            <ItemTemplate>
                <%#GetCli(Eval("userid").ToString())%>
            </ItemTemplate>
            <HeaderStyle Width="10%" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="销售">
            <ItemTemplate>
                <%#GetXs(Eval("userid").ToString())%>
            </ItemTemplate>
            <HeaderStyle Width="10%" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="引导">
            <ItemTemplate>
                <%#GetGuide(Eval("userid").ToString())%>
            </ItemTemplate>
            <HeaderStyle Width="10%" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="批准">
            <ItemTemplate>
                <%#GetYj(Eval("userid").ToString())%>
            </ItemTemplate>
            <HeaderStyle Width="10%" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="上级">
            <ItemTemplate>
                <a href='<%#Hrefnone(Eval("parentuserid").ToString()) %>'>
                    <%# Eval("tjr") %>
                </a>
            </ItemTemplate>
            <HeaderStyle Width="10%" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="操作" ShowHeader="False"></asp:TemplateField>
    </Columns>
     <PagerStyle HorizontalAlign="Center" />
                    <RowStyle Height="24px" HorizontalAlign="Center"  />
            </ZL:ExGridView>
<span id="Fy" style="text-align: center" runat="server">共
    <asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
    条数据
    <asp:LinkButton ID="Toppage" runat="server" OnClick="Toppage_Click">首页</asp:LinkButton>&nbsp;
    <asp:LinkButton ID="Nextpage" runat="server" OnClick="Nextpage_Click">上一页</asp:LinkButton>&nbsp;
    <asp:LinkButton ID="Downpage" runat="server" OnClick="Downpage_Click">下一页</asp:LinkButton>&nbsp;
    <asp:LinkButton ID="Endpage" runat="server" OnClick="Endpage_Click">尾页</asp:LinkButton>&nbsp;
    &nbsp;页次：
    <asp:Label ID="Nowpage" runat="server" Text="" />
    /
    <asp:Label ID="PageSize" runat="server" Text="" />
    页
    <asp:Label ID="pagess" runat="server" Text="" />
    <asp:TextBox ID="txtPage" name="page" runat="server" AutoPostBack="true"
         OnTextChanged="txtPage_TextChanged">10</asp:TextBox>
    条数据/页 转到第
    <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
    </asp:DropDownList>
    页
    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtPage"
        ErrorMessage="只能输入数字" Type="Integer" MaximumValue="100000" MinimumValue="0"></asp:RangeValidator>
</span>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        HideColumn("0,2,3,4,5,6"); 
    </script>
</asp:Content>


