<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowPic.aspx.cs" Inherits=" ZoomLa.WebSite.User.UserZone.Pic.ShowPic" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<title>会员中心 >> 我的相册</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<div class="us_topinfo">
<a title="会员中心" href='<%=ResolveUrl("~/User/Default.aspx")%>' target="_blank">会员中心</a> &gt;&gt;><a title="我的相册" href='<%=ResolveUrl("~/User/UserZone/Pic/CategList.aspx")%>'>我的相册</a>&gt;&gt;<%=CategName %> 
</div>
  <uc1:WebUserControlTop id="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
  <br />
  <div>
    <table width="760px" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td><h2> <%=Name %> 的相册 相册名：<%=CategName %> </h2></td>
      </tr>
      <tr>
        <td align="center">&nbsp;
          <table width="300px">
            <tr>
              <td><asp:Label ID="labCount" runat="server"></asp:Label></td>
              <td id="tdUp" runat="server" align="center"></td>
              <td id="tdDown" runat="server" align="center"></td>
            </tr>
          </table></td>
      </tr>
      <tr>
        <td align="center"><asp:Image ID="img" runat="server" /></td>
      </tr>
      <asp:Panel ID="Panel1" runat="server" Width="760px">
        <tr>
          <td align="center"><table width="400px">
              <tr>
                <td style="height: 35px"><a href='EditPic.aspx?picID=<%=Request.QueryString["picID"].ToString()%>'>编辑这张相片</a></td>
                <td style="height: 35px"><asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" OnClientClick="return confirm('你确定要删除这张相片吗？')" CausesValidation="false">删除这张照片</asp:LinkButton></td>
              </tr>
              <tr>
                <td colspan="2" style="height: 35px"><asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" CausesValidation="false">把这张照片设为相册封面</asp:LinkButton></td>
              </tr>
            </table></td>
        </tr>
        </asp:Panel>
      <tr>
        <td> 发表评论 </td>
      </tr>
      <tr>
        <td><ZL:ExGridView ID="EGV" runat="server" PageSize="20" DataKeyNames="ID" AutoGenerateColumns="False">
            <Columns>
            <asp:TemplateField>
              <ItemTemplate>
                <table width="760" border="1">
                  <tr>
                    <td style="width: 140px" rowspan="2" align="center"><table>
                        <tr>
                          <td style="height: 80px"><a>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%#DataBinder.Eval(Container.DataItem,"UserPic") %>' />
                            </a></td>
                        </tr>
                        <tr>
                          <td><a> <%#DataBinder.Eval(Container.DataItem,"UserName")%> </a></td>
                        </tr>
                      </table></td>
                    <td><table width="100%">
                        <tr>
                          <td align="left" style="color: gray"><%#DataBinder.Eval(Container.DataItem, "CritiqueTime")%></td>
                          <td align="right"><asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" OnClientClick="return confirm('你确定要删除这个评论吗？')" CausesValidation="false">删除</asp:LinkButton></td>
                        </tr>
                      </table></td>
                  </tr>
                  <tr>
                    <td style="height: 100px" valign="top"><%#DataBinder.Eval(Container.DataItem, "CritiqueContent")%></td>
                  </tr>
                </table>
              </ItemTemplate>
            </asp:TemplateField>
            </Columns>
          </ZL:ExGridView>
          &nbsp; </td>
      </tr>
      <tr>
        <td align="center"><%=UserName %></td>
      </tr>
      <tr>
        <td align="center" style="height: 172px"><textarea id="TextArea1" style="width: 400px; height: 150px" runat="server"></textarea>
          <br />
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入要发表的评论" ControlToValidate="TextArea1"></asp:RequiredFieldValidator></td>
      </tr>
      <tr>
        <td align="center"><asp:Button ID="Button1" runat="server" Text="发  表" OnClick="Button1_Click" /></td>
      </tr>
    </table>
  </div>
</form>
</body>
</html>