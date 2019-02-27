<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserModelField.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Page.UserModelField" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>申请字段</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Label runat="server" ID="Label1" Visible="false"></asp:Label>
    <table class="table table-striped table-bordered table-hover" id="FieldList">
        <tr class="gridtitle" align="center" style="height: 25px;">
            <td style="width: 10%; height: 20px;"><strong>字段名</strong></td>
            <td style="width: 10%;"><strong>字段别名</strong></td>
            <td style="width: 10%;"><strong>字段类型</strong></td>
            <td style="width: 10%;"><strong>字段级别</strong></td>
            <td style="width: 10%;"><strong>是否必填</strong></td>
            <td style="width: 20%;"><strong>排序</strong></td>
            <td style="width: 30%;"><strong>操作</strong></td>
        </tr>
        <asp:Repeater ID="RepSystemModel" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%#Eval("FieldName")%></td>
                    <td align="center"><%#Eval("FieldAlias")%></td>
                    <td align="center"><%# Eval("FieldType", "{0}")%></td>
                    <td align="center"><span style="color: #339900">系统</span></td>
                    <td align="center"><%# GetStyleTrue(Eval("IsNotNull", "{0}"))%></td>
                    <td></td>
                    <td align="center"></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Repeater ID="RepModelField" runat="server" OnItemCommand="Repeater1_ItemCommand">
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <%#Eval("IsShow", "{0}") == "False" ? "<font color=#999999>" : ""%><%#Eval("FieldName")%><%#Eval("IsShow","{0}")=="False"?"</font>":"" %>
                    </td>
                    <td align="center">
                        <%#Eval("FieldAlias")%>
                    </td>
                    <td align="center">
                        <%# GetFieldType(Eval("FieldType", "{0}"))%>
                    </td>
                    <td align="center">自定义</td>
                    <td align="center">
                        <%# GetStyleTrue(Eval("IsNotNull", "{0}"))%>
                    </td>
                    <td align="center">
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="UpMove" CommandArgument='<%# Eval("FieldID") %>'>上移</asp:LinkButton>
                        |
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="DownMove" CommandArgument='<%# Eval("FieldID") %>'>下移</asp:LinkButton>
                    </td>
                    <td align="center"><a href="EditField.aspx?ModelID=<%#Eval("ModelID")%>&FieldID=<%# Eval("FieldID") %>"><i class="fa fa-pencil" title="修改"></i></a> 
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Delete" CommandArgument='<%# Eval("FieldID") %>' OnClientClick="return confirm('确定删除此字段吗?\r\n\r\n删除字段后需要重新生成静态Html代码')" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div class="clearbox"></div>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td style="width: 30%">
                <asp:TextBox ID="TxtTemplate" MaxLength="255" runat="server" Columns="50" CssClass="form-control"></asp:TextBox>
                <input type="button" value="选择模板" class="btn btn-primary" onclick="WinOpenDialog('../Template/TemplateList.aspx?OpenerText=' + escape('TxtTemplate') + '&FilesDir=', 650, 480)" />
                <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="设定模板" OnClick="SetTemplate" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Common.js" type="text/javascript"></script>
    <script type="text/javascript">
        $().ready(function () {
            var FiledListTable = document.getElementById("FieldList");
            for (var i = 0; i < FiledListTable.rows.length; i++) {
                var FiledLevel = FiledListTable.rows[i].cells[3];
                if (FiledLevel.innerText.Trim() == "系统") {
                    FiledListTable.rows[i].style.display = "none";
                }
                else {
                    FiledListTable.rows[i].style.display = "";
                }
            }
        })
        function ShowList() {
            var FiledListTable = document.getElementById("FieldList");
            for (var i = 0; i < FiledListTable.rows.length; i++) {
                var FiledLevel = FiledListTable.rows[i].cells[3];
                if (FiledLevel.innerHTML.indexOf("系统") != -1 && FiledListTable.rows[i].style.display == "none") {
                    FiledListTable.rows[i].style.display = "";
                    document.getElementById("ShowLink").innerText = "[隐藏系统字段]";
                }
                else if (FiledLevel.innerHTML.indexOf("系统") != -1 && FiledListTable.rows[i].style.display == "") {
                    FiledListTable.rows[i].style.display = "none";
                    document.getElementById("ShowLink").innerText = "[显示所有字段]";
                }
            }
        }
    </script>
    <asp:Literal ID="LModelName" runat="server" Visible="false"></asp:Literal>

</asp:Content>
