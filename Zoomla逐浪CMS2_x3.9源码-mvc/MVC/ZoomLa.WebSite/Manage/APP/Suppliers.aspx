<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Suppliers.aspx.cs" Inherits="ZoomLaCMS.Manage.APP.Suppliers"MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title><%=Resources.L.APP配置 %></title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ul class="nav nav-tabs" role="tablist">
        <li class="active"><a href="#app" aria-controls="app" role="tab" data-toggle="tab"><%=Resources.L.社会化登录 %></a></li>
        <li><a href="#other" aria-controls="other" role="tab" data-toggle="tab"><%=Resources.L.其它配置 %></a></li>
        <li><a href="#blog" aria-controls="other" role="tab" data-toggle="tab">博客平台</a></li>
    </ul>
    <div class="tab-content">
        <div role="tabpanel" class="tab-pane active" id="app">
            <table class="table table-striped table-bordered">
                <tr>
                    <td class="td_l">
                        <img style="cursor: pointer;" src="/App_Themes/Admin/Sina_2.png" />
                        <div><asp:CheckBox runat="server" ID="Sina_Enable_Chk" Text="开启新浪微博登录" /></div>
                    </td>
                    <td>
                        <div class="input-group margin_b2px">
                            <span class="input-group-addon">Key</span>
                            <input id="ASina" type="text" class="form-control text_300" runat="server" />
                            <span class="input-group-addon">Secret</span>
                            <input id="SSina" type="text" class="form-control text_300" runat="server" />
                        </div>
                        <div class="input-group">
                            <span class="input-group-addon"><%=Resources.L.回调 %></span>
                            <input id="SSinaURL" type="text" class="form-control" runat="server" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img style="cursor: pointer;" src="/App_Themes/Admin/QQ_2.png" />
                        <div><asp:CheckBox runat="server" ID="QQ_Enable_Chk" Text="开启QQ登录" /></div>
                    </td>
                    <td>
                        <div class="input-group margin_b2px">
                            <span class="input-group-addon">APPID</span>
                            <input id="QQ_Login_APPID_T" type="text" class="form-control text_300" runat="server" />
                            <span class="input-group-addon">Key</span>
                            <input id="QQ_Login_Key_T" type="text" class="form-control text_300" runat="server" />
                        </div>
                        <div class="input-group">
                            <span class="input-group-addon"><%=Resources.L.回调 %></span>
                            <input id="QQ_Login_CallBack_T" type="text" class="form-control" runat="server" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img style="cursor: pointer;" src="/App_Themes/Admin/Baidu_2.png" />
                        <div><asp:CheckBox runat="server" ID="Baidu_Enable_Chk" Text="开启百度登录" /></div>
                    </td>
                    <td>
                        <div class="input-group margin_b2px">
                            <span class="input-group-addon">Key</span>
                            <input id="ABaidu" type="text" class="form-control text_300" runat="server" />
                            <span class="input-group-addon">Secret</span>
                            <input id="SBaidu" type="text" class="form-control text_300" runat="server" />
                        </div>
                        <div class="input-group">
                            <span class="input-group-addon"><%=Resources.L.回调 %></span>
                            <input id="UBaidu" type="text" class="form-control" runat="server" />
                        </div>
                    </td>
                </tr>
                <%--<tr>
                    <td>
                        <img style="cursor: pointer;" src="/App_Themes/Admin/Kaixin_2.png" />
                    </td>
                    <td>
                        <div class="input-group margin_b2px">
                            <span class="input-group-addon">Key</span>
                            <input id="AKaixin" type="text" class="form-control text_300" runat="server" />
                            <span class="input-group-addon">Secret</span>
                            <input id="SKaixin" type="text" class="form-control text_300" runat="server" />
                        </div>
                        <div class="input-group">
                            <span class="input-group-addon"><%=Resources.L.回调 %></span>
                            <input id="SKaixiuUrl" type="text" class="form-control" runat="server" />
                        </div>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <i class="fa fa-wechat" style="font-size: 2em; color: green;"></i>
                        <span><%=Resources.L.微信登录 %></span>
                        <div><asp:CheckBox runat="server" ID="Wechat_Enable_Chk" Text="开启微信登录" /></div>
                    </td>
                    <td>
                        <div class="input-group margin_b2px">
                            <span class="input-group-addon">APPID</span>
                            <asp:TextBox runat="server" ID="WeChat_APPID_T" CssClass="form-control text_300"></asp:TextBox>
                            <span class="input-group-addon">Secret</span>
                            <asp:TextBox runat="server" ID="WeChat_Secret_T" CssClass="form-control text_300"></asp:TextBox>
                        </div>
                        <div class="input-group">
                            <span class="input-group-addon"><%=Resources.L.回调 %></span>
                            <asp:TextBox runat="server" ID="WeChat_URL_T" CssClass="form-control"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td>
                        <img style="cursor: pointer;" src="/App_Themes/Admin/Netease_2.png" /></td>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon">AppID</span><input id="chat_AppIDT" type="text" class="form-control text_300" runat="server" />
                            <span class="input-group-addon">AppKey</span><input id="chat_AppKeyT" type="text" class="form-control text_300" runat="server" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="Login_Save_Btn" CssClass="btn btn-primary" Text="保存社会化登录信息" OnClick="Login_Save_Btn_Click" /></td>
                </tr>
            </table>
        </div>
        <div role="tabpanel" class="tab-pane" id="other">
            <table class="table table-striped table-bordered">
                <tr>
                    <td class="td_m"><%=Resources.L.百度翻译 %></td>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon">Key</span>
                            <asp:TextBox runat="server" ID="Baidu_Translate_Key_T" CssClass="form-control text_300" />
                            <span class="input-group-addon">Secret</span>
                            <asp:TextBox runat="server" ID="Baidu_Translate_Secret_T" CssClass="form-control text_300" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>飞印打印</td>
                    <td>
                        <div class="input-group margin_b2px">
                            <span class="input-group-addon">Key</span>
                            <asp:TextBox runat="server" ID="Printer_Key_T" CssClass="form-control text_300" />
                            <span class="input-group-addon">Secret</span>
                            <asp:TextBox runat="server" ID="Printer_Secret_T" CssClass="form-control text_300" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" CssClass="btn btn-primary" ID="Other_Save_Btn" Text="保存配置" OnClick="Other_Save_Btn_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="tab-pane" id="blog">
            <table class="table table-striped table-bordered">
                <%--<tr>
                    <td class="td_m">新浪</td>
                    <td>
                        <div class="input-group margin_b2px">
                            <span class="input-group-addon">Key</span>
                            <asp:TextBox runat="server" ID="Sina_Blog_Key_T" CssClass="form-control text_300" />
                            <span class="input-group-addon">Secret</span>
                            <asp:TextBox runat="server" ID="Sina_Blog_Secret_T" CssClass="form-control text_300" />
                        </div>
                        <div class="input-group">
                            <span class="input-group-addon">回调</span>
                            <asp:TextBox runat="server" ID="Sina_Blog_CallBack_T" CssClass="form-control" />
                        </div>
                    </td>
                </tr>--%>
                <tr>
                    <td class="td_m">腾迅</td>
                    <td>
                        <div class="input-group margin_b2px">
                            <span class="input-group-addon">Key</span>
                            <asp:TextBox runat="server" ID="QQ_Blog_Key_T" CssClass="form-control text_300" />
                            <%--  <span class="input-group-addon">APPSecret</span>
                        <asp:TextBox runat="server" ID="QQ_Blog_Secret_T" CssClass="form-control text_300" />--%>
                        </div>
                        <div class="input-group">
                            <span class="input-group-addon">回调</span>
                            <asp:TextBox runat="server" ID="QQ_Blog_CallBack_T" CssClass="form-control" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="Blog_Save_Btn" CssClass="btn btn-primary" Text="保存博客信息" OnClick="Blog_Save_Btn_Click" /></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">
    .input-group {width: 700px;}
    .input-group-addon {min-width: 68px;}
</style>
</asp:Content>
