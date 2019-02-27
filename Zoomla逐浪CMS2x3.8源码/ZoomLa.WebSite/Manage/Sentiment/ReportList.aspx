<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportList.aspx.cs" Inherits="Manage_Sentiment_ReportList" MasterPageFile="~/User/Empty.master" ValidateRequest="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="reportlist">
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" OnPageIndexChanging="EGV_PageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <input type="checkbox" value="<%#Eval("Name") %>" name="idchk" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="名称" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="路径">
                    <ItemTemplate>/UploadFiles/Report/<%#Eval("Name") %></ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField DataField="CreateTime" HeaderText="创建时间" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="/UploadFiles/Report/<%#Eval("Name") %> " class="option_style"><i class="fa fa-download"></i>下载</a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
            <RowStyle Height="24px" HorizontalAlign="Center" />
        </ZL:ExGridView>
        <asp:Button runat="server" ID="BatDel_Btn" Text="批量删除" OnClick="BatDel_Btn_Click" OnClientClick="return confirm('确定要删除吗?');" class="btn btn-primary" />
    </div>
    <style>
        #AllID_Chk::after{ display:block; content:"全选"; padding-left:15px; width:50px; cursor:pointer; }
    </style>
</asp:Content>
