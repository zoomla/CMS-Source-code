<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="SelfLogManage.aspx.cs" Inherits="SelfLogManage" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>我的日志</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="zone"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/Userzone/Default.aspx">我的空间</a></li>
        <li class="active">我的日志</li>
    </ol>
</div>
  <div class="container btn_green">
      <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
  </div> 
<div class="container btn_green">
    <table class="table table-bordered table-striped">
        <tr>
            <td rowspan="2" class="text-center" style="width: 110px; vertical-align:middle;">
                <span class="fa fa-book" style="font-size:2em;color:#66CCFF;"></span>
            </td>
            <td>&nbsp;我的日志 </td>
        </tr>
        <tr>
            <td valign="top" style="height: 16px">&nbsp;<a href="CreatLog.aspx">写新日志</a> &nbsp;|&nbsp;
        <asp:LinkButton ID="lbtnManage" runat="server">我的日志</asp:LinkButton></td>
        </tr>
    </table>
        <div class="input-group" style="width: 403px;">
            <asp:DropDownList ID="SearchType_Dlist" CssClass="form-control text_150" Style="border-right: none;" runat="server">
                <asp:ListItem Value="0">按日期查找</asp:ListItem>
                <asp:ListItem Value="1">按关键字查找</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="Keys_T" CssClass="form-control text_md" runat="server"></asp:TextBox>
            <span class="input-group-btn">
                <asp:Button ID="Search_B" runat="server" class="btn btn-primary" Text="搜索" OnClick="Search_B_Click" />
            </span>
        </div>
    <table class="table table-striped table-bordered">
        <tr>
            <td style="width: 80%" bgcolor="#FFFFFF" valign="top">
                <ZL:ExGridView ID="gvLog" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvLog_RowDataBound" GridLines="None" CellPadding="4" PageSize="10" DataKeyNames="ID" AllowPaging="True" OnPageIndexChanging="gvLog_PageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="日记" DataField="LogTitle" />
                        <asp:BoundField HeaderText="时间" DataField="CreatDate" />
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <a href="CreatLog.aspx?LogID=<%#DataBinder.Eval(Container.DataItem,"ID")%>">编辑</a> &nbsp;|&nbsp;
                                <asp:LinkButton ID="lbtnDelete" runat="server" OnClick="lbtnDelete_Click" OnClientClick="return confirm('确定删除吗？');">删除</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField>
                            <ItemTemplate>
                                <table class="table table-bordered">
                                    <tr>
                                        <td class="text-center" colspan="2">
                                            我的日志
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table class="table table-striped table-bordered table-hover">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LogTitle") %>'></asp:Label></td>
                                                    <td>
                                                        <a href="CreatLog.aspx?LogID=<%#DataBinder.Eval(Container.DataItem,"ID")%>">编辑</a> &nbsp;|&nbsp;
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" OnClick="lbtnDelete_Click" OnClientClick="return confirm('确定删除吗？');">删除</asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CreatDate") %>'></asp:Label>
                                                        (分类:
                                                        <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LogTypeID") %>'></asp:Label>
                                                        ) </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LogContext") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>   
                                                        <a href="LogReadCriticism.aspx?LogID=<%#DataBinder.Eval(Container.DataItem,"LogTypeID")%>">阅读</a>(
                                                        <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ReadCount") %>'></asp:Label>
                                                        ) 
                                                    </td>
                                                    <td>
                                                        <a href="LogReadCriticism.aspx?LogID=<%#DataBinder.Eval(Container.DataItem,"ID")%>">评论</a>(
                                                        <asp:Label ID="Label6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CriticismCount") %>'></asp:Label>
                                                        ) 
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </ZL:ExGridView>
            </td>
            <td valign="top" bgcolor="Silver">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <br />
                            日志分类 </td>
                    </tr>
                    <tr>
                        <td>
                            <ZL:ExGridView ID="gvLogType" runat="server" AutoGenerateColumns="False" Width="100%"
                                GridLines="None" CellPadding="4" DataKeyNames="ID">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnLogTypeList" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LogTypeName") %>' OnClick="lbtnLogTypeList_Click"></asp:LinkButton>
                                            (
                    <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LogCount") %>'></asp:Label>
                                            )
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </ZL:ExGridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>日志存档 </td>
                    </tr>
                    <tr>
                        <td>
                            <ZL:ExGridView ID="gvdateLog" PageSize="20" runat="server" AutoGenerateColumns="False" Width="100%"
                                GridLines="None" CellPadding="4">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lbtnLogDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CreatDate","{0:yyyy年MM月dd日}") %>' OnClick="lbtnLogDate_Click"></asp:LinkButton>
                                                        (
                          <asp:Label ID="labDateCount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ReadCount") %>'></asp:Label>
                                                        ) </td>
                                                </tr>
                                                <tr>
                                                    <td style="background: 247,247,247"></td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </ZL:ExGridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div class="s_bright" id="dwindow" style="position: absolute; top: 0px; left: 0px; width: 60%; height: 400px; display: none; filter: alpha(opacity=70); background-color: Gray; padding-top: 10px; margin-top: 50px; margin-left: 100px;"
        runat="server">
        <div class="i_r_ftitle">登录会员</div>
        <div class="i_r_fbody">
            <h1>请输入正确的用户名与密码 <font color='red'>
        <label id="LbAlert" runat="server" />
        </font></h1>
            <div class="cleardiv" style="height: 30px;"></div>
            <ul>
                <li style="width: 150px; text-align: right;"><b>用户名：</b></li>
                <li>
                    <asp:TextBox ID="TxtUserName" class="input1" MaxLength="20" runat="server"></asp:TextBox>
                </li>
            </ul>
            <div class="cleardiv"></div>
            <ul>
                <li style="width: 150px; text-align: right;"><b>密码：</b></li>
                <li>
                    <asp:TextBox ID="TxtPassword" runat="server" class="input1" TextMode="Password"></asp:TextBox>
                </li>
            </ul>
            <asp:PlaceHolder ID="PhValCode" runat="server">
                <ul>
                    <li style="width: 150px; text-align: right;"><b>验证码：</b></li>
                    <li>
                        <asp:TextBox ID="TxtValidateCode" class="input1" MaxLength="6" runat="server"></asp:TextBox>
                        <asp:Image ID="VcodeLogin" runat="server" ImageUrl="~/Common/ValidateCode.aspx" Height="20px" />
                    </li>
                </ul>
            </asp:PlaceHolder>
            <ul>
                <li style="width: 150px; text-align: right;"><b>Cookie：</b></li>
                <li>
                    <asp:DropDownList ID="DropExpiration" runat="server">
                        <asp:ListItem Value="Day" Text="保存一天"></asp:ListItem>
                        <asp:ListItem Value="Month" Text="保存一月"></asp:ListItem>
                        <asp:ListItem Value="Year" Text="保存一年"></asp:ListItem>
                    </asp:DropDownList>
                </li>
            </ul>
            <div class="cleardiv"></div>
            <ul>
                <li style="width: 150px; text-align: right;">&nbsp;</li>
                <li>
                    <asp:ImageButton ID="IbtnEnter" ImageUrl="../../images/login.gif" runat="server"
                        OnClick="IbtnEnter_Click" />
                    <a href="../../Register.aspx">
                        <img src="../../images/reg1.gif" alt="" /></a> </li>
            </ul>
            <div class="cleardiv"></div>
            <ul>
                <li style="width: 150px; text-align: right;">&nbsp;</li>
                <li><a href="/User/GetPassword.aspx">忘记密码了？ </a></li>
                <li>如果您尚未在本站注册为用户，请先<a href="../../Register.aspx">点此注册</a> 。</li>
            </ul>
        </div>
    </div>
</div>
</asp:Content>
