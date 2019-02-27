<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageInfo.aspx.cs" Inherits="manage_Config_MessageInfo"  EnableViewStateMac="false"%>
<!DOCTYPE html>
<html>
<head runat="server">
<title>发送消息</title>
<link rel="stylesheet" type="text/css" href="../../App_Themes/AdminDefaultTheme/style.css" />
<script type="text/javascript">
    var xmlReq;
    function sendsms() {
        var s_sendno = document.getElementById("t_sendNo").value;
        var s_memo = document.getElementById("t_sendMemo").value;

        if (s_sendno == "") {
            alert("请输入对方手机号码!");
            document.getElementById("t_sendNo").focus();
            return;
        }
        if (s_memo == "") {
            alert("请输入发送内容!");
            document.getElementById("t_sendMemo").focus();
            return;
        }
        document.form1.submit();
        setTimeout(sub, 10000); //以毫秒为单位的.1000代表一秒钟.
        try {
            if (window.ActiveXObject) {
                xmlReq = new ActiveXObject("Microsoft.XMLHTTP");
            }
            else if (window.XMLHttpRequest) {
                xmlReq = new XMLHttpRequest();
            }
        }
        catch (e) {
            xmlReq = null;
            alert(e);
            return;
        }

        try {
            var s_eid = document.getElementById("x_eid").value;
            var s_uid = document.getElementById("x_uid").value;
            var s_pwd = document.getElementById("x_pwd_md5").value;
            var s_time = document.getElementById("t_sendtime").value;
            var x_gate_id = document.getElementById("h_gate_id").value;
            var data = "x_eid=" + s_eid + "&x_uid=" + s_uid + "&x_pwd_md5=" + s_pwd + "&x_gate_id=300&x_ac=10&x_target_no=" + s_sendno + "&x_memo=" + s_memo + "&x_send_time=" + s_time;
            var url = "http://gateway.woxp.cn:6630/utf8/web_api/?" + data;
            xmlReq.open("GET", url, true);
            xmlReq.onreadystatechange = resHandler;         //设置返回值处理函数
            xmlReq.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            xmlReq.send();
            document.getElementById("btn_send").disabled = true;
        }
        catch (e) {
            alert(e);
        }
    }

    //接收短信
    function Resms() {
        try {
            if (window.ActiveXObject) {
                xmlReq = new ActiveXObject("Microsoft.XMLHTTP");
            }
            else if (window.XMLHttpRequest) {
                xmlReq = new XMLHttpRequest();
            }
        }
        catch (e) {
            xmlReq = null;
            alert(e);
            return;
        }
        try {
            var s_eid = document.getElementById("x_eid").value;
            var s_uid = document.getElementById("x_uid").value;
            var s_pwd = document.getElementById("x_pwd_md5").value;
            var s_time = document.getElementById("t_sendtime").value;
            var x_gate_id = document.getElementById("h_gate_id").value;
            var data = "x_eid=" + s_eid + "&x_uid=" + s_uid + "&x_pwd_md5=" + s_pwd + "&x_ac=30" + "&x_gate_id" + g_gate_id;
            var url = "http://gateway.woxp.cn:6630/utf8/web_api/?" + data;
            xmlReq.open("POST", url, true);
            xmlReq.onreadystatechange = RecSMS;         //设置返回值处理函数
            xmlReq.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            xmlReq.send(data);
            document.getElementById("btn_send2").disabled = true;
        }
        catch (e) {
            alert(e);
        }

    }
    function LoadXML(xmlstr) {
        var out = "<table width=\"100%\" border=\"0\" cellpadding=\"2\" cellspacing=\"1\" bgcolor=\"#BAC5D9\"><tr>    <td width=\"21%\" height=\"27\" align=\"center\" bgcolor=\"#ECEFF4\">回复者号码</td>    <td width=\"44%\" height=\"27\" align=\"center\" bgcolor=\"#ECEFF4\">回复内容</td>    <td width=\"18%\" height=\"27\" align=\"center\" bgcolor=\"#ECEFF4\">回复时间</td>    <td width=\"17%\" height=\"27\" align=\"center\" bgcolor=\"#ECEFF4\">回复运营商号码</td></tr>";
        var t_tr = "";
        var t_tr_all = "";
        var xml = new ActiveXObject("Microsoft.XMLDOM");
        if (xml == null || xmlstr == '') {
            document.getElementById("div_sms").innerHTML = '<font color=red>接收失败,请重试!</font>';
            return;
        }
        if (xmlstr == '<NoData/>') {
            document.getElementById("div_sms").innerHTML = '<font color=red>没有回复!</font>';
            return;
        }
        try {
            xml.loadXML(xmlstr);
            var bi = xml.documentElement.selectNodes("Item");
        }
        catch (e) {
            alert(e);
            return;
        }
        for (i = 0; i < bi.length; i++) {
            t_tr = "<tr height=\"20\">";
            t_tr += "<td  align=\"center\" bgcolor=\"#FFFFFF\">" + bi[i].childNodes[0].text + "</td>";
            t_tr += "<td  align=\"left\" bgcolor=\"#FFFFFF\">" + bi[i].childNodes[1].text + "</td>";
            t_tr += "<td  align=\"center\" bgcolor=\"#FFFFFF\">" + bi[i].childNodes[2].text + "</td>";
            t_tr += "<td  align=\"center\" bgcolor=\"#FFFFFF\">" + bi[i].childNodes[3].text + "</td></tr>";
            t_tr_all += t_tr;
        }
        out = out + t_tr_all + '</table>';
        document.getElementById("div_sms").innerHTML = out;
    }
    function RecSMS() {
        if (xmlReq.readystate == 4) {
            if (isNaN(xmlReq.responseText)) {
                //		alert(xmlReq.responseText);		
                //			document.getElementById("div_sms").innerText=xmlReq.responseText;
                LoadXML(xmlReq.responseText);
            }
            else {
                alert("接收短信失败,代码:" + xmlReq.responseText);
            }
        }
        document.getElementById("btn_send2").disabled = false;
    }
    //处理结果
    function resHandler() {
        //alert(xmlReq.readystate);
        if (xmlReq.readystate == 4) {
            var flg = parseInt(xmlReq.responseText);
            if (flg > 0) {
                alert("发送成功");
                
            }
            else {
                alert("发送失败,代码:" + flg);
            }
        }
        document.getElementById("btn_send").disabled = false;
    }
