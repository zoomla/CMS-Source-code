<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReplyList.aspx.cs" Inherits="ZoomLaCMS.Plat.Blog.ReplyList" %>
<form id="form1" runat="server">
<start>
<div class="reply_exinfo_div" id="bar_div" runat="server" visible="false">
    <div><strong><i class="fa fa-share-alt-square"></i> 社区信息</strong></div>
    <asp:Repeater runat="server" ID="Bar_RPT" EnableViewState="false">
        <ItemTemplate>
            <div style="padding-top:5px;border-bottom:1px dashed #ddd;">
                <div class="pull-left">
                   <%-- <div class="uword uword_xs"><%#Eval("UserName","").Substring(0,1) %></div>
                    <img class="uimg img_xs replyface" src="<%#Eval("UserFace") %>" onerror="showword(this);" />--%>
                    <%#ZoomLa.BLL.B_User_Plat.WordFace(Eval("CUser"),"uword_xs",Eval("HoneyName",""),Eval("UserName","")) %>
                </div>
                <div class="pull-left" style="float: left; overflow: hidden; text-overflow: ellipsis;padding-left:5px;">
                   <a href="javascript:;"><%#GetUName() %>：</a>
                </div>
                <div class="text-left"><%#ZoomLa.BLL.Helper.StrHelper.DecompressString(Eval("MsgContent","")) %></div>
                <div class="clearfix"></div>
                <div class="text-right" style="font-size:12px;"><%#Eval("CDate","{0:yyyy年MM月dd日 HH:mm}") %></div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
<asp:Repeater runat="server" ID="ReplyList_RPT">
<ItemTemplate>
<div class="replyrow" id="msgitem-<%#Eval("ID") %>">
  <div class="pull-left">
    <%--  <div class="uword uword_xs"><%#Eval("HoneyName") %></div>
      <img class="uimg img_xs replyface" src="<%#Eval("UserFace") %>" onerror="showword(this);" data-uid="<%#Eval("CUser") %>" /> --%>
       <%#ZoomLa.BLL.B_User_Plat.WordFace(Eval("CUser"),"uword_xs",Eval("HoneyName",""),Eval("UserName","")) %>
  </div>
  <div class="reply_body">
    <div class="reply_btnlist"><%#GetReplyStr(Eval("ReplyID")) %> </div>
    <div class="reply_files"><%#GetAttach() %></div>
    <div class="replybottom grayremind">
        <%#Convert.ToDateTime(Eval("CDate","{0:yyyy年MM月dd日 HH:mm}")) %>
        <%#Convert.ToInt32(Eval("CUser"))==UserID?"<span class='fa fa-trash-o' title='删除' onclick=\"PostDelMsg("+Eval("ID")+");\"></span>":"" %>
        <span class="fa fa-comment" title="回复" onclick="DisReplyOP(<%#Eval("Pid") %>,<%#Eval("ID") %>,'<%#GetUName() %>');"></span>
  </div>
  <div class="replyspear"></div>
</div></div>
</ItemTemplate>
<FooterTemplate></FooterTemplate>
</asp:Repeater>
</start>
</form>

