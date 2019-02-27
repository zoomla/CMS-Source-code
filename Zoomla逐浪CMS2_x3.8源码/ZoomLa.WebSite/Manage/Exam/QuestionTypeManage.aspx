<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuestionTypeManage.aspx.cs" Inherits="manage_Question_QuestionTypeManage" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>题型管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table  class="table table-striped table-bordered table-hover">
    <tr align="center" class="title">
        <td width="5%">
            <asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" />
        </td>
        <td width="15%">名称</td>
        <td width="10%">类型</td>
        <td width="40%">说明</td>
        <td width="20%">创建时间</td>
        <td width="15%" class="title">操作</td>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td height="22" align="center">
                    <input name="Item" type="checkbox" value='<%#Eval("t_id") %>' />
                </td>
                <td height="22" align="center">
                    <%#Eval("t_name")%>
                </td>
                <td height="22" align="center">
                    <%#GetType(Eval("t_type","{0}"))%>
                </td>
                <td height="22" align="center">
                    <%#Eval("t_remark")%>
                </td>
                <td height="22" align="center">
                    <%#Eval("t_createtime")%>
                </td>
                <td height="22" align="center">
                    <a href="AddQuestionType.aspx?id=<%#Eval("t_id")%>" class="option_style"><i class="fa fa-pencil" title="修改"></i>修改</a><%-- <a href="?menu=delete&id=<%#Eval("t_id")%>" OnClick="return confirm('确实要删除此题型吗？');">删除</a>--%>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    <tr class="tdbg">
        <td height="22" colspan="9" align="center" class="tdbg">
          共<asp:Label ID="Allnum" runat="server" Text=""></asp:Label>条数据
                        <asp:Label ID="Toppage" runat="server" Text="" />
                        <asp:Label ID="Nextpage" runat="server" Text="" />
                        <asp:Label ID="Downpage" runat="server" Text="" />
                        <asp:Label ID="Endpage" runat="server" Text="" />页次：
                        <asp:Label ID="Nowpage" runat="server" Text="" />/
                        <asp:Label ID="PageSize" runat="server" Text="" />页
                        <asp:TextBox ID="txtPage" runat="server" AutoPostBack="true" OnTextChanged="txtPage_TextChanged"></asp:TextBox>
                        条数据/页 转到第
                        <asp:DropDownList ID="DropDownList1" runat="server" 
                AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                        页<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtPage"
                            ErrorMessage="只能输入数字" Type="Integer" MaximumValue="100000" MinimumValue="0"></asp:RangeValidator>
        </td>
    </tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Common.js"></script>
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
</script> 
</asp:Content>
