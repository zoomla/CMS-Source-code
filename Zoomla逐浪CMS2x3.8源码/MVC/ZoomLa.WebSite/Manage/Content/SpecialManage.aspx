<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecialManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.SpecialManage"MasterPageFile="~/Manage/I/Default.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>专题列表</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="noncate" class="divbox" style="display: none" runat="server"><font color="red">暂时没有专题类别</font></div>
    <div id="catelist" runat="server">
        <table id="EGV" class="table table-striped table-bordered table-hover">
            <tr id="head_tr"><td>ID</td><td>专题名称</td><td>专题目录</td><td>专题描述</td><td>内容数量</td><td>添加时间</td><td>操作</td></tr>
            <tr id="gendir_tr" data-id="0">
                <td class="td_s">0</td><td><a href="javascript:;"><span class="fa fa-folder-open"></span></a> 根</td><td></td><td></td><td></td><td></td>
                <td>
                    <a href='AddSpec.aspx?Action=Add&pid=0' class='option_style'><i class='fa fa-plus' title='添加'></i>子专题</a>
                    <a href='javascript:;' onclick='ShowOrder(0)' class='option_style' title='排序'><i class='fa fa-list-ol'></i>排序</a>
                </td>
            </tr>
            <tbody id="speclist_body">
                 
            </tbody>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script>   
        var searchflag = "<%=SKey %>";//是否为搜索结果
        var isselmod = "";//是否开启选择模式
        $(function () {
            LoadNodes();
            BindEvent($("#gendir_tr")[0]);
        })
        
        var Temp = "<tr data-layer='@layer' data-pid='@pid' data-id='@id' onclick='LoadChilds(this,@id)'><td>@id</td><td>@layerhtml<img src='/Images/TreeLineImages/t.gif'><a href='SpecContent.aspx?SpecID=@id'><span class='@imgicon'></span></a> <a href='AddSpec.aspx?ID=@id'>@SpecName</a></td><td>@SpecDir</td><td>@SpecDesc</td><td>@ContentCount</td><td>@CDate</td><td><a href='AddSpec.aspx?ID=@id' class='option_style'><i class='fa fa-pencil' title='修改'></i></a> <a href='AddSpec.aspx?Pid=@id' class='option_style'><i class='fa fa-plus' title='添加'></i>子专题</a> <a href='SpecContent.aspx?SpecID=@id' class='option_style'><i class='fa fa-navicon' title='内容'></i>内容</a> <a href='javascript:;' onclick='ShowOrder(@id)' class='option_style' title='排序'><i class='fa fa-list-ol'></i>排序</a> <a title='前台预览' href='/Special_@id/Default' target='_blank' class='option_style'><i class='fa fa-globe'></i>浏览</a> <a href='javascript:;' onclick='DelCofim(@id)' class='option_style'><i class='fa fa-trash-o' title='删除'></i>删除</a></td></tr>";
        var layerTemp = "<img src='/Images/TreeLineImages/tree_line4.gif' />";
        function LoadNodes() {
            $.post('', { action: "GetChild", value: 0 }, function (data) {
                if (data != "" && data != "[]") {
                    data = JSON.parse(data);
                    $("#speclist_body").html(ReplaceTlp(Temp, 1, data));
                    HideColumn("4");
                }
            });
        }
        function LoadChilds(obj, id) {
            if (searchflag != "") {return; }
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
            if(confirm("是否确定删除!"))
            $.ajax({
                type: "Post",
                //url: "Guest",
                data: { action: "Del", value: id },
                success: function (data) {
                    if (data =="1") {
                        window.location = location;
                    }
                },
                error: function (data) {
                }
            });
        }
        function ReplaceTlp(Temp, layer, data) {
            var layerhtml = ""; var html = "";
            for (var i = 0; i < layer; i++) {
                layerhtml += layerTemp;
            }
            Temp = Temp.replace(/@layerhtml/g, layerhtml).replace(/@layer/g, ++layer);
            for (var i = 0; i < data.length; i++) {
                html += Temp.replace(/@id/g, data[i].SpecID).replace(/@pid/g, data[i].Pid).replace(/@SpecName/g, data[i].SpecName).replace(/@SpecDir/g, data[i].SpecDir).replace(/@SpecDesc/g, data[i].SpecDesc).replace(/@ContentCount/g, data[i].ContentCount).replace(/@CDate/g, data[i].CDate).replace(/@imgicon/g, data[i].ChildCount > 0 ? "fa fa-folder" : "fa fa-file");
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
        function ShowOrder(pid) {
            ShowComDiag("SetSpecOrder.aspx?pid=" + pid, "排序管理");
        }
    </script>
</asp:Content>
