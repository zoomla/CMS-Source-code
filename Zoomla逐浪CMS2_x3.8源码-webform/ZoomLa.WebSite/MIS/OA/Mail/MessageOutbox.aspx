<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageOutbox.aspx.cs" Inherits="User_Message_MessageOutbox" EnableViewStateMac="false" MasterPageFile="~/User/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>发件箱</title>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
<script type="text/javascript">
    $().ready(function () {
        $("#EGV tr:last >td>:text").css("line-height", "normal");
        $("#EGV tr:first >th").css("text-align", "center");
        $("#<%=EGV.ClientID%>  tr>th:eq(0)").html("<input type=checkbox id='chkAll'/>");//EGV顶部
        $("#chkAll").click(function () { selectAllByName(this, "idChk"); });
    });
    $().ready(function () {
        $("tr:gt(1)").addClass("tdbg");
        $("tr:gt(1)").mouseover(function () { $(this).removeClass("tdbg").addClass("tdbgmouseover") }).mouseout(function () { $(this).removeClass("tdbgmouseover").addClass("tdbg") });
        $("tr:gt(1)").dblclick(function () { v = $(this).find("[name='idChk']").val(); location = "MessageRead.aspx?read=1&id=" + v; });
        $("tr:last").unbind("mouseover").unbind("dblclick");
    });
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="mainDiv"> 
<div class="us_seta" style="margin-top: 5px;">
<div id="site_main">
<div class="input-group text_md">
<asp:TextBox runat="server" ID="searchText" placeholder="请输入需要查询的信息" CssClass="form-control" />
<span class="input-group-btn">
<asp:Button runat="server" CssClass="btn btn-primary" ID="searchBtn" Text="搜索" OnClick="searchBtn_Click" />
</span>
</div> 
            <div style="margin-top:5px;">
                <ZL:ExGridView runat="server" ID="EGV" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False" GridLines="None" CellPadding="2" CellSpacing="1" Width="98%" EmptyDataText="当前没有信息!!" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="选择" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <input type="checkbox" name="idChk" value='<%#Eval("MsgID") %>'/>
                            </ItemTemplate>
                            <HeaderStyle Width="5%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="主题">
                            <HeaderStyle Width="40%" />
                            <ItemTemplate>
                                <%# Eval("Title", "{0}")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="收件人">
                            <ItemTemplate>
                                <%# GetUserName(Eval("Incept","{0}")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PostDate" HeaderText="发送日期" HeaderStyle-Width="20%"
                            ItemStyle-HorizontalAlign="center">
                            <HeaderStyle Width="20%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="操作" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDel" runat="server" CommandName="DeleteMsg" OnClientClick="if(!this.disabled) return confirm('确实要删除此信息到垃圾箱吗？');"
                                    CommandArgument='<%# Eval("MsgID")%>' Text="删除"></asp:LinkButton>
                                <asp:LinkButton ID="lbRead" runat="server" CommandName="ReadMsg" CommandArgument='<%# Eval("MsgID")%>'>阅读信息</asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="20%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <RowStyle Height="24px" HorizontalAlign="Center" />
                </ZL:ExGridView>
            </div>
        </div>
        <asp:Button ID="Button2" runat="server" Font-Size="9pt" Text="批量删除" OnClick="Button2_Click" OnClientClick="return confirm('要将邮件批量置入回收站吗!!!');" class="btn btn-primary" />
    </div>
</div>
</asp:Content>
