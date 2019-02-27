<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeFile="Projects.aspx.cs" Inherits="manage_AddOn_Projects" ValidateRequest="false" %>

<asp:Content ContentPlaceHolderID="head" runat="Server"><title><%=lang.LF("查看项目")%></title></asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <style type="text/css">
        .inputgp {
            border-right-color: currentColor;
            border-right-width: medium;
            border-right-style: none;
        }
    </style>
    <div class="top_opbar">
        <div class="input-group" style="width: 775px;">
            <asp:DropDownList ID="SkeyType_DP" CssClass="form-control text_s inputgp" runat="server">
                <asp:ListItem Value="1" Selected="True">项目名称</asp:ListItem>
                <asp:ListItem Value="2">项目经理</asp:ListItem>
                <asp:ListItem Value="3">技术负责人</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="Skey_T" runat="server" class="form-control text_md inputgp" placeholder="关键字" />
            <asp:TextBox runat="server" ID="STime_T" class="form-control text_md inputgp" onclick="WdatePicker();" type="text" placeholder="开始时间" />
            <asp:TextBox runat="server" ID="ETime_T" class="form-control text_md" onclick="WdatePicker();" type="text" placeholder="结束时间" />
            <span class="input-group-btn">
                <asp:Button ID="BntSearch" runat="server" Text="查询" OnClick="BntSearch_Click" class="btn btn-primary" />
            </span>
        </div>
    </div>
    <div class="divbox" id="nocontent" runat="server" style="display: none">
        <%=lang.LF("暂无项目信息")%>
    </div>
    <ul class="nav nav-tabs">
        <li id="tab0"><a href="Projects.aspx"><%=lang.LF("所有")%></a></li>
        <li id="tab1"><a onclick="ShowTabss(0,0)"><%=lang.LF("未审核")%></a></li>
        <li id="tab2"><a onclick="ShowTabss(1,0)"><%=lang.LF("已审核")%></a></li>
        <li id="tab3"><a onclick="ShowTabss(2,0)"><%=lang.LF("未启动")%></a></li>
        <li id="tab4"><a onclick="ShowTabss(3,0)"><%=lang.LF("已启动")%></a></li>
        <li id="tab5"><a onclick="ShowTabss(4,0)"><%=lang.LF("已完成")%></a></li>
        <li id="tab6"><a onclick="ShowTabss(5,0)"><%=lang.LF("存档")%></a></li>
    </ul>
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" OnRowDataBound="EGV_RowDataBound"
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand" DataKeyNames="ID"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无相关信息！">
        <Columns>
            <asp:TemplateField HeaderText="选择" HeaderStyle-Width="4%">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%# Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="10%">
                <ItemTemplate>
                    <a href="ProjectsDetail.aspx?ProjectID=<%# Eval("ID","{0}")%>">
                        <%# Eval("ProName")%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="项目类型" HeaderStyle-Width="7%">
                <ItemTemplate>
                    <a href="Projects.aspx?type=<%#Eval("ZType") %>&skey=<%:Skey%>&mystatus=<%:Status %>">
                        <%# GetProType(Eval("ZType","{0}")) %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="项目价格" HeaderStyle-Width="9%">
                <ItemTemplate>
                    <%#Eval("ProPrice", "￥{0}.00")%>
                </ItemTemplate>
            </asp:TemplateField>
            <%--            <asp:TemplateField HeaderText="启动时间" HeaderStyle-Width="12%">
                <ItemTemplate>
                    <%#Eval("CDate", "yyyy-MM-dd")%>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="项目经理" HeaderStyle-Width="8%">
                <ItemTemplate>
                    <%#GetManageer(Eval("ProManageer","{0}"))%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="审核" HeaderStyle-Width="5%">
                <ItemTemplate>
                    <%# GetAudit(Eval("ZStatus","{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="项目管理" HeaderStyle-Width="7%">
                <ItemTemplate>
                    <%# GetProStatus(Eval("ZStatus", "{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="当前进度" HeaderStyle-Width="7%">
                <ItemTemplate>
                    <a href='<%#Eval("ID","ProjectsProcesses.aspx?ID={0}") %>'>
                        <div style="width: 90%; border: solid 1px green; height: 5px">
                            <div id="line" runat="server" style="background-color: #bddb52; height: 5px; float: left">
                            </div>
                        </div>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="申请时间" HeaderStyle-Width="12%">
                <ItemTemplate>
                    <%#Eval("CDate")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="20%">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("ID")%>'
                        CommandName="manage" CssClass="option_style"><i class="fa fa-pencil" title="修改"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument='<%# Eval("ID")%>'
                        CommandName="del" OnClientClick="if(!this.disabled) return confirm('确实要删除吗？');" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                    <asp:LinkButton ID="LbtnComments" runat="server" CommandArgument='<%# Eval("ID")%>'
                        CommandName="Comments" CssClass="option_style"><i class="fa fa-comments" title="评论"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton4" runat="server" CommandArgument='<%# Eval("ID")%>'
                        CommandName="Run" CssClass="option_style"><i class="fa fa-play-circle"></i><%#Eval("ZStatus", "{0}") != "0"&& Eval("ZStatus", "{0}") != "1" ? "启动" : "停止"%></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("ID")%>'
                        CommandName="Audit" CssClass="option_style"><i class="fa fa-check-square-o"></i><%#Eval("ZStatus", "{0}") == "0" ? "<font color=green>通过审核</font>" : "<font color=red>取消审核</font>"%></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:Button runat="server" ID="BatDel_Btn" CssClass="btn btn-primary" Text="批量删除" OnClick="BatDel_Btn_Click" OnClientClick="return confirm('确定要删除所选内容吗?');" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/DatePicker/WdatePicker.js"></script>
    <script>
        $(function () {
            $("#tab<%:MyStatus%>").addClass("active");
        })
        function getinfo(id) {
            location.href = "ProjectsDetail.aspx?ProjectID=" + id + "";
        }
        function fun(SkeyType_DP, Skey_T) {
            location.href = "Projects.aspx?SkeyType_DP=" + SkeyType_DP + "&Skey_T=" + Skey_T;
        }
        function ShowTabss(status, orderby) {
            location = "Projects.aspx?type=<%:SkeyType_DP.SelectedValue%>&skey=<%:Skey%>&mystatus=" + status;
        }

        HideColumn("6,7,8,9,10");
    </script>
    <style>
        .nav li a {
            cursor: pointer;
            text-decoration: none;
        }
    </style>
</asp:Content>
