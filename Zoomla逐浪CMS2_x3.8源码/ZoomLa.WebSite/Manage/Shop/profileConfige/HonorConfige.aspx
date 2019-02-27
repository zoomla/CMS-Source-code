<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HonorConfige.aspx.cs" Inherits="manage_Shop_profileConfige_HonorConfige" EnableViewStateMac="false"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>兑现配置信息</title>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="spacingtitle" align="center">
                <asp:Literal ID="LTitle" runat="server" Text="修改兑现信息配置"></asp:Literal>
            </td>
        </tr>
        <tr class="tdbg">
            <td valign="top" style="margin-top: 0px; margin-left: 0px;">
              <table width="100%" cellpadding="2" cellspacing="1" style="background-color: white;">
                 <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td class="tdbgleft"><strong>订单返利最低兑现金额：</strong></td>
                    <td>
                        <asp:TextBox ID="txtAcou" runat="server"  class="form-control pull-left"></asp:TextBox>(即订单返利最低兑现金额为该值得整数倍)
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ErrorMessage="订单返利最低兑现金额不能为空!" ControlToValidate="txtAcou"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td class="tdbgleft"><strong>分站返利最低兑现金额：</strong></td>
                    <td>
                        <asp:TextBox ID="txtsite" runat="server"  class="form-control pull-left"></asp:TextBox>(即分站返利兑现金额为该值得整数倍)
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ErrorMessage="分站返利最低兑现金额不能为空!" ControlToValidate="txtsite"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td class="tdbgleft"><strong>Email内容：</strong><br />提示内容支持HTML，提示内容中可用标签说明如下：<br />
                         {$UserName}会员名称<br />{$DataTime}申请时间<br />{$SiteName}网站名称<br />{$SiteUrl}网站网址</td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server"  class="form-control pull-left" TextMode="MultiLine" 
                            Height="82px" ></asp:TextBox>(即管理员确认支付时提示会员的Email格式)
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ErrorMessage="提示信息不能为空!" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td class="tdbgleft"><strong>手机短信内容：</strong></td>
                    <td>
                        <asp:TextBox ID="txtMobile" runat="server"  class="form-control pull-left" TextMode="MultiLine" 
                            Height="82px" ></asp:TextBox>(即管理员确认支付时提示会员的短信格式)
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ErrorMessage="短信内容不能为空!" ControlToValidate="txtMobile"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                   <td class="tdbgleft"><strong>注意事项：</strong></td>
                   <td>
                    <textarea id="info" name="infoeditor" class="form-control" rows="10" style="max-width:100%" runat="server"></textarea>
                        <%=Call.GetUEditor("MsgContent_T",4) %>
                    </td>
                 </tr>
             </table>
            </td>
        </tr>
    </table>
    <table id="TABLE1">
        <tr>
            <td align="left" style="height: 59px">
                &nbsp; &nbsp;
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="修改" runat="server"  onclick="EBtnSubmit_Click" />
                <input type="button" class="btn btn-primary" name="Button2" value="返回列表" onclick="location.href = '../profile/HonorManage.aspx'" id="Button2" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>
