<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PageRecyle.aspx.cs" Inherits="Manage_I_Page_PageRecyle" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>回收站</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">

     <div>
         <div class="us_seta">
             <div class="input-group pull-right" style="width:300px; margin-left:5px;">
                 <input type="text" runat="server" id="TxtSearchTitle" class="form-control" style="color: #666;" placeholder="请输入标题" />
                 <span class="input-group-btn">
                 <asp:Button ID="Btn_Search" runat="server" Text="搜索" CssClass="btn btn-primary" OnClick="Btn_Search_Click" />
                 </span>
             </div>
             <ZL:ExGridView ID="Egv" runat="server" AllowPaging="True" OnRowCommand="Lnk_Click" AutoGenerateColumns="False" DataKeyNames="GeneralID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" CellPadding="4" GridLines="None" CssClass="table table-striped table-bordered table-hover"
                 EmptyDataText="没有相关数据">
                 <Columns>
                     <asp:TemplateField HeaderText="选择">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSel" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="GeneralID" HeaderText="ID">
                            <HeaderStyle Width="6%" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="标题">
                            <HeaderStyle Width="50%" />
                            <ItemTemplate>
                                <a href="<%# GetUrl(Eval("GeneralID", "{0}"))%>" target="_blank"><%# GetModel(Eval("GeneralID", "{0}"))%><%# Eval("Title")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态">
                            <ItemTemplate>
                                <%# GetStatus(Eval("Status", "{0}")) %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="已生成">
                            <ItemTemplate>
                                <%# GetCteate(Eval("IsCreate", "{0}"))%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="Btn_Rec" runat="server" CommandName="Rec" CommandArgument='<%# Eval("GeneralID") %>' ToolTip="还原"><span class="fa fa-repeat"></span></asp:LinkButton>
                                <asp:LinkButton ID="Btn_Del" runat="server" CommandName="Del" CommandArgument='<%# Eval("GeneralID") %>' OnClientClick="return confirm('你确定将该数据彻底删除吗？')" ><span class="fa fa-trash-o"></span></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </ZL:ExGridView>
                <div>
                    <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" Font-Size="9pt" OnCheckedChanged="CheckBox2_CheckedChanged" Text="选中本页显示的所有项目" />
                    <asp:Button ID="Button1" runat="server" Text="批量还原" OnClick="btnRecAll_Click" OnClientClick="if(!IsSelectedId()){alert('请选择还原项');return false;}else{return confirm('你确定要将所选中的项还原吗？')}" CssClass="btn btn-primary" UseSubmitBehavior="true" />
                    <asp:Button ID="Bat_Del" Text="批量删除" runat="server" OnClick="Bat_Del_Click" OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('你确定要将所选中的项删除吗？')}" CssClass="btn btn-primary"  />
                    <asp:Button ID="Btn_DelAll" Text="全部清空" runat="server" OnClick="Btn_DelAll_Click" OnClientClick="return confirm('你确定将该数据彻底删除吗？')" CssClass="btn btn-primary"  />
                    <script>
                        document.getElementById("TxtSearchTitle").value.trim();
                    </script>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <asp:HiddenField ID="HiddenField3" runat="server" />
                    <asp:HiddenField ID="HiddenField4" runat="server" />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdid" runat="server" />
        <asp:HiddenField ID="hdflag" runat="server" />
        <asp:HiddenField ID="hdnoid" runat="server" />
        <asp:HiddenField ID="hdmdid" runat="server" />
</asp:Content>