<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SPwd.ascx.cs" Inherits="Manage_I_ASCX_SPwd" %>
<div class="pop_box panel panel-primary " style="width: 350px;margin:auto;" id="spwd_div" runat="server" visible="false">
    <div class="panel-heading">二级密码验证</div>
    <div class="panel-body">
        <asp:TextBox runat="server" ID="SPwd_T" placeholder="请输入二级密码" CssClass="form-control" />
        <asp:RequiredFieldValidator runat="server" ID="v1" ControlToValidate="SPwd_T" ValidationGroup="spwd" ForeColor="Red" Display="Dynamic" ErrorMessage="不能为空" />
    </div>
    <div class="panel-footer">
        <asp:Button runat="server" ID="Sure_Btn" Text="确定" OnClick="Sure_Btn_Click" CssClass="btn btn-primary" ValidationGroup="spwd" />
    </div>
</div>

<div class="pop_box panel panel-primary " style="width: 350px;margin:auto;" runat="server" id="nospwd_div" visible="false">
 <div class="panel-heading">二级密码验证</div>
    <div class="panel-body" style="text-align:center;">
        请输入二级验证码(相关帮助可检索官方文档）<br />
        <a href="http://www.z01.com/search/SearchList.aspx?keyword=%E4%BA%8C%E7%BA%A7%E9%AA%8C%E8%AF%81%E7%A0%81&ButtonSo=%E6%90%9C++%E7%B4%A2" class="btn btn-primary" title="点击前往" target="_blank">点击前往</a>
    </div>
</div>
