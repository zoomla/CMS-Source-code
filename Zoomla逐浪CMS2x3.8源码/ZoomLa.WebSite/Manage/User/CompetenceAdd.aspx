<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompetenceAdd.aspx.cs" Inherits="Manage_User_CompetenceAdd" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>权限设置</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2" class="text-center text_150">角色权限设置</td>
        </tr>
        <tr>
            <td class="text-right">部门名：</td>
            <td><asp:Label ID="RoleName" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right">部门描述：</td>
            <td><asp:Label ID="RoleInfo" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right">OA权限设置：</td>
            <td>
                <div class="checkdiv">
                    <input type="checkbox" onclick="checkall(this);" name="OAMeetingManage" id="OAMeetingManage" value="OAMeetingManage" />会议权限
                    
                </div>
                <div class="checkdiv">
                    <input type="checkbox" id="OATop" name="OATop" onclick="showtext(this)"  value="OATop" /> 置顶权限
                    <asp:TextBox ID="OATopNodeID_T" runat="server" style="display:none;"></asp:TextBox><span id="OATopSpan" style="display:none;">输入节点ID，格式为,1,2,</span>
                </div>
                <div class="checkdiv">
                    <input type="checkbox" id="OADel" name="OADel" onclick="showtext(this)" value="OADel" /> 删除权限
                    <asp:TextBox ID="OADelNodeID_T" runat="server" style="display:none;"></asp:TextBox><span id="OADelSpan" style="display:none;">输入节点ID，格式为,1,2,</span>
                </div>
                <div class="checkdiv">
                    <input type="checkbox" id="OAEdit" name="OAEdit" onclick="showtext(this)" value="OAEdit" /> 修改权限
                    <asp:TextBox ID="OAEditNodeID_T" runat="server" style="display:none;"></asp:TextBox><span id="OAEditSpan" style="display:none;">输入节点ID，格式为,1,2,</span>
                </div>
                <div class="checkdiv">
                    <input type="checkbox" name="OATaskManage" id="OATaskManage" value="OATaskManage" />任务管理器
                </div>
                <div class="checkdiv">
                    <input type="checkbox" onclick="checkall(this);" name="OAEmailManage" id="OAEmailManage" value="OAEmailManage" />邮件权限<br />
                    <input type="checkbox" name="OAEmailManage" id="Checkbox9" value="OAEmailWrite" />写邮件权限
                    <input type="checkbox" name="OAEmailManage" id="Checkbox1" value="OAEmailRead" />读邮件权限
                    <input type="checkbox" name="OAEmailManage" id="Checkbox2" value="OAEmailContact " />邮件联系人权限
                </div>
                <div class="checkdiv">
                    <input type="checkbox" onclick="checkall(this);" name="OADepartmentManage" id="OADepartmentManage" value="OADepartmentManage" />部门公告权限<br />
            </td>
        </tr>
        <tr>
            <td class="text-right td_m">OA流程权限</td>
            <td>
                 <label><input type="checkbox" name="auth_oa" value="oa_doc_edit" />修订</label>
                 <label><input type="checkbox" name="auth_oa" value="oa_doc_sign" />签章</label>
                 <label><input type="checkbox" name="auth_oa" value="oa_doc_head" />套红</label>
                 <label><input type="checkbox" name="auth_oa" value="oa_doc_traceless" />无痕修订</label>
                 <label><input type="checkbox" name="auth_oa" value="oa_doc_hastrace" />有痕修订</label>
            </td>
        </tr>
        <tr><td class="text-right">OA权限</td><td>
            <label><input type="checkbox" name="auth_oa" value="oa_pro_no"/>编号</label>
            <label><input type="checkbox" name="auth_oa" value="oa_pro_file" />归档</label>
            <label><input type="checkbox" name="auth_oa" value="oa_pro_visor" />督办</label></td></tr>
        <tr>
            <td class="text-right">模块权限</td>
            <td>
                <input type="checkbox" name="ContentManage" id="ContentManage" value="ContentManage" />内容管理
                <input type="checkbox" name="ModelNodeManage" id="ModelNodeManage" value="ModelNodeManage" />模型节点管理
                <input type="checkbox" name="TemplateLabelManage" id="TemplateLabelManage" value="TemplateLabelManage" />模板标签管理
                <input type="checkbox" name="ShopManage" id="ShopManage" value="ShopManage" />商城管理
                <input type="checkbox" name="PageManage" id="PageManage" value="PageManage" />黄页管理
                <input type="checkbox" name="UserShopManage" id="UserShopManage" value="UserShopManage" />店铺管理
                <input type="checkbox" name="UserZoneManage" id="UserZoneManage" value="UserZoneManage" />空间管理
            </td>
        </tr>
        <tr><td class="text-right">其它权限</td><td><label><input type="checkbox" value="design" name="other_chk" />在线网页设计</label></td></tr>  
        <tr>
            <td></td>
            <td><input type="button" onclick="check()" name="btnSave" class="btn btn-primary" value="保存权限" />
                <a href="PermissionInfo.aspx" class="btn btn-primary">返回列表</a></td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        $().ready(function () {
            var codes = '<%= GetCodes()%>';
            if (codes != "") {
                var codesArr = codes.split(',');
                for (var i = 0; i < codesArr.length; i++) {
                    var checkArr = $("input[type=checkbox][value=" + codesArr[i] + "]");
                    checkArr[0].checked = true;
                }
            }
            if ($("input[type=checkbox][id=OATop]")[0].checked==true)
            {
                $("#OATopNodeID_T").show();
                $("#OATopSpan").show();
            }
            if ($("input[type=checkbox][id=OADel]")[0].checked == true) {
                $("#OADelNodeID_T").show();
                $("#OADelSpan").show();
            }
            if ($("input[type=checkbox][id=OAEdit]")[0].checked == true) {
                $("#OAEditNodeID_T").show();
                $("#OAEditSpan").show();
            }
    })
    function check() {
        var checkArr = $("input[type=checkbox]");
        var checkValue = "";
        var nodeid = "";
        var id = "<%=Request.QueryString["ID"]%>";
        for (var i = 0; i < checkArr.length; i++) {
            if (checkArr[i].checked) {
                checkValue += checkArr[i].value + ",";
            }
        }
        nodeid = $("#OATopNodeID_T").val() + "|" + $("#OADelNodeID_T").val() + "|" + $("#OAEditNodeID_T").val();
        checkValue = checkValue.substring(0, checkValue.length - 1);
        postToCS(checkValue, id, nodeid);
    }
    function postToCS(a, b,c) {
        $.ajax({
            type: "Post",
            url: "CompetenceAdd.aspx",
            data: { Codes: a, ID: b, NodeID: c },
            success: function (data) { if (data == 1) { alert("修改成功"); location = location; } },
            error: function () { alert("修改失败"); }
        });
    }
    function checkall(obj) {
        var name = $(obj).attr("name");
        chkArr = $(obj).parent().find(":checkbox[name=" + name + "]");
        for (var i = 0; i < chkArr.length; i++) {
            chkArr[i].checked = obj.checked;
        }
    }
    function showtext(obj) {
        var str = $(obj).attr("id");
        if (obj.checked) {
            $("#" + str + "NodeID_T").show();
            $("#" + str + "Span").show();
        }
        else {
            $("#" + str + "NodeID_T").hide();
            $("#" + str + "Span").hide();
        }
    }
</script>
</asp:Content>