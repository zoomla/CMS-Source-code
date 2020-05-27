<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditContent.aspx.cs" Inherits="ZoomLaCMS.Guest.Bar.EditContent" MasterPageFile="~/Guest/Guest.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title><%:Call.SiteName+"贴吧" %></title>
<script src="/Plugins/Ueditor/bar.config.js"></script>
<script src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container">
    <div runat="server" id="send_div">
        <div style="padding-top:5px;padding-bottom:2px;">
            <div>
                <span class="pull-left">  
                    <asp:Label runat="server" ID="Tip_T" Text="编辑帖子"></asp:Label> 
                    <a href="/PItem?id=<%=PostID %>">
                    <asp:Label runat="server" ID="PostName_L" /></a>   
                <span runat="server" id="Anony_Span" visible="false" class="card_menNum" style="margin-left:20px;font-size:12px;">[匿名发帖模式]</span>    
                </span>
                <span class="pull-right"><asp:HyperLink runat="server" ID="ReturnBar_a">返回</asp:HyperLink></span>   
                <div class="clearfix"></div>                 
            </div>
        </div>
        <div><asp:TextBox runat="server" ID="MsgTitle_T" Visible="false" data-type="normal" CssClass="form-control"/></div>
        <div style="padding: 5px 0 5px 0;">
            <asp:TextBox runat="server" ID="MsgContent_T" data-type="normal" TextMode="MultiLine" Style="height: 200px;width:100%;" placeholder="说点什么吧..."/></div>
        <div id="SendDiv" runat="server">
          <span id="Valid_S" runat="server" visible="false">
              <asp:TextBox ID="VCode" placeholder="验证码" MaxLength="6" runat="server" style="display:inline-block" CssClass="form-control text_x" autocomplete="off"/>
              <img id="VCode_img" class="codeimg" title="点击刷新验证码" style="margin-left:2px;width:80px;"/>
              <input type="hidden" id="VCode_hid" name="VCode_hid" />
          </span>
          <div style="height:20px;"></div>
          <div id="bar_editBtn" class="navbar-fixed-bottom">
          <div class="container text-center">  
          <asp:Button runat="server" ID="PostMsg_Btn" Text="发布主题"  style="display:inline;" OnClick="PostMsg_Btn_Click" OnClientClick="return CheckData();" CssClass="btn btn-primary" />
          <asp:Button runat="server" ID="DelMsg_Btn" Text="删除主题"  style="display:inline;" OnClick="DelMsg_Btn_Click" OnClientClick="return confirm('确定要删除吗');" CssClass="btn btn-primary" />
          <asp:Button runat="server" ID="Button1" Text="重置主题"  style="display:inline;" OnClick="CancelMsg_Btn_Click" CssClass="btn btn-primary" />
          </div>
          </div>
        </div>
    </div>
     <div id="noauth_div" runat="server" visible="false">你当前没有发贴权限,请联系管理员开通!!</div>
    <%=Call.GetUEditor("MsgContent_T",4)%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/JS/ZL_ValidateCode.js"></script>
<script>
    $("#VCode").ValidateCode();
	function CheckData() {
	    var msg = UE.getEditor("MsgContent_T").getContent();;
	    if ($("#MsgTitle_T").val() == "") { alert("贴子标题不能为空!"); return false;}
		if (msg.replace(" ", "") == "") { alert("内容不能为空"); return false; }
		if ($("#VCode").val() == "") { alert("验证码不能为空"); return false; }
	}
	function GetSource() { return "<%=Request.RawUrl%>"; }
    //删除帖子
    function PostDelMsg(msgid) {
         
        if (confirm("确定要删除该条信息吗!!")) { 
            PostToCS("DeleteMsg", msgid, function (data) { if (data == "failed") alert("删除失败,当前用户无权限!"); });
        }
    } 
</script> 
</asp:Content>