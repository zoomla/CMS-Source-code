<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Userinfo.aspx.cs" Inherits="ZoomLaCMS.Manage.User.Userinfo"  MasterPageFile="~/Manage/I/Default.master" %>
<%@ Import Namespace="ZoomLa.Model" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>会员信息</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ul class="nav nav-tabs">
    <li class="active"><a href="#Tabs0" data-toggle="tab">基本信息</a></li>
    <li><a href="#Tabs1" data-toggle="tab">联系信息</a></li>
    <li><a href="#Tabs2" data-toggle="tab">其他信息</a></li>
    <li><a id="platInfo_A" href="#Tabs3" data-toggle="tab" visible="false" runat="server">能力中心信息</a></li>
    <li><a href="#Tabs4" data-toggle="tab">权限角色</a></li>
</ul>
<div class="tab-content">
    <div class="tab-pane active" id="Tabs0">
        <table class="table table-striped table-bordered table-hover">
            <tbody>
                <asp:Repeater ID="UInfo_RPT" runat="server" OnItemCommand="UInfo_RPT_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 15%; height: 25px;" class="text-right">用户名： </td>
                            <td style="width: 35%; height: 25px;">
                                <%#Eval("UserName") %> &nbsp;
                                            <a href="Addon/UserLogin.aspx?uname=<%#HttpUtility.UrlEncode(Eval("UserName",""))+"&upwd="+Eval("UserPwd") %>" target="_blank"><i class="fa fa-external-link"></i>以此会员登录</a>
                            </td>
                            <td style="width: 15%;" class="text-right">会员组： </td>
                            <td style="width: 35%;"><%# GetGroupName(Eval("GroupID","{0}")) %>
                                <%#GetIsAuthor(Eval("UserID")) %>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-right">昵称： </td>
                            <td><%#GetHoneyName() %></td>
                            <td class="text-right">剩余资金： </td>
                            <td><%#Eval("Purse") %></td>
                        </tr>

                        <tr>
                            <td class="text-right">Email： </td>
                            <td><%#Eval("Email")%></td>
                            <td class="text-right">用户信誉值：</td>
                            <td><%#Eval("UserCreit")%></td>
                        </tr>
                        <tr>
                            <td style="width: 15%;" class="text-right">剩余银币： </td>
                            <td style="width: 35%;"><%#Eval("SilverCoin")%></td>
                            <td style="width: 15%;" class="text-right">用户积分： </td>
                            <td style="width: 35%;"><%#Eval("UserExp") %></td>

                        </tr>
                        <tr>
                            <td style="width: 15%;" class="text-right">提示问题： </td>
                            <td style="width: 35%;"><%:mu.Question %></td>
                            <td style="width: 15%;" class="text-right">问题答案：</td>
                            <td style="width: 35%"><%:mu.Answer %></td>

                        </tr>
                        <tr>
                            <td style="width: 15%;" class="text-right">点券： </td>
                            <td style="width: 35%;"><%#Eval("UserPoint")%></td>
                            <td style="width: 15%;" class="text-right">卖家积分： </td>
                            <td style="width: 35%;"><%#Eval("boffExp") %></td>
                        </tr>
                        <tr>
                            <td class="text-right">消费积分： </td>
                            <td><%#Eval("ConsumeExp")%></td>
                            <td class="text-right">有效期截止时间： </td>
                            <td><%#Eval("DeadLine","{0}") %></td>
                        </tr>
                        <tr>
                            <td class="text-right">最后登录时间： </td>
                            <td><%#Eval("LastLoginTimes","{0:yyyy年MM月dd日 HH:mm}")%></td>
                            <td class="text-right">最后登录IP： </td>
                            <td><%#Eval("LastLoginIP")+"("+GetIpLocation(Eval("LastLoginIP").ToString())+")" %></td>
                        </tr>
                        <tr>
                            <td class="text-right">最近活跃时间： </td>
                            <td><%#Eval("LastActiveTime","{0:yyyy年MM月dd日 HH:mm}")%></td>
                            <td class="text-right">密码修改时间： </td>
                            <td><%#Eval("LastPwdChangeTime") %></td>
                        </tr>
                        <tr>
                            <td style="width: 15%;" class="text-right">已发布文章数(统计数)： </td>
                            <td style="width: 35%;">共计<%=GetCount()%>篇</td>
                            <td style="width: 15%;" class="text-right">云盘空间(占用数)： </td>
                            <td style="width: 35%;"><%=GetCloud()%>
                                <asp:LinkButton ID="CloudManage" runat="server" OnClick="CloudManage_Click" ForeColor="Red">&nbsp;[<%=cloud()%>]</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <div class="tab-pane" id="Tabs1">
        <table class="table table-striped table-bordered table-hover">
            <tbody>
                <asp:Repeater ID="BaseMU_RPT" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 15%;" class="text-right">真实姓名： </td>
                            <td style="width: 35%;"><%:mu.TrueName%></td>
                            <td style="width: 15%;" class="text-right">性别： </td>
                            <td style="width: 35%;"><%#(Eval("UserSex","{0}")=="True") ? "男" : "女" %></td>
                        </tr>
                        <tr>
                            <td style="width: 15%;" class="text-right">出生日期： </td>
                            <td style="width: 35%;"><%#Eval("BirthDay")%></td>
                            <td style="width: 15%;" class="text-right">办公电话： </td>
                            <td style="width: 35%;"><%#Eval("OfficePhone")%></td>
                        </tr>
                        <tr>
                            <td style="width: 15%;" class="text-right">家庭电话： </td>
                            <td style="width: 35%;"><%#Eval("HomePhone")%></td>
                            <td style="width: 15%;" class="text-right">传真： </td>
                            <td style="width: 35%;"><%#Eval("Fax")%></td>
                        </tr>
                        <tr>
                            <td class="text-right">手机号码： </td>
                            <td><%#Eval("Mobile") %></td>
                            <td class="text-right">小灵通： </td>
                            <td><%#Eval("PHS") %></td>
                        </tr>
                        <tr>
                            <td style="width: 15%;" class="text-right">省市县：  </td>
                            <td style="width: 35%;"><%#Eval("Province") %> - <%#Eval("City") %> - <%#Eval("County") %></td>
                            <td style="width: 15%;" class="text-right">推荐人： </td>
                            <td style="height: 22px"><%#GetParentUser()%></td>
                        </tr>
                        <tr>
                            <td style="width: 15%;" class="text-right">联系地址： </td>
                            <td style="width: 35%;"><%#Eval("Address") %></td>
                            <td style="width: 15%;" class="text-right">邮政编码： </td>
                            <td style="width: 35%;"><%#Eval("ZipCode") %></td>
                        </tr>
                        <tr>
                            <td style="width: 15%;" class="text-right">身份证号码： </td>
                            <td style="width: 35%;"><%#Eval("IDCard") %></td>
                            <td style="width: 15%;" class="text-right">个人主页： </td>
                            <td style="width: 35%;"><%#Eval("HomePage") %></td>
                        </tr>
                        <tr>
                            <td style="width: 15%;" class="text-right">QQ号码： </td>
                            <td style="width: 35%;"><%#Eval("QQ") %></td>
                            <td style="width: 15%;" class="text-right">头像地址： </td>
                            <td style="width: 35%; height: 26px;">
                                <img title="<%#Eval("UserFace") %>" style="width: 40px; height: 40px;" src="<%#Eval("UserFace") %>" onerror="shownoface(this);" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%;" class="text-right">头像宽度： </td>
                            <td style="width: 35%;"><%#Eval("FaceWidth") %></td>
                            <td style="width: 15%;" class="text-right">头像高度： </td>
                            <td style="width: 35%;"><%#Eval("FaceHeight") %></td>
                        </tr>

                        <tr>
                            <td style="width: 15%;" class="text-right">公司名称： </td>
                            <td style="height: 22px"><%# GetCompanyName(Eval("UserID","{0}")) %></td>
                            <td style="width: 15%;" class="text-right">公司简介： </td>
                            <td><%#GetCompanyDesc(Eval("UserID","{0}")) %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <div class="tab-pane" id="Tabs2">
        <table class="table table-striped table-bordered table-hover">
            <tbody>
                <tr>
                    <td style="width: 15%;" class="text-right">商铺认证有效期截止： </td>
                    <td style="width: 35%;">
                        <asp:Label ID="txtCerificateDeadLine" runat="server"></asp:Label></td>
                    <td style="width: 15%;" class="text-right">有效期截止时间： </td>
                    <td style="width: 35%;">
                        <asp:Label ID="txtDeadLine" runat="server"></asp:Label></td>
                </tr>
                <tr>
                <tr>
                    <td style="width: 15%;" class="text-right">签名档： </td>
                    <td style="width: 35%;">
                        <asp:Label ID="tbSign" runat="server" TextMode="MultiLine" Width="253px"
                            Height="60"></asp:Label></td>
                    <td style="width: 15%;" class="text-right">隐私设定： </td>
                    <td style="width: 35%;">
                        <asp:RadioButtonList ID="tbPrivacy" runat="server" Visible="false">
                            <asp:ListItem Selected="True" Value="0">公开信息</asp:ListItem>
                            <asp:ListItem Value="1">只对好友公开</asp:ListItem>
                            <asp:ListItem Value="2">完全保密</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Label runat="server" ID="Privancy" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="padding-left: 0;">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <asp:Literal ID="lblHtml" runat="server"></asp:Literal>
                        </table>
                    </td>
                </tr>
                <asp:Literal ID="ltlTab" runat="server"></asp:Literal>
            </tbody>
        </table>
    </div>
    <div class="tab-pane" id="Tabs3">
        <table class="table table-striped table-bordered table-hover padding0">
            <tr>
                <td style="width: 15%;" class="text-right">真实姓名：</td>
                <td style="width: 35%;">
                    <asp:Label ID="tbTrueName_L" runat="server"></asp:Label></td>
                <td style="width: 15%;" class="text-right">公司名称：</td>
                <td style="width: 35%;">
                    <asp:Label ID="tbCompName_L" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 15%;" class="text-right">公司部门：</td>
                <td style="width: 35%;">
                    <asp:Label ID="tbPost_L" runat="server"></asp:Label></td>
                <td style="width: 15%;" class="text-right">手机号码：</td>
                <td style="width: 35%;">
                    <asp:Label ID="tbPhone_L" runat="server"></asp:Label></td>
            </tr>
        </table>
    </div>
    <div class="tab-pane" id="Tabs4">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td style="width: 200px;"><strong>管理员角色设置：</strong></td>
                <td valign="top">
                    <asp:CheckBoxList ID="cblRoleList" runat="server" /></td>
            </tr>
        </table>
    </div>
