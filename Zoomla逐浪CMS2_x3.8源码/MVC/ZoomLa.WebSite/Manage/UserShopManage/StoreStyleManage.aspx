<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoreStyleManage.aspx.cs" Inherits="ZoomLaCMS.Manage.UserShopMannger.StoreStyleManage" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>商品列表</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Egv_RowCommand" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有内容" OnRowDataBound="Egv_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <input type="checkbox" name="idchk" title="" value='<%#Eval("ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="ID" DataField="ID" />
                <asp:TemplateField HeaderText="模板缩略图">
                    <ItemTemplate>
                        <asp:Image ID="img2" runat="server" Height="50" ImageUrl='<%#GetImg(Eval("ID").ToString())%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="模板名称" DataField="StyleName" />
                <asp:TemplateField HeaderText="所属模型">
                    <ItemTemplate>
                        <%#GetModel(Eval("ModelID").ToString())%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="模板状态">
                    <ItemTemplate>
                        <%#GetState(Eval("IsTrue").ToString())%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="StoreStyleEdit.aspx?id=<%#Eval("ID")%>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="del1" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td style="height: 21px">&nbsp;<asp:Button ID="Button1" class="btn btn-primary" Style="width: 110px;" runat="server"
                Text="设为启用" CommandName="1" OnClick="Button2_Click" />
                <asp:Button ID="Button2" class="btn btn-primary" Style="width: 110px;" runat="server" Text="设为停用"
                    CommandName="0" OnClick="Button2_Click" />
                <asp:Button ID="Button3" class="btn btn-primary" Style="width: 110px;" runat="server" Text="批量删除"
                    CommandName="5" OnClick="Button2_Click" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" />
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript" src="/js/Common.js"></script>
    <script>
        $().ready(function () {
            
        })
        function CheckAll(spanChk) //CheckBox全选
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
    </script>
</asp:Content>

