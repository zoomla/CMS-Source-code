<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Common/Master/Empty.master" CodeFile="page.aspx.cs" Inherits="MIS_page" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>目标</title>
<%Call.Label("{ZL:Boot()/}"); %>
<link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />
<script type="text/javascript">
    function loadPage(id, url) {
        $("#" + id).addClass("loader");
        $("#" + id).append("Loading......");
        $.ajax({
            type: "get",
            url: url,
            cache: false,
            error: function () { alert('加载页面' + url + '时出错！'); },
            success: function (msg) {
                $("#" + id).empty().append(msg);
                $("#" + id).removeClass("loader");
            }
        });
    }
    function setEmpty(obj) {
        if (obj.value == "请输入关键字") {
            obj.value = "";
        }
    }
    function settxt(obj) {
        if (obj.value == "") {
            obj.value = "请输入关键字";
        }
    }
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="Meno">
        <div id="pro_left">
            <div class="new_tar"><a href="AddTarget.aspx">新建目标</a></div>
            <div class="search">
                <div class="pull-left">
                    <asp:DropDownList ID="drType" CssClass="form-control" runat="server" data-container="body" Width="100">
                        <asp:ListItem Value="">全部</asp:ListItem>
                        <asp:ListItem Value="0">事业</asp:ListItem>
                        <asp:ListItem Value="1">财富</asp:ListItem>
                        <asp:ListItem Value="2">家庭</asp:ListItem>
                        <asp:ListItem Value="3">休闲</asp:ListItem>
                        <asp:ListItem Value="4">学习</asp:ListItem>
                    </asp:DropDownList> 
                </div>
                <div class="input-group pull-right" style="width:180px;">
                    <asp:TextBox ID="TxtKey" CssClass="form-control" runat="server" Text="请输入关键字" onclick="setEmpty(this)" onblur="settxt(this)" Width="140"></asp:TextBox>
                    <span class="input-group-btn">
                        <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="搜索" OnClick="Button_Click" />
                    </span>
                </div>
                <div class="Target_list">
                    <ul>
                        <asp:Repeater ID="Repeater2" runat="server">
                            <ItemTemplate>
                                <li><a href="Default.aspx?ID=<%#Eval("ID") %>"><%#Eval("Title") %></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </div>
        <div id="pro_right">
            <div id="Target">
                <table>
                    <tr>
                        <td><img src="/App_Themes/User/iGoal.jpg" /></td>
                        <td><h1><a href="AddTarget.aspx">添加目标</a></h1>
                            <p>确定任务/项目名称，规划或总结执行各阶段内容，呈现真实执行现场！</p>
                            <p>让工作看得见，是所有工作者和管理者的梦想。然而大多数的业务执行从计划、实施再到总结评估，所有记录往往都碎片式的分散于不同部门、不同人员手中。记录不完整，过程无主线，在整个业务执行过程中产生的问题也就很难被发现。</p>
                            <p>目标是本网站新推出的全新办公工具。通过工作框和现场图，将企业运作的实际过程，简单清晰的保留下来。完整记录业务执行的整个过程，使执行全程可视化，管理“看”得见。</p>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
