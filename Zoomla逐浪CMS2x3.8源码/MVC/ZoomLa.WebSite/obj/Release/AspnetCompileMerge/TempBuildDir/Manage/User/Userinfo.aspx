<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Userinfo.aspx.cs" Inherits="ZoomLaCMS.Manage.User.Userinfo" MasterPageFile="~/Manage/I/Default.master" %>

<%@ Import Namespace="ZoomLa.Model" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>会员信息</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ul class="nav nav-tabs">
        <li class="active"><a href="#Tabs0" data-toggle="tab">基本信息</a></li>
        <li><a href="#Tabs1" data-toggle="tab">联系信息</a></li>
        <li><a href="#Tabs2" data-toggle="tab">其他信息</a></li>
        <li><a href="#Tabs4" data-toggle="tab">权限角色</a></li>
    </ul>
    <div class="tab-content">
        <div class="tab-pane active" id="Tabs0">
            <table class="table table-striped table-bordered table-hover">
                <tbody>
                    <asp:Repeater ID="UInfo_RPT" runat="server" OnItemCommand="UInfo_RPT_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td style="width: 15%;" class="text-right">用户名： </td>
                                <td colspan="3">
                                    <img title="/Images/userface/noface.png" style="width: 40px; height: 40px;" src="<%=mu.UserFace %>" onerror="shownoface(this);">
                                    <span><%#Eval("UserName") %></span>
                                    <a href="Addon/UserLogin.aspx?uname=<%#HttpUtility.UrlEncode(Eval("UserName",""))+"&upwd="+Eval("UserPwd") %>" class="margin_l5" target="_blank"><i class="fa fa-external-link"></i>以此会员登录</a>
                                    <a href="UserModify.aspx?UserID=<%#Eval("UserID") %>" class="btn btn-default margin_l5" title="修改信息"><i class="fa fa-pencil"></i>修改</a>
                                    <%if (!pmu.IsNull)
                                      {%><a href="Userinfo.aspx?id=<%:mu.ParentUserID %>" class="btn btn-default" title="点击查看"><i class="fa fa-user"></i> 推荐人：<%=pmu.UserName+"("+pmu.UserID+")" %></a><%} %>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-right">资金管理：</td>
                                <td colspan="3">
                                    <a href="UserExp.aspx?UserID=<%#Eval("UserID") %>&type=1" class="btn btn-default" title="管理资金">资金：<%#Eval("Purse") %></a>
                                    <a href="UserExp.aspx?UserID=<%#Eval("UserID") %>&type=2" class="btn btn-default" title="管理银币">银币：<%#Eval("SilverCoin") %></a>
                                    <a href="UserExp.aspx?UserID=<%#Eval("UserID") %>&type=3" class="btn btn-default" title="管理积分">积分：<%#Eval("UserExp") %></a>
                                    <a href="UserExp.aspx?UserID=<%#Eval("UserID") %>&type=4" class="btn btn-default" title="管理点卷">点卷：<%#Eval("UserPoint") %></a>
                                    <a href="UserExp.aspx?UserID=<%#Eval("UserID") %>&type=5" class="btn btn-default" title="管理虚拟币">虚拟币：<%#Eval("DummyPurse") %></a>
                                    <a href="UserExp.aspx?UserID=<%#Eval("UserID") %>&type=6" class="btn btn-default" title="管理信誉值">信誉值：<%#Eval("UserCreit") %></a>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-right">用户信息： </td>
                                <td colspan="3">
                                    <div class="input-group suinfo">
                                        <span class="input-group-addon">ID</span>
                                        <label class="form-control"><%#Eval("UserID") %></label>
                                        <span class="input-group-addon">会员组</span>
                                        <label class="form-control"><%#GetGroupName(Eval("GroupID","{0}")) %></label>
                                        <span class="input-group-addon">昵称</span>
                                        <label class="form-control"><%#mu.HoneyName %></label>
                                        <span class="input-group-addon">Email</span>
                                        <label class="form-control"><%#Eval("Email") %></label>
                                        <span class="input-group-addon">手机</span>
                                        <label class="form-control"><%:string.IsNullOrEmpty(basemu.Mobile)?"未设置":basemu.Mobile %></label>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td style="width: 15%;" class="text-right">提示问题： </td>
                                <td><%:mu.Question %></td>
                                <td class="text-right">问题答案：</td>
                                <td style="width: 35%"><%:mu.Answer %></td>
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
                                <td class="text-right">已发布文章数(统计数)： </td>
                                <td>共计<%=GetCount()%>篇</td>
                                <td class="text-right">云盘空间(占用数)： </td>
                                <td><%=GetCloud()%>
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
                                <td class="text-right" style="width: 15%;">真实姓名： </td>
                                <td><%:mu.TrueName%></td>
                                <td class="text-right">性别： </td>
                                <td style="width: 35%;"><%#(Eval("UserSex","{0}")=="True") ? "男" : "女" %></td>
                            </tr>
                            <tr>
                                <td class="text-right">出生日期： </td>
                                <td><%#Eval("BirthDay")%></td>
                                <td class="text-right">办公电话： </td>
                                <td><%#Eval("OfficePhone")%></td>
                            </tr>
                            <tr>
                                <td class="text-right">家庭电话： </td>
                                <td><%#Eval("HomePhone")%></td>
                                <td class="text-right">传真： </td>
                                <td><%#Eval("Fax")%></td>
                            </tr>
                            <tr>
                                <td class="text-right">省市县：  </td>
                                <td><%#Eval("Province") %> - <%#Eval("City") %> - <%#Eval("County") %></td>
                                <td class="text-right">QQ号码： </td>
                                <td><%#Eval("QQ") %></td>
                            </tr>
                            <tr>
                                <td class="text-right">联系地址： </td>
                                <td><%#Eval("Address") %></td>
                                <td class="text-right">邮政编码： </td>
                                <td><%#Eval("ZipCode") %></td>
                            </tr>
                            <tr>
                                <td class="text-right">身份证号码： </td>
                                <td><%#Eval("IDCard") %></td>
                                <td class="text-right">个人主页： </td>
                                <td><%#Eval("HomePage") %></td>
                            </tr>
                            <tr>
                                <td class="text-right">头像宽度： </td>
                                <td><%#Eval("FaceWidth") %></td>
                                <td class="text-right">头像高度： </td>
                                <td><%#Eval("FaceHeight") %></td>
                            </tr>

                            <tr>
                                <td class="text-right">公司名称： </td>
                                <td style="height: 22px"><%# mu.CompanyName%></td>
                                <td class="text-right">公司简介： </td>
                                <td><%#mu.CompanyDescribe %></td>
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
                        <td class="text-right">商铺认证有效期截止： </td>
                        <td>
                            <asp:Label ID="txtCerificateDeadLine" runat="server"></asp:Label></td>
                        <td class="text-right">有效期截止时间： </td>
                        <td>
                            <asp:Label ID="txtDeadLine" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <tr>
                        <td class="text-right">签名档： </td>
                        <td>
                            <asp:Label ID="tbSign" runat="server" TextMode="MultiLine" Width="253px"
                                Height="60"></asp:Label></td>
                        <td class="text-right">隐私设定： </td>
                        <td>
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
        <div class="tab-pane" id="Tabs4">
            <table class="table table-striped table-bordered table-hover">
                <tr>
                    <td style="width: 200px;">管理员角色设置：</td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cblRoleList" runat="server" /></td>
                </tr>
                <tr>
                    <td>当前会员组：</td>
                    <td>
                        <asp:DropDownList runat="server" ID="UserGroup_DP" OnSelectedIndexChanged="UserGroup_DP_SelectedIndexChanged" AutoPostBack="true" DataTextField="GroupName" DataValueField="GroupID" CssClass="form-control text_300"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>特许商品：</td>
                    <td>
                        <input type="button" value="选择商品" onclick="upro.showdiag();" class="btn btn-info" />
                        <asp:Button runat="server" ID="AddUPro_Btn" OnClick="AddUPro_Btn_Click" class="btn btn-info" Text="保存修改" />
                        <div>
                            <asp:Repeater runat="server" ID="UPRONode_RPT" OnItemDataBound="UPRONode_RPT_ItemDataBound">
                                <ItemTemplate>
                                    <div>
                                        <div class="up_nodeitem"><%#Eval("NodeName") %></div>
                                        <asp:Repeater runat="server" ID="UPro_RPT">
                                            <ItemTemplate>
                                                <div class="btn btn-default up_proitem">
                                                    <div class="proname"><%#Eval("Proname") %></div>
                                                    <a href="javascript:;" style="z-index: 10;" class="pull-right" onclick="upro.del(this,'<%#Eval("ID") %>');"><i class="fa fa-trash-o"></i></a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <asp:HiddenField runat="server" ID="UProIDS_Hid" />
                        <style type="text/css">
                            .up_nodeitem { padding-bottom: 5px; margin-bottom: 5px; margin-top: 5px; border-bottom: 1px solid #ddd; font-weight: bolder; }
                            .up_proitem { width: 160px; }
                            .up_proitem .proname { width: 120px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; display: inline-block; }
                        </style>
                        <script>
                            var upro = {};
                            upro.showdiag = function () { ShowComDiag("<%:customPath2%>Shop/ProductsSelect.aspx?callback=selupro", "选择商品"); }
                            upro.del = function (btn, proid) {
                                var ids = $("#UProIDS_Hid").val();
                                ids = ids.replace("," + proid + ",", ",");
                                $("#UProIDS_Hid").val(ids);
                                $(btn).closest(".up_proitem").remove();
                            }
                            function selupro(list) {
                                list = JSON.parse(list);
                                var ids = list.GetIDS("id");
                                $("#UProIDS_Hid").val($("#UProIDS_Hid").val() + "," + ids);
                                $("#AddUPro_Btn").click();
                            }
                        </script>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="text-center">
        <a href="UserModify.aspx?UserID=<%=UserID %>" class="btn btn-primary" title="修改信息"><i class="fa fa-pencil"></i>修改</a>
        <asp:Button ID="LockUser" CssClass="btn btn-primary" runat="server" Text="禁止登录" OnClick="LockUser_Click" />
        <asp:Button ID="ClearCode_B" runat="server" Text="取消验证器" OnClick="ClearCode_B_Click" CssClass="btn btn-primary" />
        <asp:Button runat="server" ID="UPClient_Btn" Text="升级为客户" class="btn btn-primary" OnClick="UPClient_Btn_Click" />
        <input type="button" class="btn btn-primary" value="能力中心信息" onclick="ShowComDiag('Addon/UPToPlat.aspx?ID=<%:UserID%>','能力信息');" />
        <asp:Button runat="server" ID="Approve_B" Text="聚合认证" class="btn btn-primary" OnClick="Approve_B_Click" Visible="false" />
        <asp:Button runat="server" ID="ApproveFailure_B" Text="取消认证" class="btn btn-primary" OnClick="ApproveFailure_B_Click" Visible="false" />
        <asp:Button ID="DelUserPost_Btn" runat="server" Text="删除用户帖子" OnClientClick="return confirm('确定要删除吗!!');" OnClick="DelUserPost_Btn_Click" class="btn btn-primary" />
        <a href="../Shop/OrderList.aspx?UserID=<%=UserID %>" class="btn btn-primary">用户订单</a>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        .suinfo label { min-width: 90px; }
    </style>
    <script src="/JS/DatePicker/WdatePicker.js"></script>
    <script src="/JS/Modal/APIResult.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/JS/Controls/ZL_Array.js"></script>
    <script src="/JS/ZL_Content.js"></script>
    <script>
        function author(url) {
            if (confirm('是否升级为作者？'))
                location.href = url;
        }
        $().ready(function () {
            $("[href='#<%=Request.QueryString["tabs"] %>']").tab('show');
        });
            function closeDiag() { CloseComDiag(); }
    </script>
</asp:Content>
