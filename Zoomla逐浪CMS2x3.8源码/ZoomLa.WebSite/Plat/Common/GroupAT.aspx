<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GroupAT.aspx.cs" Inherits="Plat_Common_SelUser" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>部门@页面</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
             <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <div style="background-color: #FAFAFA; padding: 0 0 10px 10px; border-bottom: 1px solid #ddd;">
            <span class="fa fa-remove" style="cursor:pointer;margin-right:10px;" title="关闭" onclick="parent.DisGroupAT();"></span>
            <span>请选择需要@的部门或联系人</span>
            <div class="input-group input-group-sm" style="width: 220px;display:inline-block;margin-left:20px;position:relative;top:10px;">
                <input type="text" id="keyText" class="form-control" placeholder="搜索同事" onkeyup="if(event.keyCode==13)SearchKey();" />
                <span class="input-group-btn">
                    <button class="btn btn-default" type="button" onclick="SearchKey();"><span class="fa fa-search"></span></button>
                </span>
            </div>
        </div>
        <div runat="server" id="Normal_Div" visible="false" class="mainDiv">
            <asp:Literal runat="server" ID="modelHtml" EnableViewState="false"></asp:Literal>
            <div><button type="button" id="sureBtn" style="width: 80px;" onclick="sureuser()" class="btn-info fixed" runat="server">确认</button></div>
        </div>
        <div runat="server" id="AllInfo_Div" class="mainDiv">
            <div id="GroupDiv"><asp:Literal runat="server" ID="AllInfo_Litral" EnableViewState="false"></asp:Literal></div>
            <asp:UpdatePanel runat="server" ID="UserDiv">
                <ContentTemplate>
                    <div style="float: left; margin-left: 10px; width: 600px; top: 2px;">
                        <div style="margin-bottom: 5px;display:none;">
                           <button type="button" id="clearBtn" onclick="ClearAll();" class="btn btn-info" style="height:34px;">清除选择</button>
                      <%--      <asp:Button runat="server" ID="showAll_Btn" Text="全部用户" CssClass="btn btn-info" Visible="false" OnClick="showAll_Btn_Click"/>--%>
                      <%--      <input type="button" id="AllInfo_sure_btn" value="确定" onclick="AllInfo_sureF();" class="btn btn-info" />--%>
                        </div>
                        <asp:Repeater runat="server" ID="RPT" EnableViewState="false">
                            <HeaderTemplate><ul id="userul"></HeaderTemplate>
                            <ItemTemplate>
                                <li class="userli">
                                     <img src="<%#Eval("UserFace") %>" />
                                     <span style="padding-left:5px;float:left;"><%#Eval("TrueName") %></span>
                                     <input type="checkbox" <%#IsChecked(Eval("UserID")+":"+Eval("TrueName")) %>  name="idChk" value="<%#Eval("UserID") %>" userinfo="<%#Eval("UserID")+":"+Eval("TrueName") %>" onclick="SaveHide(this);" style="margin-right:5px;"/>
                                     <input type="hidden" name="AllInfo_hid" value="<%#Eval("Gid") %>" />
                                </li>
                            </ItemTemplate>
                            <FooterTemplate></ul></FooterTemplate>
                        </asp:Repeater>
                    </div>
                    <div style="display:none;">
                        <asp:TextBox runat="server" ID="Skey_T"></asp:TextBox>
                        <asp:Button runat="server" ID="keyBtn" Text="搜索" CssClass="btn btn-info" OnClick="keyBtn_Click"/>
                        <asp:Button runat="server" ID="ReBind_Btn" OnClick="ReBind_Btn_Click"/>
                    </div>
                    <asp:HiddenField runat="server" ID="GroupID_H" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField runat="server" ID="UserInfo_H" />
            <div class="clear"></div>
        </div>
    </div><!--End-->
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
       <style type="text/css">
        * {margin: 0;padding: 0;font-family: 'Microsoft YaHei';font-size: 12px;}
        ul{list-style-type:none;}
        #GroupDiv {float: left;padding:5px 0 10px 0; width: 120px;height: 400px;border-right: 1px solid #E6E6E6;background-color:#FAFAFA;}
        #GroupSel li{padding:5px;padding-left:10px;border-bottom:1px solid #ddd;}
        #GroupSel li a{text-decoration:none;display:inline-block;width:80px;}
        .GroupChk{float:right;}
        .liactive{background-color:#4DAFE3;}
        .liactive a{color:#fff;}
        #GroupSel li:hover{color:#fff;background-color:#4DAFE3;}
        #UserDiv{overflow-y:auto;height:400px;width:525px;overflow-x:hidden;}
        .userli{float:left;width:120px;height:40px;color:#5f5f5f;border-radius:2px;cursor:pointer;padding:5px; margin-right:5px;margin-bottom:5px;text-align:right;}
        .userli:hover{background-color:rgba(77, 175, 227, 0.43);color:white;}
        .userli img{height:30px;width:30px;display:inline-block;float:left;}
        #keyText {width: 160px;display:inline;}
    </style>
    <script type="text/javascript" src="/Plugins/JqueryUI/spin/spin.js"></script>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript">
        $("#GroupSel").find("li").find("ul").hide();
        function EventBind() {
            $(".userli").click(function () {
                $(this).find("input:checkbox").click();
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
            $("#GroupID_H").val(gid);
            //$("#curLabel").text(gn);
            $("#ReBind_Btn").click();
        }
        //清空所选CheckBox
        function ClearChk() {
            $("input[name=idChk]:checkbox").each(function () { this.checked = false; });
            $("#UserInfo_H").val("");
        }
        //拼接Json,传入$chkArr,包含UserInfo=UserID:UserName
        function CreateJson($obj) {
            var userJson = "";
            var uinfo = $("#UserInfo_H").val();
            if (uinfo) uinfo = uinfo.substring(0, uinfo.length - 1);
            var uArr = uinfo.split(',');
            for (var i = 0; i < uArr.length; i++) {
                v = uArr[i];
                //userJson += '{"UserID":"' + v.split(':')[0] + '","UserName":"' + v.split(':')[1] + '","TrueName":"' + v.split(':')[2] + '"},';
                userJson += '{"UserID":"' + v.split(':')[0] + '","UserName":"' + v.split(':')[1] + '"},';
            }
            if (userJson) {//去除尾号,并Json化
                userJson = userJson.substring(0, userJson.length - 1);
                userJson = eval("[" + userJson + "]");
            }
            return userJson;
        }
        //确定选择
        function AllInfo_sureF() {
            var $chkArr = $("input[name=idChk]:checked");
            if ($("#UserInfo_H").val() == "") { alert("你尚未选定用户!!!"); return; }
            else
            {
                parent.GroupATFunc(CreateJson($chkArr));
            }
        }
        //Tool
        function SearchKey() {
            $("#Skey_T").val($("#keyText").val());
            if ($("#Skey_T").val() == "") return false;
            $("#keyBtn").click();
        }
        function GetParam2() {
            var index = location.href.indexOf("#");
            var r = "";
            if (index > -1) {
                r = location.href.substring((index + 1), location.href.length);
            }
            return r;
        }
        function SaveHide(obj) {
            if (obj.checked) {
                if ($("#UserInfo_H").val().indexOf(($(obj).attr("userinfo") + ",")) < 0)
                    $("#UserInfo_H").val($("#UserInfo_H").val() + $(obj).attr("userinfo") + ",");
            }
            else {
                $("#UserInfo_H").val($("#UserInfo_H").val().replace($(obj).attr("userinfo") + ",", ""));
            }
            parent.GroupATFunc(obj);
            event.stopPropagation();
        }
        function SaveGroup(obj) {
            parent.GroupFunc(obj);
        }
        function ClearAll() {
            var chkArr = $("input[type=checkbox][name=idChk]");
            for (var i = 0; i < chkArr.length; i++) {
                chkArr[i].checked = false;
            }
            $("#UserInfo_H").val("");
        }
        //CSS
        $().ready(function () {
            $("#GroupSel li").click(function () { $(this).addClass("liactive").siblings().removeClass("liactive"); });
            //.mouseover(function () { $(this).addClass("liactive") }).mouseout(function () {$(this).removeClass("liactive") });
            $("#GroupSel li:eq(2)").addClass("liactive");
        });
    </script>
</asp:Content>
