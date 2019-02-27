<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderRepairAudit.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="Manage_Shop_OrderRepairAudit" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title><%=Resources.L.售后审核 %></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
        <ZL:ExGridView ID="EGV" runat="server" AllowPaging="True" AllowSorting="True" 
            AutoGenerateColumns="False" OnPageIndexChanging="EGV_PageIndexChanging" 
            class="table table-striped table-bordered table-hover" PageSize="10" EmptyDataText="<%$Resources:L,没有相关数据 %>" >
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <input type="checkbox" value="<%#Eval("ID") %>" name="idchk" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:L,商品信息 %>">
                    <ItemTemplate>
                        <div class="container-fluid">
                            <div class="col-md-2">
                                <img src="/<%#Eval("Thumbnails") %>" style="width:100%; height:50px;" />
                            </div>
                            <div class="col-md-10">
                                <%#Eval("Proname") %>
                            </div>
                        </div>
                    </ItemTemplate>
                    <ItemStyle Width="30%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:L,售后类型 %>">
                    <ItemTemplate>
                        <%#GetServieType() %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:L,申请凭据 %>">
                    <ItemTemplate>
                        <%#GetCret() %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UserName" HeaderText="<%$Resources:L,客户姓名 %>" />
                <asp:BoundField DataField="Phone" HeaderText="<%$Resources:L,联系方式 %>" />
                <asp:BoundField DataField="TakeTime" HeaderText="<%$Resources:L,取件时间 %>" />
                <asp:TemplateField HeaderText="<%$Resources:L,审核状态 %>">
                    <ItemTemplate>
                        <%#GetStatus() %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CDate" HeaderText="<%$Resources:L,创建时间 %>" />
                <asp:TemplateField HeaderText="<%$Resources:L,操作 %>">
                    <ItemTemplate>
                        <a href="RepairDeailt.aspx?id=<%#Eval("ID") %>" class="option_style"><i class="fa fa-eye" title="<%=Resources.L.查看 %>"></i><%=Resources.L.查看详情 %></a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
    <asp:Button ID="Audit_Btn" CssClass="btn btn-primary" runat="server" Text="<%$Resources:L,批量审核 %>" OnClick="Audit_Btn_Click" />
    <asp:Button ID="UnAudit_Btn" CssClass="btn btn-primary" runat="server" Text="<%$Resources:L,解除审核 %>" OnClick="UnAudit_Btn_Click" />
    <asp:Button ID="Del_Btn" CssClass="btn btn-primary" runat="server" Text="<%$Resources:L,批量删除 %>" OnClick="Del_Btn_Click" />

</asp:Content>





