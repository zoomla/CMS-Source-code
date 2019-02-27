<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectTeacherName.aspx.cs" Inherits="manage_Question_SelectTeacherName" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>选择课程</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
    <tr align="center" class="title">    
        <td width="10%" class="title">教师名称</td>
        <td width="10%" class="title">职位</td>
        <td width="10%" class="title">授课</td>
        <td width="15%" class="title">创建时间</td>
        <td width="40%" class="title">老师信息</td>
        <td width="15%" class="title">操作</td>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
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
                     <a href="SelectTeacherName.aspx?menu=select&id=<%#Eval("ID") %>&name=<%#Eval("TName") %>">选择</a>                   
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
    <script type="text/javascript" src="/JS/Dialog.js"></script>
    <script type="text/javascript">
        function onstr() {
            window.opener = null;
            parent.Dialog.close();
        }

        function setvalue(objname, valuetxt) {
            var mainright = window.top.main_right;
            parent.document.getElementById(objname).value = valuetxt;
        }
    </script>
</asp:Content>