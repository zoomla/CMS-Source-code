<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditProcedure.aspx.cs" Inherits="ZoomLaCMS.MIS.Approval.EditProcedure" MasterPageFile="~/Common/Master/Empty.master"  %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>流程编辑</title>
<link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />
<script type="text/javascript">
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
function putid(id) {
document.getElementById("TitComType").innerText = "添加类型";
document.getElementById("HidCommTxt").value = "0";
document.getElementById("TxtComment").value = "";
document.getElementById("CommentDiv").style.display = "block";
var h = document.getElementById("divcon").getBoundingClientRect().top;
document.getElementById("CommentDiv").style.top = "200px";
document.getElementById("CommentDiv").style.background = "#fdfce6";
}

function Punids(id, Cont) {
document.getElementById("TitComType").innerText = "修改类型";
document.getElementById("CommentDiv").style.display = "block";
var h = document.getElementById("divcon" + id).getBoundingClientRect().top;
document.getElementById("HidCommTxt").value = id;
document.getElementById("TxtComment").value = Cont;
document.getElementById("CommentDiv").style.top = (h - 126) + "px";
document.getElementById("CommentDiv").style.background = "#fdfce6";
}

var save = "";
        
function AddSignRow() {
//读取最后一行的行号，存放在txtTRLastIndex文本框中 
var txtTRLastIndex = document.getElementById("txtTRLastIndex");
            
var rowID = parseInt(txtTRLastIndex.value);
//添加行
var tables = document.getElementById("TabFoots");
var newTR = tables.insertRow(tables.rows.length);
newTR.id = "SignItem" + rowID;
//添加列:序号
var newNameTD =newTR.insertCell(0);
//添加列内容
newNameTD.innerHTML = ""+rowID+"<input name='txtNum" + rowID + "' id='txtNum" + rowID + "' type='hidden' size='6' value='" + rowID + "' />"
 
//添加列:级别名
var newLevelTD=newTR.insertCell(1);
//添加列内容
newLevelTD.innerHTML = "<input style=\"width:200px;height:22px;border:1px solid #ccc;\" name='txtName" + rowID + "' id='txtName" + rowID + "' type='text' size='12' value='第" + rowID + "级审批' />";
//添加列:审批人
var newPesonTD=newTR.insertCell(2);
//添加列内容
newPesonTD.innerHTML = "<label id='lblPeson" + rowID + "'></label><a href='javascript:;' onclick=\"PopupDiv('div_share');setEdit('lblPeson" + rowID + "','txtPeson" + rowID + "');\">[选择]</a><input name='txtPeson" + rowID + "' id='txtPeson" + rowID + "' type='hidden'  />";
//添加列:抄送给
var newPesonTD = newTR.insertCell(3);
//添加列内容
newPesonTD.innerHTML = "<label id='lblSend" + rowID + "'></label><a href='javascript:;' onclick=\"PopupDiv('div_share');setEdit('lblSend" + rowID + "','txtSend" + rowID + "')\">[选择]</a><input name='txtSend" + rowID + "' id='txtSend" + rowID + "' type='hidden' />";

//添加列:删除按钮
var newDeleteTD = newTR.insertCell(4);

txtTRLastIndex.value = (rowID + 1).toString();
//添加列内容
newDeleteTD.innerHTML = "<a href='javascript:;' OnClientClick='return confirm('确定要删除该条记录么？')'  onclick='DeleteSignRow(" + rowID + ")'>[删除]</a>";
//将行号推进下一行
save = save + rowID+",";
document.getElementById("HiddenField1").value = save;
}
//删除指定行
function DeleteSignRow(rowid) {
var signFrame = document.getElementById("TabFoots");
var signItem = document.getElementById("SignItem"+rowid+"");
//获取将要删除的行的Index
var rowIndexs = signItem.rowIndex;
//删除指定Index的行
signFrame.deleteRow(rowIndexs);
//重新排列序号，如果没有序号，这一步省略
for (i = rowIndexs; i < signFrame.rows.length; i++) {
    signFrame.rows.cells[0].innerHTML = i.toString();
  }
}

