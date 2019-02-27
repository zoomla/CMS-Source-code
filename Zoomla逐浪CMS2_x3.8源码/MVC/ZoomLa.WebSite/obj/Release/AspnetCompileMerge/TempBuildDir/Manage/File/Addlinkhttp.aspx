<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Addlinkhttp.aspx.cs" Inherits="ZoomLaCMS.Manage.FtpFile.Addlinkhttp" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>站内链接</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Lnk_Click" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无相关信息！！">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="chkSel" title="" value='<%#Eval("ID") %>' />
                </ItemTemplate>
                <HeaderStyle CssClass="hidden-xs hidden-sm" Width="5%" />
                <ItemStyle CssClass="hidden-xs hidden-sm" />
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField HeaderText="关键字" DataField="KeyWord" />
            <asp:TemplateField HeaderText="链接">
                <ItemTemplate>
                    <%# GetUrl(Eval("KeyValue","{0}")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="正则表达式" DataField="Regex" />
            <asp:BoundField HeaderText="正则替换值" DataField="RegexValue" />
            <asp:BoundField HeaderText="权值" DataField="Priority" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="tjlink.aspx?ID=<%#Eval("ID") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton ID="LinkButton2" CommandName="del1" CommandArgument='<%#Eval("ID") %>' runat="server" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <div>
        <asp:Button ID="Button3" class="btn btn-primary" runat="server" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" Text="批量删除" OnClick="Button3_Click1" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        $().ready(function () {
            $("#Egv tr>th:eq(0)").html("<input type=checkbox id='chkAll'/>");//EGV顶部
            //$("#Egv  tr>th").css("height", "30px").css("line-height", "30px");
            $("#chkAll").click(function () {//EGV 全选
                selectAllByName(this, "chkSel");
            });

            if (getParam("type")) {
                $("li a[href='#tab" + getParam("type") + "']").parent().addClass("active").siblings("li").removeClass("active");;
            }
        });
    </script>
</asp:Content>
