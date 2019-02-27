<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UAction.aspx.cs" Inherits="Manage_User_Addon_UAction" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>用户行为记录</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="top_opbar">
        <div class="input-group" style="width: 310px; margin-left: 5px; float: left;">
            <asp:DropDownList ID="SearchType_Drop" runat="server" CssClass="form-control" Style="width: 120px; border-right: none;">
                <asp:ListItem Value="1">用户名</asp:ListItem>
                <asp:ListItem Value="2">用户标识</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="Search_T" placeholder="在此搜索" runat="server" CssClass="form-control" Style="width: 150px;"></asp:TextBox>
            <span class="input-group-btn">
                <asp:LinkButton ID="Search_Btn" runat="server" CssClass="btn btn-default" Text="搜索" OnClick="Search_Btn_Click"><span class="fa fa-search"></span></asp:LinkButton>
            </span>
        </div>
        <div class="clearfix"></div>
    </div>
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false"
        OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" AllowPaging="True" AllowSorting="True" 
        CssClass="table table-striped table-bordered table-hover margin_t5" EmptyDataText="无相关数据" EnableTheming="False" EnableModelValidation="True">
        <Columns>
            <asp:TemplateField>
                <ItemStyle CssClass="td_s" />
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="uid" HeaderText="用户ID"/>
            <asp:TemplateField HeaderText="用户标识">
                <ItemTemplate>
                    <a href="UAction.aspx?searchkey=<%#Eval("idflag") %>&type=2"><%#Eval("idflag") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="用户名">
                <ItemTemplate>
                    <a href="UAction.aspx?searchkey=<%#Eval("uname") %>&type=1"><%#Eval("uname") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="页面">
                <ItemTemplate><a href="<%#Eval("PageUrl") %>" title="点击浏览" target="_blank">(<%#Eval("Title") %>)<%#Eval("PageUrl") %></a></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="action" HeaderText="用户动作" />
            <asp:BoundField DataField="ip" HeaderText="IP地址" />
            <asp:BoundField DataField="CDate" HeaderText="操作日期" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="javascript:;" onclick="ShowUserWord('<%#Eval("idflag") %>')">行为路线</a>
                    <asp:LinkButton runat="server" CommandName='event' CommandArgument='<%#Eval("idflag") %>' CssClass="option_style"><i class="fa fa-comments"></i>发起聊天</asp:LinkButton>
                    <%--<a href="/Common/Chat/Chat.aspx?Login=admin" target="_blank">发起聊天</a>--%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:Button runat="server" ID="BatDel_Btn" CssClass="btn btn-primary" Text="批量删除" OnClick="BatDel_Btn_Click" OnClientClick="return confirm('确定要删除吗?');" />
    <div class="alert alert-info margin_t5">
             1,在需要追踪用户行为的页面引入(<%:"<script src='/JS/Plugs/ZL_UAction.js'></script>" %>)<br />
             2,默认为每浏览1个页面,即提交一次行为给后台,可按需求修改,建议改为五次(刷新忽略,可修改)<br />
	         3,后台--用户--行为追踪可以查看该用户的路线图,点击发起聊天,前台用户处便会弹窗,可实时与其交谈
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">
    #chatdiag_div {position:fixed;left:30%;overflow:hidden;width:610px; height:550px;top:60px;border:none;}
</style>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
    function ShowUserWord(flag) {
        ShowComDiag("UActionImgWrok.aspx?idflag=" + flag, "用户行为图");
    }
    function GetTo(uid) {
        var diag = new ZL_Dialog();
        diag.ShowMask("请等待8秒左右,正在打开客户端聊天窗口");
        setTimeout(function () {
            var url = "/Common/Chat/Chat.aspx?Login=admin&userflag=" + uid;
            var hrefBtn = $('<a href="javascript:;"/>');
            hrefBtn.click(function () {
                var tlp = "<div id='chatdiag_div'><iframe src='@url' style='width:620px;height:700px; border:none;'></iframe></div>";
                $('body').append(tlp.replace(/@url/, url));
                //ShowComDiag(url, "客户对话");
            });
            hrefBtn.trigger("click");
            diag.CloseModal();
        }, 8000);
    }
    function ChatClose() {
        $("#chatdiag_div").remove();
    }
</script>
</asp:Content>
