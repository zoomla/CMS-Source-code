<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BKConfig.aspx.cs" Inherits="Manage_Guest_BKConfig" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>百科配置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="Creat_tips">
    <div class="alert alert-danger fade in margin_b2px">
      <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
      <h4>提示!</h4>
      <p style="padding-left: 50px; line-height: 40px;"> <strong> 
          本页内容针对百科模块进行相关设置，可点击<a href="/Guest/BaiKe/" target="_blank" class="btn btn-info">百科词条</a>访问该页面
        </strong> </p>
      <p>注意事项: 查看、创建、编辑词条权限如不勾选用户组,则代表无用户限制! </p>
    </div>
  </div>
    <ul class="nav nav-tabs" role="tablist">
    <li role="presentation" class="active"><a href="#auth" aria-controls="auth" role="tab" data-toggle="tab">权限管理</a></li>
    <li role="presentation"><a href="#exp" aria-controls="exp" role="tab" data-toggle="tab">奖励设定</a></li>
    </ul>
    <div class="tab-content">
        <div role="tabpanel" class="tab-pane active" id="auth">
            <table class="table table-striped table-bordered table-hover">
                <tr>
                <td class="text-right td_l">查看权限:</td>
                <td>
                    <asp:Repeater ID="selGroup_Rpt" EnableViewState="false" runat="server">
                        <ItemTemplate>
                            <label><input type="checkbox" name="selGroup" <%#GetCheck(1) %> value="<%#Eval("GroupID") %>" /><%#Eval("GroupName") %></label> 
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td class="text-right">创建权限:</td>
                <td>
                    <asp:Repeater ID="CreateGroup_Rpt"  EnableViewState="false" runat="server">
                        <ItemTemplate>
                            <label><input type="checkbox" name="CreateGroup" <%#GetCheck(2) %> value="<%#Eval("GroupID") %>" /><%#Eval("GroupName") %></label> 
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td class="text-right">编辑权限:</td>
                <td>
                     <asp:Repeater ID="EditGroup_Rpt"  EnableViewState="false" runat="server">
                        <ItemTemplate>
                            <label><input type="checkbox" name="EditGroup" <%#GetCheck(3) %> value="<%#Eval("GroupID") %>" /><%#Eval("GroupName") %></label>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            </table>
        </div>
        <div role="tabpanel" class="tab-pane" id="exp">
            <table class="table table-striped table-bordered table-hover">
                <tr>
            <td class="text-right td_l">奖励类型:</td>
            <td>
                <asp:RadioButtonList ID="PointType_R" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Point" Selected="True" Text="积分"></asp:ListItem>
                    <asp:ListItem Value="SIcon" Text="银币"></asp:ListItem>
                    <asp:ListItem Value="UserPoint" Text="点卷"></asp:ListItem>
                    <asp:ListItem Value="DummyPoint" Text="虚拟币"></asp:ListItem>
                    <asp:ListItem Value="Credit" Text="信誉点"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="text-right">创建奖励:</td>
            <td>
                <asp:TextBox ID="CreatePoint_T" Text="0" runat="server" CssClass="form-control text_md num"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-right">编辑奖励:</td>
            <td>
                <asp:TextBox ID="EditPoint_T" runat="server" Text="0" CssClass="form-control text_md num"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-right">推荐奖励:</td>
            <td>
                <asp:TextBox ID="RemmPoint_T" runat="server" Text="0" CssClass="form-control text_md num"></asp:TextBox> <span>*被管理员设为推荐时所获奖励</span>
            </td>
        </tr>
            </table>
        </div>
    </div>
    <div class="text-center">
        <asp:Button ID="Save_B" runat="server" OnClientClick="return CheckData()" OnClick="Save_B_Click" CssClass="btn btn-primary" Text="保存配置" />
        <a href="javascript:;" onclick="clearData()" class="btn btn-primary">重置参数</a>
    </div>
    <script src="/JS/ZL_Regex.js"></script>
    <script>
        function clearData() {
            $("input[type='text']").val('0');
            $("input[type='checkbox']").each(function (i, v) {
                v.checked = false;
            });
        }
        function CheckData() {
            var bl = true;
            $(".num").each(function () {
                if (!ZL_Regex.isNum($(this).val())) {
                    alert("数据格式不正确!");
                    $(this).focus();
                    bl = false;
                    return false;
                }
            });
            return bl;
        }
    </script>
</asp:Content>

