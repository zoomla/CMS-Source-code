<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyJoinComp.aspx.cs" Inherits="Plat_Common_ApplyJoinComp" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>加入公司</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<center style="background:url(http://code.z01.com/user_login.jpg);background-position: center;left:0;top:0;right:0;bottom:0; position: absolute; background-repeat:no-repeat;background-size:cover;">
<div class="userform">
    <div class="panel panel-default">
        <div class="panel-heading"><h2 class="pull-left">申请加入企业</h2><div class="clearfix"></div></div>
        <div class="panel-body">
            <div>
                <div id="apply_div" runat="server">
                    <div class="input-group" style="width:380px;">
                        <asp:TextBox id="Skey_T" runat="server" CssClass="form-control text_300" placeholder="请输入公司全名或简称" />
                        <span class="input-group-btn">
                            <asp:Button ID="Skey_Btn" runat="server" CssClass="btn btn-info" Text="搜索公司" OnClick="Skey_Btn_Click" />
                        </span>
                    </div>
                </div>
                <div id="cancel_div" runat="server" visible="false">
                    <div class="alert alert-danger"><asp:Label ID="Mess_L" runat="server"></asp:Label>  
                    <asp:Button ID="Cancel_B" runat="server" CssClass="btn btn-info" Text="取消申请" OnClientClick="return confirm('确定要取消申请吗?');" OnClick="Cancel_B_Click" /></div>
                </div>
            </div>
            <table id="comp_tab" runat="server" visible="false" class="table">
                <tr><td><asp:Image ID="Logo_Img" runat="server" style="height:65px;height:65px;" /></td><td></td></tr>
                <tr><td class="td_m">公司名称</td><td><asp:Label ID="CompName_T" runat="server" /></td></tr>
                <tr><td>创建时间</td><td><asp:Label ID="CreateTime_T" runat="server" /></td></tr>
                <tr><td>公司简介</td><td><asp:Label ID="Desc_T" runat="server" /></td></tr>
                <tr>
                    <td colspan="2" class="text-center">
                        <asp:Button ID="Apply_B" runat="server" CssClass="btn btn-info" Text="提交申请" OnClientClick="return confirm('确定要提交申请吗?');" OnClick="Apply_B_Click" />
                    </td>
                </tr>
            </table>
            <div id="empty_div" runat="server" class="alert alert-info" visible="false"><asp:Label ID="Empty_L" runat="server"/></div>
        </div>
    </div>
</div>
<asp:HiddenField ID="ualyId_Hid" runat="server" />
<asp:HiddenField ID="compId_Hid" runat="server" />
</center>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style>
#empty_div{margin:30px;width:380px;}
.alert{font-size:14px;}
.userform{min-height:220px;}
</style>
</asp:Content>