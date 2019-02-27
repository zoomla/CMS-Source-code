<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConPush.aspx.cs" Inherits="test_ConPush" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>智农推送</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
        <li><a href="<%=CustomerPageAction.customPath2 %>Main.aspx">工作台</a></li>
        <li><a href="<%=CustomerPageAction.customPath2 %>"user/UserManage.aspx">用户管理</a></li>
        <li>智农推送</li>
    </ol>
    <div style="height:45px;"></div>
    <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <div class="col-lg-3 col-md-3 col-xs-3 col-sm-3">
        <table class="table table-striped table-hover table-bordered">
            <tr><td>节点列表</td></tr>
            <asp:Repeater runat="server" ID="NodeRPT">
                <ItemTemplate>
                    <tr>
                        <td>
                            <input type="checkbox" checked="checked" name="nodechk" value="<%#Eval("NodeID") %>" onclick="ReBind();" />
                            <%#Eval("SourceAlias") %><span class="fa fa-arrow-right"></span><%#Eval("T_CateName") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div class="col-lg-9 col-md-9 col-xs-9 col-sm-9">
        <asp:UpdatePanel ID="LeftPanel" runat="server"><ContentTemplate>
            <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" 
                OnPageIndexChanging="EGV_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True"
                CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有内容">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input type="checkbox" name="idchk" value="<%#Eval("Index") %>" />
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="封面图">
                        <ItemTemplate>
                            <img src="<%#Eval("ImgUrl") %>" onerror="shownopic(this);" class="img_50" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="标题" DataField="Title" />
                    <asp:BoundField HeaderText="作者" DataField="Author" />
                    <asp:BoundField HeaderText="目标节点" DataField="CateName" />
                    <asp:TemplateField HeaderText="日期">
                        <ItemTemplate>
                            <%#Eval("PublishDate","{0:yyyy年MM月dd日 HH:mm}") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </ZL:ExGridView>
            <asp:Button runat="server" ID="PushSel_Btn" Text="推送选定" OnClick="PushSel_Btn_Click" CssClass="btn btn-primary" OnClientClick="disBtn(this);" />
            <asp:Button runat="server" ID="PushAll_Btn" Text="推送全部" OnClick="PushAll_Btn_Click" CssClass="btn btn-primary" OnClientClick="return confirm('确定要推送全部内容吗');"/>
            <asp:Button runat="server" ID="ReBind_Btn" OnClick="ReBind_Btn_Click" style="display:none;" />
            <asp:HiddenField runat="server" ID="Nodes_Hid" />
        </ContentTemplate></asp:UpdatePanel>
        <div class="alert alert-info margin_t5">需要增加或修改节点映射,请修改/Config/Conpush.config文件</div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        function ReBind() {
            var nodes = "";
            $("input[name=nodechk]:checked").each(function () {
                nodes += $(this).val() + ",";
            });
            $("#Nodes_Hid").val(nodes);
            $("#ReBind_Btn").trigger("click");
            var obj = document.getElementById("PushAll_Btn");
            disBtn(obj);
        }
    </script>
</asp:Content>