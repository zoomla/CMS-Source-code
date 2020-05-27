<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Zone.CarManage" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>车辆管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="PID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" OnRowCommand="Egv_RowCommand" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有车辆！！">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <input type="checkbox" name="chkSel" title="" value='<%#Eval("PID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="ID" DataField="PID" />
                <asp:TemplateField HeaderText="图片">
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# ZoomLa.Components.SiteConfig.SiteOption.UploadDir + Eval("P_car_img") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="LOGO">
                    <ItemTemplate>
                        <asp:Image ID="Image2" runat="server" ImageUrl='<%# ZoomLa.Components.SiteConfig.SiteOption.UploadDir + Eval("P_car_img_logo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="名称" DataField="P_car_name" />
                <asp:BoundField HeaderText="价格" DataField="P_car_money" />
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="CarEdit.aspx?id=<%# Eval("PID") %>" class="option_style"><i class="fa fa-pencil" title="编辑"></i>编辑</a>
                        <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("PID") %>' CommandName="stop" runat="server"><%#GetStr(DataBinder.Eval(Container.DataItem,"P_car_check").ToString())%></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
    </div>
</asp:Content>
