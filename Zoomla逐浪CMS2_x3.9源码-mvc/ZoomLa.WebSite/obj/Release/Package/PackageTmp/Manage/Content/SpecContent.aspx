<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecContent.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.SpecContent" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title><%=lang.LF("专题内容管理")%></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Label ID="Label1" runat="server" Text="专题名" Visible="false"></asp:Label>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td>
                <span>
                    <asp:DropDownList ID="ddlOrder" runat="server" CssClass="form-control" Width="150" AutoPostBack="true" OnSelectedIndexChanged="ddlOrder_SelectedIndexChanged">
                        <asp:ListItem Value="GeneralID">按ID排序</asp:ListItem>
                        <asp:ListItem Value="CreateTime">按发布时间排序</asp:ListItem>
                        <asp:ListItem Value="UpDateTime">按更新时间排序</asp:ListItem>
                    </asp:DropDownList></span>
                <span>
                    <asp:DropDownList ID="ddlConditions" runat="server" CssClass="form-control" Width="80" AutoPostBack="True" OnSelectedIndexChanged="ddlConditions_SelectedIndexChanged">
                        <asp:ListItem Value="desc">降序</asp:ListItem>
                        <asp:ListItem Value="asc">升序</asp:ListItem>
                    </asp:DropDownList>
                </span>
            </td>
        </tr>
    </table>
    <div id="nocontent" runat="server" style="margin:5px;margin-bottom:20px;">
        <span class='alert alert-info'  style='padding:9px;'>该专题无内容!!!</span>
    </div>
    <ZL:ExGridView runat="server" ID="ExGridView1" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  
                CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" 
                OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" >     
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="idchk" title="" value='<%#Eval("GeneralID") %>' />
                </ItemTemplate>
                <HeaderStyle Width="5%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" DataField="GeneralID">
                <HeaderStyle Width="5%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="标题">
                <ItemTemplate>
                    <a style='<%#Eval("TitleStyle","{0}") %>' href="ShowContent.aspx?GID=<%# Eval("GeneralID")%>&modeid=<%#Eval("ModelID") %>">
                        <%# Eval("Title")%></a>
                </ItemTemplate>
                <HeaderStyle Width="30%" />
                <ItemStyle CssClass="tdbg" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="录入人" DataField="Inputer">
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField HeaderText="点击数" DataField="Hits">
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="推荐">
                <ItemTemplate>
                    <%# GetElite(Eval("EliteLevel", "{0}")) %>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <%# GetStatus(Eval("Status", "{0}")) %>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="已生成">
                <ItemTemplate>
                    <%# GetCteate(Eval("IsCreate", "{0}"))%>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="EditContent.aspx?GeneralID=<%# Eval("GeneralID")%>"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Del" CommandArgument='<%# Eval("GeneralID") %>' OnClientClick="return confirm('你确定将该数据从专题中删除吗？')"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
     <PagerStyle HorizontalAlign="Center" />
        <RowStyle Height="24px" HorizontalAlign="Center"  />
</ZL:ExGridView>
    <asp:Button ID="Button1" runat="server" Text="从所属专题删除" class="btn btn-primary" OnClick="btnDelete_Click" OnClientClick="if(!IsSelectedId()){alert('请选择内容项');return false;}else{return confirm('你确定要从专题中删除选中内容吗？')}" />
    <%--<asp:Button ID="Button2" runat="server" Text="添加到其他专题" OnClick="btnAddTo_Click" class="btn btn-primary" OnClientClick="if(!IsSelectedId()){alert('请选择内容项');return false;}" />
    <asp:Button ID="Button3" runat="server" Text="移动到其他专题" OnClick="btnMoveTo_Click" class="btn btn-primary" OnClientClick="if(!IsSelectedId()){alert('请选择内容项');return false;}" />--%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Common.js"></script>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript">
        function change(id) {
            location.href = "EditContent.aspx?GeneralID=" + id;
        }
        $().ready(function () {
            
            if (getParam("type")) {
                $("li a[href='#tab" + getParam("type") + "']").parent().addClass("active").siblings("li").removeClass("active");;
            }
        })
        function ShowTabs(n) {
            location.href = 'CommentManage.aspx?type=' + n;
        }
        function IsSelectedId() {
            var checkArr = $("input[type=checkbox][name=idchk]:checked");
            if (checkArr.length > 0)
                return true
            else
                return false;
        }
    </script>
</asp:Content>
