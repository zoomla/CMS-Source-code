<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeFile="WorkManage.aspx.cs" Inherits="manage_AddOn_WorkManage" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>项目工作内容列表</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <asp:Label ID="LblProject" runat="server" Text=""></asp:Label>
    <ZL:ExGridView ID="Egv" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
         DataKeyNames="WorkID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" Width="100%" OnRowCommand="GridView1_RowCommand" 
        EmptyDataText="无任何相关数据" OnRowDataBound="Egv_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("WorkID") %>" />
                </ItemTemplate>
                <HeaderStyle Width="4%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="WorkID" HeaderText="ID" ItemStyle-HorizontalAlign="Center">
                <HeaderStyle Width="5%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="流程名称" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href="AddWork.aspx?Wid=<%#Eval("WorkID")%>">
                        <%#DataBinder.Eval(Container.DataItem, "WorkName").ToString()%></a>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" Width="25%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="启用" ItemStyle-HorizontalAlign="Center">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%# (int)Eval("Status") == 0 ? "<span style=\"color: red\">×</span>" : "<font color=green>√</font>"%>
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:HiddenField ID="hfid" runat="server" Value='<%#Eval("WorkID") %>' />
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="EditWork" CommandArgument='<%# Eval("WorkID")%>' CssClass="option_style"><i class="fa fa-pencil" title="修改"></i></asp:LinkButton>
                    <asp:LinkButton ID="LnkDelete" runat="server" CommandName="DelWork" OnClientClick="if(!this.disabled) return confirm('确实要删除吗？');" CommandArgument='<%# Eval("WorkID")%>' CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="FinishWork" CommandArgument='<%# Eval("WorkID")%>' OnClientClick="if(!this.disabled) return confirm('确实要执行此操作吗？');" CssClass="option_style"><i class="fa fa-play-circle"></i><%# (int)Eval("Status") == 0 ? "启用":"停用"%></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:Label ID="Lbl" runat="server" Text=""></asp:Label>
    <div class="clearbox">
    </div>
    <asp:Button ID="btnDel" runat="server" Text="批量删除" OnClick="btnDel_Click" OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('你确定要将所有选择项删除吗？')}"
        class="btn btn-primary" />
    <asp:Label ID="LbComment" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Panel ID="Pl" runat="server" Width="100%" Visible="false">
        <table class="table table-striped table-bordered table-hover">
            <tr align="center">
                <td colspan="2" class="spacingtitle">
                    <asp:Label ID="LblTitle" runat="server" Text="项目评论" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 105px">
                    <strong>项目评价:&nbsp;</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TBCommon" TextMode="MultiLine" Rows="8" Columns="50" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ValrKeywordText" ControlToValidate="TBCommon" runat="server" ErrorMessage="项目名称不能为空！" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 105px">
                    <strong>项目评分:&nbsp;</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtScore" runat="server" TextMode="MultiLine" class="form-control" Height="75px" Width="316px"></asp:TextBox>
                    <font color="red">(注：输入0-100的数字)</font>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TxtScore" Type="Integer" ErrorMessage="评分必须满足0～100" MaximumValue="100" MinimumValue="0"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="text-center">
                    <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" class="btn btn-primary" />
                    <input name="Cancel" type="button" class="btn btn-primary" id="Cancel" value="取消" runat="server" onclick="javascript: history.go(-1)" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:LinkButton ID="LB" runat="server" Visible="false" OnClick="LB_Click">你尚未填写项目评价,马上填写?</asp:LinkButton>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <%--<script type="text/javascript" src="/js/Common.js"></script>--%>
</asp:Content>
