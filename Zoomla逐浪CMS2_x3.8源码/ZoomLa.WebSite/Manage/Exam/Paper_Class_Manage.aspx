<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Paper_Class_Manage.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="Manage_Exam_Paper_Class_Manage" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>试卷分类管理</title>
<style>
#AllID_Chk{display:none;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table id="EGV" class="table table-striped table-bordered table-hover">
            <tr><td class="td_s">分类ID</td><td>名称</td><td>所属分类</td><td>备注</td><td>操作</td></tr>
        <ZL:Repeater ID="RPT" runat="server">
            <ItemTemplate>
              <tr data-layer="1" data-id='<%#Eval("ID") %>'>
                  <td><%#Eval("ID") %></td>
                  <td onclick="ShowChild(this);" data-child="<%#Eval("ChildCount") %>"><a href='Papers_System_Manage.aspx?type=<%#Eval("ID") %>'><span data-type='icon' class='<%#GetIcon() %>'></span></a> <a href='AddPaperClass.aspx?ID=<%# Eval("ID")%>'><%#Eval("TypeName") %></a></td>
                  <td><%#string.IsNullOrEmpty(Eval("TypeName").ToString())?"无所属分类":Eval("TypeName") %></td>
                  <td><%#Eval("Remind") %></td>
                  <td>
                      <a href='AddPaperClass.aspx?id=<%# Eval("ID")%>' class="option_style"><i class="fa fa-pencil" title="修改"></i></a> 
                      <a href="javascript:;" onclick='DelCofim(<%# Eval("ID")%>)' class="option_style"><i class="fa fa-trash-o" title="删除"></i></a> 
                      <a href='AddPaperClass.aspx?pid=<%# Eval("ID")%>' class="option_style"><i class="fa fa-plus" title="添加"></i>子分类</a> 
                      <a href='Papers_System_Manage.aspx?type=<%#Eval("ID") %>' class="option_style"><i class="fa fa-navicon" title="内容"></i>试卷内容</a> 
                  </td>
              </tr>
            </ItemTemplate>
        </ZL:Repeater>
        </table>
    <script type="text/javascript" src="/JS/Controls/ZL_Array.js"></script>
    <script>
        var childTlp = '<tr data-layer="@Layer" data-id="@ID" data-pid="@Pid"><td>@ID</td>'
        + '<td onclick="ShowChild(this);" data-child="@ChildCount">@LayerHtml<fun>GetIcon(@ChildCount,@ID,\"@TypeName\")</fun></td>'
        + '<td>@ParentName</td>'
        + '<td><fun>GetCStr(@Pid,@ID)</fun></td></tr>';
        //第一次点击时加载,以后点击时显示子级
        function ShowChild(obj) {
            if ($(obj).data("child") < 1)//无子级直接返回
            {
                obj.onclick = null;
                return false;
            }
            $tr = $(obj).closest("tr");
            $.post("", { action: "getchild", nid: $tr.data("id") }, function (data) {
                if (data != "" && data != "[]") {
                    data = JSON.parse(data);
                    var html = ReplaceTlp(childTlp, $tr.data("layer"), data);
                    obj.onclick = function () { ToggleChild(obj); }
                    $tr.after(html);
                    $tr.find("[data-type=icon]").attr("class", "fa fa-folder-open");
                    //用于内容与栏目
                    if ($(".menu_node").is(":visible")) { ShowMenu("node"); }
                    else if ($(".menu_content").is(":visible")) { ShowMenu("content"); }
                }
            });
        }
        //确定是要显示还是隐藏
        function ToggleChild(obj) {
            $tr = $(obj).closest("tr");
            var id = $tr.data("id");
            $trs = $("#EGV tr[data-pid=" + id + "]");
            if ($trs.length < 1) return;
            var flag = $trs.is(":visible");
            if (flag) {
                HideByPid(id);
                $tr.find("[data-type=icon]").attr("class", "fa fa-folder");
            }//隐藏的话递归  
            else {
                $trs.show();
                $tr.find("[data-type=icon]").attr("class", "fa fa-folder-open");
            }
        }
        //true隐藏,false显示 
        function HideByPid(pid) {
            $trs = $("#EGV tr[data-pid=" + pid + "]");
            if (!$trs || $trs.length < 1) return;//不存在,或下再无子级时跳出递归
            for (var i = 0; i < $trs.length; i++) {
                HideByPid($($trs[i]).data("id"));
            }
            $("#EGV tr[data-pid=" + pid + "]").hide();
        }
        //附加数据
        function ReplaceTlp(tlp, layer, list) {
            var layerTlp = "<img src='/Images/TreeLineImages/tree_line4.gif' />";
            var layerhtml = "";
            for (var i = 0; i < layer; i++) {
                layerhtml += layerTlp;
            }
            tlp = tlp.replace("@LayerHtml", layerhtml).replace("@Layer", ++layer);
            //替换模板
            return JsonHelper.FillData(tlp, list);
        }
        //返回生成字符串
        function GetCStr(Pid, nodeID) {
            //有内容和栏目两种选项
            var html = "";
            html += "<a href='AddPaperClass.aspx?id=" + nodeID + "' class='option_style'><i class='fa fa-pencil' title='修改'></i></a> ";
            html += "<a href='javascript:;' onclick='DelCofim(" + nodeID + ")' class='option_style'><i class='fa fa-trash-o' title='删除'></i></a>";
            html += "<a href='AddPaperClass.aspx?pid=" + nodeID + "' class='option_style'><i class='fa fa-plus' title='添加'></i>子分类</a> ";
            html += "<a href='Papers_System_Manage.aspx?type=" + nodeID + "' class='option_style'><i class='fa fa-navicon' title='内容'></i>试卷内容</a> ";
            return html;
        }
        //获得带图标类型名称
        function GetIcon(count,id, typename) {
            var classname = count > 0 ? "fa fa-folder" : "fa fa-file";
            return "<a href='papers_system_manage.aspx?type=" + id + "'><span data-type='icon' class='" + classname + "'></span></a> <a href='AddPaperClass.aspx?id=" + id + "'>" + typename + "</a>";
        }
        function DelCofim(id) {
            if (confirm("是否确定删除!"))
                $.ajax({
                    type: "Post",
                    //url: "Guest",
                    data: { action: "del", value: id },
                    success: function (data) {
                        if (data == "1") {
                            window.location = location;
                        }
                    },
                    error: function (data) {
                    }
                });
        }
    </script>
</asp:Content>


