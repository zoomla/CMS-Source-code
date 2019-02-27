<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExTeacherManage.aspx.cs" Inherits="manage_Question_ExTeacherManage" ValidateRequest="false" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>教师管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover" id="EGV">
    <tr align="center" class="title">
        <td width="5%">
            
        </td>
        <td width="10%" class="title">教师分类</td>
        <td width="10%" class="title">教师名称</td>
        <td width="10%" class="title">职位</td>
        <td width="10%" class="title">授课</td>
        <td width="15%" class="title">创建时间</td>
        <td width="30%" class="title">老师信息</td>
        <td width="15%" class="title">操作</td>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td height="22" align="center">
                    <input name="Item" type="checkbox" value='<%#Eval("ID") %>' />
                </td>
                <td height="22" align="center">
                    <%#GetTeachClass(Eval("TClsss", "{0}"))%>
                </td>
                <td height="22" align="center">
                    <%#Eval("TName")%>
                </td>
                <td height="22" align="center">
                    <%#Eval("Post")%>
                </td>
                <td height="22" align="center">
                    <%#Eval("Teach")%>
                </td>
                <td height="22" align="center">
                  <%#Eval("CreatTime")%>
                </td>
                <td height="22" align="center">
                    <%#GetRemark(Eval("Remark","{0}"))%>
                </td>
                <td height="22" align="center">
                    <a href="AddExTeacher.aspx?id=<%#Eval("ID")%>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a> 
                    <a href="?menu=delete&id=<%#Eval("ID")%>" onclick="return confirm('确实要删除此教师?');" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    <tr class="tdbg">
        <td style="text-align:center"><asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" /></td>
        <td height="22" colspan="8" align="center" class="tdbg">
          共<asp:Label ID="Allnum" runat="server" Text=""></asp:Label>条数据
                        <asp:Label ID="Toppage" runat="server" Text="" />
                        <asp:Label ID="Nextpage" runat="server" Text="" />
                        <asp:Label ID="Downpage" runat="server" Text="" />
                        <asp:Label ID="Endpage" runat="server" Text="" />页次：
                        <asp:Label ID="Nowpage" runat="server" Text="" />/
                        <asp:Label ID="PageSize" runat="server" Text="" />页
                        <asp:TextBox ID="txtPage" runat="server" AutoPostBack="true" class="l_input" OnTextChanged="txtPage_TextChanged"></asp:TextBox>
                        条数据/页 转到第
                        <asp:DropDownList ID="DropDownList1" runat="server" 
                AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                        页<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtPage"
                            ErrorMessage="只能输入数字" Type="Integer" MaximumValue="100000" MinimumValue="0"></asp:RangeValidator>
        </td>
    </tr>
</table>
<div>
    <asp:Button ID="Button3" class="btn btn-primary" runat="server" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" Text="批量删除" onclick="Button3_Click" /></div>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Common.js"></script>
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
        HideColumn("5,6");
    </script>
</asp:Content>