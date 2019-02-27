<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VersionList.aspx.cs" Inherits="User_Exam_VersionList" MasterPageFile="~/User/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>教材版本</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="edu" data-ban="ke"></div>
    <div class="container margin_t5">
        <ol class="breadcrumb">
            <li><a href="/user">用户中心</a></li>
            <li>教材版本[<a href="AddVersion.aspx">添加教材版本</a>]</li>
        </ol>
    </div>
    <div class="container">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td>ID</td>
                <td>版本名称</td>
                <%--<td>版本时间</td>--%>
                <%--<td>年级</td>--%>
                <%--<td>科目</td>--%>
                <%--<td>册序</td>--%>
                <td>章(单元)名称</td>
                <td>节名称</td>
                <td>课名称</td>
                <td>知识点</td>
                <td>排序</td>
                <td>操作</td>
            </tr>
            <tbody id="EGV"></tbody>
        </table>
        <div>
            <asp:Button runat="server" Text="上传教材" CssClass="btn btn-primary" />
            <asp:Button runat="server" Text="取消" CssClass="btn btn-primary" />
        </div>
        <%--<ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false"
	OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
	CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="你尚未定义教材版本">
	<Columns>
		<asp:TemplateField>
			<ItemTemplate>
				<input type="checkbox" name="idchk" value="<%#Eval("ID") %>" /></ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="版本名称">
			<ItemTemplate>
				<a href="AddVersion.aspx?id=<%#Eval("ID") %>"><%#Eval("VersionName") %></a>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:BoundField HeaderText="版本时间" DataField="VersionTime" />
		<asp:BoundField HeaderText="年级" DataField="GradeName" />
		<asp:BoundField HeaderText="科目" DataField="NodeName" />
		<asp:BoundField HeaderText="册序" DataField="Volume" />
		<asp:BoundField HeaderText="节名称" DataField="SectionName" />
		<asp:BoundField HeaderText="课名称" DataField="CourseName" />
		<asp:TemplateField HeaderText="操作">
			<ItemTemplate>
                <a class="option_style" href="AddVersion.aspx?pid=<%#Eval("ID") %>"><i class="fa fa-plus"></i>添加</a>
                <a class="option_style" href="AddVersion.aspx?id=<%#Eval("ID") %>"><i class="fa fa-pencil" title="修改"></i>修改</a>
				<asp:LinkButton runat="server" CssClass="option_style" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗');"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateField>
	</Columns>
