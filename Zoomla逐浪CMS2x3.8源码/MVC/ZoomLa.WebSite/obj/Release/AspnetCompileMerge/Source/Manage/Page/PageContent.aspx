<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageContent.aspx.cs" Inherits="ZoomLaCMS.Manage.Page.PageContent" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>内容管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
    	<li><a href='<%=CustomerPageAction.customPath2 %>I/Main.aspx'>工作台</a></li><li><a href='PageManage.aspx'>企业黄页</a></li>
        <li><a href="<%=Request.RawUrl %>">黄页内容管理</a>  [<a href='PageRecyle.aspx'>黄页回收站</a>]</li>
        <li>
            <span>
                <%=lang.LF("排序") %>：
                <asp:DropDownList ID="txtbyfilde" CssClass="form-control" Width="150" runat="server" OnSelectedIndexChanged="txtbyfilde_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:DropDownList ID="txtbyOrder" runat="server" CssClass="form-control" Width="150" OnSelectedIndexChanged="txtbyOrder_SelectedIndexChanged">
                </asp:DropDownList>
            </span>
            
            <span>
                <asp:DropDownList ID="DropDownList1" CssClass="form-control" Width="150" runat="server">
                    <asp:ListItem Value="0" Selected="True">标题</asp:ListItem>
                    <asp:ListItem Value="1">ID</asp:ListItem>
                    <asp:ListItem Value="2">录入者检索</asp:ListItem>
                </asp:DropDownList>
            </span>
            <span style="display:none;"><%=lang.LF("按发布者地区筛选")%>： 
                <select runat="server" id="selprovince" class="form-control" style="width:150px;" name="selprovince" onchange="javascript:area()"></select>
                <asp:DropDownList ID="selcity" runat="server" CssClass="form-control" Width="150" AutoPostBack="true" OnSelectedIndexChanged="selcity_SelectedIndexChanged">
                </asp:DropDownList>
            </span>
            <div class="input-group pull-right" style="margin-left:5px;">
                <asp:TextBox runat="server" ID="TextBox1" class="form-control" placeholder="请输入需要搜索的内容" />
                <span class="input-group-btn" style="width:auto;">
                    <asp:LinkButton runat="server" CssClass="btn btn-default" ID="Button4" OnClick="Button4_Click"><span class="fa fa-search"></span></asp:LinkButton>
                </span>
            </div>
        </li>
    </ol>
    <ul class="nav nav-tabs" id="navul">
            <li class="active"><a href="PageContent.aspx?ModelID=<%=Request.QueryString["ModelID"]+"&li=0" %>">内容列表</a></li>
            <li><a href="PageContent.aspx?ModelID=<%=Request.QueryString["ModelID"] %>&flag=Audit&li=1">已审核内容</a></li>
            <li><a href="PageContent.aspx?ModelID=<%=Request.QueryString["ModelID"] %>&flag=UnAudit&li=2">未审核内容</a></li>
            <li><a href="PageContent.aspx?ModelID=<%=Request.QueryString["ModelID"] %>&flag=Elite&li=3">推荐内容</a></li>
    </ul>
    <div>
        <table class="table table-striped table-bordered table-hover content_list">
            <tr>
                <td></td><td>ID</td><td>标题</td><td>所属黄页</td><td>发布者</td><td>创建时间</td><td>状态</td><td>操作</td>
            </tr>
            <ZL:ExRepeater ID="RPT" runat="server" PageSize="10" PagePre="<tr><td colspan='9'><label class='allchk_l'><input type='checkbox' id='chkAll'/>全选</label><div class='text-center'>" PageEnd="</div></td></tr>"
                 OnItemCommand="Lnk_Click">
                <ItemTemplate>
                    <tr>
                        <td><input type="checkbox" name="chkSel" title="" value='<%#Eval("GeneralID") %>' /></td>
                        <td><%#Eval("GeneralID") %></td>
                        <td><%# GetTitle(Eval("GeneralID", "{0}"), Eval("ModelID", "{0}"), Eval("Title", "{0}"))%></td>
                        <td><a href="PageContent.aspx?ModelID=<%#Eval("ModelID") %>"><%#GetModel(Eval("ModelID").ToString()) %></a></td>
                        <td><a href="javascript:;" onclick="ShowUserInfo(<%#GetUserID() %>)"><%#Eval("Inputer") %></a></td>
                        <td><%#Eval("CreateTime") %></td>
                        <td><%# GetStatus(Eval("Status", "{0}")) %></td>
                        <td>
                            <a href="<%#GetUrl(Eval("GeneralID").ToString()) %>" class="option_style" target="_blank" title="预览"><span class="fa fa-eye" title="预览"></span></a>
                            <asp:LinkButton ID="LinkButton2" runat="server" ToolTip="修改" CommandName="Edit" CssClass="option_style" CommandArgument='<%# Eval("GeneralID") %>'><i class="fa fa-pencil" title="修改"></i></asp:LinkButton>
                             <asp:LinkButton ID="LinkButton3" runat="server" CssClass="option_style" CommandName="Del" CommandArgument='<%# Eval("GeneralID") %>' OnClientClick="return confirm('确定将该数据删除到回收站?')"><i class="fa fa-trash-o" title="删除"></i> 删除</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </ZL:ExRepeater>
        </table>
    </div>
    <asp:Button ID="Button1" class="btn btn-info" runat="server" Text="审核通过" OnClick="btnAudit_Click" OnClientClick="if(!IsSelectedId()){alert('请选择审核项');return false;}else{return confirm('你确定要审核选中内容吗？')}" />
    <asp:Button ID="Button3" CssClass="btn btn-info" runat="server" OnClick="Button3_Click" Text="取消审核" OnClientClick="if(!IsSelectedId()){alert('请选择取消审核项');return false;}else{return confirm('你确定要取消审核选中内容吗？')}" />
    <asp:Button ID="Button2" class="btn btn-info" runat="server" Text="批量删除" OnClick="btnDeleteAll_Click"
        OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('你确定要将所有选择项放入回收站吗？')}" UseSubmitBehavior="true" />&nbsp;
    <asp:Label runat="server" ID="Label1" Visible="false"></asp:Label>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript" src="/js/Common.js"></script>
    <script type="text/javascript" src="/JS/ajaxrequest.js"></script>
    <script type="text/javascript" src="/JS/PassStrong.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript">
        function SetLI()
        {
            var id = '<%=Request.QueryString["li"]%>';
            $("#navul").children("li").removeClass("active");
            $("#navul").children("li:eq(" + id + ")").addClass("active");
            console.log(id);
        }
        $().ready(function () {
            SetLI();
            $("#chkAll").click(function () {//EGV 全选
                selectAllByName(this, "chkSel");
            });
        })
        function IsSelectedId() {
            var checkArr = $("input[type=checkbox][name=chkSel]:checked");
            if (checkArr.length > 0)
                return true
            else
                return false;
        }
        var diag = new ZL_Dialog();
        function ShowUserInfo(id) {
            diag.title = "用户信息";
            diag.url = "../User/UserInfo.aspx?id=" + id;
            diag.ShowModal();
        }
    </script>

</asp:Content>
