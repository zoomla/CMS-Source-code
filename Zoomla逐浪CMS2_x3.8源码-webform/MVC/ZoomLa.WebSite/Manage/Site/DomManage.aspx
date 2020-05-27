<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DomManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Site.DomManage" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>域名管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="top_opbar">
        <div class="input-group text_300">
            <asp:TextBox runat="server" CssClass="form-control" ID="keyWord" placeholder="域名搜索"/>
            <div class="input-group-btn">
                <asp:Button runat="server" ID="searchBtn" Text="搜索" OnClick="searchBtn_Click" CssClass="btn btn-primary" />
                <input type="button" value="添加" onclick="location.href = 'AddDomain.aspx';" class="btn btn-primary" />
            </div>
        </div>
    </div>
    <ZL:ExGridView runat="server" ID="EGV" CssClass="table table-striped table-bordered table-hover" 
        AutoGenerateColumns="false" AllowPaging="true" RowStyle-CssClass="tdbg"  OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand"
        OnRowCancelingEdit="EGV_RowCancelingEdit" PageSize="10" EnableTheming="False" EmptyDataText="没有任何数据！"  AllowSorting="True" EnableModelValidation="True" IsHoldState="false">
<PagerStyle HorizontalAlign="Center" />
<RowStyle Height="24px" HorizontalAlign="Center" />
    <Columns>
        <%-- <asp:BoundField HeaderText="ID" DataField="ID" />--%>
            <asp:BoundField HeaderText="序号" DataField="ID" ReadOnly="true" />
        <%--   <asp:BoundField HeaderText="站点ID" DataField="SiteID" ReadOnly="true" />--%>
            <asp:TemplateField HeaderText="域名">
            <ItemTemplate>
                <a href="<%# "http://"+Eval("DomName") %>"  target="_blank" title="打开站点"><%#Eval("DomName") %></a>
            </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label runat="server" ID="lDomain" Text='<%#Eval("DomName").ToString().ToLower().Replace("www.","") %>'></asp:Label>
                </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="所属用户" DataField="UserName"/>
<%--                     <asp:TemplateField HeaderText="站点名">
            <ItemTemplate>
                <a href="SiteDetail.aspx?SiteName=<%#Server.UrlEncode(Eval("SiteName") as string) %>" target="_blank" title="站点详情"><%#Eval("SiteName") %></a>
            </ItemTemplate>
        </asp:TemplateField>--%>
        <asp:TemplateField HeaderText="到期日">
            <ItemTemplate>
                <%#DataBinder.Eval(Container.DataItem, "EndDate", "{0:yyyy年M月d日}") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="证书">
            <ItemTemplate>
            <a href="/Plugins/Domain/ViewCert.aspx?id=<%#Eval("ID") %>" target="_viewCert">查看证书</a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="续费多久" Visible="false">
            <ItemTemplate>
            </ItemTemplate>
            <EditItemTemplate>
                <select name="periodDP">
                    <option value="1">1年</option>
                    <option value="2">2年</option>
                    <option value="3">3年</option>
                    <option value="4">4年</option>
                    <option value="5">5年</option>
                </select>
            </EditItemTemplate>
        </asp:TemplateField>
        <%--  <asp:BoundField HeaderText="到期日期" DataField="EndDate" />--%>
            <asp:TemplateField HeaderText="操作">
            <ItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Container.DisplayIndex %>' CommandName="Edit2">续费</asp:LinkButton>
                <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("ID")%>' CommandName="Details">查看详情</asp:LinkButton>
            </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument='<%#Container.DisplayIndex %>' CommandName="Save" OnClientClick="return confirm('你确定要续费该域名吗');">确定</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton4" runat="server" CommandArgument='<%#Container.DisplayIndex %>' CommandName="Cancel">取消</asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>
    </Columns>
</ZL:ExGridView> 
</asp:Content>