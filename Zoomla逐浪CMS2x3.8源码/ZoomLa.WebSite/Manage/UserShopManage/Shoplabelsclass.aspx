<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Shoplabelsclass.aspx.cs" Inherits="manage_UserShopManage_Shoplabelsclass" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>信息配置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table width="100%" cellpadding="2" cellspacing="1" class="border" style="background-color: white;">
        <tbody id="Tbody1">
            <tr class="tdbg">
                <td width="100%" height="24" align="left" class="tdbgleft" colspan="2">
                    <asp:Label ID="lablelistname" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
    </table>
    <div>
        <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无相关信息！！" OnRowDataBound="Egv_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <%#Getinput(Eval("ID","{0}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="标签名称">
                    <ItemTemplate>
                        <span style="cursor: pointer" onclick="location.href='addshoplabels.aspx?menu=edit&id=<%#Eval("ID") %>'"><%#GetName(Eval("ID","{0}"))%></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="标签说明">
                    <ItemTemplate>
                        <%#Eval("LableInfo") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="标签分类">
                    <ItemTemplate>
                        <%#Eval("LableClass", "{0}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="标签状态">
                    <ItemTemplate>
                        <%#Getture(Eval("IsTrue","{0}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <%#Getbottom(Eval("ID","{0}")) %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
    </div>
    [帮助:]什么是派生标签？派生标签就是继承所派生的标签所有的功能可以另外取名的一种功能<br />
    [提示:]<font color="blue">蓝颜色显示的标签</font>为<font color="green">派生标签</font>，黑色为正常标签<b> [注意:]标签名不能重复!</b><br />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="设为启用" CommandName="1" OnClick="Button1_Click" />
                <asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" Text="设为停用" CommandName="0" OnClick="Button2_Click" />
                <asp:Button ID="Button3" CssClass="btn btn-primary" runat="server" Text="批量删除" CommandName="5" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" OnClick="Button3_Click" />
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

        $().ready(function () {
            $("#Egv tr>th:eq(0)").html("<input type=checkbox id='chkAll'/>");//EGV顶部
            //$("#Egv  tr>th").css("height", "30px").css("line-height", "30px");
            $("#chkAll").click(function () {//EGV 全选
                selectAllByName(this, "Item");
            });
        })
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
    </script>
</asp:Content>
