<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgListByUser.aspx.cs" Inherits="ZoomLaCMS.Plat.Blog.MsgListBody" EnableViewState="false" EnableViewStateMac="false" %>
<start>
  <div id="ttitle_div" class="timeline_title" runat="server">
      <div class="img_div">
          <img src="<%= GetUser().UserFace %>" onerror="shownoface(this);" />
          <span><%=GetUser().UserName %>的时间线</span>
      </div>
      <div class="link_div">
          <a href="javascript:;" onclick="AddAT('<%=GetUser().UserName %>', '<%=GetUser().UserID %>');">@他(她)</a>
          <a href="javascript:;" onclick="ChatShow('<%=GetUser().UserID %>', '<%=GetUser().UserName %>');" title="发私信"><i class="fa fa-commenting-o"></i></a>
          <a href="TimeLineToMHT.aspx?uid=<%=GetUser().UserID %>&type=mht" title="导出为MHT" ><i class="fa fa-file-archive-o"></i></a>
          <a href="TimeLineToMHT.aspx?uid=<%=GetUser().UserID %>&type=pdf" title="导出为PDF" ><i class="fa fa-file-pdf-o"></i></a>
      </div>
  </div>
 <section id="time-timeline" class="time-container">
  <asp:Repeater runat="server" ID="MsgRepeater" OnItemDataBound="MsgRepeater_ItemDataBound">
	<ItemTemplate>
    <div class="time-timeline-block">
        <%--日期--%>
		<div class="time-timeline-img text-center">
            <i class="fa <%#Eval("timeline").Equals("date")? "fa-home":"fa-circle" %>"></i>
		</div>
        <div class="time-timeline-text <%#Eval("timeline") %>">
            <p class="first"><%#Eval("CDate","{0:yyyy-MM-dd}") %></p>
            <p class="second"><%#Eval("CDate","{0:HH:MM}") %></p>
        </div>
		<div class="time-timeline-content">
	  <div class="msgitem" style="min-height:0px;" id="msgitem-<%#Eval("ID") %>">
		<div class="msg_content_div plat_content_div">
		    <div id="normal" runat="server" visible="false">
			    <div class="msg_content_article_div"><%#GetContent()%></div>
			    <%#GetAttach() %> <%#GetForward() %> </div>
		    <div id="vote" runat="server" visible="false">
			    <div class="paddbottom5"><strong><%#Eval("Title") %></strong></div>
			    <div class="vote_user_div" id="vote_user_div" runat="server">
			      <ul class="vote_list_ul">
				    <%#GetVoteLI() %>
			      </ul>
			      <div>
				    <input type="button" value="投票" onclick="PostUserVote(<%#Eval("ID") %>);" class="btn btn-primary btn-sm"/>
				    <input type="button" value="查看结果" onclick="ShowMsgDiv('<%#Eval("ID") %>    ','.vote_user_div','.vote_result_div');" class="btn btn-primary btn-sm"/>
			      </div>
			    </div>
			    <div class="vote_result_div" id="vote_result_div" runat="server">
			      <asp:Repeater runat="server" EnableViewState="false" ID="VoteResultRep">
				    <ItemTemplate>
				      <div><%#Eval("opName") %></div>
				      <div class="progress vote_progress">
					    <div class="progress-bar progress-bar-success" role="progressbar" aria-valuemin="0" aria-valuemax="100" <%#"style='width:"+Eval("Percent")+"%;'" %>></div>
				      </div>
				      <label class="vote_person_num"><%#Eval("count")+"人" %></label>
				    </ItemTemplate>
			      </asp:Repeater>
			      <div class="clearfix"></div>
			      <div id="msg_op_btn_div" runat="server">
				    <input value="返回投票" class="btn btn-primary btn-sm" onclick="ShowMsgDiv(<%#Eval("ID")%>,'.vote_result_div','.vote_user_div');" />
			      </div>
			    </div>
			    <div class="paddin5"><strong><%#GetVoteBottom() %></strong></div>
			    <div class="paddbottom5"><%#Eval("MsgContent") %></div>
		      </div>
            <div id="longarticle" runat="server" visible="false">
                  <div class="subtitle grayremind"><%#Eval("Title") %><div class="clearfix"></div><input type="button" value="浏览全文" class="btn btn-xs btn-primary" onclick="ShowLong(<%#Eval("ID")%>);" /></div>
              </div>
		  <a href="Item.aspx?id=<%#Eval("ID") %>&uid=<%# Eval("CUser") %>" class="grayremind" title="浏览信息详情"><%#Convert.ToDateTime(Eval("CDate","{0:yyy年MM月dd日 HH:mm}")) %></a>
          <span class="grayremind" title="哪些人可见"><%#GetWhoCanSee()%></span>
		  <div class="likeids_div">
		  </div>
		  <div id='reply_<%#Eval("ID") %>'>
			<asp:Literal runat="server" ID="ReplyList_L" EnableViewState="false"></asp:Literal>
		  </div>
		  <div id="reply_div_<%#Eval("ID") %>" class="reply_item">
			<input type="hidden" id="Reply_Rid_Hid_<%#Eval("ID") %>" value="0" />
			<textarea id="MsgContent_<%#Eval("ID") %>" class="form-control atwho reply_text" style="height:40px;" placeholder="说一句吧..."></textarea>
			<input type="button" value="回复" onclick="AddReply(<%#Eval("ID") %>);" class="btn btn-primary btn-sm replybtn" />
			<div class="clearfix"></div>
		  </div>
		</div>
		<div class="clearfix"></div>
	  </div>
    </div></div>
	</ItemTemplate>
  </asp:Repeater>
</section>
</start>