</ZL:ExGridView>--%>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Controls/ZL_Array.js"></script>
    <script src="/JS/ZL_Regex.js"></script>
    <script>
        var table = $("#EGV");
        //子父级之间依靠 data-pid来确认,如何比较好的定位层级
        var trTlp = '<tr data-id="@ID" data-pid="@Pid" data-child="@Child" data-layer="@layer" title="双击打开"><td class="VID">@ID</td><td><a href="AddVersion.aspx?id=@ID"><fun>getLayer("@layer","@Child")</fun>@VersionName</a></td><td>@VersionTime</td><td>@GradeName</td><td>@NodeName</td><td>@Volume</td><td>@Chapter</td><td>@SectionName</td><td>@CourseName</td>'
                     + '<td>@Knows</td><td><fun>getMove(@Pid,@OrderID)</fun></td><td>'
                     + '<a class="option_style" href="AddVersion.aspx?pid=@ID"><i class="fa fa-plus"></i>添加</a><a class="option_style" href="AddVersion.aspx?id=@ID"><i class="fa fa-pencil" title="修改"></i>修改</a><a class="option_style" href="javacript:;" onclick="del(this);"><i class="fa fa-trash-o" title="删除"></i>删除</a></td></tr>';
        //获取数据
        var getlist = function (obj, pid, isappend) {//需要添加的行
            $(obj).unbind("dblclick");
            $.post("VersionList.aspx", { "action": "getlist", "pid": pid }, function (data) {
                setLayer(data, $(obj).data("layer"));
                var items = [];
                for (var i = 0; i < data.length; i++) {
                    var tlp = "";
                    if (data[i].Pid == 0) {
                        tlp = '<tr data-id="@ID" data-pid="@Pid" data-child="@Child" data-layer="@layer" title="双击打开"><td class="VID">@ID</td><td>@VersionName</td><td></td><td></td><td></td><td></td><td></td><td><a class="option_style" href="AddVersion.aspx?pid=@ID"><i class="fa fa-plus"></i>添加</a><a class="option_style" href="AddVersion.aspx?id=@ID"><i class="fa fa-pencil" title="修改"></i>修改</a><a class="option_style" href="javacript:;" onclick="del(this);"><i class="fa fa-trash-o" title="删除"></i>删除</a></td></tr>';
                    }
                    else
                    {
                        if (data[i].PPid == 0)
                        {
                            tlp = '<tr data-id="@ID" data-pid="@Pid" data-child="@Child" data-layer="@layer" title="双击打开"><td class="VID">@ID</td><td></td><td>@Chapter</td><fun>getMove(@Pid,@OrderID)</fun></td><td></td><td></td><td></td><td></td><td><a class="option_style" href="AddVersion.aspx?pid=@ID"><i class="fa fa-plus"></i>添加</a><a class="option_style" href="AddVersion.aspx?id=@ID"><i class="fa fa-pencil" title="修改"></i>修改</a><a class="option_style" href="javacript:;" onclick="del(this);"><i class="fa fa-trash-o" title="删除"></i>删除</a></td></tr>';
                        }
                        else
                        {
                            tlp = '<tr data-id="@ID" data-pid="@Pid" data-child="@Child" data-layer="@layer" title="双击打开"><td class="VID">@ID</td><td></td><td></td><td>@SectionName</td><td>@CourseName</td><td>@Knows</td><td><fun>getMove(@Pid,@OrderID)</fun></td><td><a class="option_style" href="AddVersion.aspx?id=@ID"><i class="fa fa-pencil" title="修改"></i>修改</a><a class="option_style" href="javacript:;" onclick="del(this);"><i class="fa fa-trash-o" title="删除"></i>删除</a></td></tr>';
                        }
                    }
                    JsonHelper.FillItem(tlp, data[i], function ($item, mod)
                    {
                        $item.bind("dblclick", function () { getlist(this, mod.ID); });
                        items.push($item);
                    });
                }
                if (isappend)
                {
                    $(obj).append(items);
                    $.each(items, function (i, v) {
                        var child = parseInt($(v).data("child"));
                        if (child > 0) { getlist($(v), $(v).data("id")); }
                    });
                }
                else { $(obj).after(items); $(obj).bind("dblclick", function () { ToggleChild(obj) }) }
            }, "JSON");
        }
        //为数据加上层级
        var setLayer = function (data, layer) {
            var newLayer = Convert.ToInt(layer, 0) + 1;
            for (var i = 0; i < data.length; i++) {
                data[i].layer = newLayer;
            }
        }
        //显示或隐藏子级
        function ToggleChild(obj) {
            var id = $(obj).data("id");
            var $tr = table.find("tr[data-pid=" + id + "]");
            if ($tr.is(":hidden")) { $tr.show(); }
            else { HideByPid(id);}
        }
        function HideByPid(pid) {
            $trs = table.find("tr[data-pid=" + pid + "]");
            if (!$trs || $trs.length < 1) return;//不存在,或下再无子级时跳出递归
            for (var i = 0; i < $trs.length; i++) {
                HideByPid($($trs[i]).data("id"));
            }
            table.find("tr[data-pid=" + pid + "]").hide();
        }
        //根据层级,显示对应的空格和图形
        var getLayer = function (layer, child) {
            var layerTlp = '<img src="/Images/TreeLineImages/tree_line4.gif" />';
            var lineTlp = '<img src="/Images/TreeLineImages/t.gif" />';
            layer = Convert.ToInt(layer, 0);
            child = Convert.ToInt(child, 0);
            lineTlp += child > 0 ? '<i class="fa fa-plus-circle"></i>' : '<i class="fa fa-minus-circle"></i>';
            var html = "";
            for (var i = 1; i < layer; i++) {
                html += layerTlp;
            }
            return (html + lineTlp);
        }
        var getMove = function (pid,orderid)
        {
            var html = "";
            if (parseInt(pid) > 0)
            {
                html += '<input type="hidden" value="' + orderid + '" name="Order_hid" /><a href="javascript:;" onclick="MoveUp(this)">↑上移</a> <a href="javascript:;" onclick="MoveDown(this)">下移↓</a>';
            }
            return html;
        }
        function del(a) {
            if (confirm("确定要删除吗?")) {
                var tr = $(a).parent().parent();
                var id = $(tr).data("id");
                $.post('<%=Request.RawUrl %>', { action: "del", id: id }, function (data) {
                    if (data == "true"){ $(tr).remove(); }
                });
        }
    }
    function MoveUp(data) {
        var tr = $(data).parent().parent().parent();
        var layer = $(tr).data("layer");
        var pid = $(tr).data("pid");
        var prev = $(tr).prev("[data-layer=" + layer + "][data-pid=" + pid + "]");
        //若已在该父版本下最上方，则不执行
        if ($(prev).data("layer") != layer || $(prev).data("pid") != pid) { return; }
        var oldid = $(tr).find("[name=Order_hid]").val();
        var noid = $(prev).find("[name=Order_hid]").val();
        $.post('<%=Request.RawUrl %>', { action: "move", oid: $(tr).find(".VID").text() + "," + oldid, nid: $(prev).find(".VID").text() + "," + noid }, function (data) {
	        if (data == "true") {
	            $(tr).find("[name=Order_hid]").val(noid);
	            $(tr).prev().find("[name=Order_hid]").val(oldid);
	            $(tr).insertBefore($(tr).prev());
	        };
	    });
    }
    function MoveDown(data) {
        var tr = $(data).parent().parent().parent();
        var layer = $(tr).data("layer");
        var pid = $(tr).data("pid");
        var next = $(tr).next("[data-layer=" + layer + "][data-pid=" + pid + "]");
        //若已在该父版本下最下方，则不执行
        if ($(next).data("layer") != layer || $(next).data("pid") != pid) { return; }
        if ($(tr).data("pid") != $(tr).next().data("pid")) { return; }
        var oldid = $(tr).find("[name=Order_hid]").val();
        var noid = $(next).find("[name=Order_hid]").val();
        $.post('<%=Request.RawUrl %>', { action: "move", oid: $(tr).find(".VID").text() + "," + $(tr).find("[name=Order_hid]").val(), nid: $(next).find(".VID").text() + "," + noid }, function (data) {
            if (data == "true") {
                $(tr).find("[name=Order_hid]").val(noid);
                $(tr).next().find("[name=Order_hid]").val(oldid);
                $(tr).insertAfter($(tr).next())
            };
        });
    }
    $(function () {
        getlist(table, "0", true);
    })
    </script>
</asp:Content>
