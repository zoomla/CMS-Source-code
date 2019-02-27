<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListDevice.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.Printer.ListDevice" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>设备列表</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" class="table table-striped table-bordered table-hover" EmptyDataText="您还没有打印设备"
        PageSize="10" OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_s">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-CssClass="td_s" />
            <asp:TemplateField HeaderText="名称">
                <ItemTemplate>
                    <a href="MessageList.aspx?DevID=<%#Eval("ID") %>"><%#Eval("Alias") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ShopName" HeaderText="所属门店" />
            <asp:BoundField DataField="DeviceNo" HeaderText="设备编码" />
            <asp:TemplateField HeaderText="默认设备">
                <ItemTemplate><%#Eval("IsDefault").ToString().Equals("1")?"是":"否" %></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Remind" HeaderText="备注" />
            <asp:TemplateField HeaderText="<%$Resources:L, 操作%>">
                <ItemTemplate>
                    <a class="option_style" href="TestPrint.aspx?DevId=<%#Eval("ID") %>"><i class="fa fa-print" title="打印"></i>打印</a>
                    <a class="option_style" href="AddDevice.aspx?id=<%#Eval("ID") %>"><i class="fa fa-pencil" title="修改"></i>修改</a>
                    <asp:LinkButton ID="SetDef_B" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="setDef" OnClientClick='<%# Eval("IsDefault").ToString().Equals("1") ? "return false;":"return true;" %>' CssClass="option_style"><span style='<%# Eval("IsDefault").ToString().Equals("1") ? "color:#b1b1b1;":"" %>'><i class="fa fa-check"></i>设为默认</span></asp:LinkButton>
                    <asp:LinkButton ID="Del" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del" OnClientClick="return confirm('确认删除？')" CssClass="option_style"><i class="fa fa-trash-o" title="<%=Resources.L.删除 %>"></i><%=Resources.L.删除 %></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:Button ID="Dels_Btn" runat="server" CssClass="btn btn-primary" Text="批量删除" OnClientClick="return confirm('确定要删除这些设备吗?');" OnClick="Dels_Btn_Click" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>
