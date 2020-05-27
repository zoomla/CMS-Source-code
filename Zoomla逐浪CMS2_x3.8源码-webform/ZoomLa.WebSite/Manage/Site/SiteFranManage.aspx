<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SiteFranManage.aspx.cs" Inherits="Manage_Site_SiteFranManage" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>网站管理</title>
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Content">
<div class="n_site_bread">
<ul class="breadcrumb">
<li><a href="<%= CustomerPageAction.customPath2 %>/Main.aspx">工作台</a></li>
<li><a href="Default.aspx">站群中心</a></li>
<li class="active">网站管理</li>
</ul> 
</div> 
    <div id="site_main" style="margin-left:2%;">
        <table class="table table-bordered table-responsive">
            <tr>
                <td>网站名称：</td>
                <td><asp:Label runat="server" ID="sName" CssClass="text_300" /></td>
            </tr>
            <tr>
                <td>启用时间：</td>
                <td><asp:TextBox runat="server" onclick="WdatePicker();" ID="StartDate" CssClass="form-control text_300" /></td>
            </tr>
            <tr>
                <td>到期时间：</td>
                <td><asp:TextBox runat="server" onclick="WdatePicker();" ID="EndDate" CssClass="form-control text_300"  /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:Button runat="server" ID="editBtn" OnClick="editBtn_Click" Text="修改" CssClass="btn btn-primary" />
                    <asp:Button runat="server" ID="cancelBtn" Text="取消" CssClass="btn btn-primary" />
                </td>
            </tr>
        </table>
    <div id="tab3">
    <div style="width:100%">
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="True" RowStyle-CssClass="tdbg" 
            MouseOverCssClass="tdbgmouseover" CellPadding="2" CellSpacing="1" Width="100%" 
            GridLines="None" EnableTheming="False" EmptyDataText="没有日志！"  AllowSorting="True" CheckBoxFieldHeaderWidth="3%" EnableModelValidation="True" IsHoldState="false" SerialText="">
            <Columns>
                <asp:BoundField HeaderText="ID" DataField="ID" ReadOnly="true"/>
                <asp:TemplateField HeaderText="站点名">
                    <ItemTemplate>
                        <%#GetSiteName(Eval("SiteID").ToString()) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="类型">
                    <ItemTemplate>
                        <%#GetType(Eval("Type")as string) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="说明" DataField="Remind"/>
                <asp:BoundField HeaderText="操作时间" DataField="CreateDate" />
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <RowStyle Height="24px" HorizontalAlign="Center" />
        </ZL:ExGridView>  
    </div> 
    </div>
    </div>
</asp:Content>
