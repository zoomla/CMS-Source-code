<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudioInfoListByTech.aspx.cs" Inherits="manage_Exam_StudioInfoListByTech" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>教师管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table  class="table table-striped table-bordered table-hover">
        <tr align="center" class="title">
            <td width="5%">
                <asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" />
            </td>
            <td width="10%" class="title">
                ID
            </td>
            <td width="10%" class="title">
                学员姓名
            </td>
            <td width="10%" class="title">
                预设用户名
            </td>
            <td width="10%" class="title">
                联系电话
            </td>
            <td width="15%" class="title">
                登记时间
            </td>
            <td width="15%" class="title">
                操作
            </td>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td height="22" align="center">
                        <input name="Item" type="checkbox" value='<%#Eval("ssid") %>' />
                    </td>
                    <td height="22" align="center">
                        <%#Eval("ssid", "{0}")%>
                    </td>
                    <td height="22" align="center">
                        <%#Eval("Studioname", "{0}")%>
                    </td>
                    <td height="22" align="center">
                        <%#Eval("PriorUserName")%>
                    </td>
                    <td height="22" align="center">
                       <%#Eval("Tel")%>
                    </td>
                    <td height="22" align="center">
                        <%#Eval("WriteTime")%>
                    </td>
                    <td height="22" align="center">
                    <a href="AddStudioInfo.aspx?stuid=<%#Eval("ssid")%>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a> 
                        <a href="?menu=delete&id=<%#Eval("ssid")%>"
                            onclick="return confirm('确实要删除此信息?');" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr class="tdbg">
            <td height="22" colspan="7" align="center" class="tdbg">
                共<asp:Label ID="Allnum" runat="server" Text=""></asp:Label>条数据
                <asp:Label ID="Toppage" runat="server" Text="" />
                <asp:Label ID="Nextpage" runat="server" Text="" />
                <asp:Label ID="Downpage" runat="server" Text="" />
                <asp:Label ID="Endpage" runat="server" Text="" />页次：
                <asp:Label ID="Nowpage" runat="server" Text="" />/
                <asp:Label ID="PageSize" runat="server" Text="" />页
                <asp:TextBox ID="txtPage" runat="server" AutoPostBack="true" OnTextChanged="txtPage_TextChanged" Text="10"></asp:TextBox>
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
            Text="批量删除" OnClick="Button3_Click" />
            <asp:Button ID="Button1" class="btn btn-primary" Style="width: 110px;" runat="server"
            Text="导出Excel" OnClick="Button1_Click" />
            </div>
    <div class="modal" id="InputUser_div">
        <div class="modal-dialog" style="width: 600px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <span class="modal-title"><strong>导入招生资料</strong></span>
                </div>
                <div class="modal-body">
                      <iframe id="InputUser_ifr" style="width:100%;height:400px;border:none;" src=""></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
     <script type="text/javascript" src="/js/Drag.js"></script>
    <script type="text/javascript" src="/js/Dialog.js"></script>
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

        function openurls(url) {
            Dialog.open({ URL: url });
        }
        function open_window() {
            $("#InputUser_ifr").attr("src", "InputUser.aspx?Tid=<%=Request.QueryString["id"] %>");
        }
    </script>
</asp:Content>
    
