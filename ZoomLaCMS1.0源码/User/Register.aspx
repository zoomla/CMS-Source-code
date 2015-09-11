<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="ZoomLa.WebSite.User.User_Register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员注册</title>
    <link href="../App_Themes/UserDefaultTheme/Default.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/UserDefaultTheme/xtree.css" type="text/css" rel="stylesheet" />
    <link href="css/user.css" rel="stylesheet" type="text/css" />
    <link href="css/default1.css" rel="stylesheet" type="text/css" />
</head>
<body>    
    <form id="form1" runat="server">
<div id="top_all">
  
  <div id="menu">
    <div id="menubox_pic"> </div>
    <div id="menubox"><a href="/">网站首页</a></div>
  </div>
</div>
<div id="center_all">
    <div class="u_management_bg">
    <div class="c_main_box">
        <asp:Panel ID="PnlRegStep0" runat="server" Visible="false" Width="100%">
            不允许注册，请和网站管理员联系！
        </asp:Panel>
        <asp:Panel ID="PnlRegStep1" runat="server" Visible="false" Width="100%">
            <div style="text-align: center">
                <b>服务条款和声明</b></div>
            <textarea cols="20" rows="2" style="font-weight: normal; font-size: 9pt; width: 98%;
                line-height: normal; font-style: normal; height: 310px; font-variant: normal"
                readonly="readonly"><asp:Literal ID="LitProtocol" runat="server"></asp:Literal></textarea>
            <div style="text-align: center">
                <asp:Button ID="BtnRegStep1" runat="server" Text="同意" OnClick="BtnRegStep1_Click" />
                <asp:Button ID="BtnRegStep1NotApprove" runat="server" Text="不同意" OnClick="BtnRegStep1NotApprove_Click" />
            </div>

            <script type="text/javascript">	
                <!--
                    var secs = 9;
                    var wait = secs * 1000;
                    var btnId = "<%= BtnRegStep1.ClientID %>";
                    document.getElementById(btnId).value = "我同意 [" + secs + "]";
                    document.getElementById(btnId).disabled = true;
                    for(i = 1; i <= secs; i++)
                    {
                        window.setTimeout("Update(" + i + ")", i * 1000);
                    }
                    window.setTimeout("Timer()", wait);
                        			
                    function Update(num)
                    {
                        if(num != secs)
                        {
                            printnr = (wait / 1000) - num;
                            document.getElementById(btnId).value = "我同意 [" + printnr + "]";
                        }
                    }
                        			
                    function Timer()
                    {
                        document.getElementById(btnId).disabled = false;
                        document.getElementById(btnId).value = " 我同意 ";
                    }
                    
                    
                //-->
            </script>

        </asp:Panel>
        <asp:Panel ID="PnlRegStep2" runat="server" Visible="false" Width="100%">
            <script type="text/javascript">
                    function CheckUser()
                    {
                        var userName = document.getElementById("<%= TxtUserName.ClientID %>");
                        var checkUserNameMessage = document.getElementById("CheckUserNameMessage");                        
                        if(userName.value=="")
                        {
                            checkUserNameMessage.innerHTML = "用户名为空";
                            checkUserNameMessage.className = "d_err";
                        }
                        else
                        {
                            CallTheServer(userName.value,"");
                        }
                    }
                    
                    function CallTheServer(arg,context)
                    {
                        var checkUserNameMessage = document.getElementById("CheckUserNameMessage");
                        checkUserNameMessage.className = "";
                        checkUserNameMessage.innerHTML = "<img src=\"images/loading.gif\" align=\"absmiddle\" />";                        
                        <%= CallBackReference %>
                    }
                    
                    function ReceiveServerData(result)
                    {
                        var checkUserNameMessage = document.getElementById("CheckUserNameMessage");
                        if(result == "true")
                        {
                            checkUserNameMessage.innerHTML = "用户名已经被注册了";
                            checkUserNameMessage.className = "d_err";
                        }
                        
                        if(result == "disabled")
                        {
                            checkUserNameMessage.innerHTML = "该用户名禁止注册";
                            checkUserNameMessage.className = "d_err";
                        }
                        
                        if(result == "false")
                        {
                            checkUserNameMessage.innerHTML = "恭喜您，用户名可以使用！";
                            checkUserNameMessage.className = "d_ok";
                        }
                    }
                    
                    
                    
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                    function EndRequestHandler(sender, args)
                    {
                        if (args.get_error() != undefined){
                        alert("检测到表单中存在HTML代码！");
                        args.set_errorHandled(true);
                        }
                    }
            </script>

            <table id="TableRegisterMust" runat="server" style="border-collapse: collapse" cellspacing="1"
                cellpadding="2" width="100%" border="0">
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                        <b>用户名：</b>
                    </td>
                    <td valign="middle" style="width: 221px">
                        <asp:TextBox ID="TxtUserName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="ReqTxtUserName" runat="server" ControlToValidate="TxtUserName"
                            SetFocusOnError="false" ErrorMessage="用户名不能为空" Display="None"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="ValgTextMaxLength" ControlToValidate="TxtUserName"
                            ValidationExpression="^[a-zA-Z0-9]{4,20}$" SetFocusOnError="false"
                            Display="None" runat="server"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                    </td>
                    <td valign="middle" style="width: 85%">
                        <input id="CheckUserName" style="float: left;" type="button" value="检查用户名是否可用" onclick="CheckUser()" /><span
                            class="d_default" id="CheckUserNameMessage"></span>
                    </td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                        <b>密码：</b>
                    </td>
                    <td valign="middle" style="width: 221px">
                        <asp:TextBox ID="TxtPassword" TextMode="Password" runat="server"></asp:TextBox>                        
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" runat="server"
                            ControlToValidate="TxtPassword" SetFocusOnError="false" Display="None" ValidationExpression="[\S]{6,}"
                            ErrorMessage="密码至少6位"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="ReqTxtPassword" runat="server" ControlToValidate="TxtPassword"
                            SetFocusOnError="false" Display="None" ErrorMessage="密码不能为空"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                        <strong>确认密码：</strong>
                    </td>
                    <td style="width: 221px">
                        <asp:TextBox ID="TxtPwdConfirm" TextMode="Password" runat="server"></asp:TextBox>                        
                        <asp:RequiredFieldValidator ID="ReqTxtPwdConfirm" runat="server" ControlToValidate="TxtPwdConfirm"
                            SetFocusOnError="false" Display="None" ErrorMessage="确认密码不能为空"></asp:RequiredFieldValidator><asp:CompareValidator
                                ID="ValCompPassword" runat="server" ControlToValidate="TxtPwdConfirm" ControlToCompare="TxtPassword"
                                Operator="Equal" SetFocusOnError="false" Display="None" ErrorMessage="两次密码输入不一致"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                        <strong>密码问题：</strong>
                    </td>
                    <td style="width: 221px">
                        <asp:TextBox ID="TxtQuestion" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="ReqTxtQuestion" runat="server" ControlToValidate="TxtQuestion"
                            SetFocusOnError="false" Display="None" ErrorMessage="密码问题不能为空"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                        <strong>问题答案：</strong><br />
                    </td>
                    <td style="width: 221px">
                        <asp:TextBox ID="TxtAnswer" runat="server"></asp:TextBox>                        
                        <asp:RequiredFieldValidator ID="ReqAnswer" runat="server" ControlToValidate="TxtAnswer"
                            SetFocusOnError="false" Display="None" ErrorMessage="问题答案不能为空"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                        <strong>Email地址：</strong>
                    </td>
                    <td style="width: 221px">
                        <asp:TextBox ID="TxtEmail" runat="server"></asp:TextBox>                        
                        <asp:RequiredFieldValidator ID="ReqTxtEmail" runat="server" ControlToValidate="TxtEmail"
                            SetFocusOnError="false" Display="None" ErrorMessage="Email不能为空"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtEmail"
                            ErrorMessage="邮件地址不规范" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"></asp:RegularExpressionValidator></td>
                </tr>
            </table>            
            <table style="border-collapse: collapse" cellspacing="1" cellpadding="2" width="100%"
                border="0">
                <tr class="tdbgleft" style="width: 15%" align="center">
                    <td colspan="2" style="height: 30px;">
                        <span style="color: #ff0000">以上所有信息都必须先正确填写后才能成功注册。</span></td>
                </tr>
                <tr class="tdbgleft" style="width: 15%" align="center">
                    <td style="height: 30px;" colspan="2">
                        <asp:Button ID="BtnSubmit" runat="server" Text="提交" OnClick="BtnSubmit_Click" />
                        <input id="Reset" type="reset" value=" 重新填写 " name="Reset" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" />
                    </td>
                </tr>
            </table>            
        </asp:Panel>
    </div>
    </div> 
</div>       
    </form>
</body>
</html>
