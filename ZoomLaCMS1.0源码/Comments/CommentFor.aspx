<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CommentFor.aspx.cs" Inherits="ZoomLa.WebSite.Comment.CommentFor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>图片显示-评论</title>
    <link href="../App_Themes/UserDefaultTheme/Default.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/UserDefaultTheme/xtree.css" type="text/css" rel="stylesheet" />
    <link href="../User/css/commentary.css" rel="stylesheet" type="text/css" />
    <link href="../User/css/default1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    
        <div id="Comment_List">
          <div class="r_navigation">
            <div class="r_n_pic"></div>
            评论总数：<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><span id="Span1"><span>支持：<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></span><span>反对：<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></span></span>
          </div>
          <asp:Repeater ID="Repeater1" runat="server">
              <ItemTemplate>
              <div class="ListLayout">
                <div class="SideBar"><%# GetUserName(Eval("UserID","{0}")) %></div>
                <div class="ListText">
                    <%# Eval("Contents")%>                    
                </div>
                <div class="SidePK"><%# GetPK(Eval("PK","{0}")) %></div>
                <div class="ListTime"><%# Eval("CommentTime") %> &gt;&gt;评分：<%# Eval("Score")%></div>
              </div>
              <div class="clearbox"></div>
              </ItemTemplate>
          </asp:Repeater>
        </div>
        <div id="CommentInput">
            <div class="r_navigation">
            <div class="r_n_pic"></div>
            发表评论&nbsp;&nbsp;<span id="Span2">本评论只代表网友个人观点，不代表本站观点。</span>
            </div>
            <div class="clearbox"></div>
            
            <div class="CommentPK"><asp:RadioButtonList ID="RBLPK" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">我支持</asp:ListItem>
                    <asp:ListItem Value="0">我反对</asp:ListItem>
                </asp:RadioButtonList>
            </div>                
            <div class="ListLayout1">
            <div class="Comment">内  容：</div>
            <div class="ContentRight">
                <asp:TextBox ID="TxtContents" runat="server" Rows="4" TextMode="MultiLine" Width="70%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="评论内容不能为空" ControlToValidate="TxtContents"></asp:RequiredFieldValidator>
            </div>
            <div class="CommentScore">评  分：</div>
            <div class="CommentRight">
                <asp:DropDownList ID="DDLScore" runat="server">
                    <asp:ListItem Value="1">1分</asp:ListItem>
                    <asp:ListItem Value="2">2分</asp:ListItem>
                    <asp:ListItem Value="3" Selected="True">3分</asp:ListItem>
                    <asp:ListItem Value="4">4分</asp:ListItem>
                    <asp:ListItem Value="5">5分</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="text-align:center;">
                <asp:HiddenField ID="HdnItemID" runat="server" />
                <asp:HiddenField ID="HdnTitle" runat="server" />
                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server" />
                <asp:Button ID="Button1" runat="server" Text="发表评论" OnClick="Button1_Click" /></div>
            </div>
        </div>
    
    </div>
    
    </form>
</body>
</html>
