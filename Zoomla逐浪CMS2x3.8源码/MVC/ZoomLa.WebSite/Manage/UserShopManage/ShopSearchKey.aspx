<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopSearchKey.aspx.cs" Inherits="ZoomLaCMS.Manage.UserShopMannger.ShopSearchKey"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>搜索管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" AllowPaging="True" 
            AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有内容">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <input name="Item" type="checkbox" value="<%#Eval("ID") %>">
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="ID" DataField="ID" />
                <asp:TemplateField HeaderText="搜索关键字">
                    <ItemTemplate>
                        <span style="cursor: pointer" onclick="location.href='Addsearchkey.aspx?menu=edit&id=<%#Eval("ID") %>'"><%#Eval("Searchkey") %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="分类">
                    <ItemTemplate>
                        <%#Getclass(Eval("Class","{0}")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="搜索次数" DataField="SearchNum" />
                <asp:BoundField HeaderText="搜索时间" DataField="SearchTime" />
                <asp:TemplateField HeaderText="首页显示">
                    <ItemTemplate>
                        <%#GetShowtop(Eval("Showtop","{0}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="推荐">
                    <ItemTemplate>
                        <%#GetCommend(Eval("Commend","{0}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="Addsearchkey.aspx?menu=edit&id=<%#Eval("ID") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a> 
                        <a href="?menu=del&id=<%#Eval("ID") %>" onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <asp:Button ID="Button3" class="btn btn-primary" runat="server" Text="批量删除" CommandName="5" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" OnClick="Button3_Click" />
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        function CheckAll(spanChk)//CheckBox全选
        {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
        }
        $().ready(function () {
            $("#Egv tr>th:eq(0)").html("<input type=checkbox id='chkAll'/>");//EGV顶部
            //$("#Egv  tr>th").css("height", "30px").css("line-height", "30px");
            $("#chkAll").click(function () {//EGV 全选
                selectAllByName(this, "Item");
            });
        })
    </script>
</asp:Content>
