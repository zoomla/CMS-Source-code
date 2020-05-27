<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Manage.Config.Default" %>
<!DOCTYPE html>
<html>
<head runat="server">
<title>短信发送</title>
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
            var data = "x_eid=" + s_eid + "&x_uid=" + s_uid + "&x_pwd_md5=" + s_pwd + "&x_gate_id=300&x_ac=10&x_target_no=" + s_sendno + "&x_memo=" + s_memo + "&x_send_time=" + s_time ;
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
//                svar money = "x_eid=" + g_eid + "&x_uid=" + g_uid + "&x_pwd_md5=" + g_pwd + "&x_ac=30" + "&x_gate_id" + g_gate_id;
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
    <form id="form1" runat="server">
    <div>
    <table width="100%" border="0" cellpadding="3" cellspacing="1" >
  <tr class="tdbg">
        <td>发送短信
            <input id="x_eid" type="hidden" value="" runat="server" /><!--企业ID代码-->
            <input id="x_uid" type="hidden" value="" runat="server"/><!--会员登录账户-->
            <input id="x_pwd_md5" type="hidden"  value="" runat="server"/><!--登录密码-->
            <input id="h_gate_id" type="hidden" value="" runat="server"/><!--使用通道的ID-->
            <input id="txtContent" type="hidden" value="" runat="server"/><!--短信内容-->
        </td>
      </tr>
  <tr>
    <td><table width="100%" border="0" cellpadding="1" cellspacing="1">
      <tr class="tdbg">
        <td bgcolor="#FFFFFF">
            <table width="100%" border="0" cellpadding="1" cellspacing="1">
          <tr class="tdbg">
            <td class="style4" align="right">目标号码:</td>
            <td  width="100%">
                &nbsp;<asp:TextBox ID="t_sendNo" runat="server" Width="472px" Height="20px"></asp:TextBox>
                (多个号码用[,]逗号分开)
              </td>
          </tr>
      <tr class="tdbg">
        <td align="right" class="style4">发送时间:</td>
        <td colspan="2">
            &nbsp;<asp:TextBox ID="t_sendTime" runat="server" Height="22px"></asp:TextBox>(立即发送.为空.定时发送,请输入发送时间)例如:2008-08-08 12:20:00 </td>     
      <tr class="tdbg">
        <td align="right" class="style4">发送内容:</td>
        <td colspan="2">
            <asp:TextBox ID="t_sendMemo" runat="server" TextMode="MultiLine" Height="150px" Width="427px"></asp:TextBox>
          (每条短信内容长度要求请参考通道说明)</td>
      </tr>
      <tr>
        <td align="right">&nbsp;</td>
        <td colspan="2"><asp:Button ID="Button1" class="C_input" runat="server" Text="发送"  Width="56px" OnClientClick="return sendsms();" OnClick="Button1_Click" />
          <input type="reset"  class="C_input" name="Submit2" value="重置" style="width: 57px"/>
            <asp:Label ID="Label1" runat="server" Text="Label" Width="139px"></asp:Label></td>
      </tr>     
    </table></td>
  </tr>
  <tr class="tdbg">
    <td ><span class="STYLE1">接收短信
        <asp:Button ID="Button2" class="C_input" runat="server" Text="读取短信"  OnClick="Button2_Click" /></span></td>
  </tr>
  <tr class="tdbg">
    <td bgcolor="#FFFFFF"><div id="div_sms" runat="server"> </div>
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  
                CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" 
                OnPageIndexChanging="EGV_PageIndexChanging" >        
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
            <asp:TemplateField HeaderText="回复者号码">
            <ItemTemplate>
            <%#Eval("SenderNo") %>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="内容">
            <ItemStyle  Width="50%"/>
            <ItemTemplate>
            <%#Eval("MsgContent") %>
            </ItemTemplate>
            </asp:TemplateField>
                        
            <asp:TemplateField HeaderText="发送时间">
            <ItemTemplate>
            <%#Eval("SendTime") %>
            </ItemTemplate>
            </asp:TemplateField>
                        
            <asp:TemplateField HeaderText="回复运营商SP">
            <ItemTemplate>
            <%#Eval("SP_PID") %>
            </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <RowStyle Height="24px" HorizontalAlign="Center"  />
</ZL:ExGridView>
   </td>
  </tr>
</table>
    </div>
    </form>
</body>
</html>
