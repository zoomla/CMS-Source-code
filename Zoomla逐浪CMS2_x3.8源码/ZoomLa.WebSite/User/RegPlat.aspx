<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegPlat.aspx.cs" Inherits="User_RegPlat" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>用户注册</title>
<link href="/App_Themes/User.css" rel="stylesheet">
<style type="text/css">
ul{list-style-type:none;}
.detail{border:none;width:300px;height:34px;border-top:1px solid #ddd;padding-left:7px;vertical-align:middle;}
body { background: url(https://code.z01.com/web/plat/reg-bg.jpg) center no-repeat;background-color:#000; }
body h1 { margin-top: 20%; margin-bottom: 1em; }
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container-fluid">
<div class="row text-center">
<div class="col-lg-12  col-md-12 col-sm-12 col-xs-12">
<div style="display:none" id="MaskLayer_Div"></div>  
<h1 class="regplat_title" style="color:#fff;">连接世界的力量</h1>
    <div runat="server" visible="false" id="Step1_Div">
    <asp:TextBox runat="server" ID="Email_T" placeholder="推荐使用企业邮箱" type="email" class="form-control" />
    <asp:Button runat="server" ID="Step1_Btn" Text="点击申请" OnClientClick="disBtn(this,3000);" OnClick="Step1_Btn_Click" CssClass="btn btn-primary" ValidationGroup="sg1" />
    <a href="/User/Login.aspx"><i class="fa fa-angle-left"></i>返回登录</a><br />
        <asp:RequiredFieldValidator runat="server" ID="s1" ControlToValidate="Email_T" Display="Dynamic" ErrorMessage="邮箱不能为空" ForeColor="Red" Font-Size="16" ValidationGroup="sg1" />
        <asp:RegularExpressionValidator runat="server" ID="s2" ControlToValidate="Email_T" Display="Dynamic" ErrorMessage="邮箱格式不正确" ForeColor="Red" Font-Size="16" ValidationGroup="sg1" />
    </div>
    <div runat="server" id="Step2_Div" style="background-color:white;width:600px;border-radius:5px;position:absolute;top:40%;left:30%;padding-left:20px;padding:20px 20px 20px 20px;z-index:2;display:none;">
        <div style="position:absolute;right:-20px;top:-20px;cursor:pointer;" title="关闭" onclick="closeme();" ><span class="fa fa-remove" style="color:white;"></span></div>
        <div style="padding:0 0 20px 0;"><span>我们已经发送注册邮件到您的邮箱，请点击邮件中的激活链接完成注册。</span></div>
        <div style="padding:0 0 20px 0;">
            <img src="/App_Themes/AdminDefaultTheme/PromptSkin/images/right.gif" /><span style="font-size:20px;font-weight:bold;margin-left:10px;">请验证您的邮箱完成注册...</span></div>
        <div>
            <asp:HyperLink runat="server" Text="前往邮箱激活注册" CssClass="btn btn-primary" Target="_blank" ID="MailSite_A"  />
            <span style="float:right;margin-top:15px;"><a href="#">重发邮件?</a><a href="#" style="margin-left:10px;">收不到邮件?</a></span></div>
    </div>
    <div runat="server" id="Step3_Div" visible="false"> 
        <ul class="detail_ul">
            <li class="descli" style="color:white;"><span class="fa fa-user"></span> <asp:Label runat="server" ID="UserName_L" /></li>
            <li><asp:TextBox runat="server" ID="TrueName_T" placeholder="真名" CssClass="detail" />
                <asp:RequiredFieldValidator runat="server" ID="tr1" Display="Dynamic"  ForeColor="Red" ErrorMessage="真名不能为空" ControlToValidate="TrueName_T" ValidationGroup="reg" />
            </li>
           <%-- <li runat="server" visible="false" id="compli"><asp:TextBox runat="server" ID="CompName_T" placeholder="企业名称" CssClass="detail" /></li>--%>
            <li><asp:TextBox runat="server" ID="Post_T" placeholder="职位" CssClass="detail"  /></li>
            <li><asp:TextBox runat="server" ID="Mobile_T" placeholder="手机" CssClass="detail" style="border-bottom:1px solid #ddd;" /><br />
                <asp:RegularExpressionValidator runat="server" ID="m1" ForeColor="Red" ErrorMessage="手机格式不正确" ValidationExpression="^1\d{10}$" Display="Dynamic" ControlToValidate="Mobile_T" ValidationGroup="reg"/>
            </li>
            <li style="padding-bottom:10px;"></li>
            <li><asp:TextBox runat="server" ID="Pwd_T" placeholder="密码" CssClass="detail" TextMode="Password"  /><br />
                <asp:RequiredFieldValidator runat="server" ID="pv1" Display="Dynamic"  ForeColor="Red" ErrorMessage="密码不能为空" ControlToValidate="Pwd_T" ValidationGroup="reg" />
                <asp:RegularExpressionValidator runat="server" ID="pv2" ValidationExpression="^((.){6,15}$)" Display="Dynamic"  ForeColor="Red" ErrorMessage="密码最小6位，最大15位！" ControlToValidate="Pwd_T" ValidationGroup="reg" />
            </li>
            <li><asp:TextBox runat="server" ID="CPwd_T" placeholder="确认密码" CssClass="detail" TextMode="Password" style="border-bottom:1px solid #ddd;"/><br />
                <asp:RequiredFieldValidator runat="server" ID="v1" Display="Dynamic"  ForeColor="Red" ErrorMessage="确认密码不能为空" ControlToValidate="CPwd_T" ValidationGroup="reg" />
                <asp:CompareValidator runat="server" ID="v2" ForeColor="Red" ErrorMessage="密码与确认密码不匹配" Display="Dynamic" ControlToCompare="Pwd_T" ControlToValidate="CPwd_T" ValidationGroup="reg" />
            </li>
            <li style="padding-bottom:10px;"></li>
            <li style="padding-bottom:10px;"><span><label style="color:#fff;"><input type="checkbox" style="margin-right:5px;height:auto;width:auto;" checked="checked" />阅读并同意</label><a href="#" title="使用条款">《使用条款》</a></span></li>
            <li><asp:Button runat="server" ID="Sub_Btn" Text="加入能力中心" OnClientClick="disBtn(this,2000);" OnClick="Sub_Btn_Click" CssClass="btn btn-info text_300" ValidationGroup="reg"/></li>
        </ul> 
    </div>
</div>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script>
    function showme() {
        $("#Step2_Div").show();
        ShowMask();
    }
    function closeme() {
        $("#Step2_Div").hide();
        $("#MaskLayer_Div").hide();
    }
    function ShowMask() {
        var sw = $(window).width();
        var sh = $(window).height();
        $("#MaskLayer_Div").css({ "display": "", "position": "fixed", "background": "#000", "z-index": "1", "-moz-opacity": "0.5", "opacity": ".50", "filter": "alpha(opacity=50)", "width": sw, "height": sh });
    }
</script>
</asp:Content>