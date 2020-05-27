<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uinfo.aspx.cs" Inherits="ZoomLaCMS.Plat.Common.uinfo"  MasterPageFile="~/Plat/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Head"><title>用户信息</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container platcontainer">
    <div class="uinfo">
        <div style="position:absolute;width:100px;height:100px;">
            <img id="uface_img" src="#" runat="server"  onerror="shownoface(this);" style="width:100px;height:100px;" />
        </div>
        <div style="width:280px;margin-left:110px; display:inline-block;">
            <div runat="server" id="HoneyName_L" class="urow" style="font-size:1.2em;font-weight:bolder;"></div>
            <div runat="server" id="Post_L" class="urow r_gray"></div>
            <div runat="server" id="CompName_l" class="urow r_gray"></div>
            <div class="urow">
                <a title="发送站内信" href="javascript:;" onclick="openwin('mail');"><i class="fa fa-comment"></i></a>
                <a title="查看工作流" href="javascript:;" onclick="openwin('blog');" style="padding-top:1px;"><i class="fa fa-th-large"></i></a>
            </div>
        </div>
        <hr/>
        <div class="urow">
            <span class="tdl">手机：</span>
            <asp:Label runat="server" ID="Mobile_L"></asp:Label>
        </div>
        <div class="urow">
            <span class="tdl">邮箱：</span>
            <asp:Label runat="server" ID="Mail_L"></asp:Label>
        </div>
        <div class="urow">
            <span class="tdl">工号：</span>
            <asp:Label runat="server" ID="WorkNum_L"></asp:Label>
        </div>
        <div class="urow">
            <span class="tdl">工作地点：</span>
            <asp:Label runat="server" ID="Work_L"></asp:Label>
        </div>
        <div class="urow">
            <span class="tdl">居住地址：</span>
            <asp:Label runat="server" ID="Home_L"></asp:Label>
        </div>
        <div class="urow">
            <span class="tdl">生日：</span>
            <asp:Label runat="server" ID="BirthDay_L"></asp:Label>
        </div>
    </div>
</div>
<asp:HiddenField runat="server" ID="uid_hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">
.uinfo {width:400px;margin:0 auto;position:relative;padding-bottom:10px;margin-top:20px;}
.uinfo .urow {display:block;margin-bottom:15px;}
.uinfo .urow a {display:inline-block;background-color:#0094ff;color:#fff;text-align:center;width:28px;height:28px;line-height:28px;border-radius:50%;text-decoration:none;}
.uinfo hr {border-bottom:1px solid #ddd;}
.uinfo .tdl {display:inline-block;width:80px;color:#999;}
</style>
<script>
function openwin(type) {
    var uid = $("#uid_hid").val();
    switch (type) {
        case "mail":
            window.open("/Plat/Mail/MessageSend.aspx?uid=" + uid);
            break;
        case "blog":
            window.open("/Plat/Blog/?uids=" + uid);
            break;
    }
}
</script>
</asp:Content>