<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShareReply.aspx.cs" Inherits="User_Order_ShareReply" %>
<form id="form1" runat="server">
<start>
<asp:Repeater runat="server" ID="ReplyList" OnItemDataBound="MsgRepeater_ItemDataBound">
<ItemTemplate>
<div class="comment-reply-item">
    <div class="reply-infor">
        <a class="user-name" href="javascript:;"><%#GetUserName()%></a>
        <%#GetReplyStr() %>
        <span class="words"><%#Eval("MsgContent") %></span>
        <span class="time"><%#Eval("CDate","{0:yyyy-MM-dd HH:mm}") %></span></div><div class="comment-operate">                        
        <span class="reply-trigger" data-info='{"id":"<%#Eval("Pid") %>","uname":"<%#GetUserName() %>","rid":"<%#Eval("ID") %>"}'>回复</span>        
        <a class="btn btn-primary btn-xs" id="Edit_A" runat="server" visible="false">修改</a>                  
        <button class="btn btn-primary btn-xs del-trigger" type="button" id="Del_Btn" runat="server" visible="false">删除</button>                
    </div>
</div>
</ItemTemplate>
<FooterTemplate></FooterTemplate>
</asp:Repeater>
</start>
</form>