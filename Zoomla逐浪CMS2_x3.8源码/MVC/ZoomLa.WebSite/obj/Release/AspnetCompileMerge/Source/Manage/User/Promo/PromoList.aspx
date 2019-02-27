<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PromoList.aspx.cs" Inherits="ZoomLaCMS.Manage.User.Promo.PromoList" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head"><title>推广列表</title><style>#sel_box .width172{width:172px;}.allchk_l{display:none;}</style></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
        <li><a href='<%=CustomerPageAction.customPath2  %>Main.aspx'>工作台</a></li>
        <li><a href='UserManage.aspx'>用户管理</a></li>
        <li><a href='<%=Request.RawUrl %>'>推广中心</a></li>
        <div id="help" class="pull-right text-center"><a href="javascript::" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a></div>
        <div id="sel_box" runat="server" class="padding5">
            <div class="input-group text_300" style="float: left;">
                <asp:DropDownList ID="SType_DP" runat="server" CssClass="form-control" Style="width: 90px; border-right: none;">
                    <asp:ListItem Selected="True" Value="uname" Text="用户名"></asp:ListItem>
                    <asp:ListItem Value="userid" Text="用户ID"></asp:ListItem>
                    <asp:ListItem Value="truename" Text="真实姓名"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="Search_T" runat="server" CssClass="form-control width172" placeholder="用户名"></asp:TextBox>
                <span class="input-group-btn">
                    <asp:LinkButton ID="Search_Btn" runat="server" CssClass="btn btn-default" OnClick="Search_Btn_Click"><i class="fa fa-search"></i></asp:LinkButton>
                </span>
            </div>
        </div>
    </ol>
    <div id="template" runat="server">
        <div class="dashed" style="margin-top: 8px;">
            <strong>年份：</strong>
            <div class="btn-group" id="years_div">
                <asp:Literal ID="Years_Li" runat="server"></asp:Literal>
            </div>
        </div>
        <div class="margin_t5 dashed">
            <strong>月份：</strong>
            <div class="btn-group">
                <asp:Literal ID="Months_Li" runat="server"></asp:Literal>
            </div>
        </div>
        <div>
            <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false"
                OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
                CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无推广数据">
                <Columns>
                    <asp:BoundField HeaderText="排名" DataField="RowNum" ItemStyle-CssClass="td_m" />
                    <asp:BoundField HeaderText="ID" DataField="UserID" ItemStyle-CssClass="td_m" />
                    <asp:TemplateField HeaderText="用户名">
                        <ItemTemplate>
                            <a href="../UserInfo.aspx?id=<%#Eval("UserID") %>" title="用户信息"><%#Eval("UserName") %></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="邮箱" DataField="Email" />
                    <asp:TemplateField HeaderText="推荐人">
                        <ItemTemplate>
                            <%#GetParentUserName() %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="推广人数">
                        <ItemTemplate>
                            <%#ZoomLa.Common.DataConverter.CLng(Eval("PCount")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="注册时间" DataField="RegTime" DataFormatString="{0:yyyy年MM月dd日}" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <%#GetOper() %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </ZL:ExGridView>
            <div class="alert alert-info" role="alert"><i class="fa fa-lightbulb-o"></i>推广代码格式：/User/Register?ParentUserID=用户ID</div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        .dashed { border-bottom: 1px dashed #ccc; bottom:5px;}
        .chart_ifr {border:none;overflow:hidden;}
    </style>
    <script src="/JS/SelectCheckBox.js"></script>
    <script>
        $(function () {
            $("#sel_btn").click(function (e) {
                if ($("#sel_box").css("display") == "none") {
                    $(this).addClass("active");
                    $("#sel_box").slideDown(300);
                    $("#template").css("margin-top", "44px");
                }
                else {
                    $(this).removeClass("active");
                    $("#sel_box").slideUp(200);
                    $("#template").css("margin-top", "0px");
                }
            });
        });
    </script>
</asp:Content>
