<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Manage/I/Default.master" CodeFile="KnowledgeManage.aspx.cs" Inherits="Manage_Exam_KnowledgeManage" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>知识点</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table id="EGV" class="table table-striped table-bordered table-hover">
    <tr><td></td><td class="td_s">ID</td><td>知识点</td><td>科目</td><td>所属年级</td><td>创建人</td><td>启用</td><td>类型</td><td>创建时间</td><td>操作</td></tr>
    <ZL:Repeater ID="RPT" runat="server" OnItemCommand="RPT_ItemCommand">
        <ItemTemplate>
            <tr data-layer="1" data-id='<%#Eval("k_id") %>'>
                <td><input type="checkbox" name="idchk" data-name="<%#Eval("k_name") %>" value="<%#Eval("k_id") %>" /></td>
                <td><%#Eval("k_id") %></td>
                <td onclick="ShowChild(this);" data-child="<%#Eval("ChildCount") %>"><a href='javascript:;'><span data-type='icon' class='<%#GetIcon() %>'></span></a> <a href='AddKnowledge.aspx?id=<%# Eval("k_id")%>&nid=<%=NodeID %>'><%#Eval("k_name") %></a></td>
                <td><%#Eval("C_ClassName") %></td>
                <td><%#Eval("GradeName") %></td>
                <td><%#Eval("UserName") %></td>
                <td><%#GetStatus() %></td>
                <td><%#GetKnowType() %></td>
                <td><%#Eval("CDate") %></td>
                <td>
                    <a href='AddKnowledge.aspx?id=<%#Eval("k_id") %>&nid=<%=NodeID %>' title='修改'><span class='fa fa-pencil option_style'></span></a>
                    <asp:LinkButton runat="server" CommandName="del2" CommandArgument='<%#Eval("k_id") %>'><i class='fa fa-trash-o option_style'></i></asp:LinkButton>
                    <a href="AddKnowledge.aspx?nid=<%:NodeID %>&pid=<%#Eval("k_id") %>"><i class="fa fa-plus option_style" title="添加知识点"></i></a>
                </td>
            </tr>
        </ItemTemplate>
    </ZL:Repeater>
</table>

    <div id="Option_Div" runat="server">
    <asp:Button ID="Dels_Btn" runat="server" CssClass="btn btn-primary" OnClientClick="return confirm('是否删除!')" Text="批量删除" OnClick="Dels_Btn_Click" />
    </div>
    <div id="Sel_Div" class="text-center" runat="server" visible="false">
        <button type="button" onclick="SelKnows()" class="btn btn-primary">确定</button>
        <button type="button" onclick="parent.CloseComDiag()" class="btn btn-primary">取消</button>
    </div>
    <input type="hidden" id="selids_hid" name="selids_hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Controls/ZL_Array.js"></script>
    <script>
        var selarr = [];//当前选中的知识点
        var childTlp = '<tr data-layer="@Layer" data-id="@k_id" data-pid="@Pid"><td><input type="checkbox" data-name="@k_name" value="@k_id" name="idchk" /></td><td>@k_id</td>'
        + '<td onclick="ShowChild(this);" data-child="@ChildCount">@LayerHtml<fun>GetIcon(@ChildCount,@k_id,\"@k_name\")</fun></td>'
        + '<td>@C_ClassName</td>'
        + '<td>@GradeName</td>'
        + '<td>@UserName</td>'
        + '<td><fun>GetStatus(@Status)</fun></td>'
        + '<td><fun>GetKnowType(@IsSys)</fun></td>'
        +'<td>@CDate</td>'
        + '<td><fun>GetOption(@k_id)</fun></td></tr>';

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
                    InitReadEvent();
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
            var $trs = $("#EGV tr[data-pid=" + pid + "]");
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
        //获取状态
        function GetStatus(status) {
            if (status==99) {
                return "<span style='color:green'>启用</span>";
            }
            return "<span style='color:red;'>禁用</span>";
        }
        //获取操作
        function GetOption(id) {
            if(<%=IsRead %>>0){return "<a href='javascript:;' onclick='SelCur(this)'>选择</a>";}
            return "<a href='AddKnowledge.aspx?id="+id+"' title='修改'><span class='fa fa-pencil'></span></a> "
               + "<a href='javascript:;' onclick='DelCofim("+id+")'><span class='fa fa-trash-o'></span></a> "
               +"<a href='AddKnowledge.aspx?nid=<%=NodeID %>&pid="+id+"'><i class='fa fa-plus option_style' title='添加知识点'></i></a>";
        }
        function GetKnowType(issys){
            if(issys==1){ return "系统";}
            return "自定义";
        }
        //获得带图标类型名称
        function GetIcon(count, id, typename) {
            var classname = count > 0 ? "fa fa-folder" : "fa fa-file";
            return "<a href='javascript:;'><span data-type='icon' class='" + classname + "'></span></a> <a href='AddKnowledge.aspx?id=" + id + "'>" + typename + "</a>";
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


        $(function () {
            //$("#EGV tr").dblclick(function () {
            //    window.location.href = "AddKnowledge.aspx?id="+$(this).find("input[name=idchk]").val();
            //});
            InitReadEvent();
        })
        //选择模式事件
        function InitReadEvent() {
            selarr=[];
            $("input[name=idchk]").unbind('change');
            $(".allchk_l").unbind('click');
            if ($("#selids_hid").val() != "") { selarr = JSON.parse($("#selids_hid").val()); }
            $("input[name=idchk]").change(function () {
                if ($(this)[0].checked) {
                    selarr.pushNoDup({ id: $(this).val(), name: $(this).data('name') });
                } else {
                    selarr.RemoveByID($(this).val());
                }
                $("#selids_hid").val(JSON.stringify(selarr));
            });
            $(".allchk_l").click(function () {
                $("input[name=idchk]").each(function () {
                    if ($(this)[0].checked) {
                        selarr.pushNoDup({ id: $(this).val(), name: $(this).data('name') });
                    } else {
                        selarr.RemoveByID($(this).val());
                    }
                });
                $("#selids_hid").val(JSON.stringify(selarr));
            });
        }
        function SelKnows() {
            parent.GetKnows(selarr);
        }
        //选择当前
        function SelCur(obj) {
            $(obj).closest('tr').find('input[name=idchk]').click();
        }
        function DelCur(obj) {
            $(obj).parent().find('.delcur').click();
        }
    </script>
</asp:Content>


