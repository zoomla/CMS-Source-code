<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RouteConfig.aspx.cs" Inherits="test_RouteConfig" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>网站路由器</title>
    <script type="text/javascript" src="/JS/Controls/Control.js"></script>
    <script type="text/javascript" src="/JS/ZL_Regex.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="bs-example bs-example-standalone" data-example-id="dismissible-alert-js">
<div class="alert alert-danger alert-dismissible fade in margin_b2px" role="alert">
  <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
  <strong>欢迎使用站点路由器，使您的目录绑定域名!</strong> 在使用前您需要对您的域名进行泛解析或在本地设置Host文件进行测，关于这一教程您可以访问逐浪官网搜索与“站群”相关的内容。
</div>
<ul class="nav nav-tabs">
    <li class="active"><a href="#cutstomdiv" data-toggle="tab">域名路由</a></li>
    <li><a href="#systemdiv" data-toggle="tab">系统路由</a></li>
</ul>
<div class="tab-content">
    <div id="cutstomdiv" class="tab-pane active">
        <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="尚未自定义路由">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="域名">
                <ItemTemplate>
                    <a href="http://<%#Eval("DomName") %>" target="_blank"><%#Eval("DomName") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="路径" DataField="Url" />
            <asp:TemplateField HeaderText="类型"><ItemTemplate>
                <%#GetType(Eval("SType","")) %>
            </ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="关联用户">
                <ItemTemplate>
                    <a href="../User/Userinfo.aspx?id=<%#Eval("UserID") %>"><%#Eval("UserName") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CDate" DataFormatString="{0:yyyy年MM月dd日}" HeaderText="创建时间" />
            <asp:BoundField DataField="Remind" HeaderText="备注" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a class="option_style" href="AddRoute.aspx?id=<%#Eval("ID") %>"><i class="fa fa-edit" title="修改"></i></a>
                    <asp:LinkButton runat="server" CssClass="option_style" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗');"><i class="fa fa-trash" title="删除"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    </div>
    <div id="systemdiv" class="tab-pane">
        <table class="table table-striped table-bordered tab-pane">
        <tr>
            <td>别名</td>
            <td>类型</td>
            <td>路径</td>
            <td>指向</td>
            <td>操作</td>
        </tr>
        <tr>
            <td>能力中心</td>
            <td>系统内置_页面路由</td>
            <td><a href="/Home" target="_blank" title="点击浏览">Home</a></td>
            <td><a href="/Plat/Blog/" target="_blank" title="浏览页面">/Plat/Blog/</a></td>
            <td><a class="disabled">修改</a><a class="disabled">删除</a></td>
        </tr>
        <tr>
            <td>贴吧</td>
            <td>系统内置_页面路由</td>
            <td><a href="/Index" target="_blank" title="点击浏览">Index</a></td>
            <td><a href="/Guest/Bar/" target="_blank" title="浏览页面">/Guest/Bar/</a></td>
            <td><a class="disabled">修改</a><a class="disabled">删除</a></td>
        </tr>
        <tr>
            <td>百科</td>
            <td>系统内置_页面路由</td>
            <td><a href="/Baike" target="_blank" title="点击浏览">Baike</a></td>
            <td><a href="/Guest/Baike/" target="_blank" title="浏览页面">/Guest/Baike/</a></td>
            <td><a class="disabled">修改</a><a class="disabled">删除</a></td>
        </tr>
        <tr>
            <td>问答</td>
            <td>系统内置_页面路由</td>
            <td><a href="/Ask" target="_blank" title="点击浏览">Ask</a></td>
            <td><a href="/Guest/Ask/" target="_blank" title="浏览页面">/Guest/Ask/</a></td>
            <td><a class="disabled">修改</a><a class="disabled">删除</a></td>
        </tr>
        <tr>
            <td>留言</td>
            <td>系统内置_页面路由</td>
            <td><a href="/Guest" target="_blank" title="点击浏览">Guest</a></td>
            <td><a href="/Guest/" target="_blank" title="浏览页面">/Guest/</a></td>
            <td><a class="disabled">修改</a><a class="disabled">删除</a></td>
        </tr>
        <tr>
            <td>OA办公</td>
            <td>系统内置_页面路由</td>
            <td><a href="/Office" target="_blank" title="点击浏览">Office</a></td>
            <td><a href="/Mis/OA/" target="_blank" title="浏览页面">/Mis/OA/</a></td>
            <td><a class="disabled">修改</a><a class="disabled">删除</a></td>
        </tr>
    </table>
    </div>
</div>
<%--<div class="alert alert-info">添加二级域名后,如未生效,请重启服务器(静态变量不在不同域名间共享)</div>--%>
    <asp:Button runat="server" CssClass="btn btn-primary" ID="BatDel_Btn" Text="批量删除" OnClick="BatDel_Btn_Click" OnClientClick="return confirm('确定要删除吗?');" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
      .disabled{margin-right:5px;color:gray;}
    </style>
<script type="text/javascript">
    function ShowAdd()
    {
        $("#domain_t").val("");
        $("#sendto_t").val("");
        $("#Save_Btn").val("添加");
        $("#CurID_Hid").val("");
        ShowModal();
    }
    function ShowModal() {
        $("#modeldiv").modal({});
        setTimeout(function () { $("#nick_t").focus(); },300);
    }
    function CloseModal() {
        $("#modeldiv").modal("hide");
    }
    function CheckData() {
        flag = false;
        var nick = $("#nick_t").val().replace(/ /g, "");
        var dir = $("#sendto_t").val().replace(/ /g, "");
        var domain = $("#domain_t").val().replace(/ /g, "");
        if (ZL_Regex.isEmpty(nick,dir,domain)) {
            alert("目录,别名,二级域名不能为空");
        }
        else if (ZL_Regex.isContainChina(domain)) {
            alert("域名不能带中文");
        }
        else if (domain.indexOf(".") < 0) {
            alert("二级域名格式不正确,示例:club.demo.com");
        }
        else { flag = true; }
        return flag;
    }
    $(function () {
        $(".disabled").click(function () {
            alert("提示：系统内置路由器不允许操作！");
        });
        Control.EnableEnter();
    })
</script>
</asp:Content>
