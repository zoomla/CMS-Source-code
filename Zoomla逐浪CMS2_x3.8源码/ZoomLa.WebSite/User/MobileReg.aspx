<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileReg.aspx.cs" Inherits="User_MobileReg" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>手机注册</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div runat="server" id="step1_div" visible="false">
        <ul class="list-unstyled infoul">
            <li>
                <asp:TextBox runat="server" ID="Mobile_T" CssClass="form-control num" placeholder="手机号" />
                <asp:RequiredFieldValidator runat="server" ID="rg3" Display="Dynamic" ForeColor="Red" ControlToValidate="Mobile_T" ErrorMessage="手机号不能为空" />
                <asp:RegularExpressionValidator ID="rg4" Display="Dynamic" ForeColor="Red" runat="server" ControlToValidate="Mobile_T" ErrorMessage="请输入正确的手机号码" ValidationExpression="^1\d{10}$" />
            </li>
            <li>
                <div class="input-group">
                    <asp:TextBox runat="server" ID="VCode_T" CssClass="form-control" MaxLength="6" placeholder="验证码" />
                    <span class="input-group-btn">
                        <input type="button" value="获取验证码" class="btn btn-info" onclick="SendMsg(this);" />
                    </span>
                </div>
                <asp:RequiredFieldValidator runat="server" ID="RM1" ForeColor="Red" ControlToValidate="VCode_T" ErrorMessage="验证码不能为空" Display="Dynamic" />
            </li>
            <li class="text-center">
                <asp:Button runat="server" ID="Next_Btn" Text="验证手机" OnClick="Next_Btn_Click" CssClass="btn btn-primary" />
            </li>
        </ul>
    </div>
    <div runat="server" id="step2_div" visible="false">
        <ul class="list-unstyled infoul">
            <li>
                <asp:TextBox runat="server" ID="UName_T" CssClass="form-control" placeholder="用户名" />
                <asp:RegularExpressionValidator ID="RU1" runat="server" ControlToValidate="UName_T" ErrorMessage="不能包含特殊字符" ValidationGroup="userVaid" ValidationExpression="^[^@#$%^&*()'?{}\[\];:]*$" Display="Dynamic" ForeColor="Red" />
                <asp:RequiredFieldValidator ID="RU2" runat="server" ControlToValidate="UName_T" ErrorMessage="用户名不能为空" ValidationGroup="userVaid" Display="Dynamic" ForeColor="Red" />
            </li>
            <li>
                <asp:Label runat="server" ID="Mobile_L"></asp:Label>
            </li>
            <li>
                <asp:TextBox runat="server" ID="Email_T" CssClass="form-control" placeholder="邮箱" />
                <asp:RegularExpressionValidator ID="RE1" runat="server" ControlToValidate="Email_T" Display="Dynamic" ForeColor="Red"
                    ErrorMessage="邮箱地址不规范" ValidationGroup="userVaid" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" />
            </li>
            <li>
                <asp:TextBox runat="server" ID="Passwd_T" CssClass="form-control" placeholder="密码" TextMode="Password" />
                <asp:RegularExpressionValidator runat="server" ID="RP3" ControlToValidate="Passwd_T"
                    ValidationExpression="[\S]{6,}" ValidationGroup="userVaid" ErrorMessage="密码至少6位" Display="Dynamic" ForeColor="Red" />
                <asp:RequiredFieldValidator runat="server" ID="RP4" ControlToValidate="Passwd_T"
                    ValidationGroup="userVaid" ErrorMessage="密码不能为空" Display="Dynamic" ForeColor="Red" />
            </li>
            <li>
                <asp:TextBox runat="server" ID="ConfirmPasswd_T" CssClass="form-control" placeholder="确认密码" TextMode="Password" />
                <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="ConfirmPasswd_T" ValidationGroup="userVaid" Display="Dynamic" ErrorMessage="确认密码不能为空" ForeColor="Red" />
                <asp:CompareValidator ID="req2" runat="server" ControlToValidate="ConfirmPasswd_T" ControlToCompare="Passwd_T"
                    Operator="Equal" ValidationGroup="userVaid" ErrorMessage="两次密码输入不一致" Display="Dynamic" ForeColor="Red" />
            </li>
            <li>
                <input type="button" class="btn btn-primary" value="注册用户" onclick="PreCheck();" />
                <asp:Button runat="server" OnClick="Sure_Btn_Click" ID="Sure_Btn" ValidationGroup="userVaid" style="display:none;"/>
            </li>
        </ul>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <style type="text/css">
        .infoul {width:300px;margin:0 auto;}
        .infoul li {margin-bottom:3px;}
    </style>
    <script src="/JS/Modal/APIResult.js"></script>
    <script src="/JS/ZL_Regex.js"></script>
    <script src="/JS/ZL_ValidateCode.js"></script>
    <script>
        var inter = null;
        $(function () {
            ZL_Regex.B_Num("num");
            //$("form").validate({});
            //$.validator.addMethod("phones", function (value) {
            //    return ZL_Regex.isMobilePhone(value);
            //}, "请输入正确的手机号码!");
        })
        function WaitFor(obj, time) {
            var text = obj.value;
            setTimeout(function () { obj.disabled = true; }, 50);
            inter = setInterval(function () { time = time - 1; obj.value = "请等待(" + time + ")秒"; }, 1000);
            setTimeout(function () { obj.disabled = false; clearInterval(inter); obj.value = text; }, time * 1000);
        }
        function SendMsg(obj) {
            var mobile = $("#Mobile_T").val();
            if (!ZL_Regex.isMobilePhone(mobile)) { alert("手机号码不正确"); return; }
            WaitFor(obj, 30);
            $.post("/API/Mod/Mobile.ashx?action=mobilereg", { "mobile": mobile }, function (data) {
                var model = APIResult.getModel(data);
                if (APIResult.isok(model)) { console.log(model.retmsg); }
                else { alert(model.retmsg); }
            })
        }
        function PreCheck() {
            //用户名或邮箱是否已存在
            $.post("/API/UserCheck.ashx?action=exist_ue", { email: $("#Email_T").val(), uname: $("#UName_T").val() }, function (data) {
                var model = APIResult.getModel(data);
                if (APIResult.isok(model)) { $("#Sure_Btn").click(); }
                else { alert(model.retmsg); }
            })
        }
    </script>
</asp:Content>
