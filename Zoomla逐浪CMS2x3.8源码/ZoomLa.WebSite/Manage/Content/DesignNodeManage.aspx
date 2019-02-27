<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DesignNodeManage.aspx.cs" Inherits="Manage_Content_DesignNodeManage" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>动力节点</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div>
    <ol class="breadcrumb navbar-fixed-top">
        <li><a href='<%=CustomerPageAction.customPath2 %>Main.aspx'>工作台</a></li>
        <li><a href='<%=customPath2 %>Config/SiteInfo.aspx'>系统设置</a></li>
        <li class='active dropdown'><a href='DesignNodeManage.aspx'>动力节点</a></li><%=Call.GetHelp(15) %>
        <div id="help" class="pull-right text-center" style="margin-right:8px;"><a href="javascript:;" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a></div>
        <div id="sel_box" runat="server" class="padding5">
            <%--<span class="pull-left">高级查询：
                <asp:DropDownList ID="souchtable" CssClass="form-control" Width="150" runat="server">
                    <asp:ListItem Value="rename">用户名称</asp:ListItem>
                    <asp:ListItem Value="proname">节点名称</asp:ListItem>
                </asp:DropDownList>
            </span>--%>
            <span class="pull-left" style="line-height:40px;">节点名称：</span>
            <div class="input-group pull-left" style="width: 300px;">
                <asp:TextBox runat="server" ID="souchkey" class="form-control" placeholder="请输入需要搜索的内容" />
                <span class="input-group-btn">
                    <asp:LinkButton runat="server" CssClass="btn btn-default" ID="souchok" OnClick="souchok_Click"><span class="fa fa-search"></span></asp:LinkButton>
                </span>
            </div>
        </div>
    </ol>
    <ul class="list-unstyled node_list">
        <ZL:ExRepeater runat="server" ID="RPT" PageSize="15" PagePre="</div> <div class='panel-footer text-center'>" PageEnd="</div>" OnItemDataBound="RPT_ItemDataBound">
            <ItemTemplate>
                <li>
                    <div class="node_box panel panel-default">
                        <div class="panel-heading">
                            <a href="javascript:;"><i class="fa fa-user"></i><strong><%#GetUser() %></strong></a>
                        </div>
                        <div class="panel-body box_body" title="双击查看子节点" ondblclick="location.href='DesignNodeList.aspx?pid=<%#Eval("NodeID") %>'">
                            <div><span class="info_tit">节点名称：</span><%#Eval("NodeDir") %></div>
                            <div><span class="info_tit">子节点数：</span><%#Eval("ChildCount","")%></div>
                            <div><span class="info_tit">文章总数：</span><%#Eval("ItemCount") %></div>
                            <div><span class="info_tit">创建时间：</span><%#Eval("CDate","{0:yyyy-MM-dd hh:MM:ss}") %></div>
                        </div>
                        <div class="panel-footer box_footer">
                            <a href="EditNode.aspx?view=design&NodeID=<%#Eval("NodeID") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i>修改</a>
                            <a href="EditNode.aspx?view=design&ParentID=<%#Eval("NodeID") %>" class="option_style"><i class="fa fa-plus" title="添加"></i>节点</a> 
                            <a href="javascript:void(0)" onclick="opendiag('添加节点','SetNodeOrder.aspx?view=design&ParentID=<%#Eval("NodeID") %>')" class="option_style"><i class="fa fa-list-ol" title="排序"></i>排序</a>
                            <a href="DelNode.aspx?view=design&NodeID=<%#Eval("NodeID") %>" onclick="return delConfirm();" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</a>
                        </div>
                    </div>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                <div class="clearfix"></div>
            </FooterTemplate>
        </ZL:ExRepeater>
    </ul>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style>
.node_list li{float:left;margin:5px 10px 0 0;width:300px;}
.node_box{width:100%;}
.node_box .info_tit{text-align: right;color: #999;display: inline-block;line-height: 22px;margin-right:10px;}
.node_box .panel-body{height:110px;}
.box_footer{text-align:right;}
.box_body>div{overflow: hidden;white-space: nowrap;text-overflow: ellipsis;display: inline-block;width: 100%;margin:-3px 0;}
.box_body:hover{background-color:#fbfbfb;cursor:pointer;}
</style>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
    var  diag = new ZL_Dialog();
    function opendiag(title,url) {
        diag.reload = true;
        diag.title = title;
        diag.url = url;
        diag.backdrop = true;
        diag.maxbtn = false;
        diag.ShowModal();
    }
    function delConfirm() {
	    return confirm("<%=Resources.L.你确定要删除该节点吗 %>？");
    }
    $("#sel_btn").click(function () {
        if ($("#sel_box").css("display") == "none") {
            showsel();
        }
        else {
            $(this).removeClass("active");
            $("#sel_box").slideUp(200);
            $(".node_list").css("margin-top","0");
        }
    });
    function showsel() {
        $(this).addClass("active");
        $("#sel_box").slideDown(300);
        $(".node_list").css("margin-top", "42px");
    }
</script>
</asp:Content>
