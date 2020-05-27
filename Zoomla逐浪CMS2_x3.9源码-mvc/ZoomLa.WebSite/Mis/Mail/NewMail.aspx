<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewMail.aspx.cs" Inherits="ZoomLaCMS.MIS.Mail.NewMail"  MasterPageFile="~/Common/Master/Empty.master"%>

<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>新建邮件</title>
<link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />
<script type="text/javascript" src="/js/ajaxrequest.js"></script>
<script>
function loadPage(id, url) {
    $("#" + id).addClass("loader");
    $("#" + id).append("Loading......");
    $.ajax({
        type: "get",
        url: url,
        cache: false,
        error: function () { alert('加载页面' + url + '时出错！'); },
        success: function (msg) {
            $("#" + id).empty().append(msg);
            $("#" + id).removeClass("loader");
        }
    });
}
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="Meno" class="Mis_pad">
<div class="Mis_Title"><strong><a href="javascript:void(0)" onclick="loadPage('Meno', 'NewMail.aspx')">新建邮件</a></strong></div>
<div id="Mail_left" class="left_menu"> 
<ul>
<li><a href="NewMail.aspx">写信</a></li>
<li><a href="Default.aspx">收信</a></li>
<li><a href="MailMenber.aspx">联系人</a></li>
</ul>
<ul><li><a href="Default.aspx">收件箱(141)</a></li>
<li><a href="Default.aspx">草稿箱 </a></li>
<li><a href="Default.aspx">已发邮件 </a></li>
<li><a href="Default.aspx">已删除   </a></li>
<li><a href="Default.aspx">垃圾邮件(86)  </a></li>
<li><a href="Default.aspx">附件管理 </a></li>
<li><a href="Default.aspx">手机短信提醒  </a></li>
</ul>
</div>
<div id="mailMeno" class="Mis_pad rig_main">
<div class="left" style=" float:left; width:650px;">
<strong> 写信</strong><br />
<table>
<tr><th>收件人：</th><td><asp:TextBox ID="txbSendTo" runat="server"></asp:TextBox></td></tr>
<tr><th>主题： </th><td><asp:TextBox ID="txbSubject" runat="server"></asp:TextBox></td></tr>
<tr><th>内容：</th><td><asp:TextBox ID="richtbxBody" runat="server" TextMode="MultiLine" Rows="24" Columns="60"></asp:TextBox></td></tr>
<tr><th valign="top">附件：</th><td><table id="filesDiv" >         </table> 
<span id="" class="filesbg"><a href="javascript:void(0)" onclick="additem('filesDiv')">添加附件</a> 单次上传文件大小不能超过36MB。 </span>
</td></tr>
    <tr><th>发送选项</th> <td ><asp:CheckBox ID="saveSend" runat="server" /> 保存到（已发邮件）<%--  已读回执--%> </td></tr>
</table>
<asp:DropDownList ID="cmbAttachment" runat="server"></asp:DropDownList> 
    <asp:Button ID="Button1" CssClass="i_bottom" OnClick="btnDeleteFile_Click" runat="server" Text="删除"  />  
<%--<asp:Button ID="addfiles" OnClick="btnAddFile_Click" runat="server" Text="添加" />--%>
&nbsp;<asp:Button ID="btnSend" CssClass="i_bottom" runat="server" Text="发送" OnClick="btnSend_Click"></asp:Button><asp:Label ID="lblMsg" runat="server"></asp:Label>
    
<asp:Button ID="btnSave" CssClass="i_bottom" runat="server" Text="存草稿" OnClick="btnSave_Click"></asp:Button>  
&nbsp;<asp:Button ID="btnConcle" CssClass="i_bottom" runat="server" Text="取消" OnClick="btnCancel_Click"></asp:Button> 
</div>
    <div class="rights" style=" float:right; width:130px;">
       内部联系人 外部联系人 
        <ul>
            <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                <ItemTemplate>
                     <li> <asp:LinkButton ID="lkbCon" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="link" Text='<%#Eval("Name") %>'> </asp:LinkButton> </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</div>
<asp:HiddenField runat="server" ID="hfNumber" Value="" />  
    
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script>
var ajax = new AJAXRequest();
var count = 0, number = 0;
var save = "";

function deleteItem(obj, string) {
    //alert("a");
    if (number >= 1) {
        number = number - 1;
        var curRow = obj.parentNode.parentNode;
        var tb3 = document.getElementById("filesDiv");
        var i;
        string = string + ",";
        i = save.indexOf(string);
        saveh1 = save.substr(0, i);
        saveh2 = save.substr(i + 2, save.length - i - 2);
        save = saveh1 + saveh2;
        tb3.deleteRow(curRow.rowIndex);
        document.getElementById("hfNumber").value = save;
    }
}
var str = "";
function additem(id) {
        str =  '<input type="file" name="File" style="width: 300px" runat="server"/>';
    var row, cell, str;
    var tab1 = document.getElementById(id); 
    row = tab1.insertRow(number);
    if (row != null) {
        row.insertCell(0).innerHTML = "<td>"+str+" <a href=\"javascript:void(0)\" class=\"button\"  onclick=\'deleteItem(this," + count + ");\'>删除</a></td>";
        save = save + count + ",";
        count++;
        number++;
    }
    document.getElementById("hfNumber").value = save; 
} 
</script>
</asp:Content>
