<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Question_Class_Manage.aspx.cs" Inherits="manage_Question_Question_Class_Manage" EnableViewStateMac="false" MasterPageFile="~/User/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>分类管理</title>
<style>
#AllID_Chk{display:none;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="edu" data-ban="ke"></div>
<div class="container margin_t5">
<ol class="breadcrumb">
<li><a href="/user">用户中心</a></li>
<li><a href='QuestionManage.aspx'>组卷管理</a></li>
<li>分类管理<a href='AddQuestion_Class.aspx?menu=Add&C_id=0'>[添加分类]</a></li>
</ol>
</div>
<div class="container>"
<div class="divbox" id="nocontent" runat="server" style="display: none">暂无分类信息</div>
<div id="gv" runat="server">
<div id="foo" style="position: fixed; top: 50%; left: 30%; display: block;"></div>
<table class="table table-striped table-bordered table-hover">
<tr>
<td>分类ID</td><td>名称</td><td>排序</td><td>操作</td>
</tr>
<asp:Repeater ID="EGV" runat="server">
<ItemTemplate>
<tr class="list<%#Eval("C_Classid") %>" id="list<%#Eval("C_id") %>" name="list<%#Eval("C_Classid") %>" onclick="getGroupList(this,'grouplist','<%#Eval("C_id") %>');" <%# ShoworHidden(Eval("C_id","{0}"),Eval("C_Classid","{0}")) %> style="cursor:pointer;">
<td style="text-align:center;" class="td_s"><%# Eval("C_id","{0}") %></td>
<td style="text-align:left;"><%# GetIcon(Eval("C_ClassName","{0}"),Eval("C_id","{0}"),Eval("C_Classid","{0}")) %></td>
<td style="text-align:center;"><%# Eval("C_OrderBy") %></td>
<td style="text-align:center;">
<a href="AddQuestion_Class.aspx?menu=Add&pid=<%#Eval("C_id") %>">添加子类</a>
<a href="AddQuestion_Class.aspx?menu=Edit&C_id=<%#Eval("C_id") %>">修改</a>
<a onclick="delquestion(<%#Eval("C_id") %>)" href="javascript:;">删除</a>
</td>
</tr>
</ItemTemplate>
</asp:Repeater>
</table>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Common.js"></script>
    <script type="text/javascript" src="/Plugins/JqueryUI/spin/spin.js"></script>
    <script>
        function showlist(sid)
        {
            var i = 0;
            var j = 0;
            var icons = 0;
            var state = $("#list" + sid).attr("state");
            if (sid == "0") {
                for (i = 3; i <= $("tr").length; i++) {
                    if (state == "1") {
                        $("tr:nth-child(" + i + ")").css("display", "none");
                        $("tr:nth-child(" + i + ")").attr("state", "1");
                        $(".list" + sid).prev().find("[data-type=icon]").attr("class", "fa fa-folder");
                    }
                    else {
                        $(".list" + sid).css("display", "");
                        $("tr:nth-child(" + i + ")").attr("state", "0");
                        if (icons == 0) {
                            $(".list" + sid).prev().find("[data-type=icon]").each(function (i, d) {
                                $(d).attr("class", "fa fa-folder-open");
                                return false;
                            });
                            icons++;
                        }
                    }
                }
            }
            else {
                for (i = 1; i <= $("tr").length; i++) {
                    if ($("tr:nth-child(" + i + ")").attr("name") == $("#list" + sid).attr("name") && $("tr:nth-child(" + i + ")").attr("id") == ("list" + sid)) {
                        j++;
                        continue;
                    }
                    if (j == 1 && $("tr:nth-child(" + i + ")").attr("name") == $("#list" + sid).attr("name")) {
                        j++;
                        break;
                    }
                    switch (j) {
                        case 1:
                            if (state == "1") {
                                if ($("tr:nth-child(" + i + ")").attr("name") != "list0") {
                                    $("tr:nth-child(" + i + ")").css("display", "none");
                                    $("tr:nth-child(" + i + ")").attr("state", "1");
                                    $(".list" + sid).prev().find("[data-type=icon]").attr("class", "fa fa-folder");
                                }
                            }
                            else {
                                $(".list" + sid).css("display", "");
                                $("tr:nth-child(" + i + ")").attr("state", "0");
                                if (icons == 0) {
                                    $(".list" + sid).prev().find("[data-type=icon]").each(function (i, d) {
                                        $(d).attr("class", "fa fa-folder-open");
                                        return false;
                                    });
                                    icons++;
                                }
                            }
                            break;
                    }
                }
            }
            if (state == "1") {
                $("#list" + sid).attr("state", "0");
            } else {
                $("#list" + sid).attr("state", "1");
            }
        }
        var opts = {
            lines: 8, // The number of lines to draw
            length: 6, // The length of each line
            width: 2, // The line thickness
            radius: 5, // The radius of the inner circle
            color: '#000', // #rbg or #rrggbb
            speed: 1, // Rounds per second
            trail: 100, // Afterglow percentage
            shadow: true // Whether to render a shadow
        };
        var trCode = "<tr onclick=\"getGroupList(this,'grouplist','{gid}');\" class=\"list{pid}\" id=\"list{gid}\" name=\"list{pid}\" align=\"center\" state=\"1\" ondblclick=\"showlist('{gid}');\" style=\"cursor:pointer;\">"
                    + "<td>{gid}</td>"
                    + "<td style=\"text-align:left;\">"
                    + "{icon}"
                    + "</td>"
                    + "<td>{C_OrderBy}</td>"
                    + "<td style='text-align:center;'><a href='AddQuestion_Class.aspx?menu=Add&pid={gid}'>添加子类</a> <a href='AddQuestion_Class.aspx?menu=Edit&C_id={gid}'>修改</a> <a href='Question_Class_Manage.aspx?menu=delete&C_id={gid}'>删除</a></td>"
                    + "</tr>";
        function getGroupList(obj, a, groupid) {
            var target = document.getElementById('foo');
            var spinner = new Spinner(opts).spin(target);
            $.ajax({
                type: "Post",
                url: "Question_Class_Manage.aspx",
                data: { want: a, gid: groupid },
                dataType: "json",//json
                success: function (data) {
                    spinner.stop();
                    if (!data) return;
                    for (var i = 0; i < data.length; i++) {
                        newTr = trCode.replace(/{gid}/g, data[i].C_id);
                        newTr = newTr.replace(/{pid}/g, data[i].C_Classid);
                        newTr = newTr.replace(/{icon}/, data[i].icon);
                        newTr = newTr.replace(/{C_OrderBy}/g, data[i].C_OrderBy);
                        $(obj).after(newTr);
                    }
                },
                error: function (data) {
                    spinner.stop();
                    //alert("自定错误" + data);
                }
            })
            obj.onclick = "";
            $(obj).find("[data-type=icon]").each(function (i, d) {
                $(d).attr("class", "fa fa-folder-open");
                return false;
            });
        }
        function delquestion(cid) {
            if (confirm("是否删除该项!")) {
                window.location.href = "Question_Class_Manage.aspx?menu=delete&C_id="+cid;
            }
        }
    </script>
</asp:Content>