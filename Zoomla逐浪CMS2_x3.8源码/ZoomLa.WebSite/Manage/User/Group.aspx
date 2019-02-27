<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Group.aspx.cs" Inherits="ZoomLa.WebSite.Manage.User.Group" EnableViewStateMac="false"  MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css"  rel="stylesheet"/>
    <script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
    <script type="text/javascript" src="/JS/ZL_Regex.js"></script>
    <title><%=Resources.L.会员组编辑 %></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:HiddenField runat="server" ID="LNav_Hid" Value="<%=Resources.L.添加房间 %>" />
  <table class="table table-striped table-bordered table-hover">
    <tr>
      <td class="spacingtitle" colspan="2" align="center"><asp:Literal ID="LTitle" runat="server" Text="<%$Resources:L,添加会员组 %>"></asp:Literal></td>
    </tr>
    <tr>
        <td class="tdbgleft td_l" ><strong><%=Resources.L.父会员组名称 %>：</strong></td>
        <td><asp:Label ID="Label1" runat="server"></asp:Label></td>
    </tr>
    <tr>
      <td class="tdbgleft"><strong><%=Resources.L.会员组名称 %>：</strong></td>
      <td><asp:TextBox class="form-control text_300" ID="TxtGroupName" runat="server" MaxLength="30" />
          <span class="rd_red">*</span>
          <asp:Label ForeColor="Red" ID="CheckName" runat="server" Text=""></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ErrorMessage="RequiredFieldValidator" ControlToValidate="TxtGroupName"><%=Resources.L.会员组名称不能为空 %></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
      <td class="tdbgleft"><strong><%=Resources.L.会员组别名 %>：</strong></td>
      <td><asp:TextBox class="form-control text_300"  ID="OtherName" runat="server"  MaxLength="200" /></td>
    </tr>
    <tr>
      <td class="tdbgleft"><strong><%=Resources.L.会员组说明 %>：</strong></td>
      <td><asp:TextBox class="form-control text_300" ID="TxtDescription" runat="server" TextMode="MultiLine"  Height="60px" /></td>
    </tr>
    <tr>
      <td class="tdbgleft"><strong><%=Resources.L.是否为招生人员 %>：</strong></td>
      <td>
           <input type="checkbox" runat="server" id="txt_Enroll" class="switchChk" />
      </td>
    </tr>
      <tr>
          <td class="tdbgleft"><strong><%=Resources.L.班级成员 %>：</strong></td>
          <td>
              <asp:RadioButtonList ID="ClassEnroll_Radio" RepeatDirection="Horizontal" runat="server">
                  <asp:ListItem Value="" Selected="True" Text="<%$Resources:L,学生 %>"></asp:ListItem>
                  <asp:ListItem Value="isteach" Text="<%$Resources:L,教师 %>"></asp:ListItem>
                  <asp:ListItem Value="isfamily" Text="<%$Resources:L,家长 %>"></asp:ListItem>
              </asp:RadioButtonList>
          </td>
      </tr>
    <tr>
      <td class="tdbgleft"><strong><%=Resources.L.是否注册可选 %>：</strong></td>
      <td>
          <input type="checkbox" runat="server" id="RBLReg" class="switchChk" checked="checked" />
     </td>
    </tr>
    <tr>
      <td class="tdbgleft"><strong><%=Resources.L.是否为企业用户组 %>：</strong></td>
      <td>
          <input type="checkbox" runat="server" id="RBcompanyG" class="switchChk" />
      </td>
    </tr>
    <tr>
      <td class="tdbgleft"><strong><%=Resources.L.是否为VIP用户组 %>：</strong></td>
      <td>
          <input type="checkbox" runat="server" id="RBVipG" class="switchChk" />
     </td>
    </tr>
    <tr>
      <td class="tdbgleft"><strong><%=Resources.L.VIP默认级别 %>：</strong></td>
      <td><asp:TextBox class="form-control text_md" runat="server" ID="txtVIPNum" Text="0"></asp:TextBox>
        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtVIPNum" ErrorMessage="<%$Resources:L,请输入有效数字 %>" MaximumValue="999" MinimumValue="0" Type="Integer"></asp:RangeValidator></td>
    </tr>
    <tr>
        <td class="tdbgleft"><strong><%=Resources.L.余额升级费用 %>：</strong></td>
        <td>
            <asp:TextBox class="form-control text_md int" ID="UpGradeMoney" runat="server">0</asp:TextBox><span><%=Resources.L.为0或空则不允许购买 %></span></td>
    </tr>
      <tr>
          <td class="tdbgleft"><strong><%=Resources.L.银币升级费用 %>：</strong></td>
          <td>
              <asp:TextBox class="form-control text_md int" ID="UpSIcon_T" runat="server">0</asp:TextBox></td>
      </tr>
      <tr>
          <td class="tdbgleft"><strong><%=Resources.L.积分升级费用 %>：</strong></td>
          <td>
              <asp:TextBox class="form-control text_md int" ID="UpPoint_T" runat="server">0</asp:TextBox></td>
      </tr>
    <tr>
      <td class="tdbgleft"><strong><%=Resources.L.返利比率 %>：</strong></td>
      <td><asp:TextBox class="form-control text_md" ID="txtRebateRate" runat="server">0</asp:TextBox><span>值范围:0-100,0为不启用</span>
        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<%$Resources:L,请输入有效数据 %>" Operator="GreaterThanEqual" Type="Double" ValueToCompare="0" Display="Dynamic" ForeColor="Red" ControlToValidate="txtRebateRate"></asp:CompareValidator>
        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="<%$Resources:L,请输入有效数据 %>" Operator="LessThanEqual" Type="Double" ValueToCompare="100" ForeColor="Red" Display="Dynamic"  ControlToValidate="txtRebateRate"></asp:CompareValidator></td>
    </tr>
    <tr>
      <td class="tdbgleft"><strong><%=Resources.L.信誉值 %>：</strong></td>
      <td><asp:TextBox class="form-control text_md" ID="txtCreit" runat="server">0</asp:TextBox>
        <span>(<%=Resources.L.用户达到多少信誉值可升级为此等级 %>)</span>
        <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<%$Resources:L,请输入有效数据 %>" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="0"  ControlToValidate="txtCreit"></asp:CompareValidator></td>
    </tr>
    <tr>
      <td class="tdbgleft"><strong><%=Resources.L.付费方式 %>：</strong></td>
      <td><span><%=Resources.L.每个包年会员费为 %></span><asp:TextBox class="form-control text_xs" ID="Byear" runat="server">50</asp:TextBox><span>,<%=Resources.L.每个月最多使用 %></span><asp:TextBox class="form-control text_xs" ID="Payment" runat="server">50</asp:TextBox><span><%=Resources.L.篇文章 %></span>
        <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="请输入有效数据！" Operator="GreaterThanEqual" Type="Double" ValueToCompare="0"  ControlToValidate="Payment"></asp:CompareValidator></td>
    </tr>
  </table>
    <table class="table table-striped table-bordered table-hover">
    <tr>
      <td colspan="2" class="text-center"><asp:HiddenField ID="HdnGroupID" runat="server" />
        <asp:Button ID="EBtnSubmit" Text="<%$Resources:L,保存设置 %>" OnClick="EBtnSubmit_Click" runat="server" class="btn btn-primary"/>
        <input name="Cancel" type="button" class="btn btn-primary" id="Cancel" value="<%=Resources.L.取消 %>" onclick="location.href = 'GroupManage.aspx'" /></td>
    </tr>
  </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        function CheckAll(spanChk)//CheckBox全选
        {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
        }
        function SelectRule() {
            window.open('RuleList.aspx?id=<%=Request.QueryString["id"] %>', '', 'width=600,height=450,resizable=0,scrollbars=yes');
    }
    function VIP() {
        var dd = document.getElementByNames("RBVipG");
    }
    $(function () {
        ZL_Regex.B_Num(".int");
    })
</script>
</asp:Content>