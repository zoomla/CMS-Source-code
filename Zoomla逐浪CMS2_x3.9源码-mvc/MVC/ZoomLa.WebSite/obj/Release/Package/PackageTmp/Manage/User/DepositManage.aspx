<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepositManage.aspx.cs" Inherits="ZoomLaCMS.Manage.User.DepositManage"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>出金管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb navbar-fixed-top">
        <li><a href='../Main.aspx'>工作台</a></li><li><a href='UserManage.aspx'>用户管理</a></li><li><a href='<%=Request.RawUrl %>'>用户出金</a></li>
        <div id="help" class="pull-right text-center"><a href="javascript::" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a></div>
            <div id="sel_box" class="padding5">
                <div class="input-group sel_box">
                    <asp:TextBox runat="server" ID="Search_T" class="form-control" placeholder="检索当前位置" />
                    <span class="input-group-btn">
                         <asp:LinkButton runat="server" CssClass="btn btn-default" ID="Search_Btn" OnClick="Search_Btn_Click">搜索</asp:LinkButton>
                            <asp:Button runat="server" ID="Search_Btn_Hid" OnClick="Search_Btn_Click" style="display:none;" />
                    </span>
                </div>
                </div>
    </ol>

    <ul class="nav nav-tabs" role="tablist">
    <li role="presentation" data-type="0"><a href="?state=0">未处理</a></li>
    <li role="presentation" data-type="99"><a href="?state=99">已确认</a></li>
    <li role="presentation" data-type="-1"><a href="?state=-1">已拒绝</a></li>
    <li role="presentation" data-type="-2"><a href="?state=-2">全部</a></li>
    </ul>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="td_xs"></td>
            <td>用户名</td><td>用户组</td><td>出金金额</td><td>申请时间</td>
            <td>出金状态</td><td>帐户信息</td>
            <td>开户银行</td><td>管理员备注</td><td>操作</td>
        </tr>
        <ZL:ExRepeater ID="Deposit_RPT" runat="server" OnItemCommand="Deposit_RPT_ItemCommand" PageSize="10" PagePre="<tr id='page_tr'><td><input type='checkbox' id='chkAll'/></td><td colspan='10' id='page_td'>" PageEnd="</td></tr>">
            <ItemTemplate>
                <tr>
                    <td><input type="checkbox" name="idchk" value="<%#Eval("Y_ID") %>" /></td>
             <%--   <td><%#Eval("Y_ID") %></td>--%>
                    <td><a href="javascript:;" onclick="ShowUserDiag(<%#Eval("UserID") %>)" title="<%#Eval("UserID") %>"><%#Eval("YName") %></a><input type="hidden" name="uname_hid" value="<%#Eval("YName") %>" /></td>
                    <td><%#Eval("GroupName") %></td>
                    <td><i class="fa fa-dollar"> </i><%#Eval("money","{0:f0}") %><input type="hidden" name="price_hid" value="<%#Eval("money") %>" /></td>
                    <td><%#Eval("STime","{0:yyyy年MM月dd日 HH:mm}") %></td>
                    <td><%#GetStatus() %></td>
                    <td class="text_300">
                        <div>开户人：<%#Eval("PeopleName") %></div>
                        <div>银行：<%#Eval("Bank") %></div>
                        <div>卡号：<%#Eval("Account") %></div></td>
                    <td><%#Eval("Remark") %></td>
                    <td><%#Eval("Result") %></td>
                    <td>
                        <asp:Button runat="server" CommandName="Check" Text="确认" style="display:none;" CommandArgument='<%#Eval("Y_ID") %>' OnClientClick="return confirm('要确认该笔出金吗?');"></asp:Button>
                        <%#GetOP() %>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>
    </table>
    <div id="backdepos_div" style="display:none;">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td style="width:15%" class="text-right">用户名：</td>
                <td><span id="uname_span"></span></td>
            </tr>
<%--            <tr>
                <td class="text-right">将返回金额：</td>
                <td><span id="price_span"></span> (拒绝后此金额将退回给用户)</td>
            </tr>--%>
            <tr>
                <td class="text-right">拒绝理由：</td>
                <td><asp:TextBox ID="BackDecs_T" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" class="text-center">
                    <asp:HiddenField ID="backID_Hid" runat="server" />
                    <asp:Button ID="BackDe_Btn" CssClass="btn btn-primary" OnClick="BackDe_B_Click" runat="server" Text="确认" />
                    <button type="button" onclick="CloseBack()" class="btn btn-primary">取消</button>
                </td>
            </tr>
        </table>
    </div>
    <asp:Button ID="CheckDepos_B" CssClass="btn btn-primary" runat="server" OnClick="CheckDepos_B_Click" OnClientClick="return confirm('要确认选定出金吗?');" Text="批量确认" />
    <div class="alert alert-info margin_t5">拒绝出金后,请手动为用户增加金额</div>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        $().ready(function () {
            $("#chkAll").click(function () {//EGV 全选
                selectAllByName(this, "idchk");
            });
        });
        //默认滑动门
        function checkState(state) {
            $("[data-type]").removeClass('active');
            $("[data-type='" + state + "']").addClass('active');
        }
        var userDiag = new ZL_Dialog();
        userDiag.maxbtn = false;
        function ShowUserDiag(uid) {
            userDiag.title = "用户信息";
            userDiag.url = "Userinfo.aspx?id=" + uid;
            userDiag.ShowModal();
        }
        var backDiag = new ZL_Dialog();
        function ShowBack(id,obj) {
            backDiag.title = "拒绝理由";
            backDiag.content = "backdepos_div";
            backDiag.ShowModal();
            $("#backID_Hid").val(id);
            $("#BackDecs_T").val("");
            var curdata = $(obj).parent().parent();
            $("#uname_span").text($(curdata).find("[name=uname_hid]").val());
            var price = parseInt($(curdata).find("[name=price_hid]").val());
            $("#price_span").text(price+"+"+(price*0.1)+"="+(price+(price*0.1)));
        }
        function CloseBack() {
            backDiag.CloseModal();
        }
        function checkFunc(obj) {
            $(obj).prev().click();
        }
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
    </script>
</asp:Content>
