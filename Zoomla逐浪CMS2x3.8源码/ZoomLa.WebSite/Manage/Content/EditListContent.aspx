<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditListContent.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Content.EditListContent" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" ValidateRequest="false" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>批量修改</title>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/JS/Common.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:HiddenField runat="server" ID="itemlist" />
    <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" Width="100%" Height="20px" AllowPaging="True" AllowSorting="True" CellPadding="2" CellSpacing="1" BackColor="White" ForeColor="Black" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" GridLines="None" EnableModelValidation="True" EmptyDataText="无相关信息！！">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="idchk" title="" value='<%#Eval("FieldName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="字段别名" DataField="FieldAlias" />
            <asp:BoundField HeaderText="字段名" DataField="FieldName" />
        </Columns>
        <RowStyle HorizontalAlign="Center" />
        <PagerStyle HorizontalAlign="Center" />
    </ZL:ExGridView>
    <div class="us_seta" style="margin-top: 10px;" id="manageinfo" runat="server">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td colspan="2">
                    <h1 style="text-align: center; font-size: 14px;">
                        <asp:Label ID="LblModelName" runat="server" Text=""></asp:Label></h1>
                </td>
            </tr>
            <asp:Literal ID="ModelHtml" runat="server"></asp:Literal><tr class="tdbgbottom border">
                <td colspan="2" class="text-center">
                    <asp:HiddenField ID="HdnModel" runat="server" />
                    <asp:HiddenField ID="HdnModelName" runat="server" />
                    <asp:HiddenField ID="HdnID" runat="server" />
                    <asp:HiddenField ID="HdnType" runat="server" />
                    <asp:TextBox ID="FilePicPath" runat="server" Text="fbangd" Style="display: none"></asp:TextBox>
                    <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />
                    <asp:Button ID="Button1" class="btn btn-primary" Text="返回" runat="server" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        $(function () {
            $(".us_seta").find("input[type=text]").addClass("form-control");
            $(".us_seta").find("select").addClass("form-control");
            $("#Egv tr:last").children().first().attr("colspan", "8").before("<td><input type=checkbox id='chkAll'/></td>");
            //$("#Egv  tr>th").css("height", "30px").css("line-height", "30px");
            $("#chkAll").click(function () {//EGV 全选
                selectAllByName(this, "idchk");
            });
        })
    </script>
</asp:Content>
