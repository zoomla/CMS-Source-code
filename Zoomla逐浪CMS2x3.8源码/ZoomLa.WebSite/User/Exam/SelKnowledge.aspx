<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User/Empty.master" CodeFile="SelKnowledge.aspx.cs" Inherits="Manage_Exam_KnowledgeManage" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>知识点</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
 <div class="input-group text_300">
     <asp:TextBox ID="Search_T" runat="server" placeholder="知识点" CssClass="form-control"></asp:TextBox>
      <span class="input-group-btn">
          <asp:LinkButton ID="Search_Btn" runat="server" CssClass="btn btn-primary" OnClick="Search_Btn_Click"><i class="fa fa-search"></i></asp:LinkButton>
      </span>
    </div>
<table id="EGV" class="table table-striped table-bordered table-hover margin_t10">
    <tr><td></td><td class="td_s">ID</td><td>知识点</td><td>科目</td><td>所属年级</td><td>创建人</td><td>启用</td><td>类型</td><td>创建时间</td></tr>
    <ZL:Repeater ID="RPT" runat="server">
        <ItemTemplate>
            <tr data-layer="1" data-id='<%#Eval("k_id") %>'>
                <td><input type="checkbox" name="idchk" data-name="<%#Eval("k_name") %>" value="<%#Eval("k_id") %>" /></td>
                <td><%#Eval("k_id") %></td>
                <td onclick="ShowChild(this);" data-child="<%#Eval("ChildCount") %>"><a href='javascript:;'><span data-type='icon' class='<%#GetIcon() %>'></span></a> <a href='javascript:;'><%#Eval("k_name") %></a></td>
                <td><%#Eval("C_ClassName") %></td>
                <td><%#Eval("GradeName") %></td>
                <td><%#Eval("UserName") %></td>
                <td><%#GetStatus() %></td>
                <td><%#GetKnowType() %></td>
                <td><%#Eval("CDate") %></td>
            </tr>
        </ItemTemplate>
    </ZL:Repeater>
</table>
        <button type="button" onclick="SelKnows()" class="btn btn-primary">确定</button>
        <button type="button" onclick="parent.CloseComDiag()" class="btn btn-primary">取消</button>
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
        + '</tr>';

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
            if (status == 0) {
                return "<span style='color:red;'>禁用</span>";
            }
            else { return "<span style='color:green'>启用</span>"; }
        }
        function GetKnowType(issys){
            if(issys==1){ return "系统";}
            return "自定义";
        }
        //获得带图标类型名称
        function GetIcon(count, id, typename) {
            var classname = count > 0 ? "fa fa-folder" : "fa fa-file";
            return "<a href='javascript:;'><span data-type='icon' class='" + classname + "'></span></a> <a href='javascript:;'>" + typename + "</a>";
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


