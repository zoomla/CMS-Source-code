<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectDictionary.aspx.cs" Inherits="ZoomLa.WebSite.Manage.AddOn.SelectDictionary" EnableViewStateMac="false" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Head"><title>选择数据字典</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table  class="TableWrap"  id="sleall">
    <tr>
        <td style="height: 21px">
            <%--<asp:TextBox ID="txtCategoryName" runat="server" Width="200px" class="form-control"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" class="btn btn-primary"/>--%>
            <div class="input-group" style="width: 300px; margin:5px;">
                <span class="input-group-addon">分类名：</span>
                <asp:TextBox ID="txtCategoryName" CssClass="form-control text_md num" runat="server" placeholder="分类名"></asp:TextBox>
                <span class="input-group-btn">
                    <asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                </span>
                <asp:HiddenField ID="HdnNameKey" runat="server" />
            </div>
            </td>
    </tr>                
</table>
<div class="clearbox"></div>
<ZL:ExGridView ID="EGV" DataKeyNames="DicCateID" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" OnRowCommand="Lnk_Click" 
    EmptyDataText="无相关数据" CssClass="table table-bordered table-hover table-striped">
    <Columns>                
        <asp:BoundField DataField="DicCateID" HeaderText="序号">
        <ItemStyle HorizontalAlign="Center" />
        <HeaderStyle Width="5%" />
        </asp:BoundField>                                               
        <asp:TemplateField HeaderText="分类名">
            <ItemTemplate>                                
               <%# Eval("CategoryName")%>
            </ItemTemplate>
             <ItemStyle HorizontalAlign="Left" />
             <HeaderStyle Width="50%" />
        </asp:TemplateField>                                                      
        <asp:TemplateField HeaderText="操作">
        <HeaderStyle Width="19%" />
        <ItemTemplate>
            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Select" CommandArgument='<%# Eval("DicCateID") %>'>选择</asp:LinkButton>                    
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
    </Columns>
    <PagerStyle HorizontalAlign="Center" />
    <RowStyle Height="24px" HorizontalAlign="Center" />
</ZL:ExGridView>
    <div style="padding: 5px;">
        <asp:TextBox ID="TxtSelectDic" runat="server" TextMode="MultiLine" Width="400px" Height="160px"></asp:TextBox><br />
        <asp:Button ID="Button1" runat="server" Text="确定" OnClientClick="add();" class="btn btn-primary" />
        <asp:HiddenField ID="HdnContrID" runat="server" />
    </div>
<script type="text/javascript">
    function add() {
        var conid = document.getElementById('<%= HdnContrID.ClientID %>').value;
        opener.document.getElementById(conid).value = document.getElementById('<%= TxtSelectDic.ClientID %>').value;
        window.close();
    }
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script"></asp:Content>