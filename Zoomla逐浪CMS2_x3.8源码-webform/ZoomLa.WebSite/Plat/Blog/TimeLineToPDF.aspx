<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TimeLine.aspx.cs" Inherits="Plat_Blog_TimeLine" %><!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
    .time-timeline-block .second { font-size:12px; position:relative;left:35px; }
    .date .second { position:relative; top:-10px; }
    .time .first { display: none; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="msglist" class="msglist" style="max-width:1170px;margin:0 auto;">  
            <div>
                <div id="ttitle_div" class="timeline_title" style="margin-left: -50px;" runat="server">
                    <div class="img_div">
                        <span style="font-size: 18px; color: #999;"><%= GetUser().UserName %>的时间线</span>
                    </div>
                </div>
                <div style="margin-left:-80px;">
                    <asp:Repeater runat="server" ID="MsgRepeater" OnItemDataBound="MsgRepeater_ItemDataBound">
                        <ItemTemplate>
                            <div class="time-timeline-block" style="position: relative; margin: 2em 0;">
                                <div class="time-timeline-img text-center" style="position: absolute; top: 0; left: 70px; width: 20px; height: 20px; background-color: white;">
                                   
                                </div>
                                <div class="time-timeline-text <%#Eval("timeline") %>" style="height: 20px;">
                                    <p class="first" style="color: #0094ff; font-size: 10px;"><%#Eval("CDate","{0:yyyy-MM-dd}") %></p>
                                    <p class="second"><%#Eval("CDate","{0:HH:MM}") %></p>
                                </div>
                                <div class="time-timeline-content" style="position: relative; margin-top: -20px; margin-left: 105px;padding:1em;">
                                    <div class="msgitem" id="msgitem-<%#Eval("ID") %>" style="margin-bottom: 0px; padding-bottom: 0px; min-height: 0px;">
                                        <div class="msg_content_div plat_content_div" style="margin-left: 10px; line-height: 18px;">
                                            <div id="normal" runat="server" visible="false">
                                                <div class="msg_content_article_div" style="white-space: normal; word-break: break-all;"><%#Eval("MsgContent")%></div>
                                                <%#GetForward() %>
                                            </div>
                                            <div id="vote" runat="server" visible="false">
                                                <div style="padding-bottom:5px;"><strong><%#Eval("Title") %></strong></div>
                                                <div class="vote_user_div" id="vote_user_div" runat="server">
                                                    <ul class="vote_list_ul">
                                                        <%#GetVoteLI() %>
                                                    </ul>
                                                </div>
                                                <div class="vote_result_div" id="vote_result_div" style="display: none;" runat="server">
                                                    <asp:Repeater runat="server" EnableViewState="false" ID="VoteResultRep">
                                                        <ItemTemplate>
                                                            <div><%#Eval("opName") %></div>
                                                            <div class="progress vote_progress" style="width: 260px; height: 20px; display: inline-block; margin-bottom: 0px; background-color: #f5f5f5;">
                                                                <div class="progress-bar progress-bar-success" style='background-color: #5cb85c; line-height: 20px; line-height: 20px; text-align: center; height: 100%; float: left; <%#"width:"+Eval("Percent")+"%;" %>' role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                                                            </div>
                                                            <label style="height: 20px; line-height: 20px; position: relative; top: -4px;"><%#Eval("count")+"人" %></label>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <div style="clear: both;"></div>
                                                    <div id="msg_op_btn_div" runat="server"></div>
                                                </div>
                                                <div style="padding: 5px;"><strong><%#GetVoteBottom() %></strong></div>
                                                <div style="padding-bottom: 5px;"><%#Eval("MsgContent") %></div>
                                            </div>
                                            <div id="longarticle" runat="server" visible="false">
                                                <div class="subtitle grayremind" style="max-height: 113px; margin-right: 2px; cursor: pointer; font-size: 12px; color: #A0A0A0;">
                                                    <%#Eval("Title") %><div style="clear: both;"></div>
                                                    <input type="button" value="浏览全文" onclick="ShowLong(<%#Eval("ID")%>);" />
                                                </div>
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
                </div>
            </div>
        </div>
    </form>
</body>
</html>
