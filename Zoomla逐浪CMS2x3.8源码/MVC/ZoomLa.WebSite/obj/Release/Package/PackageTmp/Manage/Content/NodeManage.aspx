<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NodeManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.NodeManage" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title><%=Resources.L.节点管理 %></title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<script src="/JS/Controls/ZL_Array.js"></script>
<div class="" id="foo" style="position: fixed; top: 50%; left: 30%; display: block;"></div>
<table class="table table-striped table-bordered table-hover nodelist_div">
	<tr class="gridtitle text-center">
		<td class="title td_s"><input type="checkbox" id="chkAll" onclick="nodechk(this);" /></td>
		<td class="td _s text-center"><strong>ID</strong></td>
		<td><strong><%=Resources.L.节点名称 %></strong></td>
		<td class="td_m"><strong><%=Resources.L.节点类型 %></strong></td>
        <td class="td_m"><strong>文章数(总计)</strong></td>
        <td><strong><%=Resources.L.创建时间 %></strong></td>
		<td><strong><%=Resources.L.操作 %></strong></td>
	</tr>
	<asp:Repeater ID="RPT" runat="server" EnableViewState="false">
		<ItemTemplate>
			<tr <%#Action.Equals("showall")?"":"onclick=\"getNodeList(this,'nodeList','"+Eval("NodeID")+"');\"" %> class="list<%# Eval("ParentID") %>" id='list<%#Eval("NodeID")%>' name="list<%#Eval("ParentID")%>"
			   align="center" <%# ShoworHidden(Eval("NodeID")) %>>
				<td class="text-center"><input id="idchk_<%#Eval("NodeID") %>" name="idchk" type="checkbox" value='<%# Eval("NodeID")%>' /></td>
				<td><label for="idchk_<%#Eval("NodeID") %>"><%# Eval("NodeID") %></label></td>
				<td class="text-left"><%# ShowIcon()%></td>
				<td><strong><%# GetNodeType(Eval("NodeType","{0}"))%></strong></td>
                <td><%#Eval("ItemCount") %></td>
                <td><%#Eval("CDate") %></td>
				<td class="optd"><%#GetOper()%></td>
			</tr>
		</ItemTemplate> 
	</asp:Repeater>
</table>
<asp:Button ID="Button3" class="btn btn-primary" runat="server" OnClientClick="return confirm('此操作将删除现有站点数据，确认？');" Text="<%$Resources:L,批量删除 %>" OnClick="Button3_Click" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">
    .optd {text-align:left;}
</style>
<script src="/Plugins/JqueryUI/spin/spin.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
    var childTlp = "<tr onclick=\"getNodeList(this,'nodeList','@NodeID');\" class=\"tdbg list@ParentID\" id=\"list@NodeID\" name=\"list@ParentID\" align=\"center\" state=\"1\" ondblclick=\"showlist(@NodeID)\" >"
				+ "<td style=\"text-align:center;\">"
				+ "<input id=\"idchk_@NodeID\" name=\"idchk\" type=\"checkbox\" value=\"@NodeID\"></td>"
				+ "<td><label for=\"idchk_@NodeID\">@NodeID</label></td>"
				+ "<td style=\"text-align:left;\">@icon"
			    + "</td><td><strong>@type2</strong></td>"
			    + "<td>@ItemCount</td><td>@CDate</td><td class='optd'>@oper</td></tr>";

    function getNodeList(obj, a, nodeID) {
        var target = document.getElementById('foo');
        var spinner = new Spinner().spin(target);
        $.ajax({
            type: "Post",
            url: "NodeManage.aspx",
            data: { want: a, nid: nodeID },
            dataType: "json",//json
            success: function (data) {
                spinner.stop();
                if (!data) return;
                $(obj).after(JsonHelper.FillData(childTlp, data));
            },
            error: function (data) { spinner.stop(); }
        });
        obj.onclick = "";
        $(obj).find("[data-type=icon]").each(function (i, d) {
            $(d).attr("class", "fa fa-folder-open");
            return false;
        });
    }
</script>
<script>
    var Num = 0;
    var nn = 0;
    $(function () {
        $("[data-type=icon]:eq(0)").attr("class", "fa fa-folder-open");
    })
    function showlist(sid) {
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
    //为了Json序列化，如此处理
    var data = ["EditNode.aspx?NodeID=", "SetNodeOrder.aspx?ParentID=", "SetNodeOrder.aspx?ParentID=", 'EditSinglePage.aspx?NodeID=', "EditOutLink.aspx?NodeID="];
    function isInt(e) { var t = /^\d+(\d+)?$/gi; return t.exec(e) ? !0 : !1 }
    var nodediag = new ZL_Dialog();
    function open_page(NodeID, strURL) {
        nodediag.maxbtn = false;
        nodediag.title = "<%=Resources.L.添加节点 %>";
	    if (isInt(strURL)) {
	        strURL = data[strURL];
	    }
	    nodediag.url = strURL + NodeID;
	    nodediag.ShowModal();
    }
    function editnode(NodeID) {
        var answer = confirm("<%=Resources.L.该栏目未绑定模板 %>，<%=Resources.L.是否立即绑定 %>");
	    if (answer == false) {
	        return false;
	    }
	    else {
	        open_page(NodeID, "EditNode.aspx?NodeID=");
	    }
	}
	function emptynode() {
	    var answer = confirm("<%=Resources.L.此操作将删除现有站点数据 %>，<%=Resources.L.确认 %>？");
	    if (answer == false) {
	        return false;
	    }
	    else {
	        window.location.href = "NodeManage.aspx?action=del";
	    }
	}
	function delConfirm() {
	    return confirm("<%=Resources.L.你确定要删除该节点吗 %>？");
	}
    function createHtml(nid) {
        var path = "<%=customPath2%>";
        var url = path + "Content/ReleaseByNode.aspx?NodeID=" + nid;
        comdiag.reload = true;
        ShowComDiag(url, "生成发布");
    }
    function nodechk(obj) {
        $("input[name='idchk']").each(function () {
            this.checked = obj.checked;
        })
    }
</script>
</asp:Content>