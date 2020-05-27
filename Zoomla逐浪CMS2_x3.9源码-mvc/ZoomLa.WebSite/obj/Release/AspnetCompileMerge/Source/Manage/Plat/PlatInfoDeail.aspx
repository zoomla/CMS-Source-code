<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlatInfoDeail.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="ZoomLaCMS.Manage.Plat.PlatInfoDeail" %>
<asp:Content ContentPlaceHolderID="head" Runat="Server">
    <title>留言内容</title>
    <script type="text/javascript" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content  ContentPlaceHolderID="Content" Runat="Server">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>ID</strong></td>
            <td class="tdbg" style="width: 85%;">
                <asp:Label ID="ID_L" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>分享人</strong></td>
            <td class="tdbg" style="width: 85%;">
                <a runat="server" id="CUser_A"></a>
                <asp:Literal runat="server" ID="CUser_L"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center; vertical-align: middle;"><strong>分享内容</strong></td>
            <td class="tdbg" style="width: 85%;">
                <asp:Label runat="server" ID="MsgContent_L" data-show="onlyread"></asp:Label>
                <div style="display: none;" data-show="edit">
                    <asp:TextBox ID="MsgContent_T" runat="server" TextMode="MultiLine" Style="width: 90%; height: 200px;" />
                </div>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>创建时间</strong></td>
            <td class="tdbg" style="width: 85%;">
                <asp:Label ID="CDate_L" runat="server" data-show="onlyread"></asp:Label>
                <asp:TextBox ID="CDate_T" runat="server" CssClass="form-control" data-show="edit" Style="display:none;max-width:300px;" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm'});"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="text-align: center;"><strong>操作</strong></td>
            <td class="tdbg">
                <input type="button" id="edit_btn" onclick="EditFunc('edit');" data-show="onlyread" value="修改" class="btn btn-primary" />
                <asp:Button ID="Save_Btn" Style="display:none" CssClass="btn btn-primary" data-show="edit" runat="server" OnClick="Save_Btn_Click" Text="保存" />
                <input type="button" id="onlyread_btn" onclick="EditFunc('onlyread');" value="取消修改" class="btn btn-primary" style="display:none;" data-show="edit" />
            </td>
        </tr>
    </table>
    <ZL:ExGridView ID="EGV" CssClass="table table-striped table-bordered table-hover" runat="server" OnPageIndexChanging="EGV_PageIndexChanging" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EmptyDataText="该内容没有回复">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <%#Eval("ID") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="信息内容">
                <ItemStyle Width="40%" />
                <ItemTemplate>
                    <%#getText() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="分享人">
                <ItemTemplate>
                    <%#Eval("CreateUName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="创建时间">
                <ItemStyle Width="10%" />
                <ItemTemplate>
                    <%#Eval("CreateTime") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="PlatInfoDeail.aspx?ID=<%#Eval("ID") %>">详情</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:Button ID="Dels" runat="server" OnClientClick="return confirm('确定要删除选中的内容吗？')" CssClass="btn btn-primary" Text="批量删除" OnClick="Dels_Click" />

    <%=Call.GetUEditor("MsgContent_T", 4) %>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        function EditFunc(flag) {
            $("[data-show=onlyread]").toggle();
            $("[data-show=edit]").toggle();
        }
    </script>
</asp:Content>