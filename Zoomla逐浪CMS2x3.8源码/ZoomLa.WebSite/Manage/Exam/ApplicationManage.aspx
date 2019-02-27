<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationManage.aspx.cs" Inherits="manage_Question_ApplicationManage" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>教师管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr align="center" class="title">
            <td width="5%">
                
            </td>
            <td width="10%" class="title">
                ID
            </td>
            <td width="10%" class="title">
                招生人员姓名
            </td>
            <td width="10%" class="title">
                加入时间
            </td>
            <td width="10%" class="title">
                学员人数
            </td>
            <td width="15%" class="title">
                联系电话
            </td>
            <td width="15%" class="title">
                操作
            </td>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td height="22" align="center">
                        <input name="Item" type="checkbox" value='<%#Eval("id") %>' />
                    </td>
                    <td height="22" align="center">
                        <%#Eval("id", "{0}")%>
                    </td>
                    <td height="22" align="center">
                        <%#GetUserName(Eval("UserID","{0}"))%>
                    </td>
                    <td height="22" align="center">
                        <%#Eval("RegTime")%>
                    </td>
                    <td height="22" align="center">
                        <%#getusercount(Eval("id", "{0}"))%>
                    </td>
                    <td height="22" align="center">
                        <%#Eval("OfficePhone")%>
                    </td>
                    <td height="22" align="center">
                        <a href="?menu=delete&id=<%#Eval("id")%>" onclick="return confirm('确实要删除此教师?');" class="option_style"><i class="fa fa-trash-o" title="删除"></i></a>
                    <a href="StudioInfoListByTech.aspx?id=<%#Eval("id")%>" class="option_style"><i class="fa fa-magic" title="管理"></i>学员资料管理</a> 
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr class="tdbg">
            <td style="text-align:center"><asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" /></td>
            <td height="22" colspan="6" align="center" class="tdbg">
                共<asp:Label ID="Allnum" runat="server" Text=""></asp:Label>条数据
                <asp:Label ID="Toppage" runat="server" Text="" />
                <asp:Label ID="Nextpage" runat="server" Text="" />
                <asp:Label ID="Downpage" runat="server" Text="" />
                <asp:Label ID="Endpage" runat="server" Text="" />页次：
                <asp:Label ID="Nowpage" runat="server" Text="" />/
                <asp:Label ID="PageSize" runat="server" Text="" />页
                <asp:TextBox ID="txtPage" runat="server" AutoPostBack="true" OnTextChanged="txtPage_TextChanged"></asp:TextBox>
                条数据/页 转到第
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
                页<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtPage"
                    ErrorMessage="只能输入数字" Type="Integer" MaximumValue="100000" MinimumValue="0"></asp:RangeValidator>
            </td>
        </tr>
    </table>
    <div>
        <asp:Button ID="Button3" class="btn btn-primary" Style="width: 110px;" runat="server" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');"
            Text="批量删除" OnClick="Button3_Click" /></div>
    <div class="modal" id="TechUser_div">
        <div class="modal-dialog" style="width: 600px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <span class="modal-title"><strong>导入招生资料</strong></span>
                </div>
                <div class="modal-body">
                      <iframe id="TechUser_ifr" style="width:100%;height:400px;border:none;" src=""></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Drag.js"></script>
    <script type="text/javascript" src="/js/Dialog.js"></script>
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

        function openurls(url) {
            Dialog.open({ URL: url });
        }
        function open_window() {
            $("#TechUser_ifr").attr("src", "InputTechUser.aspx");
        }
    </script>
</asp:Content>