</script>
</head>
<body>
<form id="form1" runat="server" action="">
  <div>
    <input id="x_eid" type="hidden" value="" runat="server" />
    <!--企业ID代码-->
    <input id="x_uid" type="hidden" value="" runat="server"/>
    <!--会员登录账户-->
    <input id="x_pwd_md5" type="hidden"  value="" runat="server"/>
    <!--登录密码-->
    <input id="h_gate_id" type="hidden" value="" runat="server"/>
    <!--使用通道的ID-->
    <input id="txtContent" type="hidden" value="" runat="server"/>
    <!--短信内容-->
    <input id="txtType" type="hidden" value="" runat="server"/>
    <!--短信类型-->
    <table align="center" class="style1">
      <tr>
        <td class="style2">手机号</td>
        <td><asp:TextBox ID="t_sendNo" runat="server"></asp:TextBox></td>
      </tr>
      <tr>
        <td class="style2"> 短信内容</td>
        <td><asp:TextBox ID="s_memo" runat="server" TextMode="MultiLine"></asp:TextBox></td>
      </tr>
      <tr>
        <td class="style2"><asp:Button ID="Button1" class="C_input" runat="server" Text="发送" 
                        OnClientClick="return sendsms();" onclick="Button1_Click"/></td>
        <td>&nbsp;</td>
      </tr>
    </table>
  </div>
</form>
</body>
</html>