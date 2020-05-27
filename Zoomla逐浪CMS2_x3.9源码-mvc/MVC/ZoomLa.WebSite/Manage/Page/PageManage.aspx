<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Page.PageManage" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>黄页列表</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
        <li><a href="/Admin/I/Main.aspx">工作台</a></li>
        <li><a href="PageManage.aspx">企业黄页</a></li>
        <li><a href="PageManage.aspx">黄页管理</a></li>
        <li>黄页列表</li>
        <li>
            <div class="input-group nav_searchDiv pull-right">
                <asp:TextBox runat="server" ID="keyword" class="form-control" placeholder="请输入用户名" />
                <span class="input-group-btn">
                    <asp:LinkButton runat="server" CssClass="btn btn-default" ID="Button1" OnClick="Button1_Click"><span class="fa fa-search"></span></asp:LinkButton>
                </span>
            </div>
        </li>
        <div id="help" class="pull-right"><a onclick="help_show('86')" title="帮助"><span class="fa fa-question-circle"></span></a></div>
    </ol>
    <div style="height: 50px;"></div>
    <ul class="nav nav-tabs">
        <li class="active"><a href="#tab0" data-toggle="tab" onclick="ShowTabs(0)">全部</a></li>
        <li><a href="#tab1" data-toggle="tab" onclick="ShowTabs(1)">已审核</a></li>
        <li><a href="#tab2" data-toggle="tab" onclick="ShowTabs(2)">待审核</a></li>
    </ul>
    <table class="table table-striped table-bordered table-hover">
        <tbody id="Tbody1">
            <tr>
                <td align="center" style="width: 5%"></td>
                <td align="center" style="width: 5%"><span>ID</span></td>
                <td align="center" style="width: 15%"><span>用户</span></td>
                <td align="center" style="width: 18%"><span>黄页名称</span></td>
                <td align="center">更新时间</td>
                <td align="center">样式名称</td>
                <td class="text-center">状态</td>
                <td align="center" style="width: 25%"><span style="width: 30%">操作</span></td>
            </tr>
            <ZL:ExRepeater ID="RPT" runat="server" PageSize="10" PagePre="<tr id='page_tr'><td colspan='11''><label class='allchk_l'><input type='checkbox' id='chkAll'/>全选</label><div class='text-center'>" PageEnd="</div></td></tr>">
                <ItemTemplate>
                    <tr id="<%#Eval("ID") %>" ondblclick="getinfo(this.id)">
                        <td align="left">
                            <input name="idchk" type="checkbox" value="<%#Eval("ID")%>" /></td>
                        <td><%#Eval("ID")%></td>
                        <td><a href="AuditUser.aspx?menu=view&id=<%#Eval("ID")%>"><%#Eval("UserName")%></a></td>
                        <td><%#Eval("CompanyName")%></td>
                        <td><%#Eval("UpdateTime")%></td>
                        <td class="text-center"><a href="AddPageStyle.aspx?menu=edit&sid=<%#Eval("NodeStyle") %>" title="点击修改样式"><%#Eval("PageNodeName") %></a></td>
                        <td class="text-center"><%#GetCurStatus() %></td>
                        <td>
                            <a class="option_style" href="/Page/Default.aspx?pageid=<%#Eval("ID") %>" target="_blank"><i class="fa fa-eye" title="预览"></i></a>
                            <a href="AuditUser.aspx?menu=modify&id=<%#Eval("ID") %>"><i class="fa fa-pencil" title="修改"></i></a>
                            <a href="?menu=del&id=<%#Eval("ID") %>" onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗');"><i class="fa fa-trash-o" title="删除"></i></a>
                            <a href="PageTemplate.aspx?StyleID=<%#Eval("NodeStyle") %>" title="栏目管理"><i class="fa fa-list-ul" title="栏目管理"></i>栏目管理</a>
                            <%#GetRecommendation()%>
                            <%#GetStatus()%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </ZL:ExRepeater>

        </tbody>
    </table>

    <asp:Button runat="server" CssClass="btn btn-primary" ID="Button3" Text="批量审核" OnClick="Button2_Click" />&nbsp;
            <asp:Button runat="server" CssClass="btn btn-primary" ID="Button2" Text="取消审核" OnClick="Button3_Click" />&nbsp;
            <asp:Button runat="server" CssClass="btn btn-primary" ID="Button4" Text="批量删除" OnClick="Button4_Click" />&nbsp;
            <asp:Button runat="server" CssClass="btn btn-primary" ID="Button5" Text="批量推荐" OnClick="Button5_Click" />&nbsp;
            <asp:Button runat="server" CssClass="btn btn-primary" ID="Button6" Text="取消推荐" OnClick="Button6_Click" />&nbsp;
            <asp:DropDownList runat="server" ID="style_DP" Visible="false"></asp:DropDownList>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/ajaxrequest.js"></script>
    <script src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript">
        $().ready(function () {
            if (getParam("type")) {
                $("li a[href='#tab" + getParam("type") + "']").parent().addClass("active").siblings("li").removeClass("active");;
            }
            $("#chkAll").click(function () { selectAllByName(this, "idchk"); });
        })
        function getinfo(id) {
            location.href = '<%=Getcurrent()%>' + "/page/AuditUser.aspx?menu=modify&id=" + id;
        }
        function area() {
            $.ajax({
                type: "POST",
                url: "PageContent.aspx",
                data: "action=area&value=" + $("#selprovince option:selected").val(),
                success: function (msg) {
                    var s = new Array();
                    s = msg.split("|");
                    var str = "";
                    for (var i = 0; i < s.length; i++) {
                        if (s[i] != null && s[i] != "")
                            str += "<option>" + s[i] + "</option>"
                    }
                    $("#selcity").html(str);
                },
                Error: function (msg) {
                    alert("地址获取失败");
                }
            });
        }
        function ShowTabs(ID) {
            location.href = "PageManage.aspx?tid=" + ID + "&type=" + ID;
        }
    </script>
</asp:Content>
