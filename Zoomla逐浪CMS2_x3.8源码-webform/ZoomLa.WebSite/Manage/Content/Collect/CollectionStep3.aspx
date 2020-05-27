<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CollectionStep3.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Content.CollectionStep3" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" ValidateRequest="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加采集项目</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr class="text-center"><td colspan="2"><%=type%></td></tr>
        <tr>
            <td style="width: 20%"class="text-right"><strong>采集项目名称：</strong></td>
            <td><asp:Label ID="lblItemName" runat="server" Text="Label"></asp:Label></td>
        </tr>
    </table>
    <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover" runat="server">
        <ItemTemplate>
            <%#GetHtml(Eval("FieldAlias","{0}"),Eval("FieldName","{0}"),Eval("FieldType","{0}"),Eval("Content","{0}"),Eval("Description","{0}"), Eval("ModelID","{0}"))%>
        </ItemTemplate>
    </asp:DataList>
    <asp:DataList ID="DataList2" CssClass="table table-striped table-bordered table-hover" runat="server">
        <ItemTemplate>
            <%#GetHtml(Eval("FieldAlias","{0}"),Eval("FieldName","{0}"),Eval("FieldType","{0}"),Eval("Content","{0}"),Eval("Description","{0}"), Eval("ModelID","{0}"))%>
        </ItemTemplate>
    </asp:DataList>
    <div style="width:100%;text-align:center;">
        <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="上一步" OnClick="Button2_Click" />
        <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="保存" OnClick="Button1_Click" />
        <input id="Button3" class="btn btn-primary" type="button" value="返回" onclick="window.location = 'CollectionManage.aspx'" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <style type="text/css">
        label{font-weight:normal;cursor:pointer;}
    </style>
    <script>
        var diag = new ZL_Dialog();
        diag.title = "字段采集规则";
        diag.maxbtn = false;
        diag.backdrop = true;
        diag.width = "width1100";
        function SaveConfig(cname,json) {
            document.getElementById("hd_s_" + cname).value = json;
            //document.getElementById("hd_e_" + cname).value = evalue;
            //document.getElementById("hd_p_" + cname).value = pvalue;
            console.log(document.getElementById("hd_s_" + cname).value);
        }
        function showpage(itemid, name, alias) {
            $(event.srcElement).closest("tr").find(":radio[value=3]")[0].checked = true;
            diag.url = "CollectionInfoPage.aspx?ItemID=" + itemid + "&Name=" + name + "&Alias=" + alias;
            diag.ShowModal();
        }
        function Close()
        {
            diag.CloseModal();
        }
    </script>
</asp:Content>
