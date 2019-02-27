<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Apply.aspx.cs" Inherits="Plat_Common_Apply" MasterPageFile="~/Common/Common.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>能力申请</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<center style="background:url(http://code.z01.com/user_login.jpg);background-position: center;left:0;top:0;right:0;bottom:0; position: absolute; background-repeat:no-repeat;background-size:cover;">
    <div id="Form_Div" runat="server" class="container userform">
        <div class="panel panel-default">
          <div class="panel-heading"><h2 class="pull-left">开通能力中心服务</h2><div class="clearfix"></div></div>
          <div class="panel-body">
            <p>请您认真填写下方申请表单以便官方客服为您提供周全服务！</p>
             <table class="table">
                <tr><td class="td_m">单位名称</td><td><ZL:TextBox runat="server" ID="CompName_T" AllowEmpty="false" class="form-control text_300"/><span class="r_red">*</span></td></tr>
                <tr><td>单位简称</td><td><ZL:TextBox runat="server" ID="CompShort_T" AllowEmpty="false" class="form-control text_300" MaxLength="4"/><span class="r_red">*</span></td></tr>
                <tr><td>联系人</td><td><ZL:TextBox runat="server" ID="Contact_T" AllowEmpty="false" ValidType="String" class="form-control text_300"/><span class="r_red">*</span></td></tr>
                <tr><td>邮箱</td><td><ZL:TextBox runat="server" ID="Email_T" AllowEmpty="false" ValidType="Mail" class="form-control text_300"/><span class="r_red">*</span></td></tr>
                <tr><td>联系电话<br />(手机或座机) </td><td><ZL:TextBox runat="server" ID="Mobile_T" AllowEmpty="false" ValidType="MobileOrPhone" class="form-control text_300"/><span class="r_red">*</span></td></tr>
                <tr><td>QQ</td><td><ZL:TextBox runat="server" ID="QQ_T" ValidType="Int" class="form-control text_300"/></td></tr>
                <tr><td>申请备注</td><td><asp:TextBox runat="server" ID="UserRemind_T" TextMode="MultiLine" class="form-control text_300" MaxLength="100" style="height:120px;resize:none;"></asp:TextBox></td></tr>
                <tr><td></td><td>
                    <asp:Button runat="server" ID="Save_Btn" OnClick="Save_Btn_Click" Text="提交申请" class="btn btn-info" />

                             </td></tr>
            </table>
          </div>
        </div>
    </div>
    <div id="Tip_Div" runat="server" class="container userform" visible="false">
        <div class="panel panel-default">
          <div class="panel-heading" style="font-size:2em;"> 恭喜！</div>
          <div class="panel-body text-center" runat="server" id="remind_div">
          <p> 您已经成功申请，系统审核后会以电子邮件通知您！</p>
              <div>
                  <a href="/" class="btn btn-info" style="color:#fff;"><i class="fa fa-home"></i> 回到首页</a>
              </div>
          </div>
        </div>
    </div>
</center>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script"></asp:Content>