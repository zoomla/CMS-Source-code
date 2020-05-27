<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageCode.aspx.cs" Inherits="ZoomLaCMS.Manage.Template.Code.PageCode" MasterPageFile="~/Manage/I/Default.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>页面源码</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ul class="nav nav-tabs">
        <li class="active"><a href="#tabs0" data-toggle="tab">页面代码</a></li>
        <li><a href="#tabs1" data-toggle="tab">后台代码</a></li>
    </ul>
    <div class="tab-content">
        <div class="tab-pane active" id="tabs0">
            <div class="panel panel-default">
                <div class="panel-body">
                    <asp:TextBox runat="server" ID="PageStr_T" TextMode="MultiLine" CssClass="form-control" Style="height: 520px;"></asp:TextBox>
                </div>
                <div class="panel-footer">
                    <asp:Button runat="server" CssClass="btn btn-primary" Text="保存代码"  ID="SavePage_Btn" OnClick="SaveCode_Btn_Click" />
                    <a class="btn btn-primary" href="PageList.aspx">返回列表</a>
                </div>
            </div>
        </div>
        <div class="tab-pane" id="tabs1">
            <div class="panel panel-default">
                <div class="panel-body">
                    <asp:TextBox runat="server" ID="CodeStr_T" TextMode="MultiLine" CssClass="form-control" Style="height: 520px;"></asp:TextBox>
                </div>
                <div class="panel-footer">
                    <asp:Button runat="server" CssClass="btn btn-primary" Text="保存代码" ID="SaveCode_Btn" OnClick="SaveCode_Btn_Click" />
                    <a class="btn btn-primary" href="PageList.aspx">返回列表</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>