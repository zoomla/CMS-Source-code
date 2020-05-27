<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JoinToComp.aspx.cs" Inherits="ZoomLaCMS.Plat.Common.JoinToComp" MasterPageFile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>加入企业</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="container platcontainer" style="padding-top:20px;">
        <div runat="server" id="join_div" visible="false">
            <div style="width: 400px; margin: 0 auto;">
                <div class="input-group">
                    <asp:TextBox runat="server" ID="Search_T" CssClass="form-control" placeholder="请输入公司全名或简称"></asp:TextBox>
                    <span class="input-group-btn">
                        <asp:Button runat="server" ID="Search_Btn" CssClass="btn btn-info" Text="搜索" OnClick="Search_Btn_Click" />
                    </span>
                </div>
                <asp:RequiredFieldValidator runat="server" ID="R1" ControlToValidate="Search_T" ForeColor="Red" ErrorMessage="公司名称不能为空" />
            </div>
            <div style="border-top: 1px solid #ddd; padding-top: 10px;">
                <div runat="server" id="nocomp_div" visible="false">
                    <div class="alert alert-warning" runat="server" id="nocomp_l"></div>
                </div>
                <div runat="server" id="hascomp_div" visible="false">
                    <div style="line-height: 100px;">
                        <span style="width: 20%; display: inline-block;">
                            <img runat="server" id="Logo_Img" style="width: 100px; height: 100px;" src="#" onerror="this.src='/Images/nopic.gif';" />
                        </span>
                        <span style="width: 20%; display: inline-block;">
                            <asp:Label runat="server" ID="CompName_L"></asp:Label>
                        </span>
                        <span style="width: 38%; display: inline-block;">
                            <asp:Label runat="server" ID="CompDesc_L"></asp:Label>
                        </span>
                        <span style="width: 20%; display: inline-block;" class="text-right">
                            <asp:Button runat="server" ID="AddComp_Btn" CssClass="btn btn-info" Text="提交申请" OnClientClick="return confirm('确定要提交申请吗?');" OnClick="AddComp_Btn_Click" />
                        </span>
                    </div>
                </div>
            </div>
        </div>
        <div runat="server" id="noauth_div" visible="false">
            <div class="alert alert-warning" runat="server" id="noauth_l"></div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>