</div>
<div class="text-center">
    <a href="UserModify.aspx?UserID=<%:UserID %>" class="btn btn-primary">修改信息</a>
    <asp:Button ID="LockUser" CssClass="btn btn-primary" runat="server" Text="禁止登录" OnClick="LockUser_Click" />
    <asp:Button ID="ClearCode_B" runat="server" Text="取消验证器" OnClick="ClearCode_B_Click" CssClass="btn btn-primary" />
    <a href="<%="UserExp.aspx?UserID=" +UserID + "&type=" + (int)M_UserExpHis.SType.Purse %>" class="btn btn-primary">资金管理</a>
    <a href="<%="UserExp.aspx?UserID=" +UserID + "&type=" + (int)M_UserExpHis.SType.SIcon %>" class="btn btn-primary">银币管理</a>
    <a href="<%="UserExp.aspx?UserID=" +UserID + "&type=" + (int)M_UserExpHis.SType.Point %>" class="btn btn-primary">积分管理</a>
    <a href="<%="UserExp.aspx?UserID=" +UserID + "&type=" + (int)M_UserExpHis.SType.UserPoint %>" class="btn btn-primary">点券管理</a>
    <a href="<%="UserExp.aspx?UserID=" +UserID + "&type=" + (int)M_UserExpHis.SType.DummyPoint %>" class="btn btn-primary">虚拟币管理</a>
    <a href="<%="UserExp.aspx?UserID=" +UserID + "&type=" + (int)M_UserExpHis.SType.Credit %>" class="btn btn-primary">信誉值管理</a>
    <asp:Button runat="server" ID="UPClient_Btn" Text="升级为客户" class="btn btn-primary" OnClick="UPClient_Btn_Click" />
    <asp:Button runat="server" ID="UpPlat_B" Text="升级为能力中心用户" class="btn btn-primary" OnClick="UpPlat_B_Click" Visible="false" />
    <asp:Button runat="server" ID="DownPlat_B" Text="移除能力中心用户" class="btn btn-primary" OnClick="DownPlat_B_Click" Visible="false" />
    <asp:Button runat="server" ID="Approve_B" Text="聚合认证" class="btn btn-primary" OnClick="Approve_B_Click" Visible="false" />
    <asp:Button runat="server" ID="ApproveFailure_B" Text="取消认证" class="btn btn-primary" OnClick="ApproveFailure_B_Click" Visible="false" />
    <asp:Button ID="DelUserPost_Btn" runat="server" Text="删除用户帖子" OnClientClick="return confirm('确定要删除吗!!');" OnClick="DelUserPost_Btn_Click" class="btn btn-primary" />
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
    function author(url) {
        if (confirm('是否升级为作者？'))
            location.href = url;
    }
    $().ready(function () {
        $("[href='#<%=Request.QueryString["tabs"] %>']").tab('show');
    });
</script>
</asp:Content>
