<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TimeLine.aspx.cs" Inherits="Plat_Blog_TimeLine" EnableViewState="false" EnableViewStateMac="false" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="msglist" class="msglist" style="max-width: 1170px; margin: 0 auto;">
        <div id="ttitle_div" class="timeline_title" style="margin-left: 28px;" runat="server">
            <div class="img_div">
                <img src="<%= GetUser().UserFace %>" onerror="shownoface(this);" style="vertical-align: middle; border: solid 1px #e8e8e8; width: 100px; height: 100px; border-radius: 50%; box-shadow: 0 0 3px 1px rgba(245,245,245,0.5);" />
                <span style="font-size: 18px; color: #999;"><%= GetUser().UserName %>的时间线</span>
            </div>
        </div>
        <section id="time-timeline" class="time-container" style="position: relative; padding: 2em 0; width: 100%; max-width: 1170px; margin: 0 auto 2em auto;">
            <asp:Repeater runat="server" ID="MsgRepeater" OnItemDataBound="MsgRepeater_ItemDataBound">
                <ItemTemplate>
                    <div class="time-timeline-block" style="position: relative; margin: 2em 0;">
                        <div class="time-timeline-img text-center" style="position: absolute; top: 0; left: 70px; width: 20px; height: 20px; background-color: white; border-radius: 50%; box-shadow: 0 0 0 1px #BABBC1, inset 0 1px 0 rgba(0, 0, 0, 0.08), 0 3px 0 1px rgba(0, 0, 0, 0.05);">
                            <i class="fa <%#Eval("timeline").Equals("date")? "fa-home":"fa-circle" %>" style="color: #BABBC1; font-size: 20px; line-height: 20px;"></i>
                        </div>
                        <div class="time-timeline-text <%#Eval("timeline") %>" style="height: 20px;">
                            <p class="first" style="color: #0094ff;"><%#Eval("CDate","{0:yyyy-MM-dd}") %></p>
                            <p class="second"><%#Eval("CDate","{0:HH:MM}") %></p>
                        </div>
                        <div class="time-timeline-content" style="position: relative; margin-top: -20px; margin-left: 105px; border-radius: 0.25em; padding: 1em; border: 1px solid #DEE0E5;">
                            <div class="msgitem" id="msgitem-<%#Eval("ID") %>" style="margin-bottom: 0px; padding-bottom: 0px; min-height:0px;">
                                <div class="msg_content_div plat_content_div" style="margin-left: 10px; line-height: 18px;">
                                    <div id="normal" runat="server" visible="false">
                                        <div class="msg_content_article_div" style="white-space: normal; word-break: break-all;"><%#Eval("content")%></div>
                                        <%#GetForward() %>
                                    </div>
                                    <div id="vote" runat="server" visible="false">
                                        <div style="padding-bottom: 5px;"><strong><%#Eval("Title") %></strong></div>
                                        <div class="vote_user_div" id="vote_user_div" runat="server">
                                            <ul class="vote_list_ul">
                                                <%#GetVoteLI() %>
                                            </ul>
                                            <div>
                                                <input type="button" value="投票" onclick="PostUserVote(<%#Eval("ID") %>);" />
                                                <input type="button" value="查看结果" onclick="ShowMsgDiv('<%#Eval("ID") %>    ','.vote_user_div','.vote_result_div');" />
                                            </div>
                                        </div>
                                        <div class="vote_result_div" id="vote_result_div" style="display: none;" runat="server">
                                            <asp:Repeater runat="server" EnableViewState="false" ID="VoteResultRep">
                                                <ItemTemplate>
                                                    <div><%#Eval("opName") %></div>
                                                    <div class="progress vote_progress" style="width: 260px; height: 20px; display: inline-block; margin-bottom: 0px; background-color: #f5f5f5; border-radius: 4px;">
                                                        <div class="progress-bar progress-bar-success" style='background-color: #5cb85c; line-height: 20px; line-height: 20px; text-align: center; box-shadow: inset 0 -1px 0 rgba(0,0,0,.15); transition: width .6s ease; height: 100%; float: left; <%#"width:"+Eval("Percent")+"%;" %>' role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                                                    </div>
                                                    <label style="height: 20px; line-height: 20px; position: relative; top: -4px;"><%#Eval("count")+"人" %></label>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <div style="clear: both;"></div>
                                            <div id="msg_op_btn_div" runat="server">
                                                <input value="返回投票" onclick="ShowMsgDiv(<%#Eval("ID")%>,'.vote_result_div','.vote_user_div');" />
                                            </div>
                                        </div>
                                        <div style="padding: 5px;"><strong><%#GetVoteBottom() %></strong></div>
                                        <div style="padding-bottom: 5px;"><%#Eval("MsgContent") %></div>
                                    </div>
                                    <div id="longarticle" runat="server" visible="false">
                                        <div class="subtitle grayremind" style="max-height: 113px; margin-right: 2px; cursor: pointer; font-size: 12px; color: #A0A0A0;"><%#Eval("Title") %><div style="clear: both;"></div>
                                            <input type="button" value="浏览全文" onclick="ShowLong(<%#Eval("ID")%>);" /></div>
                                    </div>
                                    <a href="#" class="grayremind" title="浏览信息详情"><%#Convert.ToDateTime(Eval("CDate","{0:yyy年MM月dd日 HH:mm}")) %></a>
                                    <span class="grayremind" title="哪些人可见"><%#GetWhoCanSee()%></span>

                                    <div id='reply_<%#Eval("ID") %>'>
                                        <asp:Literal runat="server" ID="ReplyList_L" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                                <div style="clear: both;"></div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </section>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
</asp:Content>