function initData() {
var tb = document.getElementById("TabFoots");
var data = [];
for (var i = 1; i < tb.rows.length; i++) {
    if (tb.rows[i].cells[0].value.length > 0 && tb.rows[i].cells[1].value.length > 0)       { //将两个文本框里值不为空的项添加到数组,假设只有前两列的数据不能为空
        data.push(tb.rows[i].cells[0].value);
        data.push(tb.rows[i].cells[1].value);
        data.push(tb.rows[i].cells[2].value);
        data.push(tb.rows[i].cells[3].value);
    } //else {这里可以给个提示说数据没填完整，是否提交，否则return false}
}
document.getElementById("HiddenField1").value = data.join("`");//使用这个比较不常用 的字符将数组拼接成字符串
return true;
};
$(window).load(function () {
    var row = document.getElementById("txtTRLastIndex").value;
    if (row=null||row <= 0){
    AddSignRow();
 }
});
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="Meno">
<div id="pro_left">
<div class="new_tar">
<a href="AddApproval.aspx">新建申请单</a></div>
<div class="pro_li">
<ul>
<li><a href="Default.aspx?type=4">我的审批</a></li>
<li><a href="Default.aspx?type=2">已归档</a></li>
<li><a href="Default.aspx?type=8">抄送给我的</a></li>
<li><a href="Default.aspx?type=7">审批设置</a></li>
</ul>
</div>
</div>

<div id="pro_right">
<div>
<div id="Meno_Title"><a href="Default.aspx?type=7">流程设置</a>>>流程编辑</div>
<div id="Procedure">
<div class="PType">流程类型:<asp:DropDownList ID="DrpType" runat="server"></asp:DropDownList><a href="javascript:;" onclick="putid('CommentDiv');">添加类型</a><div id="divcon"></div></div>
<div class="PName">
    <span>*</span>流程名称:
    <asp:TextBox ID="TxtName" runat="server" ></asp:TextBox>
    <asp:Label ID="lblprompt" runat="server"></asp:Label>
</div>
<div class="BtnPAdd">
    <input type="button" class="i_bottom" value="添加级别" onclick="AddSignRow()" />
    <input name='txtTRLastIndex' type='hidden' runat="server" id='txtTRLastIndex' value="1" />
    <input type="hidden" ID="HiddenField1" runat="server" />
</div>
</div>
<div class="ProLevel">
<table id="Leveltab" runat="server" border="0">
    <tr class="TrHeads">
        <th class="ThLevel">级别</th>
        <th class="ThName">级别名称</th>
        <th class="ThPeson">审批人</th>
        <th class="ThSend">抄送给</th>
        <th class="ThOperation">操作</th>
    </tr>
