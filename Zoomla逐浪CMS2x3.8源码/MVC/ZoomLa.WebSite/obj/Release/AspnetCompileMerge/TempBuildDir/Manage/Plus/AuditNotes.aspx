<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Manage/I/Default.master" CodeBehind="AuditNotes.aspx.cs" Inherits="ZoomLaCMS.Manage.Plus.AuditNotes" %>


<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>审核记录</title>
    <style>
        #AllID_Chk{display:none;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <ZL:ExGridView ID="Egv" CssClass="table table-striped table-bordered table-hover" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="Egv_PageIndexChanging" runat="server" AutoGenerateColumns="False" PageSize="20" Width="100%" EmptyDataText="无相关数据">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="内容标题">
                    <ItemStyle CssClass="rap" /> 
                    <HeaderStyle Width="18%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                        <%# GetStatus(Eval("Status", "{0}")) %>
                    </ItemTemplate>
                    <HeaderStyle Width="10%" /> 
                </asp:TemplateField>
                <asp:BoundField DataField="UpDateTime" HeaderText="更新时间"> 
                    <HeaderStyle Width="16%" />
                </asp:BoundField>
                <asp:BoundField DataField="because_back" HeaderText="审核原因"> 
                    <HeaderStyle Width="20%" />
                </asp:BoundField>
            </Columns>
        </ZL:ExGridView>
    </div>
    <asp:HiddenField ID="HdnCate" runat="server" />
</asp:Content>
