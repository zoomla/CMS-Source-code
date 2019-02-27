<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuestTieShow.aspx.cs" Inherits="ZoomLaCMS.Manage.Guest.GuestTieShow" ValidateRequest="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content ContentPlaceHolderID="head" Runat="Server">
    <title>留言内容</title>
    <script type="text/javascript" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content  ContentPlaceHolderID="Content" Runat="Server">
    <table class="table table-striped table-bordered table-hover">
        <tr align="center">
            <td colspan="2" class="spacingtitle"><strong>查看留言内容</strong></td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>ID</strong></td>
            <td class="tdbg" style="width: 85%;">
                <asp:Label ID="ID_L" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>发贴人</strong></td>
            <td class="tdbg" style="width: 85%;">
                <asp:Literal ID="CUser_LB" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>贴子标题</strong></td>
            <td class="tdbg" style="width: 85%;">
                <asp:Label ID="Title_L" runat="server" data-show="onlyread"></asp:Label>
                <asp:TextBox ID="txtTitle" CssClass="form-control" runat="server" data-show="edit" Style="display:none;"></asp:TextBox>
                <asp:HiddenField ID="txtStyle_Hid" runat="server" />
                <input type="button" class="btn btn-primary btn-xs" style="display:none;" data-show="edit" value="标题属性" onclick="open_style()" />
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center; vertical-align: middle;"><strong>贴子内容</strong></td>
            <td class="tdbg" style="width: 85%;">
                <asp:Label runat="server" ID="MsgContent_L" data-show="onlyread"></asp:Label>
                <div style="display: none;" data-show="edit">
                    <asp:TextBox ID="MsgContent_T" runat="server" TextMode="MultiLine" Style="width: 90%; height: 200px;" />
                </div>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>IP地址</strong></td>
            <td class="tdbg" style="width: 85%;">
                <asp:Label ID="IP_L" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>创建时间</strong></td>
            <td class="tdbg" style="width: 85%;">
                <asp:Label ID="CDate_L" runat="server" data-show="onlyread"></asp:Label>
                <asp:TextBox ID="CDate_T" runat="server" data-show="edit" Style="display:none" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm'});"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-center"><strong>所得勋章</strong></td>
            <td>
                <asp:Literal ID="Medals_Li" runat="server" EnableViewState="false"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="text-align: center;"><strong>操作</strong></td>
            <td class="tdbg">
                <input type="button" id="edit_btn" onclick="EditFunc('edit');" data-show="onlyread" value="修改信息" class="btn btn-primary" />
                <asp:Button ID="Save_Btn" Style="display:none" CssClass="btn btn-primary" data-show="edit" OnClientClick="SaveTxtStyle()" runat="server" OnClick="Save_Btn_Click" Text="保存信息" />
                <input type="button" id="onlyread_btn" onclick="EditFunc('onlyread');" value="取消修改" class="btn btn-primary" style="display:none;" data-show="edit" />
                <asp:Button ID="AddMedal_Btn" runat="server" CssClass="btn btn-primary" Text="颁发勋章" OnClick="AddMedal_Btn_Click" />
            </td>
        </tr>
    </table>
    <div id="Pager1" runat="server" style="display: none;"></div>
    <asp:HiddenField ID="HdnGID" runat="server" />
    <div class="clearfix"></div>
    <ZL:ExGridView ID="Egv1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Lnk_Click" AllowPaging="True" AllowSorting="True" OnRowEditing="Egv_RowEditing" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="该帖没有回复">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="idchk" title="" value='<%#Eval("ID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="回贴内容">
                <ItemTemplate>
                    <%#GetMsgContent() %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
                <HeaderStyle Width="55%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="回帖时间">
                <ItemTemplate>
                    <%#Eval("CDate") %>
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                <HeaderStyle Width="20%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="回帖人">
                <ItemTemplate>
                    <a href="../User/UserInfo.aspx?id=<%# Eval("CUser")%>" title="点击查看用户详情">
                        <%# GetUserName(Eval("CUser","{0}")) %></a>
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                <HeaderStyle Width="10%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Del" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('确实要删除吗？');">删除</asp:LinkButton>|
                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="QList" CommandArgument='<%# Eval("ID") %>'>帖子内容</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                <HeaderStyle Width="10%" />
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center"  />
		<RowStyle HorizontalAlign="Center" />
    </ZL:ExGridView>
    <table class="TableWrap" border="0" cellpadding="0" cellspacing="0" width="100%" id="sleall">
        <tr>
            <td style="height: 21px">
                <asp:Button ID="btndelete" runat="server" CssClass="btn btn-primary" OnClientClick="return confirm('确定要删除选中的项目吗？')" Text="批量删除" OnClick="btndelete_Click" />
                <asp:HiddenField ID="HdnCateID" runat="server" />
            </td>
        </tr>
    </table>
    <div style="text-align: center; margin-top: 5px;">
        <!--<asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" OnClick="Button2_Click" Text="继续回复" />-->
        &nbsp;<asp:Button ID="Button1" runat="server" Text="返回" CssClass="btn btn-primary" OnClick="Button1_Click" />
    </div>
        <div class="modal" id="userinfo_div">
        <div class="modal-dialog" style="width: 1024px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <span class="modal-title"><strong>用户信息</strong></span>
                </div>
                <div class="modal-body">
                      <iframe id="user_ifr" style="width:100%;height:600px;border:none;" src=""></iframe>
                </div>
            </div>
        </div>
    </div>
    <%=Call.GetUEditor("MsgContent_T", 4) %>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
   
    <script>
        $().ready(function () {
            
        });
        function EditFunc(flag)
        {
            $("[data-show=onlyread]").toggle();
            $("[data-show=edit]").toggle();
        }
        var stylediag = new ZL_Dialog();
        function open_style() {
            
            stylediag.title = "设置标题字体<span style='font-weight:normal'>[ESC键退出当前操作]</span>";
            stylediag.url = "/Common/SelectStyle.htm";
            stylediag.ShowModal();
        }
        function SaveTxtStyle() {
            $("#txtStyle_Hid").val($("#txtTitle").attr("style"));
        }
        var userdiag = new ZL_Dialog();
        function ShowUserInfo(id) {
            userdiag.title = "用户信息";
            userdiag.url = "../User/UserInfo.aspx?id=" + id;
            userdiag.ShowModal();
        }
    </script>
</asp:Content>