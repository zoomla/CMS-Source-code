<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Question_Class_Manage.aspx.cs" Inherits="manage_Question_Question_Class_Manage" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>科目管理</title>
    <style>
        #AllID_Chk{display:none;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="divbox" id="nocontent" runat="server" style="display: none">暂无分类信息</div>
    <div id="gv" runat="server">
        <table id="EGV" class="table table-striped table-bordered table-hover">
            <tr><td class="td_s">科目ID</td><td>名称</td><td>所属科目</td><td>操作</td></tr>
        <ZL:Repeater ID="RPT" runat="server">
            <ItemTemplate>
              <tr data-layer="1" data-id='<%#Eval("C_Id") %>' onclick='LoadChilds(this,<%#Eval("C_Id") %>)'>
                  <td><%#Eval("C_Id") %></td>
                  <td><a href='QuestionManage.aspx?type=1&tag=<%#Eval("C_Id") %>'><span class='<%#GetIcon() %>'></span></a> <a href='AddQuestion_Class.aspx?Action=Modify&ID=<%# Eval("C_Id")%>&Pid=<%# Eval("C_Classid")%>'><%#Eval("C_ClassName") %></a></td>
                  <td><%#string.IsNullOrEmpty(Eval("ParentName").ToString())?"无所属科目":Eval("ParentName") %></td>
                  <td>
                      <a href='AddQuestion_Class.aspx?Action=Modify&ID=<%# Eval("C_Id")%>&Pid=<%# Eval("C_Classid")%>' title="修改"><span class="fa fa-pencil" title="修改"></span></a>
                      <a href="javascript:;" onclick='DelCofim(<%# Eval("C_Id")%>)' title="删除"><span class="fa fa-trash-o" title="删除"></span></a>
                      <a href='AddQuestion_Class.aspx?Action=Add&ID=<%# Eval("C_Id")%>' class="option_style"><i class="fa fa-plus" title="添加"></i>子科目</a>
                      <a href="AddKnowledge.aspx?nid=<%#Eval("C_Id") %>" class="option_style"><i class="fa fa-plus" title="添加"></i>知识点</a>
                      <a href="KnowledgeManage.aspx?nid=<%#Eval("C_Id") %>" class="option_style"><i class="fa fa-list-alt" title="列表"></i>知识点列表</a>
                      <a href='QuestionManage.aspx?NodeID=<%#Eval("C_Id") %>' class="option_style"><i class="fa fa-navicon" title="内容"></i>试题内容</a>
                  </td>
              </tr>
            </ItemTemplate>
        </ZL:Repeater>
        </table>
    </div>
  </asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Controls/ZL_Array.js"></script>
    <script type="text/javascript" src="/js/Common.js"></script>
    <script>
        var Temp = "<tr data-layer='@layer' data-pid='@C_Classid' data-id='@C_id' onclick='LoadChilds(this,@C_id)'>"
                + "<td>@C_id</td><td>@layerhtml<img src='/Images/TreeLineImages/t.gif'><a href='QuestionManage.aspx?type=1&tag=@C_id'><span class='@imgicon'></span></a> <a href='AddQuestion_Class.aspx?Action=Modify&ID=@C_id&Pid=@C_Classid'>@C_ClassName</a></td>"
                + " <td>@ParentName</td><td><a href='AddQuestion_Class.aspx?Action=Modify&ID=@C_id&Pid=@C_Classid' title='修改'><span class='fa fa-pencil'></span></a>"
                + " <a href='javascript:;' onclick='DelCofim(@C_id)' title='删除'><span class='fa fa-trash-o'></span></a>"
                + " <a href='AddQuestion_Class.aspx?Action=Add&ID=@C_id'>添加子科目</a>"
                + " <a href='QuestionManage.aspx?type=1&tag=@C_id'>试题内容</a></td></tr>"
        var layerTemp = "<img src='/Images/TreeLineImages/tree_line4.gif' />";
        function LoadChilds(obj, id) {
            $.ajax({
                type: "Post",
                //url: "Guest",
                data: { action: "GetChild", value: id },
                success: function (data) {
                    if (data != "" && data != "[]") {
                        data = JSON.parse(data);
                        var html = ReplaceTlp(Temp, $(obj).data("layer"), data);
                        obj.onclick = "";
                        $(obj).after(html);
                        BindEvent(obj);
                    }
                },
                error: function (data) {
                }
            });
        }
        function DelCofim(id) {
            if (confirm("是否确定删除!"))
                $.ajax({
                    type: "Post",
                    //url: "Guest",
                    data: { action: "Del", value: id },
                    success: function (data) {
                        if (data == "1") {
                            window.location = location;
                        }
                    },
                    error: function (data) {
                    }
                });
        }
        function ReplaceTlp(Temp, layer, data) {
            var layerhtml = ""; var html = "";
            for (var i = 0; i <layer; i++) {
                layerhtml += layerTemp;
            }
            Temp = Temp.replace(/@layerhtml/g, layerhtml).replace(/@layer/g, ++layer);
            for (var i = 0; i < data.length; i++) {
                var curtemp = Temp.replace(/@ParentName/g,data[i].ParentName==""?"无所属科目":data[i].ParentName).replace(/@imgicon/g, data[i].ChildCount > 0 ? "fa fa-folder" : "fa fa-file");
                html += JsonHelper.FillData(curtemp, data[i]);
            }
            return html;
        }
        function BindEvent(obj) {
            $(obj).unbind('click');
            $(obj).find(".fa-folder").attr("class", "fa fa-folder-open");
            $(obj).dblclick(function () {
                var pid = $(this).data("id");
                if ($("#EGV tr[data-pid=" + pid + "]").is(":visible")) {
                    HideTr(pid);
                    $(this).find(".fa-folder-open").attr("class", "fa fa-folder");
                } else {
                    $("#EGV").find("[data-pid=" + $(this).data("id") + "]").show();
                    $(this).find(".fa-folder").attr("class", "fa fa-folder-open");
                }
            });
        }
        function HideTr(pid) {
            if ($("#EGV tr[data-pid=" + pid + "]").length < 1) return;
            $("#EGV tr[data-pid=" + pid + "]").each(function (i, v) {
                HideTr($(v).data("id"));
            });
            $("#EGV tr[data-pid=" + pid + "]").find(".fa-folder-open").attr("class", "fa fa-folder");
            $("#EGV tr[data-pid=" + pid + "]").hide();
        }
    </script>
</asp:Content>