<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentList.aspx.cs" Inherits="ZoomLa.WebSite.Manage.I.Pay.PaymentList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>充值信息管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
        <li><a href=" <%=CustomerPageAction.customPath2 %>Main.aspx">工作台</a></li><li><a href="<%=CustomerPageAction.customPath2 %>Config/SiteInfo.aspx">系统设置</a></li>
        <li><a href='PayPlatManage.aspx'>在线支付平台</a></li>
        <li class="active">充值信息管理[<a runat="server" id="SwitchUrl_A">回收站</a>]</li>
        <div id="help" class="pull-right text-center"><a href="javascript::" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a></div>
        <div id="sel_box" class="padding5">
            <div class="input-group" style="width: 340px;margin-left:5px;float:left;">
                <asp:DropDownList ID="Search_Drop" runat="server" CssClass="form-control " Style="width:150px;border-right:none;">
		          <asp:ListItem Selected="True" Value="1">会员名</asp:ListItem>
		          <asp:ListItem Value="2">订单(充值)号</asp:ListItem>
		        </asp:DropDownList>
		        <asp:TextBox ID="Search_T" runat="server" CssClass="form-control" style="width:150px;border-right:none;"></asp:TextBox>
                <span class="input-group-btn">
		        <asp:LinkButton runat="server" ID="Search_Btn" OnClick="Serarch_Btn_Click" CssClass="btn btn-default"><span class="fa fa-search"></span></asp:LinkButton>
		        </span>
            </div>
            <div class="pull-left" style="margin-left:10px;">
                金额搜索:
                <asp:TextBox ID="MinMoney_T" runat="server" CssClass="form-control text_x"></asp:TextBox>
                -
                <asp:TextBox ID="MaxMoney_T" runat="server" CssClass="form-control text_x"></asp:TextBox>
                <asp:LinkButton ID="SearchMoney_Btn" CssClass="btn btn-default" OnClick="SearchMoney_Btn_Click" runat="server"><span class="fa fa-search"></span> </asp:LinkButton>
            </div>
        </div>
    </ol>
    <ul class="nav nav-tabs" role="tablist">
        <li id="tab0" class="active"><a href="PaymentList.aspx" role="tab">所有记录</a></li>
        <li id="tab3"><a href="PaymentList.aspx?status=3" role="tab">成功记录</a></li>
        <li id="tab1"><a href="PaymentList.aspx?status=1" role="tab">失败记录</a></li>
        <li id="tab2"><a href="PaymentList.aspx?remark=donate" role="tab">捐赠记录</a></li>
    </ul>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td></td><td><asp:LinkButton ID="IDAsc" OnClick="IDAsc_Click" runat="server">ID</asp:LinkButton></td><td>会员名</td><td>支付单号</td><td>订单(充值)号</td><td>支付平台</td><td><asp:LinkButton ID="PriceAsc" OnClick="PriceAsc_Click" runat="server">金额</asp:LinkButton></td><td>实际金额</td><td>状态</td><td>结果</td>
            <td>发起时间</td>
            <td><asp:LinkButton ID="DateAsc" OnClick="DateAsc_Click" runat="server">完成时间<span class="fa fa-arrow-up"></span></asp:LinkButton></td><td>操作</td>
        </tr>
        <ZL:ExRepeater ID="RPT" runat="server" BoxType="dp" OnItemCommand="RPT_ItemCommand" PageSize="20" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='12'><div class='text-center'>" PageEnd="</div></td></tr>">
        <ItemTemplate>
            <tr ondblclick="location.href='DepostDetail.aspx?ID=<%#Eval("PaymentID") %>';">
                <td>
                    <input name="idchk" value="<%#Eval("PaymentID") %>" type="checkbox" />
                </td>
                <td><%#Eval("PaymentID") %></td>
                <td><a href="PaymentList.aspx?userid=<%#Eval("UserID") %>"><%#Eval("UserName") %></a> </td>
                <td><%#Eval("PayNo") %></td>
                <td><a href="DepostDetail.aspx?ID=<%#Eval("PaymentID") %>"><%#Eval("PaymentNum") %></a></td>
                <td>
                    <%#Eval("PayPlatName") %>
                </td>
                <td>
                    <%#Eval("MoneyPay","{0:f2}") %>
                </td>
                <td>
                    <%#Eval("MoneyTrue","{0:f2}") %>
                </td>
                <td>
                    <%#GetStatus(Eval("Status","{0}")) %>
                </td>
                <td> <%#GetCStatus(Eval("CStatus","{0}")) %></td>
                <td><%#Eval("PayTime") %></td>
                <td>
                    <%#Eval("SuccessTime")%>
                </td>
                <td>
                    <asp:LinkButton runat="server" CommandName="Del" OnClientClick="return confirm('是否确定删除?')" CommandArgument='<%#Eval("PaymentID") %>' CssClass="option_style"><i class="fa fa-trash-o <%=Status != -99 ? "" : "hidden" %>" title="删除"></i></asp:LinkButton>
                    <asp:LinkButton runat="server"  CommandName="RecDel" OnClientClick="return confirm('回收站数据删除不可恢复，确定要删除吗?')" CommandArgument='<%#Eval("PaymentID") %>' CssClass="option_style"><i class="fa fa-trash-o <%=Status == -99 ? "" : "hidden" %>" title="删除"></i></asp:LinkButton>
                    <asp:LinkButton runat="server"  CommandName="Rec" OnClientClick="return confirm('确定恢复这条数据吗?')" CommandArgument='<%#Eval("PaymentID") %>' CssClass="option_style"><i class="fa fa-recycle <%=Status == -99 ? "" : "hidden" %>" title="恢复"></i></asp:LinkButton>
                    <a href="DepostDetail.aspx?ID=<%#Eval("PaymentID") %>" class="option_style"><i class="fa fa-globe" title="浏览"></i>浏览</a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate></FooterTemplate>
    </ZL:ExRepeater>
    </table>
    <div runat="server" id="normal_div" visible="false">
        <asp:Button ID="Dels_Btn" runat="server" OnClientClick="return confirm('是否确定所选项删除?')" CssClass="btn btn-primary" Text="批量删除" OnClick="Dels_Btn_Click" />
        <asp:LinkButton ID="ExportExcel_Btn" runat="server" CssClass="btn btn-primary" OnClick="ExportExcel_Btn_Click"><span class="fa fa-file-excel-o"></span> 导出Excel</asp:LinkButton>
    </div>
    <div runat="server" id="recycle_div" visible="false">
        <asp:Button runat="server" CssClass="btn btn-primary" ID="Recover_Btn" Text="批量恢复" OnClick="Recover_Btn_Click" />
        <asp:Button runat="server" CssClass="btn btn-primary" ID="RealDel_Btn" Text="彻底删除" OnClick="RealDel_Btn_Click" />
        <asp:Button runat="server" CssClass="btn btn-primary" ID="ClearRecycle_Btn" Text="清空回收站"
             OnClick="ClearRecycle_Btn_Click" OnClientClick="return confirm('确定要清空回收站吗?');"  />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
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
            $("#chkAll").click(function () {
                var checkall = $(this)[0];
                $("input[name=idchk]").each(function () {
                    $(this)[0].checked = checkall.checked;
                });
            });
            var status = '<%=Status %>';
            var remark = '<%=Remark %>';
            if (remark == 'donate') { status = 2;}
            if (status == '-99') { $(".nav-tabs").hide(); }
            $(".nav-tabs li").removeClass("active");
            $("#tab"+status).addClass("active");
        });
    </script>
</asp:Content>