</table>
    <table border="0">
    <asp:Repeater ID="RepLevel" runat="server" OnItemCommand="RepLevel_ItemCommand">
    <ItemTemplate>
    <tr onmouseover="this.style.backgroundColor='#eaeae8'" onmouseout="this.style.backgroundColor='#fff'">
    <td class="ThLevel"><%#Eval("Level")%></td>
    <td class="ThName"><asp:TextBox ID="TxtLevelName" runat="server" Text='<%#Eval("LevelName")%>'></asp:TextBox></td>
    <td class="ThPeson"> 
    <asp:Label ID="lblApprover" runat="server" Text='<%#GetUserName(Eval("UserID"))%>'></asp:Label> 
        <a href="javascript:;" onclick="PopupDiv('div_share','<%#Eval("UserID") %>');setEdit2(this,'lblApprover','txtPeson');">[选择]</a>
        <asp:HiddenField id="txtPeson" runat="server" value='<%#Eval("UserID") %>' /></td>
    <td class="ThSend">
        <asp:Label ID="lblSend" runat="server" Text='<%#GetUserName(Eval("Send"))%>'></asp:Label>
        <a href="javascript:void(0)" onclick="PopupDiv('div_share','<%#Eval("Send") %>');setEdit2(this,'lblSend','txtSend');">[选择]</a>
        <asp:HiddenField id="txtSend" runat="server" value='<%#Eval("Send") %>' /></td>
    <td class="ThOperation">
        <a href="javascript:;" onclick="delSure(this,'<%#Eval("ID") %>');">[删除]</a>
    </td>
    </tr>
    </ItemTemplate>
    </asp:Repeater>
    </table>
    <script type="text/javascript">
        var obj1, obj2;
        //标识正在修改的行
        function setEdit(id1,id2)
        {
            obj1 = $("#"+id1);
            obj2 = $("#"+id2);
        }
        function setEdit2(obj,id1,id2)
        {
            obj1 = $(obj).parent().find("#"+id1);
            obj2 = $(obj).parent().find("#" + id2);
        }
        function PopupDiv(divID,chk)
        {
            $("#" + divID).show();
            $("#selUserFrame")[0].contentWindow.chkSome(chk);
        }
        function HideDiv(id)
        {
            $("#" + id).hide();
        }
        function sureFunc(id)
        {
            var chkArr = $("#selUserFrame").contents().find(":checkbox[name='ck1']:checked");//选中的值
            var userName = "";
            var userValue = "";
            for (var i = 0; i < chkArr.length; i++) {
                $chk = $(chkArr[i]);
                if ((i + 1) == chkArr.length) {
                    userName += $chk.attr("userName");
                    userValue += $chk.val();
                }
                else {
                    userName += $chk.attr("userName")+",";
                    userValue += $chk.val() + ",";
                }
            }
            $(obj1).text(userName);//label
            $(obj2).val(userValue);//Hidden
            HideDiv(id);
        }
        function postToCS(a, v) {
            $.ajax({
                type: "Post",
                url: "EditProcedure.aspx",
                data: { action: a, value: v },
                success: function (data)
                {
                    if (data == 1) { alert("操作成功"); }
                    else { alert("操作失败"); }
                },
                error: function (data) { result = data; }
            });
        }
        function delSure(obj,id) {
            if (confirm('确定要删除该条记录么？')) {
                $(obj).parent().parent().remove();
                postToCS('del', id);
            }
        }
    </script>
<table id="TabFoots">
<tr style="height:0px;">
<th class="ThLevel"></th>
<th class="ThName"></th>
<th class="ThPeson"></th>
<th class="ThSend"></th>
<th class="ThOperation"></th>
</tr>
</table>
<input type="hidden" id="HidPid" runat="server" />
<input type="hidden" id="TxtApprover" runat="server"/>
</div>

<div class="BtnDivs">
<asp:Button ID="BtnSub" OnClientClick='initData()' CssClass="i_bottom" runat="server" Text="提交" OnClick="BtnSub_Click" />
&nbsp;<asp:Button ID="BtnDelete" CssClass="i_bottom" runat="server" Text="删除流程" OnClick="BtnDelete_Click"/>
</div>
</div>

<div id="CommentDiv" style="position:absolute; padding:8px;border:1px solid #ccc;display:none; background-color:#fdfce6;">
<div style="float:left; width:680px;"><asp:Label ID="TitComType" runat="server"></asp:Label></div>
<div style="text-align:right; float:left;">
<span class="closex"><a href="javascript:void(0)" onclick="HideDiv('CommentDiv')" >×</a></span>
    </div>
<asp:TextBox ID="TxtComment" Text="" runat="server" Width="686px"></asp:TextBox>
<br />
    <input ID="HidCommTxt" type="hidden" value="" runat="server" />
    <span style=" float:right;">
    <asp:Button ID="BtnComment" Text="确定" runat="server" OnClick="BtnComment_Click" />
<input type="button" value="取消" id="conbtn" onclick="HideDiv('CommentDiv')"/> 
    </span>
</div>
<div id="div_share" class="pop_box">
<div class="p_head">
<div class="p_h_title">人员选择</div>
<div class="p_h_close" onclick="HideDiv('div_share')">关闭</div></div>
<iframe id="selUserFrame" src="/Mis/SelUser.aspx?OpenerText=TxtApprover" width="420" height="200" scrolling="yes" frameborder="0"></iframe>
<input type="button" id="sureBtn" value="确定" class="btn btn-primary" onclick="sureFunc('div_share')" />
<div class="p_bottom">
</div>
</div>
</div>
</div>
<asp:HiddenField ID="TxtResults" Value="1" runat="server" />
</asp:Content>
