<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelUser.aspx.cs" MasterPageFile="~/Common/Master/Empty.master" Inherits="MIS_ZLOA_Mail_SelUser" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>选择会员</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div style="padding:10px;background:#fff">
    <asp:TextBox ID="searchT" Width="200" CssClass="form-control" style="display:inline;" runat="server"></asp:TextBox> <asp:Button ID="SearchBtn" CssClass="btn btn-primary" runat="server" Text="查找" OnClick="SearchBtn_Click" />
</div>
<div id="seluser">
    <div id="CheckDiv" runat="server" visible="false">
    <ul class="list-unstyled" id="Group">
        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound" EnableViewState="false">
            <ItemTemplate>
                <li><input type="checkbox" onclick="chkAll(this);"  name="ck1" value="<%#Eval("GroupID") %>" myName="<%#Eval("GroupName")%>" />
                    <a href="javascript:;" ><%# Eval("GroupName") %></a>
                    <ul class="list-unstyled userul" style="padding-left:20px;display:none;">
                        <asp:Repeater ID="Repeater2" runat="server" >
                            <ItemTemplate>
                                <li>
                                    <input type="checkbox"  name="ck2" value="<%#Eval("UserID") %>" myName="<%#Eval("UserName")%>" />
                                    <a href="javascript:;" onclick="chkClick(this);">[<%#Eval("GroupName") %>]<%#Eval("UserName") %></a> 
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div><button type="button" id="sureBtn" style="width:80px;" onclick="sureuser()" class="btn btn-primary fixed">确认</button></div>
    </div>

    <div id="RadioDiv" runat="server" visible="false">
        <ul class="list-unstyled" id="GroupRad">
            <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                <ItemTemplate>
                    <li>
                        <a href="javascript:;" ><%# Eval("GroupName") %></a>
                        <ul class="list-unstyled userul" style="padding-left:20px;display:none;">
                            <asp:Repeater ID="Repeater2" runat="server" >
                                <ItemTemplate>
                                    <li>
                                        <input type="radio" name="rd1" value="<%#Eval("UserID") %>" myName="<%#Eval("UserName")%>" />
                                        <a href="javascript:;" onclick="radClick(this);"><%#Eval("UserName") %></a> 
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <div><button type="button" id="Button1" style="width:80px;" onclick="sureuserrad()" class="btn btn-primary fixed">确认</button></div>
    </div>

    <div id="AllInfo_Div" runat="server"><!--多选,显示用户详细信息-->
    <ul class="list-unstyled" id="AllInfo_Group_ul">
        <asp:Repeater ID="AllInfo_Rep" runat="server" OnItemDataBound="Repeater1_ItemDataBound" EnableViewState="false">
            <ItemTemplate>
                <li><input type="checkbox" onclick="chkAll(this);"  name="ck1" value="<%#Eval("GroupID") %>" myName="<%#Eval("GroupName")%>" />
                    <a href="javascript:;" ><%# Eval("GroupName") %></a>
                    <ul class="list-unstyled userul" style="padding-left:20px;display:none;">
                        <asp:Repeater ID="Repeater2" runat="server" >
                            <ItemTemplate>
                                <li>
                                    <input type="checkbox"  name="ck2" value="<%#Eval("UserID") %>" myName="<%#Eval("UserName")%>" />
                                    <a href="javascript:;" onclick="chkClick(this);">[<%#Eval("GroupName") %>]<%#Eval("UserName") %></a> 
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div><button type="button" id="AllInfo_sure_btn" style="width:80px;" onclick="sureuser()" class="btn btn-primary fixed">确认</button></div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
        *{margin:0;padding:0;}
        * html,* html body{ background-image:url(about:blank);background-attachment:fixed;}
        * html .fixed{
            position:absolute;bottom:auto;
            top:expression(eval(document.documentElement.scrollTop+
            document.documentElement.clientHeight-this.offsetHeight-
            (parseInt(this.currentStyle.marginTop,10)||0)-
            (parseInt(this.currentStyle.marginBottom,10)||0)));}      
        .fixed{position:fixed;bottom:0px;top:auto;right:5px;}   
    </style>
<script>
    var IDS = '<% =Request.QueryString["SUID"]%>';
    var CID = '<% =Request.QueryString["CUID"]%>';
    var id = '<%= Request.QueryString["ID"] %>';
    var IDArr = IDS.split(',');
    var CIDArr = CID.split(',');
    var rdArr = $("#GroupRad").find(":radio[name=rd1][value=" + id + "]");
    var type = '<%=Request.QueryString["Type"]%>';
    if (type == "Radio")
        $("#AllInfo_sure_btn").hide();
    for (var i = 0; i < rdArr.length; i++) {
        rdArr[i].checked = true;
    }
    selectID(IDArr);
    selectID(CIDArr);
    function selectID(Arr)
    {
        for (var j = 0; j < Arr.length; j++) {
            chkArr1 = $("#Group").find(":checkbox[name=ck2][value=" + Arr[j] + "]");
            for (var i = 0; i < chkArr1.length; i++) {
                chkArr1[i].checked = true;
            }
        }
    }
    function chkAll(obj)
    {
        chkArr = $(obj).parent().find(":checkbox[name=ck2]");
        for (var i = 0; i < chkArr.length; i++) {
            chkArr[i].checked = obj.checked;
        }
    }
    function valueChk()//有参则选中默认值
    {
        if (getParam("Gid")) {
        }
        if (getParam("Uid")) {
           
        }
    }
    $().ready(function () {
        valueChk();
        $("#Group").children('li').children('a').click(function () {
            $(this).parent().children('ul').toggle("fast");
        });
        $("#GroupRad").children('li').children('a').click(function () {
            $(this).parent().children('ul').toggle("fast");
        });
        $("#AllInfo_Group_ul").children('li').children('a').click(function () {
            $(this).parent().children('ul').toggle("fast");
        });
    });
    function sureuser()
    {
        //[{ "userName": "wtwt", "userID": 1}];
        var groupJson="";//需要赋值，否则下面会为undefined{}
        var userJson = "";
        $("input[type=checkbox][name=ck1]:checked").each(function ()
        {
            groupJson += '{ "Gname": "'+$(this).attr("myName")+'", "Gid":"'+$(this).val()+'"},';
        })
        $("input[type=checkbox][name=ck2]:checked").each(function ()
        {
            userJson += '{ "Uname": "' + $(this).attr("myName") + '", "Uid": "' + $(this).val() + '"} ,';
        })
        if (groupJson)
        {
            groupJson = groupJson.substring(0, groupJson.length - 1);
            groupJson = eval("["+groupJson+"]");
        }
        if (userJson) {
            userJson = userJson.substring(0, userJson.length - 1);
            userJson = eval("[" + userJson + "]");
        }
        if ('<%= Request.QueryString["Type"]%>' == "1") {
            parent.seluser1(groupJson, userJson);
        }
        else
        {
            parent.seluser(groupJson, userJson);
        }
        $(":checkbox").each(function () { this.checked = ''; });
        parent.hidediv();
    }
    function sureuserrad() {
        //[{ "userName": "wtwt", "userID": 1}];
        var groupJson = "";//需要赋值，否则下面会为undefined{}
        var userJson = "";
        $("input[type=radio][name=rd1]:checked").each(function () {
            groupJson += '{ "Gname": "' + $(this).attr("myName") + '", "Gid":"' + $(this).val() + '"},';
        })
        $("input[type=radio][name=rd1]:checked").each(function () {
            userJson += '{ "Uname": "' + $(this).attr("myName") + '", "Uid": "' + $(this).val() + '"} ,';
        })
        if (groupJson) {
            groupJson = groupJson.substring(0, groupJson.length - 1);
            groupJson = eval("[" + groupJson + "]");
        }
        if (userJson) {
            userJson = userJson.substring(0, userJson.length - 1);
            userJson = eval("[" + userJson + "]");
        }
        parent.seluser(groupJson, userJson);
        $(":radio").each(function () { this.checked = ''; });
        parent.hidediv();
    }
    function getParam(paramName) {
        paramValue = "";
        isFound = false;
        if (this.location.search.indexOf("?") == 0 && this.location.search.indexOf("=") > 1) {
            arrSource = decodeURI(this.location.search).substring(1, this.location.search.length).split("&");
            i = 0;
            while (i < arrSource.length && !isFound) {
                if (arrSource[i].indexOf("=") > 0) {
                    if (arrSource[i].split("=")[0].toLowerCase() == paramName.toLowerCase()) {
                        paramValue = arrSource[i].split("=")[1];
                        isFound = true;
                    }
                }
                i++;
            }
        }
        return paramValue;
    }
    function chkClick(obj)
    {
        chk = $(obj).parent().find(":checkbox")[0];
        chk.checked = !chk.checked;
    }
    function radClick(obj) {
        rad = $(obj).parent().find(":radio")[0];
        rad.checked = true;
    }
</script>
</asp:Content>
