<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountForm.aspx.cs" Inherits="ZoomLaCMS.Manage.AccountForm" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>填取表单获得后台访问权限！</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<center style="background:url(http://code.z01.com/user_login.jpg);background-position: center;left:0;top:0;right:0;bottom:0; position: absolute; background-repeat:no-repeat;background-size:cover;">
    <div id="Form_Div" runat="server" class="container userform">
        <div class="panel panel-default">
          <div class="panel-heading"><h2 class="pull-left">申请测试帐号</h2><span class="pull-right"><a href="Login.aspx?hasAccount=1" class="hascount"><span class="fa fa-info-circle"></span> 已有帐号</a></span><div class="clearfix"></div></div>
          <div class="panel-body">
            <p>请您认真填写下方申请表单以便官方客服为您提供周全服务！</p>
            <table class="table" id="userform_table">
                <tr><td class="text-right td_m"><span>姓名</span></td>
                    <td class="text_td"><asp:TextBox runat="server" ID="UserName_T" data-enter="0" CssClass="form-control valid required string" placeholder="用户姓名" MaxLength="20"/></td>
                    <td style="width:126px;"><span class="result_sp">*您登录后台的帐号</span></td></tr>
                <tr><td class="text-right"><span>手机</span></td><td class="text_td"><asp:TextBox runat="server" ID="UserPhone_T" data-enter="1" CssClass="form-control int valid required mobile" placeholder="手机号码" MaxLength="11"/></td><td><span class="result_sp"></span></td></tr>
                <tr><td class="text-right"><span>邮箱</span></td>
                    <td class="text_td"><asp:TextBox runat="server" ID="UserEmail_T" data-enter="2" CssClass="form-control valid required email" placeholder="邮箱地址" MaxLength="50"/></td>
                    <td><span class="result_sp"></span></td></tr>
                <tr id="email_tr"><td></td><td colspan="2">
                     <div class="btn-group" id="email_btns">
                            <a href="javascript:;" class="btn btn-default" style="color:#333;" data-val="@qq.com">QQ</a>
                            <a href="javascript:;" class="btn btn-default" style="color:#333;" data-val="@live.com">Live</a>
                            <a href="javascript:;" class="btn btn-default" style="color:#333;" data-val="@hotmail.com">Hotmail</a>
                            <a href="javascript:;" class="btn btn-default" style="color:#333;" data-val="@126.com">126</a>
                            <a href="javascript:;" class="btn btn-default" style="color:#333;" data-val="@163.com">163</a>
                            <a href="javascript:;" class="btn btn-default" style="color:#333;" data-val="@sina.com">Sina</a>
            <%--                <a href="javascript:;" class="btn btn-default" style="color:#333;" data-val="@outlook.com">OutLook</a>--%>
                        </div>
                </td></tr>
                <tr><td class="text-right"><span>公司</span></td><td class="text_td"><asp:TextBox runat="server" ID="Compay_T" data-enter="3" CssClass="form-control" placeholder="如个人请保留为空" MaxLength="50"/></td><td></td></tr>
                <tr><td class="text-right"><span>所在地</span></td><td class="text_td"><select id="tbProvince" name="tbProvince" class="btn btn-default"></select><select id="tbCity" name="tbCity" class="btn btn-default"></select> <select id="tbCounty" name="tbCounty" class="btn btn-default"></select></td></tr>
                <tr><td class="text-right"><span>QQ号</span></td><td class="text_td"><asp:TextBox runat="server" ID="QQ_T" data-enter="4" CssClass="form-control int valid qq" placeholder="QQ号码" MaxLength="20"/></td><td><span class="result_sp">至少5位数字</span></td></tr>
                <tr><td colspan="3" class="text-center">
                    <asp:Button ID="Submit_B" runat="server" OnClick="Submit_B_Click" CssClass="btn btn-info" Text="提交"  data-enter="5" disabled="disabled"/> 
                    <input type="reset" class="btn btn-info" value="重置" /></td></tr>
            </table>
          </div>
        </div>
    </div>
    <div id="Tip_Div" runat="server" class="container userform" visible="false">
        <div class="panel panel-default">
          <div class="panel-heading" style="font-size:2em;"> 恭喜！</div>
          <div class="panel-body text-center">
          <p> 您已经成功申请，系统审核后会以电子邮件通知您！</p>
              <div><a href="Login.aspx?hasAccount=1" class="btn btn-primary" style="color:#fff;"><i class="fa fa-backward"></i> 返回登录</a>
                  <a href="/" class="btn btn-info" style="color:#fff;"><i class="fa fa-home"></i> 回到首页</a>
              </div>
          </div>
        </div>
    </div>
</center>
<style type="text/css">
#userform_table span {line-height:30px;}
#userform_table tr.check_ok .form-control{border:1px solid #17e217}
#userform_table tr.check_err .form-control {border:1px solid #f31111;}
#userform_table tr .fa-check {color:#17e217;position:absolute;font-size:1.5em;right:15px;top:16px;}
#userform_table tr .fa-remove {color:#f31111;position:absolute;font-size:1.5em;right:15px;top:16px;}
#userform_table .text_td{position:relative;}
#userform_table .result_sp {color:red;}
</style>
<script src="/JS/ICMS/area.js"></script>
<script src="/JS/Controls/ZL_PCC.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script src="/JS/Modal/APIResult.js"></script>
<script src="/JS/Controls/Control.js"></script>
<script>
    if (window.top != window.self) { parent.window.location = window.location; }
    var pcc = new ZL_PCC("tbProvince", "tbCity", "tbCounty");
    $(function () {
        //如有父页面则重定向父页面
        $("body").addClass("cms2login");
        Control.EnableEnter();
        ZL_Regex.B_Num(".int");
        $("#email_btns a").click(function () {
            var text = $("#UserEmail_T").val().split('@')[0] + $(this).data("val");
            $("#UserEmail_T").val(text)
        });
        //-----------------------
        var isAllok = function () {
            var flag = true;
            $(".required").each(function () {
                if (ZL_Regex.isEmpty($(this).val())) { flag = false; }
            })
            if (flag && $("#userform_table .check_err").length < 1) { document.getElementById("Submit_B").disabled = false; }
        }
        var showResult = function ($input, css, msg) {
            var okTlp = '<i class="fa fa-check"></i>';
            var errTlp = '<i class="fa fa-remove"></i>';
            var sub = document.getElementById("Submit_B");
            var $tr = $input.closest("tr");
            $tr.find(".fa-check,.fa-remove").remove();
            $tr.removeClass("check_err").removeClass("check_ok");
            $tr.addClass(css);
            $tr.find(".result_sp").text(msg);
            if (css == "check_ok") {
                $tr.find(".text_td").append(okTlp);
                isAllok();
            }
            else { sub.disabled = true; $tr.find(".text_td").append(errTlp); }
        }
        $(".valid").blur(function () {
            var $input = $(this);
            if ($input.hasClass("required") && ZL_Regex.isEmpty($input.val())) { showResult($input, "check_err", "不能为空"); }
            else if ($input.hasClass("string") && !ZL_Regex.isCharorNum($input.val())) { showResult($input, "check_err", "不能包含特殊符号"); }
            else if ($input.hasClass("mobile") && !ZL_Regex.isMobilePhone($input.val())) { showResult($input, "check_err", "手机号格式错误"); }
            else if ($input.hasClass("email") && !ZL_Regex.isEmail($input.val())) { showResult($input, "check_err", "邮箱格式错误"); }
            else if ($input.hasClass("qq") && !ZL_Regex.isEmpty($input.val()) && $input.val().length <= 5) { showResult($input, "check_err", "QQ格式错误,最少5位数字"); }
            else //特殊规则检测
            {
                if ($input.attr("id") == "UserName_T") {
                    $.post("/API/Mod/admin.ashx?action=regcheck", { name: $input.val() }, function (data) {
                        var model = APIResult.getModel(data);
                        if (APIResult.isok(model)) { showResult($input, "check_ok", ""); }
                        else {
                            showResult($input, "check_err", model.retmsg);
                        }
                    })
                }
                else if ($input.attr("id") == "UserEmail_T")
                {
                    $.post("/API/UserCheck.ashx?action=exist_ue", { uname: $input.val(), email: $input.val() }, function (data) {
                        var model = APIResult.getModel(data);
                        if (APIResult.isok(model)) { showResult($input, "check_ok", ""); }
                        else {
                            showResult($input, "check_err", model.retmsg);
                        }
                    })
                }
                else { showResult($input, "check_ok", ""); }
            }
        });
    });
</script>
</asp:Content>