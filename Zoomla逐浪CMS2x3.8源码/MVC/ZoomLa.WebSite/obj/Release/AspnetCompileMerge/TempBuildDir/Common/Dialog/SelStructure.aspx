<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelStructure.aspx.cs" Inherits="ZoomLaCMS.Common.Dialog.SelStructure" ClientIDMode="Static" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>选择部门</title>
<script type="text/javascript" src="/Plugins/JqueryUI/spin/spin.js"></script>
<script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
<script type="text/javascript" src="/JS/ICMS/ZL_Common.js"></script>
<style type="text/css">
* {margin: 0;padding: 0;font-family: 'Microsoft YaHei';font-size: 12px;}
.fixed {position: fixed;bottom: 0px;top: auto;right: 5px;}
.mainDiv {padding-top: 10px;}
.divBorder {border: 1px solid #ddd;min-height: 337px;padding:5px;}
#EGV tr th {color: black;background: url("");text-align: center; }
#EGV tr td input {line-height: normal;}
#EGV .dataRow:hover{background-color:#D5D5FD;}
#AllID_Chk{display:none;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="padding-left: 10px;" runat="server" id="Normal_Div" visible="false" class="mainDiv">
            <asp:Literal runat="server" ID="modelHtml" EnableViewState="false"></asp:Literal>
            <div>
                <button type="button" id="sureBtn" style="width: 80px;" onclick="sureuser()" class="btn btn-primary fixed" runat="server">确认</button>
            </div>
        </div>
        <div runat="server" id="AllInfo_Div" class="mainDiv container" visible="false">
            <div class="divBorder" style="float: left; width: 130px; margin-bottom: 10px; height: 380px; overflow-y: scroll;">
                <asp:Literal runat="server" ID="AllInfo_Litral" EnableViewState="false"></asp:Literal>
            </div>
            <asp:UpdatePanel runat="server" ID="UserDiv">
                <ContentTemplate>
                    <div style="float: left; margin-left: 10px; width: 580px; top: 2px;">
                       <div class="input-group" style="margin-bottom:5px;">
                            <span class="input-group-addon">
                                <asp:Label runat="server" ID="curLabel">全部用户</asp:Label></span>
                            <asp:TextBox runat="server" CssClass="form-control" ID="keyText" onkeydown="return GetEnterCode('click','keyBtn');" placeholder="可输入用户名,会员组名,工号" />
                            <span class="input-group-btn">
                                <asp:Button runat="server" ID="keyBtn" Text="搜索" CssClass="btn btn-primary" OnClick="keyBtn_Click" OnClientClick="return Search();" />
                                <asp:Button runat="server" ID="showAll_Btn" Text="全部用户" CssClass="btn btn-primary" Visible="false" OnClick="showAll_Btn_Click" />
                                <input type="button" id="AllInfo_sure_btn" value="确定" onclick="AllInfo_sureF();" class="btn btn-primary" />
                                <input type="button" id="clearBtn" onclick="ClearAll();" class="btn btn-primary" value="清除全部" />>
                            </span>
                        </div>
                        <table class="table table-bordered table-striped">
                            <tr><th><input id='chkAll' onclick='checkAll(this)' type='checkbox' /></th><th>工号</th><th>用户名</th><th>真实姓名</th></tr>
                            <ZL:ExRepeater ID="RPT" runat="server" PageSize="10" PagePre="<tr><td colspan='4'><div class='text-center'>" PageEnd="</div></td></tr>">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <input type="checkbox" <%#IsChecked(Eval("UserID")+":"+Eval("HoneyName")) %>  name="idChk" value="<%#Eval("UserID") %>" userinfo="<%#Eval("UserID")+":"+Eval("HoneyName") %>" onclick="SaveHide(this);" />
                                            <input type="hidden" name="AllInfo_hid" value="<%#Eval("GroupID") %>" />
                                        </td>
                                        <td><%#Eval("WorkNum") %></td>
                                        <td><%#Eval("UserName") %></td>
                                        <td><%#Eval("HoneyName") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate></FooterTemplate>
                            </ZL:ExRepeater>
                        </table>
                    </div>
                    <asp:Button runat="server" ID="ReBind_Btn" OnClick="ReBind_Btn_Click" Style="display: none;" />
                    <asp:HiddenField runat="server" ID="GroupID_H" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField runat="server" ID="UserInfo_H" />
            <div class="clear"></div>
        </div>
        <script>
            $("#GroupSel").find("li").find("ul").hide();
            function checkAll(obj) {//必须通过单击事件进行状态更改
                $("input[name='idChk']").each(function () {
                    if (this.checked != obj.checked)
                    { $(this).click();}
                });
            }
            function hiddenul(obj) {
                $(obj).parent().find("ul").toggle();
                $(obj).parent().find("ul").find("li").find("ul").hide();
            }
            function sureuser() {
                var groupJson = "";
                var s = "";
                $("input[type=checkbox][name=selgroup]:checked").each(function () {
                    s += $(this).attr("myName") + ",";
                    groupJson += '{ "Gname": "' + $(this).attr("myName") + '", "Gid":"' + $(this).val() + '"},';
                })
                if (groupJson) {
                    groupJson = groupJson.substring(0, groupJson.length - 1);
                    groupJson = eval("[" + groupJson + "]");
                }
                $(":checkbox").each(function () { this.checked = ''; });
                parent.selgroup(groupJson);
            }
            //----
            function disFrame(v) {
                parent.selGroup(v);
            }
            //根据会员组筛选
            function FilterTr(gid, gn) {
                $("#EGV tr:gt(0)").hide();
                $("#GroupID_H").val(gid);
                $("#ReBind_Btn").click();
            }
            //搜索
            function Search() {
                v = $("#keyText").val();
                if (v == "请输入用户名或会员组名") return false;
            }
            //清空所选CheckBox
            function ClearChk() {
                $("#EGV tr td :checkbox").each(function () { this.checked = false; });
                $("#UserInfo_H").val("");
            }
            function ShowAllTr() {
                //$("#EGV tr").show();
            }
            //拼接Json,传入$chkArr,包含UserInfo=UserID:UserName
            function CreateJson($obj) {
                var userJson = "";
                var uinfo = $("#UserInfo_H").val();
                if (uinfo) uinfo = uinfo.substring(0, uinfo.length - 1);
                var uArr = uinfo.split(',');
                for (var i = 0; i < uArr.length; i++) {
                    v = uArr[i];
                    var uname = v.split(':')[1]; if (!uname) uname = "";
                    userJson += '{"UserID":"' + v.split(':')[0] + '","UserName":"' + uname + '"},';
                }
                if (userJson) {//去除尾号,并Json化
                    userJson = userJson.substring(0, userJson.length - 1);
                    userJson = eval("[" + userJson + "]");
                }
                else { userJson = { UserID: "", UserName: "" }; }
                return userJson;
            }
            //确定选择
            function AllInfo_sureF() {
                var $chkArr = $("#EGV tr td :checkbox:checked");
                //if ($("#UserInfo_H").val() == "") { alert("你尚未选定用户!!!"); return; }
                //else
                //{
                    var select = GetParam2();
                    if (select != "")
                        parent.UserFunc(CreateJson($chkArr), select);
                    else
                        parent.UserFunc(CreateJson($chkArr));
                //}
            }
            //-----Tool
                function GetParam2() {
                    var index = location.href.indexOf("#");
                    var r = "";
                    if (index > -1) {
                        r = location.href.substring((index + 1), location.href.length);
                    }
                    return r;
                }
                function SaveHide(obj)
                {
                    if (obj.checked)
                    {
                        if ($("#UserInfo_H").val().indexOf(($(obj).attr("userinfo") + ",")) < 0)
                            $("#UserInfo_H").val($("#UserInfo_H").val() + $(obj).attr("userinfo") + ",");
                    }
                    else
                    {
                        $("#UserInfo_H").val($("#UserInfo_H").val().replace($(obj).attr("userinfo") + ",", ""));
                    }
                }
                function ClearAll() {
                    if (confirm("确定要清除所有选择的用户吗?")) {
                        var chkArr = $("input[type=checkbox][name=idChk]");
                        for (var i = 0; i < chkArr.length; i++) {
                            chkArr[i].checked = false;
                        }
                        $("#UserInfo_H").val("");
                    }
                }
        </script>
</asp:Content>
