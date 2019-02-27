<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="ViewPub.aspx.cs" Inherits="User_Pages_ViewPub" ClientIDMode="Static" ValidateRequest="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>会员组模型</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="Default.aspx">企业黄页</a></li>
        <li class="active">会员组模型</li>
    </ol>
    <div class="us_seta" id="manageinfo" runat="server">
        <asp:HiddenField ID="HiddenPubRole" runat="server" />
        <asp:HiddenField ID="HdnModelID" runat="server" />
        <asp:HiddenField ID="HiddenTable" runat="server" />
        <asp:HiddenField ID="HdnModelName" runat="server" />
        <asp:HiddenField ID="HiddenGuang" runat="server" />
        <asp:HiddenField ID="Hiddenpagenum" runat="server" />
        <asp:HiddenField ID="HiddenPubid" runat="server" />
        <asp:HiddenField ID="HiddenUserID" runat="server" />
        <table class="table table-striped table-bordered table-hover">
            <tr align="center">
                <td width="5%">
                    <asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" /></td>
                <td width="5%">ID</td>
                <td width="27%">
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>标题</td>
                <td width="10%">参与人数</td>
                <td width="18%">添加时间</td>
                <td width="45%">操作</td>
            </tr>
            <asp:Repeater ID="gvCard" runat="server">
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <input name="Item" type="checkbox" value='<%# Eval("ID")%>' /></td>
                        <td align="center"><%# Eval("ID")%></td>
                        <td align="left">
                            <a href="javascript:void();" onclick="javascript: window.open('ViewSmallPub.aspx?Pubid=<%#this.HiddenPubid.Value%>&ID=<%#Eval("ID") %>', 'newwindow', 'height=500, width=800, top=200, left=150, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no'); ">
                                <%#returnlen(Eval("PubTitle"))%>
                            </a>
                        </td>
                        <td align="center"><%#Eval("Pubnum")%>
                        <td align="center"><%#Eval("PubAddTime")%>
                        <td align="center">
                            <a href="javascript:void();" onclick="javascript: window.open('ViewSmallPub.aspx?Pubid=<%#this.HiddenPubid.Value%>&ID=<%#Eval("ID") %>', 'newwindow', 'height=500, width=800, top=200, left=150, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no'); ">相关信息与回复管理</a>&nbsp;<a href="javascript:void();" onclick="javascript: window.open('AddPub.aspx?Pubid=<%#this.HiddenPubid.Value%>&Parentid=<%#Eval("ID") %>', 'newwindow', 'height=500, width=800, top=200, left=150, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no');">回复</a>&nbsp;<a href="PubView.aspx?Pubid=<%#this.HiddenPubid.Value%>&ID=<%#Eval("ID") %>">查看</a>&nbsp;<%#showuse(DataBinder.Eval(Container.DataItem, "Pubstart").ToString(), Eval("ID").ToString())%>&nbsp;<a href="EditPub.aspx?Pubid=<%#this.HiddenPubid.Value%>&ID=<%#Eval("ID") %>">修改</a>&nbsp;<a href=" Delpub.aspx?Pubid=<%#this.HiddenPubid.Value%>&ID=<%# Eval("ID")%>" onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');">删除</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="7" align="center">
                    共
                    <asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
                    个信息
                    <asp:Label ID="Toppage" runat="server" Text="" />
                    <asp:Label ID="Nextpage" runat="server" Text="" />
                    <asp:Label ID="Downpage" runat="server" Text="" />
                    <asp:Label ID="Endpage" runat="server" Text="" />
                    页次：
                    <asp:Label ID="Nowpage" runat="server" Text="" />/<asp:Label ID="PageSize" runat="server" Text="" />
                    页
                    <asp:Label ID="pagess" runat="server" Text="" />个信息/页 转到第<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"></asp:DropDownList>
                    页
                </td>
            </tr>
        </table>
        <asp:Button ID="Button3" runat="server" CssClass="btn btn-primary" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" Text="批量删除" OnClick="Button3_Click" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script language="javascript">
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
    <script>
        function OpenLink(lefturl, righturl) {
            if (lefturl != "") {
                parent.frames["left"].location = lefturl;
            }
            parent.frames["main_right"].location = righturl;
        }
    </script>
    <script type="text/javascript">
        //根据传入的checkbox的选中状态设置所有checkbox的选中状态
        function selectAll(obj) {
            var allInput = document.getElementsByTagName("input");
            //alert(allInput.length);
            var loopTime = allInput.length;
            for (i = 0; i < loopTime; i++) {
                //alert(allInput[i].type);
                if (allInput[i].type == "checkbox") {
                    allInput[i].checked = obj.checked;
                }
            }
        }
        //判断是否选中记录，用户确认删除
        function judgeSelect() {
            var result = false;
            var allInput = document.getElementsByTagName("input");
            var loopTime = allInput.length;
            for (i = 0; i < loopTime; i++) {
                if (allInput[i].checked) {
                    result = true;
                    break;
                }
            }
            if (!result) {
                alert("请先选则要删除的记录！");
                return result;
            }
            result = confirm("你确认要删除选定的记录吗？");
            return result;
        }
        //判断是否选中记录，判断是否取消生成
        function IsCreateSelect() {
            var result = false;
            var allInput = document.getElementsByTagName("input");
            var loopTime = allInput.length;
            for (i = 0; i < loopTime; i++) {
                if (allInput[i].checked) {
                    result = true;
                    break;
                }
            }
            if (!result) {
                alert("请先选则要还原的记录！");
                return result;
            }
            result = confirm("你确认要还原选定的记录吗？");
            return result;
        }
    </script>
</asp:Content>
