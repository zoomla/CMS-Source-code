<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudioManage.aspx.cs" Inherits="manage_Question_StudioManage" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>学员管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr align="center" class="title">
            <td width="2%">
                <asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" />
            </td>
            <td width="10%">
                学员ID
            </td>
            <td width="10%">
                学员姓名
            </td>
            <td width="10%">
                所在组别
            </td>
            <td width="10%">
                是否合格
            </td>
            <td width="10%">
                到期时间
            </td>
            <td width="6%" class="title">
                操作
            </td>
        </tr>
        <ZL:ExRepeater ID="RPT" runat="server" PagePre="<tr><td colspan='7' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>">
            <ItemTemplate>
                <tr id="<%#Eval("Stuid") %>" ondblclick="ShowTabs(this.id)">
                    <td height="22" align="center">
                        <input name="Item" type="checkbox" value='<%#Eval("Stuid") %>' />
                    </td>
                    <td height="22" align="center">
                        <%#Eval("Stuid")%>
                    </td>
                    <td height="22" align="center">
                        <%#Eval("Stuname")%>
                    </td>
                    <td height="22" align="center">
                        <%#Eval("Stugroup")%>
                    </td>
                     <td height="22" align="center">
                        <%#Eval("Qualified")%>
                    </td>
                     <td height="22" align="center">
                        <%#Eval("Exptime")%>
                    </td>
                    <td height="22" align="center">
                        <a href="AddStudio.aspx?id=<%#Eval("Stuid")%>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a> 
                        <a href="?menu=delete&id=<%#Eval("Stuid")%>" OnClick="return confirm('确实要删除此学员?');" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>
    </table>
    <div>
        <asp:Button ID="Button3" class="btn btn-primary" runat="server" OnClientClick="return confirm('不可恢复性删除数据,确定将该数据删除?');"
            Text="批量删除" onclick="Button3_Click" /></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
   <script type="text/javascript">
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