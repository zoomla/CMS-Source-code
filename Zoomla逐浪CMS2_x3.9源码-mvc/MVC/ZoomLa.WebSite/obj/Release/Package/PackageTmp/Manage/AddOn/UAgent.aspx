<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UAgent.aspx.cs" Inherits="ZoomLaCMS.Manage.AddOn.UAgent" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>设备管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table   class="table table-striped table-bordered table-hover"> 
<tr>
<th class="td_s"></th>
<th style="width:5%;">ID</th>
<th style="width:20%;">设备名称</th>
<th style="width:20%;">关键字</th>
<th>状态</th> 
<th style="width:20%;">目标地址</th>
</tr>
<ZL:ExRepeater ID="RPT" PageSize="20" runat="server" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='8'><div class='text-center'>" PageEnd="</div></td></tr>" OnItemCommand="Repeater1_ItemCommand">
<ItemTemplate>
<tr class="tdbg" onDblClick="UpadateSite('<%#Eval("ID")%>')">
    <td><input type="checkbox" <%#retEnab(Convert.ToInt32(Eval("ID"))) %> name="Btchk" id="Btchk" value='<%#Eval("ID")%>'/></td>
    <td><%#Eval("ID") %></td>
    <td><%#Eval("Headers") %></td>
    <td title="双击修改" ><%#Eval("UserAgent") %> </td>
    <td><asp:ImageButton ID="Update_lbk" runat="server" CommandName="Update" CommandArgument='<%#Eval("ID") %>' ImageUrl='<%# "~/Images/" + GetState(Eval("Status", "{0}")) +".gif" %>' OnClick="ImageButton_Click"/></td>
    <td><a href="<%#Eval("Url")%>"><%#Eval("Url") %></a></td>
</tr>
</ItemTemplate>
<FooterTemplate></FooterTemplate>
</ZL:ExRepeater>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function IsSelectedId() {
            var flag = false;
            var s = document.form1.getElementsByTagName("INPUT")
            for (var i = 0; i < s.length; i++) {
                if (s[i].type == "checkbox") {
                    if (s[i].checked) {
                        flag = true;
                        break;
                    }
                }
            }
            return flag;
        }
        function CheckAll(spanChk)//CheckBox全选
        {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (i > 9) {
                        if (elm[i].checked != xState)
                            elm[i].click();
                    }
                }
        }
        function UpadateSite(id) {
            window.location.href = "AddUAgent.aspx?ID=" + id;
        }
    </script>
</asp:Content>
