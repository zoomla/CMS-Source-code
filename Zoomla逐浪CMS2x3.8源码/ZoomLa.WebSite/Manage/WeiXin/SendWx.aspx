<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendWx.aspx.cs" Inherits="manage_WeiXin_SendWx" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>消息群发</title>
    <style>
        .wxlist li{cursor:pointer;}
        .wxlist li .badge{display:none;}
        .wxlist li .active{display:inline-block;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-2">
                <div class="panel panel-default">
                  <!-- Default panel contents -->
                  <div class="panel-heading">选择公众号 <span class="pull-right"><a href="javascript:;" onclick="CheckAll()">全选</a></span></div>
                  <ul class="list-group wxlist">
                      <asp:Repeater ID="WxApp_RPT" runat="server">
                          <ItemTemplate>
                                <li class="list-group-item wx_option">
                                    <%#Eval("Alias") %> <span class="badge"><span class="fa fa-check"></span></span><input type="checkbox" name="appids" value="<%#Eval("ID") %>" style="display:none" />
                                </li>
                          </ItemTemplate>
                      </asp:Repeater>
                  </ul>
                </div>
            </div>
            <div class="col-md-10">
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td class="td_m">选择分组:</td>
                        <td>
                            <asp:DropDownList ID="WxGroup_D" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>信息类型:</td>
                        <td>
                            <label><input type="radio" name="msgtype_rad" value="text" checked="checked" />文字</label>
                        </td>
                    </tr>
                    <tbody class="op" id="text_tb">
                        <tr>
                            <td>信息内容:</td>
                            <td><asp:TextBox ID="TxtContent" runat="server" TextMode="MultiLine" CssClass="form-control m715-50" style="height:120px;" />
                                <asp:RequiredFieldValidator ID="v1" runat="server" ControlToValidate="TxtContent" Display="Dynamic" ForeColor="Red" SetFocusOnError="True" ErrorMessage="内容不能为空!" ValidationGroup="text"/>
                            </td>
                        </tr>
                     <tr>
                        <td></td>
                        <td><asp:Button ID="SendAll_B" runat="server" OnClick="SendAll_B_Click" Text="发送信息" CssClass="btn btn-primary" OnClientClick="disBtn(this,3000);" ValidationGroup="text" /></td>
                    </tr>
                    </tbody>
                   
                    
                </table>
            </div>
        </div>
        
    </div>
    
<div class="alert alert-info" role="alert">
    1,根据微信群发规则，认证订阅好与服务号每天可使用全组群发1次,每月累计可使用群发4次(不管是全组群发还是分组群发)<br />
    2,群发后,根据微信服务器网络状况,可能会延迟10-20分钟左右用户才能收到信息
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        $(function () {
            $("input[name=msgtype_rad]").click(function () {
                var val = this.value;
                $(".op").hide();
                $("#" + val + "_tb").show();
            });
            $("input[name=msgtype_rad]")[0].checked = true;
            //初始化选中值
            var appid = '<%=AppID %>';
            if (appid!=""&&appid!="0") {
                $("input[value='" + appid + "']")[0].checked = true;
            }
            InitWxList();
            $(".wxlist li").click(function () {
                var checks = $(this).find("input[name='appids']")[0];
                checks.checked = !checks.checked;
                InitWxList();
            });
        })
        //初始化微信列表状态
        function InitWxList() {
            $(".wxlist li").each(function (i, v) {
                if ($(v).find("input[name='appids']")[0].checked)
                    $(v).find('.badge').addClass("active");
                else
                    $(v).find('.badge').removeClass("active");
            });
        }
        function CheckAll() {
            $(".wxlist li input[name='appids']").each(function (i, v) {
                $(v)[0].checked = true;
            });
            InitWxList();
        }
    </script>
</asp:Content>