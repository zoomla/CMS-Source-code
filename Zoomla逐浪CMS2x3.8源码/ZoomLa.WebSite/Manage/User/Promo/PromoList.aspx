<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PromoList.aspx.cs" Inherits="Manage_User_Promo_PromoList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>推广列表</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
        <li><a href='<%=CustomerPageAction.customPath2  %>'Main.aspx'>工作台</a></li><li><a href='UserManage.aspx'>用户管理</a></li><li><a href='<%=Request.RawUrl %>'>推广中心</a></li>
         <div id="help" class="pull-right text-center"><a href="javascript::" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a></div>
        <div id="sel_box" class="padding5">
            <div class="input-group text_300" style="float:left;">
                <asp:TextBox ID="Search_T" runat="server" CssClass="form-control" placeholder="用户名"></asp:TextBox>
                <span class="input-group-btn">
                    <asp:LinkButton ID="Search_Btn" runat="server" CssClass="btn btn-default" OnClick="Search_Btn_Click"><i class="fa fa-search"></i></asp:LinkButton>
                </span>
            </div>
        </div>
    </ol>
    <div class="dashed">
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
    <div class="margin_t5">
        <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false"
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无推广数据">
        <Columns>
            <asp:BoundField HeaderText="排名" DataField="RowNum" ItemStyle-CssClass="td_m" />
            <asp:TemplateField HeaderText="用户名">
                <ItemTemplate>
                    <a href="../UserInfo.aspx?id=<%#Eval("UserID") %>" title="用户信息"><%#Eval("UserName") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="邮箱" DataField="Email" />
            <asp:BoundField HeaderText="推广人数" DataField="PCount" />
            <asp:BoundField HeaderText="注册时间" DataField="RegTime" DataFormatString="{0:yyyy年MM月dd日}" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                        <a href="PromoUserList.aspx?PUserID=<%#Eval("UserID") %>" class="option_style"><i class="fa fa-user" title="下线会员"></i>下线会员</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
        <div class="alert alert-info" role="alert"><i class="fa fa-lightbulb-o"></i> 推广代码格式：/User/Register.aspx?ParentUserID=用户ID</div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        .dashed { border-bottom: 1px dashed #ccc;padding-bottom:5px;}
        .chart_ifr {border:none;overflow:hidden;}
    </style>
    <script src="/JS/SelectCheckBox.js"></script>
    <script>
        $(function () {
            $("#sel_btn").click(function (e) {
                if ($("#sel_box").css("display") == "none") {
                    $(this).addClass("active");
                    $("#sel_box").slideDown(300);
                }
                else {
                    $(this).removeClass("active");
                    $("#sel_box").slideUp(200);
                }
            });
        });
    </script>
</asp:Content>